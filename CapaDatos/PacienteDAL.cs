﻿using System.Collections.Generic;
using CapaEntidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


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
            List<PacienteCLS> lista = new List<PacienteCLS>();
            lista = _context.Pacientes
                .FromSqlRaw("EXEC uspObtenerPacientes")
                .ToList();
            return lista;
        }

        public List<PacienteCLS> FiltrarPaciente(PacienteCLS objPaciente)
        {
            List<PacienteCLS> lista = new List<PacienteCLS>();
            lista = _context.Pacientes
                .FromSqlRaw("EXEC uspFiltrarPaciente @p0, @p1, @p2, @p3, @p4, @p5",
                    objPaciente.Nombre == null ? "" : objPaciente.Nombre,
                    objPaciente.Apellido == null ? "" : objPaciente.Apellido,
                    objPaciente.Email == null ? "" : objPaciente.Email,
                    objPaciente.Telefono == null ? "" : objPaciente.Telefono,
                    objPaciente.FechaNacimiento == default ? (object)DBNull.Value : objPaciente.FechaNacimiento,
                    objPaciente.Direccion == null ? "" : objPaciente.Direccion)
                .ToList();
            return lista;
        }



        public int AgregarPaciente(PacienteCLS paciente)
        {
            try
            {
                return _context.Database.ExecuteSqlRaw("EXEC uspInsertarPaciente @p0, @p1, @p2, @p3, @p4, @p5",
                    paciente.Nombre, paciente.Apellido, paciente.FechaNacimiento,
                    paciente.Telefono, paciente.Email, paciente.Direccion);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public void Actualizar(PacienteCLS paciente)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspActualizarPaciente @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                paciente.Id, paciente.Nombre, paciente.Apellido, paciente.FechaNacimiento,
                paciente.Telefono, paciente.Email, paciente.Direccion);
        }

        public void Eliminar(int id)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarPaciente @p0", id);
        }
    }
}
