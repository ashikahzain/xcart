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


        #region Get all Employees
        [HttpGet]                                                                           //[HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeService.GetAllEmployees();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }
        #endregion


        #region Get Employee By Id
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
        #endregion


        #region Get points of all employees
        [HttpGet("employee-points")]
   
        public async Task<IActionResult> GetEmployeePoints(int pagenumber,int pagesize)
        {
            var empPoints = await employeeService.GetEmployeePoints(pagenumber,pagesize);
            if (empPoints == null)
            {
                return NotFound();
            }
            return Ok(empPoints);
        }
        #endregion


        #region Get Employee with most points
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
        #endregion


        #region Get Employee By Id
        [HttpGet]
        [Route("employees/{id}")]

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

  
        #region Get order details by employee id
        [HttpGet]
        [Route("OrderByEmpId/{id}")]
        public async Task<IActionResult> GetAllOrdersByEmployeeId(int id, int pagenumber, int pagesize)
        {
            var order = await employeeService.GetAllOrdersByEmployeeId(id, pagenumber,pagesize);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
#endregion


        #region Get employee count

        [HttpGet]
        [Route("employee-count")]
        public async Task<IActionResult> GetEmployeeCount()
        {
            var count = await employeeService.GetEmployeeCount();
            return Ok(count);
        }


        #endregion


        #region Get Employee Order Count
        [HttpGet]
        [Route("employee-order-count/{id}")]
        public async Task<IActionResult> GetEmployeeOrderCount(int id)
        {
            var count = await employeeService.GetEmployeeOrderCount(id);
            return Ok(count);
        }
        #endregion


    }
}
