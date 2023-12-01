using MediatR;
using OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Commands
{
    public record PlaceOrderCommand(OrderViewModelWrite obj) : IRequest<OrderViewModelWrite>
    {
    }
}
