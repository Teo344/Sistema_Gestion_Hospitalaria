using Microsoft.AspNetCore.Mvc;
using CapaNegocios;
using CapaEntidades;
using System.Linq;

namespace CapaPresentacion.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AdministradorBL _administradorBL;

        // Inyección de dependencia
        public AccesoController(AdministradorBL administradorBL)
        {
            _administradorBL = administradorBL;
        }

        public IActionResult Index()
        {
            // Mostrar mensajes de error si existen
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string clave)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(clave))
            {
                TempData["Error"] = "Correo y contraseña son obligatorios.";
                return RedirectToAction("Index");
            }

            var oAdministrador = _administradorBL.ObtenerAdministradores()
                .FirstOrDefault(p => p.Email == email && _administradorBL.VerificarClave(clave, p.Clave));

            if (oAdministrador == null)
            {
                TempData["Error"] = "Correo o contraseña incorrecta.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult CerrarSesion()
        {
            return RedirectToAction("Index", "Acceso");
        }
    }
}