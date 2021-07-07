using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace ROP_Informe 
{
    public partial class ErrorPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            //if (exc is HttpUnhandledException)
            //{
            lblMensajeError.Text = "Error: ";// + exc.Message;
            //}
            Server.ClearError();

            //lblMensajeError.Text = "Error"; // Session["mensajeError"].ToString();
        }
    }
}