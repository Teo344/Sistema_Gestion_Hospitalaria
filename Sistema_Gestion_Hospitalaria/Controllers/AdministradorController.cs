using Microsoft.AspNetCore.Mvc;
using CapaNegocios;
using CapaEntidades;
using CapaDatos;

namespace CapaPresentacion.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly AdministradorBL _administradorBL;

        public AdministradorController(AdministradorBL administradorBL)
        {
            _administradorBL = administradorBL;
        }

        public AdministradorCLS RecuperarAdministrador(int id)
        {
            return _administradorBL.RecuperarAdministrador(id);
        }

        public IActionResult Index()
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");

            if (adminId == null)
            {
                return RedirectToAction("Index", "Acceso"); 
            }

            var administrador = RecuperarAdministrador(adminId.Value);

            if (administrador == null)
            {
                return RedirectToAction("Index", "Acceso"); 
            }

            return View(administrador);
        }

        public int ActualizarAdministrador(AdministradorCLS administrador)
        {
            return _administradorBL.ActualizarAdministrador(administrador);
        }

        public int CambiarClave(int id, string nuevaClave)
        {
            return _administradorBL.ActualizarClaveAdministrador(id, nuevaClave);

        }

        [HttpGet]
        public JsonResult ObtenerIdSesion()
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            Console.WriteLine($"ID obtenido: {adminId}");
            if (adminId == null)
            {
                return Json("No se pudo obtener el ID del administrador.");
            }
            return Json(adminId);
        }
    }
}