using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Lote:Conexion
    {
        public IEnumerable<LoteModel> Consultar()
        {
            Conectar();
            List<LoteModel> lista = new List<LoteModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select l.id, p.nombre as idProducto, l.numeroLote, l.fechaVence from lote as l join producto as p on l.idProducto = p.id", cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LoteModel model = new LoteModel()
                    {
                        Id = (int)reader[0],
                        idProducto = reader[1] + "",
                        numeroLote = reader[2] + "",
                        fechaVence = (DateTime)reader[3]
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

        public LoteModel Consultar(int id)
        {
            Conectar();
            LoteModel lote = new LoteModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select l.id, p.nombre as idProducto, l.numeroLote, l.fechaVence from lote as l join producto as p on l.idProducto = p.id where l.id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lote = new LoteModel()
                    {
                        Id = (int)reader[0],
                        idProducto = reader[1] + "",
                        numeroLote = reader[2] + "",
                        fechaVence = (DateTime)reader[3]
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

            return lote;
        }

        public void Crear(LoteModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into lote(idProducto, numeroLote, fechaVence) " +
                    "values(" + modelo.idProducto + "" +
                    ", '" + modelo.numeroLote + "'" +
                    ", '" + modelo.fechaVence.ToString("yyyy-MM-dd") + "')", cn);

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

        public void Editar(LoteModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("update lote set idProducto = " + modelo.idProducto + ", " +
                    "numeroLote = '" + modelo.numeroLote + "', " +
                    "fechaVence = '" + modelo.fechaVence.ToString("yyyy-MM-dd") + "' where id = " + modelo.Id, cn);

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
                MySqlCommand cmd = new MySqlCommand("delete from lote where id = " + id, cn);

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
