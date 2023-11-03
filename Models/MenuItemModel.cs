using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoFinalDesarrolloWeb.Models
{
    public class MenuItemModel
    {
        public string controlador { get; set; }
        public string accion { get; set; }
        public string titulo { get; set; }
    }
}
