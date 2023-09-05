using System.ComponentModel.DataAnnotations;

namespace Sales.API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public DateTime CreationDate { get; set; }
    }
}
