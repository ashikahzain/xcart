using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface ILoginService
    {
        public User ValidateUser(string UserName, string password);

        public string GenerateJWTToken(LoginViewModel userModel);

        public Task<List<LoginViewModel>> GetByUserName(string UserName);

        public Task<LoginViewModel> GetbyEmailId(string email);

        public bool VerifyUser(string UserName, string password);

    }
}
