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

        public IActionResult Index(int pag = 1)
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult Crear(PuestoModel modelo)
        {
            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el puesto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PuestoModel modelo)
        {
            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el puesto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PuestoModel modelo)
        {
            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el puesto correctamente!";
            return RedirectToAction("Index");
        }
    }
}
