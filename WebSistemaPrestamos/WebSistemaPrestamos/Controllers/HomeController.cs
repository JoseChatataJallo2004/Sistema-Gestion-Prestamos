using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSistemaPrestamos.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {

        int idrolusuario;
        public ActionResult Index()
        {
            return View();
        }

       
       
    }
}