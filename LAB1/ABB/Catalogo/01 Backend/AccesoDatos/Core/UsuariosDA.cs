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

namespace AccesoDatos.Core
{
    public class UsuariosDA
    {
        public Usuarios LlenarEntidad(IDataReader reader)
        {
            Usuarios usuario = new Usuarios();
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdUsuario'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1) && !Convert.IsDBNull(reader["IdUsuario"]))
            {
                usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
            }
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'CodUsuario'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1) && !Convert.IsDBNull(reader["CodUsuario"]))
            {
                usuario.CodUsuario = Convert.ToString(reader["CodUsuario"]);
            }
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'Nombres'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1) && !Convert.IsDBNull(reader["Nombres"]))
            {
                usuario.Nombres = Convert.ToString(reader["Nombres"]);
            }
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdRol'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1) && !Convert.IsDBNull(reader["IdRol"]))
            {
                usuario.IdRol = Convert.ToInt32(reader["IdRol"]);
            }
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'DesRol'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1) && !Convert.IsDBNull(reader["DesRol"]))
            {
                usuario.DesRol = Convert.ToString(reader["DesRol"]);
            }
            return usuario;
        }
        public List<Usuarios> ListarUsuarios()
        {
            List <Usuarios> ListaEntidad = new List<Usuarios> ();
            Usuarios entidad = null;
    
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand(" ListarUsuarios ",conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = LlenarEntidad(reader);
                        ListaEntidad.Add(entidad);
                    }
                }
                conexion.Close();
            }
            return ListaEntidad;
        }
    }
}