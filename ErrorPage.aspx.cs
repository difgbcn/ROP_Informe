using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace ROP_Informe 
{
    public partial class ErrorPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            conexiones.crearConexion();
            conexiones.consulta = "ROP_Flags_ConsultaTodos";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvTablas.DataSource = dr;
            grvTablas.DataBind();

            conexiones.conexion.Close();
        }
    }
}