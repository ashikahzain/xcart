using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class UserRole
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        /*
         * 
         * [ForeignKey("UserId")]
        public virtual User UserId { get; set; }
         * 
         */

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        
    }
}
