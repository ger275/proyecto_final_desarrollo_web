using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Permiso:Conexion
    {
        public IEnumerable<PermisoModel> Consultar()
        {
            Conectar();
            List<PermisoModel> lista = new List<PermisoModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from permiso", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PermisoModel model = new PermisoModel()
                    {
                        Id = (int)reader[0],
                        menu = reader[1] + "",
                        controlador = reader[2] + "",
                        accion = reader[3] + "",
                        titulo = reader[4] + ""
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

        public PermisoModel Consultar(int id)
        {
            Conectar();
            PermisoModel permiso = new PermisoModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from permiso where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    permiso = new PermisoModel()
                    {
                        Id = (int)reader[0],
                        menu = reader[1] + "",
                        controlador = reader[2] + "",
                        accion = reader[3] + "",
                        titulo = reader[4] + ""
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

            return permiso;
        }

        public void Crear(PermisoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into permiso(menu,controlador,accion,titulo) " +
                    "values('" + modelo.menu + "'" +
                    ", '" + modelo.controlador + "'" +
                    ", '" + modelo.accion + "'" +
                    ", '" + modelo.titulo + "')", cn);

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

        public void Editar(PermisoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update permiso set menu = '" + modelo.menu + "', " +
                    "controlador = '" + modelo.controlador + "', " +
                    "accion = '" + modelo.accion + "', " +
                    "titulo = '" + modelo.titulo + "' where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from permiso where id = " + id, cn);

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
