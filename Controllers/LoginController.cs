using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Services;

namespace xcart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Dependency Injection - Login Service
        ILoginService login;

        public LoginController(ILoginService _login)
        {
            login = _login;
        }

        //Token Generation POST Method
        //[AllowAnonymous]
        [HttpPost("{userName}/{password}")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            IActionResult response = Unauthorized();

            var user = login.ValidateUser(userName, password);

            if(user!=null)
            {
                var userModelList =await login.GetByCredential(userName);
                var userModel = userModelList[0];
                var tokenString = login.GenerateJWTToken(userModel);
                response = Ok(new
                {
                    token = tokenString,
                    userName = userName
                });
            }
            return response;
        }

        //[Authorize(Roles ="User")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{userName}")]

        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await login.GetByCredential(userName);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user[0]);
        }
    }
}
