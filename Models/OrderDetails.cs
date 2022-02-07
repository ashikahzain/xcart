using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class OrderDetails
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        
        [ForeignKey("OrderId")]
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        
	
        [Column("Quantity")]
        [Required]
        public int Quantity { get; set; }

    }
}
