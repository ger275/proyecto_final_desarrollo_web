using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class PacienteHabitacion:Conexion
    {
        public IEnumerable<PacienteHabitacionModel> Consultar()
        {
            Conectar();
            List<PacienteHabitacionModel> lista = new List<PacienteHabitacionModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select ph.id, p.nombres, h.descripcion, ph.fechaHoraEntrada, ph.fechaHoraSalida from paciente_en_habitacion as ph join paciente as p on ph.idPaciente = p.id join habitacion as h on h.id = ph.idHabitacion", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PacienteHabitacionModel model = new PacienteHabitacionModel()
                    {
                        Id = (int)reader[0],
                        idPaciente = reader[1] + "",
                        idHabitacion = reader[2] + "",
                        fechaHoraEntrada = (DateTime)reader[3],
                        fechaHoraSalida = (DateTime)reader[4]
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

        public PacienteHabitacionModel Consultar(int id)
        {
            Conectar();
            PacienteHabitacionModel prod = new PacienteHabitacionModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select ph.id, p.nombres, h.descripcion, ph.fechaHoraEntrada, ph.fechaHoraSalida from paciente_en_habitacion as ph join paciente as p on ph.idPaciente = p.id join habitacion as h on h.id = ph.idHabitacion where ph.id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    prod = new PacienteHabitacionModel()
                    {
                        Id = (int)reader[0],
                        idPaciente = reader[1] + "",
                        idHabitacion = reader[2] + "",
                        fechaHoraEntrada = (DateTime)reader[3],
                        fechaHoraSalida = (DateTime)reader[4]
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

            return prod;
        }

        public void Crear(PacienteHabitacionModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into paciente_en_habitacion(idPaciente,idHabitacion,fechaHoraEntrada,fechaHoraSalida) " +
                    "values(" + modelo.idPaciente + "" +
                    ", " + modelo.idHabitacion + "" +
                    ", '" + modelo.fechaHoraEntrada.ToString("yyyy-MM-dd") + "'" +
                    ", '" + modelo.fechaHoraSalida.ToString("yyyy-MM-dd") + "')", cn);

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

        public void Editar(PacienteHabitacionModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update paciente_en_habitacion set idPaciente = " + modelo.idPaciente + ", " +
                    "idHabitacion = " + modelo.idHabitacion + ", " +
                    "fechaHoraEntrada = '" + modelo.fechaHoraEntrada.ToString("yyyy-MM-dd") + "', " +
                    "fechaHoraSalida = '" + modelo.fechaHoraSalida.ToString("yyyy-MM-dd") + "' where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from paciente_en_habitacion where id = " + id, cn);

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
