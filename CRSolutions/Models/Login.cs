using System.ComponentModel.DataAnnotations;

namespace CRSolutions.Models
{
    public class Login
    {
        [Required(ErrorMessage = "UserName is required")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Compare("Password")]
        public string Password { get; set; }
    }
}
