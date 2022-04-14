using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public int TableNumber { get; set; }
        public string PaymentId { get; set; }
        public string UserComment { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

        public decimal TimeForWaiting { get; set; }
    }
}
