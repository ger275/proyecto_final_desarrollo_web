using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Habitacion:Conexion
    {
        public IEnumerable<HabitacionModel> Consultar()
        {
            Conectar();
            List<HabitacionModel> lista = new List<HabitacionModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select id, numero, descripcion, case status when 1 then 'DISPONIBLE' when 2 then 'OCUPADA' else '' end status from habitacion", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    HabitacionModel model = new HabitacionModel()
                    {
                        Id = (int)reader[0],
                        numero = (int)reader[1],
                        descripcion = (string)reader[2],
                        status = (string)reader[3]
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

        public HabitacionModel Consultar(int id)
        {
            Conectar();
            HabitacionModel habitacion = new HabitacionModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from habitacion where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    habitacion = new HabitacionModel()
                    {
                        Id = (int)reader[0],
                        numero = (int)reader[1],
                        descripcion = (string)reader[2],
                        status = reader[3] + ""
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

            return habitacion;
        }

        public void Crear(HabitacionModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into habitacion(numero,descripcion,status) " +
                    "values(" + modelo.numero + "" +
                    ", '" + modelo.descripcion + "'" +
                    ", " + modelo.status + ")", cn);

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

        public void Editar(HabitacionModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update habitacion set numero = " + modelo.numero + ", " +
                    "descripcion = '" + modelo.descripcion + "', " +
                    "status = " + modelo.status + " where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from habitacion where id = " + id, cn);

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
