using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2
{ 
    public static class UsuarioHandler
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";
        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String listaUsuariosQuery = "SELECT * FROM USUARIO";
                    using (SqlCommand cmd = new SqlCommand(listaUsuariosQuery, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    Usuario usuario = new Usuario();

                                    usuario.Id            = Convert.ToInt32(reader["ID"]);
                                    usuario.Nombre        = Convert.ToString(reader["NOMBRE"]);
                                    usuario.Apellido      = Convert.ToString(reader["APELLIDO"]);
                                    usuario.NombreUsuario = Convert.ToString(reader["NOMBREUSUARIO"]);
                                    usuario.Contraseña    = Convert.ToString(reader["CONTRASEÑA"]);
                                    usuario.Mail          = Convert.ToString(reader["MAIL"]);

                                    listaUsuarios.Add(usuario);
                                }
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message + "\n" +
                                     "Error Number: " + ex.Number + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source + "\n" +
                                     "Procedure:    " + ex.Procedure + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            return listaUsuarios;
        }
        public static bool EliminarUsuario(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM USUARIO WHERE ID = @ID";

                    SqlParameter sqlParameter = new SqlParameter("ID", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = id;

                    using (SqlCommand cmd = new SqlCommand(baja, cn))
                    {
                        
                        cmd.Parameters.Add(sqlParameter);                        
                        int filasAfectadas = cmd.ExecuteNonQuery();                       
                        if (filasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message + "\n" +
                                     "Error Number: " + ex.Number + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source + "\n" +
                                     "Procedure:    " + ex.Procedure + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            return resultado;
        }
        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String modificar = "UPDATE USUARIO SET " +
                                       "NOMBRE = @NOMBRE," +
                                       "APELLIDO = @APELLIDO," +
                                       "NOMBREUSUARIO = @NOMBREUSUARIO," +
                                       "CONTRASEÑA = @CONTRASEÑA," +
                                       "MAIL = @MAIL " +
                                       "WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(modificar, cn))
                    {
                        cmd.Parameters.AddWithValue("@ID", usuario.Id);
                        cmd.Parameters.AddWithValue("@NOMBRE", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@APELLIDO", usuario.Apellido);
                        cmd.Parameters.AddWithValue("@NOMBREUSUARIO", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("CONTRASEÑA", usuario.Contraseña);
                        cmd.Parameters.AddWithValue("MAIL", usuario.Mail);

                        int numFilasAfectadas = cmd.ExecuteNonQuery();

                        if(numFilasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;                        
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message + "\n" +
                                     "Error Number: " + ex.Number + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source + "\n" +
                                     "Procedure:    " + ex.Procedure + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            return resultado;
        }
        public static bool CrearUsuario(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertar = "INSERT INTO USUARIO" +
                                      "(NOMBRE,APELLIDO,NOMBREUSUARIO,CONTRASEÑA,MAIL)" +
                                      "VALUES(@NOMBRE,@APELLIDO,@NOMBREUSUARIO,@CONTRASEÑA,@MAIL)";

                    SqlParameter nombreParameter        = new SqlParameter("NOMBRE", System.Data.SqlDbType.VarChar)        { Value = usuario.Nombre };
                    SqlParameter apellidoParameter      = new SqlParameter("APELLIDO", System.Data.SqlDbType.VarChar)      { Value = usuario.Apellido };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NOMBREUSUARIO", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                    SqlParameter contraseñaParameter    = new SqlParameter("CONTRASEÑA", System.Data.SqlDbType.VarChar)    { Value = usuario.Contraseña };
                    SqlParameter mailParameter          = new SqlParameter("MAIL", System.Data.SqlDbType.VarChar)          { Value = usuario.Mail };

                    using (SqlCommand cmd = new SqlCommand(insertar, cn))
                    {
                        cmd.Parameters.Add(nombreParameter);
                        cmd.Parameters.Add(apellidoParameter);
                        cmd.Parameters.Add(nombreUsuarioParameter);
                        cmd.Parameters.Add(contraseñaParameter);
                        cmd.Parameters.Add(mailParameter);

                        int numFilasAfectadas = cmd.ExecuteNonQuery();

                        if (numFilasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    cn.Close();
                }
            }
            catch(SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message + "\n" +
                                     "Error Number: " + ex.Number + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source + "\n" +
                                     "Procedure:    " + ex.Procedure + "\n");

                Console.WriteLine(errorMessages.ToString());            
            }
            return resultado;
        }
    }    
}
