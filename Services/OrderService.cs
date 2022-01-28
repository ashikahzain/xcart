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

        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            if (db != null)
            {
                return await(from order in db.Order
                             from user in db.User
                             from status in db.StatusDescription
                             where order.User.Id == user.Id
                             where order.StatusDescription.Id==status.Id

                             select new OrderViewModel
                             {
                                 Id = order.Id,
                                 DateOfOrder=order.DateOfOrder,
                                 DateOfDelivery=order.DateOfDelivery,
                                 UserName=user.Name,
                                 Status= status.Status
                             }
                    ).ToListAsync();
            }
            return null;
        }

        /*
        public async List<Item> GetTrendingItem()
        {
            if (db != null)
            {
                 var itemId = from orderDetails in db.OrderDetails
                              group orderDetails by orderDetails.Item.Id into Occurance

                              select new Item
                              {
                                  Id = (from OD2 in Occurance
                                        select OD2.Item.Id).Max()
                              };
                 return await itemId.ToListAsync();
                return (from item in db.Item
                        from orderDetails in db.OrderDetails
                        where orderDetails.Item.Id == item.Id
                        group orderDetails by item into itemGroups
                        select new Item
                        {
                            Id = itemGroups,
                            numberOfOrders = itemGroups.Count()
                        }
                        ).OrderByDescending(x => x.numberOfOrders).Distinct().Take(10);

            }
            return null;
        }*/

    }
}
