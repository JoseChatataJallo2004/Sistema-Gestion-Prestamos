using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Logica;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Negocio
{
    public class UbicacionNegoc
    {
        private UbicacionLogic objcapadato=new UbicacionLogic();

        public List<Departamento> ObtenerDepartamento()
        {
            return objcapadato.ObtenerDepartamento();
        }

        public List<Provincia> ObtenerProvincia(int iddepartamento)
        {
            return objcapadato.ObtenerProvincia(iddepartamento);
        }
        public List<Distrito> ObtenerDistrito(int idprovincia)
        {
            return objcapadato.ObtenerDistrito(idprovincia);
        }

        public List<Documento> ObtenerDocumento()
        {
            return objcapadato.ObtenerDocumento();
        }
    }
}