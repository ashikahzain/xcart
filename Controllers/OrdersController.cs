using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xcart.Models;
using xcart.Services;
using xcart.ViewModel;

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
        //[Authorize]
        [HttpGet] 
        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
            //return Ok(db.Order.FirstOrDefault());
            
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
        
        //To change the status of an order
        [HttpPut]
        [Route("Change-Status")]
        public async Task<IActionResult> ChangeStatus([FromBody]StatusOrderViewModel order)
        {
                try
                {
                    await orderService.ChangeStatus(order);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
        }
        
        //To get the Item details by Order Id
        [HttpGet]
        [Route("GetOrderDetails/{id}")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int id)
        {
            var order = await orderService.GetOrderDetailsByOrderId(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        //Get Orders By Id
        [HttpGet]
        [Route("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrdersById(int id)
        {
            var order = await orderService.GetOrdersById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        [Route("Status/{id}")]
        public async Task<IActionResult> GetSpecificOrders(int id)
        {
            var order = await orderService.GetSpecificOrders(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

    }
}
