﻿using System;
using System.Collections.Generic;
using CapaEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos
{
    public class AdministradorDAL
    {
        private readonly HospitalDbContext _context;

        public AdministradorDAL(HospitalDbContext context)
        {
            _context = context;
        }

        public List<AdministradorCLS> ObtenerAdministradores()
        {
            List<AdministradorCLS> lista = new List<AdministradorCLS>();
            lista = _context.Administradores
                .FromSqlRaw("EXEC uspObtenerAdministradores")
                .ToList();
            return lista;
        }

        public List<AdministradorCLS> FiltrarAdministrador(AdministradorCLS objAdministrador)
        {
            List<AdministradorCLS> lista = new List<AdministradorCLS>();
            lista = _context.Administradores
                .FromSqlRaw("EXEC uspFiltrarAdministrador @p0, @p1, @p2",
                    objAdministrador.Nombre == null ? "" : objAdministrador.Nombre,
                    objAdministrador.Apellido == null ? "" : objAdministrador.Apellido,
                    objAdministrador.Email == null ? "" : objAdministrador.Email
                )
                .ToList();

            return lista;
        }

        public int AgregarAdministrador(AdministradorCLS administrador)
        {
            try
            {
                return _context.Database.ExecuteSqlRaw("EXEC uspInsertarAdministrador @p0, @p1, @p2, @p3",
                    administrador.Nombre, administrador.Apellido, administrador.Clave, administrador.Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar administrador: {ex.Message}");
                return 0;
            }
        }

        public AdministradorCLS RecuperarAdministrador(int id)
        {
            return _context.Administradores
                .FromSqlRaw("EXEC uspRecuperarAdministrador @p0", id)
                .AsEnumerable()
                .FirstOrDefault(); // Para obtener un solo objeto en lugar de una lista
        }

        public void ActualizarAdministrador(AdministradorCLS administrador)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC uspActualizarAdministrador @p0, @p1, @p2, @p3, @p4",
                    administrador.Id, administrador.Nombre, administrador.Apellido, administrador.Clave, administrador.Email);
            }
            catch (Exception ex)
            {
                throw; // Relanzar la excepción para manejarla en la capa superior
            }
        }

        public void EliminarAdministrador(AdministradorCLS administrador)
        {
            _context.Database.ExecuteSqlRaw("EXEC uspEliminarAdministrador @p0", administrador.Id);
        }
    }
}