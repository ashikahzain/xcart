using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
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

        //Authenticate user POST Method
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                IActionResult response = Unauthorized();



                var dbUser = login.ValidateUser(user.Name, user.Password);

                if (dbUser != null)
                {
                    var userModelList = await login.GetByCredential(user.Name);
                    var userModel = userModelList[0];
                    var tokenString = login.GenerateJWTToken(userModel);
                    response = Ok(new
                    {
                        token = tokenString,
                        userName = user.Name
                    });
                }
                return response;
            }
            return BadRequest();

        }
    }
}
