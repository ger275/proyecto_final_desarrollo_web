using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class EmpleadoModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Puesto")]
        public string idPuesto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombres")]
        [StringLength(199)]
        public string nombres { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Apellidos")]
        [StringLength(199)]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Fecha de Nacimiento")]
        public DateTime fechaNacimiento { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Número de DPI")]
        [StringLength(19)]
        public string numeroDpi { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Salario")]
        [Range(0.01, 1000000.00)]
        public decimal salario { get; set; }

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
