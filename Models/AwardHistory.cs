using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public partial class AwardHistory
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
        public long EmployeeId { get; set; }
        public virtual User Employee { get; set; }

        [ForeignKey("AwardId")]
        public int AwardId { get; set; }
        public virtual Award Award { get; set; }

        [ForeignKey("PresenteeId")]
        public long? PresenteeId { get; set; }
        public virtual User Presentee  { get; set; }

        [Column("Point")]
        [Required]
        public int Point { get; set; }

        [Column("Status")]
        public bool Status { get;set;}
 
    }
}
