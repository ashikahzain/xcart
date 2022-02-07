using System;
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
        Task ChangeStatus(Order order);
        Task<List<OrderDetailsViewModel>> GetOrderDetailsByOrderId(int id);
    }
}
