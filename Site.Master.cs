using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace ROP_Informe
{
    public partial class SiteMaster : MasterPage
    {
        public class JSONCabeceraAceler
        {
            public string QUOTATIONID { get; set; }
            public DateTime CREATEDDATETIME { get; set; }
            public string MASTEROBRASID { get; set; }
            public string JOBSITENAME { get; set; }
            public string MASTEROBRASNAME { get; set; }
            public string CURRENCYCODE { get; set; }
            public string COMPANY { get; set; }
            public string DELEGATION { get; set; }
            public string COSTCENTER { get; set; }
            public int CUSTOMERTYPE { get; set; }
            public string CUSTOMER { get; set; }
            public string CUSTOMERNAME { get; set; }
            public decimal KILOMETER { get; set; }
            public int DLVMODE { get; set; }
            public int TRANSPMGM { get; set; }
            public int TRANSPTYPE { get; set; }
            public string PAYMENT { get; set; }
            public string PAYMENTSCH { get; set; }
            public List<JSONLineasAceler> LINE { get; set; }
        }

        public class JSONLineasAceler
        {
            public string CHAPTERID { get; set; }
            public string CHAPTERNAME { get; set; }
            public int CHAPTERTYPE { get; set; }
            public string ITEMID { get; set; }
            public DateTime BASEDATE { get; set; }
            public decimal SALESQTY { get; set; }
            public int ESTIMLENGTH { get; set; }
            public decimal DAYPRICE { get; set; }
            public decimal SALESPRICE { get; set; }
            public decimal LINEAMOUNT { get; set; }
            public decimal PLYWOODCHARGE { get; set; }
            public decimal LEGALCHARGE { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonString ="";
            string strConexion;
            SqlCommand comando;
            SqlConnection conexion;
            string ID = "";

            // SI LLAMA ACELER OBLIGO A LIMPIAR LAS VARIABLES DE SESSION
            if (Request.QueryString.Count == 3)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
                    ID = Request.QueryString["ID"].ToString().ToUpper();

                if (Session["IDEnviado"] != null)
                {
                    if (ID != Session["IDEnviado"].ToString())
                    {
                        //Session.Clear();
                        Session.Add("usuario", "");
                    }
                }
            }

            if (!UsuarioLogueado)
            {
                Session.Clear();
                Session.Add("usuario", "usuarioPassw");
                string idioma = "";
                string tipo = "";
                string empresa = "";
                string numero = "";
                string usuario = "";

                Session.Add("cabeceraEnviado", "");
                Session.Add("IDEnviado", "");
                Session.Add("idiomaEnviado", "");
                Session.Add("tipoEnviado", "");
                Session.Add("empresaEnviado", "");
                Session.Add("numeroEnviado", "");
                Session.Add("usuarioAXAPTA", "");

                if (Request.QueryString.Count == 3)
                {
                    tipo = "1"; // SERÁ OFERTA PORQUE VIENE DEL ACELER
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
                        ID = Request.QueryString["ID"].ToString().ToUpper();
                    if (!String.IsNullOrEmpty(Request.QueryString["IDIOMA"].ToString()))
                        idioma = Request.QueryString["IDIOMA"].ToString().ToUpper();
                    if (!String.IsNullOrEmpty(Request.QueryString["USUARIO"].ToString()))
                        usuario = Request.QueryString["USUARIO"].ToString();

                    // RECUPERAR EL JSON QUE HEMOS GUARDADO EN LA BD
                    strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ACELER_AccesoWeb_Consulta";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader dr;
                    dr = comando.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            jsonString = dr["JSON"].ToString();
                            break;
                        }
                    }
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    Session["cabeceraEnviado"] = jsonString;
                }
                else if (Request.QueryString.Count == 4)
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["IDIOMA"].ToString()))
                        idioma = Request.QueryString["IDIOMA"].ToString().ToUpper();
                    if (!String.IsNullOrEmpty(Request.QueryString["TIPO"].ToString()))
                        tipo = Request.QueryString["TIPO"].ToString();
                    if (!String.IsNullOrEmpty(Request.QueryString["EMPRESA"].ToString()))
                        empresa = Request.QueryString["EMPRESA"].ToString();
                    if (!String.IsNullOrEmpty(Request.QueryString["NUMERO"].ToString()))
                        numero = Request.QueryString["NUMERO"].ToString();
                    usuario = "AXAPTA";
                }
                else if (Request.QueryString.Count == 5)
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["IDIOMA"].ToString()))
                        idioma = Request.QueryString["IDIOMA"].ToString().ToUpper();
                    if (!String.IsNullOrEmpty(Request.QueryString["TIPO"].ToString()))
                        tipo = Request.QueryString["TIPO"].ToString();
                    if (!String.IsNullOrEmpty(Request.QueryString["EMPRESA"].ToString()))
                        empresa = Request.QueryString["EMPRESA"].ToString();
                    if (!String.IsNullOrEmpty(Request.QueryString["NUMERO"].ToString()))
                        numero = Request.QueryString["NUMERO"].ToString();
                    if (!String.IsNullOrEmpty(Request.QueryString["USUARIO"].ToString()))
                        usuario = Request.QueryString["USUARIO"].ToString();

                    if (usuario.Trim() == "")
                        usuario = "AXAPTA";
                }
                else if (Request.QueryString.Count == 0)
                {
                    usuario = "ROP";
                }

                string queMeEstaLlegando = "";
                foreach (String key in Request.QueryString.AllKeys)
                    queMeEstaLlegando = queMeEstaLlegando + "Key: " + key + " - Value: " + Request.QueryString[key] + " // ";

                Session["IDEnviado"] = ID;
                Session["idiomaEnviado"] = idioma;
                Session["tipoEnviado"] = tipo;
                Session["empresaEnviado"] = empresa;
                Session["numeroEnviado"] = numero;
                Session["usuarioAXAPTA"] = queMeEstaLlegando;

                //Session.Add("IDEnviado", ID);
                //Session.Add("idiomaEnviado", idioma);
                //Session.Add("tipoEnviado", tipo);
                //Session.Add("empresaEnviado", empresa);
                //Session.Add("numeroEnviado", numero); 
                //Session.Add("usuarioAXAPTA", queMeEstaLlegando);

                // usuario = "PRUEBA USUARIO ENVIADO";
                int arroba = 0;
                arroba = usuario.IndexOf("@");
                if (arroba >= 0)
                    usuario = usuario.Substring(0, arroba);
                Session.Add("usuarioEnviado", usuario.Replace("@alsina.com", "").Replace("@alsina.es", ""));

                //bool es = validarUsuario;
                Response.Redirect("Informe.aspx");
            }
        }

        public bool UsuarioLogueado { get { return (Session["usuario"] != null && Session["usuario"].ToString() != ""); } }
      
        protected override void OnInit(EventArgs e)
        {
            //Session["usuario"] = "";
            //if (!UsuarioLogueado)
            //    Response.Redirect("Login.aspx");
            //base.OnInit(e);
        }

        public bool validarUsuario
        {
            get
            {
                string strConexion;
                SqlConnection conexion;
                SqlCommand comando;
                bool devolver;
               
                menuConfiguracion.Visible = false;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionUsuarioConsulta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
                parametroUsuario.Value = Session["usuarioEnviado"].ToString();
                comando.Parameters.Add(parametroUsuario);
                if (System.Web.HttpContext.Current.Session["tipoEnviado"].ToString() != "")
                    comando.Parameters.AddWithValue("@Acceso", "AXAPTA");
                else
                    comando.Parameters.AddWithValue("@Acceso", "WEB");
                comando.Parameters.AddWithValue("@Entorno", Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["TipoVersion"]).ToUpper());
                comando.Parameters.AddWithValue("@TipoCalculo", System.Web.HttpContext.Current.Session["Concepto"].ToString());
                comando.Parameters.AddWithValue("@Empresa", System.Web.HttpContext.Current.Session["Empresa"].ToString());
                comando.Parameters.AddWithValue("@Numero", System.Web.HttpContext.Current.Session["Numero"].ToString());
                comando.Parameters.AddWithValue("@incluir", 0);
                comando.Parameters.AddWithValue("@usuarioAXAPTA", Session["usuarioAXAPTA"].ToString());
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToBoolean(dr["USR_Visualizar"])) 
                    {
                        if (System.Web.HttpContext.Current.Session["tipoEnviado"].ToString() != "")
                        {
                            devolver = false;
                            menuConfiguracion.Visible = false;
                        }
                        else
                        {
                            devolver = true;
                            menuConfiguracion.Visible = true;
                        }
                    }
                    else
                    {
                        devolver = false;
                        menuConfiguracion.Visible = false;
                    }
                }
                else
                {
                    devolver = false;
                    menuConfiguracion.Visible = false;
                }
                dr.Close();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                return devolver;
            }
        }
    }

    public interface IActionResult
    {
    }
}