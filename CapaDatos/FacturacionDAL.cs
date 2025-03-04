using CapaEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class FacturacionDAL
    {

        private readonly HospitalDbContext _context;

        public FacturacionDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<FacturacionCLS> ObtenerFacturaciones()
        {
            try
            {
                return _context.Facturaciones
                    .FromSqlRaw("EXEC ObtenerFacturaciones") // Llamada al procedimiento almacenado
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




    }
}
