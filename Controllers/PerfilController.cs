using Microsoft.AspNetCore.Mvc;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        Perfil p = new Perfil();

        public IActionResult Index(int pag = 1)
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult Crear(PerfilModelo modelo)
        {
            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el perfil correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PerfilModelo modelo)
        {
            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el perfil correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PerfilModelo modelo)
        {
            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el perfil correctamente!";
            return RedirectToAction("Index");
        }
    }
}
