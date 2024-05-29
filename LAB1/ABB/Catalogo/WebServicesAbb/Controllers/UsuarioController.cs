using Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace ABB.Catalogo.WebServicesABB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/Api/Usuario")]
    public class UsuarioController : Controller{
        [HttpGet]
        public IEnumerable<Usuarios> Get(){
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = new UsuariosLN().ListarUsuarios();
            return usuarios;
        }
        [HttpGet("{id}")]
        public Usuarios GetById(int id)
        {
            return new UsuariosLN().GetUsuariosById(id);
        }
        [HttpGet("/getUserId")]
        public ActionResult Get([FromQuery] string pUsuario, [FromQuery] string pPassword)
        {
            if (pUsuario == null || pPassword == null)
            {
                return BadRequest("Debe ingresar credenciales validas");
            }

            try
            {
                UsuariosLN usuario = new UsuariosLN();
                var rsp = usuario.GetUsuarioId(pUsuario, pPassword);
                return Ok(Convert.ToString(rsp));
                // return usuario.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody]Dictionary<string,object> usuariosParcial)
        {
            try
            {

                Usuarios usuarios = new Usuarios() {
                    Nombres = ((JsonElement)usuariosParcial["nombres"]).GetString(),
                    ClaveTxt = ((JsonElement)usuariosParcial["clave"]).GetString(),
                    IdRol = ((JsonElement)usuariosParcial["idRol"]).GetInt32(),
                    CodUsuario = ((JsonElement)usuariosParcial["codUsuario"]).GetString()
                };
                if (usuarios.CodUsuario == null)
                {
                    return BadRequest("CodUsuario es nulo");
                }
                if (usuarios.ClaveTxt == null)
                {
                    return BadRequest("ClaveTxt es nulo");
                }
                if (usuarios.Nombres == null)
                {
                    return BadRequest("Nombres es nulo");
                }
                if (usuarios.IdRol <= 0)
                {
                    return BadRequest("IdRol es nulo");
                }
                Usuarios usuario = new UsuariosLN().InsertarUsuario(usuarios);
                new UsuariosLN().InsertarUsuario(usuarios);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Controller:" + innerException);
                return BadRequest(innerException);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody]Dictionary<string, object> usuariosParcial)
        {
            try
            {
                if(id<=0)
                {
                    return BadRequest("CodUsuario es nulo");
                }
                Usuarios usuarios = new Usuarios()
                {
                    Nombres = ((JsonElement)usuariosParcial["nombres"]).GetString(),
                    ClaveTxt = ((JsonElement)usuariosParcial["clave"]).GetString(),
                    IdRol = ((JsonElement)usuariosParcial["idRol"]).GetInt32(),
                    CodUsuario = ((JsonElement)usuariosParcial["codUsuario"]).GetString()
                };

                new UsuariosLN().ModificarUsuario(id,usuarios);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
                return BadRequest(innerException);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("CodUsuario es nulo");
                }
                new UsuariosLN().BorrarUsuario(id);
                return Ok("Usuario eliminado");
            }
            catch(Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
                return BadRequest(innerException);
            }
        }
    }
}
