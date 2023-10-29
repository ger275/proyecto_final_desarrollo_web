using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Perfil:Conexion
    {
        public IEnumerable<PerfilModelo> Consultar()
        {
            Conectar();
            List<PerfilModelo> lista = new List<PerfilModelo>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from perfil", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PerfilModelo model = new PerfilModelo()
                    {
                        Id = (int)reader[0],
                        descripcion = reader[1] + ""
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

        public PerfilModelo Consultar(int id)
        {
            Conectar();
            PerfilModelo puesto = new PerfilModelo();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from perfil where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    puesto = new PerfilModelo()
                    {
                        Id = (int)reader[0],
                        descripcion = reader[1] + ""
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

            return puesto;
        }

        public void Crear(PerfilModelo modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into perfil(descripcion) " +
                    "values('" + modelo.descripcion + "')", cn);

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

        public void Editar(PerfilModelo modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update perfil set descripcion = '" + modelo.descripcion + "' " +
                    " where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from perfil where id = " + id, cn);

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
