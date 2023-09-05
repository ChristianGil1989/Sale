using System.ComponentModel.DataAnnotations;

namespace Sales.API.Models.DTOs
{
    public class CategoryDto
    {
        [Display(Name = "Categoría")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

    }
}
