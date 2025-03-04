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



    }
}
