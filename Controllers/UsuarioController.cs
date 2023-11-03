using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        Usuario u = new Usuario();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Usuario", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<UsuarioModel> lista = u.Consultar();

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
            if (log.ValidarAccion("Usuario", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<UsuarioModel> lista = u.Consultar();
            IEnumerable<UsuarioModel> listaResultado;

            var resultado = (from usuario in lista
                             where ((buscar == "" || usuario.nombre.ToLower().Contains(buscar)) || (buscar == "" || usuario.idEmpleado.ToLower().Contains(buscar)))
                             select new UsuarioModel
                             {
                                 Id = usuario.Id,
                                 idEmpleado = usuario.idEmpleado,
                                 idPerfil = usuario.idPerfil,
                                 nombre = usuario.nombre,
                                 password = usuario.password,
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Usuario", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Empleados = ListaEmpleados();
            ViewBag.Perfiles = ListaPerfiles();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(UsuarioModel modelo)
        {
            if (log.ValidarAccion("Usuario", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            u.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el usuario correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Usuario", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Empleados = ListaEmpleados();
            ViewBag.Perfiles = ListaPerfiles();
            return View(u.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(UsuarioModel modelo)
        {
            if (log.ValidarAccion("Usuario", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            u.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el usuario correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Usuario", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(u.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(UsuarioModel modelo)
        {
            if (log.ValidarAccion("Usuario", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            u.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el usuario correctamente!";
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ListaEmpleados()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select id, nombres from empleado", cn.cn);
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

        private List<SelectListItem> ListaPerfiles()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select id, descripcion from perfil", cn.cn);
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
