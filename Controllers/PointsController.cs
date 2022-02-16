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
        IPointService pointService;
        XCartDbContext db;

        //Constructor
        public PointsController(IPointService _pointService, XCartDbContext _db)
        {
            pointService = _pointService;
            db = _db;
        }

        #region Get Points By Employee Id
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPointsByEmployeeId(long id)
        {
            try
            {
                var point = await pointService.GetPointsByEmployeeId(id);

                if (point == null)
                {
                    return NotFound("Points of employee not found");
                }
                return Ok(point.CurrentPoints);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Get Point Table
        [HttpGet]
        public  List<Point> GetPointsByEmployeeId()
        {
            try
            {
                var point = db.Point.ToList();

                if (point == null)
                {
                    return null;
                }
                return point;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region Get Point Limit
        [Authorize]
        [HttpGet]
        [Route("point-limit")]
        public async Task<IActionResult> GetPointLimit()
        {
            try
            {
                var pointlimit = await pointService.GetPointLimit();

                if (pointlimit == 0)
                {
                    return NotFound("No Point Limit");
                }
                return Ok(pointlimit);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Get Point Limit
        [Authorize]
        [HttpPut]
        [Route("point-limit")]
        public async Task<IActionResult> UpdatePointLimit(PointLimit pointLimit)
        {
            try
            {
                var pointlimit = await pointService.UpdatePointLimit(pointLimit);
                return Ok(pointlimit);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion
    }

}
