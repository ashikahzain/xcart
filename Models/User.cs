using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class User
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

        [Column("Gender")]
        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

        [Column("Email")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("Image")]
        [Required]
        [StringLength(150)]
        public string Image { get; set; }

        [Column("Contact")]
        [Required]
        [StringLength(30)]
        public string Contact { get; set; }

        [Column("UserName")]
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [ForeignKey("LocationId")]
        public Location LocationId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department DepartmentId { get; set; }

        [ForeignKey("GradeId")]
        public Grade GradeId { get; set; }

        [ForeignKey("JobTitleId")]
        public JobTitle JobTitleId { get; set; }

        
    }
}
