using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Logica
{
    public class CrudGeneralLogic
    {

        public List<Usuario> ListarUsuariosPorPadre(int idPadre)
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "usp_listarusuariosporpadres";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idpadre", idPadre);
                        oconexion.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    idusuario = Convert.ToInt32(dr["idusuario"]),
                                    nombres = dr["nombres"].ToString(),
                                    apellidos = dr["apellidos"].ToString(),
                                    odocumento = new Documento
                                    {
                                        iddocumento = Convert.ToInt32(dr["iddocumento"]),
                                        descripcion = dr["TipoDocumento"].ToString()
                                    },
                                    docume = dr["docume"].ToString(),
                                    odepartametno = new Departamento
                                    {
                                        iddepartamento = Convert.ToInt32(dr["Iddepartamento"]),
                                        NombreDepartamento = dr["NombreDepartamento"].ToString()
                                    },
                                    oprovincia = new Provincia
                                    {
                                        IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                        NombreProvincia = dr["NombreProvincia"].ToString()
                                    },
                                    odistrito = new Distrito
                                    {
                                        IdDistrito = Convert.ToInt32(dr["IdDistrito"]),
                                        nombredistrito = dr["NombreDistrito"].ToString()
                                    },
                                    orol = new Rol
                                    {
                                        idrol = Convert.ToInt32(dr["idrol"]),
                                        descripcion = dr["descripcion"].ToString()
                                    },
                                    correo = dr["correo"].ToString()
                                    
                                };
                                lista.Add(usuario);
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