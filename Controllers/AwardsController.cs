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

        ILoggerService logger;

        //constructor 
        public AwardsController(IAwardService _awardService, XCartDbContext _db, ILoggerService _logger)
        {
            awardService = _awardService;
            db = _db;
            logger = _logger;
        }

        #region Get All Awards /api/awards GET
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            try
            {
                logger.LogInfo("Get all awards");
                var awards = await awardService.GetAllAwards();
                if (awards == null)
                {
                    logger.LogWarn("Awards not found");
                    return NotFound("No active awards");
                }
                logger.LogInfo("Getting all award records");
                return Ok(awards);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
        #endregion

        #region Get Award by Id /api/awards/{id}
        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetAwardById(int id)
        {
            try
            {
                logger.LogInfo($"Getting award with ID {id}");
                var award = await awardService.GetAwardById(id);
                if (award == null)
                {
                    logger.LogWarn($"Award with ID {id} not found");
                    return NotFound("No award with the given Id");
                }
                logger.LogInfo($"Returning award with ID {id}");
                return Ok(award);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        #endregion

        #region Add new Award /api/awards POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAward([FromBody] Award model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    logger.LogInfo("Adding an Award API called");
                    var awardId = await awardService.AddAward(model);
                    if (awardId > 0)
                    {
                        logger.LogInfo("Award has been added");
                        return Ok(awardId);
                    }
                    else
                    {
                        logger.LogWarn("Award has not been added");
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    logger.LogWarn("Award has not been added due to incorrect format of entry parameters");
                    return BadRequest("Valid Data expected");
                }
            }
            logger.LogWarn("Award has not been added due to incorrect format of entry parameters");
            return BadRequest("Valid Data expected"); 
        }
        #endregion

        #region Update Award /api/awards/update-award PUT
        [Authorize]
        [HttpPut]
        [Route("update-award")]
        public async Task<IActionResult> UpdateAward([FromBody] Award model)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    logger.LogInfo("Updating Award detail");
                    await awardService.UpdateAward(model);
                    return Ok();
                }
                catch (Exception)
                {
                    logger.LogWarn("Award not updated due to incorrect input properties");
                    return BadRequest("Valid Data expected");
                }
            }
            return BadRequest();
        }
        #endregion

        #region Delete Award /api/awards/delete-award/{id}
        [Authorize]
        [HttpPut]
        [Route("delete-award/{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            //Check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    logger.LogInfo($"Deleting award with ID {id}");
                    await awardService.DeleteAward(id);
                    return Ok();
                }
                catch (Exception)
                {
                    logger.LogWarn($"Award of ID {id} was not found. Deletion could not be done.");
                    return BadRequest("No award with the given Id");
                }
            }
            logger.LogWarn($"Award of ID {id} was not found. Deletion could not be done.");
            return BadRequest();
        }
        #endregion

    }
}
