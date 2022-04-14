using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public int RestaurantId{ get; set; }

        
        public string Name { get; set; }
        public int VolumeMl { get; set; }
        public decimal Price { get; set; }
     
    }
}
