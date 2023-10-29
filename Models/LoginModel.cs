using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string pass { get; set; }

        public string[] roles { get; set; }
    }
}
