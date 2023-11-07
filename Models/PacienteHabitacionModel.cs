using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class PacienteHabitacionModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Paciente")]
        public string idPaciente { get; set; }

        [DisplayName("Habitación")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string idHabitacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Fecha de Entrada")]
        public DateTime fechaHoraEntrada { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Fecha de Salida")]
        public DateTime fechaHoraSalida { get; set; }
    }
}
