using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.ViewModels
{
    public class CartViewModelWrite
    {
        public ProductViewModelWrite Product { get; set; }
        public string UserId { get; set; }
        public DateTime Added_On { get; set; }
    }
}
