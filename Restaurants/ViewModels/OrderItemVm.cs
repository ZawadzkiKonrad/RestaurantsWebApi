using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.ViewModels
{
    public class OrderItemVm
    {
        public string Identifier { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}
