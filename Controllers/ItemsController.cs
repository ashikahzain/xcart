using Microsoft.AspNetCore.Authorization;
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
