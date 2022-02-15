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

        //Constructor
        public AwardHistoryController(XCartDbContext db, IAwardHistoryService awardHistoryService,IPointService pointservice)
        {
            this.db = db;
            this.awardHistoryService = awardHistoryService;
            this.pointService = pointservice;
        }


        #region Get All Award List
        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            try
            {
                var awards = await db.AwardHistory.ToListAsync();
                if (awards == null)
                {
                    return NotFound();
                }
                return Ok(awards);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Add to Award History
        [HttpPost]
        public async Task<IActionResult> AddAwardHistory(AwardHistory award)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    if (award.Status)   //Checking if the Points are to be added  (Status=1:Add,Status=0:Subtract)
                    {
                        var response = pointService.AddPoint(award.Point, award.EmployeeId); //Adding the points to point table corresponding Employee
                        
                        if (response == null)
                        {
                            return BadRequest("Points not added");

                        }
                    }
                    else                //Subtracting Point
                    {
                        var response = pointService.RemovePoints(award.Point, award.EmployeeId);

                        if (response == null)
                        {
                            return BadRequest("Points not removed");

                        }
                    }

                    var awardId = await awardHistoryService.AddAwardHistory(award);  //Adding Award to Award History table

                    if (awardId != 0)
                    {
                        return Ok(awardId);
                    }
                    else
                    {
                        return NotFound("Award history not added");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
        #endregion

        #region Get award history of an employee
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAwardHistory(long id)
        {
            try
            {
                var awardhistory = await awardHistoryService.GetAwardHistory(id);

                if (awardhistory == null)
                {
                    return NotFound("Award history of employee not found");
                }
                return Ok(awardhistory);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

    }
}
