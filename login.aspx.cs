using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ROP_Informe
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Add("usuario", "usuarioPassw");
            Response.Redirect("default.aspx");
        }

            protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session.Add("usuario", "pepepep");
            //Session.Add(txtUsuario.Text, txtPassword.Text);
            Response.Redirect("default.aspx");
        }
    }
}