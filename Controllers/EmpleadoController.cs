using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        Empleado e = new Empleado();

        public IActionResult Index(int pag = 1)
        {
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
            ViewBag.Puestos = ListaPuestos();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(EmpleadoModel modelo)
        {
            e.Crear(modelo);
            TempData["Mensaje"] = "¡Se creo el empleado correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Puestos = ListaPuestos();
            return View(e.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoModel modelo)
        {
            e.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito el empleado correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            return View(e.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(EmpleadoModel modelo)
        {
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
