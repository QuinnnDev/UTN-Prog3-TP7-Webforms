using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace TP7_Grupo_8.Conexion
{
    public class AccesoDatos
    {

        string stringConexion = @"Data Source=localhost\\sqlexpress;Initial Catalog=BDSucursales;Integrated Security=True";

        public SqlConnection ObtenerConexion()
        {
            SqlConnection sqlConnection = new SqlConnection(stringConexion);
            try
            {
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}