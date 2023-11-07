using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class HabitacionModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Número")]
        public int numero { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Descripción")]
        [StringLength(149)]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Status")]
        public string status { get; set; }
    }
}
