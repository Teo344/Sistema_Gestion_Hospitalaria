using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class FacturacionBL
    {

        private readonly FacturacionDAL _facturacionDAL;

        public FacturacionBL(FacturacionDAL facturacionDAL)
        {
            _facturacionDAL = facturacionDAL;
        }

        public List<FacturacionCLS> ObtenerFacturaciones()
        {
            return _facturacionDAL.ObtenerFacturaciones();
        }


        public List<FacturacionCLS> FiltrarFacturacion(FacturacionCLS objFacturacion)
        {
            return _facturacionDAL.FiltrarFacturacion(objFacturacion);
        }


        public int AgregarFacturacion(FacturacionCLS facturacion)
        {
            if (!validarCampos(facturacion))
            {
                return -1;
            }

            if (!validarTratamientos(facturacion.TratamientoId))
            {
                return -2;
            }


            return _facturacionDAL.AgregarFacturacion(facturacion);
        }
      

        public bool validarCampos(FacturacionCLS facturacion)
        {

            if (string.IsNullOrWhiteSpace(facturacion.PacienteIdentificacion) ||
                string.IsNullOrWhiteSpace(facturacion.MetodoPago)
                
                )
            {
                return false;
            }
            return true;
        }

        public bool validarTratamientos(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            return true;
        }


        public FacturacionCLS RecuperarFacturacion(int id)
        {
            return _facturacionDAL.RecuperarFacturacion(id);
        }

        public int EliminarFacturacion(FacturacionCLS facturacion)
        {
            if (facturacion.Id <= 0)
            {
                return 0;
            }

            try
            {
                _facturacionDAL.EliminarFacturacion(facturacion);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la factura: " + ex.Message);
            }
        }



    }
}
