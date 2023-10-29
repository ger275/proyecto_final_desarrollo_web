using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Producto:Conexion
    {
        public IEnumerable<ProductoModel> Consultar()
        {
            Conectar();
            List<ProductoModel> lista = new List<ProductoModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from producto", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoModel model = new ProductoModel()
                    {
                        Id = (int)reader[0],
                        nombre = (string)reader[1],
                        precioCosto = (decimal)reader[2],
                        precioVenta = (decimal)reader[3]
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

        public ProductoModel Consultar(int id)
        {
            Conectar();
            ProductoModel producto = new ProductoModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from producto where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    producto = new ProductoModel()
                    {
                        Id = (int)reader[0],
                        nombre = (string)reader[1],
                        precioCosto = (decimal)reader[2],
                        precioVenta = (decimal)reader[3]
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

            return producto;
        }

        public void Crear(ProductoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into producto(nombre,precioCosto,precioVenta) " +
                    "values('" + modelo.nombre + "'" +
                    ", " + modelo.precioCosto + "" +
                    ", " + modelo.precioVenta + ")", cn);

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

        public void Editar(ProductoModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update producto set nombre = '" + modelo.nombre + "', " +
                    "precioCosto = " + modelo.precioCosto + ", " +
                    "precioVenta = " + modelo.precioVenta + " where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from producto where id = " + id, cn);

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
