using CartServices.Commands;
using CartServices.Models;
using CartServices.Queries;
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
    public class ReadCartProductsHandler : IRequestHandler<ReadCartProductsQuery, CartViewModelRead>
    {
        private readonly ICartRepository cartRepository;

        public ReadCartProductsHandler(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<CartViewModelRead> Handle(ReadCartProductsQuery request, CancellationToken cancellationToken)
        {
            var res=await cartRepository.GetCartByUserId(request.userId);
            return res;
        }
    }
}
