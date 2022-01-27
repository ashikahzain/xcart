using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xcart.Services;

namespace xcart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
             orderService= _orderService;
        }

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
}
}
