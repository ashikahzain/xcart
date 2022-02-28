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
        ILoggerService logger;

        //constructor 
        public OrdersController(IOrderService _orderService, XCartDbContext _db,IPointService _pointService,
            ICartService _cartService,IItemService _itemservice, ILoggerService _logger)
        {
            orderService = _orderService;
            db = _db;
            pointService = _pointService;
            cartservice = _cartService;
            itemService = _itemservice;
            logger = _logger;
            
        }

        #region Get All Orders
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders(int pagenumber, int pagesize)
        {
            try
            {
                logger.LogInfo("Get all the orders");
                var orders = await orderService.GetAllOrders(pagenumber, pagesize);
                if (orders == null)
                {
                    logger.LogWarn("Orders Not Found");
                    return NotFound("No Orders found in database");
                }
                logger.LogInfo($"All Orders returned {orders}");
                return Ok(orders);
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            }
        }
        #endregion

        #region Get top 2 trending items
        [Authorize]
        [HttpGet]
        [Route("trending-item")]
        public async Task<IActionResult> GetTrendingItems()
        {
            try
            {
                logger.LogInfo("Get top 2 trending items");
                var orders = await orderService.GetTrendingItems();
                if (orders == null)
                {
                    logger.LogWarn("No trending items found");
                    return NotFound("No trending items found");
                }
                logger.LogInfo($"Top 2 trending items returned {orders}");
                return Ok(orders);
            }
            catch
            {
                logger.LogError("Bad request");
                return BadRequest();
            }
        }
        #endregion

        #region To change the status of an order
        [Authorize]
        [HttpPut]
        [Route("Change-Status")]
        public async Task<IActionResult> ChangeStatus([FromBody] StatusOrderViewModel order)
        {
            try
            {
                logger.LogInfo("To change the status of an order");
                await orderService.ChangeStatus(order);
                logger.LogInfo("Order status changed");
                return Ok();
            }
            catch
            {
                logger.LogError("Bad request");
                return BadRequest();
            }
        }
        #endregion

        #region To get the orderdetails by order id
        [Authorize]
        [HttpGet]
        [Route("{id}/order-details")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(long id)
        {
            try
            {
                logger.LogInfo($"To return the order details od order {id}");
                var order = await orderService.GetOrderDetailsByOrderId(id);
                if (order == null)
                {
                    logger.LogWarn($"Order {id} was not found");
                    return NotFound("No items found for that particular order Id");
                }
                logger.LogInfo($"Order details of order {id} was returned. {order}");
                return Ok(order);
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            }
        }
        #endregion

        #region To get orders based on status
        [Authorize]
        [HttpGet]
        [Route("Status/{id}")]
        public async Task<IActionResult> GetSpecificOrders(int id, int pagenumber, int pagesize)
        {
            try
            {
                logger.LogInfo("To return the list of orders based on status");
                var order = await orderService.GetSpecificOrders(id, pagenumber, pagesize);
                if (order == null)
                {
                    logger.LogWarn("List of orders qas not found");
                    return NotFound("No orders found for this status");
                }
                logger.LogInfo("List of orders were returned");
                return Ok(order);
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            } 
        }
        #endregion

        #region Add to Order from Buy
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToOrder([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.LogInfo("An Item is orderes directly");
                    //c is assigned with orderid
                    var c = await orderService.AddOrder(order);
                    if (c > 0)
                    {
                        //to remove points from the current points
                        var userpoints = pointService.RemovePointsonCheckout(order.Points, order.UserId);
                        logger.LogInfo($"Order {c} is Added to Order List");
                        return Ok(c);
                    }
                    logger.LogInfo("Order added to orderlist");
                    return Ok();
                }
           }
           catch
           {
                logger.LogError("Bad Request");
               return BadRequest();
           }
            logger.LogError("Bad Request");
            return BadRequest();
        }
        #endregion

        #region Add to Order from Cart
        [Authorize]
        [HttpPost("cart")]
        public async Task<IActionResult> AddToOrderFromCart([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.LogInfo("Order is added to order list from cart");
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
                        logger.LogInfo($"Order {c} is deleted from the cart");
                        return Ok(c);
                    }
                    logger.LogInfo("Order {c} is added to the orderlist from cart");
                    return Ok();
                }
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            }
            logger.LogError("Bad Request");
            return BadRequest();
        }
        #endregion

        #region To add to orderdetails
        [Authorize]
        [HttpPost]
        [Route("order-details")]
        public async Task<IActionResult> AddToOrderDetails([FromBody] OrderDetails order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.LogInfo(" order details is added");
                    //c is assigned with orderdetailsid
                    var c = await orderService.AddOrderdetails(order);
                    if (c > 0)
                    {
                        //decreases the quantity used from Total quantity in item class
                        var itemquantity = itemService.DescreaseQuantity(order.ItemId, order.Quantity);
                        logger.LogInfo($"Orderdetail {c} is returned");
                        return Ok(c);
                    }
                }
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            }
            logger.LogError("Bad Request");
            return BadRequest();
        }
        #endregion

        #region Get order count
        [Authorize]
        [HttpGet]
        [Route("order-count")]
        public async Task<IActionResult> GetOrderCount()
        {
            try
            {
                logger.LogInfo("TO get the count of total orders");
                var count = await orderService.GetOrderCount();
                if (count == 0)
                {
                    logger.LogWarn("No orders found");
                    return NotFound("No orders found");
                }
                logger.LogInfo("Number of orders is returned");
                return Ok(count);
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            }
        }
        #endregion

        #region Get status order count
        [Authorize]
        [HttpGet]
        [Route("{id}/order-status-count")]
        public async Task<IActionResult> GetStatusCount(int id)
        {
            try
            {
                logger.LogInfo("To get the count of order based on status");
                var count = await orderService.GetStatusCount(id);
                if (count == 0)
                {
                    logger.LogWarn("No orders found");
                    return NotFound("No orders found for that status");
                }
                logger.LogInfo("The count of orders is returned");
                return Ok(count);
            }
            catch
            {
                logger.LogError("Bad Request");
                return BadRequest();
            }  
        }
        #endregion

    }
}
