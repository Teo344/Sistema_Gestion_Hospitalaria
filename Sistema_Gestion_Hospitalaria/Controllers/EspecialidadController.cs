using CapaEntidades;
using CapaNegocios;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly EspecialidadBL _especialidadBL;


        // Constructor que recibe la clase de la capa de negocio
        public EspecialidadController(EspecialidadBL especialidadBL)
        {
            _especialidadBL = especialidadBL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<EspecialidadCLS> ObtenerEspecialidades()
        {
            return _especialidadBL.ObtenerEspecialidades();
        }

        public List<EspecialidadCLS> FiltrarEspecialidad(EspecialidadCLS objEspecialidad)
        {
            return _especialidadBL.FiltrarEspecialidad(objEspecialidad);
        }

        public int AgregarEspecialidad(EspecialidadCLS especialidad)
        {
            return _especialidadBL.AgregarEspecialidad(especialidad);
        }

        public EspecialidadCLS RecuperarEspecialidad(int id)
        {
            return _especialidadBL.RecuperarEspecialidad(id);
        }

        public int ActualizarEspecialidad(EspecialidadCLS especialidad)
        {
            return _especialidadBL.ActualizarEspecialidad(especialidad);
        }

        public int EliminarEspecialidad(EspecialidadCLS especialidad)
        {
            return _especialidadBL.EliminarEspecialidad(especialidad);
        }



    }
}
