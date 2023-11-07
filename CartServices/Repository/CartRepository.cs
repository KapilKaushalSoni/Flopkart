using CartServices.Models;
using CartServices.ViewModels;
using Grpc.Net.Client;
using GrpcService1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CartServices.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _carts;
        private readonly IMongoCollection<Product> _products;
        private readonly IConfiguration configuration;
        private readonly ILogger<CartRepository> logger;

        public CartRepository(IConfiguration configuration,ILogger<CartRepository> logger)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);
            _carts = database.GetCollection<Cart>("Cart");
            _products = database.GetCollection<Product>("Product");
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<List<Cart>> Get() =>
            _carts.Find(item => true).ToList();

        public async Task<Cart> Get(string id) =>
            _carts.Find(item => item.Id == id).FirstOrDefault();

        public async Task<Cart> Create(Cart item, Product product)
        {
            item.Is_Active = true;

            try
            {
                //cart exits and active
                var existingCart = await _carts.Find(p => p.UserId == item.UserId && p.Is_Active == true).FirstOrDefaultAsync();

                if (existingCart == null)
                {
                    await _carts.InsertOneAsync(item);
                }
                else
                {
                    item.Id = existingCart.Id;
                }



                product.CartId = item.Id;
                product.UserId = item.UserId;

                //existing products need to update

                //check existing products
                var _productsOfUser = _products.Find(p => p.CartId == existingCart.Id && p.ProductId == product.ProductId).FirstOrDefault();

                if (_productsOfUser != null)
                {
                    var update = Builders<Product>.Update
                        .Set(p => p.Price, product.Price)
                        .Set(p => p.Qty, product.Qty)
                        .Set(p => p.Updated_On, DateTime.Now);

                    FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, _productsOfUser.Id);
                    await _products.UpdateOneAsync(filter, update);
                }
                else
                    //new products need to insert
                    await _products.InsertOneAsync(product);

                FilterDefinition<Cart> filterCart = Builders<Cart>.Filter.Eq(p => p.UserId, existingCart.UserId);

                FilterDefinition<Product> filterProduct = Builders<Product>.Filter.Eq(p => p.UserId, existingCart.UserId);

                var totalProducts = await _products.Find(p => p.UserId == existingCart.UserId).CountDocumentsAsync();

                var updateCart = Builders<Cart>.Update.Set(p => p.TotalItems, totalProducts);

                await _carts.UpdateOneAsync(filterCart, updateCart);

            }
            catch (Exception ex)
            {
                throw;
            }

            return item;
        }

        public async Task Update(string id, Cart itemIn) =>
           await _carts.ReplaceOneAsync(item => item.Id == id, itemIn);

        public async Task Remove(string id) =>
            await _carts.DeleteOneAsync(item => item.Id == id);

        public async Task<CartViewModelRead> GetCartByUserId(string userid)
        {
            CartViewModelRead cartViewModelRead = new CartViewModelRead();
            var cartDetails = (await _carts.FindAsync(item => item.UserId == userid && item.Is_Active == true)).FirstOrDefault();

            cartViewModelRead.UserId = cartDetails.UserId;

            if (cartDetails != null)
            {
               

                var products = await _products.Find(p => p.UserId == userid && p.CartId == cartDetails.Id).ToListAsync();
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                using var channel = GrpcChannel.ForAddress(configuration.GetSection("GrpcProductServerUrl").Value, new GrpcChannelOptions { HttpHandler = httpHandler });

                var productClient = new Products.ProductsClient(channel);
                ProductResponse clientResponse;
                try
                {
                    clientResponse = productClient.GetProductDetails(new ProductRequest() { ProductId = 1 });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    throw;
                }
              

                cartViewModelRead.Products = products.Select(p => new ProductViewModelRead()
                {

                    Added_On = p.Added_On,
                    Discount = clientResponse.Discount,
                    Is_Active = p.Is_Active,
                    Price = clientResponse.Price,
                    ProductId = p.ProductId,
                    ProductName = clientResponse.ProductName,
                    Qty = p.Qty
                });
                return cartViewModelRead;
            }
            else
                return new CartViewModelRead() { UserId = userid, Products = null };
        }
    }
}
