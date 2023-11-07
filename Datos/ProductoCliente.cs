using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class ProductoCliente:Conexion
    {
        public IEnumerable<ProductoClienteModel> Consultar()
        {
            Conectar();
            List<ProductoClienteModel> lista = new List<ProductoClienteModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select pp.id, p.nombres, prod.nombre, pp.cantidad, pp.fecha from producto_por_cliente as pp join paciente as p on p.id = pp.idPaciente join producto as prod on prod.id = pp.idProducto", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoClienteModel model = new ProductoClienteModel()
                    {
                        Id = (int)reader[0],
                        idPaciente = reader[1] + "",
                        idProducto = reader[2] + "",
                        cantidad = (int)reader[3],
                        fecha = (DateTime)reader[4]
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

        public ProductoClienteModel Consultar(int id)
        {
            Conectar();
            ProductoClienteModel prod = new ProductoClienteModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select pp.id, p.nombres, prod.nombre, pp.cantidad, pp.fecha from producto_por_cliente as pp join paciente as p on p.id = pp.idPaciente join producto as prod on prod.id = pp.idProducto where pp.id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    prod = new ProductoClienteModel()
                    {
                        Id = (int)reader[0],
                        idPaciente = reader[1] + "",
                        idProducto = reader[2] + "",
                        cantidad = (int)reader[3],
                        fecha = (DateTime)reader[4]
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

        public void Crear(ProductoClienteModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into producto_por_cliente(idPaciente,idProducto,cantidad,fecha) " +
                    "values(" + modelo.idPaciente + "" +
                    ", " + modelo.idProducto + "" +
                    ", " + modelo.cantidad + "" +
                    ", '" + modelo.fecha.ToString("yyyy-MM-dd") + "')", cn);

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

        public void Editar(ProductoClienteModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update producto_por_cliente set idPaciente = " + modelo.idPaciente + ", " +
                    "idProducto = " + modelo.idProducto + ", " +
                    "cantidad = " + modelo.cantidad + ", " +
                    "fecha = '" + modelo.fecha.ToString("yyyy-MM-dd") + "' where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from producto_por_cliente where id = " + id, cn);

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
