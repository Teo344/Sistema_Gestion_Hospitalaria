using CapaEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class EspecialidadDAL
    {
        private readonly HospitalDbContext _context;

        public EspecialidadDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<EspecialidadCLS> ObtenerEspecialidades()
        {
            List<EspecialidadCLS> lista = new List<EspecialidadCLS>();
            lista = _context.Especialidades
                .FromSqlRaw("EXEC uspObtenerEspecialidades")
                .ToList();
            return lista;
        }

        public List<EspecialidadCLS> FiltrarEspecialidad(EspecialidadCLS objEspecialidad)
        {
            List<EspecialidadCLS> lista = new List<EspecialidadCLS>();
            lista = _context.Especialidades
            .FromSqlRaw("EXEC uspFiltrarEspecialidad @p0" , 
                objEspecialidad.Nombre == null ? "" : objEspecialidad.Nombre 
            )
            .ToList();

            return lista;
        }

        public int AgregarEspecialidad(EspecialidadCLS especialidad)
        {
            try
            {
                return _context.Database.ExecuteSqlRaw("EXEC uspInsertarEspecialidad @p0",
                especialidad.Nombre);

            }
            catch (Exception)
            {
                return 0;
            }
        }


        public EspecialidadCLS RecuperarEspecialidad(int id)
        {
            return _context.Especialidades
                .FromSqlRaw("EXEC uspRecuperarEspecialidad @p0", id)
                .AsEnumerable()
                .FirstOrDefault(); // Para obtener un solo objeto en lugar de una lista
        }

        public void ActualizarEspecialidad(EspecialidadCLS especialidad)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC uspActualizarEspecialidad  @p0, @p1",
                    especialidad.Id, especialidad.Nombre);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarEspecialidad(EspecialidadCLS especialidad)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC uspEliminarEspecialidad @p0", especialidad.Id);
            }
            catch (SqlException sqlEx)
            {
                // Capturar específicamente errores SQL
                throw new Exception("Error al intentar eliminar la especialidad. Detalles: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error
                throw new Exception("Ocurrió un error inesperado: " + ex.Message, ex);
            }
        }


    }
}
