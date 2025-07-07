using System.Dynamic;
using Newtonsoft.Json;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Threading;
using System.Threading.Tasks;

namespace ROP_Informe
{
    public partial class Pruebas : System.Web.UI.Page
    {

        public static string WSDominio = "";
        public static string WSUsuario = "";
        public static string WSContrasena = "";

        public static int progreso = 0;

        protected override void OnLoad(EventArgs e)
        {
            Page.Title = "INFORME ROP";
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                WSDominio = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["WSDomain"]);
                WSUsuario = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["WSUser"]);
                WSContrasena = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["WSPassword"]);
           
            }
        }

        protected void buscar_1(object sender, EventArgs e)
        {
            //string pepe = "";
            //myTiempo.InnerHtml = "1000";
            for (long vencimiento = 0; vencimiento <= 10000000000000; vencimiento++)
            {
                myTiempo.Value = vencimiento.ToString();
                //pepe = "";
            }
        }

        protected void buscar_2(object sender, EventArgs e)
        {
            //string pepe = "";
            //myTiempo.InnerHtml = "1000";
            for (long vencimiento = 0; vencimiento <= 1000000000000; vencimiento++)
            {
                myTiempo.Value = vencimiento.ToString();
                //pepe = "";
            }
        }
    }
}