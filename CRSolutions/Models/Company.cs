using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRSolutions.Models
{
    public class Company
    {
        [Key]
        public Guid IdCompany { get; set; }

        [Required]
        [Display(Name = "Nombre Empresa")]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string CompanyName { get; set;}

        [Display(Name = "Descripción")]
        [Column(TypeName = "VARCHAR(500)")]
        [StringLength(500)]
        public string CompanyDescription { get; set;} = string.Empty;

        [Display(Name ="Razon Social")]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string BusinessName { get; set;}

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string RFC { get; set; }

        public bool Credits { get; set;}

        public bool Status { get; set;}

        public virtual ICollection<User> Users { get; } = new List<User>(); // Collection navigation containing dependents

        public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();
    }
}
