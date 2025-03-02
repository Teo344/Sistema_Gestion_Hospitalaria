using System.Collections.Generic;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class PacienteBL
    {
        private readonly PacienteDAL _pacienteDAL;

        public PacienteBL(PacienteDAL pacienteDAL)
        {
            _pacienteDAL = pacienteDAL;
        }

        public List<PacienteCLS> ObtenerPacientes()
        {
            return _pacienteDAL.ObtenerPacientes();
        }

        public List<PacienteCLS> FiltrarPaciente(PacienteCLS objPaciente)
        {
            return _pacienteDAL.FiltrarPaciente(objPaciente);
        }

        public int AgregarPaciente(PacienteCLS paciente)
        {
            if (!validarCampos(paciente))
            {
                return -1; 
            }

            if (!validarCedula(paciente.Identificacion))
            {
                return -2; 
            }

            if (!validarEmail(paciente.Email))
            {
                return -3; 
            }

            if (!validarTelefono(paciente.Telefono))
            {
                return -4; 
            }

            if (!validarFechaNacimiento(paciente.FechaNacimiento))
            {
                return -5;
            }

            return _pacienteDAL.AgregarPaciente(paciente);
        }

        public PacienteCLS RecuperarPaciente(int id)
        {
            return _pacienteDAL.RecuperarPaciente(id);
        }

        public int ActualizarPaciente(PacienteCLS paciente)
        {
            if (paciente.Id <= 0)
            {
                return 0; 
            }
            if (!validarCampos(paciente))
            {
                return -1; 
            }

            if (!validarCedula(paciente.Identificacion))
            {
                return -2; 
            }

            if (!validarEmail(paciente.Email))
            {
                return -3;
            }

            if (!validarTelefono(paciente.Telefono))
            {
                return -4; 
            }

            if (!validarFechaNacimiento(paciente.FechaNacimiento))
            {
                return -5; 
            }

            _pacienteDAL.ActualizarPaciente(paciente);
            return 1; 
        }
        public int EliminarPaciente(PacienteCLS paciente)
        {
            if (paciente == null || paciente.Id <= 0)
            {
                return 0; // Retorna 0 si el paciente es nulo o tiene un id no válido.
            }

            _pacienteDAL.EliminarPaciente(paciente);
            return 1; // Retorna 1 si la eliminación fue exitosa.
        }

        public bool validarCampos(PacienteCLS paciente)
        {
            if (string.IsNullOrWhiteSpace(paciente.Nombre) ||
                string.IsNullOrWhiteSpace(paciente.Apellido) ||
                string.IsNullOrWhiteSpace(paciente.Direccion))
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

        public bool validarFechaNacimiento(DateOnly fechaNacimiento)
        {
            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);

            if (fechaNacimiento > fechaActual)
            {
                return false; 
            }

            // Verificar que la fecha de nacimiento no sea de más de 150 años atrás
            DateOnly fechaMinima = fechaActual.AddYears(-150);
            if (fechaNacimiento < fechaMinima)
            {
                return false; 
            }

            return true;
        }
    }
}