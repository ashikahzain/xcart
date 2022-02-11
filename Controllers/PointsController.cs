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
    public class PointsController : ControllerBase
    {

        //Dependency Injection

        IPointService pointService;

        XCartDbContext db;

        
        public PointsController(IPointService _pointService, XCartDbContext _db)
        {
            pointService = _pointService;
            db = _db;
        }

        #region Get Points By Employee Id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPointsByEmployeeId(int id)
        {
            
                var point = await pointService.GetPointsByEmployeeId(id);

                if (point == null)
                {
                    return NotFound();
                }
                return Ok(point.CurrentPoints);
        }
        #endregion

        #region Get Point Table
        [HttpGet]
        public  List<Point> GetPointsByEmployeeId()
        {

            var point =  db.Point.ToList();

            if (point == null)
            {
                return null;
            }
            return point;
        }
        #endregion
    }

}
