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

        #region Get All Orders
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders(int pagenumber, int pagesize)
        {
            try
            {
                var orders = await orderService.GetAllOrders(pagenumber, pagesize);
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region Get top 2 trending items
        [HttpGet]
        [Route("trending-item")]
        public async Task<IActionResult> GetTrendingItems()
        {
            try
            {
                var orders = await orderService.GetTrendingItems();
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region To change the status of an order
        [HttpPut]
        [Route("Change-Status")]
        public async Task<IActionResult> ChangeStatus([FromBody] StatusOrderViewModel order)
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
        #endregion

        #region To get the orderdetails by order id
        [HttpGet]
        [Route("{id}/order-details")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(long id)
        {
            try
            {
                var order = await orderService.GetOrderDetailsByOrderId(id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region To get orders based on status
        [HttpGet]
        [Route("Status/{id}")]
        public async Task<IActionResult> GetSpecificOrders(int id)
        {
            try
            {
                var order = await orderService.GetSpecificOrders(id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch
            {
                return BadRequest();
            } 
        }
        #endregion

        #region Add to Order from Buy
        [HttpPost]
        public async Task<IActionResult> AddToOrder([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //c is assigned with orderid
                    var c = await orderService.AddOrder(order);
                    if (c > 0)
                    {
                        //to remove points from the current points
                        var userpoints = pointService.RemovePointsonCheckout(order.Points, order.UserId);
                        return Ok(c);
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

        #region Add to Order from Cart
        [HttpPost("cart")]
        public async Task<IActionResult> AddToOrderFromCart([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //c gets assigned with orderid
                    var c = await orderService.AddOrder(order);
                    var userId = order.UserId;
                    //itemList is list of cart details corresponding to a userId
                    var itemList = cartservice.GetCartByUserId(userId);

                    //for each item in cart, it gets added to the orderdetails class
                    //And decrease the total quantity from Item class
                    foreach (Cart item in itemList)
                    {
                        OrderDetails orderDetails = new OrderDetails();

                        orderDetails.OrderId = c;
                        orderDetails.ItemId = item.ItemId;
                        orderDetails.Quantity = item.Quantity;

                        int x = await orderService.AddOrderDetails(orderDetails);
                        itemService.DescreaseQuantity(item.ItemId, item.Quantity);
                    }
                    await cartservice.DeleteCartbyUserId(userId);
                    //Removes used points from current points
                    if (c > 0)
                    {
                        var userpoints = pointService.RemovePointsonCheckout(order.Points, order.UserId);
                        return Ok(c);
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


        #region To add to orderdetails
        [HttpPost]
        [Route("order-details")]
        public async Task<IActionResult> AddToOrderDetails([FromBody] OrderDetails order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //c is assigned with orderdetailsid
                    var c = await orderService.AddOrderdetails(order);
                    if (c > 0)
                    {
                        //decreases the quantity used from Total quantity in item class
                        var itemquantity = itemService.DescreaseQuantity(order.ItemId, order.Quantity);
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
        #endregion

        #region Get order count

        [HttpGet]
        [Route("order-count")]
        public async Task<IActionResult> GetOrderCount()
        {
            try
            {
                var count = await orderService.GetOrderCount();
                return Ok(count);
            }
            catch
            {
                return BadRequest();
            }
            
        }


        #endregion

        #region Get status order count
        [HttpGet]
        [Route("{id}/order-status-count")]
        public async Task<IActionResult> GetStatusCount(int id)
        {
            try
            {
                var count = await orderService.GetStatusCount(id);
                return Ok(count);
            }
            catch
            {
                return BadRequest();
            }  
        }
        #endregion

    }
}
