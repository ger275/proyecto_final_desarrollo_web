using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Paciente:Conexion
    {
        public IEnumerable<PacienteModel> Consultar()
        {
			Conectar();
			List<PacienteModel>lista = new List<PacienteModel> ();

			try
			{
				MySqlCommand cmd = new MySqlCommand("select * from paciente", cn);
				cmd.CommandType = System.Data.CommandType.Text;
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					PacienteModel model = new PacienteModel()
					{
						Id = (int)reader[0],
						nombres = reader[1] + "",
						apellidos = reader[2] + "",
						fechaNacimiento = (DateTime)reader[3],
						numeroDpi = reader[4] + "",
						telefono = reader[5] + "",
						direccionResidencia = reader[6] + "",
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

        public PacienteModel Consultar(int id)
        {
            Conectar();
            PacienteModel paciente = new PacienteModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from paciente where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    paciente = new PacienteModel()
                    {
                        Id = (int)reader[0],
                        nombres = reader[1] + "",
                        apellidos = reader[2] + "",
                        fechaNacimiento = (DateTime)reader[3],
                        numeroDpi = reader[4] + "",
                        telefono = reader[5] + "",
                        direccionResidencia = reader[6] + "",
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

            return paciente;
        }

        public void Crear(PacienteModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into paciente(nombres,apellidos,fechaNacimiento,numeroDpi,telefono,direccionResidencia) " +
                    "values('" + modelo.nombres + "'" +
                    ", '" + modelo.apellidos + "'" +
                    ", '" + modelo.fechaNacimiento.ToString("yyyy-MM-dd") + "'" +
                    ", '" + modelo.numeroDpi + "'" +
                    ", '" + modelo.telefono + "'" +
                    ", '" + modelo.direccionResidencia + "')", cn);

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

        public void Editar(PacienteModel modelo)
        {
            Conectar();
            PacienteModel paciente = new PacienteModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update paciente set nombres = '" + modelo.nombres + "', " +
                    "apellidos = '" + modelo.apellidos + "', " +
                    "fechaNacimiento = '" + modelo.fechaNacimiento.ToString("yyyy-MM-dd") + "', " +
                    "numeroDpi = '" + modelo.numeroDpi + "', " +
                    "telefono = '" + modelo.telefono + "', " +
                    "direccionResidencia = '" + modelo.telefono + "' where id = " + modelo.Id, cn);

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
            PacienteModel paciente = new PacienteModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("delete from paciente where id = " + id, cn);

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
