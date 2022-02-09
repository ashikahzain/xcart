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
                              where order.UserId == user.Id
                              where order.StatusDescriptionId == status.Id

                              select new OrderViewModel
                              {
                                  Id = order.Id,
                                  DateOfOrder = order.DateOfOrder,
                                  DateOfDelivery = order.DateOfDelivery,
                                  UserName = user.Name,
                                  Points = order.Points,
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
                var itemlist = await (from orderdetails in db.OrderDetails
                                      group orderdetails.Quantity by orderdetails.Item.Id into g
                                      orderby g.Sum() descending
                                      select g.Key).Take(2).ToListAsync();
                var trendinglist = new List<Item>();
                foreach (int itemId in itemlist)
                {
                    var trendingitem = (from item in db.Item
                                        where item.Id == itemId
                                        select new Item
                                        {
                                            Id = itemId,
                                            Name = item.Name,
                                            Image = item.Image,
                                            Quantity = item.Quantity,
                                            IsActive = item.IsActive,
                                            Points = item.Points
                                        }
                                 ).FirstOrDefault();
                    trendinglist.Add(trendingitem);

                }
                return trendinglist;

            }
            return null;
        }
        #endregion

        #region Change the Status of Order
        public async Task ChangeStatus(StatusOrderViewModel order)
        {
            var orderdetails = await db.Order.FirstOrDefaultAsync(o => o.Id == order.Id);
            if (orderdetails.StatusDescriptionId == 1)
            {
                var vm = new StatusOrderViewModel
                {
                    Id = order.Id,
                    DateOfDelivery = DateTime.Now.ToLongDateString(),
                    StatusDescriptionId = 2
                };
                vm.MaptoModel(orderdetails);
                db.SaveChanges();
            }
        }

        #endregion
        
        #region Get order Details By Order Id
        public async Task<List<OrderDetailsViewModel>> GetOrderDetailsByOrderId(int id)
        {
            if (db != null)
            {
                return await (from order in db.Order
                              from orderdetails in db.OrderDetails
                              from item in db.Item
                              where order.Id == id && 
                              order.Id == orderdetails.OrderId && 
                              orderdetails.ItemId == item.Id
                              select new OrderDetailsViewModel
                              {
                                  Id = order.Id,
                                  Name = item.Name,
                                  Quantity = orderdetails.Quantity
                              }).ToListAsync();
            }
            return null;
        }
        #endregion

        #region Get Order By Id
        public async Task<Order> GetOrdersById(int id)
        {
            if (db != null)
            {
                return await (from order in db.Order
                              where order.Id == id

                              select new Order
                              {
                                  Id = order.Id,
                                  DateOfOrder = order.DateOfOrder,
                                  DateOfDelivery = order.DateOfDelivery,
                                  Points = order.Points,
                                  StatusDescriptionId=order.StatusDescriptionId
                              }
                    ).FirstOrDefaultAsync();
            }
            return null;
        }


        #endregion

        #region Get Specific Orders
        public async Task<List<OrderViewModel>> GetSpecificOrders(int id)
        {
            if (db != null)
            {
                return await (from order in db.Order
                              from user in db.User
                              from stat in db.StatusDescription
                              where order.StatusDescriptionId==id
                              where order.UserId == user.Id
                              where order.StatusDescriptionId == stat.Id

                              select new OrderViewModel
                              {
                                  Id = order.Id,
                                  DateOfOrder = order.DateOfOrder,
                                  DateOfDelivery = order.DateOfDelivery,
                                  UserName = user.Name,
                                  Points = order.Points,
                                  Status = stat.Status
                              }
                    ).ToListAsync();
            }
            return null;
        }

        public async Task<long> AddOrder(Order order)
        {
           await db.Order.AddAsync(order);
            await db.SaveChangesAsync();
            return order.Id;
        }
        #endregion
    }
}
