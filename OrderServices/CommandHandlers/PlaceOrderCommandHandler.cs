using MediatR;
using OrderServices.Commands;
using OrderServices.Data;
using OrderServices.Models;
using OrderServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderServices.CommandHandler
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderViewModelWrite>
    {
        private readonly IOrderRepository orderRepository;

        public PlaceOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<OrderViewModelWrite> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            Orders objOrder = new();
            objOrder.Price = request.obj.Amount;
            var res = await orderRepository.Create(objOrder);
            request.obj.Id = res.Id;
            return request.obj;
        }
    }
}
