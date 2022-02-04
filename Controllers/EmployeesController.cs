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
    }
}
