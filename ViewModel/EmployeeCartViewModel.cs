using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class EmployeeCartViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public byte[] ItemImage { get; set; }
        public int Quantity { get; set; }
        public int ItemPoints { get; set; }
        public bool? IsActive { get; set; }
    }
}
