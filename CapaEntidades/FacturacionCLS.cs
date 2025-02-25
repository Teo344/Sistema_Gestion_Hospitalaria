﻿using System;
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
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public DateOnly FechaPago { get; set; }

        // Relación con Paciente
        public PacienteCLS Paciente { get; set; }
    }
}
