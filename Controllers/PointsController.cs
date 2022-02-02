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
    public class PointsController : ControllerBase
    {
        IPointService pointService;

        XCartDbContext db;

        //constructor 
        public PointsController(IPointService _pointService, XCartDbContext _db)
        {
            pointService = _pointService;
            db = _db;
        }

        #region Get Points By Employee Id
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetPointsByEmployeeId(int id)
        {
            try
            {
                var point = await pointService.GetPointsByEmployeeId(id);
                if (point == null)
                {
                    return NotFound();
                }
                return Ok(point);
            }
            catch
            {
                return BadRequest();
            }
                
        }
        #endregion
    }

}
