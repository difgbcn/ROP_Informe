using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ROP_Informe
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Session_Start(Object sender, EventArgs e)
        //{
        //    // Limpiar las variables de sesión
        //    Session.Clear();
        //}

        void Application_End(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
        }
    }
}