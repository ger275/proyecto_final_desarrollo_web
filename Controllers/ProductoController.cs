using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        Producto p = new Producto();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Producto", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<ProductoModel> lista = p.Consultar();

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
            if (log.ValidarAccion("Producto", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<ProductoModel> lista = p.Consultar();
            IEnumerable<ProductoModel> listaResultado;

            var resultado = (from producto in lista
                                 //where buscar == "" || paciente.nombres.ToLower().StartsWith(buscar)
                             where (buscar == "" || producto.nombre.ToLower().Contains(buscar))
                             select new ProductoModel
                             {
                                 Id = producto.Id,
                                 nombre = producto.nombre,
                                 precioCosto = producto.precioCosto,
                                 precioVenta = producto.precioVenta
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Producto", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProductoModel modelo)
        {
            if (log.ValidarAccion("Producto", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el producto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Producto", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(ProductoModel modelo)
        {
            if (log.ValidarAccion("Producto", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el producto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Producto", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(ProductoModel modelo)
        {
            if (log.ValidarAccion("Producto", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el producto correctamente!";
            return RedirectToAction("Index");
        }
    }
}
