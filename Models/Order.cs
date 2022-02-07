using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class Order
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Column("DateOfOrder")]
        [Required]
        public DateTime DateOfOrder { get; set; }

        [Column("DateOfDelivery")]
        public String DateOfDelivery { get; set; }

        [Column("Points")]
        [Required]
        public int Points { get; set; }

        
        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public virtual StatusDescription StatusDescription { get; set; }
    }
}
