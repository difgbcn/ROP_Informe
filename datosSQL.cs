using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ROP_Informe
{
    public class datosSQL 
    {
        public static Hashtable valoresConfiguracion;

        public static void datosConfigurados(string empresa, string articulos, string usuarioSQL)
        {
            valoresConfiguracion = new Hashtable();
            try
            {
                conexiones.crearConexionBI();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_BI_FamiliasSubfamilias";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@empresa", empresa);
                conexiones.comando.Parameters.AddWithValue("@articulos", articulos);
                conexiones.comando.Parameters.AddWithValue("@usuario", usuarioSQL);
                conexiones.comando.ExecuteNonQuery();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_DatosArticulosFamiliasSubfamilias";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@usuario", usuarioSQL);
                conexiones.comando.ExecuteNonQuery();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();
            }
            catch (Exception ex)
            {
                conexiones.conexion.Close();
            }
        }
    }
}
