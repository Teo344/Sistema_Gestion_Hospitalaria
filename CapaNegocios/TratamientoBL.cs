using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class TratamientoBL
    {
        private readonly TratamientoDAL _tratamientoDAL;

        public TratamientoBL(TratamientoDAL tratamientoDAL)
        {
            _tratamientoDAL = tratamientoDAL;
        }

        public List<TratamientoCLS> ObtenerTratamientos()
        {
            return _tratamientoDAL.ObtenerTratamientos();
        }

        public List<TratamientoCLS> FiltrarTratamientos(TratamientoCLS objTratamiento)
        {
            return _tratamientoDAL.FiltrarTratamientos(objTratamiento);
        }

        public int AgregarTratamiento(TratamientoCLS tratamiento)
        {
            if (!ValidarCampos(tratamiento))
            {
                return -1; 
            }

            if (!ValidarCosto(tratamiento.Costo))
            {
                return -2; 
            }

            if (!ValidarFecha(tratamiento.Fecha))
            {
                return -3; 
            }

            try
            {
                return _tratamientoDAL.AgregarTratamiento(tratamiento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el tratamiento: " + ex.Message);
            }
        }

        public TratamientoCLS RecuperarTratamiento(int id)
        {
            return _tratamientoDAL.RecuperarTratamiento(id);
        }

        public int ActualizarTratamiento(TratamientoCLS tratamiento)
        {
            if (tratamiento.Id <= 0)
            {
                return 0; 
            }

            if (!ValidarCampos(tratamiento))
            {
                return -1; 
            }

            if (!ValidarCosto(tratamiento.Costo))
            {
                return -2; 
            }

            if (!ValidarFecha(tratamiento.Fecha))
            {
                return -3; 
            }

            try
            {
                _tratamientoDAL.ActualizarTratamiento(tratamiento);
                return 1; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el tratamiento: " + ex.Message);
            }
        }

        public int EliminarTratamiento(TratamientoCLS tratamiento)
        {
            if (tratamiento.Id <= 0)
            {
                return 0; 
            }

            try
            {
                _tratamientoDAL.EliminarTratamiento(tratamiento);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el tratamiento: " + ex.Message);
            }
        }

        private bool ValidarCampos(TratamientoCLS tratamiento)
        {
            if (string.IsNullOrWhiteSpace(tratamiento.Descripcion))
            {
                return false;
            }
            return true;
        }

        private bool ValidarCosto(decimal costo)
        {
            return costo > 0;
        }

        public bool ValidarFecha(DateOnly fechaN)
        {
            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);

            if (fechaN > fechaActual)
            {
                return false;
            }

            return true;
        }


        public List<TratamientoCLS> ObtenerTratamientosPorPaciente(string identificacion)
        {
            return _tratamientoDAL.ObtenerTratamientosPorPaciente(identificacion);
        }


    }
}