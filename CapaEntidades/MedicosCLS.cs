using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class MedicosCLS
    {
        [Key]
        public int IdMedico { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreMedico { get; set; }

        [Required]
        [StringLength(100)]
        public string ApellidoMedico { get; set; }

        // Clave foránea para Especialidad
        [ForeignKey("Especialidad")]
        public int EspecialidadId { get; set; }

        public virtual EspecialidadesCLS Especialidad { get; set; }

        [Required]
        [StringLength(15)]
        public string TelefonoMedico { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailMedico { get; set; }
    }
}
