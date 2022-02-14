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
        public async Task<List<AllEmployeePointViewModel>> GetEmployeePoints(int pageNumber,int pagesize)
        {
            
            if (db != null)
            {
                var queryResult= await (from emp in db.User
                              from point in db.Point
                              where point.UserId==emp.Id

                              select new AllEmployeePointViewModel
                              {
                                  UserId = emp.Id,
                                  Name = emp.Name,
                                  CurrentPoints = point.CurrentPoints
                              }).Skip(pagesize * (pageNumber-1)).Take(pagesize).ToListAsync();
               
                return queryResult;
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

        #region Get Employee Profile
        public async Task<List<EmployeeProfileViewModel>> GetEmployeeProfile(int id)
        {
            if (db != null)
            {
                return await(from user in db.User
                             from location in db.Location
                             from department in db.Department
                             from jobtitle in db.JobTitle
                             from point in db.Point
                             from grade in db.Grade

                             where user.Id == id &&
                             user.LocationId == location.Id &&
                             user.DepartmentId == department.Id &&
                             user.GradeId == grade.Id &&
                             user.JobTitleId == jobtitle.Id &&
                             point.UserId == user.Id

                             select new EmployeeProfileViewModel
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Gender = user.Gender,
                                 Email = user.Email,
                                 Image = user.Image,
                                 Contact = user.Contact,
                                 UserName = user.UserName,
                                 LocationName = location.Name,
                                 DepartmentName = department.Name,
                                 GradeName = grade.Type,
                                 JobTitleName = jobtitle.Name,
                                 CurrentPoints = point.CurrentPoints
                             }).ToListAsync();
            }
            return null;
        }

        #endregion

        #region Get Order Details By Employee Id
        public async Task<List<OrderViewModel>> GetAllOrdersByEmployeeId(int id, int pageNumber, int pagesize)
        {
            if (db != null)
            {
                return await(from order in db.Order
                             from user in db.User
                             from status in db.StatusDescription
                             where user.Id==id
                             where order.UserId == user.Id
                             where order.StatusDescriptionId == status.Id
                             orderby order.Id descending

                             select new OrderViewModel
                             {
                                 Id = order.Id,
                                 DateOfOrder = order.DateOfOrder,
                                 DateOfDelivery = order.DateOfDelivery,
                                 UserName = user.Name,
                                 Points = order.Points,
                                 Status = status.Status
                             }
                    ).Skip(pagesize * (pageNumber - 1)).Take(pagesize).ToListAsync();
            }
            return null;
        }


        #endregion

        #region Get number of Employees
        public async Task<int> GetEmployeeCount()
        {
            var count = await db.User.CountAsync();
            return count;
        }
        #endregion

        #region Get Employee Order Count
        public async Task<int> GetEmployeeOrderCount(int id)
        {
            var count = await db.Order.Where(o => o.UserId == id).CountAsync();
            return count;
        }
        #endregion
    }
}
