using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class PacienteModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombres")]
        [StringLength(149)]
        public string nombres { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Apellidos")]
        [StringLength(149)]
        public string apellidos { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Fecha de Nacimiento")]
        public DateTime fechaNacimiento { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Número de DPI")]
        [StringLength(19)]
        public string numeroDpi { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Teléfono")]
        [StringLength(9)]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Dirección de Residencia")]
        [StringLength(99)]
        public string direccionResidencia { get; set; }
    }
}
