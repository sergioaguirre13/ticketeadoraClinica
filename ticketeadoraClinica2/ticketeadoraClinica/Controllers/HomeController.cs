using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ticketeadoraClinica.Models;

namespace ticketeadoraClinica.Controllers
{
    public class HomeController : Controller
    {

        private readonly ClinicaContext _context;

        public HomeController(ClinicaContext context)
        {
            _context = context;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult ConfirmarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                ViewBag.Mensaje = "Por favor, ingrese un DNI valido";
                ViewBag.Tipo = "danger";
            }
            else if (dni.Length != 8)
            {
                ViewBag.Mensaje = "Debe ingresar un dni con mas de 8 digitos";
                ViewBag.Tipo = "warning";
            }
            // Buscar en la base de datos
            var paciente = _context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if (paciente == null)
            {
                ViewBag.Mensaje = "No se encontró ningún paciente con ese DNI.";
                ViewBag.Tipo = "danger";
                return View("Index");
            }

            // Si existe, redirigir al menú principal con sus datos
            return RedirectToAction("MenuPrincipal", new { dni });
        }


        public IActionResult MenuPrincipal(string dni)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            //string codigoTurno = GenerarCodigoTurno();

            //// Guardar en ViewBag (más adelante podrías guardarlo en la BD)
            //ViewBag.CodigoTurno = codigoTurno;

            return View(paciente);
        
        }

        public IActionResult TurnoAsignado(bool tieneDiscapacidad, bool esPrioritario)
        {
            Console.WriteLine($"[DEBUG] tieneDiscapacidad: {tieneDiscapacidad}, esPrioritario: {esPrioritario}");

            string codigoTurno = GenerarCodigoTurno(tieneDiscapacidad,esPrioritario);
            ViewBag.CodigoTurno = codigoTurno;
            ViewBag.TieneDiscapacidad = tieneDiscapacidad;
            ViewBag.EsPrioritario = esPrioritario;

            return View();
        }



        private static int contadorTurnos = 0;
        private string GenerarCodigoTurno(bool tieneDiscapacidad, bool esPrioritario)
        {
            contadorTurnos++;

            string prefijo = string.Empty;

            if (tieneDiscapacidad)
            {
                prefijo = "D";
            }
            else if (esPrioritario)
            {
                prefijo = "P";
            }
            else
            {
                prefijo = "A";
            }

                return $"{prefijo}{contadorTurnos:D3}";
        }
    }
}
