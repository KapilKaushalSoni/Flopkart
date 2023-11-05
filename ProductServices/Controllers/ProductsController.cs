using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Models;
using ProductServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServices.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            var res = await productsRepository.GetAll();
            return Ok(res);
        }
        [HttpGet]
        [Route("nameLike/{nameLike}")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetByNameContains(string nameLike)
        {
            var res = await productsRepository.GetByFilter(p=>p.Name.Contains(nameLike));
            return Ok(res);
        }
        [HttpPost]
        [Route("bulksave")]
        public async Task<ActionResult<APIResponse>> BulkSave(IEnumerable<ProductViewModel> objs)
        {
            var res = await productsRepository.Save(objs);
            return Ok(res);
        }
    }
}
