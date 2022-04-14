using Microsoft.AspNetCore.Mvc;
using Restaurants.Services;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
     [HttpPost("register")]
     public ActionResult RegisterUser([FromBody]RegisterUserVm vm)
        {
            _accountService.RegisterUser(vm);
            return Ok();
        } 
        
        [HttpPost("login")]
     public ActionResult LoginUser([FromBody]LoginUserVm vm)
        {

            string token = _accountService.GenerateJwt(vm);
            return Ok(token);
        }
    }
}
 