using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class PuestoModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Descripción")]
        [StringLength(99)]
        public string descripcion { get; set; }
    }
}
