using CapaEntidades;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos
{
    public class PacienteDAL
    {
        private readonly HospitalDbContext _context;

        public PacienteDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<PacienteCLS> ObtenerPacientes()
        {
            return _context.Pacientes
                .Select(p => new PacienteCLS
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    FechaNacimiento = p.FechaNacimiento,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Direccion = p.Direccion
                }).ToList();
        }

        public PacienteCLS ObtenerPorId(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                return new PacienteCLS
                {
                    Id = paciente.Id,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                    FechaNacimiento = paciente.FechaNacimiento,
                    Telefono = paciente.Telefono,
                    Email = paciente.Email,
                    Direccion = paciente.Direccion
                };
            }
            return null;
        }


        public PacienteCLS RecuperarPaciente(int id)
        {
            var nuevoPaciente = new PacienteCLS
            {
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                FechaNacimiento = paciente.FechaNacimiento,
                Telefono = paciente.Telefono,
                Email = paciente.Email,
                Direccion = paciente.Direccion
            };
            _context.Pacientes.Add(nuevoPaciente);
            _context.SaveChanges();
        }

        public void ActualizarPaciente(PacienteCLS paciente)
        {
            var pacienteExistente = _context.Pacientes.Find(paciente.Id);
            if (pacienteExistente != null)
            {
                pacienteExistente.Nombre = paciente.Nombre;
                pacienteExistente.Apellido = paciente.Apellido;
                pacienteExistente.FechaNacimiento = paciente.FechaNacimiento;
                pacienteExistente.Telefono = paciente.Telefono;
                pacienteExistente.Email = paciente.Email;
                pacienteExistente.Direccion = paciente.Direccion;
                _context.SaveChanges();
            }
        }

        public void EliminarPaciente(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
            }
        }
    }
}