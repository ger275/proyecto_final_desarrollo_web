using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoFinalDesarrolloWeb.Datos;
using ProyectoFinalDesarrolloWeb.Models;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    [Authorize]
    public class PacienteHabitacionController : Controller
    {
        PacienteHabitacion p = new PacienteHabitacion();
        Login log = new Login();

        public IActionResult Index(int pag = 1)
        {
            if (log.ValidarAccion("PacienteHabitacion", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            IEnumerable<PacienteHabitacionModel> lista = p.Consultar();

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
            if (log.ValidarAccion("PacienteHabitacion", "Index", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            buscar = string.IsNullOrEmpty(buscar) ? "" : buscar.ToLower();
            IEnumerable<PacienteHabitacionModel> lista = p.Consultar();
            IEnumerable<PacienteHabitacionModel> listaResultado;

            var resultado = (from pacientehabitacion in lista
                             where (buscar == "" || pacientehabitacion.idPaciente.ToLower().Contains(buscar)) || (buscar == "" || pacientehabitacion.idHabitacion.ToLower().Contains(buscar))
                             select new PacienteHabitacionModel
                             {
                                 Id = pacientehabitacion.Id,
                                 idPaciente = pacientehabitacion.idPaciente,
                                 idHabitacion = pacientehabitacion.idHabitacion,
                                 fechaHoraEntrada = pacientehabitacion.fechaHoraEntrada,
                                 fechaHoraSalida = pacientehabitacion.fechaHoraSalida
                             }
                );

            listaResultado = resultado;

            return View(listaResultado);
        }

        public ActionResult Crear()
        {
            if (log.ValidarAccion("PacienteHabitacion", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Habitaciones = ListaHabitaciones();
            ViewBag.Pacientes = ListaPacientes();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(PacienteHabitacionModel modelo)
        {
            if (log.ValidarAccion("PacienteHabitacion", "Crear", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Crear(modelo);
            TempData["Mensaje"] = "¡Se registro la entrada correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            if (log.ValidarAccion("PacienteHabitacion", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            ViewBag.Habitaciones = ListaHabitaciones();
            ViewBag.Pacientes = ListaPacientes();
            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Editar(PacienteHabitacionModel modelo)
        {
            if (log.ValidarAccion("PacienteHabitacion", "Editar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Editar(modelo);
            TempData["Mensaje"] = "¡Se edito la entrada correctamente!";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            if (log.ValidarAccion("PacienteHabitacion", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(p.Consultar(id));
        }

        [HttpPost]
        public ActionResult Eliminar(PacienteHabitacionModel modelo)
        {
            if (log.ValidarAccion("PacienteHabitacion", "Eliminar", User.Identity.Name) == 0)
            {
                return RedirectToAction("Privacy", "Home");
            }

            p.Eliminar(modelo.Id);
            TempData["Mensaje"] = "¡Se elimino el registro correctamente!";
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ListaHabitaciones()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select id, concat(numero, ' ', descripcion) as habitacion from habitacion", cn.cn);
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

        private List<SelectListItem> ListaPacientes()
        {
            List<SelectListItem> lista = new();
            lista.Add(new SelectListItem() { Text = "--Seleccione", Value = "", Selected = true });

            Conexion cn = new Conexion();

            try
            {
                cn.Conectar();

                MySqlCommand cmd = new MySqlCommand("select id, nombres from paciente", cn.cn);
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
