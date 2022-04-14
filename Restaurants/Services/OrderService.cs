using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Services
{
    public class OrderService
    {
        private readonly Context _context;

        public OrderService(Context context)
        {
            _context = context;
        }
    }
}
