using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xcart.Models;

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

        public string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //token
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
