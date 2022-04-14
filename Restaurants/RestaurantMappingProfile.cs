using AutoMapper;
using Restaurants.Entities;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants
{
    public class RestaurantMappingProfile:Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantVm>()
            .ForMember(m => m.City, c => c.MapFrom(s => s.Adress.City))
            .ForMember(m => m.Street, c => c.MapFrom(s => s.Adress.Street))
            .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Adress.PostalCode));

            CreateMap<Dish, DishVm>();

            CreateMap<CreateRestaurantVm, Restaurant>()
                .ForMember(r => r.Adress,
                c => c.MapFrom(vm => new Adress()
                { City = vm.City, PostalCode = vm.PostalCode, Street = vm.Street }
                ));
            CreateMap<Drink, DrinkVm>();

            CreateMap<CreateOrderVm, Order>();
            CreateMap<Order, OrderVm>();
            CreateMap<OrderItem, OrderItemVm>();
           
            
        }
    }
}
