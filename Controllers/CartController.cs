using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CartController : ControllerBase
    {
        ICartService cartService;

        XCartDbContext db;

        ILoggerService logger;
        public CartController(ICartService _cartService, XCartDbContext _db, ILoggerService _logger)
        {
            cartService = _cartService;
            db = _db;
            logger = _logger;
        }

        #region Get Cart By Id
        [Authorize]
        [HttpGet]
        [Route("cart/{id}")]
        public async Task<IActionResult> GetCartById(long id)
        {
            try
            {
                logger.LogInfo($"Getting cart data of employee with ID {id}");
                var cart = await cartService.GetCartById(id);
                if (cart == null)
                {
                    logger.LogWarn($"Cart found empty for employee with ID {id}");
                    return NotFound();
                }
                logger.LogInfo($"Returning cart data of employee with ID {id}");
                return Ok(cart);
            }
            catch(Exception ex)
            {
                logger.LogWarn($"Exception error occured {ex.Message}");
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Add to Cart
        [Authorize]
        [HttpPost]

       public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            try
            {
                logger.LogInfo("Adding items to cart");
                if (ModelState.IsValid)
                {
                    var c = await cartService.AddToCart(cart);
                    if(c>0)
                    {
                        logger.LogInfo("Item has been added to cart");
                        return Ok(cart);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogWarn($"Exception error occured {ex.Message}");
                return BadRequest();
            }
            return BadRequest();     
        }
        #endregion

        #region Decrease Item Quantity
        [Authorize]
        [HttpGet]
        [Route("decrease-quantity/{id}")]

        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            try
            {
                logger.LogInfo("Decreasing quantity of item added to cart");
                var items = await cartService.DecreaseQuantity(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                logger.LogWarn($"Exception error occured {ex.Message}");
                return BadRequest();
            }

        }

        #endregion

        #region Delete Cart
        [Authorize]
        [HttpDelete]
        [Route("{id}")]

            public async Task<IActionResult> DeleteCart(int id)
            {
                try
                {
                logger.LogInfo($"Deleting item with {id} from cart");
                    var c = await cartService.DeleteCart(id);
                    if (c != null)
                    {
                    logger.LogInfo($"Item with ID {id} deleted from cart");
                        return Ok(c);
                    }
                    logger.LogWarn($"Item with ID {id} not found in cart");
                    return NotFound();
                }
                catch(Exception ex)
                {
                    logger.LogWarn($"Exception error occured {ex.Message}");
                    return BadRequest();
                }
            }
        #endregion

        #region increase Item Quantity
        [Authorize]
        [HttpGet]
            [Route("increase-quantity/{id}")]

            public async Task<IActionResult> IncreaseQuantity(int id)
            {
                try
                {
                    logger.LogInfo($"Increasing quantity of item with ID {id} in cart");
                    var items = await cartService.IncreaseQuantity(id);
                    logger.LogInfo($"Item quantity has been increased in cart");
                    return Ok(id);
                }
                catch (Exception ex)
                {
                    logger.LogWarn($"Exception error occured {ex.Message}");
                    return BadRequest();
                }
            }
        #endregion

        #region Delete Cart by User Id
        [Authorize]
        [HttpDelete]
        [Route("delete-cart/{id}")]

        public async Task<IActionResult> DeleteCartbyUserId(long id)
        {
            try
            {
                logger.LogInfo("Deleting cart by user id");
                var c = await cartService.DeleteCartbyUserId(id);
                if (c != null)
                {
                    logger.LogInfo("Cart has been deleted");
                    return Ok(c);
                }
                logger.LogWarn("Deletion was not done");
                return NotFound();
            }
            catch(Exception ex)
            {
                logger.LogWarn($"Exception error occured {ex.Message}");
                return BadRequest();
            }
        }
        #endregion

    }
}
