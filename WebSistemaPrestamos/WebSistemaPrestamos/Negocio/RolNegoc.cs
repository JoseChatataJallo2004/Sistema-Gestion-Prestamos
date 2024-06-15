using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Logica;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Negocio
{
    public class RolNegoc
    {


        private RolLogic objcapaDato = new RolLogic();

        public List<Rol> listar()
        {
            return objcapaDato.listar();
        }
    }
}