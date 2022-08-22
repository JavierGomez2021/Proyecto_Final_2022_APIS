using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi2.Repository;

namespace MiPrimeraApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public String CargarVenta([FromBody] List<ProductoVendido> listaProductosVendido)
        {
            String resultado;
            List<ProductoVendido> lista = new List<ProductoVendido>();
            lista = listaProductosVendido;

            //0 - Verifico si los productos de la lista enviada por el front existen en la tabla de productos. 
            bool getProductos = false;

            foreach(ProductoVendido producto in lista)
            {
                getProductos = ProductoHandler.ExisteProducto(producto.IdProducto);
                
                if (getProductos == false)
                {
                    return resultado = "El producto " + producto.IdProducto + " no existe en la tabla Productos. Revisar la lista";                    
                }
            }
            //0 - Fin

            //1 - Cargo en la tabla venta una nueva venta. 
            bool resultadoNuevaVenta = false;

            resultadoNuevaVenta = VentaHandler.NuevaVenta(new Venta
            {
                Comentarios = "Nueva venta " + DateTime.Now, 
            }) ;
            //1 - fin

            //2 - Busco el ultimo ID de venta asigando en el paso 1 para usarlo en la carga de productoVendido y mantener la relacion de las tablas.
            int maxIdVenta;

            maxIdVenta = VentaHandler.VentaMaxId();
            //2 - fin

            //3 - Cargo en la tabla ProductoVendido la lista de productos vendidos. 
            bool resultadoCrearProductoVendido = false;
            bool resultadoRestoStockProducto   = false;
            
            foreach (ProductoVendido productoVendido in lista)
            {
                //3.1 - Cargo cada producto vendido enviado por el front en la tabla productoVendido
                resultadoCrearProductoVendido = ProductoVendidoHandler.CargoProductoVendido(new ProductoVendido
                {
                    Stock      = productoVendido.Stock,
                    IdProducto = productoVendido.IdProducto,
                    //Este es el ultimo IdVenta generado
                    IdVenta    = maxIdVenta
                });
                //3.1 - fin

                //3.2 - Busco el stock en la tabla de productos con el id del productoVendido para restar el stock en tabla Producto
                Producto productoId = new Producto();
                productoId = ProductoHandler.GetProductoPorId(productoVendido.IdProducto);
                //3.2 - fin

                //3.3 - verifico stock y Modifico / resto el stock del producto vendido menos el stock del producto. Asumo que el stock en producto vendido
                //refiere a la cantidad de productos vendidos para esa venta. 
                if (productoId.Stock >= productoVendido.Stock)
                {
                    resultadoRestoStockProducto = ProductoHandler.ModificarProductoStock(new Producto
                    {
                        Id    = productoVendido.IdProducto,
                        Stock = productoId.Stock - productoVendido.Stock
                    });                    
                }
                else
                {
                    return resultado = "La cantidad de unidades solicitadas " + productoVendido.Stock + " supera al stock disponible de " + productoId.Stock + " unidades del producto " + productoVendido.IdProducto;
                }
                //3.3 - fin
            }
            //3 - FIN

            //4 - Verifico si todos los metodos terminaron ok
            if (resultadoNuevaVenta == true && resultadoCrearProductoVendido == true && resultadoRestoStockProducto == true)
            {
                resultado = "Operacion satisfactoria";
            }
            else
            {
                resultado = "Operacion erronea";
            }
            //4 - fin
            return resultado;
        }
    }
}
