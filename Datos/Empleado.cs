using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Empleado:Conexion
    {
        public IEnumerable<EmpleadoModel> Consultar()
        {
            Conectar();
            List<EmpleadoModel> lista = new List<EmpleadoModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select e.id, p.descripcion as idPuesto, e.nombres, e.apellidos, e.fechaNacimiento, e.numeroDpi, e.salario, e.telefono, e.direccionResidencia from empleado as e join puesto as p on e.idPuesto = p.id", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmpleadoModel model = new EmpleadoModel()
                    {
                        Id = (int)reader[0],
                        idPuesto = reader[1] + "",
                        nombres = reader[2] + "",
                        apellidos = reader[3] + "",
                        fechaNacimiento = (DateTime)reader[4],
                        numeroDpi = reader[5] + "",
                        salario = (decimal)reader[6],
                        telefono = reader[7] + "",
                        direccionResidencia = reader[8] + ""
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

        public EmpleadoModel Consultar(int id)
        {
            Conectar();
            EmpleadoModel empleado = new EmpleadoModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select e.id, p.descripcion as idPuesto, e.nombres, e.apellidos, e.fechaNacimiento, e.numeroDpi, e.salario, e.telefono, e.direccionResidencia from empleado as e join puesto as p on e.idPuesto = p.id where e.id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empleado = new EmpleadoModel()
                    {
                        Id = (int)reader[0],
                        idPuesto = reader[1] + "",
                        nombres = reader[2] + "",
                        apellidos = reader[3] + "",
                        fechaNacimiento = (DateTime)reader[4],
                        numeroDpi = reader[5] + "",
                        salario = (decimal)reader[6],
                        telefono = reader[7] + "",
                        direccionResidencia = reader[8] + ""
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

            return empleado;
        }

        public void Crear(EmpleadoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into empleado(idPuesto, nombres, apellidos, fechaNacimiento, numeroDpi, salario, telefono, direccionResidencia) " +
                    "values(" + modelo.idPuesto + "" +
                    ", '" + modelo.nombres + "'" +
                    ", '" + modelo.apellidos + "'" +
                    ", '" + modelo.fechaNacimiento.ToString("yyyy-MM-dd") + "'" +
                    ", '" + modelo.numeroDpi + "'" + 
                    ", '" + modelo.salario + "'" +
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

        public void Editar(EmpleadoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update empleado set idPuesto = " + modelo.idPuesto + ", " +
                    "nombres = '" + modelo.nombres + "', " +
                    "apellidos = '" + modelo.apellidos + "', " +
                    "fechaNacimiento = '" + modelo.fechaNacimiento.ToString("yyyy-MM-dd") + "', " + 
                    "numeroDpi = '" + modelo.numeroDpi + "', " +
                    "salario = " + modelo.salario + ", " +
                    "telefono = '" + modelo.telefono + "', " +
                    "direccionResidencia = '" + modelo.direccionResidencia + "' " +
                    "where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from empleado where id = " + id, cn);

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
