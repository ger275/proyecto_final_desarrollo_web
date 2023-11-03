using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class PermisoController : Controller
    {
        Permiso p = new Permiso();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Permiso", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<PermisoModel> lista = p.Consultar();

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
            if (log.ValidarAccion("Permiso", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<PermisoModel> lista = p.Consultar();
            IEnumerable<PermisoModel> listaResultado;

            var resultado = (from permiso in lista
                                 //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                             where (buscar == "" || permiso.menu.ToLower().Contains(buscar)) || (buscar == "" || permiso.controlador.ToLower().Contains(buscar))
                             select new PermisoModel
                             {
                                 Id = permiso.Id,
                                 menu = permiso.menu,
                                 controlador = permiso.controlador,
                                 accion = permiso.accion,
                                 titulo = permiso.titulo
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Permiso", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(PermisoModel modelo)
        {
            if (log.ValidarAccion("Permiso", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el permiso correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Permiso", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PermisoModel modelo)
        {
            if (log.ValidarAccion("Permiso", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el permiso correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Permiso", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PacienteModel modelo)
        {
            if (log.ValidarAccion("Permiso", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el permiso correctamente!";
            return RedirectToAction("Index");
        }
    }
}
