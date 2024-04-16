using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Core;
using AccesoDatos.Core;

namespace LogicaNegocio.Core
{
    public class UsuariosLN
    {
        public List<Usuarios> ListarUsuarios()
        {
            List <Usuarios> lista = new List<Usuarios> ();
            try
            {
                UsuariosDA usuarios = new UsuariosDA();
                return usuarios.ListarUsuarios();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" :
                ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir(&quot;Error en Logica de Negocio: &quot; + ex.Message + &quot;. &quot;
                return lista;
            }
        }
    }
}
