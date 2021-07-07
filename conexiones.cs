using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ROP_Informe
{
    public static class conexiones
    {
        public static SqlConnection conexion;
        public static SqlCommand comando;
        public static string strConexion, consulta;

        public static void crearConexion()
        {
            strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
            conexion = new SqlConnection(strConexion);
            conexion.Open();
        }

        public static void crearConexionBI()
        {
            strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP_BI"].ToString();
            conexion = new SqlConnection(strConexion);
            conexion.Open();
        }
    }
}