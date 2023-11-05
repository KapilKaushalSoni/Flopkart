using CartServices.Commands;
using CartServices.Models;
using CartServices.Repository;
using CartServices.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CartServices.CommandHandler
{
    public class AddToCartProductsHandler : IRequestHandler<AddToCartProductsCommand, CartViewModelWrite>
    {
        private readonly ICartRepository cartRepository;

        public AddToCartProductsHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public async Task<CartViewModelWrite> Handle(AddToCartProductsCommand request, CancellationToken cancellationToken)
        {
            Cart cart = new Cart();
            cart.UserId = request.obj.UserId;

            Product Product =new Product()
            {
                ProductId = request.obj.Product.ProductId,
                Price = request.obj.Product.Price,
                Qty = request.obj.Product.Qty
            };
            var res=await cartRepository.Create(cart, Product);
            CartViewModelWrite cartViewModelWrite = new CartViewModelWrite();

            cartViewModelWrite.UserId = res.UserId;
            cartViewModelWrite.Product = request.obj.Product;

            return cartViewModelWrite;
        }
    }
}
