using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi2.Controllers.DTO;
using MiPrimeraApi2.Repository;

namespace MiPrimeraApi2.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpDelete]
        public bool EliminarProducto([FromBody] int id)
        {
            bool resultado              = false;
            bool eliminoProducto        = false;
            bool eliminoProductoVendido = false;

            bool getProductoVendidoPorId = ProductoVendidoHandler.GetProductoVendidoPorId(id);
     
            if (getProductoVendidoPorId == true)
            {
                eliminoProductoVendido = ProductoVendidoHandler.EliminoProductoVendido(id);
            }

            eliminoProducto = ProductoHandler.EliminarProducto(id);

            if (eliminoProducto == true)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        [HttpPost]
        public String CrearProducto([FromBody] PostProducto producto)
        {
            bool resultado = false;

            if (producto.Descripcion == String.Empty)
            {
                return "No se informo la Descripcion";
            }
            else if(producto.Stock < 0)
                {
                    return "No se informo el Stock";
                }
                else if(producto.IdUsuario <= 0)
                    {
                        return "No se informa el IDUSUARIO";
                    }
            else                
            {
                resultado = ProductoHandler.CrearProducto(new Producto
                {
                    Descripcion = producto.Descripcion,
                    Costo       = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock       = producto.Stock,
                    IdUsuario   = producto.IdUsuario
                });
            }

            if(resultado == true)
            { 
                return "Se dio de alta el nuevo Producto";
            }
            else
            {
                return "Error en el alta del nuevo producto";
            }
        }

        [HttpPut]
        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            return ProductoHandler.ModificarProducto(new Producto
            {
                Id = producto.Id,
                Descripcion = producto.Descripcion,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario
            });
        }
    }
}
