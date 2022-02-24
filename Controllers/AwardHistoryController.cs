using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AwardHistoryController : ControllerBase
    {
        XCartDbContext db;
        IAwardHistoryService awardHistoryService;
        IPointService pointService;
        ILoggerService logger;

        //Constructor
        public AwardHistoryController(XCartDbContext db, IAwardHistoryService awardHistoryService,IPointService pointservice,ILoggerService logger)
        {
            this.db = db;
            this.awardHistoryService = awardHistoryService;
            this.pointService = pointservice;
            this.logger = logger;
        }


        #region Get All Award List
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            try
            {
                logger.LogInfo("Get all Awards");
                var awards = await db.AwardHistory.ToListAsync();
                if (awards == null)
                {
                    logger.LogWarn("Awards Not Found");
                    return NotFound();
                    
                }
                logger.LogInfo($"Returned Awards {awards}");
                return Ok(awards);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Add to Award History
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAwardHistory(AwardHistory award)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    logger.LogInfo($"Add to awardHistory {award.Award} by Admin with user id{award.PresenteeId} to employee with userid {award.EmployeeId}");
                    if (award.Status)   //Checking if the Points are to be added  (Status=1:Add,Status=0:Subtract)
                    {
                        var response = pointService.AddPoint(award.Point, award.EmployeeId); //Adding the points to point table corresponding Employee
                        
                        if (response == null)
                        {
                            logger.LogInfo($"Points not added to employee with response ");

                            return BadRequest("Points not added");
                        }
                        logger.LogInfo($"Points added to employee with user id {response.UserId}");

                    }
                    else                //Subtracting Point
                    {
                        var response = pointService.RemovePoints(award.Point, award.EmployeeId);

                        if (response == null)
                        {
                            logger.LogInfo($"Points not removed to employee with response");

                            return BadRequest("Points not removed");

                        }
                        logger.LogInfo($"Points removed to employee with user id {response.UserId}");

                    }

                    var awardId = await awardHistoryService.AddAwardHistory(award);  //Adding Award to Award History table

                    if (awardId != 0)
                    {
                        logger.LogInfo($"Award added with id {awardId}");
                        return Ok(awardId);
                    }
                    else
                    {
                        logger.LogWarn("Award not added to award history table");

                        return NotFound("Award history not added");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
            logger.LogError("bad request");
            return BadRequest();
        }
        #endregion

        #region Get award history of an employee
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAwardHistory(long id)
        {
            try
            {
                logger.LogInfo($"get award history of employee with id {id}");
                var awardhistory = await awardHistoryService.GetAwardHistory(id);

                if (awardhistory == null)
                {
                    return NotFound("Award history of employee not found");
                }
                logger.LogInfo($"Award history returned:{awardhistory.ToArray()}");
                return Ok(awardhistory);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        #endregion

    }
}
