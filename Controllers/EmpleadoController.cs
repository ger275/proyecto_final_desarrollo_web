using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    public class EmpleadoController : Controller
    {
        Empleado e = new Empleado();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Empleado", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<EmpleadoModel> lista = e.Consultar();

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
            if (log.ValidarAccion("Empleado", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<EmpleadoModel> lista = e.Consultar();
            IEnumerable<EmpleadoModel> listaResultado;

            var resultado = (from empleado in lista
                                 //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                             where (buscar == "" || empleado.nombres.ToLower().Contains(buscar)) || (buscar == "" || empleado.apellidos.ToLower().Contains(buscar))
                             select new EmpleadoModel
                             {
                                 Id = empleado.Id,
                                 idPuesto = empleado.idPuesto,
                                 nombres = empleado.nombres,
                                 apellidos = empleado.apellidos,
                                 fechaNacimiento = empleado.fechaNacimiento,
                                 numeroDpi = empleado.numeroDpi,
                                 salario = empleado.salario,
                                 telefono = empleado.telefono,
                                 direccionResidencia = empleado.direccionResidencia
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Empleado", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Puestos = ListaPuestos();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(EmpleadoModel modelo)
        {
            if (log.ValidarAccion("Empleado", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            e.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el empleado correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Empleado", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Puestos = ListaPuestos();
            return View(e.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoModel modelo)
        {
            if (log.ValidarAccion("Empleado", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            e.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el empleado correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Empleado", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(e.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(EmpleadoModel modelo)
        {
            if (log.ValidarAccion("Empleado", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            e.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el empleado correctamente!";
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ListaPuestos()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select id, descripcion from puesto", cn.cn);
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
