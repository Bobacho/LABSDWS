using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LogicaNegocio.Core;
using Entidades.Core;
using Microsoft.AspNetCore.Mvc;

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
