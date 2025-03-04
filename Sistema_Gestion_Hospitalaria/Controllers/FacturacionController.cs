using CapaEntidades;
using CapaNegocios;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    public class FacturacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly FacturacionBL _facturacionBL;

        // Constructor que recibe la clase de la capa de negocio
        public FacturacionController(FacturacionBL facturacionBL)
        {
            _facturacionBL = facturacionBL;
        }

        public List<FacturacionCLS> ObtenerFacturaciones()
        {
            return _facturacionBL.ObtenerFacturaciones();
        }




    }
}
