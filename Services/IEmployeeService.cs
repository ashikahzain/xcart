using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface IEmployeeService
    {
        Task<MostAwardedEmployeeViewModel> GetMostAwardedEmployee();

        Task<List<User>> GetAllEmployees();

        Task<List<AllEmployeePointViewModel>> GetEmployeePoints();


        Task<User> GetEmployeeById(int id);

    }
}
