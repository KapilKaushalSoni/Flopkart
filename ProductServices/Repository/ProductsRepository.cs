using Microsoft.EntityFrameworkCore;
using ProductServices.Data;
using ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductServices.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public ProductsRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var res = await productsDbContext.Products
                .Include(p=>p.CategoryType)
                .Include(p=>p.ProductType)
                .ToListAsync();
            var result = res.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Added_By = p.Added_By,
                Added_By_IP = p.Added_By_IP,
                Added_On = p.Added_On,
                CategoryType = p.CategoryType.Category,
                Is_Active = p.Is_Active,
                Discount = p.Discount,
                Price = p.Price,
                Name = p.Name,
                ProductType = p.ProductType.Type,
                Updated_By = p.Updated_By,
                Updated_By_IP = p.Updated_By_IP,
                Updated_On = p.Updated_On
            });
            return result;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByFilter(Expression<Func<Product, bool>> filter)
        {
            try
            {
                var res = productsDbContext.Products.Where(filter);
                var result = await res.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Added_By = p.Added_By,
                    Added_By_IP = p.Added_By_IP,
                    Added_On = p.Added_On,
                    Discount = p.Discount,
                    Price = p.Price,
                    CategoryType = p.CategoryType.Category,
                    Is_Active = p.Is_Active,
                    Name = p.Name,
                    ProductType = p.ProductType.Type,
                    Updated_By = p.Updated_By,
                    Updated_By_IP = p.Updated_By_IP,
                    Updated_On = p.Updated_On
                }).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
          
        }

        public Task<APIResponse> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> Save(ProductViewModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> Save(IEnumerable<ProductViewModel> obj)
        {
            APIResponse aPIResponse = new APIResponse();

            var products = obj.Select(p => new Product()
            {
                Id = p.Id,
                Added_By = p.Added_By,
                Added_By_IP = p.Added_By_IP,
                Added_On = p.Added_On,
                CategoryType = new CategoryType() { Category = p.CategoryType },
                Is_Active = p.Is_Active,
                Name = p.Name,
                ProductType = new ProductType() { Type = p.ProductType },
                Updated_By = p.Updated_By,
                Updated_By_IP = p.Updated_By_IP,
                Updated_On = p.Updated_On
            });

            try
            {
                await productsDbContext.Products.AddRangeAsync(products);
                await productsDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                aPIResponse.Success = false;
                aPIResponse.Error = ex.Message;
                return aPIResponse;

            }
            aPIResponse.Success = true;
            return aPIResponse;
        }

        public Task<APIResponse> Update(ProductViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
