using System;
using System.Data;
using System.Data.SqlClient;
using TP7_Grupo_8.Conexion;

namespace TP7_Grupo_8
{
    public class GestionSucursales
    {
        private readonly AccesoDatos datos = new AccesoDatos();

        public DataTable ObtenerTabla(string nombreTabla, string consultaSQL)
        {
            DataSet dataSet = new DataSet();
            AccesoDatos datos = new AccesoDatos();
            SqlDataAdapter sqlDataAdapter = datos.ObtenerAdapdator(consultaSQL);
            sqlDataAdapter.Fill(dataSet, nombreTabla);
            return dataSet.Tables[nombreTabla];
        }

        public DataTable ObtenerTodosLosDatos()
        {
            string consulta = "SELECT * FROM Sucursal";
            return ObtenerTabla("Sucursal", consulta);
        }
    }
}
