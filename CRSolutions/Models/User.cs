﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Password { get; set; }        
        public bool Status { get; set; }        
        public Guid IdRol { get; set; }        
        public Guid IdCompany { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; } = null!;
        [JsonIgnore]
        public virtual Company Company { get; set; } = null!;

        public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>(); // Collection navigation containing dependents

    }
}
