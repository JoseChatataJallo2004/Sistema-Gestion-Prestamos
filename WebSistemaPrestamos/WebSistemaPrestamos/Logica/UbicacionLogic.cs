using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Logica
{
    public class UbicacionLogic
    {

        //obtener departamento
        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lista = new List<Departamento>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {

                    string sql = "select * from Departamento";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Departamento
                            {
                                iddepartamento = Convert.ToInt32(dr["Iddepartamento"]),
                                NombreDepartamento = dr["NombreDepartamento"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Departamento>();
            }
            return lista;

        }

        //obtener provincia
        public List<Provincia> ObtenerProvincia(int iddepartamento)
        {
            List<Provincia> lista = new List<Provincia>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string sql = "select * from Provincia where  IdDepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Provincia
                            {
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                NombreProvincia = dr["NombreProvincia"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<Provincia>();
            }
            return lista;
        }

        //obtener distrito
        public List<Distrito> ObtenerDistrito(int idprovincia)
        {
            List<Distrito> lista = new List<Distrito>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string sql = "select * from Distrito where IdProvincia = @idprovincia ";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = System.Data.CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Distrito
                            {
                                IdDistrito = Convert.ToInt32(dr["iddistrito"]),
                                nombredistrito = dr["NombreDistrito"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<Distrito>();
            }
            return lista;
        }

        //Listar Tipo de documento
        public List<Documento> ObtenerDocumento()
        {
            List<Documento> lista = new List<Documento>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string sql = "select * from Documento ";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Documento
                            {
                                iddocumento = Convert.ToInt32(dr["iddocumento"]),
                                descripcion = dr["TipoDocumento"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<Documento>();
            }
            return lista;
        }


    }
}