using System.ComponentModel.DataAnnotations;

namespace Sales.API.Models.DTOs
{
    public class LoginUserDto
    {
        [Display(Name = "User Name")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserName { get; set; }

        [Display(Name = "Contaseña")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Password { get; set; }
    }
}
