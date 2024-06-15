using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSistemaPrestamos.Models
{
    public class Prestamo
    {

        public int IdPrestamo { get; set; }
        public int IdUsuarioPrestatario { get; set; }
        public string FechaInicioPago { get; set; }
        public string FechaFinPago { get; set; }
        public decimal MontoPrestamo { get; set; }
        public int nrodias { get; set; }
        public decimal valorpordia { get; set; }
        public int estado {  get; set; }



        ///usuario
        public string nombres { get; set; }
        public string apellidos { get; set; }

        public string correo {  get; set; }

    }
}