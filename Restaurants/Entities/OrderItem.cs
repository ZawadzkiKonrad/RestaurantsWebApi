using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order{ get; set; }
    }
}
