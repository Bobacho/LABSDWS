using System;
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
    public class UsuariosDA
    {
        private SqlConnectionStringBuilder builder;
        public UsuariosDA()
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
        public Usuarios LlenarEntidad(IDataReader reader)
        {
            Usuarios usuario = new Usuarios();
            Dictionary<string, string> columnMappings = new Dictionary<string, string>
            {
                { "IdUsuario", nameof(usuario.IdUsuario) },
                { "CodUsuario", nameof(usuario.CodUsuario) },
                { "ClaveTxt" , nameof(usuario.ClaveTxt)},
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
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return ListaEntidad;
        }
        public Usuarios GetUsuarioById(int id)
        {
            Usuarios entidad = null;
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            SqlCommand command = new SqlCommand("paUsuario_BuscaUserId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ParamUsuario", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                entidad = LlenarEntidad(reader);
            }
            connection.Close();
            return entidad;
        }
        public Usuarios BuscarUsuario(Usuarios usuario)
        {
            Usuarios SegSSOMUsuario = null;
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            SqlCommand command = new SqlCommand("paUsuario_BuscaCodUserClave", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ParamPass", usuario.Clave);
            command.Parameters.AddWithValue("@ParamUsuario", usuario.CodUsuario);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SegSSOMUsuario = LlenarEntidad(reader);
            }
            connection.Close();
            return SegSSOMUsuario;
        }
        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            try
            {
                byte[] UserPass = EncriptacionHelper.EncriptarByte(pPassword);
                Console.WriteLine(UserPass.ToString());
                int returnedVal = 0;
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlCommand command = new SqlCommand("paUsuario_BuscaCodUserClave", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ParamUsuario", pUsuario);
                command.Parameters.AddWithValue("@ParamPass", UserPass);
                connection.Open();
                returnedVal = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                Console.WriteLine(returnedVal);
                return Convert.ToInt32(returnedVal);
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine("Error en DA:" + innerException);
                return -1;
            }
        }

        public Usuarios InsertarUsuario(Usuarios usuarios)
        {
            try
            {
                byte[] UserPass = EncriptacionHelper.EncriptarByte(usuarios.ClaveTxt);
                usuarios.Clave = UserPass;
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlCommand command = new SqlCommand("paUsuario_Insertar", connection);
                command.CommandType = CommandType.StoredProcedure;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Clave",usuarios.Clave},
                    { "@CodUsuario",usuarios.CodUsuario},
                    { "@Nombres" ,usuarios.Nombres},
                    { "@IdRol" , usuarios.IdRol}
                };
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return usuarios;
            }
            catch (Exception e)
            {
                string innerExcetion = e.InnerException == null ? "" : e.InnerException.ToString();
                Console.WriteLine("Error en DA:" + innerExcetion);
                return new Usuarios();
            }
        }

        public Usuarios ModificarUsuario(int id, Usuarios usuarios)
        {
            try
            {
                try
                {
                    byte[] UserPass = EncriptacionHelper.EncriptarByte(usuarios.ClaveTxt);
                    usuarios.Clave = UserPass;
                    SqlConnection connection = new SqlConnection(builder.ConnectionString);
                    SqlCommand command = new SqlCommand("paUsuario_Modificar", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@IdUsuario" , id},
                    { "@Clave",usuarios.Clave},
                    { "@CodUsuario",usuarios.CodUsuario},
                    { "@Nombres" ,usuarios.Nombres},
                    { "@IdRol" , usuarios.IdRol}
                };
                    foreach (var item in parameters)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return usuarios;
                }
                catch (Exception e)
                {
                    string innerExcetion = e.InnerException == null ? "" : e.InnerException.ToString();
                    Console.WriteLine(innerExcetion);
                    return new Usuarios();
                }
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                Console.WriteLine(innerException);
                return new Usuarios();
            }
        }
        public void BorrarUsuario(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                SqlCommand command = new SqlCommand("DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario;", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdUsuario", id);
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
    }
}
