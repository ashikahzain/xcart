using Microsoft.AspNetCore.Authorization;
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

        ILoggerService logger;

        //constructor     
        public EmployeesController(IEmployeeService _employeeService, XCartDbContext _db, ILoggerService _logger)
        {
            employeeService = _employeeService;
            db = _db;
            logger = _logger;
        }

        #region Get All employees
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                logger.LogInfo("Get all Employees");
                var employees = await employeeService.GetAllEmployees();
                if (employees == null)
                {
                    logger.LogWarn("Employee records not found");
                    return NotFound();
                }
                logger.LogInfo($"Returned Awards {employees}");
                return Ok(employees);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get Employee By ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(long id)
        {
            try
            {
                logger.LogInfo("Get an employee by ID API called");
                var employees = await employeeService.GetEmployeeById(id);
                if (employees == null)
                {
                    logger.LogWarn($"Employee with ID {id} was not found");
                    return NotFound();
                }
                logger.LogInfo("Employee record found");
                return Ok(employees);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Get Points of all Employees
        [Authorize]
        [HttpGet]
   
        public async Task<IActionResult> GetEmployeePoints(int pagenumber,int pagesize)
        {
            try
            {
                logger.LogInfo("Getting all Employee Points");
                var empPoints = await employeeService.GetEmployeePoints(pagenumber, pagesize);
                if (empPoints == null)
                {
                    logger.LogWarn("Employee points record not found");
                    return NotFound();
                }
                logger.LogInfo("Returning employee points records");
                return Ok(empPoints);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Get Most Rewarded Employee
        [Authorize]
        [HttpGet]
        [Route("most-awards")]
        public async Task<IActionResult> GetMostAwardedEmployee()
        {
            try
            {
                logger.LogInfo("Getting record of most rewarded employee record");
                var orders = await employeeService.GetMostAwardedEmployee();
                if (orders == null)
                {
                    logger.LogWarn("Most rewarded employee record not found");
                    return NotFound();
                }
                logger.LogInfo("Returning Most Awarded Employee");
                return Ok(orders);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
        #endregion

        #region Get Employee Profile By Id

        //Get employee by id : GET Method : https://localhost:44396/api/employees/employee/2
        [Authorize]
        [HttpGet]
        [Route("employee-profile/{id}")]

        public async Task<IActionResult> GetEmployeeProfile(long id)
        {
            try
            {
                logger.LogInfo($"Get profile of Employee ID {id}");
                var employee = await employeeService.GetEmployeeProfile(id);
                if (employee == null)
                {
                    logger.LogWarn($"Profile of employee with ID {id} not found");
                    return NotFound();
                }
                logger.LogInfo($"Returning Employee profile of Employee ID {id}");
                return Ok(employee);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Get Order Details by employee id
        [Authorize]
        [HttpGet]
        [Route("{id}/orders")]
        public async Task<IActionResult> GetAllOrdersByEmployeeId(long id, int pagenumber, int pagesize)
        {
            try
            {
                logger.LogInfo($"Get all orders placed by employee with ID {id}");
                var order = await employeeService.GetAllOrdersByEmployeeId(id, pagenumber, pagesize);
                if (order == null)
                {
                    logger.LogWarn($"No orders found for employee with ID {id}");
                    return NotFound();
                }
                logger.LogInfo($"Returning all orders placed by employee with ID {id}");
                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Get employee count
        [Authorize]
        [HttpGet]
        [Route("employee-count")]
        public async Task<IActionResult> GetEmployeeCount()
        {
            try
            {
                logger.LogInfo("Getting total no: of employees");
                var count = await employeeService.GetEmployeeCount();
                return Ok(count);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        #endregion

        #region Get Employee Order Count
        [Authorize]
        [HttpGet]
        [Route("{id}/order-count")]
        public async Task<IActionResult> GetEmployeeOrderCount(int id)
        {
            try
            {
                logger.LogInfo($"Getting order count of employee with ID {id}");
                var count = await employeeService.GetEmployeeOrderCount(id);
                return Ok(count);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion


    }
}
