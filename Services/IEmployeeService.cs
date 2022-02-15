﻿using System;
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

        Task<List<AllEmployeePointViewModel>> GetEmployeePoints(int pageNumber,int pagesize);

        Task<User> GetEmployeeById(int id);

        Task<List<EmployeeProfileViewModel>> GetEmployeeProfile(int id);
        public Task<List<OrderViewModel>> GetAllOrdersByEmployeeId(int id, int pageNumber, int pagesize);

        public Task<int> GetEmployeeCount();
        public Task<int> GetEmployeeOrderCount(int id);

    }
}
