using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSistemaPrestamos.Models
{
    public class DetallePrestamo
    {
        public int IdPrestamoDetalle { get; set; }
        public int idprestamos { get; set; } 
        public int NroCuota { get; set; }
        public decimal MontoDiario { get; set; } 
        public int Estado { get; set; } 
        public DateTime FechaPagado { get; set; } 
    }
}