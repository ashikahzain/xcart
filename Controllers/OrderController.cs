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
    public class OrderController : ControllerBase
    {
        IOrderService orderService;

        XCartDbContext db;

        public OrderController(IOrderService _orderService, XCartDbContext _db)
        {
            orderService = _orderService;
            db = _db;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);

        }
        /*
        [HttpGet("trending")]
        public Task<IActionResult> GetTrending()
        {


            if (db != null)
            {
                var itemId = from orderDetails in db.OrderDetails
                             group orderDetails by orderDetails.Item.Id into Occurance

                             select new Item
                             {
                                 Id = (from OD2 in Occurance
                                       select OD2.Item.Id).Max()
                             };
                //int i = Convert.ToInt32(itemId);
                return Ok(itemId)

            }
            return null;
        }*/
    }
}
