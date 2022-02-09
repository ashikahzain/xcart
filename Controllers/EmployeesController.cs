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
    public class EmployeesController : ControllerBase
    {
        IEmployeeService employeeService;

        XCartDbContext db;

        //constructor 
        public EmployeesController(IEmployeeService _employeeService, XCartDbContext _db)
        {
            employeeService = _employeeService;
            db = _db;
        }

        //Get All Employees
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeService.GetAllEmployees();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        //get Employee By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employees = await employeeService.GetEmployeeById(id);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        //get points of all employees
        [HttpGet]
        public async Task<IActionResult> GetEmployeePoints()
        {
            var empPoints = await employeeService.GetEmployeePoints();
            if (empPoints == null)
            {
                return NotFound();
            }
            return Ok(empPoints);
        }

        //get employee with most points
        [HttpGet]
        [Route("most-awards")]
        public async Task<IActionResult> GetMostAwardedEmployee()
        {
            var orders = await employeeService.GetMostAwardedEmployee();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);

        }

        #region Get Employee By Id

        //Get employee by id : GET Method : https://localhost:44396/api/employees/employee/2
        [HttpGet]
        [Route("employee/{id}")]

        public async Task<IActionResult> GetEmployeeProfile(int id)
        {
            var employee = await employeeService.GetEmployeeProfile(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        #endregion

        // get order details by employee id
        [HttpGet]
        [Route("OrderByEmpId/{id}")]
        public async Task<IActionResult> GetAllOrdersByEmployeeId(int id)
        {
            var order = await employeeService.GetAllOrdersByEmployeeId(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }




    }
}
