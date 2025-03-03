using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CapaPresentacion.Controllers
{
    public class CitaController : Controller
    {
        private readonly CitaBL _citaBL;

        public CitaController(CitaBL citaBL)
        {
            _citaBL = citaBL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public List<CitaCLS> ObtenerCitas()
        {
            return _citaBL.ObtenerCitas();
        }

        public List<CitaCLS> FiltrarCitas(CitaCLS objCita)
        {
            return _citaBL.FiltrarCitas(objCita);
        }

        public int AgregarCita(CitaCLS cita)
        {
            return _citaBL.AgregarCita(cita);
        }

        public CitaCLS RecuperarCita(int id)
        {
            return _citaBL.RecuperarCita(id);
        }

        public int ActualizarCita(CitaCLS cita)
        {
            return _citaBL.ActualizarCita(cita);
        }

        public int EliminarCita(CitaCLS cita)
        {
            return _citaBL.EliminarCita(cita);
        }
    }
}