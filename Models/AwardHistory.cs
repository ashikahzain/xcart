﻿using System;
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


        [ForeignKey("UserId")]
        public virtual User UserId { get; set; }

        [ForeignKey("AwardId")]
        public virtual Award AwardId { get; set; }

        [ForeignKey("PresenteeId")]
        public virtual User PresenteeId  { get; set; }

        [Column("Point")]
        [Required]
        public int Point { get; set; }
 
    }
}
