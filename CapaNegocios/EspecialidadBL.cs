using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class EspecialidadBL
    {
        private readonly EspecialidadDAL _especialidadDAL;

        public EspecialidadBL(EspecialidadDAL especialidadDAL)
        {
            _especialidadDAL = especialidadDAL;
        }

        public List<EspecialidadCLS> ObtenerEspecialidades()
        {
            return _especialidadDAL.ObtenerEspecialidades();
        }

        public List<EspecialidadCLS> FiltrarEspecialidad(EspecialidadCLS objEspecialidad)
        {
            return _especialidadDAL.FiltrarEspecialidad(objEspecialidad);
        }

        public int AgregarEspecialidad(EspecialidadCLS especialidad)
        {
            if (!validarCampos(especialidad))
            {
                return -1;
            }

            if (!validarNombre(especialidad.Nombre))
            {
                return -2;
            }

            return _especialidadDAL.AgregarEspecialidad(especialidad);
        }

        public EspecialidadCLS RecuperarEspecialidad(int id)
        {
            return _especialidadDAL.RecuperarEspecialidad(id);
        }

        public int ActualizarEspecialidad(EspecialidadCLS especialidad)
        {
            if (especialidad.Id <= 0)
            {
                return 0;
            }
            if (!validarCampos(especialidad))
            {
                return -1;
            }

            if (!validarNombre(especialidad.Nombre))
            {
                return -2;
            }


            _especialidadDAL.ActualizarEspecialidad(especialidad);
            return 1; // Retorna 1 si la actualización fue exitosa
        }
        public int EliminarEspecialidad(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            _especialidadDAL.EliminarEspecialidad(id);
            return 1;
        }

        public bool validarCampos(EspecialidadCLS especialidad)
        {
            if (string.IsNullOrWhiteSpace(especialidad.Nombre))
            {
                return false;
            }
            return true;
        }


        public bool validarNombre(string especialidad)
        {
            // Expresión regular que permite letras, espacios, guiones y puntos
            string regex = @"^[a-zA-ZáéíóúÁÉÍÓÚ\s\.\-]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(especialidad, regex)) //Explicar mucho mejor esto en la documentacion
            {
                return false;
            }

            return true;
        }
    }





}
