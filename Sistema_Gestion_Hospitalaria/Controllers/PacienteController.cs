using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapaDatos;

namespace CapaPresentacion.Controllers
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

        public List<PacienteCLS> FiltrarPaciente(PacienteCLS objPaciente)
        {
            return _pacienteBL.FiltrarPaciente(objPaciente);
        }

        public int AgregarPaciente(PacienteCLS paciente)
        {
            return _pacienteBL.AgregarPaciente(paciente);
        }

        public PacienteCLS RecuperarPaciente(int id)
        {
            return _pacienteBL.RecuperarPaciente(id);
        }

        public int ActualizarPaciente(PacienteCLS paciente)
        {
            _pacienteBL.ActualizarPaciente(paciente);
            return 1; // Retorna 1 si la actualización fue exitosa
        }

        public int EliminarPaciente(int id)
        {
            return _pacienteBL.EliminarPaciente(id);
        }
    }
}
