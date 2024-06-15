using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Logica
{
    public class DetallePrestamosLogic
    {

        public List<DetallePrestamo> ListardetallePrestamos(int idprestamo)
        {
            List<DetallePrestamo> lista = new List<DetallePrestamo>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "sp_listardetallerprestamos";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idprestamo", idprestamo);
                        oconexion.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DetallePrestamo detalleprestamo = new DetallePrestamo
                                {
                                    IdPrestamoDetalle = Convert.ToInt32(dr["IdPrestamoDetalle"]),
                                    idprestamos = Convert.ToInt32(dr["IdPrestamo"]),
                                    NroCuota = Convert.ToInt32(dr["Nrocuota"]),
                                    MontoDiario = Convert.ToDecimal(dr["MontoDiario"]),
                                    Estado= Convert.ToInt32(dr["Estado"])

                                };
                                lista.Add(detalleprestamo);
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