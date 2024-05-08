using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Core;
using ABB.Catalogo.Utiles.Helpers;
using Microsoft.IdentityModel.Protocols;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace AccesoDatos.Core
{
    public class ProductoDA
    {
        private SqlConnectionStringBuilder builder;
        public ProductoDA()
        {
            builder = new SqlConnectionStringBuilder()
            {
                DataSource = "34.172.53.168",
                TrustServerCertificate = true,
                UserID = "sqlserver",
                Password = "(n?V(R1f<;#XqlCh",
                InitialCatalog = "DBCatalogo"
            };
        }
        public Producto LlenarEntidad(IDataReader reader)
        {
            Producto producto = new Producto();
            Dictionary<string, string> columnMappings = new Dictionary<string, string>
            {
                { "IdProducto", nameof(producto.IdProducto) },
                { "DescripcionTecnica", nameof(producto.DescripcionTecnica)},
                { "IdCategoria", nameof(producto.IdCategoria)},
                { "DescCategoria", nameof(producto.DescCategoria)},
                { "MarcaProducto", nameof(producto.MarcaProducto) },
                { "LineaProducto" , nameof(producto.LineaProducto)},
                {  "ModeloProducto", nameof(producto.ModeloProducto) },
                { "GarantiaProducto", nameof(producto.GarantiaProducto) },
                { "Precio", nameof(producto.Precio) },
                { "NomProducto",nameof(producto.NomProducto)}
            };

            foreach (var columnMapping in columnMappings)
            {
                reader.GetSchemaTable().DefaultView.RowFilter = $"ColumnName = '{columnMapping.Key}'";

                if (reader.GetSchemaTable().DefaultView.Count == 1 && !Convert.IsDBNull(reader[columnMapping.Key]))
                {
                    var propertyInfo = typeof(Producto).GetProperty(columnMapping.Value);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(producto, Convert.ChangeType(reader[columnMapping.Key], propertyInfo.PropertyType));
                    }
                }
            }

            return producto;
        }
        public void BorrarProducto(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlCommand command = new SqlCommand("DELETE FROM PRODUCTO WHERE IdProducto = @IdProducto;", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdProducto", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
            }
        }

        public Producto GetProductoById(int id)
        {
            throw new NotImplementedException();
        }

        public Producto InsertarProducto(Producto producto)
        {
            try
            {
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlCommand command = new SqlCommand("paProducto_Insertar", connection);
                command.CommandType = CommandType.StoredProcedure;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@GarantiaProducto",producto.GarantiaProducto},
                    { "@MarcaProducto", producto.MarcaProducto },
                    { "@LineaProducto" ,producto.LineaProducto},
                    { "@NomProducto" , producto.NomProducto},
                    { "@Precio", producto.Precio},
                    { "@DescCategoria", producto.DescCategoria}
                };
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return producto;
            }
            catch (Exception e)
            {
                string innerExcetion = e.InnerException == null ? "" : e.InnerException.ToString();
                Console.WriteLine("Error en DA:" + innerExcetion);
                return new Producto();
            }
        }

        public List<Producto> ListarProductos()
        {
            List<Producto> ListaEntidad = new List<Producto>();
            Producto entidad = null;
            Console.WriteLine("Inicio");
            try
            {
                SqlConnection conexion = new SqlConnection(builder.ConnectionString);
                SqlCommand comando = new SqlCommand("EXEC paListarProductos", conexion);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    entidad = LlenarEntidad(reader);
                    ListaEntidad.Add(entidad);
                    Console.WriteLine(entidad.ToString());
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return ListaEntidad;
        }

        public Producto ModificarProducto(int id, Producto producto)
        {
            try
            {
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlCommand command = new SqlCommand("paProducto_Modificar", connection);
                command.CommandType = CommandType.StoredProcedure;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdProducto" , id},
                    { "@GarantiaProducto",producto.GarantiaProducto},
                    { "@MarcaProducto", producto.MarcaProducto },
                    { "@LineaProducto" ,producto.LineaProducto},
                    { "@DescripcionTecnica", producto.DescripcionTecnica },
                    { "@NomProducto" , producto.NomProducto},
                    { "@Precio", producto.Precio},
                    { "@DescCategoria", producto.DescCategoria}
                };
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return producto;
            }
            catch (Exception e)
            {
                string innerExcetion = e.InnerException == null ? "" : e.InnerException.ToString();
                Console.WriteLine("Error en DA:" + innerExcetion);
                return new Producto();
            }
        }
    }
}
