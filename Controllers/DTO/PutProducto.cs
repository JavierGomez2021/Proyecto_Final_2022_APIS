namespace MiPrimeraApi2.Controllers.DTO
{
    public class PutProducto
    {
        public int    Id          { get; set; }
        public string Descripcion { get; set; }
        public double Costo       { get; set; }
        public double PrecioVenta { get; set; }
        public int    Stock       { get; set; }
        public int    IdUsuario   { get; set; }
    }
}
