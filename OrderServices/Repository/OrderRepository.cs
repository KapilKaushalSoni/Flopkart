using OrderServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext ordersDbContext;

        public OrderRepository(OrdersDbContext ordersDbContext)
        {
            this.ordersDbContext = ordersDbContext;
        }
        public async Task<Orders> Create(Orders objOrder)
        {
            ordersDbContext.Orders.Add(objOrder);
            await ordersDbContext.SaveChangesAsync();
            return objOrder;
        }
    }
}
