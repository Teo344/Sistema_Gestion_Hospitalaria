using System;
using System.Collections.Generic;
using System.Data;
using CapaEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos
{
    public class CitaDAL
    {
        private readonly HospitalDbContext _context;

        public CitaDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<CitaCLS> ObtenerCitas()
        {
            List<CitaCLS> lista = new List<CitaCLS>();
            lista = _context.Citas
                .FromSqlRaw("EXEC uspObtenerCitas")
                .ToList();
            return lista;
        }

        public List<CitaCLS> FiltrarCitas(CitaCLS objCita)
        {
            List<CitaCLS> lista = new List<CitaCLS>();
            lista = _context.Citas
                .FromSqlRaw("EXEC uspFiltrarCitas @p0, @p1, @p2, @p3, @p4",
                    objCita.PacienteIdentificacion == null ? (object)DBNull.Value : objCita.PacienteIdentificacion,
                    objCita.MedicoIdentificacion == null ? (object)DBNull.Value : objCita.MedicoIdentificacion,
                    objCita.FechaHora == default ? (object)DBNull.Value : objCita.FechaHora,
                    objCita.FechaHora == default ? (object)DBNull.Value : objCita.FechaHora,
                    objCita.Estado == null ? (object)DBNull.Value : objCita.Estado
                )
                .ToList();
            return lista;
        }

        public int AgregarCita(CitaCLS cita)
        {
            try
            {
                return _context.Database.ExecuteSqlRaw("EXEC uspInsertarCita @p0, @p1, @p2, @p3",
                    cita.PacienteIdentificacion,
                    cita.MedicoIdentificacion,
                    cita.FechaHora,
                    cita.Estado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar cita: {ex.Message}");
                return 0;
            }
        }

        public CitaCLS RecuperarCita(int id)
        {
            return _context.Citas
                .FromSqlRaw("EXEC uspRecuperarCita @p0", id)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public void ActualizarCita(CitaCLS cita)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC uspActualizarCita @p0, @p1, @p2, @p3, @p4",
                    cita.Id,
                    cita.PacienteIdentificacion,
                    cita.MedicoIdentificacion,
                    cita.FechaHora,
                    cita.Estado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cita: {ex.Message}");
                throw;
            }
        }

        public void EliminarCita(CitaCLS cita)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarCita @p0", cita.Id);
        }
        public bool ExistePaciente(string identificacion)
        {
            return _context.Pacientes.Any(p => p.Identificacion == identificacion);
        }
        public bool ExisteMedico(string identificacion)
        {
            return _context.Medicos.Any(m => m.Identificacion == identificacion);
        }

        public bool MedicoTieneCitaEnHorario(string medicoIdentificacion, DateTime fechaHora, int? citaId = null)
        {
            var medicoId = _context.Medicos
                .Where(m => m.Identificacion == medicoIdentificacion)
                .Select(m => m.Id)
                .FirstOrDefault();

            if (medicoId == 0)
            {
                return false; // Médico no encontrado
            }

            // Calcular el rango de tiempo (30 minutos antes y después de la cita)
            DateTime fechaHoraInicio = fechaHora.AddMinutes(-29);
            DateTime fechaHoraFin = fechaHora.AddMinutes(29);

            return _context.Citas
                .Any(c => c.MedicoId == medicoId &&
                          c.FechaHora >= fechaHoraInicio &&
                          c.FechaHora <= fechaHoraFin &&
                          (citaId == null || c.Id != citaId)); 
        }

    }
}