﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServices.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string ProductType { get; set; }
        public string CategoryType { get; set; }
        public DateTime Added_On { get; set; }
        public string Added_By { get; set; }
        public string Added_By_IP { get; set; }
        public DateTime Updated_On { get; set; }
        public string Updated_By { get; set; }
        public string Updated_By_IP { get; set; }
        public bool Is_Active { get; set; }
    }
}
