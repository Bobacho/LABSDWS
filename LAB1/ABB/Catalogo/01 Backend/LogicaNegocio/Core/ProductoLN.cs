using AccesoDatos.Core;
using Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Core
{
    public class ProductoLN
    {
        public List<Producto> ListarProducto()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                ProductoDA productos = new ProductoDA();
                return productos.ListarProductos();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null)
                ? "" : ex.InnerException.ToString();
                return lista;
            }
        }

        public Producto GetProductoById(int id)
        {
            try
            {
                ProductoDA productoDA = new ProductoDA();
                return productoDA.GetProductoById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Producto InsertarProducto(Producto producto)
        {
            try
            {
                return new ProductoDA().InsertarProducto(producto);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
                return new Producto();
            }
        }

        public Producto ModificarProducto(int id, Producto producto)
        {
            try
            {
                return new ProductoDA().ModificarProducto(id, producto);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Error en LN:" + innerException);
                return new Producto();
            }
        }

        public void BorrarProducto(int id)
        {
            try
            {
                new ProductoDA().BorrarProducto(id);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }
    }
}
