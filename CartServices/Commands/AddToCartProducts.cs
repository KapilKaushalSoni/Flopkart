using CartServices.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.Commands
{
    public record AddToCartProductsCommand(CartViewModelWrite obj): IRequest<CartViewModelWrite>
    {
    }
}
