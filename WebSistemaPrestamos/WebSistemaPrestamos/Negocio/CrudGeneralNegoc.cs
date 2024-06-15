using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Logica;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Negocio
{
    public class CrudGeneralNegoc
    {
        private CrudGeneralLogic objcapadato=new CrudGeneralLogic();

        public List<Usuario> ListarUsuariosPorPadre(int idPadre)
        {
            return objcapadato.ListarUsuariosPorPadre(idPadre);
        }

    }
}