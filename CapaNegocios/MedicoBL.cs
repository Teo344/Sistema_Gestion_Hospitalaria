using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class MedicoBL
    {
        private readonly MedicoDAL _medicoDAL;

        public MedicoBL(MedicoDAL medicoDAL)
        {
            _medicoDAL = medicoDAL;
        }

        public List<MedicoCLS> ObtenerMedicos()
        {
            return _medicoDAL.ObtenerMedicos();
        }

        public MedicoCLS ObtenerPorId(int id)
        {
            return _medicoDAL.ObtenerPorId(id);
        }

        public void AgregarMedico(MedicoCLS medico)
        {
            // Lógica adicional si es necesario, como validaciones
            _medicoDAL.Agregar(medico);
        }

        public void ActualizarMedico(MedicoCLS medico)
        {
            // Lógica adicional si es necesario, como validaciones
            _medicoDAL.Actualizar(medico);
        }

        public int EliminarMedico(MedicoCLS medico)
        {
            if (medico.Id <= 0)
            {
                return 0;
            }

            try
            {
                _medicoDAL.EliminarMedico(medico);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el medico: " + ex.Message);
            }
        }





    }
}
