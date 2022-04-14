using System.Collections.Generic;

namespace Restaurants.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Dish> Dishes { get; set; }


    }
}