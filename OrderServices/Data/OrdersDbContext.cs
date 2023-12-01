using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Data
{
    public class OrdersDbContext:DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options):base(options)
        {

        }
    }
}
