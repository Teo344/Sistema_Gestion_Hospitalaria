using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class MedicoBL
    {
        private readonly MedicoDAL _medicoDAL;

        public MedicoBL(MedicoDAL medicoDAL)
        {
            _medicoDAL = medicoDAL;
        }

        public List<MedicoCLS> ObtenerMedicos()
        {
            return _medicoDAL.ObtenerMedicos();
        }


        public int AgregarMedico(MedicoCLS medico)
        {
            if (!validarCampos(medico))
            {
                return -1;
            }

            if (!validarCedula(medico.Identificacion))
            {
                return -2;
            }

            if (!validarEmail(medico.Email))
            {
                return -3;
            }

            if (!validarTelefono(medico.Telefono))
            {
                return -4;
            }

            if (!validarEspecialidad(medico.EspecialidadId))
            {
                return -5;
            }

            return _medicoDAL.AgregarMedico(medico);
        }


        public bool validarCampos(MedicoCLS medico)
        {
            if (string.IsNullOrWhiteSpace(medico.Nombre) ||
                string.IsNullOrWhiteSpace(medico.Apellido))
            {
                return false;
            }
            return true;
        }

        public bool validarCedula(string cedula)
        {

            if (cedula.Length != 10 || !cedula.All(char.IsDigit))
            {
                return false;
            }

            int provincia = int.Parse(cedula.Substring(0, 2));
            if (provincia < 0 || provincia > 24)
            {
                return false;
            }

            // Algoritmo de validación del dígito verificador (Módulo 10)
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < coeficientes.Length; i++)
            {
                int digito = int.Parse(cedula.Substring(i, 1));
                int producto = digito * coeficientes[i];

                if (producto >= 10)
                {
                    producto -= 9;
                }

                suma += producto;
            }

            int digitoVerificador = int.Parse(cedula.Substring(9, 1));
            int resultadoEsperado = (suma % 10 == 0) ? 0 : 10 - (suma % 10);

            if (digitoVerificador != resultadoEsperado)
            {
                return false;
            }

            return true;
        }

        public bool validarEmail(string email)
        {
            string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, regex))
            {
                return false;
            }

            return true;
        }

        public bool validarTelefono(string telefono)
        {
            // Expresión regular para validar que el teléfono tenga 10 dígitos
            string regex = @"^09\d{8}$";

            if (string.IsNullOrWhiteSpace(telefono) || !System.Text.RegularExpressions.Regex.IsMatch(telefono, regex))
            {
                return false;
            }

            return true;
        }


        public bool validarEspecialidad(int id)
        {
            if (id <= 0)
            {
                return false;
            }   
            return true;
        }

        public bool validarActivo(int id)
        {
            if (id < 0)
            {
                return false;
            }
            return true;
        }

        public MedicoCLS RecuperarMedico(int id)
        {
            return _medicoDAL.RecuperarMedico(id);
        }


        public int ActualizarMedico(MedicoCLS medico)
        {
            if (medico.Id <= 0)
            {
                return 0;
            }

            if (!validarCampos(medico))
            {
                return -1;
            }

            if (!validarCedula(medico.Identificacion))
            {
                return -2;
            }

            if (!validarEmail(medico.Email))
            {
                return -3;
            }

            if (!validarTelefono(medico.Telefono))
            {
                return -4;
            }

            if (!validarEspecialidad(medico.EspecialidadId))
            {
                return -5;
            }

            _medicoDAL.ActualizarMedico(medico);
            return 1;
        }





        public List<MedicoCLS> FiltrarMedico(MedicoCLS objMedico)
        {
            return _medicoDAL.FiltrarMedico(objMedico);
        }





        public int EliminarMedico(MedicoCLS medico)
        {
            if (medico.Id <= 0)
            {
                return 0;
            }

            try
            {
                _medicoDAL.EliminarMedico(medico);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el medico: " + ex.Message);
            }
        }





    }
}
