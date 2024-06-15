using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSistemaPrestamos.Models;
using WebSistemaPrestamos.Negocio;



//Controlador de Prestamo
namespace WebSistemaPrestamos.Controllers
{
    [Authorize]
    public class PrestamistaController : Controller
    {
        // GET: Prestamista
        public ActionResult Index()
        {
            int idup = (int)Session["IdSessionPadre"];
            ViewBag.IdUsuarioPadre = idup;
            return View();
        }


        public ActionResult AprobarPrestamos()
        {
          
            return View();
        }




        [HttpGet]
        public JsonResult ListarUsuarioGeneral(int idpadre)
        {
            List<Usuario> olista = new List<Usuario>();
            olista = new CrudGeneralNegoc().ListarUsuariosPorPadre(idpadre);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult guardarusuariogeneral(Usuario obj)
        {
            object resultado = null;
            string mensaje = string.Empty;
            //Registrar
            if (obj.idusuario == 0)
            {
                resultado = new UsuarioNegoc().registrarusuariogenerl(obj, out mensaje);
            }
            //Editar
            else
            {
                resultado = new UsuarioNegoc().EditarUsuarioGEneral(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult ListarPrestamosGeneralPrestamista(int idusuariopadre)
        {
            List<Prestamo> olista = new List<Prestamo>();
            olista = new PrestamosNegoc().ListarPrestamosPrestamista(idusuariopadre);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }

    }
}