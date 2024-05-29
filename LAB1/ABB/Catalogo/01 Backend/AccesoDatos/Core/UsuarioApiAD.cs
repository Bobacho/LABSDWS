using Entidades.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Core
{
    public class UsuarioApiAD
    {
        private SqlConnectionStringBuilder builder;
        public UsuarioApiAD()
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
        public UsuariosApi BuscaUsuarioApi(UsuariosApi ParamUserApi)
        {
            UsuariosApi entidad = new UsuariosApi();
            SqlConnection conexion = new SqlConnection(builder.ConnectionString);
            SqlCommand comando = new SqlCommand("users_UsuarioApi", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Codigo", ParamUserApi.Codigo);
            comando.Parameters.AddWithValue("@UserName", ParamUserApi.UserName);
            comando.Parameters.AddWithValue("@Clave", ParamUserApi.Clave);
            comando.Parameters.AddWithValue("@Nombre", ParamUserApi.Nombre);
            comando.Parameters.AddWithValue("@Rol", ParamUserApi.Rol);
            conexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                entidad = new UsuariosApi();
                entidad.Codigo = Convert.ToInt32(reader["Codigo"]);
                entidad.UserName = reader["UserName"].ToString();
                entidad.Clave = reader["Clave"].ToString();
                entidad.Nombre = reader["Nombre"].ToString();
                entidad.Rol = reader["Rol"].ToString();
            }
            conexion.Close();
            return entidad;
        }
    }
}
