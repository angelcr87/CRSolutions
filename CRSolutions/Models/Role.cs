using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRSolutions.Models
{
    public class Role
    {
        [Key]
        public Guid IdRol { get; set; }
        [Required]
        [Display(Name = "Nombre Rol")]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string RoleName { get; set; }
        [Display(Name = "Descripción")]
        [Column(TypeName = "VARCHAR(500)")]
        [StringLength(500)]
        public string Description { get; set; }
        public bool Status { get; set; }
        public  ICollection<User> Users { get; } = new List<User>(); // Collection navigation containing dependents
    }
}
