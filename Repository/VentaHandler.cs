using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2.Repository
{
    public static class VentaHandler
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";

        public static bool NuevaVenta(Venta venta)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertar = "INSERT INTO VENTA" +
                                      "(COMENTARIOS)" +
                                      "VALUES(@COMENTARIOS)";

                    SqlParameter comentariosParameter = new SqlParameter("COMENTARIOS", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                    using (SqlCommand cmd = new SqlCommand(insertar, cn))
                    {

                        cmd.Parameters.Add(comentariosParameter);                        

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
        public static int VentaMaxId()
        {
            int maxIdVenta = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string selectMaxId = "SELECT MAX(ID) AS MAXID FROM VENTA";
                                        
                    using (SqlCommand cmd = new SqlCommand(selectMaxId, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    maxIdVenta = Convert.ToInt32(reader["MAXID"]);
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
            return maxIdVenta;
        }
    }
}
