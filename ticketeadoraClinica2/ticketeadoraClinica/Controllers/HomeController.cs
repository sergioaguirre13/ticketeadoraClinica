using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ticketeadoraClinica.Models;

namespace ticketeadoraClinica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
            else
            {
                ViewBag.Mensaje = ViewBag.Mensaje = $"  el dni ingresado es: {dni}";
                ViewBag.Tipo = "success";

                return RedirectToAction("Privacy");
            }

            return View("Index");
        }
    }
}
