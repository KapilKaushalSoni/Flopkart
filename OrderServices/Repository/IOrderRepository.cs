using OrderServices.Data;
using OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Repository
{
    public interface IOrderRepository
    {
        Task<Orders> Create(Orders objOrder);
    }
}
