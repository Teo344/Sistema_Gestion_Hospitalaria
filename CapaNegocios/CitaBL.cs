using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class CitaBL
    {
        private readonly CitaDAL _citaDAL;

        public CitaBL(CitaDAL citaDAL)
        {
            _citaDAL = citaDAL;
        }

        public List<CitaCLS> ObtenerCitas()
        {
            return _citaDAL.ObtenerCitas();
        }

        public List<CitaCLS> FiltrarCitas(CitaCLS objCita)
        {
            return _citaDAL.FiltrarCitas(objCita);
        }

        public int AgregarCita(CitaCLS cita)
        {
            if (!ValidarCampos(cita))
            {
                return -1; 
            }

            if (!ValidarFechaHora(cita.FechaHora))
            {
                return -2; 
            }

            if (!ValidarEstado(cita.Estado))
            {
                return -3; 
            }

            if (!_citaDAL.ExistePaciente(cita.PacienteIdentificacion))
            {
                return -4;
            }

            if (!_citaDAL.ExisteMedico(cita.MedicoIdentificacion))
            {
                return -5;
            }

            if (cita.FechaHora.TimeOfDay < TimeSpan.FromHours(7) || cita.FechaHora.TimeOfDay > TimeSpan.FromHours(18))
            {
                return -6; // Código de error: Hora fuera del rango permitido
            }

            if (_citaDAL.MedicoTieneCitaEnHorario(cita.MedicoIdentificacion, cita.FechaHora))
            {
                return -7; // Código de error: El médico ya tiene una cita en ese horario
            }

            return _citaDAL.AgregarCita(cita);
        }

        public CitaCLS RecuperarCita(int id)
        {
            return _citaDAL.RecuperarCita(id);
        }

        public int ActualizarCita(CitaCLS cita)
        {
            if (cita.Id <= 0)
            {
                return 0; 
            }

            if (!ValidarCampos(cita))
            {
                return -1; 
            }

            if (!ValidarFechaHora(cita.FechaHora))
            {
                return -2; 
            }

            if (!ValidarEstado(cita.Estado))
            {
                return -3; 
            }

            if (!_citaDAL.ExistePaciente(cita.PacienteIdentificacion))
            {
                return -4; 
            }

            if (!_citaDAL.ExisteMedico(cita.MedicoIdentificacion))
            {
                return -5; 
            }

            if (cita.FechaHora.TimeOfDay < TimeSpan.FromHours(7) || cita.FechaHora.TimeOfDay > TimeSpan.FromHours(18))
            {
                return -6; // Código de error: Hora fuera del rango permitido
            }

            if (_citaDAL.MedicoTieneCitaEnHorario(cita.MedicoIdentificacion, cita.FechaHora))
            {
                return -7; // Código de error: El médico ya tiene una cita en ese horario
            }

            _citaDAL.ActualizarCita(cita);
            return 1; 
        }

        public int EliminarCita(CitaCLS cita)
        {
            if (cita.Id <= 0)
            {
                return 0; 
            }

            _citaDAL.EliminarCita(cita);
            return 1; 
        }

        private bool ValidarCampos(CitaCLS cita)
        {
            if (string.IsNullOrWhiteSpace(cita.PacienteIdentificacion) ||
                string.IsNullOrWhiteSpace(cita.MedicoIdentificacion) ||
                string.IsNullOrWhiteSpace(cita.Estado))
            {
                return false;
            }
            return true;
        }

        private bool ValidarFechaHora(DateTime fechaHora)
        {
            if (fechaHora < DateTime.Now)
            {
                return false; 
            }
            return true;
        }

        private bool ValidarEstado(string estado)
        {
            List<string> estadosPermitidos = new List<string> { "Programada", "Cancelada", "Completada" };
            return estadosPermitidos.Contains(estado);
        }
    }
}