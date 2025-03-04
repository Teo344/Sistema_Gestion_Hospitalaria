using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class MedicoCLS
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int EspecialidadId { get; set; }
        public string Identificacion { get; set; }

        public string Telefono { get; set; }
        public string Email { get; set; }

        public string Identificacion { get; set; }


        public string NombreEspecialidad { get; set; }
        public bool Activo { get; set; }

        public string Disponibilidad { get; set; }

        // Relación con Especialidad
        public EspecialidadCLS Especialidad { get; set; }
    }
}
