using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class UsuarioModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Empleado")]
        public string idEmpleado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Perfil")]
        public string idPerfil { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombre de Usuario")]
        [StringLength(44)]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Contraseña")]
        [StringLength(44)]
        public string password { get; set; }
    }
}
