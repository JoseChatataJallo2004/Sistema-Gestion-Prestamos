using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebSistemaPrestamos.Recursos
{
    public class externos
    {

        //encriptacion de texto en sha256
        public static string convertirsha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            //usar refenrencia de system.security.cryptography
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }


        public static string generarclave()
        {
            //que sea nuero y clave del 0 al  6 
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }


        //para el correo debe ser de google en tu cuenta de gmail ir a verificacion de 2 pasos y contraseñas de aplicaciones
        public static bool enviarcorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("soportesisfinan@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("soportesisfinan@gmail.com", "amdicfxlohwrxskw"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                };
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            return resultado;
        }

    }
}