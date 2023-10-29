using Microsoft.AspNetCore.Mvc;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        Paciente p = new Paciente();

        public IActionResult Index(int pag = 1)
        {
            IEnumerable<PacienteModel> lista = p.Consultar();

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

            //return View(lista);
            return View(nuevaLista);
        }

        [HttpPost]
        public IActionResult Index(string buscar = "")
        {
            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<PacienteModel> lista = p.Consultar();
            IEnumerable<PacienteModel> listaResultado;

            var resultado = (from paciente in lista
                     //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                     where (buscar == "" || paciente.nombres.ToLower().Contains(buscar)) || (buscar == "" || paciente.apellidos.ToLower().Contains(buscar))
                     select new PacienteModel
                     {
                         Id = paciente.Id,
                         nombres = paciente.nombres,
                         apellidos = paciente.apellidos,
                         fechaNacimiento = paciente.fechaNacimiento,
                         numeroDpi = paciente.numeroDpi,
                         telefono = paciente.telefono,
                         direccionResidencia = paciente.direccionResidencia
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
        public ActionResult Crear(PacienteModel modelo)
        {
            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el paciente correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PacienteModel modelo)
        {
            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el paciente correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PacienteModel modelo)
        {
            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el paciente correctamente!";
            return RedirectToAction("Index");
        }
    }
}
