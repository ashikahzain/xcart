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

        #region Get All Awards
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
        #endregion

        #region Add new Award
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAward([FromBody] Award model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var awardId = await awardService.AddAward(model);
                    if (awardId > 0)
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
        #endregion

        #region Update Award
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAward([FromBody] Award model)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await awardService.UpdateAward(model);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

        #region Delete Award
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            //Check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await awardService.DeleteAward(id);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

    }
}
