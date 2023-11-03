using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class PermisoModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Menú")]
        [StringLength(44)]
        public string menu { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Controlador")]
        [StringLength(44)]
        public string controlador { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Acción")]
        [StringLength(44)]
        public string accion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Título")]
        [StringLength(44)]
        public string titulo { get; set; }
    }
}
