using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Entities;
using Restaurants.Exceptions;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Services
{
    public interface IRestaurantService
    {
        RestaurantVm GetById(int id);
        IEnumerable<RestaurantVm> GetAll();
        int Create(CreateRestaurantVm vm);
        void Delete(int id);
        void Update(UpdateRestaurantVm vm, int id);
    }


    public class RestaurantService : IRestaurantService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(Context context, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(CreateRestaurantVm vm)
        {
            var restaurant = _mapper.Map<Restaurant>(vm);
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();


        }
        public void Update(UpdateRestaurantVm vm, int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            else
            {
                restaurant.Name = vm.Name;
                restaurant.Description = vm.Description;
                restaurant.ContactEmail = vm.ContactEmail;
                _context.SaveChanges();
            }

        }


        public IEnumerable<RestaurantVm> GetAll()
        {
            var restaurants = _context.Restaurants
                .Include(x => x.Dishes)
                .Include(x => x.Adress)
                .Include(x => x.Drinks)
                .ToList();
            var restaurantsVms = _mapper.Map<List<RestaurantVm>>(restaurants);
            return restaurantsVms;
        }

        public RestaurantVm GetById(int id)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Drinks)
                .Include(x => x.Adress)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);


            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");

            }
            var restaurantVm = _mapper.Map<RestaurantVm>(restaurant);
            return restaurantVm;
        }
    }
}

