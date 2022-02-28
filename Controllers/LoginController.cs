using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.Services;
using xcart.ViewModel;

namespace xcart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyService]
    public class LoginController : ControllerBase
    {
        //Dependency Injection - Login Service
        ILoginService login;

        public LoginController(ILoginService _login)
        {
            login = _login;
        }

        //Authenticate user POST Method
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                IActionResult response = Unauthorized();
                var dbUser = login.ValidateUser(user.UserName, user.Password);

                if (dbUser != null)
                {
                    var userModelList = await login.GetByUserName(user.UserName);
                    var userModel = userModelList[0];
                    var tokenString = login.GenerateJWTToken(userModel);
                    response = Ok(new
                    {
                        UserId=userModel.UserId,
                        Token = tokenString,
                        UserName = user.UserName,
                        RoleName = userModel.RoleName
                    });
                }
                return response;
            }
            return BadRequest();

        }
        //authenticate user by email id
        [HttpGet]
        public async Task<IActionResult> microsoftlogin(string email)
        {
            IActionResult response = Unauthorized();
            var userdetails = await login.GetbyEmailId(email);
            if(userdetails == null)
            {
                return response;
            }
            var tokenString = login.GenerateJWTToken(userdetails);
            response = Ok(new
            {
                UserId = userdetails.UserId,
                Token = tokenString,
                UserName = userdetails.UserName,
                RoleName = userdetails.RoleName
            });
            return response;

        }
       
    }
}
