using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Hospitalaria.Models;
using CapaNegocios;

namespace CapaPresentacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdministradorBL _administradorBL;

        public HomeController(ILogger<HomeController> logger, AdministradorBL administradorBL)
        {
            _logger = logger;
            _administradorBL = administradorBL;
        }

        private void CargarTotales()
        {
            ViewBag.TotalPacientes = _administradorBL.ContarPacientes();
            ViewBag.TotalMedicos = _administradorBL.ContarMedicos();
            ViewBag.TotalEspecialidades = _administradorBL.ContarEspecialidades();
            ViewBag.TotalCitas = _administradorBL.ContarCitas();
            ViewBag.TotalIngreso = _administradorBL.ObtenerIngresoTotal();
            ViewBag.TotalIngresoMes = _administradorBL.ObtenerIngresoMesActual();
        }

        public IActionResult Index()
        {
            CargarTotales();
            return View();
        }
    }
}
