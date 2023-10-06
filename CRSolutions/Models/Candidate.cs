using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRSolutions.Models
{
    public class Candidate
    {
        [Key]
        public Guid IdCantidate { get; set; }

        [Required]
        [Display(Name = "Nombre Completo")]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string? FullName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(18)]
        public string? CURP { get; set; }

        //[Required]
        //[Display(Name = "Empresa")]
        //public Guid IdCompany { get; set; }

        //[Required]
        //[Display(Name = "Solicitante")]
        //public Guid? IdUser { get; set; }

        [Required]
        [Display(Name = "Posición Evaluada")]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string? EvaluatedPosition { get; set; }
        [Required]
        [Display(Name = "Score de Riesgo")]
        public int IdRiskScore { get; set; }
        [Required]
        [Display(Name = "Fecha Evaluacion")]
        public DateTime EvaluationDate { get; set; }

        
        [Display(Name = "Reporte")]
        //[Column(TypeName = "VARCHAR(600)")]
        //[StringLength(600)]
        public byte[]? ReportFile { get; set; }

       
        [Display(Name = "Audio")]
        //[Column(TypeName = "VARCHAR(600)")]
        //[StringLength(600)]
        public byte[]? AudioFile { get; set; }

        
        [Display(Name = "Creditos")]
        //[Column(TypeName = "VARCHAR(600)")]
        //[StringLength(600)]
        public byte[]? CreditFile { get; set; }

        [Required]
        [Display(Name = "Tipo Test")]
        public int IdTypeTest { get; set; }

        [Display(Name = "Hist. de Eval.")]
        [Column(TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string? RecordEvaluation { get; set; }
        public string? BlackList { get; set; }

        public bool Status { get; set; }

        [Display(Name = "Solicitante")]
        public Guid? IdUser { get; set; }

        [Display(Name = "Empresa")]
        public Guid IdCompany { get; set; }

        public byte[]? Photo { get; set; }

        [NotMapped]
        public string? antiquity { get; set; }
        [NotMapped]
        public IFormFile? routeReportFile { get; set; }
        [NotMapped]
        public IActionResult? routeAudioFile { get; set; }
        [NotMapped]
        public IActionResult? routeCreditFile { get; set; }

        public virtual User? User { get; set; } = null!;
        public virtual Company? Company { get; set; } = null!;  


    }
}
