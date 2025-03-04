using CapaEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


        public int AgregarFacturacion(FacturacionCLS facturacion)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                 "EXEC uspInsertarFacturacion @TratamientoId, @MetodoPago, @Monto, @FechaPago",
                new SqlParameter("@TratamientoId", facturacion.TratamientoId),
                new SqlParameter("@MetodoPago", facturacion.MetodoPago),
                new SqlParameter("@Monto", facturacion.Monto),
                new SqlParameter("@FechaPago", DateTime.Today) // Fecha de pago es hoy
                );

                return 1; // Indicar que la inserción fue exitosa

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar medico: {ex.Message}");
                return 0;
            }


        }



        public List<FacturacionCLS> FiltrarFacturacion(FacturacionCLS objFacturacion)
        {
            var resultado = _context.Facturaciones
                .FromSqlRaw("EXEC uspFiltrarFacturas @p0, @p1",
                    objFacturacion.PacienteIdentificacion == null ? (object)DBNull.Value : objFacturacion.PacienteIdentificacion,
                    objFacturacion.FechaPago == default ? (object)DBNull.Value : objFacturacion.FechaPago
                )
                .ToList();

            return resultado;
        }


        public FacturacionCLS RecuperarFacturacion(int id)
        {
            return _context.Facturaciones
                .FromSqlRaw("EXEC uspRecuperarFactura @p0", id)
                .AsEnumerable()
                .FirstOrDefault(); // Para obtener un solo objeto en lugar de una lista
        }

        public void EliminarFacturacion(FacturacionCLS facturacion)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarFacturacion @p0",facturacion.Id);
        }




    }
}
