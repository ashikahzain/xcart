﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface IOrderService
    {
        public Task<List<OrderViewModel>> GetAllOrders();
        Task<List<Item>> GetTrendingItems();
        Task ChangeStatus(StatusOrderViewModel order);
        Task<List<OrderDetailsViewModel>> GetOrderDetailsByOrderId(int id);
        Task<Order> GetOrdersById(int id);
        public Task<List<OrderViewModel>> GetSpecificOrders(int id);
        public Task<long> AddOrder(Order order);

        public  Task<int> AddOrderDetails(OrderDetails orderDetails);
    }
}
