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

        IPointService pointService;

        ICartService cartservice;

        IItemService itemService;

        //constructor 
        public OrdersController(IOrderService _orderService, XCartDbContext _db,IPointService _pointService,ICartService _cartService,IItemService _itemservice)
        {
            orderService = _orderService;
            db = _db;
            pointService = _pointService;
            cartservice = _cartService;
            itemService = _itemservice;
            
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

        #region Add to Order
        [HttpPost]
        public async Task<IActionResult> AddToOrder([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var c = await orderService.AddOrder(order);
                    var userId = order.UserId;
                    var itemList = cartservice.GetCartByUserId(userId);

                    foreach (Cart item in itemList)
                    {
                        OrderDetails orderDetails = new OrderDetails();

                        orderDetails.OrderId = c;
                        orderDetails.ItemId = item.ItemId;
                        orderDetails.Quantity = item.Quantity;

                        int x= await orderService.AddOrderDetails(orderDetails);
                    }

                    await cartservice.DeleteCartbyUserId(userId);

                    if (c > 0)
                    {
                        var userpoints = pointService.RemovePointsonCheckout(order.Points, order.UserId);
                        //return Ok(c);
                    }

                    return Ok();
                }
           }
           catch
           {
               return BadRequest();
           }
            return BadRequest();
        }
        #endregion

        #region Add to Order
        [HttpPost("cart")]
        public async Task<IActionResult> AddToOrderFromCart([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var c = await orderService.AddOrder(order);
                    var userId = order.UserId;
                    var itemList = cartservice.GetCartByUserId(userId);

                    foreach (Cart item in itemList)
                    {
                        OrderDetails orderDetails = new OrderDetails();

                        orderDetails.OrderId = c;
                        orderDetails.ItemId = item.ItemId;
                        orderDetails.Quantity = item.Quantity;

                        int x = await orderService.AddOrderDetails(orderDetails);
                    }

                    await cartservice.DeleteCartbyUserId(userId);

                    if (c > 0)
                    {
                        var userpoints = pointService.RemovePointsonCheckout(order.Points, order.UserId);
                        //return Ok(c);
                    }

                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
            return BadRequest();
        }
        #endregion

        [HttpGet]
        [Route("quantity-check/{id}")]
        public async Task<IActionResult> GetQuantity(int id)
        {
            var order = await cartservice.CompareQuantity(id);
            return Ok(order);
        }

        [HttpPost]
        [Route("order-details")]
        public async Task<IActionResult> AddToOrderDetails([FromBody] OrderDetails order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var c = await orderService.AddOrderdetails(order);
                    if (c > 0)
                    {
                        var itemquantity= itemService.DescreaseQuantity(order.ItemId, order.Quantity);
                        return Ok(c);
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
            return BadRequest();
        }
        }
}
