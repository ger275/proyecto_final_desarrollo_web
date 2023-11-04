using Microsoft.AspNetCore.Mvc;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        Perfil p = new Perfil();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Perfil", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<PerfilModelo> lista = p.Consultar();

            const int tamanioPagina = 10;
            if (pag < 1)
            {
                pag = 1;
            }

            int cantidadRegistros = lista.Count();

            var vista = new VistaModel(cantidadRegistros, pag, tamanioPagina);

            int saltoRegistros = (pag - 1) * tamanioPagina;

            var nuevaLista = lista.Skip(saltoRegistros).Take(vista.TamanioPagina).ToList();

            this.ViewBag.Vista = vista;

            return View(nuevaLista);
        }

        [HttpPost]
        public IActionResult Index(string buscar = "")
        {
            if (log.ValidarAccion("Perfil", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<PerfilModelo> lista = p.Consultar();
            IEnumerable<PerfilModelo> listaResultado;

            var resultado = (from perfil in lista
                                 //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                             where (buscar == "" || perfil.descripcion.ToLower().Contains(buscar))
                             select new PerfilModelo
                             {
                                 Id = perfil.Id,
                                 descripcion = perfil.descripcion
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Perfil", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(PerfilModelo modelo)
        {
            if (log.ValidarAccion("Perfil", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el perfil correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Perfil", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PerfilModelo modelo)
        {
            if (log.ValidarAccion("Perfil", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el perfil correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Perfil", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PerfilModelo modelo)
        {
            if (log.ValidarAccion("Perfil", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el perfil correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult PermisosPerfil(int id)
        {
            if (log.ValidarAccion("Perfil", "PermisosPerfil", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<PermisoModel> lista = p.ConsultarPermisosPerfil(id);
            ViewBag.permisosPerfil = lista;
            ViewBag.Permisos = ListaPermisos(id);

            return View(p.ConsultarPerfil(id));
        }

        [HttpPost]
        public ActionResult PermisosPerfil(PermisoPerfilModel modelo)
        {
            if (log.ValidarAccion("Perfil", "PermisosPerfil", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.CrearPermisoPerfil(modelo);

            //return RedirectToAction("PermisosPerfil", "Perfil", modelo.idPerfil);
            return RedirectToAction("PermisosPerfil", new { id = modelo.idPerfil });
        }

        public ActionResult EliminarPermisoPerfil(int id)
        {
            int idPerfil = 0;

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select idPerfil from permiso_por_perfil where id = " + id, cn.cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    idPerfil = (int)reader[0];
                }
            }
            catch (MySqlException e)
            {
                throw;
            }
            int dd = idPerfil;
            p.EliminarPermisoPerfil(id);

            return RedirectToAction("PermisosPerfil", new { id = 3 });
        }

        private List<SelectListItem> ListaPermisos(int idPerfil)
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select p.id, concat('CONTROLADOR: ', p.controlador, ' - ACCION: ', p.titulo) " +
                    "from permiso as p " +
                    "where not exists (select * from permiso_por_perfil as pp where p.id = pp.idPermiso and pp.idPerfil = '" + idPerfil + "') " +
                    "order by p.controlador asc, p.accion asc", cn.cn);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new SelectListItem() { Text = (string)reader[1], Value = reader[0] + "", Selected = false });
                }
            }
            catch (MySqlException e)
            {
                throw;
            }


            return lista;
        }
    }
}
