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

        public PermisoPerfilModel ConsultarPerfil(int id)
        {
            Conectar();
            PermisoPerfilModel permiso = new PermisoPerfilModel();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from perfil where id = " + id, cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    permiso = new PermisoPerfilModel()
                    {
                        Id = (int)reader[0],
                        perfil = reader[1] + "",
                        idPerfil = reader[0] + "",
                        idPermiso = ""
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

        public void CrearPermisoPerfil(PermisoPerfilModel modelo)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into permiso_por_perfil(idPerfil, idPermiso) " +
                    "values('" + modelo.idPerfil + "', '" + modelo.idPermiso + "')", cn);

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

        public IEnumerable<PermisoModel> ConsultarPermisosPerfil(int idPerfil)
        {
            Conectar();
            List<PermisoModel> lista = new List<PermisoModel>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("select pp.id, p.menu, p.controlador, p.accion, p.titulo " +
                    "from permiso_por_perfil as pp join permiso as p on pp.idPermiso = p.id and pp.idPerfil = " + idPerfil + " order by p.controlador asc, p.accion asc", cn);
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

        public void EliminarPermisoPerfil(int id)
        {
            Conectar();

            try
            {
                MySqlCommand cmd = new MySqlCommand("delete from permiso_por_perfil where id = " + id, cn);

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
