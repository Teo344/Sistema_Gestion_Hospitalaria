using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class PacientesCLS
    {
        public int idPaciente { get; set; }

        public string nombrePaciente { get; set; }

        public string apellidoPaciente { get; set; }

        public DateOnly fechaNacimientoPaciente { get; set; } // DateOnly is a new type in C# 9.0

        public string telefonoPaciente { get; set; }

        public string emailPaciente { get; set; }

        public string direccionPaciente { get; set; }

    }
}
