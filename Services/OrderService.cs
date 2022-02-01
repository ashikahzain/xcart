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

    }
}
