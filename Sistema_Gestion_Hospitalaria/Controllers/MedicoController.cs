using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoBL _medicoBL;

        // Constructor que recibe la clase de la capa de negocio
        public MedicoController(MedicoBL medicoBL)
        {
            _medicoBL = medicoBL;
        }
        public ActionResult Index()
        {
            return View();
        }

        public List<MedicoCLS> ObtenerMedicos()
        {
            return _medicoBL.ObtenerMedicos();
        }

    }
}
