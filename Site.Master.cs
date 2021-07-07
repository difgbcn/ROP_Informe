using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ROP_Informe
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("Default.aspx");
            if (!UsuarioLogueado)
                Response.Redirect("Login.aspx");
        }

        public bool UsuarioLogueado { get { return Session["usuario"]!=null; } }

        public bool validarUsuario
        {
            get {
                bool devolver;

                //conexiones.crearConexion();
                //conexiones.consulta = "sp_ROP_ConfiguracionUsuarioConsulta";
                //conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                //conexiones.comando.CommandType = CommandType.StoredProcedure;

                //SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
                //parametroUsuario.Value = Environment.UserName;
                //conexiones.comando.Parameters.Add(parametroUsuario);
                //SqlDataReader dr = conexiones.comando.ExecuteReader();
                //if (dr.HasRows)
                    devolver= true;
                //else
                //    devolver= false;
                //dr.Close();
                //conexiones.conexion.Close();
                return devolver;
            }
        }
    }
}