using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades;
using BCrypt.Net; 

namespace CapaNegocios
{
    public class AdministradorBL
    {
        private readonly AdministradorDAL _administradorDAL;

        public AdministradorBL(AdministradorDAL administradorDAL)
        {
            _administradorDAL = administradorDAL;
        }

        public List<AdministradorCLS> ObtenerAdministradores()
        {
            return _administradorDAL.ObtenerAdministradores();
        }

        public List<AdministradorCLS> FiltrarAdministrador(AdministradorCLS objAdministrador)
        {
            return _administradorDAL.FiltrarAdministrador(objAdministrador);
        }

        public int AgregarAdministrador(AdministradorCLS administrador)
        {
            if (!ValidarCampos(administrador))
            {
                return -1; // Campos obligatorios no válidos
            }

            if (!ValidarEmail(administrador.Email))
            {
                return -2; // Email no válido
            }

            if (!ValidarClave(administrador.Clave))
            {
                return -3; // Contraseña no válida
            }

            // Hashear la contraseña antes de guardarla
            administrador.Clave = HashClave(administrador.Clave);

            return _administradorDAL.AgregarAdministrador(administrador);
        }

        public AdministradorCLS RecuperarAdministrador(int id)
        {
            return _administradorDAL.RecuperarAdministrador(id);
        }

        public int ActualizarAdministrador(AdministradorCLS administrador)
        {
            if (administrador.Id <= 0)
            {
                return 0; // ID no válido
            }

            if (!ValidarCampos(administrador))
            {
                return -1; // Campos obligatorios no válidos
            }

            if (!ValidarEmail(administrador.Email))
            {
                return -2; // Email no válido
            }

            // No se valida ni actualiza la contraseña aquí
            _administradorDAL.ActualizarAdministrador(administrador);
            return 1; // Actualización exitosa
        }

        public int ActualizarClaveAdministrador(int id, string nuevaClave)
        {
            if (id <= 0)
            {
                return 0; // ID no válido
            }

            if (!ValidarClave(nuevaClave))
            {
                return -3; // Contraseña no válida
            }

            // Hashear la nueva contraseña antes de guardarla
            string claveHasheada = HashClave(nuevaClave);

            _administradorDAL.ActualizarClaveAdministrador(id, claveHasheada);
            return 1; // Actualización de contraseña exitosa
        }

        public int EliminarAdministrador(AdministradorCLS administrador)
        {
            if (administrador == null || administrador.Id <= 0)
            {
                return 0; // Administrador no válido
            }

            _administradorDAL.EliminarAdministrador(administrador);
            return 1; // Eliminación exitosa
        }

        private bool ValidarCampos(AdministradorCLS administrador)
        {
            if (string.IsNullOrWhiteSpace(administrador.Nombre) ||
                string.IsNullOrWhiteSpace(administrador.Apellido))
            {
                return false;
            }
            return true;
        }

        private bool ValidarEmail(string email)
        {
            string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, regex))
            {
                return false;
            }

            return true;
        }

        private bool ValidarClave(string clave)
        {
            // La clave debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número
            string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(clave, regex))
            {
                return false;
            }

            return true;
        }

        private string HashClave(string clave)
        {
            // Hashear la contraseña usando BCrypt
            return BCrypt.Net.BCrypt.HashPassword(clave);
        }

        public bool VerificarClave(string claveIngresada, string claveHasheada)
        {
            return BCrypt.Net.BCrypt.Verify(claveIngresada, claveHasheada);
        }

        public int ContarPacientes()
        {
            return _administradorDAL.ObtenerTotalPacientes();
        }

        public int ContarMedicos()
        {
            return _administradorDAL.ObtenerTotalMedicos();
        }

        public int ContarEspecialidades()
        {
            return _administradorDAL.ObtenerTotalEspecialidades();
        }

        public int ContarCitas()
        {
            return _administradorDAL.ObtenerTotalCitas();
        }

        public float ObtenerIngresoTotal()
        {
            return _administradorDAL.ObtenerIngresoTotal();
        }

        public float ObtenerIngresoMesActual()
        {
            return _administradorDAL.ObtenerIngresoMesActual();
        }
    }
}