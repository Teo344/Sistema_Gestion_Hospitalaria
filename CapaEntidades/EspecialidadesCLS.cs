using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class EspecialidadesCLS
    {
        [Key]
        public int IdEspecialidad { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreEspecialidad { get; set; }

        //Propiedad de navegación para la relación con Médicos
        public virtual ICollection<MedicosCLS> Medicos { get; set; } = new List<MedicosCLS>(); // Define la relación inversa uno a muchos con la clase MedicosCLS.

    }
}
