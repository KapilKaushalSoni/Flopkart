using CartServices.Commands;
using CartServices.Models;
using CartServices.Queries;
using CartServices.Repository;
using CartServices.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.Controllers
{
    [Route("api/v1/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CartsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CartViewModelWrite>> AddToCart(CartViewModelWrite cartViewModel)
        {
            var res = await mediator.Send<CartViewModelWrite>(new AddToCartProductsCommand(cartViewModel));
            return Ok(cartViewModel);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartViewModelRead>> GetCart(string userId)
        {
            var res = await mediator.Send<CartViewModelRead>(new ReadCartProductsQuery(userId));
            return Ok(res);
        }
    }
}
