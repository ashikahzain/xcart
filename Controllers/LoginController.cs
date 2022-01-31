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

<<<<<<< HEAD
        //Token Generation POST Method
        [HttpPost("{userName}/{password}")]
        public async Task<IActionResult> Login(string userName, string password)
=======
        //Authenticate user POST Method
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
>>>>>>> 593a082a2b93de62a304cadd185bfde390f26aac
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

<<<<<<< HEAD
<<<<<<< HEAD
        [Authorize(Roles ="User")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
=======
        [Authorize(Roles ="Admin")]
>>>>>>> 499a1734d6a5668984ce5db969b9e4583db394d9
        [HttpGet("{userName}")]

        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await login.GetByCredential(userName);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
=======
>>>>>>> 593a082a2b93de62a304cadd185bfde390f26aac
        }
    }
}
