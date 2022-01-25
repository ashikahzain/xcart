using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class Point
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        [ForeignKey("UserId")]
        public virtual User UserId { get; set; }

        [Column("TotalPoints")]
        public long TotalPoints { get; set; }

        [Column("CurrentPoints")]
        public long CurrentPoints { get; set; }
    }
}
