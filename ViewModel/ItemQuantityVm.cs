using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.ViewModel
{
    public class ItemQuantityVm
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public void MaptoModel(Item item)
        {
            item.Quantity = Quantity;
        }
    }
}
