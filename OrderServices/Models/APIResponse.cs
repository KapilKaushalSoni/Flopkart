using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public dynamic Data { get; set; }
    }
}
