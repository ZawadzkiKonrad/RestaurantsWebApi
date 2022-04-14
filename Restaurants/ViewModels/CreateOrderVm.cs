using Restaurants.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.ViewModels
{
    public class CreateOrderVm
    {
        public int RestaurantId { get; set; }
        public int TableNumber { get; set; }
        public string UserComment { get; set; }

        public int StatusId { get; set; } = 2;

        public virtual List<string> DzishIdentifiers { get; set; }
    }
}
