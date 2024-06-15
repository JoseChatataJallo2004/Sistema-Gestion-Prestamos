using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Logica;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Negocio
{
    public class DetallePrestamosNegoc
    {
        private DetallePrestamosLogic objcapa=new DetallePrestamosLogic();

        public List<DetallePrestamo> ListardetallePrestamos(int idprestamo)
        {
            return objcapa.ListardetallePrestamos(idprestamo);
        }


    }
}