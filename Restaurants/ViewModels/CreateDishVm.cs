using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.ViewModels
{
    public class CreateDishVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}
