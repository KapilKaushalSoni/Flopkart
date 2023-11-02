using ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductServices.Repository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<IEnumerable<ProductViewModel>> GetByFilter(Expression<Func<Product, bool>> filter);
        Task<APIResponse> GetById(int Id);
        Task<APIResponse> Save(ProductViewModel obj);
        Task<APIResponse> Save(IEnumerable<ProductViewModel> obj);
        Task<APIResponse> Update(ProductViewModel obj);
    }
}
