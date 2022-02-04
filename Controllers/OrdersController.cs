using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xcart.Models;
using xcart.Services;

namespace xcart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class OrdersController : ControllerBase
    {
        IOrderService orderService;

        XCartDbContext db;

        //constructor 
        public OrdersController(IOrderService _orderService, XCartDbContext _db)
        {
            orderService = _orderService;
            db = _db;
        }

        //get all orders
        [Authorize]
        [HttpGet] 
        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);

        }

        //get the top two trending items
        [HttpGet]
        [Route("trending-item")]
        public async Task<IActionResult> GetTrendingItems()
        {
            var orders = await orderService.GetTrendingItems();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);

        }

    }
}
