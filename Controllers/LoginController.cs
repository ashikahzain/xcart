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
        ILoginService login;

        public LoginController(ILoginService _login)
        {
            login = _login;
        }

        [HttpPost("{userName}/{password}")]
        public IActionResult Login(string userName, string password)
        {
            IActionResult response = Unauthorized();

            var user = login.ValidateUser(userName, password);

            if(user!=null)
            {
                var tokenString = login.GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userName = userName
                }
                    );
                  
                   
  
            }
            return response;

        }
    }
}
