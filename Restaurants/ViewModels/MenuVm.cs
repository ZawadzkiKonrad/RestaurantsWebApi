using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.ViewModels
{
    public class MenuVm
    {
        public string Category { get; set; }
        public List<DishVm> Dishes { get; set; }

    }
}
