using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_Gestion_Hospitalaria.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteBL _pacienteBL;

        // Constructor que recibe la clase de la capa de negocio
        public PacienteController(PacienteBL pacienteBL)
        {
            _pacienteBL = pacienteBL;
        }
        public ActionResult Index()
        {
            return View();
        }

        public List<PacienteCLS> ObtenerPacientes()
        {
            return _pacienteBL.ObtenerPacientes();
        }

    }
}
