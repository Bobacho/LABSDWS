using Entidades.Core;
using AccesoDatos.Core;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class UsuariosLN
    {
        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                UsuariosDA usuarios = new UsuariosDA();
                return usuarios.ListarUsuarios();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null)
                ? "" : ex.InnerException.ToString();
                return lista;
            }
        }
        public Usuarios BuscarUsuario(Usuarios Usuario)
        {
            try
            {
                return new UsuariosDA().BuscarUsuario(Usuario);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw;
            }
        }
        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            try
            {
                UsuariosDA usuario = new UsuariosDA();
                return usuario.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Error en LN:" + innerException);
                return -1;
            }
        }

        public Usuarios GetUsuariosById(int id)
        {
            try
            {
                UsuariosDA usuariosDA = new UsuariosDA();
                return usuariosDA.GetUsuarioById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Usuarios InsertarUsuario(Usuarios usuarios)
        {
            try
            {
                return new UsuariosDA().InsertarUsuario(usuarios);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
                return new Usuarios();
            }
        }

        public Usuarios ModificarUsuario(int id, Usuarios usuarios)
        {
            try
            {
                return new UsuariosDA().ModificarUsuario(id, usuarios);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Error en LN:" + innerException);
                return new Usuarios();
            }
        }

        public void BorrarUsuario(int id)
        {
            try
            {
                new UsuariosDA().BorrarUsuario(id);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }
    }
}
