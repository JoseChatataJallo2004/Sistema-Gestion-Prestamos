using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Logica
{
    public class RolLogic
    {

        public List<Rol> listar()
        {
            List<Rol> lista = new List<Rol>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "select * from rol";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                idrol = Convert.ToInt32(dr["idrol"]),
                                descripcion = dr["descripcion"].ToString()
                               
                               



                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Rol>();
            }
            return lista;
        }



    }
}