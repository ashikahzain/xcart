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
    }
}
