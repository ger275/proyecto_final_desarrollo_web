using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class PermisoPerfilModel
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Perfil")]
        public string idPerfil { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Perfil")]
        public string perfil { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Permiso")]
        public string idPermiso { get; set; }
    }
}
