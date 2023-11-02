using Microsoft.EntityFrameworkCore;
using ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServices.Data
{
    public class ProductsDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options):base(options)
        {

        }
    }
}
