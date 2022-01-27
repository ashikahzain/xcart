using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public class LoginService : ILoginService
    {
        private IConfiguration config;

        XCartDbContext db;
        public LoginService(IConfiguration _config, XCartDbContext _db)
        {
            config = _config;
            db = _db;
        }


        public string GenerateJWTToken(LoginViewModel userModel)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

           /* var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userModel.UserName),
                new Claim(ClaimTypes.Role,userModel.RoleName)
            };*/

            //token
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<LoginViewModel>> GetByCredential(string UserName)
        {
            if(db!=null)
            {
                return await (from user in db.User
                              from userrole in db.UserRole
                              from role in db.Role

                              where user.UserName == UserName
                              where userrole.Role.Id == role.Id
                              where userrole.User.Id == user.Id


                              select new LoginViewModel
                              {
                                  UserId = user.Id,
                                  UserName = user.Name,
                                  RoleName = role.Name
                              }
                    ).ToListAsync();
            }
            return null;
        }

        public User ValidateUser(string UserName, string password)
        {
           if(db!=null)
            {
                User u = db.User.FirstOrDefault(em => em.UserName == UserName && em.Password == password);
                if(u!=null)
                {
                    return u;
                }
                return null;
            }
            return null;
        }
    }
}
