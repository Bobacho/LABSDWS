using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Core;
using Microsoft.IdentityModel.Protocols;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace AccesoDatos.Core
{
    public class UsuariosDA
    {
        public Usuarios LlenarEntidad(IDataReader reader)
        {
            Usuarios usuario = new Usuarios();
            Dictionary<string, string> columnMappings = new Dictionary<string, string>
            {
                { "IdUsuario", nameof(usuario.IdUsuario) },
                { "CodUsuario", nameof(usuario.CodUsuario) },
                {  "Nombres", nameof(usuario.Nombres) },
                { "IdRol", nameof(usuario.IdRol) },
                { "DesRol", nameof(usuario.DesRol) }
            };

            foreach (var columnMapping in columnMappings)
            {
                reader.GetSchemaTable().DefaultView.RowFilter = $"ColumnName = '{columnMapping.Key}'";

                if (reader.GetSchemaTable().DefaultView.Count == 1 && !Convert.IsDBNull(reader[columnMapping.Key]))
                {
                    var propertyInfo = typeof(Usuarios).GetProperty(columnMapping.Value);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(usuario, Convert.ChangeType(reader[columnMapping.Key], propertyInfo.PropertyType));
                    }
                }
            }

            return usuario;
        }

        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> ListaEntidad = new List<Usuarios>();
            Usuarios entidad = null;
            Console.WriteLine("Inicio");
            try {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
                {
                    DataSource = "localhost\\SQLEXPRESS",
                    TrustServerCertificate = true,
                    UserID = "sa",
                    Password = "123",
                    InitialCatalog = "DBCatalogo"
                };
                SqlConnection conexion = new SqlConnection(builder.ConnectionString);
                SqlCommand comando = new SqlCommand("EXEC ListarUsuarios", conexion);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    entidad = LlenarEntidad(reader);
                    ListaEntidad.Add(entidad);
                    Console.WriteLine(entidad);
                }
                conexion.Close();
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
            return ListaEntidad;
        }
    }
}