using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2.Repository
{
    public class ProductoVendidoHandler
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";

        public static bool CargoProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
                        
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertar = "INSERT INTO PRODUCTOVENDIDO" +
                                      "(STOCK,IDPRODUCTO,IDVENTA)" +
                                      "VALUES(@STOCK,@IDPRODUCTO,@IDVENTA)";

                    SqlParameter stockParameter      = new SqlParameter("STOCK", System.Data.SqlDbType.Int)         { Value = productoVendido.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("IDPRODUCTO", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                    SqlParameter idVentaParameter    = new SqlParameter("IDVENTA", System.Data.SqlDbType.BigInt)    { Value = productoVendido.IdVenta };

                    using (SqlCommand cmd = new SqlCommand(insertar, cn))
                    {

                        cmd.Parameters.Add(stockParameter);
                        cmd.Parameters.Add(idProductoParameter);
                        cmd.Parameters.Add(idVentaParameter);

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

        public static bool GetProductoVendidoPorId(int id)
        {
            {
                bool resultado = false;

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConnectionString))
                    {
                        conexion.Open();
                        string selectId = "SELECT * FROM PRODUCTOVENDIDO WHERE IDPRODUCTO = @ID";

                        SqlParameter idParameter = new SqlParameter("ID", System.Data.SqlDbType.BigInt) { Value = id };

                        using (SqlCommand command = new SqlCommand(selectId, conexion))
                        {
                            command.Parameters.Add(idParameter);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    resultado = true;
                                }
                            }
                        }
                        conexion.Close();
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
        }

        public static bool EliminoProductoVendido(int id)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM PRODUCTOVENDIDO WHERE IDPRODUCTO = @ID";

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
    }

}

