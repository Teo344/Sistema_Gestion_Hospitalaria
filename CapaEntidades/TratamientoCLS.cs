using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class TratamientoCLS
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }

        // Relación con Paciente
        public PacienteCLS Paciente { get; set; }
    }
}
