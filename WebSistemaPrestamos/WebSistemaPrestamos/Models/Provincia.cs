using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSistemaPrestamos.Models
{
    public class Provincia
    {
        public int IdProvincia { get; set; }
        public Departamento odepartamento { get; set; }
        public string NombreProvincia { get; set; }
    }
}