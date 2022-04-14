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
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public MenuController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
