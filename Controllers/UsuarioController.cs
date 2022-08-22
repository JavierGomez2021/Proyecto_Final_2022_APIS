using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi2.Controllers.DTO;

namespace MiPrimeraApi2.Controllers
{
    [ApiController] //sirve para avisar que nuestra clase va a ser un controller
    [Route("[controller]")]
    public class UsuarioController : ControllerBase //Implementa ControllerBase: Expone todas las clases que lo implementen
    {
        [HttpGet(Name = "GetUsuario")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();         
        }

        [HttpDelete]
        public bool EliminarUsuario([FromBody] int id)
        {
            return UsuarioHandler.EliminarUsuario(id);
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarUsuario(new Usuario
            {
               Id = usuario.Id,
               Nombre = usuario.Nombre,
               Apellido = usuario.Apellido,
               NombreUsuario = usuario.NombreUsuario,
               Contraseña = usuario.Contraseña,
               Mail = usuario.Mail
            });
        }

        [HttpPost]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            return UsuarioHandler.CrearUsuario(new Usuario
            {                
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail

            }); ;
        }
    }
}
