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
    }
}
