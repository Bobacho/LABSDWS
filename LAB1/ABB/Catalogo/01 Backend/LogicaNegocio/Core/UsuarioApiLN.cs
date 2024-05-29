using Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Core;

namespace LogicaNegocio.Core
{
    public class UsuarioApiLN
    {
        public UsuariosApi BuscaUsuarioApi(UsuariosApi usuarioAutentica)
        {
            return new UsuarioApiAD().BuscaUsuarioApi(usuarioAutentica);
        }
    }
}
