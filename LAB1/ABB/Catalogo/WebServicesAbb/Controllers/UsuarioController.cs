using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Entidades.Core;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using ABB.Catalogo.LogicaNegocio.Core;
using System.Text.Json;

namespace ABB.Catalogo.WebServicesABB.Controllers
{
    [ApiController]
    [Route("/Api/Usuario")]
    public class UsuarioController{
        [HttpGet]
        public IEnumerable<Usuarios> Get(){
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = new UsuariosLN().ListarUsuarios();
            return usuarios;
        }
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }
        [HttpGet("/getuserid")]
        public int GetByUsuarioAndPassword([FromQuery]string pUsuario, [FromQuery] string pPassword)
        {
            try{
                UsuariosLN usuario = new UsuariosLN();
                return usuario.GetUsuarioId(pUsuario,pPassword);
            }
            catch(Exception ex){
                string innerException =ex.InnerException==null ? "":ex.InnerException.ToString();
                Console.WriteLine("Error en Controller:"+innerException);
                return -1; 
            }
        }
        [HttpPost]
        public void Post([FromBody]Dictionary<string,object> usuariosParcial)
        {
            try
            {

                Usuarios usuarios = new Usuarios() {
                    Nombres = ((JsonElement)usuariosParcial["nombres"]).GetString(),
                    ClaveTxt = ((JsonElement)usuariosParcial["clave"]).GetString(),
                    IdRol = ((JsonElement)usuariosParcial["idRol"]).GetInt32(),
                    CodUsuario = ((JsonElement)usuariosParcial["codUsuario"]).GetString()
                };
                new UsuariosLN().InsertarUsuario(usuarios);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Controller:" + innerException);
            }
        }
        [HttpPut("{id}")]
        public void Put(int id,[FromBody]Dictionary<string, object> usuariosParcial)
        {
            try
            {
                Usuarios usuarios = new Usuarios()
                {
                    Nombres = ((JsonElement)usuariosParcial["nombres"]).GetString(),
                    ClaveTxt = ((JsonElement)usuariosParcial["clave"]).GetString(),
                    IdRol = ((JsonElement)usuariosParcial["idRol"]).GetInt32(),
                    CodUsuario = ((JsonElement)usuariosParcial["codUsuario"]).GetString()
                };

                new UsuariosLN().ModificarUsuario(id,usuarios);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                new UsuariosLN().BorrarUsuario(id);
            }
            catch(Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }
    }
}
