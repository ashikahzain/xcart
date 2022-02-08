using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public class CartService: ICartService
    {
        XCartDbContext db;

        public CartService(XCartDbContext db)
        {
            this.db = db;
        }


        #region Get Cart By Id
        public async Task<List<EmployeeCartViewModel>> GetCartById(int id)
        {
           
                if (db != null)
                {
                    /*
                    var cart = await db.Cart.ToListAsync();
                    return cart;
                    */
                    return await (from item in db.Item
                                  from cart in db.Cart

                                  where cart.UserId == id &&
                                  item.IsActive == true &&
                                  cart.ItemId == item.Id

                                  select new EmployeeCartViewModel
                                  {
                                      ItemId = item.Id,
                                      ItemName = item.Name,
                                      ItemImage = item.Image,
                                      Quantity = cart.Quantity,
                                      ItemPoints = item.Points,
                                      IsActive = item.IsActive
                                  }).ToListAsync();
                }
                return null;
            
           
        }
        #endregion
    }
}
