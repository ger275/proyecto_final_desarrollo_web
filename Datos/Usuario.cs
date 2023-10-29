using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Usuario:Conexion
    {
        public IEnumerable<UsuarioModel> Consultar()
        {
            Conectar();
            List<UsuarioModel> lista = new List<UsuarioModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select a.id, b.nombres as idEmpleado, c.descripcion as idPerfil, a.nombre, a.password from usuario as a join empleado as b on a.idEmpleado = b.id join perfil as c on c.id = a.idPerfil", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UsuarioModel model = new UsuarioModel()
                    {
                        Id = (int)reader[0],
                        idEmpleado = reader[1] + "",
                        idPerfil = reader[2] + "",
                        nombre = reader[3] + "",
                        password = reader[4] + ""
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

        public UsuarioModel Consultar(int id)
        {
            Conectar();
            UsuarioModel usuario = new UsuarioModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select a.id, b.nombres as idEmpleado, c.descripcion as idPerfil, a.nombre, a.password from usuario as a join empleado as b on a.idEmpleado = b.id join perfil as c on c.id = a.idPerfil where a.id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario = new UsuarioModel()
                    {
                        Id = (int)reader[0],
                        idEmpleado = reader[1] + "",
                        idPerfil = reader[2] + "",
                        nombre = reader[3] + "",
                        password = reader[4] + ""
                    };
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

            return usuario;
        }

        public void Crear(UsuarioModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into usuario(idEmpleado, idPerfil, nombre, password) " +
                    "values(" + modelo.idEmpleado + "" +
                    ", " + modelo.idPerfil + "" +
                    ", '" + modelo.nombre + "'" +
                    ", '" + modelo.password + "')", cn);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }
        }

        public void Editar(UsuarioModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update usuario set idEmpleado = " + modelo.idEmpleado + ", " +
                    "idPerfil = " + modelo.idPerfil + ", " +
                    "nombre = '" + modelo.nombre + "', " + 
                    "password = '" + modelo.password + "' where id = " + modelo.Id, cn);

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }
        }

        public void Eliminar(int id)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("delete from usuario where id = " + id, cn);

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {

                throw;
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
