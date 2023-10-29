using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class LoteModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Producto")]
        public string idProducto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Número de Lote")]
        [StringLength(44)]
        public string numeroLote { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Fecha de Vencimiento")]
        public DateTime fechaVence { get; set; }
    }
}
