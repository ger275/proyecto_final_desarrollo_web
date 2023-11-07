using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class ProductoClienteModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Paciente")]
        public string idPaciente { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string idProducto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Cantidad")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Fecha de Despacho")]
        public DateTime fecha { get; set; }
    }
}
