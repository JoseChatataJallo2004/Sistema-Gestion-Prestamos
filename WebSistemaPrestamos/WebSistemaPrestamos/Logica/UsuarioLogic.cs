using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebSistemaPrestamos.Models;

namespace WebSistemaPrestamos.Logica
{
    public class UsuarioLogic
    {
        public List<Usuario> IngresarAlSistema()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "usp_Ingresaralsistema";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                idusuario = Convert.ToInt32(dr["idusuario"]),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),
                                reestablecer =Convert.ToBoolean(dr["reestablecer"]),
                                orol = new Rol() { idrol = Convert.ToInt32(dr["idrol"]), descripcion = dr["descripcion"].ToString() },
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }
            return lista;
        }



        public int registrarprestatariovista(Usuario obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPrestatario", oconexion);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@idtipodocumento", obj.odocumento.iddocumento);
                    cmd.Parameters.AddWithValue("@docume", obj.docume);
                    cmd.Parameters.AddWithValue("@iddepartamento", obj.odepartametno.iddepartamento);
                    cmd.Parameters.AddWithValue("@idprovincia", obj.oprovincia.IdProvincia);
                    cmd.Parameters.AddWithValue("@iddistrito", obj.odistrito.IdDistrito);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@clave", obj.clave);
                    cmd.Parameters.AddWithValue("@idrol", 1);
                    cmd.Parameters.AddWithValue("@idusuariopadre", obj.ousuario.idusuario);

                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                mensaje = ex.Message;
            }
            return idautogenerado;
        }


        public bool cambiarclave(int idusuario, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @nuevaclave,reestablecer=1 where idusuario=@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }


        public bool reestablecerclave(int idusuario, string clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @clave,reestablecer=0 where idusuario=@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }


        public List<Usuario> Listarprestamista()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    string query = "usp_listarprestamistas";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                idusuario = Convert.ToInt32(dr["idusuario"]),
                                nombres = dr["NombreCompleto"].ToString()
                              
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }
            return lista;
        }

        //vistas dinamicas pero un slo registro
        public int registrarusuariogenerl(Usuario obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuariosGeneral", oconexion);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@idtipodocumento", obj.odocumento.iddocumento);
                    cmd.Parameters.AddWithValue("@docume", obj.docume);
                    cmd.Parameters.AddWithValue("@iddepartamento", obj.odepartametno.iddepartamento);
                    cmd.Parameters.AddWithValue("@idprovincia", obj.oprovincia.IdProvincia);
                    cmd.Parameters.AddWithValue("@iddistrito", obj.odistrito.IdDistrito);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@clave", obj.clave);
                    cmd.Parameters.AddWithValue("@idrol", obj.idddrol);
                    cmd.Parameters.AddWithValue("@idusuariopadre", obj.idpadrevista);
                    cmd.Parameters.AddWithValue("@reestablacer", 0);

                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                mensaje = ex.Message;
            }
            return idautogenerado;
        }


        public bool EditarUsuarioGEneral(Usuario obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadenasistemas))
                {
                    SqlCommand cmd = new SqlCommand("usp_editar_usuariosgeneral", oconexion);
                    cmd.Parameters.AddWithValue("@idusuario", obj.idusuario);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@idtipodocumento", obj.odocumento.iddocumento);
                    cmd.Parameters.AddWithValue("@docume", obj.docume);
                    cmd.Parameters.AddWithValue("@iddepartamento", obj.odepartametno.iddepartamento);
                    cmd.Parameters.AddWithValue("@idprovincia", obj.oprovincia.IdProvincia);
                    cmd.Parameters.AddWithValue("@iddistrito", obj.odistrito.IdDistrito);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);                 
                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@resultado"].Value);
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;

        }



    }
}