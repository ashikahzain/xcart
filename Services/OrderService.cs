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

        public async Task<List<int>> GetTrendingIteme()
        {
            if (db != null)
            {
                //join User and Point
                /*return await(from orders in db.OrderDetails
                             from item in db.Item
                             group orders by orders.Item.Id into a
                             select new TrendingItemViewModel
                             {
                                 Id = a.Key,
                                 Quantity = a.Count(x=>x.Quantity),
                             }).FirstOrDefaultAsync();*/

                return await db.OrderDetails.GroupBy(a => a.Item.Id).OrderByDescending(b => b.Count()).Take(4).Select(a => a.Key).ToListAsync();
            }
            return null;
        }

    }
}
