using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class FacturacionCLS
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }

        public string PacienteApellido { get; set; }

        public string PacienteIdentificacion { get; set; }

        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public DateOnly FechaPago { get; set; }

        public int TratamientoId { get; set; }

        // Relación con Paciente
        public PacienteCLS Paciente { get; set; }

        public TratamientoCLS Tratamiento { get; set; }

        public decimal TratamientoCosto { get; set; }
    }
}
