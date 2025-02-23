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

        public List<MedicoCLS> ObtenerTodos()
        {
            return _medicoDAL.ObtenerTodos();
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

        public void EliminarMedico(int id)
        {
            // Lógica adicional si es necesario, como verificar dependencias
            _medicoDAL.Eliminar(id);
        }
    }
}
