using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.Entities;
using Restaurants.Exceptions;
using Restaurants.Services;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Restaurants.Controller
{
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _restaurantService;

        public MenuController(Context context, IMapper mapper, IRestaurantService restaurantService)
        {
            _context = context;
            _mapper = mapper;
            _restaurantService = restaurantService;
        }

        [HttpPost("addDish/{restaurantId}")]

        public ActionResult AddDish([FromBody] CreateDishVm vm, [FromRoute] int restaurantId)
        {

            var dish = _mapper.Map<Dish>(vm);
            dish.RestaurantId = restaurantId;
            _context.Dishes.Add(dish);
            _context.SaveChanges();


            //var id = _restaurantService.Create(vm);
            return Created($"/api/menu/addDish/{dish.Id}", null);
        }

        [HttpPut("updateDish/{dishId}")]
        public ActionResult UpdateDish([FromBody] CreateDishVm vm, [FromRoute] int dishId)
        {

            var dish = _context.Dishes.FirstOrDefault(x => x.Id == dishId);
            if (dish is null)
            {
                throw new NotFoundException("Dish not found");
            }
            else
            {
                //dish = _mapper.Map<Dish>(vm);
                dish.Name = vm.Name;
                dish.Description = vm.Description;
                dish.CategoryId = vm.CategoryId;
                dish.Image = vm.Image;
                dish.Price = vm.Price;
                _context.SaveChanges();
            }

            return Ok();

        }

        [HttpGet("getDish/{dishId}")]

        public ActionResult<DishVm> GetDish([FromRoute] int dishId)
        {
            var dish = _context.Dishes.FirstOrDefault(x => x.Id == dishId);
            var dishVm = _mapper.Map<DishVm>(dish);
            return dishVm;

        }

        [HttpGet("getCategories")]

        public ActionResult<IEnumerable<CategoryVm>> GetCategories()
        {
            var categories = _context.Categories;
            var categoriesVm = _mapper.Map<List<CategoryVm>>(categories);
            return categoriesVm;

        }

        [HttpGet("{restaurantId}")]
        public ActionResult<IEnumerable<MenuVm>> Getmenu([FromRoute] int restaurantId)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Dishes)
                .Include(x => x.Drinks)
                .FirstOrDefault(x => x.Id == restaurantId);
            var categories = _context.Categories;
            var listMenuVm = new List<MenuVm>();
            foreach (var item in categories)
            {
                var menuVm = new MenuVm()
                {
                    Category = item.Name,
                    Dishes = _mapper.Map<List<DishVm>>(restaurant.Dishes.Where(x => x.CategoryId == item.Id))
                };
                listMenuVm.Add(menuVm);
            };
            //var menuVm = new MenuVm()
            //{
            //    Dishes = _mapper.Map<List<DishVm>>(restaurant.Dishes),
            //   // Drinks = _mapper.Map<List<DrinkVm>>(restaurant.Drinks)
            //};
            return Ok(listMenuVm);

        }

    }
}
