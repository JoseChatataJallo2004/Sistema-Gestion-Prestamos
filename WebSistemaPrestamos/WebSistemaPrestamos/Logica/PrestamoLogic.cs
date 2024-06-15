using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Logica
{
    public class PrestamoLogic
    {
        public int RegistrarPrestamos(Prestamo obj,out string mensaje)
        {
            int resultado = 0;

            mensaje=String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarprestamos", oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuarioPrestatario", obj.IdUsuarioPrestatario);
                    cmd.Parameters.AddWithValue("@correousuario", obj.correo);
                    cmd.Parameters.AddWithValue("@FechaInicioPago", obj.FechaInicioPago);
                    cmd.Parameters.AddWithValue("@FechaFinPago", obj.FechaFinPago);
                    cmd.Parameters.AddWithValue("@MontoPrestamo", obj.MontoPrestamo);
                    cmd.Parameters.AddWithValue("@nrodias", obj.nrodias);
                    cmd.Parameters.AddWithValue("@Valordia", obj.valorpordia);

                    // cmd.Parameters.Add("@mensaje",SqlDbType.VarChar,500).Direction= ParameterDirection.Input;

                    // Definir el parámetro @mensaje como salida
                    SqlParameter mensajeParam = cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;


                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    resultado=cmd.ExecuteNonQuery();
                    // mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                    // Obtener el valor del parámetro de salida @mensaje
                    mensaje = mensajeParam.Value.ToString();

                }
            }
            catch (Exception ex)
            {
               mensaje=ex.Message;
            }
            return resultado;
        }


        public List<Prestamo> ListarPrestamosPrestatario(int idusuario)
        {
            List<Prestamo> lista = new List<Prestamo>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "sp_listarprestamos";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idusuario", idusuario);
                        oconexion.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Prestamo prestamo = new Prestamo
                                {
                                    IdPrestamo = Convert.ToInt32(dr["IdPrestamo"]),
                                    IdUsuarioPrestatario = Convert.ToInt32(dr["IdUsuarioPrestatario"]),
                                    FechaInicioPago = dr["FechaInicioPago"].ToString(),
                                    FechaFinPago = dr["FechaFinPago"].ToString(),
                                    MontoPrestamo = Convert.ToDecimal(dr["MontoPrestamo"]),
                                    nrodias = Convert.ToInt32(dr["nrodias"]),
                                    valorpordia = Convert.ToDecimal(dr["Valordia"]),
                                    estado = Convert.ToInt32(dr["Estado"])
                                    

                                };
                                lista.Add(prestamo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return lista;
        }



        public List<Prestamo> ListarPrestamosPrestamista(int idusuariopadre)
        {
            List<Prestamo> lista = new List<Prestamo>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "sp_listarPrestamistaPrestamos";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idpadreusuario", idusuariopadre);
                        oconexion.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Prestamo prestamo = new Prestamo
                                {
                                    IdPrestamo = Convert.ToInt32(dr["IdPrestamo"]),
                                    nombres = dr["nombres"].ToString(),
                                    apellidos = dr["apellidos"].ToString(),
                                    FechaInicioPago = dr["FechaInicioPago"].ToString(),
                                    FechaFinPago = dr["FechaFinPago"].ToString(),
                                    MontoPrestamo = Convert.ToDecimal(dr["MontoPrestamo"]),
                                    nrodias = Convert.ToInt32(dr["nrodias"]),
                                    valorpordia = Convert.ToDecimal(dr["Valordia"]),
                                    estado = Convert.ToInt32(dr["Estado"])


                                };
                                lista.Add(prestamo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return lista;
        }


    }
}