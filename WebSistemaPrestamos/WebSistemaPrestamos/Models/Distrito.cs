using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSistemaPrestamos.Models
{
    public class Distrito
    {
        public int IdDistrito { get; set; }
        public Provincia oprovincia { get; set; }
        public string nombredistrito { get; set; }
    }
}