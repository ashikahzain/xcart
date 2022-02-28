using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Points { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
    }
}
