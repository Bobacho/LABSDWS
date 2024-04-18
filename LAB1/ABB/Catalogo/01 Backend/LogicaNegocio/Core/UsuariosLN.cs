using ABB.Catalogo.Entidades.Core;
using AccesoDatos.Core;

namespace ABB.Catalogo.LogicaNegocio.Core{
    public class UsuariosLN{
        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios> ();
            try{
                UsuariosDA usuarios = new UsuariosDa();
                return usuarios.ListarUsuarios();
            }
            catch(Exception ex)
            {
                string innerException = (ex.InnerException == null) 
                ? "" : ex.InnerException.ToString();
                return lista;
            }
        }
        public int GetUsuarioId(string pUsuario,string pPassword)
        {
            try{
                UsuariosDA usuario = new UsuariosDA();
                return usuario.GetUsuarioId(pUsuario,pPassword);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString(); 
                return -1;
            }
        }
    }
}