using AccesoDatos.Core;
using Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Core
{
    public class RolLN
    {
        public List<Rol> ListaRol()
        {
            try
            {
                RolDA roles = new RolDA();
                return roles.ListarRoles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
