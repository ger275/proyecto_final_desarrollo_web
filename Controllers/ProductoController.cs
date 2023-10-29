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

        public IActionResult Index(int pag = 1)
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProductoModel modelo)
        {
            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el producto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(ProductoModel modelo)
        {
            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el producto correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(ProductoModel modelo)
        {
            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el producto correctamente!";
            return RedirectToAction("Index");
        }
    }
}
