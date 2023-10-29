using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class ProductoModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombre")]
        [StringLength(149)]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Precio Costo")]
        [Range(0.01, 1000000.00)]
        public decimal precioCosto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Precio Venta")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Este dato debe ser de tipo numérico")]
        public decimal precioVenta { get; set; }
    }
}
