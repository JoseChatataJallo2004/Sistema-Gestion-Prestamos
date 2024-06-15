using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebSistemaPrestamos.Logica
{
    public class Conexion
    {
        public static string cadenasistemas = ConfigurationManager.ConnectionStrings["SistemaCadena"].ToString();

    }
}