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

        public int AgregarMedico(MedicoCLS medico)
        {
            return _medicoBL.AgregarMedico(medico);
        }

        public MedicoCLS RecuperarMedico(int id)
        {
            return _medicoBL.RecuperarMedico(id);
        }

        public int EliminarMedico(MedicoCLS medico)
        {
            return _medicoBL.EliminarMedico(medico);
        }
        public List<MedicoCLS> FiltrarMedico(MedicoCLS objMedico)
        {
            return _medicoBL.FiltrarMedico(objMedico);
        }



        public int ActualizarMedico(MedicoCLS medico)
        {
            return _medicoBL.ActualizarMedico(medico);
        }

    }
}
