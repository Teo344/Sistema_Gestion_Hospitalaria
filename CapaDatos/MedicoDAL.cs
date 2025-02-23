using CapaEntidades;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos
{
    public class MedicoDAL
    {
        private readonly HospitalDbContext _context;

        public MedicoDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<MedicoCLS> ObtenerMedicos()
        {
            List<MedicoCLS> lista = _context.Medicos
                .Include(m => m.Especialidad)
                .Select(medico => new MedicoCLS
                {
                    Id = medico.Id,
                    Nombre = medico.Nombre,
                    Apellido = medico.Apellido,
                    EspecialidadId = medico.EspecialidadId,
                    Telefono = medico.Telefono,
                    Email = medico.Email,
                    Especialidad = new EspecialidadCLS
                    {
                        Id = medico.Especialidad.Id,
                        Nombre = medico.Especialidad.Nombre
                    }
                }).ToList();

            return lista;
        }

        public MedicoCLS ObtenerPorId(int id)
        {
            var medico = _context.Medicos
                .Include(m => m.Especialidad)
                .FirstOrDefault(m => m.Id == id);

            if (medico == null)
                return null;

            return new MedicoCLS
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                EspecialidadId = medico.EspecialidadId,
                Telefono = medico.Telefono,
                Email = medico.Email,
                Especialidad = new EspecialidadCLS
                {
                    Id = medico.Especialidad.Id,
                    Nombre = medico.Especialidad.Nombre
                }
            };
        }

        public void Agregar(MedicoCLS medico)
        {
            var nuevoMedico = new MedicoCLS
            {
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                EspecialidadId = medico.EspecialidadId,
                Telefono = medico.Telefono,
                Email = medico.Email
            };

            _context.Medicos.Add(nuevoMedico);
            _context.SaveChanges();
        }

        public void Actualizar(MedicoCLS medico)
        {
            var medicoExistente = _context.Medicos.Find(medico.Id);

            if (medicoExistente != null)
            {
                medicoExistente.Nombre = medico.Nombre;
                medicoExistente.Apellido = medico.Apellido;
                medicoExistente.EspecialidadId = medico.EspecialidadId;
                medicoExistente.Telefono = medico.Telefono;
                medicoExistente.Email = medico.Email;

                _context.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            var medico = _context.Medicos.Find(id);

            if (medico != null)
            {
                _context.Medicos.Remove(medico);
                _context.SaveChanges();
            }
        }
    }
}
