using System.Collections.Generic;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class PacienteBL
    {
        private readonly PacienteDAL _pacienteDAL;

        public PacienteBL(PacienteDAL pacienteDAL)
        {
            _pacienteDAL = pacienteDAL;
        }

        public List<PacienteCLS> ObtenerPacientes()
        {
            return _pacienteDAL.ObtenerPacientes();
        }
    }
}