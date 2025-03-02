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
            try
            {
                return _context.Medicos
                    .FromSqlRaw("EXEC uspObtenerMedicos") // Llamada al procedimiento almacenado
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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

        public void EliminarMedico(MedicoCLS medico)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarMedico @p0", medico.Id);
        }
    }
}
