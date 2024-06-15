using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSistemaPrestamos.Models
{
    public class Usuario
    {


        public int idusuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public Documento odocumento { get; set; }
        public string docume {  get; set; }
        public Departamento odepartametno { get; set; }
        public Provincia oprovincia { get; set; }
        public Distrito odistrito { get; set; }

        public string correo { get; set; }
        public string clave { get; set; }
        public string confirmarclave { get; set; }

        public Rol orol { get; set; }


        public Usuario ousuario { get; set; }   

        public bool reestablecer { get; set; }

        //ADICIONALES
        public int idddrol { get; set; }
        public int idpadrevista { get; set; }
    }
}