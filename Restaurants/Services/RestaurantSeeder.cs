using Restaurants.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Services
{
    public class RestaurantSeeder
    {
        private readonly Context _context;

        public RestaurantSeeder(Context context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
                if (!_context.Statuses.Any())
                {
                    var status1 = new Status()
                    {
                        Name = "Oczekujący/Waiting"
                    };
                    var status2 = new Status()
                    {
                        Name = "Opłacony/Paid"
                    };
                    _context.Statuses.Add(status1);
                    _context.Statuses.Add(status2);
                    _context.SaveChanges();
                }

                if (!_context.Categories.Any())
                {
                    var categories = new List<Category>()
            {
                new Category()
                {
                    Name="Dania Główne"
                },
                new Category()
                {
                    Name="Przystawki"
                },
                new Category()
                {
                    Name="Inne"
                }
            };
                    _context.Categories.AddRange(categories);
                    _context.SaveChanges();
                }

                if (!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    _context.SaveChanges();
                }
                //if (!_context.Dishes.Any())
                //{
                //    var restaurants = GetRestaurants();
                //    _context.Restaurants.AddRange(restaurants);
                //    _context.SaveChanges();
                //}

                //var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == 1);
                //var drink = new Drink()
                //{
                //    Name = "cola",
                //    Price = 2.2M,
                //    RestaurantId = 1
                //};
                //restaurant.Drinks.Add(drink);
                //_context.SaveChanges();
            }




        }
        private IEnumerable<Role> GetMenu()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="Admin"
                },
                new Role()
                {
                    Name="Manager"
                },
                new Role()
                {
                    Name="User"
                }
            };
            return roles;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="Admin"
                },
                new Role()
                {
                    Name="Manager"
                },
                new Role()
                {
                    Name="User"
                }
            };
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                //new Restaurant ()
                //{
                //    Name="Sushi Room",
                //    Category="Japanesse",
                //    Description="sushi",
                //    ContactEmail="email@sushiroom.pl"
                    
                //},
                new Restaurant ()
                {
                    Name="Kraft Bistro",
                    Category="Polska",
                    Description="rozne",
                    ContactEmail="email@kraftbistro.pl",
                    Dishes= new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Margherita",
                            Price=19.00M,
                            CategoryId=4
                        },
                        new Dish()
                        {
                            Name="Parma",
                            Price=26.00M,
                            CategoryId=4
                        },
                    },
                    Adress=new Adress()
                    {
                        City="Radom",
                        Street="Curie-Sklodowskiej 5",
                        PostalCode="26-600"
                    }
                }
            };
            return restaurants;
        }
    }
}
