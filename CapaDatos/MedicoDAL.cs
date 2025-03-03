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

        public List<MedicoCLS> FiltrarMedico(MedicoCLS objMedico)
        {

            Console.WriteLine($"Nombre: {objMedico.Nombre}");
            Console.WriteLine($"Apellido: {objMedico.Apellido}");
            Console.WriteLine($"Identificación: {objMedico.Identificacion}");
            Console.WriteLine($"EspecialidadId: {objMedico.EspecialidadId}");
            Console.WriteLine($"Activo: {objMedico.Activo}");


            List<MedicoCLS> lista = new List<MedicoCLS>();
            lista = _context.Medicos
            .FromSqlRaw("EXEC uspFiltrarMedico @p0, @p1, @p2, @p3, @p4",
                objMedico.Nombre == null ? "" : objMedico.Nombre,
                objMedico.Apellido == null ? "" : objMedico.Apellido,
                objMedico.Identificacion == null ? "" : objMedico.Identificacion,
                objMedico.EspecialidadId == -1 ? "" : objMedico.EspecialidadId,
                objMedico.Activo == null ? "" : objMedico.Activo
            )
            .ToList();



            return lista;
        }



        public int AgregarMedico(MedicoCLS medico)
        {
            try
            {
                return _context.Database.ExecuteSqlRaw("EXEC uspInsertarMedico @p0, @p1, @p2, @p3, @p4, @p5, @p6",
    medico.Nombre, medico.Apellido, medico.EspecialidadId, medico.Telefono, medico.Email, medico.Identificacion, true);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar medico: {ex.Message}");
                return 0;
            }
        }

        public MedicoCLS RecuperarMedico(int id)
        {
            return _context.Medicos
                .FromSqlRaw("EXEC uspRecuperarMedico @p0", id)
                .AsEnumerable()
                .FirstOrDefault(); // Para obtener un solo objeto en lugar de una lista
        }


        public void ActualizarMedico(MedicoCLS medico)
        {


            try
            {
                // Ejecuto el stored procedure uspActualizarMedico con los parámetros adecuados
                _context.Database.ExecuteSqlRaw("EXEC uspActualizarMedico @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7",
                    medico.Id,              
                    medico.Nombre,           
                    medico.Apellido,          
                    medico.EspecialidadId,    
                    medico.Telefono,          
                    medico.Email,            
                    medico.Identificacion,  
                    medico.Activo);         
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el médico: " + ex.Message, ex);
            }
        }







        public void EliminarMedico(MedicoCLS medico)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarMedico @p0", medico.Id);
        }
    }
}
