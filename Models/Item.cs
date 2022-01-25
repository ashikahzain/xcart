﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class Item
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("Quantity")]
        [Required]
        public int Quantity { get; set; }

        [Column("Points")]
        [Required]
        public int Points { get; set; }

        [Column("Image")]
        [Required]
        [StringLength(150)]
        public string Image { get; set; }

        [Column("IsActive")]
        [Required]
        public bool? IsActive { get; set; }

    }
}
