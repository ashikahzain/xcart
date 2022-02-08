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

        public AwardHistoryController(XCartDbContext db, IAwardHistoryService awardHistoryService,IPointService pointservice)
        {
            this.db = db;
            this.awardHistoryService = awardHistoryService;
            this.pointService = pointservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            var awards = await db.AwardHistory.ToListAsync();
            if (awards == null)
            {
                return NotFound();
            }
            return Ok(awards);

        }

        [HttpPost]
        public async Task<IActionResult> AddAwardHistory(AwardHistory award)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    if (award.Status)
                    {
                        var response = pointService.AddPoint(award.Point, award.EmployeeId);
                        if (response == null)
                        {
                            return BadRequest();

                        }
                    }
                    else
                    {
                        var response = pointService.RemovePoints(award.Point, award.EmployeeId);
                        if (response == null)
                        {
                            return BadRequest();

                        }
                    }
                    
                    var awardId = await awardHistoryService.AddAwardHistory(award);
                    if (awardId != 0)
                    {
                        return Ok(awardId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        //get award history of an employee
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAwardHistory(int id)
        {
            var awardhistory = await awardHistoryService.GetAwardHistory(id);
            if (awardhistory == null)
            {
                return NotFound();
            }
            return Ok(awardhistory);

        }
    }
}
