using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class CitaCLS
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }

        // Relaciones
        public PacienteCLS Paciente { get; set; }
        public MedicoCLS Medico { get; set; }
    }
}
