using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSistemaPrestamos.Models;
using WebSistemaPrestamos.Negocio;

//referencias para la api de paypal
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
//Models de payapl
using WebSistemaPrestamos.Models.Paypal_Order;
using WebSistemaPrestamos.Models.Paypal_Transaction;

namespace WebSistemaPrestamos.Controllers
{

    [Authorize]
    public class PrestatarioController : Controller
    {
        // GET: Prestatario
        public ActionResult Index()
        {

            int idup = (int)Session["IdSessionPadre"];
            ViewBag.IdUsuarioPadre = idup;

            //int idprestageneral = (int)Session["IdSessionPrestamista"];
            //ViewBag.IdPrestamiPadre= idprestageneral;

            return View();
        }

        public async Task<ActionResult> Abunt()
        {
            string token = Request.QueryString["token"];

            //  ViewData["IdTransaccion"] = "hola soy el jose data";
            //ViewData["Status"] = true;

            bool status = false;
              // Configurar las credenciales de autenticación de PayPal
                var userName = "AceDg6yf6zlibcKhUCnXzhpKZ78r_CpkfuCSRJJQ08uQGj8KIJcIwY-yHYIgBX9u2YILhGDRJCr_z9vu";
                var password = "EBE03zsi9ey0KQvX9SBPui-PKjeD2eTVHK418QZL6ut8dMxTD8EysI61xcjS91tZITim0KFec3AwJbqe";


                // Configurar la solicitud HTTP a la API de PayPal
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
                    var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var data = new StringContent("{}", Encoding.UTF8, "application/json");


                    // Realizar la solicitud POST a la API de PayPal
                    HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);

                    status = response.IsSuccessStatusCode;

                    if (status)
                    {
                    var jsonresult=response.Content.ReadAsStringAsync().Result;

                        Paypal_transaction objeto=JsonConvert.DeserializeObject<Paypal_transaction>(jsonresult);
                      //  ViewData["IdTransaccion"] = objeto.payment_source.paypal;
                    }
                    
                }
            
            

            return View();
        }


        public ActionResult Vermisprestamos()
        {     
            return View();
        }

        [HttpPost]
        public JsonResult guardarregistroprestamos(Prestamo obj)
        {
            object resultado = null;
            string mensaje=string.Empty;
            resultado=new PrestamosNegoc().RegistrarPrestamos(obj, out mensaje);
            return Json(new { resultado = resultado , mensaje=mensaje}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPrestamosGeneral(int idusuario)
        {
            List<Prestamo> olista = new List<Prestamo>();
            olista = new PrestamosNegoc().ListarPrestamosPrestatario(idusuario);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarDetallePrestamosGeneral(int idprestamo)
        {
            List<DetallePrestamo> olista = new List<DetallePrestamo>();
            olista = new DetallePrestamosNegoc().ListardetallePrestamos(idprestamo);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }




        //[HttpPost]
        //public async Task<JsonResult> Paypal(string precio , string ncuota)
        //{
        //    bool status = false;
        //    string respuesta = string.Empty;

        //    using (var client=new HttpClient())
        //    {
        //        var userName = "AceDg6yf6zlibcKhUCnXzhpKZ78r_CpkfuCSRJJQ08uQGj8KIJcIwY-yHYIgBX9u2YILhGDRJCr_z9vu";
        //        var password = "EBE03zsi9ey0KQvX9SBPui-PKjeD2eTVHK418QZL6ut8dMxTD8EysI61xcjS91tZITim0KFec3AwJbqe";

        //        client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

        //        var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));


        //        var orden = new PaypalOrder()
        //        {
        //            intent = "CAPTURE",
        //            purchase_units = new List<Models.Paypal_Order.PurchaseUnit>()
        //            {
        //                new Models.Paypal_Order.PurchaseUnit()
        //                {
        //                    items = new List<Models.Paypal_Order.Item>()
        //                    {
        //                        new Models.Paypal_Order.Item(){
        //                            name = "Numero de cuota " + ncuota,
        //                            description = "Sistema de Prestamo Grupo 8A Cibertec",
        //                            quantity = "1",
        //                            unit_amount = new Models.Paypal_Order.UnitAmount()
        //                            {
        //                                currency_code = "USD",
        //                                value = precio
        //                            }
        //                        }
        //                    }
        //                },
        //                new Models.Paypal_Order.PurchaseUnit()
        //                {
        //                    amount = new Models.Paypal_Order.Amount()
        //                    {
        //                        currency_code = "USD",
        //                        value = precio,
        //                        breakdown = new Models.Paypal_Order.Breakdown()
        //                        {
        //                            item_total = new Models.Paypal_Order.ItemTotal()
        //                            {
        //                                currency_code = "USD",
        //                                value = precio
        //                            }
        //                        }
        //                    }
        //                }
        //            },
        //            application_context = new ApplicationContext()
        //            {
        //                return_url = "https://www.youtube.com/watch?v=lbvkYag_wGE&t=2640s",
        //                cancel_url = "https://www.facebook.com"
        //            }
        //        };
        //        var json = JsonConvert.SerializeObject(orden);
        //        var data = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

        //        status = response.IsSuccessStatusCode;
        //        if (status)
        //        {
        //            respuesta=response.Content.ReadAsStringAsync().Result;
        //        }
        //    }

        //    return Json(new {status=status, respuesta=respuesta},JsonRequestBehavior.AllowGet);

        //}


        [HttpPost]
        public async Task<JsonResult> Paypal(string precio, string ncuota)
        {
            bool status = false;
            string respuesta = string.Empty;

            try
            {
                // Configurar las credenciales de autenticación de PayPal
                var userName = "AceDg6yf6zlibcKhUCnXzhpKZ78r_CpkfuCSRJJQ08uQGj8KIJcIwY-yHYIgBX9u2YILhGDRJCr_z9vu";
                var password = "EBE03zsi9ey0KQvX9SBPui-PKjeD2eTVHK418QZL6ut8dMxTD8EysI61xcjS91tZITim0KFec3AwJbqe";

                // Construir el objeto de solicitud a la API de PayPal
                var orden = new
                {
                    intent = "CAPTURE",
                    purchase_units = new[]
                    {
                    new
                    {
                        items = new[]
                        {
                            new
                            {
                                name = "Numero de cuota: " + ncuota,
                                description = "Sistema de Prestamos Grupo 8A Cibertec",
                                quantity = "1",
                                unit_amount = new
                                {
                                    currency_code = "USD",
                                    value = precio
                                }
                            }
                        },
                        amount = new
                        {
                            currency_code = "USD",
                            value = precio,
                            breakdown = new
                            {
                                item_total = new
                                {
                                    currency_code = "USD",
                                    value = precio
                                }
                            }
                        }
                    }
                },
                    application_context = new
                    {
                        return_url = "http://localhost:56826/Prestatario/Abunt",
                        cancel_url = "http://localhost:56826/Home/Index"
                    }
                };

                // Convertir el objeto a formato JSON
                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // Configurar la solicitud HTTP a la API de PayPal
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
                    var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                    // Realizar la solicitud POST a la API de PayPal
                    var response = await client.PostAsync("/v2/checkout/orders", data);
                    status = response.IsSuccessStatusCode;

                    if (status)
                    {
                        respuesta = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        respuesta = "La solicitud a la API de PayPal no fue exitosa.";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "Ocurrió un error al procesar la solicitud: " + ex.Message;
            }

            // Retornar un JsonResult con el estado de la solicitud y la respuesta obtenida
            return Json(new { status = status, respuesta = respuesta }, JsonRequestBehavior.AllowGet);
        }



    }
}