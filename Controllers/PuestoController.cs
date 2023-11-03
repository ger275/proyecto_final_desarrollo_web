using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class PuestoController : Controller
    {
        Puesto p = new Puesto();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Puesto", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<PuestoModel> lista = p.Consultar();

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
            if (log.ValidarAccion("Puesto", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<PuestoModel> lista = p.Consultar();
            IEnumerable<PuestoModel> listaResultado;

            var resultado = (from puesto in lista
                                 //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                             where (buscar == "" || puesto.descripcion.ToLower().Contains(buscar))
                             select new PuestoModel
                             {
                                 Id = puesto.Id,
                                 descripcion = puesto.descripcion
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Puesto", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(PuestoModel modelo)
        {
            if (log.ValidarAccion("Puesto", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el puesto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Puesto", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PuestoModel modelo)
        {
            if (log.ValidarAccion("Puesto", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el puesto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Puesto", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PuestoModel modelo)
        {
            if (log.ValidarAccion("Puesto", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el puesto correctamente!";
            return RedirectToAction("Index");
        }
    }
}
