using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.Entities;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Controller
{
    [Route("api/order")]

    public class OrderController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public OrderController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateOrder([FromBody] CreateOrderVm vm)
        {

            var order = _mapper.Map<Order>(vm);
            var orderItems = new List<OrderItem>();
            foreach (var id in vm.DzishIdentifiers)
            {
                var dish = _context.Dishes.FirstOrDefault(x => x.Identifier == id);
                var orderItem = new OrderItem()
                {
                    Identifier = dish.Identifier,
                    Price = dish.Price,
                   OrderId=dish.Id

                };
                orderItems.Add(orderItem);
                
                
            };
            order.OrderItems = orderItems;
            _context.Orders.Add(order);
            _context.SaveChanges();


            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderVm>> GetAll()
        {
            var orders = _context.Orders.Include(x => x.OrderItems).ToList();
            var ordersVm = new List<OrderVm>();
            foreach (var order in orders)
            {
                var orderVm = _mapper.Map<OrderVm>(order);

                if (order.StatusId == 1)
                {
                    orderVm.Status = "Opłacony";
                }
                else
                {
                    orderVm.Status = "Oczekujący";
                };
                var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == order.RestaurantId);
                orderVm.Restaurant = restaurant.Name;
                ordersVm.Add(orderVm);
            }
            return Ok(ordersVm);
        }
        [HttpGet("Waiting")]

        public ActionResult<IEnumerable<OrderVm>> GetWaitingOrders()
        {
            var orders = _context.Orders.Include(x => x.OrderItems)
                .Where(x => x.StatusId == 2);
            var ordersVm = new List<OrderVm>();
            foreach (var order in orders)
            {
                var orderVm = _mapper.Map<OrderVm>(order);
                ordersVm.Add(orderVm);
            }
            return Ok(ordersVm);


        }
        [HttpPost("MarkAsPaid")]

        public ActionResult MarkAsPaid(int orderId, decimal timeForWaiting)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            order.StatusId = 1;
            order.TimeForWaiting = timeForWaiting;
            _context.SaveChanges();

            return Ok();


        }

        [HttpGet("GetStatus")]

        public ActionResult<string> GetOrderStatus(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            string status = "";
            if (order.StatusId == 1)
            {
                status = "Opłacony";
            }
            else
            {
                status = "Oczekujący";
            }
            return Ok(status);

        }
    }
}
