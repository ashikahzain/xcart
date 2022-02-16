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
        public async Task<List<EmployeeCartViewModel>> GetCartById(long id)
        {
           
                if (db != null)
                {
                    return await (from item in db.Item
                                  from cart in db.Cart

                                  where cart.UserId == id &&
                                  item.IsActive == true &&
                                  cart.ItemId == item.Id

                                  select new EmployeeCartViewModel
                                  {
                                      CartId=cart.Id,
                                      ItemId = item.Id,
                                      ItemName = item.Name,
                                      ItemImage = item.Image,
                                      Quantity = cart.Quantity,
                                      TotalQuantity=item.Quantity,
                                      ItemPoints = item.Points,
                                      IsActive = item.IsActive
                                  }).ToListAsync();
                }
                return null;
            
           
        }
        #endregion

        #region Get Cart By UserId

        public List<Cart> GetCartByUserId(long id)
        {
            if (db != null)
            {
                return (
                             from cart in db.Cart

                             where cart.UserId == id

                             select new Cart
                             {
                                 ItemId=cart.ItemId,
                                 Quantity=cart.Quantity
                             }).ToList();
            }
            return null;
        }

        #endregion

        #region Get all cart by id
        public async Task<List<Cart>> GetAllCartById(int id)
        {
            if (db != null)
            {
                return await db.Cart.ToListAsync();
            }
            return null;
        }
        #endregion
   
        #region Add to Cart
        public async Task<long> AddToCart(Cart cart)
        {

            if (db != null)
            {
                await db.Cart.AddAsync(cart);
                await db.SaveChangesAsync();
                return cart.UserId;
            }
            return 0;
        }




        #endregion

        #region Delete Cart
        public async Task<Cart> DeleteCart(int id)
        {
            if (db != null)
            {
                Cart doc = db.Cart.Where(x=>x.Id == id).FirstOrDefault();
                db.Cart.Remove(doc);
                await db.SaveChangesAsync(); 
                return doc;
            }
            return null;

        }
        #endregion

        #region Increase Quantity on cart
        //Add quantity (cart update)
        public async Task<int> IncreaseQuantity(int id)
        {
            var itemdetails = await db.Cart.FirstOrDefaultAsync(i => i.Id == id);
            var vm = new ItemQuantityVm()
            {
                Id = id,
                Quantity = itemdetails.Quantity + 1
            };
            vm.MaptoModel(itemdetails);
            db.SaveChanges();
            return id;
        }
        #endregion

        #region Decrease Quantity on cart
        //DESCREASE QUANTITY
        public async Task<int> DecreaseQuantity(int id)
        {

            var itemdetails = await db.Cart.FirstOrDefaultAsync(i => i.Id == id);
            var vm = new ItemQuantityVm()
            {
                Id = id,
                Quantity = itemdetails.Quantity - 1
            };
            vm.MaptoModel(itemdetails);
            db.SaveChanges();
            return id;
        }
        #endregion

        #region Delete Cart by User Id

        public async Task<List<Cart>> DeleteCartbyUserId(long id)
        {
            if (db != null)
            {
                var doc = await db.Cart.Where(x => x.UserId == id).ToListAsync();
                foreach (Cart c in doc)
                {
                    db.Cart.Remove(c);
                    await db.SaveChangesAsync();

                }
                return doc;

            }
            return null;
        }
        #endregion


       
    }
}
