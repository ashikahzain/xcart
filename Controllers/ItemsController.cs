﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using xcart.Models;
using xcart.Services;
using xcart.ViewModel;

namespace xcart.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        IItemService itemService;

        XCartDbContext db;

        ILoggerService logger;

        //constructor 
        public ItemsController(IItemService _itemService, XCartDbContext _db, ILoggerService _logger)  
        {
            itemService = _itemService;
            db = _db;
            logger = _logger;
        }


        #region get all active items 
        //[Authorize]
        [HttpGet]
        [Route("active-items")]

        public async Task<IActionResult> GetAllActiveItems()
        {
            logger.LogInfo("Get All Active items hit");
            var items = await itemService.GetAllActiveItems();
            if (items == null)
            {
               
                return NotFound("no active items");
            }
           
            return Ok(items);



        }
        #endregion

        #region get all inactive items 
        //[Authorize]
        [HttpGet]
        [Route("inactive-items")]
        public async Task<IActionResult> GetAllInactiveItems()
        {
           
            
            var items = await itemService.GetAllInactiveItems();
                if (items == null)
                {
                
                return NotFound();
                }
          
            return Ok(items);
       
            


        }
        #endregion

        #region add item
        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddItem(ItemViewModel item)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var eventId = await itemService.AddItem(item);
                    return Ok(eventId);
                }
                catch (Exception)
                {
                    //logger.LogError("Error encountered");
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

        #region Get Item by Id
        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                //logger.LogError("Bad Request Error");
                var item = await itemService.GetItemById(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception)
            {
                
                return BadRequest();
            }

        }

        #endregion

        #region Delete Item by changing is active status
        [Authorize]
        [HttpGet]
        [Route("delete-item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await itemService.DeleteItem(id);  
                if (item == 0)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion

        #region Update Item
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] ItemViewModel item)
        {
            //Check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await itemService.UpdateItem(item);
                    return Ok(item);
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
