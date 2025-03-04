using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    public class TratamientoController : Controller
    {
        private readonly TratamientoBL _tratamientoBL;

        public TratamientoController(TratamientoBL tratamientoBL)
        {
            _tratamientoBL = tratamientoBL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public List<TratamientoCLS> ObtenerTratamientos()
        {
            try
            {
                return _tratamientoBL.ObtenerTratamientos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<TratamientoCLS>();
            }
        }

        public List<TratamientoCLS> FiltrarTratamientos(TratamientoCLS objTratamiento)
        {
            return _tratamientoBL.FiltrarTratamientos(objTratamiento);
        }

        public int AgregarTratamiento(TratamientoCLS tratamiento)
        {
            return _tratamientoBL.AgregarTratamiento(tratamiento);
        }

        public TratamientoCLS RecuperarTratamiento(int id)
        {
            return _tratamientoBL.RecuperarTratamiento(id);
        }

        public int ActualizarTratamiento(TratamientoCLS tratamiento)
        {
            return _tratamientoBL.ActualizarTratamiento(tratamiento);
        }

        public int EliminarTratamiento(TratamientoCLS tratamiento)
        {
            return _tratamientoBL.EliminarTratamiento(tratamiento);
        }
    }
}