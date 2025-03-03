using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaEntidades;

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
                return -1; 
            }

            if (!ValidarEmail(administrador.Email))
            {
                return -2; 
            }

            if (!ValidarClave(administrador.Clave))
            {
                return -3;
            }

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
                return 0; 
            }

            if (!ValidarCampos(administrador))
            {
                return -1; 
            }

            if (!ValidarEmail(administrador.Email))
            {
                return -2; 
            }

            if (!ValidarClave(administrador.Clave))
            {
                return -3; 
            }

            _administradorDAL.ActualizarAdministrador(administrador);
            return 1; 
        }

        public int EliminarAdministrador(AdministradorCLS administrador)
        {
            if (administrador == null || administrador.Id <= 0)
            {
                return 0; 
            }

            _administradorDAL.EliminarAdministrador(administrador);
            return 1; 
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