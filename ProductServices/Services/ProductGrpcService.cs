using Grpc.Core;
using GrpcService1;
using ProductServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServices.Services
{
    public class ProductGrpcService: Products.ProductsBase
    {
        private readonly IProductsRepository productsRepository;

        public ProductGrpcService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public  override async Task<ProductResponse> GetProductDetails(ProductRequest request, ServerCallContext context)
        {
            var res=(await productsRepository.GetByFilter(p => p.Id == request.ProductId)).FirstOrDefault();
            var model = new ProductResponse() { ProductName = res.Name, Discount = Convert.ToDouble(res.Discount), Price = Convert.ToDouble(res.Price), ProductId = request.ProductId };
            return model;
        }
    }
}
