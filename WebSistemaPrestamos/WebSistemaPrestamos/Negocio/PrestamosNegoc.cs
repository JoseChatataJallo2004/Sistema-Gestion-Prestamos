using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Logica;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Negocio
{
    public class PrestamosNegoc
    {
        private PrestamoLogic objcapa=new PrestamoLogic();

        public int RegistrarPrestamos(Prestamo obj, out string mensaje)
        {

            mensaje=string.Empty;

            string asunto = "Solicitud Registrado";
            string mensaje_correo = @"
                            <!DOCTYPE html>
                            <html lang=""es"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title></title>
                                <style>
                                    body {
                                        margin: 0;
                                        padding: 0;
                                        min-width: 100%;
                                        width: 100% !important;
                                        height: 100% !important;
                                        background-color: #f0f0f0;
                                        color: #000000;
                                        -webkit-font-smoothing: antialiased;
                                        text-size-adjust: 100%;
                                        -ms-text-size-adjust: 100%;
                                        -webkit-text-size-adjust: 100%;
                                        line-height: 100%;
                                        font-family: Arial, sans-serif;
                                    }

                                    .container {
                                        max-width: 600px;
                                        margin: 0 auto;
                                        padding: 20px;
                                        background-color: #ffffff;
                                        border-radius: 8px;
                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                    }

                                    h4 {
                                        color: #000000;
                                        font-weight: bold;
                                    }

                                    p {
                                        color: #000000;
                                    }

                                    a {
                                        color: #127DB3;
                                        text-decoration: none;
                                    }

                                    .line hr {
                                        margin: 0;
                                        padding: 0;
                                        border: none;
                                        height: 1px;
                                        background-color: #E0E0E0;
                                    }

                                    footer {
                                        margin-top: 20px;
                                        text-align: center;
                                        font-size: 12px;
                                        color: #888888;
                                    }
                                </style>
                            </head>
                            <body>
                                <div class=""container"">
                                    <div style=""text-align: center;"">
                                        <img src=""https://www.shutterstock.com/image-vector/congratulations-paper-banner-color-confetti-260nw-401555809.jpg"" alt=""Restablecer contraseña"" style=""max-width: 200px; height: 0 auto;"">
                                       
                                    </div>
        
                                    <p>Hola Estimado Usuario tu solicitud de prestamo se registro en nuestro sistema este atento si sera aprobada  </p>

                                    <div class=""line"">
                                        <hr>
                                    </div>
                                    <div class=""line"">
                                        <hr>
                                    </div>
                                    <p>No responda a este mensaje. Este correo electrónico ha sido enviado a través de un sistema automatizado que no permite dar respuesta a las preguntas enviadas a esta dirección. Para ponerse en contacto con nosotros haga clic en <a href=""https://josechatatajallo.odoo.com/"" target=""_blank"">contacto</a>.</p>
        
                                    <footer>
                                        &copy; 2024 Derechos reservados
                                    </footer>
                                </div>
                            </body>
                            </html>";

            bool respuesta = Recursos.externos.enviarcorreo(obj.correo, asunto, mensaje_correo);

            if (respuesta)
            {
                return objcapa.RegistrarPrestamos(obj, out mensaje);
            }
            else
            {
                return 0;
            }


            //return objcapa.RegistrarPrestamos(obj);
        }


        public List<Prestamo> ListarPrestamosPrestatario(int idusuario)
        {
            return objcapa.ListarPrestamosPrestatario(idusuario);
        }

        public List<Prestamo> ListarPrestamosPrestamista(int idusuariopadre)
        {
            return objcapa.ListarPrestamosPrestamista(idusuariopadre);
        }

    }
}