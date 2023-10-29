using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Puesto:Conexion
    {
        public IEnumerable<PuestoModel> Consultar()
        {
            Conectar();
            List<PuestoModel> lista = new List<PuestoModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from puesto", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PuestoModel model = new PuestoModel()
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

        public PuestoModel Consultar(int id)
        {
            Conectar();
            PuestoModel puesto = new PuestoModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from puesto where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    puesto = new PuestoModel()
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

        public void Crear(PuestoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into puesto(descripcion) " +
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

        public void Editar(PuestoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update puesto set descripcion = '" + modelo.descripcion + "' " +
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
                MySqlCommand cmd = new MySqlCommand("delete from puesto where id = " + id, cn);

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
