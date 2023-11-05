using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.ViewModels
{
    public class ProductViewModelRead
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public DateTime Added_On { get; set; }
        public bool Is_Active { get; set; }
    }
}
