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
