using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.Services;

namespace xcart.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : Controller
    {
        IAwardService awardService;

        XCartDbContext db;

        //constructor 
        public AwardsController(IAwardService _awardService, XCartDbContext _db)
        {
            awardService = _awardService;
            db = _db;
        }
        //get all awards 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            var awards = await awardService.GetAllAwards();
            if (awards == null)
            {
                return NotFound();
            }
            return Ok(awards);

        }
    }
}
