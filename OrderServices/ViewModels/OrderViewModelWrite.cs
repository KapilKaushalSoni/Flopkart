using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Models
{
    public class OrderViewModelWrite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IEnumerable<int> ProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Order_Placed_On { get; set; }
        public string Order_Status { get; set; }
        public bool Is_Delivered { get; set; }
        public DateTime Delivered_On { get; set; }
    }
}
