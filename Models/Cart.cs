using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class Cart
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        /*
        [ForeignKey("ItemId")]
        public virtual Item ItemId { get; set; }
        */

        [Column("Quantity")]
        [Required]
        public int Quantity { get; set; }
	
        [Column("UsedId")]
        [Required]
        public long UsedId { get; set; }

    }
}
