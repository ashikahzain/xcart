using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public class EmployeeService : IEmployeeService
    {
        XCartDbContext db;

        public EmployeeService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Get employee by UserId
        public async Task<User> GetEmployeeById(int id)
        {
            var emp = await db.User.FirstOrDefaultAsync(emp => emp.Id == id);
            if (emp == null)
            {
                return null;
            }
            return emp;
        }
        #endregion

        #region Get All Employees

        public async Task<List<User>> GetAllEmployees()
        {
            if (db != null)
            {
                return await db.User.ToListAsync();
            }
            return null;
        }
        #endregion

        #region Get all Employee points
        public async Task<List<AllEmployeePointViewModel>> GetEmployeePoints()
        {
            if (db != null)
            {
                return await (from emp in db.User
                              from point in db.Point
                              where emp.Id == point.Id

                              select new AllEmployeePointViewModel
                              {
                                  UserId = emp.Id,
                                  Name = emp.Name,
                                  CurrentPoints = point.CurrentPoints
                              }).ToListAsync();
            }
            return null;
        }
        #endregion

        #region Most Awarded Employee
        public async Task<MostAwardedEmployeeViewModel> GetMostAwardedEmployee()
        {
            if (db != null)
            {
                //join User and Point
                return await(from user in db.User
                             from point in db.Point
                             orderby point.TotalPoints descending
                             select new MostAwardedEmployeeViewModel
                             {
                                 Id = point.User.Id,
                                 Name = user.Name,
                                 TotalPoints = point.TotalPoints
                             }).FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion


    }
}
