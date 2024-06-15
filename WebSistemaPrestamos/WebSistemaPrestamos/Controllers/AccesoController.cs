using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSistemaPrestamos.Models;


using WebSistemaPrestamos.Negocio;

namespace WebSistemaPrestamos.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult IniciarSistema()
        {
            return View();
        }


        public ActionResult RegistrarUsuario()
        {
            return View();
        }


        public ActionResult RegistrarPrestatarioVista()
        {
            return View();
        }



        public ActionResult Reestablecer()
        {
            return View();

        }
       
        public ActionResult Cambioclave()
        {
            return View();

        }


      //  [Authorize]
        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            return RedirectToAction("IniciarSistema", "Acceso");
        }




        //[Authorize]
        [HttpPost]
        public ActionResult IniciarSistema(string correo, string clave)
        {
            Usuario ousuarioa = null;
            ousuarioa = new UsuarioNegoc().IngresarAlSistema().Where(item => item.correo == correo && item.clave == Recursos.externos.convertirsha256(clave)).FirstOrDefault();
            if (ousuarioa == null)
            {
                ViewBag.error = "El Correo o la Contraseña no son correctas";
                return View();
            }
            else
            {
                if (ousuarioa.reestablecer ==false)
                {
                    TempData["idusuario"] = ousuarioa.idusuario;
                    return RedirectToAction("Cambioclave", "Acceso");
                }
                else
                {

                    FormsAuthentication.SetAuthCookie(ousuarioa.correo, false);

                    Session["Usuario"] = ousuarioa;
                    Session["IdSessionPadre"] = ousuarioa.idusuario;


                   // Session["IdPadrePrestamo"] = ousuarioa.ousuario.idusuario;
                    ViewBag.error = null;
                    return RedirectToAction("Index", "Home");
                }



            }

        }



     


        [HttpPost]
        public JsonResult Registrarprestatario( Usuario obj)
        {
            object resultado=null;
            string mensaje = string.Empty;


            if (obj.idusuario == 0)
            {
                resultado =new UsuarioNegoc().registrarprestatariovista (obj, out mensaje);
            }
           // else
            //{
              //  resultado = trabajorescn.ActualizarTrabajador(obj, out mensaje);
            //}

            return Json(new { resultado = resultado, mensaje = mensaje },JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {

            Usuario ousuario = new Usuario();
            ousuario = new UsuarioNegoc().IngresarAlSistema().Where(item => item.correo == correo).FirstOrDefault();
            if (ousuario == null)
            {
                ViewBag.error = "No se encontro un Usuario relacionado a ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new UsuarioNegoc().reestablecerclave(ousuario.idusuario, correo, out mensaje);
            if (respuesta)
            {
                ViewBag.error = null;
                return RedirectToAction("IniciarSistema");
            }
            else
            {
                ViewBag.error = mensaje;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Cambioclave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            Usuario ousuario = new Usuario();
            ousuario = new UsuarioNegoc().IngresarAlSistema().Where(c => c.idusuario == int.Parse(idusuario)).FirstOrDefault();
            if (ousuario.clave != Recursos.externos.convertirsha256(claveactual))
            {
                TempData["idusuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.error = "la contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["idusuario"] = idusuario;
                //vclave es para cuando las contraseñas nuevas no coinciden la contraseña actual no se borre
                ViewData["vclave"] = claveactual;
                ViewBag.error = "las contraseñas nuevas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = Recursos.externos.convertirsha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new UsuarioNegoc().cambiarclave(int.Parse(idusuario), nuevaclave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("IniciarSistema", "Acceso");
            }
            else
            {
                TempData["idusuario"] = idusuario;
                ViewBag.error = mensaje;
                return View();
            }
        }



        #region ubicaciones en json
        //obtener tipo de documento
        [HttpGet]
        public JsonResult ListarDocumento()
        {
            List<Documento> olista = new List<Documento>();
            olista = new UbicacionNegoc().ObtenerDocumento();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult ListarPrestamista()
        {
            List<Usuario> olista = new List<Usuario>();
            olista = new UsuarioNegoc().Listarprestamista();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerDepartamento()
        {
            List<Departamento> olista = new List<Departamento>();
            olista = new UbicacionNegoc().ObtenerDepartamento();
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ObtenerProvincias(int iddepartamento)
        {
            List<Provincia> olista = new List<Provincia>();
            olista = new UbicacionNegoc().ObtenerProvincia(iddepartamento);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ObtenerDistritos(int idprovincia)
        {
            List<Distrito> olista = new List<Distrito>();
            olista = new UbicacionNegoc().ObtenerDistrito(idprovincia);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
}