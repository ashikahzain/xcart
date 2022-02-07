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
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("GradeId")]
        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey("JobTitleId")]
        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }

        
    }
}
