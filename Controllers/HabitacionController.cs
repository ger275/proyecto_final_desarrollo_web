using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class HabitacionController : Controller
    {
        Habitacion h = new Habitacion();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Habitacion", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<HabitacionModel> lista = h.Consultar();

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
            if (log.ValidarAccion("Habitacion", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<HabitacionModel> lista = h.Consultar();
            IEnumerable<HabitacionModel> listaResultado;

            var resultado = (from habitacion in lista
                                 //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                             where (buscar == "" || habitacion.descripcion.ToLower().Contains(buscar)) || (buscar == "" || habitacion.status.ToLower().Contains(buscar))
                             select new HabitacionModel
                             {
                                 Id = habitacion.Id,
                                 numero = habitacion.numero,
                                 descripcion = habitacion.descripcion,
                                 status = habitacion.status
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Habitacion", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.status = ListaStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(HabitacionModel modelo)
        {
            if (log.ValidarAccion("Habitacion", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            h.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo la habitación correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Habitacion", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(h.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(HabitacionModel modelo)
        {
            if (log.ValidarAccion("Habitacion", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            h.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito la habitación correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Habitacion", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(h.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(HabitacionModel modelo)
        {
            if (log.ValidarAccion("Habitacion", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            h.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino la habitación correctamente!";
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ListaStatus()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione--", Value = "", Selected = false });
            lista.Add(new SelectListItem() { Text = "DISPONIBLE", Value = "1", Selected = true });
            lista.Add(new SelectListItem() { Text = "OCUPADA", Value = "2", Selected = false });

            return lista;
        }
    }
}
