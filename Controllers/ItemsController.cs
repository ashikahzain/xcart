using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        [Route("active-items")]

        public async Task<IActionResult> GetAllActiveItems()
        {
            logger.LogInfo("Get All Active items");
            var items = await itemService.GetAllActiveItems();
            if (items == null)
            {
                logger.LogWarn("Active Items Not Found");
                return NotFound("no active items");
            }
            logger.LogInfo($" Active Items Returned {items}");
            return Ok(items);
        }
        #endregion

        #region get all inactive items 
        [Authorize]
        [HttpGet]
        [Route("inactive-items")]
        public async Task<IActionResult> GetAllInactiveItems()
        {
            logger.LogInfo("Get All InActive items");
            var items = await itemService.GetAllInactiveItems();
            if (items == null)
            {
                logger.LogWarn("Inactive Items Not Found");
                return NotFound();
            }
            logger.LogInfo($"Inactive Items Returned {items}");
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
                    logger.LogInfo($"Add to Item List {item}");
                    var eventId = await itemService.AddItem(item);
                    logger.LogInfo($"Items added  to the itemList with Item id {eventId}");
                    return Ok(eventId);
                }
                catch (Exception)
                {
                    logger.LogWarn("Error encountered. Item not added to the Item list");
                    return BadRequest();
                }
            }
            logger.LogError("Bad Request");
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
                logger.LogInfo($"Get item by itemid {id}");
                var item = await itemService.GetItemById(id);
                if (item == null)
                {
                    logger.LogWarn($"Item {id} Not Found");
                    return NotFound();
                }
                logger.LogInfo($"Item {id} is returned {item}");
                return Ok(item);
            }
            catch (Exception)
            {
                logger.LogError("Bad Request");
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
                logger.LogInfo($"Item {id} id to be deleted");
                var item = await itemService.DeleteItem(id);  
                if (item == 0)
                {
                    logger.LogWarn($"Item {id} Not Found");
                    return NotFound();
                }
                logger.LogInfo($"Item {id} is deleted");
                return Ok(item);
            }
            catch (Exception)
            {
                logger.LogError("Bad Request");
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
                    logger.LogInfo("Updating Item details to");
                    await itemService.UpdateItem(item);
                    logger.LogInfo($"Updated the Item details to item {item.Id}");
                    return Ok(item);
                }
                catch (Exception)
                {
                    logger.LogError("Bad Request");
                    return BadRequest();
                }
            }
            logger.LogError("Bad Request");
            return BadRequest();
        }
        #endregion
    }
}
