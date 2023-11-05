using CartServices.Models;
using CartServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.Repository
{
    public interface ICartRepository
    {
        Task<List<Cart>> Get();
        Task<Cart> Get(string id);
        Task<CartViewModelRead> GetCartByUserId(string userid);
        Task<Cart> Create(Cart item,Product product);
        Task Update(string id, Cart itemIn);
        Task Remove(string id);
    }
}
