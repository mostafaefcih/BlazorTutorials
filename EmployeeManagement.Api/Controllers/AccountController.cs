using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService account;

        public AccountController(IAccountService account)
        {
            this.account = account;
        }
        [HttpGet("index")]
        public async Task<IActionResult>  index() 
        {

            AddRoleModel model = new AddRoleModel()
            {
                Role = "Admin",
                UserId = "2efab35e-3d87-4743-b50e-ef23a042f561"
            };
         var result = await account.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
         return Ok(result);
        //RegisterationModel model = new RegisterationModel
        //{
        //    Email = "g1@g.com",
        //    FirstName = "Gamal",
        //    LastName = "Salah",
        //    UserName = "Gemmy1",
        //    Password = "Demo@123"
        //};
        //var result = await account.RegisterAsync(model);

        //    AuthenticationRequest model = new AuthenticationRequest
        //    {
        //        Email = "g1@g.com",
        //        Password = "Demo@123"
        //    };
        //    var result = await account.AuthenticateAsync(model);

        //    if (!result.isAuthenticated)
        //        return BadRequest(result.Message);
        //    return Ok($"welcome in account {result}");
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);
            var result = await account.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(model);
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);
            var result = await account.AuthenticateAsync(model);

            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> register(RegisterationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);
            var result = await account.RegisterAsync(model);

            return Ok(result);
        }
    }
}
