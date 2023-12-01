using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Data
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Product_Id { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public DateTime Order_Date { get; set; }
    }
}
