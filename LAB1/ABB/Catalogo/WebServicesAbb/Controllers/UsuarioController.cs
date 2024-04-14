using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.LogicaNegocio.Core;

namespace ABB.Catalogo.WebServicesABB.Controllers
{
    public class UsuarioController: ApiController{
        public IEnumerable<string> Get(){
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = new UsuariosLN().ListarUsuarios();
        }
        public string Get(int id)
        {
            return "value";
        }
        public void Post([FromBody]string value)
        {
            
        } 
        public void Put(int id,[FromBody] string value)
        {

        }
        public void Delete(int id)
        {
            
        }
    }
}
