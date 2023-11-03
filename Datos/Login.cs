using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Login : Conexion
    {
        public List<LoginModel> ListaUsuarios(string usuario, string pass)
        {
            Conectar();
            List<LoginModel> lista = new List<LoginModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select nombre, password, idPerfil from usuario where nombre = '" + usuario + "' and password = '" + pass + "'", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LoginModel model = new LoginModel()
                    {
                        usuario = reader[0] + "",
                        pass = reader[1] + "",
                        roles = new string[] { (reader[2] + "") },
                    };

                    lista.Add(model);
                }
            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }



            return lista;
        }

        public LoginModel ValidarUsuario(string usuario, string pass)
        {
            return ListaUsuarios(usuario, pass).Where(item => item.usuario == usuario && item.pass == pass).FirstOrDefault();
        }

        public int ValidarMenu(string menu, string usuario)
        {
            int resultado = 0;

            Conectar();
            List<LoginModel> lista = new List<LoginModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from usuario as a join perfil as b on a.idPerfil = b.id join permiso_por_perfil as c on b.id = c.idPerfil join permiso as d on c.idPermiso = d.id where a.nombre = '" + usuario + "' and d.menu = '" + menu + "' limit 1 ", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultado = 1;
                }
            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }

            return resultado;
        }

        public int ValidarAccion(string controlador, string accion, string usuario)
        {
            int resultado = 0;

            Conectar();
            List<LoginModel> lista = new List<LoginModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select d.controlador, d.accion, d.titulo from usuario as a join perfil as b on a.idPerfil = b.id join permiso_por_perfil as c on b.id = c.idPerfil join permiso as d on c.idPermiso = d.id where a.nombre = '" + usuario + "' and d.controlador = '" + controlador + "' and d.accion = '" + accion + "' limit 1", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultado = 1;
                }
            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }

            return resultado;
        }

        public IEnumerable<MenuItemModel> GetItemsMenu(string menu, string usuario)
        {
            Conectar();

            List<MenuItemModel> lista = new List<MenuItemModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select d.controlador, d.accion, d.titulo from usuario as a join perfil as b on a.idPerfil = b.id join permiso_por_perfil as c on b.id = c.idPerfil join permiso as d on c.idPermiso = d.id where a.nombre = '" + usuario + "' and d.menu = '" + menu + "' and d.accion = 'Index' ", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MenuItemModel model = new MenuItemModel()
                    {
                        controlador = reader[0] + "",
                        accion = reader[1] + "",
                        titulo = reader[2] + ""
                    };

                    lista.Add(model);
                }
            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }

            return lista;
        }

    }
}
