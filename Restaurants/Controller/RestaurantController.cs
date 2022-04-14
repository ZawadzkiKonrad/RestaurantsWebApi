using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.Entities;
using Restaurants.Services;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Controller
{
    [Route("api/restaurant")]
    [ApiController]
    //[Authorize]

    public class RestaurantController : ControllerBase
    {
        
        private readonly IRestaurantService _restaurantService;

        public RestaurantController( IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }


        [HttpGet("{id}")]
        public ActionResult<RestaurantVm> Get([FromRoute] int id)
        {
            var restaurntVm = _restaurantService.GetById(id);
           
            return Ok(restaurntVm);
        }

        [HttpPost]

        public ActionResult CreateRestaurant([FromBody] CreateRestaurantVm vm)
        {
            
            var id = _restaurantService.Create(vm);

            return Created($"/api/restaurant/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromBody] UpdateRestaurantVm vm, [FromRoute] int id)
        {
           
            _restaurantService.Update(vm, id);

            return Ok();

        }
        [HttpDelete("{id}")]

        public ActionResult DeleteRestaurant([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
          
        }

        
        [HttpGet]
        [AllowAnonymous] //pozwala na uzycie akcji bez logowania(autoryzacji)
        public ActionResult<IEnumerable<RestaurantVm>> GetAll()
        {
            var restaurantVms = _restaurantService.GetAll();
            return Ok(restaurantVms);
        }

    }
}
