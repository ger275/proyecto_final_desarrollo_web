using MySql.Data.MySqlClient;

namespace ProyectoFinalDesarrolloWeb.Datos
{
    public class Conexion
    {
        public MySqlConnection cn;

        public void Conectar()
        {
            try
            {
                //cn = new MySqlConnection("Server=192.168.0.20; Port=3306; Database=proyecto_final_desarrollo_web; Uid=root; Pwd=root;");
                cn = new MySqlConnection("server=proyectofinaldesarrolloweb.mysql.database.azure.com;uid=adminserver;pwd=ad_2023a;database=proyecto_final_desarrollo_web");
                cn.Open();
            }
            catch (MySqlException e)
            {

                throw;
            }
        }

        public void Desconectar()
        {
            try
            {
                cn.Close();
            }
            catch (MySqlException e)
            {

                throw;
            }
        }
    }
}
