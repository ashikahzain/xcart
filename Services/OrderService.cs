﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                //LINQ
                //join order, user and status
                return await (from order in db.Order
                              from user in db.User
                              from status in db.StatusDescription
                              where order.UserId == user.Id
                              where order.StatusDescriptionId == status.Id
                              orderby order.Id descending

                              select new OrderViewModel
                              {
                                  Id = order.Id,
                                  DateOfOrder = order.DateOfOrder,
                                  DateOfDelivery = order.DateOfDelivery,
                                  UserName = user.Name,
                                  Points = order.Points,
                                  Status = status.Status
                              }).ToListAsync();
            }
            return null;
        }
        #endregion

        #region Get top two sold items
        public async Task<List<Item>> GetTrendingItems()
        {
            if (db != null)
            {
                //LINQ
                //Sorting and Taking the sum of Items
                var itemlist = await (from orderdetails in db.OrderDetails
                                      group orderdetails.Quantity by orderdetails.Item.Id into g
                                      orderby g.Sum() descending
                                      select g.Key).ToListAsync();

                //object of Item class
                var trendinglist = new List<Item>();
                int count = 0;

                //iterating through each item and assigning it to trendingitem
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
                                        }).FirstOrDefault();

                    //Appending only 2 items to trendinglist
                    if (count < 2)
                    {
                        trendinglist.Add(trendingitem);
                        count++;
                    }
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
            //Checking if Status is Open
            if (orderdetails.StatusDescriptionId == 1)
            {
                //object of StatusOrderViewModel
                var vm = new StatusOrderViewModel
                {
                    Id = order.Id,
                    DateOfDelivery = DateTime.Now.ToLongDateString(),
                    StatusDescriptionId = 2
                };
                //Mapping the changes to Order class
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
                //LINQ
                //join order, orderdetails and item
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

        #region Get Specific Orders
        public async Task<List<OrderViewModel>> GetSpecificOrders(int id)
        {
            if (db != null)
            {
                //LINQ
                //join order, user and statusdescription in ascending order of DateOfOrder
                return await (from order in db.Order
                              from user in db.User
                              from stat in db.StatusDescription  
                              where order.StatusDescriptionId==id
                              where order.UserId == user.Id
                              where order.StatusDescriptionId == stat.Id
                              orderby order.Id ascending

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


        #endregion

        #region Add Order
        public async Task<long> AddOrder(Order order)
        {
            //Object of class Order
            var orderdetails = new Order
            {
                DateOfOrder = order.DateOfOrder,
                UserId = order.UserId,
                DateOfDelivery = null,
                StatusDescriptionId = 1,
                Points = order.Points

            };
            //Adding new record 
            await db.Order.AddAsync(orderdetails);
            await db.SaveChangesAsync();
            return orderdetails.Id;
        }
        #endregion

        #region Add OrderDetails from direct Buy
        public async Task<long> AddOrderdetails(OrderDetails orderDetails)
        {
            //adding a new record to orderdetails
            await db.OrderDetails.AddAsync(orderDetails);
            await db.SaveChangesAsync();
            return orderDetails.Id;
        }
        #endregion

        #region Add Order Details from Cart
        public async Task<int> AddOrderDetails(OrderDetails orderDetails)
        {
            if (db != null)
            {
                //adding a new record to orderdetails
                await db.OrderDetails.AddAsync(orderDetails);
                await db.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        #endregion

    }
}
