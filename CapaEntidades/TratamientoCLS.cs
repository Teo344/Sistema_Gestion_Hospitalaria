using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

public class TratamientoCLS
{
    public int Id { get; set; } // Id_Tratamiento
    public int PacienteId { get; set; } // Id_Paciente
    public string IdentificacionPaciente { get; set; } // Identificacion_Paciente
    public string Descripcion { get; set; } // Descripcion
    public DateOnly Fecha { get; set; } // Fecha
    public decimal Costo { get; set; } // Costo

    // Relación con Paciente
    public PacienteCLS Paciente { get; set; }
}
