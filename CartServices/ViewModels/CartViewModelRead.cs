using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.ViewModels
{
    public class CartViewModelRead
    {
        public IEnumerable<ProductViewModelRead> Products { get; set; }
        public string UserId { get; set; }
    }
}
