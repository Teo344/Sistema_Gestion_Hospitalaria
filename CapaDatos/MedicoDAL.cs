using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<MedicoCLS> ObtenerTodos()
        {
            return _context.Medicos.Include(m => m.Especialidad).ToList();
        }

        public MedicoCLS ObtenerPorId(int id)
        {
            return _context.Medicos.Include(m => m.Especialidad)
                                   .FirstOrDefault(m => m.Id == id);
        }

        public void Agregar(MedicoCLS medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
        }

        public void Actualizar(MedicoCLS medico)
        {
            _context.Medicos.Update(medico);
            _context.SaveChanges();
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
