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

        //get award history of an employee
        [HttpGet("awards/{id}")]
        public async Task<IActionResult> GetAwardHistory(int id)
        {
            var awardhistory = await employeeService.GetAwardHistory(id);
            if (awardhistory == null)
            {
                return NotFound();
            }
            return Ok(awardhistory);

        }

    }
}
