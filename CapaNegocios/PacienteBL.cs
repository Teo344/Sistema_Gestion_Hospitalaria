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

        public List<PacienteCLS> FiltrarPaciente(PacienteCLS objPaciente)
        {
            return _pacienteDAL.FiltrarPaciente(objPaciente);
        }

        public int AgregarPaciente(PacienteCLS paciente)
        {
            // Se valida que los datos sean correctos antes de enviarlos a la base de datos
            if (string.IsNullOrWhiteSpace(paciente.Nombre) ||
                string.IsNullOrWhiteSpace(paciente.Apellido))
            {
                return 0; // Retorna 0 si los datos no son válidos
            }

            return _pacienteDAL.AgregarPaciente(paciente);
        }

        public PacienteCLS RecuperarPaciente(int id)
        {
            return _pacienteDAL.RecuperarPaciente(id);
        }

        public int ActualizarPaciente(PacienteCLS paciente)
        {
            if (paciente.Id <= 0)
            {
                return 0; // Retorna 0 si los datos no son válidos
            }

            _pacienteDAL.ActualizarPaciente(paciente);
            return 1; // Retorna 1 si la actualización fue exitosa
        }
        public int EliminarPaciente(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            _pacienteDAL.EliminarPaciente(id);
            return 1;
        }
    }
}