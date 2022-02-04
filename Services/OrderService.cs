using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public class OrderService : IOrderService
    {
        XCartDbContext db;

        public OrderService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Get all Orders

        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            if (db != null)
            {               
                return await (from order in db.Order
                              from user in db.User
                              from status in db.StatusDescription
                              where order.User.Id == user.Id
                              where order.StatusDescription.Id == status.Id

                              select new OrderViewModel
                              {
                                  Id = order.Id,
                                  DateOfOrder = order.DateOfOrder,
                                  DateOfDelivery = order.DateOfDelivery,
                                  UserName = user.Name,
                                  Status = status.Status
                              }
                    ).ToListAsync();
            }
            return null;
        }
        #endregion
        #region Get top two sold items
        public async Task<List<Item>> GetTrendingItems()
        {
            if (db != null)
            {
                var itemlist = await(from orderdetails in db.OrderDetails        
                                    group orderdetails.Quantity by orderdetails.Item.Id into g 
                                    orderby g.Sum() descending
                                    select g.Key).Take(2).ToListAsync();
                var trendinglist = new List<Item>();
                foreach(int itemId in itemlist)
                {
                    var trendingitem=(from item in db.Item
                                  where item.Id == itemId
                                  select new Item
                                  {
                                      Id=itemId,
                                      Name =item.Name,
                                      Image=item.Image,
                                      Quantity=item.Quantity,
                                      IsActive=item.IsActive,
                                      Points =item.Points
                                  }
                                 ).FirstOrDefault();
                    trendinglist.Add(trendingitem);
                    
                }
                return trendinglist;
                    
            }
            return null;
        }
        #endregion
    }
}
