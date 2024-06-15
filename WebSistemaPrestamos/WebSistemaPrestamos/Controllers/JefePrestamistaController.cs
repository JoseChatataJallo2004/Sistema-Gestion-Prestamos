using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSistemaPrestamos.Models;
using WebSistemaPrestamos.Negocio;


//Controlador de Jefe de Prestamista
namespace WebSistemaPrestamos.Controllers
{
    [Authorize]
    public class JefePrestamistaController : Controller
    {
        // GET: JefePrestamista
        public ActionResult Index()
        {
            if (Session["IdSessionPadre"] != null)
            {
                int idup = (int)Session["IdSessionPadre"];
                ViewBag.IdUsuarioPadre = idup;
                return View();
            }
            else
            {
                // Manejar el caso en el que Session["IdSessionPadre"] es null
                // Puede redirigir a otra página o realizar alguna otra acción apropiada.
                return RedirectToAction("PaginaDeError");
            }
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
    }
}
