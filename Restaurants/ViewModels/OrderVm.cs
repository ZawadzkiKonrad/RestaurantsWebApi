using Restaurants.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.ViewModels
{
    public class OrderVm
    {
        public int Id { get; set; }
        public string Restaurant { get; set; }
        public int TableNumber { get; set; }
        public string UserComment { get; set; }
        public string Status { get; set; }
        public decimal TimeForWaiting { get; set; }
        //public virtual List<DishVm> Dishes { get; set; }
        public virtual List<OrderItemVm> OrderItems { get; set; }
        public decimal Price { get; set; }
    }
}
