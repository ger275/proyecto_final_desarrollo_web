using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Data
{
    public class DA_Logica
    {
        public List<LoginModel> ListaUsuarios()
        {
            return new List<LoginModel>
            {
                new LoginModel { usuario = "admin", pass = "admin", roles = new string[]{ "Administrador" } },
                new LoginModel { usuario = "pruebas", pass = "pruebas", roles = new string[]{ "Administrador" } }
            };
        }

        public LoginModel ValidarUsuario(string usuario, string pass)
        {
            return ListaUsuarios().Where(item => item.usuario == usuario && item.pass == pass).FirstOrDefault();
        }
    }
}
