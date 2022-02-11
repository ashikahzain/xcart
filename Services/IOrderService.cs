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
        public Task<List<Item>> GetTrendingItems();
        public Task ChangeStatus(StatusOrderViewModel order);
        public Task<List<OrderDetailsViewModel>> GetOrderDetailsByOrderId(int id);
        public Task<List<OrderViewModel>> GetSpecificOrders(int id);
        public Task<long> AddOrder(Order order);
        public Task<long> AddOrderdetails(OrderDetails orderDetails);
        public Task<int> AddOrderDetails(OrderDetails orderDetails);
    }
}
