using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardHistoryController : ControllerBase
    {
        XCartDbContext db;

        public AwardHistoryController(XCartDbContext db)
        {
            this.db = db;
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
        public async Task<IActionResult> AddEvent(AwardHistory award)
        {
            //--- member function to add patient ---//
            if (db != null)
            {
                await db.AwardHistory.AddAsync(award);
                await db.SaveChangesAsync();
                return Ok(award.Id);
            }
            return null;

        }
    }
}
