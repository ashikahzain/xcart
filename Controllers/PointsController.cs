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
        ILoggerService logger;

        //Constructor
        public PointsController(IPointService _pointService, XCartDbContext _db,ILoggerService logger)
        {
            pointService = _pointService;
            db = _db;
            this.logger = logger;
        }

        #region Get Points By Employee Id
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPointsByEmployeeId(long id)
        {
            try
            {
                logger.LogInfo($"get points of an employee with userId {id}");
                var point = await pointService.GetPointsByEmployeeId(id);

                if (point == null)
                {
                    logger.LogWarn("Point of employee not found");
                    return NotFound("Points of employee not found");
                }
                logger.LogInfo($"Point of employee returned:{point.CurrentPoints}");
                return Ok(point.CurrentPoints);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
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
                logger.LogInfo("Get points of all employees");
                var point = db.Point.ToList();

                if (point == null)
                {
                    logger.LogWarn("no Points found");
                    return null;
                }
                return point;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
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
                logger.LogInfo("get point info");
                var pointlimit = await pointService.GetPointLimit();

                if (pointlimit == 0)
                {
                    logger.LogWarn("point limit not found");
                    return NotFound("No Point Limit");
                }
                logger.LogInfo($"point limit returned:{pointlimit}");
                return Ok(pointlimit);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Update Point Limit
        [Authorize]
        [HttpPut]
        [Route("point-limit")]
        public async Task<IActionResult> UpdatePointLimit(PointLimit pointLimit)
        {
            try
            {
                logger.LogInfo($"update point limit to {pointLimit.Point}");
                var pointlimit = await pointService.UpdatePointLimit(pointLimit);

                if (pointlimit != 0)
                {
                    return Ok(pointlimit);

                }
                return NotFound();
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
