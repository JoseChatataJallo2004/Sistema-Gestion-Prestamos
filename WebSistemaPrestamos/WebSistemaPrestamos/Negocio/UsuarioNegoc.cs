using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Logica;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Negocio
{
    public class UsuarioNegoc
    {

        private UsuarioLogic objcapaDato = new UsuarioLogic();

        public List<Usuario> IngresarAlSistema()
        {
            return objcapaDato.IngresarAlSistema();
        }


        public List<Usuario> Listarprestamista()
        {
            return objcapaDato.Listarprestamista();
        }





        public int registrarprestatariovista(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {
                mensaje = "El nombre del Usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                mensaje = "El apellido del Usuario no puede ser vacio";

            }
            else if (obj.odocumento.iddocumento == 0)
            {
                mensaje = "Debe seleccionar un tipo de documento";
            }
            else if (string.IsNullOrEmpty(obj.docume) || string.IsNullOrWhiteSpace(obj.docume))
            {
                mensaje = "El Documento  del Usuario no puede ser vacio";

            }
            else if (obj.odepartametno.iddepartamento == 0)
            {
                mensaje = "Debe Seleccionar un Departamento";
            }
            else if (obj.oprovincia.IdProvincia == 0)
            {
                mensaje = "Debe Seleccionar un Provincia";
            }
            else if (obj.odistrito.IdDistrito == 0)
            {
                mensaje = "Debe Seleccionar un Distrito";
            }




            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "El correo del Usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.clave) || string.IsNullOrWhiteSpace(obj.clave))
            {
                mensaje = "La contraseña del Usuario no puede ser vacio";

            }
            else if (obj.ousuario.idusuario == 0)
            {
                mensaje = "Debe Seleccionar un prestamista ";
            }



            if (string.IsNullOrEmpty(mensaje))
            {




                obj.clave = Recursos.externos.convertirsha256(obj.clave);
                return objcapaDato.registrarprestatariovista(obj, out mensaje);


            }
            else
            {
                return 0;
            }
        }


        //panel de claves
        public bool cambiarclave(int idusuario, string nuevaclave, out string mensaje)
        {
            return objcapaDato.cambiarclave(idusuario, nuevaclave, out mensaje);
        }



        //reestablecer
        public bool reestablecerclave(int idcliente, string correo, out string mensaje)
        {
            mensaje = string.Empty;

            string nuevaclave = Recursos.externos.generarclave();

            bool resultado = objcapaDato.reestablecerclave(idcliente, Recursos.externos.convertirsha256(nuevaclave), out mensaje);

            if (resultado)
            {

                string asunto = "Contraseña reestablecida";

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
                                        <img src=""https://cdn-icons-png.flaticon.com/512/6357/6357048.png"" alt=""Restablecer contraseña"" style=""max-width: 80px; height: auto; margin-right: 30px;"">
                                        <h4>Hola Estimado Usuario</h4>
                                    </div>
        
                                    <p>Recientemente has solicitado restablecer tu contraseña. Por favor sigue el proceso como se te indica. En caso de que no hayas sido tú quien solicitó restablecerla, pongase en contacto con Soporte Tecnico .</p>

                                    <p>Su contraseña para ingresar al sistema es:  !clave! </p>
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



                //string mensaje_correo = "<h3> Hola    Su cuenta fue reestablecida correctamente </h3>" +
                //    "</br>" +

                //    "<p>Su contraseña para acceder ahora es : !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);


                bool respuesta = Recursos.externos.enviarcorreo(correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    return true;
                }
                else
                {
                    mensaje = "No se pudo enviar al correo";
                    return false;
                }
            }
            else
            {
                mensaje = "No se pudo enviar al corre";
                return false;
            }

        }


        //Registrar usuario en general
        public int registrarusuariogenerl(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {
                mensaje = "El nombre del Usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                mensaje = "El apellido del Usuario no puede ser vacio";

            }
            else if (obj.odocumento.iddocumento == 0)
            {
                mensaje = "Debe seleccionar un tipo de documento";
            }
            else if (string.IsNullOrEmpty(obj.docume) || string.IsNullOrWhiteSpace(obj.docume))
            {
                mensaje = "El Documento  del Usuario no puede ser vacio";

            }
            else if (obj.odepartametno.iddepartamento == 0)
            {
                mensaje = "Debe Seleccionar un Departamento";
            }
            else if (obj.oprovincia.IdProvincia == 0)
            {
                mensaje = "Debe Seleccionar un Provincia";
            }
            else if (obj.odistrito.IdDistrito == 0)
            {
                mensaje = "Debe Seleccionar un Distrito";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "El correo del Usuario no puede ser vacio";

            }
            if (string.IsNullOrEmpty(mensaje))
            {
                string clave = Recursos.externos.generarclave();
                string asunto = "Creacion de Cuenta";

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
        
                                    <p>Hola Estimado Usuario acabas de ser registrado en el Sistema de Gestion de prestamos disfrute de nuestro sistema </p>

                                    <p>Su contraseña para ingresar al sistema es:  !clave! </p>
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



                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = Recursos.externos.enviarcorreo(obj.correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.clave = Recursos.externos.convertirsha256(clave);
                    return objcapaDato.registrarusuariogenerl(obj, out mensaje);
                }
                else
                {
                    mensaje = "No se pido enviar el correo";
                    return 0;
                }


            }
            else
            {
                return 0;
            }


        }

        public bool EditarUsuarioGEneral(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {
                mensaje = "El nombre del Usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                mensaje = "El apellido del Usuario no puede ser vacio";

            }
            else if (obj.odocumento.iddocumento == 0)
            {
                mensaje = "Debe seleccionar un tipo de documento";
            }
            else if (string.IsNullOrEmpty(obj.docume) || string.IsNullOrWhiteSpace(obj.docume))
            {
                mensaje = "El Documento  del Usuario no puede ser vacio";

            }
            else if (obj.odepartametno.iddepartamento == 0)
            {
                mensaje = "Debe Seleccionar un Departamento";
            }
            else if (obj.oprovincia.IdProvincia == 0)
            {
                mensaje = "Debe Seleccionar un Provincia";
            }
            else if (obj.odistrito.IdDistrito == 0)
            {
                mensaje = "Debe Seleccionar un Distrito";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "El correo del Usuario no puede ser vacio";

            }
            if (String.IsNullOrEmpty(mensaje))
            {
                return objcapaDato.EditarUsuarioGEneral(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }



    }
}