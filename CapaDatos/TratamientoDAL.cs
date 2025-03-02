using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos
{
    public class TratamientoDAL
    {
        private readonly HospitalDbContext _context;

        public TratamientoDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<TratamientoCLS> ObtenerTratamientos()
        {
            try
            {
                return _context.Tratamientos
                    .FromSqlRaw("EXEC uspObtenerTratamientos")
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                throw; // Re-throw the exception to see it in the browser
            }
        }

        public List<TratamientoCLS> FiltrarTratamientos(TratamientoCLS objTratamiento)
        {
            List<TratamientoCLS> lista = new List<TratamientoCLS>();

            lista = _context.Tratamientos
                .FromSqlRaw("EXEC uspFiltrarTratamientos @p0, @p1, @p2, @p3",
                    objTratamiento.IdentificacionPaciente == null ? "" : objTratamiento.IdentificacionPaciente,
                    objTratamiento.Descripcion == null ? "" : objTratamiento.Descripcion,
                    objTratamiento.Fecha == default ? (object)DBNull.Value : objTratamiento.Fecha,
                    objTratamiento.Costo == default ? (object)DBNull.Value : objTratamiento.Costo
                )
                .ToList();

            return lista;
        }


        public int AgregarTratamiento(TratamientoCLS tratamiento)
        {
            try
            {
                return _context.Database.ExecuteSqlRaw("EXEC uspInsertarTratamiento @p0, @p1, @p2, @p3",
                    tratamiento.IdentificacionPaciente,
                    tratamiento.Descripcion,
                    tratamiento.Fecha,
                    tratamiento.Costo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar tratamiento: {ex.Message}");
                return 0;
            }
        }

        public TratamientoCLS RecuperarTratamiento(int id)
        {
            return _context.Tratamientos
                .FromSqlRaw("EXEC uspRecuperarTratamiento @p0", id)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public void ActualizarTratamiento(TratamientoCLS tratamiento)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC uspActualizarTratamiento @p0, @p1, @p2, @p3, @p4",
                    tratamiento.Id,
                    tratamiento.IdentificacionPaciente,
                    tratamiento.Descripcion,
                    tratamiento.Fecha,
                    tratamiento.Costo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarTratamiento(TratamientoCLS tratamiento)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarTratamiento @p0", tratamiento.Id);
        }
    }
}
