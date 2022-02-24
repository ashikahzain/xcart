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
        public CartController(ICartService _cartService, XCartDbContext _db)
        {
            cartService = _cartService;
            db = _db;
        }

        #region Get Cart By Id
        [Authorize]
        [HttpGet]
        [Route("cart/{id}")]
        public async Task<IActionResult> GetCartById(long id)
        {
            var cart = await cartService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        #endregion

        #region Add to Cart
        [Authorize]
        [HttpPost]

       public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var c = await cartService.AddToCart(cart);
                    if(c>0)
                    {
                        return Ok(cart);
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

        #region increase Item Quantity
        [Authorize]
        [HttpGet]
        [Route("decrease-quantity/{id}")]

        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            try
            {
                var items = await cartService.DecreaseQuantity(id);
                return Ok(id);
            }
            catch (Exception)
            {
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
                    var c = await cartService.DeleteCart(id);
                    if (c != null)
                    {
                        return Ok(c);
                    }
                    return NotFound();
                }
                catch
                {
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
                    var items = await cartService.IncreaseQuantity(id);
                    return Ok(id);
                }
                catch (Exception)
                {
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
                var c = await cartService.DeleteCartbyUserId(id);
                if (c != null)
                {
                    return Ok(c);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

    }
}
