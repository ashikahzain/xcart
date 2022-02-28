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

        #region Token Generation
        //Token Generation method implementation
        public string GenerateJWTToken(LoginViewModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Adding UserName and RoleName as claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userModel.UserName),
                new Claim(ClaimTypes.Role,userModel.RoleName)
            };

            //token is generated 
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region Get User-Role 
        //View Model for user role
        public async Task<List<LoginViewModel>> GetByUserName(string UserName)
        {
            if(db!=null)
            {                 //Joining userTable and RoleTable using userName to retrieve roleName
                return await (from user in db.User
                              from userrole in db.UserRole
                              from role in db.Role

                              where user.UserName == UserName
                              where userrole.RoleId == role.Id
                              where userrole.UserId == user.Id


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

     
        #endregion

        #region Validate User
        //User Validation with database
        public User ValidateUser(string UserName, string password)
        {
           if(db!=null)
            {
                User user = db.User.FirstOrDefault(em => em.UserName == UserName && em.Password == password);
                if(user!=null)
                {
                    return user;
                }
                return null;
            }
            return null;
        }
        #endregion

        #region Get user by email id

        public async Task<LoginViewModel> GetbyEmailId(string email)
        {
            if (db != null)
            {
               return await(from user in db.User
                                 from userrole in db.UserRole
                                 from role in db.Role

                                 where user.Email == email
                                 where userrole.RoleId == role.Id
                                 where userrole.UserId == user.Id


                                 select new LoginViewModel
                                 {
                                     UserId = user.Id,
                                     UserName = user.Name,
                                     RoleName = role.Name
                                 }
                    ).FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion
        #region Validate User
        //User Validation with database
        public async  Task<User> VerifyUser(string UserName, string password)
        {
            if (db != null)
            {
                User user =await db.User.FirstOrDefaultAsync(em => em.UserName == UserName && em.Password == password);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            return null;
        }
        #endregion


    }
}
