using Entidades.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Core
{
    public class RolDA
    {
        private SqlConnectionStringBuilder builder;
        public RolDA() {
            builder = new SqlConnectionStringBuilder()
            {
                DataSource = "34.172.53.168",
                TrustServerCertificate = true,
                UserID = "sqlserver",
                Password = "(n?V(R1f<;#XqlCh",
                InitialCatalog = "DBCatalogo"
            };
        }

        public List<Rol> ListarRoles()
        {
            List<Rol> entidades = new List<Rol>();    
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            SqlCommand command = new SqlCommand("ListarRol", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure; 
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Rol entidad = new Rol();
                entidad.IdRol = Convert.ToInt32(reader["IdRol"]);
                entidad.DesRol = Convert.ToString(reader["DesRol"]);
                entidades.Add(entidad);
            }
            connection.Close();
            return entidades;
        }
    }
}
