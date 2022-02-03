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

        [HttpGet]
        [Route("trending-item")]
        public async Task<IActionResult> GetTrendingIteme()
        {
            var orders = await orderService.GetTrendingIteme();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);

        }

    }
}
