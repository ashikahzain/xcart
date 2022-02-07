using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class Grade
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Int16 Id { get; set; }

        [Column("Type")]
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Column("Description")]
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
