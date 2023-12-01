using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Commands;
using OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        } 
        [HttpPost]
        [Route("placeorder")]
        public async Task<ActionResult<APIResponse>> Place_Order(OrderViewModelWrite orderViewModelWrite)
        {
            APIResponse aPIResponse = new APIResponse();
            var res = await mediator.Send<OrderViewModelWrite>(new PlaceOrderCommand(orderViewModelWrite));
            return Ok(aPIResponse);
        }
    }
}
