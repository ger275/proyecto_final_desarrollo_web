using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class LoteController : Controller
    {
        Lote l = new Lote();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("Lote", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<LoteModel> lista = l.Consultar();

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
            if (log.ValidarAccion("Lote", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<LoteModel> lista = l.Consultar();
            IEnumerable<LoteModel> listaResultado;

            var resultado = (from lote in lista
                             where (buscar == "" || lote.numeroLote.ToLower().Contains(buscar))
                             select new LoteModel
                             {
                                 Id = lote.Id,
                                 idProducto = lote.idProducto,
                                 numeroLote = lote.numeroLote,
                                 fechaVence = lote.fechaVence
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("Lote", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Productos = ListaProductos();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(LoteModel modelo)
        {
            if (log.ValidarAccion("Lote", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            l.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el lote correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("Lote", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Productos = ListaProductos();
            return View(l.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(LoteModel modelo)
        {
            if (log.ValidarAccion("Lote", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            l.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el lote correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("Lote", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(l.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PacienteModel modelo)
        {
            if (log.ValidarAccion("Lote", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            l.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el lote correctamente!";
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ListaProductos()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select id, nombre from producto", cn.cn);
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
