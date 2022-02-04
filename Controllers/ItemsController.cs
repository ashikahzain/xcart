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
using xcart.ViewModel;

namespace xcart.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        IItemService itemService;

        XCartDbContext db;

        //constructor 
        public ItemsController(IItemService _itemService, XCartDbContext _db)
        {
            itemService = _itemService;
            db = _db;
        }
        //get all items 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await itemService.GetAllItems();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);

        }
    }
}
