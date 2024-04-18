using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LogicaNegocio.Core;
using Entidades.Core;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using ABB.Catalogo.LogicaNegocio.Core;

namespace ABB.Catalogo.WebServicesABB.Controllers
{
    [ApiController]
    [Route("/Api/Usuario")]
    public class UsuarioController{
        [HttpGet]
        [Produces("application/xml")]
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
        [HttpGet("{pUsuario}/{pPassword}")]
        public int GetByUsuarioAndPassword(string pUsuario,string pPassword)
        {
            try{
                UsuariosLN usuario = new UsuariosLN();
                return usuario.GetUsuarioId(pUsuario,pPassword);
            }
            catch(Exception ex){
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
                return -1; 
            }
        }
        [HttpPost]
        public void Post([FromBody]string value)
        {
            
        }
        [HttpPut]
        public void Put(int id,[FromBody] string value)
        {

        }
        [HttpDelete]
        public void Delete(int id)
        {
            
        }
    }
}
