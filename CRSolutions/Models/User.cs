using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRSolutions.Models
{
    public class User
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdUser { get; set; }

        [Required]
        [Display(Name = "Nombre Completo")]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Password { get; set; }
        
        public bool Status { get; set; }        
        public Guid IdRol { get; set; }        
        public Guid IdCompany { get; set; }

        public Role Role { get; set; } = null!;
        public Company Company { get; set; } = null!;

        public ICollection<Candidate> Candidates { get; } = new List<Candidate>(); // Collection navigation containing dependents

    }
}
