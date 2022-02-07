using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string DateOfDelivery { get; set; }
        public string Status { get; set; }

    }
}
