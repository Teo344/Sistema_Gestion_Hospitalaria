using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class PacientesCLS
    {
        public int IdPaciente { get; set; }

        public string NombrePaciente { get; set; }

        public string ApellidoPaciente { get; set; }

        public DateOnly FechaNacimientoPaciente { get; set; } // DateOnly is a new type in C# 9.0

        public string TelefonoPaciente { get; set; }

        public string EmailPaciente { get; set; }

        public string DireccionPaciente { get; set; }

    }
}
