using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class AwardHistory
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        [Column("Date")]
        [Required]
        public DateTime Date { get; set; }


        [ForeignKey("EmployeeId")]
        public virtual User Employee { get; set; }

        [ForeignKey("AwardId")]
        public virtual Award Award { get; set; }

        [ForeignKey("PresenteeId")]
        public virtual User Presentee  { get; set; }

        [Column("Point")]
        [Required]
        public int Point { get; set; }

        [Column("Status")]
        public bool Status { get;set;}
 
    }
}
