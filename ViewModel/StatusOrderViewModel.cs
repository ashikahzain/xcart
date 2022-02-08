using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.ViewModel
{
    public class StatusOrderViewModel
    {
        public int Id { get; set; }
        public int StatusDescriptionId { get; set; }
        public string DateOfDelivery { get; set; }

        public void MaptoModel(Order order)
        {
            order.DateOfDelivery = DateOfDelivery;
            order.StatusDescriptionId = StatusDescriptionId;
        }

    }
}
