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
        [HttpGet]
        [Route("cart/{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await cartService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        #endregion

        #region increase Item Quantity
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

        #region increase Item Quantity
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


       
    }
}
