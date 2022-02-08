using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
     public interface IItemService
    {
        public Task<List<Item>> GetAllItems();
        public Task<int> AddItem(ItemViewModel item);
        public Task<Item> GetItemById(int id);
        public Task<int> EditItemQuantity(ItemQuantityVm item);
        public Task<int> DeleteItem(int id);
        public Task<int> UpdateItem(ItemViewModel item);

    }
}
