using Entidades.Core;
using ABB.Catalogo.AccesoDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class OpcionLN
    {
        public List<Opcion> ListaOpciones()
        {
            try
            {
                return new OpcionDA().ListaOpciones();
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw;
            }
        }
    }
}
