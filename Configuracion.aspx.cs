using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Configuration;

namespace ROP_Informe
{

    public partial class Configuracion : System.Web.UI.Page
    {
        public static string nombreInforme = "";
        public static string ficheroExcel = "";

        public static string IDUsuarioBorrar = "";
        public static string IDAbonoBorrar = "";

        // GRIDS
        int COLGRID_USR_ID = 0;
        int COLGRID_USR_UsuarioRed = 1;
        int COLGRID_USR_Visualizar = 2;
        int COLGRID_USR_Exportar = 3;
        int COLGRID_USR_Importar = 4;
        int COLGRID_USR_Eliminar = 5;
        int COLGRID_USR_Elegir = 6;
        int COLGRID_USR_Abono = 7;
        int COLGRID_USR_Tecnico = 8;
        //int COLGRID_USR_btnEditar = 7;
        //int COLGRID_USR_btnEliminar = 8;

        int COLGRID_ID = 0;
        int COLGRID_Familia = 1;
        int COLGRID_Subfamilia = 2;
        int COLGRID_ART_ID = 3;
        //int COLGRID_CFGSERV_Tipo = 4;
        //int COLGRID_SER_btnEditar = 5;
        //int COLGRID_SER_btnEliminar = 6;

        // Transporte
        public static int transporsteID = -1;
        int CAMPO_IDTransporte = 0;
        int CAMPO_Meses = 1;
        int CAMPO_Desvio = 2;
        int CAMPO_CoeficienteVuelta = 3;
        int CAMPO_Fecha = 4;
        int CAMPO_KilometrosMinimos = 5;

        //int COLGRID_CFGTRA_ID = 0;
        //int COLGRID_BU = 1;
        //int COLGRID_Empresa = 2;
        //int COLGRID_Delegacion = 3;
        //int COLGRID_Margen = 4;
        //int COLGRID_Desde = 5;
        //int COLGRID_Hasta = 6;
        //int COLGRID_Distancia = 7;
        //int COLGRID_Prop = 8;
        //int COLGRID_Valor = 9;
        //int COLGRID_Desvio = 10;
        //int COLGRID_TRA_btnEditar = 11;
        //int COLGRID_TRA_btnEliminar = 12;

        //int COLGRID_TRANS_ID = 0;
        //int COLGRID_TRANS_Subfamilia = 4;
        //int COLGRID_TRANS_btnEditar = 5;
        //int COLGRID_TRANS_btnEliminar=6;

        int COLGRID_MOV_ID = 0;
        int COLGRID_MOV_Signo = 3;
        int COLGRID_MOV_Dias = 4;

        int COLGRID_PANEL_ID_Indice = 0;
        //int COLGRID_PANEL_ID = 1;
        //int COLGRID_PANEL_Descripcion = 2;
        int COLGRID_PANEL_Estandar = 3;

        // EXCELS
        //int COL_Version = 0;
        int COL_NombreVersion = 1;
        int COL_Desde = 2;
        //int COL_Hasta = 3;
        //int COL_Alcance = 4;

        int COL_Concepto = 0;
        int COL_Bu = 1;
        int COL_Empresa = 2;
        int COL_Familia = 3;
        int COL_Subfamilia = 4;
        int COL_Articulo = 5;
        int COL_Valorar = 6;
        int COL_ValorDesde = 7;
        int COL_ValorHasta = 8;
        int COL_Valor = 9;
        int COL_Moneda = 10;

        int COL_Concepto_Nivel2 = 0;
        int COL_BU_Nivel2 = 1;
        int COL_Empresa_Nivel2 = 2;
        int COL_Delegacion_Nivel2 = 3;
        int COL_Valor_Nivel2 = 4;

        int COL_Concepto_Nivel3 = 0;
        int COL_Signo_Nivel3 = 1;
        int COL_Dias_Nivel3 = 2;

        int COL_Concepto_Nivel4 = 0;
        int COL_Valor_Nivel4 = 1;

        int COL_FamiliaServicios = 0;
        int COL_SubfamiliaServicios = 1;
        int COL_ArticuloServicios = 2;
        int COL_TipoServicio = 3;

        int COL_TransporteMeses = 0;
        int COL_TransporteDesvio = 1;
        int COL_TransporteCoeficienteVuelta = 2;
        int COL_TransporteFecha = 3;
        int COL_TransporteKilometrosMinimos = 4;

        //int COL_TransporteBU = 0;
        //int COL_TransporteEmpresa = 1;
        //int COL_TransporteDelegacion = 2;
        //int COL_TransporteSubfamilia = 3;

        int COL_TransporteDatosBU = 0;
        int COL_TransporteDatosEmpresa = 1;
        int COL_TransporteDatosDelegacion = 2;
        int COL_TransporteDatosMargen = 3;
        int COL_TransporteDatosDesde = 4;
        int COL_TransporteDatosHasta = 5;
        int COL_TransporteDatosDistancia = 6;
        int COL_TransporteDatosPropuesto = 7;
        int COL_TransporteDatosValor = 8;
        int COL_TransporteDatosDesvio = 9;
        int COL_TransporteDatosVales = 10;
        int COL_TransporteDatosMeses = 11;
        int COL_TransporteDatosPropuesto12Meses = 12;
        int COL_TransporteDatosDesvio12Meses = 13;
        int COL_TransporteDatosVales12Meses = 14;

        int COL_AAFPaneles = 0;
        int COL_EstandarPaneles = 1;

        int COL_AjustePorObraTaxBU = 0;
        int COL_AjustePorObraTaxEmpresa = 1;
        int COL_AjustePorObraTaxDelegacion = 2;
        int COL_AjustePorObraTaxDuracionMinima = 3;
        int COL_AjustePorObraTaxDuracionMaxima = 4;
        int COL_AjustePorObraTaxAbonoMensual = 5;

        // Usuarios
        //int CAMPO_USR_ID = 0;
        //int CAMPO_USR_UsuarioRed = 1;
        int CAMPO_USR_Visualizar = 2;
        int CAMPO_USR_Exportar = 3;
        int CAMPO_USR_Importar = 4;
        int CAMPO_USR_Eliminar = 5;

        protected override void OnLoad(EventArgs e)
        {
            Page.Title = "CONFIGURACION";
            this.Title = "CONFIGURACIÓN";
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                errores.Visible = false;
                chkBoxActivos.Checked = true;
                //txtPropuesto.Enabled = false;
                txtUsuarioRed.Text = "";
                chkVisualizar.Checked = true;
                chkExportar.Checked = false;
                chkImportar.Checked = false;
                chkEliminar.Checked = false;
                chkElegirVersion.Checked = false;
                ViewState["FiltroVersion"] = "";
                ViewState["FiltroConceptoValor"] = "";
                ViewState["FiltroConceptoValorTaximetros"] = "";
                ViewState["FiltroVersionGeneral"] = "";
                ViewState["FiltroConceptoGeneral"] = "";
                ViewState["FiltroVersionTaximetros"] = "";
                ViewState["FiltroVersionServicios"] = "";
                ViewState["FiltroVersionPaneles"] = "";
                ViewState["FiltroBUGeneral"] = "";
                ViewState["FiltroEmpresaGeneral"] = "";
                ViewState["FiltroDelegacionGeneral"] = "";
                ViewState["FiltroVersionMovimientosGeneral"]="";
                ViewState["FiltroMovimiento"] = "";
                ViewState["FiltroEmpresaTransporte"] = "";
                ViewState["FiltroDelegacionTransporte"] = "";
                ViewState["FiltroDesdeTransporte"] = "";
                ViewState["FiltroDistanciaTransporte"] = "";
                ViewState["FiltroVersionBU"] = "";
                ViewState["FiltroVersionAT"] = "";

                cmbAfectaAbono.Items.Clear();
                cmbAfectaAbono.Items.Insert(0, new ListItem("ROP"));
                cmbAfectaAbono.Items.Insert(1, new ListItem("ROP Básico"));
                cmbAfectaAbono.Items.Insert(2, new ListItem("MB"));
      
                //rdbOperativo.Checked = true;
                rellenarGridGeneral();
                rellenarGrid();
                rellenarGridTaximetros();
                rellenarCombosVersion();
                rellenarGridServicios();
                rellenarGridTransporte();
                rellenarGridAjustes();
                rellenarGridBU();
                rellenarTransporteGeneral();
                //datosPaneles();
                rellenarGridPaneles();
                rellenarGridUsuarios();
                rellenarGridAbonos(-1,"");
                validarAccionUsuario();
                rellenarAjustesFechaMovimientos();
                rellenarHilos();
                rellenarGridVersiones();
                //rdbGFV.Checked = true;
                rellenarGridHistorico();
                rellenarGridDatosTransporte();
                btnAbrirExcel.Visible = false;
                btnAbrirExcelTransporte.Visible = false;
                btnAbrirExcelEtiquetas.Visible = false;

                rellenarRelacionTablas();
                //txtDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void rellenarRelacionTablas()
        {
            conexiones.crearConexion();
            conexiones.consulta = "ROP_Flags_ConsultaTodos";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvTablas.DataSource = dr;
            grvTablas.DataBind();
            dr.Close();

            conexiones.consulta = "SendDataToManypExecutions_Consulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader dr2 = conexiones.comando.ExecuteReader();
            grvTablasAxpata.DataSource = dr2;
            grvTablasAxpata.DataBind();
            dr2.Close();

            conexiones.conexion.Close();
        }

        public bool validarUsuarioAbono
        {
            get
            {
                bool devolver;

                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionUsuarioConsulta";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
                parametroUsuario.Value = Session["usuarioEnviado"].ToString(); // Environment.UserName;
                conexiones.comando.Parameters.Add(parametroUsuario);
                conexiones.comando.Parameters.AddWithValue("@incluir", 0);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToBoolean(dr["USR_Abono"]))
                        devolver = true;
                    else
                        devolver = false;
                }
                else
                    devolver = false;
                dr.Close();
                conexiones.conexion.Close();
                return devolver;
            }
        }

    protected void validarAccionUsuario()
        {
            //ImageButton imgBoton;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
            parametroUsuario.Value = Session["usuarioEnviado"].ToString(); // Environment.UserName;
            conexiones.comando.Parameters.Add(parametroUsuario);
            conexiones.comando.Parameters.AddWithValue("@incluir", 0);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                //cmbVersionGeneralExportar.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                //btnExcelGeneral.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                //btnAbrirExcelGeneral.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                //rdbOperativoGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                //rdbPruebaGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                //ficheroSeleccionadoGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                //btnImportarExcelGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                //cmbVersionGeneralEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                //btnExcelGeneralEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                //cmbVersionGeneralPruebas.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                //btnVersionGeneralReal.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);

                cmbVersionExportar.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                btnExcel.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                btnAbrirExcel.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                //rdbOperativo.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                //rdbPrueba.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                ficheroSeleccionado.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                btnImportarExcel.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                cmbVersionEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                btnExcelEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                //cmbVersionPruebas.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                //btnVersionReal.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);

                btnLimpiarUsuario.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                btnAgregarUsuario.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                lblUsuario.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                txtUsuarioRed.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                lblVisualizar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                chkVisualizar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                lblExportar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                chkExportar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                lblImportar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                chkImportar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                lblEliminar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                chkEliminar.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                lblElegirVersion.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                chkElegirVersion.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
             }
            else
            {
                cmbVersionExportar.Visible = false;
                btnExcel.Visible = false;
                btnAbrirExcel.Visible = false;
                //rdbOperativo.Visible = false;
                //rdbPrueba.Visible = false;
                ficheroSeleccionado.Visible = false;
                btnImportarExcel.Visible = false;
                cmbVersionEliminar.Visible = false;
                btnExcelEliminar.Visible = false;
                //cmbVersionPruebas.Visible = false;
                //btnVersionReal.Visible = false;

                btnLimpiarUsuario.Visible = false;
                btnAgregarUsuario.Visible = false;
                lblUsuario.Visible = false;
                txtUsuarioRed.Visible = false;
                lblVisualizar.Visible = false;
                chkVisualizar.Visible = false;
                lblExportar.Visible = false;
                chkExportar.Visible = false;
                lblImportar.Visible = false;
                chkImportar.Visible = false;
                lblEliminar.Visible = false;
                chkEliminar.Visible = false;
                lblElegirVersion.Visible = false;
                chkElegirVersion.Visible = false;
            }
            conexiones.conexion.Close();
        }

        #region ConfiguracionGeneral
        protected void CambioFiltroVersionGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersionGeneral"] = cmbFiltroVersion.SelectedValue;
            this.rellenarGridGeneral();
        }

        private void rellenarFiltroVersionGeneral(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralVersionConsulta";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CFG_Version";
            cmbFiltroVersion.DataValueField = "CFG_Version";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroVersionGeneral"].ToString()).Selected = true;
        }

        protected void CambioFiltroConceptoGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroConcepto = (DropDownList)sender;
            ViewState["FiltroConceptoGeneral"] = cmbFiltroConcepto.SelectedValue;
            this.rellenarGridGeneral();
        }

        private void rellenarFiltroConceptoGeneral(DropDownList cmbFiltroConcepto)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralConceptosConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroConceptoGeneral"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@concepto", ViewState["FiltroConceptoGeneral"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroConcepto.DataSource = dt;
            cmbFiltroConcepto.DataTextField = "Concepto";
            cmbFiltroConcepto.DataValueField = "Concepto";
            cmbFiltroConcepto.DataBind();
            conexiones.conexion.Close();
            cmbFiltroConcepto.Items.FindByValue(ViewState["FiltroConceptoGeneral"].ToString()).Selected = true;
        }

        protected void CambioFiltroEmpresaGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroEmpresa = (DropDownList)sender;
            ViewState["FiltroEmpresaGeneral"] = cmbFiltroEmpresa.SelectedValue;
            this.rellenarGridGeneral();
        }

        private void rellenarFiltroEmpresaGeneral(DropDownList cmbFiltroEmpresa)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralEmpresaConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroEmpresaGeneral"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@empresa", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@empresa", ViewState["FiltroEmpresaGeneral"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroEmpresa.DataSource = dt;
            cmbFiltroEmpresa.DataTextField = "Empresa";
            cmbFiltroEmpresa.DataValueField = "Empresa";
            cmbFiltroEmpresa.DataBind();
            conexiones.conexion.Close();
            cmbFiltroEmpresa.Items.FindByValue(ViewState["FiltroEmpresaGeneral"].ToString()).Selected = true;
        }

        private void rellenarFiltroDelegacionGeneral(DropDownList cmbFiltroDelegacion)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralDelegacionConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroDelegacionGeneral"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@delegacion", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@delegacion", ViewState["FiltroDelegacionGeneral"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroDelegacion.DataSource = dt;
            cmbFiltroDelegacion.DataTextField = "Delegacion";
            cmbFiltroDelegacion.DataValueField = "Delegacion";
            cmbFiltroDelegacion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroDelegacion.Items.FindByValue(ViewState["FiltroDelegacionGeneral"].ToString()).Selected = true;
        }

        protected void CambioFiltroBUGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroBU = (DropDownList)sender;
            ViewState["FiltroBUGeneral"] = cmbFiltroBU.SelectedValue;
            this.rellenarGridGeneral();
        }
        protected void CambioFiltroDelegacionGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroDelegacion = (DropDownList)sender;
            ViewState["FiltroDelegacionGeneral"] = cmbFiltroDelegacion.SelectedValue;
            this.rellenarGridGeneral();
        }

        private void rellenarFiltroBUGeneral(DropDownList cmbFiltroEmpresa)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralBUConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroBUGeneral"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@BU", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@BU", ViewState["FiltroBUGeneral"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroEmpresa.DataSource = dt;
            cmbFiltroEmpresa.DataTextField = "BU";
            cmbFiltroEmpresa.DataValueField = "BU";
            cmbFiltroEmpresa.DataBind();
            conexiones.conexion.Close();
            cmbFiltroEmpresa.Items.FindByValue(ViewState["FiltroBUGeneral"].ToString()).Selected = true;
        }

        private void rellenarGridGeneral()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersionGeneral"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersionGeneral"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);

            SqlParameter parametroConcepto = new SqlParameter("@concepto", SqlDbType.NVarChar, 100);
            if (ViewState["FiltroConceptoGeneral"].ToString() == "")
                parametroConcepto.Value = null;
            else
                parametroConcepto.Value = ViewState["FiltroConceptoGeneral"].ToString();
            conexiones.comando.Parameters.Add(parametroConcepto);

            SqlParameter parametroBU = new SqlParameter("@BU", SqlDbType.NVarChar, 10);
            if (ViewState["FiltroBUGeneral"].ToString() == "")
                parametroBU.Value = null;
            else
                parametroBU.Value = ViewState["FiltroBUGeneral"].ToString();
            conexiones.comando.Parameters.Add(parametroBU);

            SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 5);
            if (ViewState["FiltroEmpresaGeneral"].ToString() == "")
                parametroEmpresa.Value = null;
            else
                parametroEmpresa.Value = ViewState["FiltroEmpresaGeneral"].ToString();
            conexiones.comando.Parameters.Add(parametroEmpresa);

            SqlParameter parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            if (ViewState["FiltroDelegacionGeneral"].ToString() == "")
                parametroDelegacion.Value = null;
            else
                parametroDelegacion.Value = ViewState["FiltroDelegacionGeneral"].ToString();
            conexiones.comando.Parameters.Add(parametroDelegacion);

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvDatosGenerales.DataSource = dr;
            grvDatosGenerales.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvDatosGenerales.HeaderRow.FindControl("FiltroVersionGeneral");
            this.rellenarFiltroVersionGeneral(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatosGenerales.HeaderRow.FindControl("FiltroConceptoGeneral");
            this.rellenarFiltroConceptoGeneral(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatosGenerales.HeaderRow.FindControl("FiltroEmpresaGeneral");
            this.rellenarFiltroEmpresaGeneral(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatosGenerales.HeaderRow.FindControl("FiltroDelegacionGeneral");
            this.rellenarFiltroDelegacionGeneral(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatosGenerales.HeaderRow.FindControl("FiltroBUGeneral");
            this.rellenarFiltroBUGeneral(cmbFiltro);
        }

        private void validarExcel(string fichero)
        {
            SLDocument sl;
            string version;
            DateTime fechaDesdeUltimaVersion = DateTime.Now;
            DateTime fechaDesde;
            string alcance;
            string observaciones;
            string dondeVa = "";

            try
            {
                ficheroExcel = fichero;
                dondeVa = "Error procesando pestaña VERSIÓN";
                sl = new SLDocument(fichero, "VERSIÓN");

                if (sl.GetCellValueAsString(1, 1).ToString().ToUpper().IndexOf("VERSIÓN") < 0)
                {
                    lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Configuración parámetros";
                    lblMensajeError.Text = "El fichero no parece tener el formato correcto." + "<br /> &nbsp;" + "Por favor, verifique el fichero e intente procesarlo de nuevo.";
                    mpeError.Show();
                    return;
                }

                // Validar valores cabecera 
                if ((sl.GetCellValueAsString(1, 2).Length == 0) || (sl.GetCellValueAsString(3, 2).Length == 0) || (sl.GetCellValueAsString(4, 2).Length == 0) || (sl.GetCellValueAsString(5, 2).Length == 0))
                {
                    lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Configuración parámetros";
                    lblMensajeError.Text = "Debe indicar la versión, la fecha desde, el alcance del cambio en la versión y un texto que indique los cambios realizados." + "<br /> &nbsp;" + "Por favor, verifique el fichero e intente procesarlo de nuevo.";
                    mpeError.Show();
                    return;
                }

                version = sl.GetCellValueAsString(1, 2);
                if (sl.GetCellValueAsString(2, 2) != "")
                    fechaDesdeUltimaVersion = Convert.ToDateTime(sl.GetCellValueAsString(2, 2));
                fechaDesde = sl.GetCellValueAsDateTime(3, 2);
                alcance = sl.GetCellValueAsString(4, 2);
                observaciones = sl.GetCellValueAsString(5, 2);

                dondeVa = "Validar versión";
                DateTime fechaVerificacion = Convert.ToDateTime("01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());
                if (sl.GetCellValueAsString(2, 2) != "")
                {
                    if (fechaDesde < fechaDesdeUltimaVersion || fechaDesde < fechaVerificacion)
                    {
                        lblTituloDecidir.Text = "I N F O R M A C I Ó N"; //"Versión";
                        lblObservaciones.Text = "La fecha desde es anterior a la fecha de última versión o anterior a la última fecha de procesamiento de márgenes.<br /><br />¿Desea continuar?";
                        mpeDecidir.Show();
                    }
                }
                else
                {
                    if (fechaDesde < fechaVerificacion)
                    {
                        lblTituloDecidir.Text = "I N F O R M A C I Ó N"; //"Versión";
                        lblObservaciones.Text = "La fecha desde es anterior a la fecha de última versión o anterior a la última fecha de procesamiento de márgenes.<br /><br />¿Desea continuar?";
                        mpeDecidir.Show();
                    }
                    else
                        procesarExcel(fichero);
                }
                    
            }
            catch (Exception ex)
            {
                lblTituloInformacion.Text = "I N F O R M A C I Ó N"; //"Fichero procesar";
                lblMensajeInformacion.Text = "Error al validar el fichero: " + fichero + "\r\n" + dondeVa + " \r\n " + ex.Message;
                mpeInformacion.Show();
            }
        }

        protected void btnOkDecidir_Click(object sender, EventArgs e)
        {
            mpeDecidir.Hide();
            procesarExcel(ficheroExcel);
        }

        private void procesarExcel(string fichero)
        {
            string nuevaVersion = "";
            SLDocument sl;
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataAdapter adaptador;
            System.Data.DataTable dtTablaErrores = new System.Data.DataTable();
            string strConexion;
            DataTable dtTopesConfig;
            DataTable dtTablaNivel1;
            DataTable dtTablaTaximetro;
            DataTable dtTablaNivel2;
            DataTable dtTablaNivel3;
            DataTable dtTablaNivel4;
            DataTable dtTablaServicios;
            DataTable dtTablaTransporte;
            DataTable dtTablaPaneles;
            DataTable dtAbonoTaxDuracion;
            DataTable dtAjusteGFDuracion;
            DataTable dtAjusteTaxOcupacion;
            DataTable dtAjusteClienteVolumen;
            DataTable dtAjusteCumplimientoPresupuesto;
            DataTable dtAjusteTopes;
            DataTable dtFallidos;
            DataTable dtBUs;
            int row;
            string version;
            DateTime fechaDesdeUltimaVersion;
            DateTime fechaDesde;
            string alcance;
            string observaciones;
            int meses;
            decimal porcentajeDesvio;
            decimal coeficienteVuelta;
            int kilometrosMinimos;
            string dondeVa = "";

            try
            {
                dondeVa = "Error procesando pestaña VERSIÓN";
                sl = new SLDocument(fichero, "VERSIÓN");

                version = sl.GetCellValueAsString(1, 2);
                if (sl.GetCellValueAsString(2, 2) != "")
                    fechaDesdeUltimaVersion = Convert.ToDateTime(sl.GetCellValueAsString(2, 2));
                fechaDesde = sl.GetCellValueAsDateTime(3, 2);
                alcance = sl.GetCellValueAsString(4, 2);
                observaciones = sl.GetCellValueAsString(5, 2);

                dondeVa = "Error procesando pestaña TRANSPORTE GENERAL";
                sl.SelectWorksheet("TRANSPORTE GENERAL");
                meses = sl.GetCellValueAsInt32(1, 2);
                porcentajeDesvio = sl.GetCellValueAsDecimal(2, 2);
                coeficienteVuelta = sl.GetCellValueAsDecimal(3, 2);
                kilometrosMinimos = sl.GetCellValueAsInt32(5, 2);

                sl.CloseWithoutSaving();

                // TOPES_CONFIG
                dondeVa = "Error procesando pestaña TOPES_CONFIG";
                dtTopesConfig = new DataTable();
                dtTopesConfig.Columns.Add(new DataColumn("Pestaña", typeof(string)));
                dtTopesConfig.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtTopesConfig.Columns.Add(new DataColumn("Columna", typeof(string)));
                dtTopesConfig.Columns.Add(new DataColumn("Minimo", typeof(int)));
                dtTopesConfig.Columns.Add(new DataColumn("Maximo", typeof(int)));
                dtTopesConfig.Columns.Add(new DataColumn("Moneda", typeof(string)));

                using (sl = new SLDocument(fichero, "TOPES_CONFIG"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTopesConfig.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 5)
                         );
                    }
                }

                // NIVEL 1
                dondeVa = "Error procesando pestaña NIVEL 1";
                dtTablaNivel1 = new DataTable();
                dtTablaNivel1.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("BU", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("Familia", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("Subfamilia", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("Articulo", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("Valorar", typeof(string)));
                dtTablaNivel1.Columns.Add(new DataColumn("Desde", typeof(int)));
                dtTablaNivel1.Columns.Add(new DataColumn("Hasta", typeof(int)));
                dtTablaNivel1.Columns.Add(new DataColumn("Valor", typeof(decimal)));
                dtTablaNivel1.Columns.Add(new DataColumn("Moenda", typeof(string)));

                using (sl = new SLDocument(fichero, "NIVEL 1"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaNivel1.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 5),
                            null,
                            //sl.GetCellValueAsString(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 9)
                         );
                    }
                }

                // TAXIMETROS
                dondeVa = "Error procesando pestaña TAXIMETROS";
                dtTablaTaximetro = new DataTable();
                dtTablaTaximetro.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("BU", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Familia", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Subfamilia", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Articulo", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Valorar", typeof(string)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Desde", typeof(int)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Hasta", typeof(int)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Valor", typeof(decimal)));
                dtTablaTaximetro.Columns.Add(new DataColumn("Moenda", typeof(string)));

                using (sl = new SLDocument(fichero, "TAXIMETROS"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaTaximetro.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 9),
                            //sl.GetCellValueAsString(row, iStartColumnIndex + 10)
                            null
                         );
                    }
                }

                // NIVEL 2
                dondeVa = "Error procesando pestaña NIVEL 2";
                dtTablaNivel2 = new DataTable();
                dtTablaNivel2.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtTablaNivel2.Columns.Add(new DataColumn("BU", typeof(string)));
                dtTablaNivel2.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtTablaNivel2.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtTablaNivel2.Columns.Add(new DataColumn("Valor", typeof(decimal)));

                using (sl = new SLDocument(fichero, "NIVEL 2"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaNivel2.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 4)
                         );
                    }
                }

                // NIVEL 3
                dondeVa = "Error procesando pestaña NIVEL 3";
                dtTablaNivel3 = new DataTable();
                dtTablaNivel3.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtTablaNivel3.Columns.Add(new DataColumn("Signo", typeof(string)));
                dtTablaNivel3.Columns.Add(new DataColumn("Dias", typeof(int)));

                using (sl = new SLDocument(fichero, "NIVEL 3"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaNivel3.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 2)
                         );
                    }
                }

                // NIVEL 4
                dondeVa = "Error procesando pestaña NIVEL 4";
                dtTablaNivel4 = new DataTable();
                dtTablaNivel4.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtTablaNivel4.Columns.Add(new DataColumn("Valor", typeof(string)));

                using (sl = new SLDocument(fichero, "NIVEL 4"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaNivel4.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1)
                         );
                    }
                }

                //dataDatos.Visible = true;
                //dataDatos.Columns.Clear();
                //dataDatos.DataSource = dtTablaNivel4;
                //dataDatos.DataBind();

                // SERVICIOS
                dondeVa = "Error procesando pestaña SERVICIOS";
                dtTablaServicios = new DataTable();
                dtTablaServicios.Columns.Add(new DataColumn("Familia", typeof(string)));
                dtTablaServicios.Columns.Add(new DataColumn("Subfamilia", typeof(string)));
                dtTablaServicios.Columns.Add(new DataColumn("Articulo", typeof(string)));
                dtTablaServicios.Columns.Add(new DataColumn("Tipo servicio", typeof(string)));

                using (sl = new SLDocument(fichero, "SERVICIOS"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaServicios.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3)
                         );
                    }
                }

                // TRANSPORTE
                dondeVa = "Error procesando pestaña TRANSPORTE";
                dtTablaTransporte = new DataTable();
                dtTablaTransporte.Columns.Add(new DataColumn("BU", typeof(string)));
                dtTablaTransporte.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtTablaTransporte.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtTablaTransporte.Columns.Add(new DataColumn("Margen", typeof(decimal)));
                dtTablaTransporte.Columns.Add(new DataColumn("Desde", typeof(DateTime)));
                dtTablaTransporte.Columns.Add(new DataColumn("Hasta", typeof(DateTime)));
                dtTablaTransporte.Columns.Add(new DataColumn("Distancia", typeof(string)));
                dtTablaTransporte.Columns.Add(new DataColumn("Propuesto", typeof(decimal)));
                dtTablaTransporte.Columns.Add(new DataColumn("Valor", typeof(decimal)));
                dtTablaTransporte.Columns.Add(new DataColumn("Desvio", typeof(decimal)));
                dtTablaTransporte.Columns.Add(new DataColumn("Moneda", typeof(string)));

                using (sl = new SLDocument(fichero, "TRANSPORTE"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtTablaTransporte.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsDateTime(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsDateTime(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 9),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 10)
                         );
                    }
                }

                // PANELES 
                dondeVa = "Error procesando pestaña PANELES";
                dtTablaPaneles = new DataTable();
                dtTablaPaneles.Columns.Add(new DataColumn("AAF", typeof(string)));
                dtTablaPaneles.Columns.Add(new DataColumn("Estandar", typeof(bool)));

                using (sl = new SLDocument(fichero, "PANELES"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        if (sl.GetCellValueAsString(row, iStartColumnIndex + 1).ToUpper() == "SI")
                            dtTablaPaneles.Rows.Add(
                                sl.GetCellValueAsString(row, iStartColumnIndex),
                                true
                             );
                        else
                            dtTablaPaneles.Rows.Add(
                           sl.GetCellValueAsString(row, iStartColumnIndex),
                           false
                        );
                    }
                }

                // ABONO TAX DURACION
                dondeVa = "Error procesando pestaña ABONO TAX DURACION";
                dtAbonoTaxDuracion = new DataTable();
                dtAbonoTaxDuracion.Columns.Add(new DataColumn("BU", typeof(string)));
                dtAbonoTaxDuracion.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtAbonoTaxDuracion.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtAbonoTaxDuracion.Columns.Add(new DataColumn("Duracion minima", typeof(int)));
                dtAbonoTaxDuracion.Columns.Add(new DataColumn("Duracion maxima", typeof(int)));
                dtAbonoTaxDuracion.Columns.Add(new DataColumn("Abono mensual", typeof(decimal)));

                using (sl = new SLDocument(fichero, "ABONO TAX DURACION"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtAbonoTaxDuracion.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 5)
                        );
                    }
                }

                // AJUSTE GF DURACION
                dondeVa = "Error procesando pestaña AJUSTE GF DURACION";
                dtAjusteGFDuracion = new DataTable();
                dtAjusteGFDuracion.Columns.Add(new DataColumn("BU", typeof(string)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Alcance", typeof(string)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Ajuste obra minima", typeof(decimal)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Duracion obra corta", typeof(int)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Ajuste obra corta", typeof(decimal)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Duracion obra larga", typeof(int)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Ajuste obra larga", typeof(decimal)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Duracion maxima", typeof(int)));
                dtAjusteGFDuracion.Columns.Add(new DataColumn("Ajuste obra muy larga", typeof(decimal)));

                using (sl = new SLDocument(fichero, "AJUSTE GF DURACION"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtAjusteGFDuracion.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 9),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 10)
                        );
                    }
                }

                // AJUSTE TAX OCUPACION
                dondeVa = "Error procesando pestaña AJUSTE TAX OCUPACION";
                dtAjusteTaxOcupacion = new DataTable();
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("BU", typeof(string)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Alcance", typeof(string)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Periodo ocupacion", typeof(int)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Ocupacion muy baja", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Abono maximo", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Ocupacion baja", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Abono ocupacion bajaa", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Ocupacion media", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Abono ocupacion media", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Ocupacion alta", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Abono ocupacion alta", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Abono ocupacion muy alta", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Porcentaje pais", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Porcentaje empresa", typeof(decimal)));
                dtAjusteTaxOcupacion.Columns.Add(new DataColumn("Porcentaje delegacion", typeof(decimal)));

                using (sl = new SLDocument(fichero, "AJUSTE TAX OCUPACION"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtAjusteTaxOcupacion.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 9),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 10),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 11),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 12),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 13),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 14),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 15),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 16)
                        );
                    }
                }

                // AJUSTE CLIENTE VOLUMEN
                dondeVa = "Error procesando pestaña AJUSTE CLIENTE VOLUMEN";
                dtAjusteClienteVolumen = new DataTable();
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("BU", typeof(string)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Alcance", typeof(string)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Valor cliente mediano", typeof(int)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Correccion cliente mediano", typeof(decimal)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Valor cliente grande", typeof(int)));
                dtAjusteClienteVolumen.Columns.Add(new DataColumn("Correccion cliente grande", typeof(decimal)));

                using (sl = new SLDocument(fichero, "AJUSTES POR CLIENTE VOLUMEN"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtAjusteClienteVolumen.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 7)
                        );
                    }
                }

                // AJUSTE CUMPLIMIENTO PRESUPUESTO
                dondeVa = "Error procesando pestaña AJUSTE CUMPLIMIENTO PRESUPUESTO";
                dtAjusteCumplimientoPresupuesto = new DataTable();
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("BU", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Meses", typeof(int)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Cumplimiento bajo GFC", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Cumplimiento bajo GFBU", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Cumplimiento alto GFC", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Cumplimiento alto GFBU", typeof(string)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Cumplimiento bajo", typeof(decimal)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Bonificacion bajo", typeof(decimal)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Cumplimiento alto", typeof(decimal)));
                dtAjusteCumplimientoPresupuesto.Columns.Add(new DataColumn("Bonificacion alto", typeof(decimal)));

                using (sl = new SLDocument(fichero, "AJUSTE CUMP PRESUP"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtAjusteCumplimientoPresupuesto.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 9),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 10),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 11)
                        );
                    }
                }

                // AJUSTES TOPES
                dondeVa = "Error procesando pestaña AJUSTE TOPES";
                dtAjusteTopes = new DataTable();
                dtAjusteTopes.Columns.Add(new DataColumn("BU", typeof(string)));
                dtAjusteTopes.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtAjusteTopes.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtAjusteTopes.Columns.Add(new DataColumn("Concepto", typeof(string)));
                dtAjusteTopes.Columns.Add(new DataColumn("Valor maximo", typeof(decimal)));


                using (sl = new SLDocument(fichero, "TOPES AJUSTES"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtAjusteTopes.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 4)
                        );
                    }
                }

                // FALLIDOS
                dondeVa = "Error procesando pestaña FALLIDOS";
                dtFallidos = new DataTable();
                dtFallidos.Columns.Add(new DataColumn("BU", typeof(string)));
                dtFallidos.Columns.Add(new DataColumn("Empresa", typeof(string)));
                dtFallidos.Columns.Add(new DataColumn("Delegacion", typeof(string)));
                dtFallidos.Columns.Add(new DataColumn("IM", typeof(int)));
                dtFallidos.Columns.Add(new DataColumn("Porcentaje cobrados", typeof(decimal)));
                dtFallidos.Columns.Add(new DataColumn("Porcentaje fallidos", typeof(decimal)));

                using (sl = new SLDocument(fichero, "FALLIDOS"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtFallidos.Rows.Add(
                            sl.GetCellValueAsString(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            Convert.ToInt32(sl.GetCellValueAsString(row, iStartColumnIndex + 3)),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsDecimal(row, iStartColumnIndex + 5)
                        );
                    }
                }

                // BUs
                dondeVa = "Error procesando pestaña BUs";
                dtBUs = new DataTable();
                dtBUs.Columns.Add(new DataColumn("Desde", typeof(DateTime)));
                dtBUs.Columns.Add(new DataColumn("Hasta", typeof(DateTime)));
                dtBUs.Columns.Add(new DataColumn("BU", typeof(string)));
                dtBUs.Columns.Add(new DataColumn("Empresa", typeof(string)));

                using (sl = new SLDocument(fichero, "BU-EMPRESAS"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtBUs.Rows.Add(
                            sl.GetCellValueAsDateTime(row, iStartColumnIndex),
                            sl.GetCellValueAsDateTime(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3)
                        );
                    }
                }

                // PROCESAR DATOS EN EL SQL
                dondeVa = "Error procesando";
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionProcesar";
                //comando.CommandText = "sp_ROP_ConfiguracionProcesar";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@version", version);
                comando.Parameters.AddWithValue("@fechaDesde", fechaDesde);
                comando.Parameters.AddWithValue("@alcance", alcance);
                comando.Parameters.AddWithValue("@observaciones", observaciones);
                comando.Parameters.AddWithValue("@meses", meses);
                comando.Parameters.AddWithValue("@porcentajeDesvio", porcentajeDesvio);
                comando.Parameters.AddWithValue("@coeficienteVuelta", coeficienteVuelta);
                comando.Parameters.AddWithValue("@kilometrosMinimos", kilometrosMinimos);
                var parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "topesConfig", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTopesConfig
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "nivel1", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaNivel1
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "taximetro", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaTaximetro
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "nivel2", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaNivel2
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "nivel3", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaNivel3
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "nivel4", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaNivel4
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "servicios", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaServicios
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "transporte", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaTransporte
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "paneles", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtTablaPaneles
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "abonoTaxDuracion", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtAbonoTaxDuracion
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "ajusteGFDuracion", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtAjusteGFDuracion
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "ajusteTaxOcupacion", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtAjusteTaxOcupacion
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "ajusteVolumenCliente", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtAjusteClienteVolumen
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "ajusteCumplimientoPresupuesto", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtAjusteCumplimientoPresupuesto
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "ajusteTopes", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtAjusteTopes
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "fallidos", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtFallidos
                };
                comando.Parameters.Add(parameter);
                parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "bus", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtBUs
                };
                comando.Parameters.Add(parameter);
                comando.Parameters.AddWithValue("@usuario", User.Identity.Name.ToString());
                
                dtTablaErrores = new System.Data.DataTable();
                adaptador = new SqlDataAdapter(comando);
                adaptador.Fill(dtTablaErrores);
                adaptador.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();

                if (dtTablaErrores.Rows.Count == 1)
                {
                    DataRow[] filaEncontrada = dtTablaErrores.Select("Pestaña = 'NUEVA VERSION'");
                    if (filaEncontrada.Length > 0)
                    {
                        foreach (DataRow fila in filaEncontrada)
                            nuevaVersion = Convert.ToString(fila["Error"]);

                        errores.Visible = false;
                        grdErrores.Visible = false;
                        grdErrores.DataSource = null;
                        grdErrores.DataBind();
                        lblTituloInformacion.Text = "I N F O R M A C I Ó N"; //"Agregar versión";
                        lblMensajeInformacion.Text = "Cargada la nueva versión: " + nuevaVersion;
                        mpeInformacion.Show();
                    }
                    else
                    {
                        errores.Visible = true;
                        grdErrores.Visible = true;
                        grdErrores.DataSource = dtTablaErrores;
                        grdErrores.DataBind();
                    }
                }
                else if (dtTablaErrores.Rows.Count > 1)
                {
                    errores.Visible = true;
                    grdErrores.Visible = true;
                    grdErrores.DataSource = dtTablaErrores;
                    grdErrores.DataBind();
                }
                //else
                //{
                //    errores.Visible = false;
                //    grdErrores.Visible = false;
                //    grdErrores.DataSource = null;
                //    grdErrores.DataBind();
                //    lblTituloInformacion.Text = "I N F O R M A C I Ó N"; //"Agregar versión";
                //    lblMensajeInformacion.Text = "Cargada la nueva versión: " + nuevaVersion;
                //    mpeInformacion.Show();
                //}
                //}

                // Volver a rellenar los grids
                ViewState["FiltroVersion"] = "";
                ViewState["FiltroConceptoValor"] = "";
                ViewState["FiltroConceptoValorTaximetros"] = "";
                ViewState["FiltroVersionGeneral"] = "";
                ViewState["FiltroConceptoGeneral"] = "";
                ViewState["FiltroBUGeneral"] = "";
                ViewState["FiltroEmpresaGeneral"] = "";
                ViewState["FiltroDelegacionGeneral"] = "";
                ViewState["FiltroVersionMovimientosGeneral"] = "";
                ViewState["FiltroMovimiento"] = "";
                ViewState["FiltroEmpresaTransporte"] = "";
                ViewState["FiltroDelegacionTransporte"] = "";
                ViewState["FiltroDesdeTransporte"] = "";
                ViewState["FiltroDistanciaTransporte"] = "";
                ViewState["FiltroVersionBU"] = "";
                ViewState["FiltroVersionTaximetros"] = "";
                rellenarGridGeneral();
                rellenarGrid();
                rellenarCombosVersion();
                rellenarGridServicios();
                rellenarGridTaximetros();
                rellenarGridTransporte();
                rellenarGridAjustes();
                rellenarGridBU();
                rellenarTransporteGeneral();
                rellenarGridPaneles();
                rellenarGridUsuarios();
                validarAccionUsuario();
                rellenarAjustesFechaMovimientos();
                rellenarGridHistorico();
                btnAbrirExcel.Visible = false;
            }
            catch (Exception ex)
            {
                lblTituloInformacion.Text = "Fichero procesar";
                lblMensajeInformacion.Text = "Error al procesar el fichero: " + fichero + "\r\n" + dondeVa + " \r\n " + ex.Message;
                mpeInformacion.Show();
            }
        }

        //private void exportarExcelGeneral()
        //{
        //    SLStyle style;
        //    SLDataValidation dv;
        //    int fila = 3;
        //    int columna = 0;
        //    int cantidadEmpresas = 0;
        //    bool encabezado = false;

        //    btnAbrirExcelGeneral.Visible = false;
        //    // Consulta SQL
        //    conexiones.crearConexion();
        //    conexiones.comando = conexiones.conexion.CreateCommand();
        //    conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralConsulta";
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;
        //    conexiones.comando.Parameters.AddWithValue("@version", cmbVersionGeneralExportar.Text);
        //    conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
        //    SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
        //    DataSet ds = new DataSet();
        //    adaptador.Fill(ds);

        //    // Crear el excel
        //    SLDocument sl = new SLDocument();

        //    sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Configuracion general");

        //    // Hojas ocultas
        //    sl.AddWorksheet("Empresas");
        //    conexiones.consulta = "sp_ROP_EmpresasListado";
        //    SqlDataAdapter adaptadorEmpresa = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
        //    System.Data.DataTable dtEmpresas = new System.Data.DataTable();
        //    adaptadorEmpresa.Fill(dtEmpresas);
        //    sl.ImportDataTable(1, 1, dtEmpresas, false);
        //    sl.HideWorksheet("Empresas");
        //    cantidadEmpresas = dtEmpresas.Rows.Count;

        //    sl.SelectWorksheet("Configuracion general");
        //    sl.SetCellValue(1, 1, "CONFIGURACIÓN GENERAL");

        //    encabezado = false;
        //    foreach (DataRow Row in ds.Tables[0].Rows)
        //    {
        //        if (!encabezado)
        //        {
        //            // VERSIÓN
        //            sl.SetCellValue(FILA_GENERAL_IDVERSION, 1, "ID");
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 12);
        //            style.Font.Bold = true;
        //            sl.SetCellStyle(FILA_GENERAL_IDVERSION, 1, style);

        //            sl.SetCellValue(FILA_GENERAL_IDVERSION, 2, Row.ItemArray[COL_GeneralID].ToString());
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 11);
        //            sl.SetCellStyle(FILA_GENERAL_IDVERSION, 2, style);

        //            sl.SetCellValue(FILA_GENERAL_VERSION, 1, "Versión");
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 12);
        //            style.Font.Bold = true;
        //            sl.SetCellStyle(FILA_GENERAL_VERSION, 1, style);

        //            sl.SetCellValue(FILA_GENERAL_VERSION, 2, Row.ItemArray[COL_GeneralVersion].ToString());
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 11);
        //            sl.SetCellStyle(FILA_GENERAL_VERSION, 2, style);

        //            // FECHA DESDE 
        //            sl.SetCellValue(FILA_GENERAL_FECHA_DESDE, 1, "Desde");
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 12);
        //            style.Font.Bold = true;
        //            style.FormatCode = "dd-MM-yyyy";
        //            sl.SetCellStyle(FILA_GENERAL_FECHA_DESDE, 1, style);

        //            sl.SetCellValue(FILA_GENERAL_FECHA_DESDE, 2, Row.ItemArray[COL_GeneralDesde].ToString());
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 11);
        //            sl.SetCellStyle(FILA_GENERAL_FECHA_DESDE, 2, style);

        //            // FECHA HASTA
        //            sl.SetCellValue(FILA_GENERAL_FECHA_HASTA, 1, "Hasta");
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 12);
        //            style.Font.Bold = true;
        //            style.FormatCode = "dd-MM-yyyy";
        //            sl.SetCellStyle(FILA_GENERAL_FECHA_HASTA, 1, style);

        //            sl.SetCellValue(FILA_GENERAL_FECHA_HASTA, 2, Row.ItemArray[COL_GeneralHasta].ToString());
        //            style = sl.CreateStyle();
        //            style.SetFont("Verdana", 11);
        //            sl.SetCellStyle(FILA_GENERAL_FECHA_HASTA, 2, style);

        //            sl.AutoFitColumn(1, 2);

        //            sl.MergeWorksheetCells("A1", "C1");
        //            style = sl.CreateStyle();
        //            style.Font.Bold = true;
        //            style.Font.Italic = true;
        //            style.SetFont("Verdana", 12);
        //            sl.SetCellStyle(1, 1, style);

        //            // Crear el encabezado del informe
        //            sl.SetCellValue(FILA_GENERAL_DATOS - 1, 1, "Concepto");
        //            sl.SetCellValue(FILA_GENERAL_DATOS - 1, 2, "Empresa");
        //            sl.SetCellValue(FILA_GENERAL_DATOS - 1, 3, "Valor");

        //            style = sl.CreateStyle();
        //            sl.SetColumnWidth(1, 30);
        //            sl.SetColumnWidth(2, 20);
        //            sl.SetColumnWidth(3, 20);
        //            style.SetFont("Verdana", 10);
        //            style.Font.Bold = true;
        //            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
        //            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
        //            style.SetFontColor(System.Drawing.Color.White);
        //            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //            for (columna = 1; columna <= 3; columna++)
        //                sl.SetCellStyle(FILA_GENERAL_DATOS - 1, columna, style);

        //            sl.FreezePanes(FILA_GENERAL_DATOS - 1, 3);
        //            fila = FILA_GENERAL_DATOS;

        //            encabezado = true;
        //        }

        //        // Datos
        //        sl.SetCellValue(fila, 1, Row.ItemArray[COL_GeneralConcepto].ToString());
        //        sl.SetCellValue(fila, 2, Row.ItemArray[COL_GeneralEmpresa].ToString());
        //        sl.SetCellValue(fila, 3, Row.ItemArray[COL_GeneralValor].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
        //        style.FormatCode = "#.##0,00";
        //        sl.SetCellStyle(fila, 3, style);

        //        fila++;
        //    }

        //    if (fila >= FILA_GENERAL_DATOS)
        //    {
        //        sl.Filter("A" + (FILA_GENERAL_DATOS - 1).ToString(), "B" + (fila - 1).ToString());

        //        dv = sl.CreateDataValidation("B" + (FILA_GENERAL_DATOS).ToString(), "B1000");
        //        dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
        //        sl.AddDataValidation(dv);

        //        dv = sl.CreateDataValidation("C" + (FILA_GENERAL_DATOS).ToString(), "C1000");
        //        dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.0, false);
        //        sl.AddDataValidation(dv);
        //    }

        //    sl.SaveAs(nombreInforme);
        //    btnAbrirExcelGeneral.Visible = true;
        //}

        //private void exportarExcelGeneralTodo()
        //{
        //    SLStyle style;
        //    int fila;
        //    int columna = 0;

        //    btnAbrirExcelGeneral.Visible = false;
        //    // Consulta SQL
        //    conexiones.crearConexion();
        //    conexiones.comando = conexiones.conexion.CreateCommand();
        //    conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralConsulta";
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;
        //    conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
        //    conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
        //    conexiones.comando.Parameters.AddWithValue("@empresa", DBNull.Value);
        //    if (cmbVersionGeneralExportar.Text == "TODOS")
        //        conexiones.comando.Parameters.AddWithValue("@tipo", 0);
        //    else if (cmbVersionGeneralExportar.Text == "TODOS Reales")
        //        conexiones.comando.Parameters.AddWithValue("@tipo", 1);
        //    else
        //        conexiones.comando.Parameters.AddWithValue("@tipo", 2);
        //    SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
        //    DataSet ds = new DataSet();
        //    adaptador.Fill(ds);

        //    // Crear el excel
        //    SLDocument sl = new SLDocument();

        //    sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Configuracion general");

        //    // Crear el encabezado del informe
        //    sl.SetCellValue(1, 1, "ID");
        //    sl.SetCellValue(1, 2, "Versión");
        //    sl.SetCellValue(1, 3, "Desde");
        //    sl.SetCellValue(1, 4, "Hasta");
        //    sl.SetCellValue(1, 5, "Concepto");
        //    sl.SetCellValue(1, 6, "Empresa");
        //    sl.SetCellValue(1, 7, "Valor");

        //    style = sl.CreateStyle();
        //    sl.SetColumnWidth(1, 20);
        //    sl.SetColumnWidth(2, 30);
        //    sl.SetColumnWidth(3, 20);
        //    sl.SetColumnWidth(4, 20);
        //    sl.SetColumnWidth(5, 30);
        //    sl.SetColumnWidth(6, 20);
        //    sl.SetColumnWidth(7, 20);
        //    style.SetFont("Verdana", 10);
        //    style.Font.Bold = true;
        //    style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
        //    style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
        //    style.SetFontColor(System.Drawing.Color.White);
        //    style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //    for (columna = 1; columna <= 7; columna++)
        //        sl.SetCellStyle(1, columna, style);

        //    fila = 2;
        //    foreach (DataRow Row in ds.Tables[0].Rows)
        //    {
        //        // Datos
        //        sl.SetCellValue(fila, 1, Row.ItemArray[COL_GeneralID].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 1, style);

        //        sl.SetCellValue(fila, 2, Row.ItemArray[COL_GeneralVersion].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 2, style);

        //        sl.SetCellValue(fila, 3, Row.ItemArray[COL_GeneralDesde].ToString());
        //        sl.SetCellValue(fila, 4, Row.ItemArray[COL_GeneralHasta].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        style.FormatCode = "dd-MM-yyyy";
        //        sl.SetCellStyle(fila, 3, style);
        //        sl.SetCellStyle(fila, 4, style);

        //        sl.SetCellValue(fila, 5, Row.ItemArray[COL_GeneralConcepto].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 5, style);

        //        sl.SetCellValue(fila, 6, Row.ItemArray[COL_GeneralEmpresa].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 6, style);

        //        sl.SetCellValue(fila, 7, Row.ItemArray[COL_GeneralValor].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
        //        style.FormatCode = "#.##0,00";
        //        sl.SetCellStyle(fila, 7, style);
        //        fila++;
        //    }

        //    sl.SaveAs(nombreInforme);
        //    btnAbrirExcelGeneral.Visible = true;
        //}
        #endregion

        #region "ConfiguracionVersion"

        private void rellenarCombosVersion()
        {
            string version = "";

            // para importar: reales y pruebas
            cmbVersionExportar.Items.Clear();
            cmbVersionExportar.Items.Add("");

            cmbVersioAjustes.Items.Clear();
            cmbVersioAjustes.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionVersionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            conexiones.comando.Parameters.AddWithValue("@selector", 1);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cmbVersionExportar.Items.Add(dr.GetString(0));
                    cmbVersioAjustes.Items.Add(dr.GetString(0));

                    if (dr.GetInt32(2) != 0)
                        version = dr.GetString(0);
                }
            }
            conexiones.conexion.Close();
            //cmbVersionExportar.Text = "TODOS";
            //cmbVersionExportar.Text = "";
            cmbVersionExportar.Text = version;

            // para eliminar: a futuro y pruebas
            cmbVersionEliminar.Items.Clear();
            cmbVersionEliminar.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionVersionEliminarConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader drEliminar = conexiones.comando.ExecuteReader();
            if (drEliminar.HasRows)
            {
                while (drEliminar.Read())
                {
                    cmbVersionEliminar.Items.Add(drEliminar.GetString(0));
                }
            }
            conexiones.conexion.Close();
            cmbVersionEliminar.Text = "";

            // para pasar a real: solo pruebas
            //cmbVersionPruebas.Items.Clear();
            //cmbVersionPruebas.Items.Add("");

            //conexiones.crearConexion();
            //conexiones.consulta = "sp_ROP_ConfiguracionVersionPruebasConsulta";
            //conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            //conexiones.comando.CommandType = CommandType.StoredProcedure;
            //SqlDataReader drPruebas = conexiones.comando.ExecuteReader();
            //if (drPruebas.HasRows)
            //{
            //    while (drPruebas.Read())
            //    {
            //        cmbVersionPruebas.Items.Add(drPruebas.GetString(0));
            //    }
            //}
            //conexiones.conexion.Close();
            //cmbVersionPruebas.Text = "";
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (cmbVersionExportar.Text == "")
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Exportar excel GFV";
                lblMensajeError.Text = "Debe indicar el tipo de fichero a exportar.";
                mpeError.Show();
                return;
            }
            if (cmbVersionExportar.Text == "TODOS" || cmbVersionExportar.Text == "TODOS Reales" || cmbVersionExportar.Text == "TODOS Pruebas")
            {
                nombreInforme = Server.MapPath("~/Ficheros excel/Configuracion__TODO_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                //exportarExcelTodo();
            }
            else
            {
                nombreInforme = Server.MapPath("~/Ficheros excel/Configuracion_" + cmbVersionExportar.Text.Replace("/", "_") + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                exportarExcel();
            }
            lblTituloInformacion.Text = "I N F O R M A C I Ó N"; //"Exportar configuración";
            lblMensajeInformacion.Text = "Fichero excel creado.";
            mpeInformacion.Show();
            btnAbrirExcel.Visible = true;
        }

        protected void btnAbrirExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbVersionExportar.Text == "TODOS" || cmbVersionExportar.Text == "TODOS Reales" || cmbVersionExportar.Text == "TODOS Pruebas")
                {
                    Context.Response.Redirect("Ficheros excel/Configuracion__TODO_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                }
                else
                {
                    Context.Response.Redirect("Ficheros excel/Configuracion_" + cmbVersionExportar.Text.Replace("/", "_") + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                }
                btnAbrirExcel.Visible = false;
            }
            catch (Exception ex)
            {
                lblTituloInformacion.Text = "I N F O R M A C I Ó N"; // "Fichero configuración";
                lblMensajeInformacion.Text = "Error al descargar el fichero: Ficheros excel/Configuracion_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx" + " // " + ex.Message;
                mpeInformacion.Show();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbVersionEliminar.Text == "")
                {
                    lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Eliminar versión";
                    lblMensajeError.Text = "Debe indicar la versión a eliminar.";
                    mpeError.Show();
                }
                else
                {
                    lblTituloDecidirBorrar.Text = "I N F O R M A C I Ó N"; //"Versión";
                    lblDecidirBorrar.Text = "Se eliminará la versión: " + cmbVersionEliminar.Text + ".<br /><br />¿Desea continuar?";
                    mpeDecidirBorrar.Show();
                }
                //conexiones.crearConexion();
                //conexiones.comando = conexiones.conexion.CreateCommand();
                //conexiones.comando.CommandText = "sp_ROP_ConfiguracionEliminar";
                //conexiones.comando.CommandType = CommandType.StoredProcedure;
                //conexiones.comando.Parameters.AddWithValue("@CFG_Version", cmbVersionEliminar.Text);
                //conexiones.comando.ExecuteNonQuery();
                //conexiones.conexion.Close();

                //ViewState["FiltroVersion"] = "";
                //ViewState["FiltroConceptoValor"] = "";
                //ViewState["FiltroVersionGeneral"] = "";
                //ViewState["FiltroConceptoGeneral"] = "";
                //ViewState["FiltroBUGeneral"] = "";
                //ViewState["FiltroEmpresaGeneral"] = "";
                //ViewState["FiltroDelegacionGeneral"] = "";
                //ViewState["FiltroVersionMovimientosGeneral"] = "";
                //ViewState["FiltroMovimiento"] = "";
                //ViewState["FiltroEmpresaTransporte"] = "";
                //ViewState["FiltroDelegacionTransporte"] = "";
                //ViewState["FiltroDesdeTransporte"] = "";
                //ViewState["FiltroDistanciaTransporte"] = "";
                //ViewState["FiltroVersionBU"] = "";
                //rellenarGridGeneral();
                //rellenarGrid();
                //rellenarCombosVersion();
                //rellenarGridServicios();
                //rellenarGridTransporte();
                //rellenarGridAjustes();
                //rellenarGridBU();
                //rellenarTransporteGeneral();
                //rellenarGridPaneles();
                //rellenarGridUsuarios();
                //validarAccionUsuario();
                //rellenarAjustesFechaMovimientos();
                //rellenarGridHistorico();
                //btnAbrirExcel.Visible = false;

                //lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Eliminar GFV";
                //lblMensajeError.Text = "Versión eliminada.";
                //mpeError.Show();
            }
            catch (Exception ex)
            {
                string i = ex.Message;
            }
        }

        protected void btnOkDecidirBorrar_Click(object sender, EventArgs e)
        {
            //mpeDecidirBorrar.Hide();
            eliminarVersion();
        }

        protected void eliminarVersion()
        { 
            try
            {
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_ConfiguracionEliminar";
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@CFG_Version", cmbVersionEliminar.Text);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();

                ViewState["FiltroVersion"] = "";
                ViewState["FiltroConceptoValor"] = "";
                ViewState["FiltroConceptoValorTaximetros"] = "";
                ViewState["FiltroVersionGeneral"] = "";
                ViewState["FiltroConceptoGeneral"] = "";
                ViewState["FiltroBUGeneral"] = "";
                ViewState["FiltroEmpresaGeneral"] = "";
                ViewState["FiltroDelegacionGeneral"] = "";
                ViewState["FiltroVersionMovimientosGeneral"] = "";
                ViewState["FiltroMovimiento"] = "";
                ViewState["FiltroEmpresaTransporte"] = "";
                ViewState["FiltroDelegacionTransporte"] = "";
                ViewState["FiltroDesdeTransporte"] = "";
                ViewState["FiltroDistanciaTransporte"] = "";
                ViewState["FiltroVersionBU"] = "";
                ViewState["FiltroVersionTaximetros"] = "";
                rellenarGridGeneral();
                rellenarGrid();
                rellenarCombosVersion();
                rellenarGridServicios();
                rellenarGridTransporte();
                rellenarGridAjustes();
                rellenarGridBU();
                rellenarTransporteGeneral();
                rellenarGridPaneles();
                rellenarGridUsuarios();
                validarAccionUsuario();
                rellenarAjustesFechaMovimientos();
                rellenarGridHistorico();
                rellenarGridTaximetros();
                btnAbrirExcel.Visible = false;

                lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Eliminar GFV";
                lblMensajeError.Text = "Versión eliminada.";
                mpeError.Show();
            }
            catch (Exception ex)
            {
                string i = ex.Message;
            }
        }

//protected void btnReal_Click(object sender, EventArgs e)
//{
//    if (cmbVersionPruebas.Text == "")
//    {
//        //MessageBox.Show("Debe indicar la versión a pasar a real.", "Pasar GFV a real", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar la versión a pasar a real.');", true);
//        lblTituloError.Text = "Pasar a real";
//        lblMensajeError.Text = "Debe indicar la versión a pasar a real.";
//        mpeError.Show();
//        return;
//    }
//}

    protected void btnSubirExcel_Click(object sender, EventArgs e)
        {
            string directorio;
            String fichero;

            directorio = Server.MapPath("~/Ficheros excel/");

            if (ficheroSeleccionado.HasFile)
            {
                fichero = ficheroSeleccionado.FileName;
                directorio += fichero;
                ficheroSeleccionado.SaveAs(directorio);
                validarExcel(directorio);
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Importar fichero excel";
                lblMensajeError.Text = "Debe indicar el fichero a importar.";
                mpeError.Show();
            }
        }

        protected void CambioFiltroVersion(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersion"] = cmbFiltroVersion.SelectedValue;
            this.rellenarGrid();
        }


        private void rellenarFiltroVersion(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionVersionConsulta";
            conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            conexiones.comando.Parameters.AddWithValue("@selector", 0);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CFG_Version";
            cmbFiltroVersion.DataValueField = "CFG_Version";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroVersion"].ToString()).Selected = true;
        }

        protected void CambioFiltroConceptoValor(object sender, EventArgs e)
        {
            DropDownList cmbFiltroConceptoValor = (DropDownList)sender;
            ViewState["FiltroConceptoValor"] = cmbFiltroConceptoValor.SelectedValue;
            this.rellenarGrid();
        }

        protected void CambioFiltroConceptoValorTaximetros(object sender, EventArgs e)
        {
            DropDownList cmbFiltroConceptoValor = (DropDownList)sender;
            ViewState["FiltroConceptoValorTaximetros"] = cmbFiltroConceptoValor.SelectedValue;
            this.rellenarGridTaximetros();
        }

        private void rellenarFiltroConceptoValor(DropDownList cmbFiltroConceptoValor)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionConceptosValorConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroConceptoValor"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@concepto", ViewState["FiltroConceptoValor"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroConceptoValor.DataSource = dt;
            cmbFiltroConceptoValor.DataTextField = "CFGCONVAL_ConceptoValor";
            cmbFiltroConceptoValor.DataValueField = "CFGCONVAL_ConceptoValor";
            cmbFiltroConceptoValor.DataBind();
            conexiones.conexion.Close();
            cmbFiltroConceptoValor.Items.FindByValue(ViewState["FiltroConceptoValor"].ToString()).Selected = true;
        }

        private void rellenarFiltroConceptoValorTaximetros(DropDownList cmbFiltroConceptoValor)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTaximetrosConceptosValorConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroConceptoValorTaximetros"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@concepto", ViewState["FiltroConceptoValorTaximetros"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroConceptoValor.DataSource = dt;
            cmbFiltroConceptoValor.DataTextField = "CFGCONVAL_ConceptoValor";
            cmbFiltroConceptoValor.DataValueField = "CFGCONVAL_ConceptoValor";
            cmbFiltroConceptoValor.DataBind();
            conexiones.conexion.Close();
            cmbFiltroConceptoValor.Items.FindByValue(ViewState["FiltroConceptoValorTaximetros"].ToString()).Selected = true;
        }

        private void rellenarGrid()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersion"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersion"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);

            SqlParameter parametroConceptoValor = new SqlParameter("@conceptoValor", SqlDbType.NVarChar, 100);
            if (ViewState["FiltroConceptoValor"].ToString() == "")
                parametroConceptoValor.Value = null;
            else
                parametroConceptoValor.Value = ViewState["FiltroConceptoValor"].ToString();
            conexiones.comando.Parameters.Add(parametroConceptoValor);

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvDatos.DataSource = dr;
            grvDatos.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvDatos.HeaderRow.FindControl("FiltroVersion");
            this.rellenarFiltroVersion(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatos.HeaderRow.FindControl("FiltroConceptoValor");
            this.rellenarFiltroConceptoValor(cmbFiltro);
        }

        protected void CambioFiltroVersionTaximetros(object sender, EventArgs e)
        {
            DropDownList cmbFiltroTax= (DropDownList)sender;
            ViewState["FiltroVersionTaximetros"] = cmbFiltroTax.SelectedValue;
            this.rellenarGridTaximetros();
        }

        private void rellenarGridTaximetros()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTaximetrosConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersionTaximetros"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersionTaximetros"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            SqlParameter parametroConceptoValor = new SqlParameter("@conceptoValor", SqlDbType.NVarChar, 100);
            if (ViewState["FiltroConceptoValorTaximetros"].ToString() == "")
                parametroConceptoValor.Value = null;
            else
                parametroConceptoValor.Value = ViewState["FiltroConceptoValorTaximetros"].ToString();
            conexiones.comando.Parameters.Add(parametroConceptoValor);

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvTaximetros.DataSource = dr;
            grvTaximetros.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvTaximetros.HeaderRow.FindControl("FiltroVersionTaximetros");
            this.rellenarFiltroVersion(cmbFiltro);

            cmbFiltro = (DropDownList)grvTaximetros.HeaderRow.FindControl("FiltroConceptoValorTaximetros");
            this.rellenarFiltroConceptoValorTaximetros(cmbFiltro);
        }

        private void exportarExcel()
        {
            SLStyle style;
            SLStyle styleInt;
            SLStyle styleDec;
            SLStyle styleDate;
            SLDataValidation dv;
            SLPageSettings ps;
            int fila = 2;
            int columna = 0;
            decimal valor = 0;
            int valorEntero = 0;
            DateTime valorFecha = new DateTime();
            int cantidadConceptosNivel1 = 0;
            int cantidadValorarNivel1 = 0;
            int cantidadConceptosNivel2 = 0;
            int cantidadConceptosNivel3 = 0;
            int cantidadConceptosNivel4 = 0;
            int cantidadValoresNivel4 = 0;
            int cantidadBUs = 0;
            int cantidadEmpresas = 0;
            int cantidadDelegaciones = 0;
            int cantidadFamilias = 0;
            int cantidadSubfamilias = 0;
            int cantidadMonedas = 0;
            int cantidadTiposServicios = 0;
            int cantidadAlcanceAjustesGastosFijos = 0;
            int cantidadAlcancelAjustesOcupacion = 0;
            int cantidadAjustesTopes = 0;
            int cantidadAjustesTopesAplicarSobre = 0;
            int cantidadAjustesTopesAplicarA = 0;
            int cantidadAjustesAlcanceVolumneCliente = 0;
            int cantidadFallidosIM = 0;
            int cantidadDistancia = 0;
            int cantidadAlcanceConfiguracion = 0;
            int cantidadSignos = 2;
            int cantidadBooleano = 2;
            int cantidadConceptosTaximetros = 0;
            int cantidadValorarTaximetros = 0;

            int filaVa = 1;

            SqlDataAdapter adatadorConceptos;
            DataSet dsConceptos = new DataSet();
            System.Data.DataTable dtConceptos = new System.Data.DataTable();
            // Color general de las pestañas de las hojas ocultas
            ps = new SLPageSettings();
            ps.TabColor = System.Drawing.Color.Red;

            btnAbrirExcel.Visible = false;
            // Crear el excel
            SLDocument sl = new SLDocument();
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "VERSIÓN");

            // HOJAS OCULTAS
            conexiones.consulta = "sp_ROP_HojasOcultasConfiguracion";
            adatadorConceptos = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            adatadorConceptos.Fill(dsConceptos);
            adatadorConceptos.Dispose();

            // -- Conceptos Nivel 1
            sl.AddWorksheet("Conceptos Nivel 1");
            sl.SetPageSettings(ps, "Conceptos Nivel 1");
            dtConceptos = dsConceptos.Tables[0].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Conceptos Nivel 1");
            cantidadConceptosNivel1 = dtConceptos.Rows.Count;

            // -- Valorar Nivel 1
            sl.AddWorksheet("Valorar Nivel 1");
            sl.SetPageSettings(ps, "Valorar Nivel 1");
            dtConceptos = dsConceptos.Tables[1].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Valorar Nivel 1");
            cantidadValorarNivel1 = dtConceptos.Rows.Count;

            // -- Conceptos Nivel 2
            sl.AddWorksheet("Conceptos Nivel 2");
            sl.SetPageSettings(ps, "Conceptos Nivel 2");
            dtConceptos = dsConceptos.Tables[2].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Conceptos Nivel 2");
            cantidadConceptosNivel2 = dtConceptos.Rows.Count;

            // -- Conceptos Nivel 3
            sl.AddWorksheet("Conceptos Nivel 3");
            sl.SetPageSettings(ps, "Conceptos Nivel 3");
            dtConceptos = dsConceptos.Tables[3].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Conceptos Nivel 3");
            cantidadConceptosNivel3 = dtConceptos.Rows.Count;

            // -- Conceptos Nivel 4
            sl.AddWorksheet("Conceptos Nivel 4");
            sl.SetPageSettings(ps, "Conceptos Nivel 4");
            dtConceptos = dsConceptos.Tables[4].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Conceptos Nivel 4");
            cantidadConceptosNivel4 = dtConceptos.Rows.Count;

            // -- Valores Nivel 4
            sl.AddWorksheet("Valores Nivel 4");
            sl.SetPageSettings(ps, "Valores Nivel 4");
            dtConceptos = dsConceptos.Tables[5].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Valores Nivel 4");
            cantidadValoresNivel4 = dtConceptos.Rows.Count;

            // -- BUs
            sl.AddWorksheet("BUs");
            sl.SetPageSettings(ps, "BUs");
            dtConceptos = dsConceptos.Tables[6].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("BUs");
            cantidadBUs = dtConceptos.Rows.Count;

            // -- Empresas
            sl.AddWorksheet("Empresas");
            sl.SetPageSettings(ps, "Empresas");
            dtConceptos = dsConceptos.Tables[7].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Empresas");
            cantidadEmpresas = dtConceptos.Rows.Count;

            // -- Delegaciones
            sl.AddWorksheet("Delegaciones");
            sl.SetPageSettings(ps, "Delegaciones");
            dtConceptos = dsConceptos.Tables[8].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Delegaciones");
            cantidadDelegaciones = dtConceptos.Rows.Count;

            // -- Famillias
            sl.AddWorksheet("Familias");
            sl.SetPageSettings(ps, "Familias");
            dtConceptos = dsConceptos.Tables[9].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Familias");
            cantidadFamilias = dtConceptos.Rows.Count;

            // -- Subfamilias
            sl.AddWorksheet("Subfamilias");
            sl.SetPageSettings(ps, "Subfamilias");
            dtConceptos = dsConceptos.Tables[10].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Subfamilias");
            cantidadSubfamilias = dtConceptos.Rows.Count;

            // -- Monedas
            sl.AddWorksheet("Monedas");
            sl.SetPageSettings(ps, "Monedas");
            dtConceptos = dsConceptos.Tables[11].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Monedas");
            cantidadMonedas = dtConceptos.Rows.Count;

            // -- Tipos servicio
            sl.AddWorksheet("Tipos servicio");
            sl.SetPageSettings(ps, "Tipos servicio");
            dtConceptos = dsConceptos.Tables[12].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Tipos servicio");
            cantidadTiposServicios = dtConceptos.Rows.Count;

            // -- Alcance ajustes gastos fijos
            sl.AddWorksheet("Alcance ajustes gastos fijos");
            sl.SetPageSettings(ps, "Alcance ajustes gastos fijos");
            dtConceptos = dsConceptos.Tables[13].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Alcance ajustes gastos fijos");
            cantidadAlcanceAjustesGastosFijos = dtConceptos.Rows.Count;

            // -- Alcance ajustes ocupacion
            sl.AddWorksheet("Alcance ajustes ocupacion");
            sl.SetPageSettings(ps, "Alcance ajustes ocupacion");
            dtConceptos = dsConceptos.Tables[14].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Alcance ajustes ocupacion");
            cantidadAlcancelAjustesOcupacion = dtConceptos.Rows.Count;

            // -- Ajustes topes
            sl.AddWorksheet("Ajustes topes");
            sl.SetPageSettings(ps, "Ajustes topes");
            dtConceptos = dsConceptos.Tables[15].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Ajustes topes");
            cantidadAjustesTopes = dtConceptos.Rows.Count;

            // -- Ajustes topes aplicar sobre
            sl.AddWorksheet("Ajustes topes aplicar sobre");
            sl.SetPageSettings(ps, "A");
            dtConceptos = dsConceptos.Tables[16].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Ajustes topes aplicar sobre");
            cantidadAjustesTopesAplicarSobre = dtConceptos.Rows.Count;

            // -- Ajustes topes aplicar a
            sl.AddWorksheet("Ajustes topes aplicar a");
            sl.SetPageSettings(ps, "A");
            dtConceptos = dsConceptos.Tables[17].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Ajustes topes aplicar a");
            cantidadAjustesTopesAplicarA = dtConceptos.Rows.Count;

            // -- Ajustes alcance volumne cliente
            sl.AddWorksheet("Ajustes alcance volumne cliente");
            sl.SetPageSettings(ps, "Ajustes alcance volumne cliente");
            dtConceptos = dsConceptos.Tables[18].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Ajustes alcance volumne cliente");
            cantidadAjustesAlcanceVolumneCliente = dtConceptos.Rows.Count;

            // -- Fallidos IM
            sl.AddWorksheet("Fallidos IM");
            sl.SetPageSettings(ps, "Fallidos IM");
            dtConceptos = dsConceptos.Tables[19].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Fallidos IM");
            cantidadFallidosIM = dtConceptos.Rows.Count;

            // -- Distancia
            sl.AddWorksheet("Distancia");
            sl.SetPageSettings(ps, "Distancia");
            dtConceptos = dsConceptos.Tables[20].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Distancia");
            cantidadDistancia = dtConceptos.Rows.Count;

            // -- Signos
            sl.AddWorksheet("Signos");
            sl.SetPageSettings(ps, "Signos");
            sl.SetCellValue(1, 1, "+");
            sl.SetCellValue(2, 1, "-");
            sl.HideWorksheet("Signos");

            // -- Booleano
            sl.AddWorksheet("Booleano");
            sl.SetPageSettings(ps, "Booleano");
            sl.SetCellValue(1, 1, "SI");
            sl.SetCellValue(2, 1, "NO");
            sl.HideWorksheet("Booleano");

            // -- Booleano
            sl.AddWorksheet("Grado de cambio configuración");
            sl.SetPageSettings(ps, "Grado de cambio configuración");
            dtConceptos = dsConceptos.Tables[21].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Grado de cambio configuración");
            cantidadAlcanceConfiguracion = dtConceptos.Rows.Count;

            // -- Valores taximetros
            sl.AddWorksheet("Conceptos taximetros");
            sl.SetPageSettings(ps, "Conceptos taximetros");
            dtConceptos = dsConceptos.Tables[22].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Conceptos taximetros");
            cantidadConceptosTaximetros = dtConceptos.Rows.Count;

            // -- Valorar taximetros
            sl.AddWorksheet("Valorar taximetros");
            sl.SetPageSettings(ps, "Valorar taximetros");
            dtConceptos = dsConceptos.Tables[23].Copy();
            sl.ImportDataTable(1, 1, dtConceptos, false);
            sl.HideWorksheet("Valorar taximetros");
            cantidadValorarTaximetros = dtConceptos.Rows.Count;


            // DATOS GENERALES DE LA VERSIÓN
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_DatosConfiguracionConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            sl.SelectWorksheet("VERSIÓN");
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                //sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "VERSIÓN");
                filaVa = 1;

                // NOMBRE VERSIÓN
                sl.SetCellValue(filaVa, 1, "Versión (base)");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                sl.SetCellStyle(filaVa, 1, style);

                sl.SetCellValue(filaVa, 2, Row.ItemArray[COL_NombreVersion].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                sl.SetCellStyle(filaVa, 2, style);

                filaVa = filaVa + 1;

                // FECHA DESDE 
                sl.SetCellValue(filaVa, 1, "Desde (última versión)");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                style.FormatCode = "dd/MM/yyyy";
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                sl.SetCellStyle(filaVa, 1, style);

                sl.SetCellValue(filaVa, 2, Row.ItemArray[COL_Desde].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                style.FormatCode = "dd/MM/yyyy";
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                sl.SetCellStyle(filaVa, 2, style);

                filaVa = filaVa + 1;

                // FECHA DESDE NUEVA VERSIÓN
                sl.SetCellValue(filaVa, 1, "Desde (nueva versión)");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                style.FormatCode = "dd/MM/yyyy";
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                style.FormatCode = "dd/MM/yyyy";
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                sl.SetCellStyle(filaVa, 2, style);
                sl.SetCellStyle(filaVa, 2, style);
                filaVa = filaVa + 1;

                // ALCANCE
                sl.SetCellValue(filaVa, 1, "Grado de cambio");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                sl.SetCellStyle(filaVa, 1, style);

                //sl.SetCellValue(filaVa, 2, Row.ItemArray[COL_Alcance].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                sl.SetCellStyle(filaVa, 2, style);
                dv = sl.CreateDataValidation("B" + filaVa.ToString(), "B" + filaVa.ToString());
                dv.AllowList("'Grado de cambio configuración'!$A$1:$A$" + cantidadAlcanceConfiguracion.ToString(), true, true);
                sl.AddDataValidation(dv);

                filaVa = filaVa + 1;

                // Observaciones
                sl.SetCellValue(filaVa, 1, "Observaciones");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                sl.SetCellStyle(filaVa, 1, style);

                sl.SetCellValue(filaVa, 2, "");
                style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                sl.SetCellStyle(filaVa, 2, style);

            }
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 100);
            // FIN DATOS GENERALES DE LA VERSIÓN

            // estilos
            styleInt = sl.CreateStyle();
            styleInt.SetFont("Verdana", 10);
            styleInt.Alignment.Horizontal = HorizontalAlignmentValues.Right;
            styleInt.FormatCode = "#,##0";

            styleDec = sl.CreateStyle();
            styleDec.SetFont("Verdana", 10);
            styleDec.Alignment.Horizontal = HorizontalAlignmentValues.Right;
            styleDec.FormatCode = "#,##0.00";

            styleDate = sl.CreateStyle();
            styleDate.SetFont("Verdana", 10);
            styleDate.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            styleDate.FormatCode = "dd/MM/yyyy";

            // Consulta SQL NIVEL 1
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionNivel1Consulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorNivel1 = new SqlDataAdapter(conexiones.comando);
            DataSet dsNivel1 = new DataSet();
            adaptadorNivel1.Fill(dsNivel1);

            sl.AddWorksheet("NIVEL 1");
            sl.Filter("A1", "K1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Nivel 1'!$A$1:$A$" + cantidadConceptosNivel1.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Familias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowList("'Subfamilias'!$A$1:$A$" + cantidadSubfamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("G2", "G1000");
            dv.AllowList("'Valorar Nivel 1'!$A$1:$A$" + cantidadValorarNivel1.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("H2", "I1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("I2", "I1000");
            dv.AllowList("'Monedas'!$A$1:$A$" + cantidadMonedas.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "BU");
            sl.SetCellValue(1, 3, "Empresa");
            sl.SetCellValue(1, 4, "Familia");
            sl.SetCellValue(1, 5, "Subfamilia");
            sl.SetCellValue(1, 6, "Artículo");
            //sl.SetCellValue(1, 7, "Aplicar a");
            sl.SetCellValue(1, 7, "Valorar");
            sl.SetCellValue(1, 8, "Desde");
            sl.SetCellValue(1, 9, "Hasta");
            sl.SetCellValue(1, 10, "Valor");
            sl.SetCellValue(1, 11, "Moneda");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 25);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 25);
            sl.SetColumnWidth(11, 25);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 11; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsNivel1.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Bu].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Empresa].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_Familia].ToString());
                sl.SetCellValue(fila, 5, Row.ItemArray[COL_Subfamilia].ToString());
                sl.SetCellValue(fila, 6, Row.ItemArray[COL_Articulo].ToString());
                sl.SetCellValue(fila, 7, Row.ItemArray[COL_Valorar].ToString());
                int.TryParse(Row.ItemArray[COL_ValorDesde].ToString(), out valorEntero);
                sl.SetCellValue(fila, 8, valorEntero);
                int.TryParse(Row.ItemArray[COL_ValorHasta].ToString(), out valorEntero);
                sl.SetCellValue(fila, 9, valorEntero);
                decimal.TryParse(Row.ItemArray[COL_Valor].ToString(), out valor);
                sl.SetCellValue(fila, 10, valor);
                sl.SetCellValue(fila, 11, Row.ItemArray[COL_Moneda].ToString());
                
                sl.SetCellStyle(fila, 8, styleInt);
                sl.SetCellStyle(fila, 9, styleInt);
                sl.SetCellStyle(fila, 10, styleDec);
                fila++;
            }
            sl.AutoFitColumn(1, 11);

            // Consulta SQL TAXIMETROS
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTaximetroConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorTaximetro = new SqlDataAdapter(conexiones.comando);
            DataSet dsTaximetro = new DataSet();
            adaptadorTaximetro.Fill(dsTaximetro);

            sl.AddWorksheet("TAXIMETROS");
            sl.Filter("A1", "K1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Taximetros'!$A$1:$A$" + cantidadConceptosTaximetros.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Familias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowList("'Subfamilias'!$A$1:$A$" + cantidadSubfamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("G2", "G1000");
            dv.AllowList("'Valorar Nivel 1'!$A$1:$A$" + cantidadValorarTaximetros.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("H2", "J1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            //dv = sl.CreateDataValidation("K2", "K1000");
            //dv.AllowList("'Monedas'!$A$1:$A$" + cantidadMonedas.ToString(), true, true);
            //sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "BU");
            sl.SetCellValue(1, 3, "Empresa");
            sl.SetCellValue(1, 4, "Familia");
            sl.SetCellValue(1, 5, "Subfamilia");
            sl.SetCellValue(1, 6, "AAF");
            sl.SetCellValue(1, 7, "Total/Superficie");
            sl.SetCellValue(1, 8, "Desde");
            sl.SetCellValue(1, 9, "Hasta");
            sl.SetCellValue(1, 10, "Valor");
            //sl.SetCellValue(1, 11, "Moneda");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 25);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 25);
            //sl.SetColumnWidth(11, 25);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 10; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsTaximetro.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Bu].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Empresa].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_Familia].ToString());
                sl.SetCellValue(fila, 5, Row.ItemArray[COL_Subfamilia].ToString());
                sl.SetCellValue(fila, 6, Row.ItemArray[COL_Articulo].ToString());
                sl.SetCellValue(fila, 7, Row.ItemArray[COL_Valorar].ToString());
                int.TryParse(Row.ItemArray[COL_ValorDesde].ToString(), out valorEntero);
                //sl.SetCellValue(fila, 8, valorEntero.ToString("#,##0"));
                sl.SetCellValue(fila, 8, valorEntero);
                int.TryParse(Row.ItemArray[COL_ValorHasta].ToString(), out valorEntero);
                //sl.SetCellValue(fila, 9, valorEntero.ToString("#,##0"));
                sl.SetCellValue(fila, 9, valorEntero);
                decimal.TryParse(Row.ItemArray[COL_Valor].ToString(), out valor);
                //sl.SetCellValue(fila, 10, valor.ToString("#,##0.00"));
                sl.SetCellValue(fila, 10, valor);
                //sl.SetCellValue(fila, 11, Row.ItemArray[COL_Moneda].ToString());

                sl.SetCellStyle(fila, 8, styleInt);
                sl.SetCellStyle(fila, 9, styleInt);
                sl.SetCellStyle(fila, 10, styleDec);
                fila++;
            }
            sl.AutoFitColumn(1, 10);

            // Consulta SQL NIVEL 2
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionNivel2Consulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorNivel2 = new SqlDataAdapter(conexiones.comando);
            DataSet dsNivel2 = new DataSet();
            adaptadorNivel2.Fill(dsNivel2);

            sl.AddWorksheet("NIVEL 2");
            sl.Filter("A1", "E1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Nivel 2'!$A$1:$A$" + cantidadConceptosNivel2.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "BU");
            sl.SetCellValue(1, 3, "Empresa");
            sl.SetCellValue(1, 4, "Delegacion");
            sl.SetCellValue(1, 5, "Valor");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 5; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsNivel2.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto_Nivel2].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_BU_Nivel2].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Empresa_Nivel2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_Delegacion_Nivel2].ToString());
                decimal.TryParse(Row.ItemArray[COL_Valor_Nivel2].ToString(), out valor);
                sl.SetCellValue(fila, 5, valor);
                //sl.SetCellValue(fila, 5, valor.ToString("#,##0.00"));

                //style = sl.CreateStyle();
                //style.SetFont("Verdana", 10);
                //style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //style.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 5, styleDec);
                fila++;
            }
            sl.AutoFitColumn(1, 5);

            // Consulta SQL NIVEL 3
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionNivel3Consulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorNivel3 = new SqlDataAdapter(conexiones.comando);
            DataSet dsNivel3 = new DataSet();
            adaptadorNivel3.Fill(dsNivel3);

            sl.AddWorksheet("NIVEL 3");
            sl.Filter("A1", "C1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Nivel 3'!$A$1:$A$" + cantidadConceptosNivel3.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Signos'!$A$1:$A$" + cantidadSignos.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "Signo");
            sl.SetCellValue(1, 3, "Días");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 3; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsNivel3.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto_Nivel3].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Signo_Nivel3].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Dias_Nivel3].ToString());

                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 2, style);

                sl.SetCellStyle(fila, 3, styleInt);
                fila++;
            }
            sl.AutoFitColumn(1, 3);

            // Consulta SQL NIVEL 4
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionNivel4_Consulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorNivel4 = new SqlDataAdapter(conexiones.comando);
            DataSet dsNivel4 = new DataSet();
            adaptadorNivel4.Fill(dsNivel4);

            sl.AddWorksheet("NIVEL 4");
            sl.Filter("A1", "B1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Nivel 4'!$A$1:$A$" + cantidadConceptosNivel4.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Valores Nivel 4'!$A$1:$A$" + cantidadValoresNivel4.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "Valor");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 20);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 2; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsNivel4.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto_Nivel4].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Valor_Nivel4].ToString());

                fila++;
            }
            sl.AutoFitColumn(1, 2);

            // Consulta SQL SERVICIOS
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionServicio_Consulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorServicio = new SqlDataAdapter(conexiones.comando);
            DataSet dsServicio = new DataSet();
            adaptadorServicio.Fill(dsServicio);

            sl.AddWorksheet("SERVICIOS");
            sl.Filter("A1", "D1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Familias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Subfamilias'!$A$1:$A$" + cantidadSubfamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Tipos servicio'!$A$1:$A$" + cantidadTiposServicios.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Familia");
            sl.SetCellValue(1, 2, "Subfamilia");
            sl.SetCellValue(1, 3, "Artículo");
            sl.SetCellValue(1, 4, "Tipo servicio");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 4; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsServicio.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_FamiliaServicios].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_SubfamiliaServicios].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_ArticuloServicios].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_TipoServicio].ToString());

                fila++;
            }
            sl.AutoFitColumn(1, 4);

            // Consulta SQL TRANSPORTE
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTransporteConsultas";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorTransporte = new SqlDataAdapter(conexiones.comando);
            DataSet dsTransporte = new DataSet();
            adaptadorTransporte.Fill(dsTransporte);
            adaptadorTransporte.Dispose();

            // TRANSPORTE GENERAL
            sl.AddWorksheet("TRANSPORTE GENERAL");
           
            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "MESES");
            sl.SetCellValue(2, 1, "% DESVÍO");
            sl.SetCellValue(3, 1, "COEFICIENTE VUELTA");
            sl.SetCellValue(4, 1, "FECHA ACTUALIZACIÓN");
            sl.SetCellValue(5, 1, "KMS MÍNIMOS");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
      
            dv = sl.CreateDataValidation("B1", "B1");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B2");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B3", "B3");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B4", "B4");
            dv.AllowCustom("", false);
            //dv.AllowDate(SLDataValidationSingleOperandValues.GreaterThanOrEqual, "01/01/2023", true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B5", "B5");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Alignment.Horizontal = HorizontalAlignmentValues.Left;
            for (columna = 1; columna <= 5; columna++)
            {
                sl.SetCellStyle(columna, 1, style);
            }
            //sl.FreezePanes(1, 1);

            fila = 1;
            foreach (DataRow Row in dsTransporte.Tables[0].Rows)
            {
                // Datos
                int.TryParse(Row.ItemArray[COL_TransporteMeses].ToString(), out valorEntero);
                sl.SetCellValue(fila, 2, valorEntero);
                sl.SetCellStyle(fila, 2, styleInt);

                decimal.TryParse(Row.ItemArray[COL_TransporteDesvio].ToString(), out valor);
                sl.SetCellValue(fila + 1, 2, valor);
                sl.SetCellStyle(fila + 1, 2, styleDec);

                decimal.TryParse(Row.ItemArray[COL_TransporteCoeficienteVuelta].ToString(), out valor);
                sl.SetCellValue(fila + 2, 2, valor);
                sl.SetCellStyle(fila + 2, 2, styleDec);

                DateTime.TryParse(Row.ItemArray[COL_TransporteFecha].ToString(), out valorFecha);
                sl.SetCellValue(fila + 3, 2, valorFecha);
                sl.SetCellStyle(fila + 3, 2, styleDate);

                int.TryParse(Row.ItemArray[COL_TransporteKilometrosMinimos].ToString(), out valorEntero);
                sl.SetCellValue(fila + 4, 2, valorEntero);
                sl.SetCellStyle(fila + 4, 2, styleInt);
                break;
            }
            sl.AutoFitColumn(1, 2);

            // TRANSPORTE
            sl.AddWorksheet("TRANSPORTE");
            sl.Filter("A1", "N1");

            dv = sl.CreateDataValidation("A2", "A1000"); // BU
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000"); // EMPRESA
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000"); // DELEGACION
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000"); // MARGEN
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "F1000"); //Desde // Hasta
            dv.AllowDate(SLDataValidationSingleOperandValues.GreaterThanOrEqual, "01/01/2023", true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("G2", "G1000"); //Distancia
            dv.AllowList("'Distancia'!$A$1:$A$" + cantidadDistancia.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("H2", "H1000"); // Prop
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("I2", "I1000"); // Valor
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.01, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("J2", "J1000"); //Desvio
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("K2", "K1000"); //Vales
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("L2", "L1000"); // Prop12Meses
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("M2", "M1000"); // Desvio12Meses
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("N2", "N1000"); // Vales12Meses
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            //dv = sl.CreateDataValidation("O2", "O1000");
            //dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            //sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "MARGEN");
            sl.SetCellValue(1, 5, "DESDE");
            sl.SetCellValue(1, 6, "HASTA");
            sl.SetCellValue(1, 7, "DISTANCIA");
            sl.SetCellValue(1, 8, "PROPUESTO");
            sl.SetCellValue(1, 9, "VALOR");
            sl.SetCellValue(1, 10, "DESVIO");
            sl.SetCellValue(1, 11, "VALES");
            sl.SetCellValue(1, 12, "MESES");
            sl.SetCellValue(1, 13, "PROPUESTO 12M");
            sl.SetCellValue(1, 14, "DESVIO 12M");
            sl.SetCellValue(1, 15, "VALES 12M");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 30);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 30);
            sl.SetColumnWidth(13, 30);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 15; columna++)
                sl.SetCellStyle(1, columna, style);

            fila = 2;
            foreach (DataRow Row in dsTransporte.Tables[1].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_TransporteDatosBU].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_TransporteDatosEmpresa].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_TransporteDatosDelegacion].ToString());
                decimal.TryParse(Row.ItemArray[COL_TransporteDatosMargen].ToString(), out valor);
                 sl.SetCellValue(fila, 4, valor);
                if (Row.ItemArray[COL_TransporteDatosDesde].ToString() == "")
                {
                    sl.SetCellValue(fila, 5, "");
                }
                else
                {
                    DateTime.TryParse(Row.ItemArray[COL_TransporteDatosDesde].ToString(), out valorFecha);
                    //sl.SetCellValue(fila, 5, valorFecha.ToString("dd/MM/yyyy"));
                    sl.SetCellValue(fila, 5, valorFecha);
                }
                if (Row.ItemArray[COL_TransporteDatosHasta].ToString() == "")
                {
                    sl.SetCellValue(fila, 6, "");
                }
                else
                {
                    DateTime.TryParse(Row.ItemArray[COL_TransporteDatosHasta].ToString(), out valorFecha);
                    //sl.SetCellValue(fila, 6, valorFecha.ToString("dd/MM/yyyy"));
                    sl.SetCellValue(fila, 6, valorFecha);
                }
                sl.SetCellValue(fila, 7, Row.ItemArray[COL_TransporteDatosDistancia].ToString());
                decimal.TryParse(Row.ItemArray[COL_TransporteDatosPropuesto].ToString(), out valor);
                //sl.SetCellValue(fila, 8, valor.ToString("#,##0.00"));
                sl.SetCellValue(fila, 8, valor);
                decimal.TryParse(Row.ItemArray[COL_TransporteDatosValor].ToString(), out valor);
                //sl.SetCellValue(fila, 9, valor.ToString("#,##0.00"));
                sl.SetCellValue(fila, 9, valor);
                decimal.TryParse(Row.ItemArray[COL_TransporteDatosDesvio].ToString(), out valor);
                 sl.SetCellValue(fila, 10, valor);
               
                int.TryParse(Row.ItemArray[COL_TransporteDatosVales].ToString(), out valorEntero);
                sl.SetCellValue(fila, 11, valorEntero);
                int.TryParse(Row.ItemArray[COL_TransporteDatosMeses].ToString(), out valorEntero);
                sl.SetCellValue(fila, 12, valorEntero);
                decimal.TryParse(Row.ItemArray[COL_TransporteDatosPropuesto12Meses].ToString(), out valor);
                sl.SetCellValue(fila, 13, valor);
                decimal.TryParse(Row.ItemArray[COL_TransporteDatosDesvio12Meses].ToString(), out valor);
                sl.SetCellValue(fila, 14, valor);
                int.TryParse(Row.ItemArray[COL_TransporteDatosVales12Meses].ToString(), out valorEntero);
                sl.SetCellValue(fila, 15, valorEntero);


                //styleDec = sl.CreateStyle();
                //styleDec.SetFont("Verdana", 10);
                //styleDec.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //styleDec.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 4, styleDec);
                sl.SetCellStyle(fila, 8, styleDec);
                sl.SetCellStyle(fila, 9, styleDec);
                sl.SetCellStyle(fila, 10, styleDec);
                sl.SetCellStyle(fila, 13, styleDec);
                sl.SetCellStyle(fila, 14, styleDec);

                sl.SetCellStyle(fila, 11, styleInt);
                sl.SetCellStyle(fila, 12, styleInt);
                sl.SetCellStyle(fila, 15, styleInt);

                //styleDate = sl.CreateStyle();
                //styleDate.SetFont("Verdana", 10);
                //styleDate.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                //styleDate.FormatCode = "dd/MM/yyyy";
                sl.SetCellStyle(fila, 5, styleDate);
                sl.SetCellStyle(fila, 6, styleDate);

                fila++;
            }
            sl.AutoFitColumn(1, 16);

            // Consulta SQL PANELES
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_DatosPanelesConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorPaneles= new SqlDataAdapter(conexiones.comando);
            DataSet dsPaneles = new DataSet();
            adaptadorPaneles.Fill(dsPaneles);

            sl.AddWorksheet("PANELES");
            sl.Filter("A1", "B1");

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Booleano'!$A$1:$A$" + cantidadBooleano.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "AAF");
            sl.SetCellValue(1, 2, "Estándar");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 2; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsPaneles.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_AAFPaneles].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_EstandarPaneles].ToString());
               
                fila++;
            }
            sl.AutoFitColumn(1, 2);

            // Consulta SQL AJUSTES
            // -- Ajuste por obra: taxímetro
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionAjustesConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorAjustes = new SqlDataAdapter(conexiones.comando);
            DataSet dsAjustes = new DataSet();
            adaptadorAjustes.Fill(dsAjustes);

            sl.AddWorksheet("ABONO TAX DURACION");
            sl.Filter("A1", "F1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "E1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "F1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "DURACIÓN MÍNIMA");
            sl.SetCellValue(1, 5, "DURACION MÁXIMA");
            sl.SetCellValue(1, 6, "% ABONO MENSUAL");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 6; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_AjustePorObraTaxBU].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_AjustePorObraTaxEmpresa].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_AjustePorObraTaxDelegacion].ToString());
                int.TryParse(Row.ItemArray[COL_AjustePorObraTaxDuracionMinima].ToString(), out valorEntero);
                //sl.SetCellValue(fila, 4, valorEntero.ToString("#,##0"));
                sl.SetCellValue(fila, 4, valorEntero);
                int.TryParse(Row.ItemArray[COL_AjustePorObraTaxDuracionMaxima].ToString(), out valorEntero);
                sl.SetCellValue(fila, 5, valorEntero);
                decimal.TryParse(Row.ItemArray[COL_AjustePorObraTaxAbonoMensual].ToString(), out valor);
                //sl.SetCellValue(fila, 6, valor.ToString("#,##0.00"));
                sl.SetCellValue(fila, 6, valor);

                sl.SetCellStyle(fila, 4, styleInt);
                sl.SetCellStyle(fila, 5, styleInt);

                sl.SetCellStyle(fila, 6, styleDec);

                fila++;
            }
            sl.AutoFitColumn(1, 6);

            // -- Ajuste por obra: gastos Fijos
            sl.AddWorksheet("AJUSTE GF DURACION");
            sl.Filter("A1", "K1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Alcance ajustes gastos fijos'!$A$1:$A$" + cantidadAlcanceAjustesGastosFijos.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "F1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("G2", "G1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("H2", "H1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("I2", "I1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("J2", "J1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("K2", "K1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "ALCANCE");
            sl.SetCellValue(1, 5, "% AJUSTE OBRA MINIMA");
            sl.SetCellValue(1, 6, "DURACION OBRA CORTA");
            sl.SetCellValue(1, 7, "% AJUSTE OBRA CORTA");
            sl.SetCellValue(1, 8, "DURACION OBRA LARGA");
            sl.SetCellValue(1, 9, "% AJUSTE OBRA LARGA");
            sl.SetCellValue(1, 10, "DURACION MÁXIMA");
            sl.SetCellValue(1, 11, "% AJUSTE OBRA MUY LARGA");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 30);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 11; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[1].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());
                decimal.TryParse(Row.ItemArray[4].ToString(), out valor);
                sl.SetCellValue(fila, 5, valor);
                int.TryParse(Row.ItemArray[5].ToString(), out valorEntero);
                sl.SetCellValue(fila, 6, valorEntero);
                decimal.TryParse(Row.ItemArray[6].ToString(), out valor);
                sl.SetCellValue(fila, 7, valor);
                int.TryParse(Row.ItemArray[7].ToString(), out valorEntero);
                sl.SetCellValue(fila, 8, valorEntero);
                decimal.TryParse(Row.ItemArray[8].ToString(), out valor);
                sl.SetCellValue(fila, 9, valor);
                int.TryParse(Row.ItemArray[9].ToString(), out valorEntero);
                sl.SetCellValue(fila, 10, valorEntero);
                decimal.TryParse(Row.ItemArray[10].ToString(), out valor);
                sl.SetCellValue(fila, 11, valor);

                //style = sl.CreateStyle();
                //style.SetFont("Verdana", 10);
                //style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //style.FormatCode = "#,##0";
                sl.SetCellStyle(fila, 6, styleInt);
                sl.SetCellStyle(fila, 8, styleInt);
                sl.SetCellStyle(fila, 10, styleInt);

                //styleDec = sl.CreateStyle();
                //styleDec.SetFont("Verdana", 10);
                //styleDec.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //styleDec.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 5, styleDec);
                sl.SetCellStyle(fila, 7, styleDec);
                sl.SetCellStyle(fila, 9, styleDec);
                sl.SetCellStyle(fila, 11, styleDec);

                fila++;
            }
            sl.AutoFitColumn(1, 11);

            // -- Ajuste por producto: ocupación
            sl.AddWorksheet("AJUSTE TAX OCUPACION");
            sl.Filter("A1", "M1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Alcance ajustes ocupacion'!$A$1:$A$" + cantidadAlcancelAjustesOcupacion.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "M1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("N2", "N1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, -100.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("O2", "Q1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "ALCANCE");
            sl.SetCellValue(1, 5, "PERIODO OCUPACIÓN");
            sl.SetCellValue(1, 6, "OCUPACION MUY BAJA");
            sl.SetCellValue(1, 7, "% ABONO MÁXIMO");
            sl.SetCellValue(1, 8, "OCUPACION BAJA");
            sl.SetCellValue(1, 9, "% ABONO OCUPACION BAJA");
            sl.SetCellValue(1, 10, "OCUPACION MEDIA");
            sl.SetCellValue(1, 11, "% ABONO OCUPACION MEDIA");
            sl.SetCellValue(1, 12, "OCUPACION ALTA");
            sl.SetCellValue(1, 13, "% ABONO OCUPACION ALTA");
            sl.SetCellValue(1, 14, "% ABONO OCUPACION MUY ALTA");
            sl.SetCellValue(1, 15, "% PAIS");
            sl.SetCellValue(1, 16, "% EMPRESA");
            sl.SetCellValue(1, 17, "% DELEGACION");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 30);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 30);
            sl.SetColumnWidth(13, 30);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 30);
            sl.SetColumnWidth(16, 30);
            sl.SetColumnWidth(17, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 17; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[2].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());
                int.TryParse(Row.ItemArray[4].ToString(), out valorEntero);
                sl.SetCellValue(fila, 5, valorEntero);
                decimal.TryParse(Row.ItemArray[5].ToString(), out valor);
                sl.SetCellValue(fila, 6, valor);
                decimal.TryParse(Row.ItemArray[6].ToString(), out valor);
                sl.SetCellValue(fila, 7, valor);
                decimal.TryParse(Row.ItemArray[7].ToString(), out valor);
                sl.SetCellValue(fila, 8, valor);
                decimal.TryParse(Row.ItemArray[8].ToString(), out valor);
                sl.SetCellValue(fila, 9, valor);
                decimal.TryParse(Row.ItemArray[9].ToString(), out valor);
                sl.SetCellValue(fila, 10, valor);
                decimal.TryParse(Row.ItemArray[10].ToString(), out valor);
                sl.SetCellValue(fila, 11, valor);
                decimal.TryParse(Row.ItemArray[11].ToString(), out valor);
                sl.SetCellValue(fila, 12, valor);
                decimal.TryParse(Row.ItemArray[12].ToString(), out valor);
                sl.SetCellValue(fila, 13, valor);
                decimal.TryParse(Row.ItemArray[13].ToString(), out valor);
                sl.SetCellValue(fila, 14, valor);
                decimal.TryParse(Row.ItemArray[14].ToString(), out valor);
                sl.SetCellValue(fila, 15, valor);
                decimal.TryParse(Row.ItemArray[15].ToString(), out valor);
                sl.SetCellValue(fila, 16, valor);
                decimal.TryParse(Row.ItemArray[16].ToString(), out valor);
                sl.SetCellValue(fila, 17, valor);

                sl.SetCellStyle(fila, 5, styleInt);
                sl.SetCellStyle(fila, 6, styleDec);
                sl.SetCellStyle(fila, 7, styleDec);
                sl.SetCellStyle(fila, 8, styleDec);
                sl.SetCellStyle(fila, 9, styleDec);
                sl.SetCellStyle(fila, 10, styleDec);
                sl.SetCellStyle(fila, 11, styleDec);
                sl.SetCellStyle(fila, 12, styleDec);
                sl.SetCellStyle(fila, 13, styleDec);
                sl.SetCellStyle(fila, 14, styleDec);
                sl.SetCellStyle(fila, 15, styleDec);
                sl.SetCellStyle(fila, 16, styleDec);
                sl.SetCellStyle(fila, 17, styleDec);

                fila++;
            }
            sl.AutoFitColumn(1, 17);

            // -- Ajuste por cliente: volumen
            sl.AddWorksheet("AJUSTES POR CLIENTE VOLUMEN");
            sl.Filter("A1", "H1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Ajustes alcance volumne cliente'!$A$1:$A$" + cantidadAjustesAlcanceVolumneCliente.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "F1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("G2", "G1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("H2", "H1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "ALCANCE");
            sl.SetCellValue(1, 5, "VALOR CLIENTE MEDIANO");
            sl.SetCellValue(1, 6, "% CORRECIÓN CLIENTE MEDIANO");
            sl.SetCellValue(1, 7, "VALOR CLIENTE GRANDE");
            sl.SetCellValue(1, 8, "% CORRECIÓN CLIENTE GRANDE");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 8; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[3].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());
                int.TryParse(Row.ItemArray[4].ToString(), out valorEntero);
                sl.SetCellValue(fila, 5, valorEntero);
                decimal.TryParse(Row.ItemArray[5].ToString(), out valor);
                sl.SetCellValue(fila, 6, valor);
                int.TryParse(Row.ItemArray[6].ToString(), out valorEntero);
                sl.SetCellValue(fila, 7, valorEntero);
                decimal.TryParse(Row.ItemArray[7].ToString(), out valor);
                sl.SetCellValue(fila, 8, valor);

                //style = sl.CreateStyle();
                //style.SetFont("Verdana", 10);
                //style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //style.FormatCode = "#,##0";
                sl.SetCellStyle(fila, 5, styleInt);
                sl.SetCellStyle(fila, 7, styleInt);

                //styleDec = sl.CreateStyle();
                //styleDec.SetFont("Verdana", 10);
                //styleDec.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                //styleDec.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 6, styleDec);
                sl.SetCellStyle(fila, 8, styleDec);

                fila++;
            }
            sl.AutoFitColumn(1, 8);

            // -- Ajuste BU
            sl.AddWorksheet("AJUSTE CUMP PRESUP");
            sl.Filter("A1", "L1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "H1000");
            dv.AllowList("'Booleano'!$A$1:$A$" + cantidadBooleano.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("I2", "L1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "MESES");
            sl.SetCellValue(1, 5, "CUMPLIMIENTO BAJO GFC");
            sl.SetCellValue(1, 6, "CUMPLIMIENTO BAJO GFBU");
            sl.SetCellValue(1, 7, "CUMPLIMIENTO ALTO GFC");
            sl.SetCellValue(1, 8, "CUMPLIMIENTO ALTO GFBU");
            sl.SetCellValue(1, 9, "CUMPLIMIENTO BAJO %");
            sl.SetCellValue(1, 10, "BONIFICACIÓN CUMPLIMIENTO BAJO %");
            sl.SetCellValue(1, 11, "CUMPLIMIENTO ALTO %");
            sl.SetCellValue(1, 12, "BONIFICACIÓN CUMPLIMIENTO ALTO %");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 30);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 12; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[4].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                int.TryParse(Row.ItemArray[3].ToString(), out valorEntero);
                sl.SetCellValue(fila, 4, valorEntero);
                sl.SetCellValue(fila, 5, Row.ItemArray[4].ToString());
                sl.SetCellValue(fila, 6, Row.ItemArray[5].ToString());
                sl.SetCellValue(fila, 7, Row.ItemArray[6].ToString());
                sl.SetCellValue(fila, 8, Row.ItemArray[7].ToString());
                decimal.TryParse(Row.ItemArray[8].ToString(), out valor);
                sl.SetCellValue(fila, 9, valor);
                decimal.TryParse(Row.ItemArray[9].ToString(), out valor);
                sl.SetCellValue(fila, 10, valor);
                decimal.TryParse(Row.ItemArray[10].ToString(), out valor);
                sl.SetCellValue(fila, 11, valor);
                decimal.TryParse(Row.ItemArray[11].ToString(), out valor);
                sl.SetCellValue(fila, 12, valor);

                sl.SetCellStyle(fila, 4, styleInt);
                sl.SetCellStyle(fila, 9, styleDec);
                sl.SetCellStyle(fila, 10, styleDec);
                sl.SetCellStyle(fila, 11, styleDec);
                sl.SetCellStyle(fila, 12, styleDec);

                fila++;
            }
            sl.AutoFitColumn(1, 12);

            // -- Topes
            sl.AddWorksheet("TOPES AJUSTES");
            sl.Filter("A1", "E1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Ajustes topes'!$A$1:$A$" + cantidadAjustesTopes.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "CONCEPTO");
            sl.SetCellValue(1, 5, "VALOR MÁXIMO %");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
          
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 5; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[5].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());
                decimal.TryParse(Row.ItemArray[4].ToString(), out valor);
                sl.SetCellValue(fila, 5, valor);

                sl.SetCellStyle(fila, 5, styleDec);

                fila++;
            }
            //sl.AutoFitColumn(1, 7);
            sl.AutoFitColumn(1, 5);

            // Consulta SQL Fallidos
            sl.AddWorksheet("FALLIDOS");
            sl.Filter("A1", "F1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Delegaciones'!$A$1:$A$" + cantidadDelegaciones.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowList("'Fallidos IM'!$A$1:$A$" + cantidadFallidosIM.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "F1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "BU");
            sl.SetCellValue(1, 2, "EMPRESA");
            sl.SetCellValue(1, 3, "DELEGACIÓN");
            sl.SetCellValue(1, 4, "IM");
            sl.SetCellValue(1, 5, "% COBRADOS");
            sl.SetCellValue(1, 6, "% FALLIDOS");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(210, 236, 254);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(210, 236, 254), System.Drawing.Color.FromArgb(210, 236, 254));
            style.SetFontColor(System.Drawing.Color.Black);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 6; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsAjustes.Tables[6].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());
                decimal.TryParse(Row.ItemArray[4].ToString(), out valor);
                sl.SetCellValue(fila, 5, valor);
                decimal.TryParse(Row.ItemArray[5].ToString(), out valor);
                sl.SetCellValue(fila, 6, valor);

                sl.SetCellStyle(fila, 5, styleDec);
                sl.SetCellStyle(fila, 6, styleDec);

                fila++;
            }
            sl.AutoFitColumn(1, 6);

            // Consulta SQL BUs
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionBUsConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorBUs = new SqlDataAdapter(conexiones.comando);
            DataSet dsBU = new DataSet();
            adaptadorBUs.Fill(dsBU);

            sl.AddWorksheet("BU-EMPRESAS");
            sl.Filter("A1", "D1");
            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowDate(SLDataValidationSingleOperandValues.GreaterThanOrEqual, "01/01/2023", true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowDate(SLDataValidationSingleOperandValues.GreaterThanOrEqual, "01/01/2023", true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'BUs'!$A$1:$A$" + cantidadBUs.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Desde");
            sl.SetCellValue(1, 2, "Hasta");
            sl.SetCellValue(1, 3, "BU");
            sl.SetCellValue(1, 4, "Empresa");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 4; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsBU.Tables[0].Rows)
            {
                // Datos
                if (Row.ItemArray[0].ToString().Length > 0)
                {
                    DateTime.TryParse(Row.ItemArray[0].ToString(), out valorFecha);
                    sl.SetCellValue(fila, 1, valorFecha);
                }
                if (Row.ItemArray[1].ToString().Length > 0)
                {
                    DateTime.TryParse(Row.ItemArray[1].ToString(), out valorFecha);
                    sl.SetCellValue(fila, 2, valorFecha);
                }
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());

                sl.SetCellStyle(fila, 1, styleDate);
                sl.SetCellStyle(fila, 2, styleDate);

                fila++;
            }
            sl.AutoFitColumn(1, 10);

            // Consulta SQL Contro de cambios
            //conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionHistoricoConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adaptadorControlCambios = new SqlDataAdapter(conexiones.comando);
            DataSet dsControlCammbios = new DataSet();
            adaptadorControlCambios.Fill(dsControlCammbios);

            sl.AddWorksheet("CONTROL DE CAMBIOS");
            ps = new SLPageSettings();
            ps.TabColor = System.Drawing.Color.Azure;
            sl.SetPageSettings(ps, "CONTROL DE CAMBIOS");

            SLSheetProtection sp = new SLSheetProtection();
            sp.AllowInsertRows = false;
            sp.AllowInsertColumns = false;
            sp.AllowFormatCells = false;
            sp.AllowDeleteColumns = false;
            sp.AllowEditObjects = false;
            sl.ProtectWorksheet(sp);
            sl.Filter("A1", "H1");

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Versión");
            sl.SetCellValue(1, 2, "Desde");
            sl.SetCellValue(1, 3, "Hasta");
            sl.SetCellValue(1, 4, "Grado de cambio");
            sl.SetCellValue(1, 5, "Observaciones");
            sl.SetCellValue(1, 6, "Fecha");
            sl.SetCellValue(1, 7, "Hora");
            sl.SetCellValue(1, 8, "Usuario");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 8; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 8);

            fila = 2;
            foreach (DataRow Row in dsControlCammbios.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[1].ToString());
                if (Row.ItemArray[2].ToString().Length > 0)
                {
                    DateTime.TryParse(Row.ItemArray[2].ToString(), out valorFecha);
                    sl.SetCellValue(fila, 2, valorFecha);
                }
                if (Row.ItemArray[3].ToString().Length > 0)
                {
                    DateTime.TryParse(Row.ItemArray[3].ToString(), out valorFecha);
                    sl.SetCellValue(fila, 3, valorFecha);
                }
                sl.SetCellValue(fila, 4, Row.ItemArray[4].ToString());
                sl.SetCellValue(fila, 5, Row.ItemArray[5].ToString());
                sl.SetCellValue(fila, 6, Row.ItemArray[6].ToString());
                sl.SetCellValue(fila, 7, Row.ItemArray[7].ToString());
                sl.SetCellValue(fila, 8, Row.ItemArray[8].ToString());

                sl.SetCellStyle(fila, 2, styleDate);
                sl.SetCellStyle(fila, 3, styleDate);

                fila++;
            }
            sl.AutoFitColumn(1, 8);

            // TOPES DEFINICIÓN CONFIGURACIÓN
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionDefinicionTopes";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adaptadorDefinicionTopes = new SqlDataAdapter(conexiones.comando);
            DataSet dsDefinicionTopes = new DataSet();
            adaptadorDefinicionTopes.Fill(dsDefinicionTopes);

            sl.AddWorksheet("TOPES_CONFIG");
            ps = new SLPageSettings();
            ps.TabColor = System.Drawing.Color.Green;
            sl.SetPageSettings(ps, "TOPES_CONFIG");

            sl.Filter("A1", "F1");

            dv = sl.CreateDataValidation("A2", "C1000");
            dv.AllowCustom("", false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, -100, false);
            //dv.SetInputMessage("", "Celda acepta valores enteros mayores o iguales a cero");
            //dv.SetErrorAlert(DataValidationErrorStyleValues.Stop, "¡ERROR!", "El campo sólo acepta enteros positivos");
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("E2", "E1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            //dv.SetInputMessage("", "Celda acepta valores enteros mayores o iguales a cero");
            //dv.SetErrorAlert(DataValidationErrorStyleValues.Stop, "¡ERROR!", "El campo sólo acepta enteros positivos");
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "F1000");
            dv.AllowList("'Monedas'!$A$1:$A$" + cantidadMonedas.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "PESTAÑA");
            sl.SetCellValue(1, 2, "CONCEPTO");
            sl.SetCellValue(1, 3, "COLUMNA");
            sl.SetCellValue(1, 4, "MÍNIMO");
            sl.SetCellValue(1, 5, "MÁXIMO");
            sl.SetCellValue(1, 6, "MONEDA");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkGreen, System.Drawing.Color.DarkGreen);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 6; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 8);

            fila = 2;
            foreach (DataRow Row in dsDefinicionTopes.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                int.TryParse(Row.ItemArray[3].ToString(), out valorEntero);
                sl.SetCellValue(fila, 4, valorEntero);
                int.TryParse(Row.ItemArray[4].ToString(), out valorEntero);
                sl.SetCellValue(fila, 5, valorEntero);
                sl.SetCellValue(fila, 6, Row.ItemArray[5].ToString());

                sl.SetCellStyle(fila, 4, styleInt);
                sl.SetCellStyle(fila, 5, styleInt);

                fila++;
            }
            sl.AutoFitColumn(1, 6);

            sl.SaveAs(nombreInforme);
            btnAbrirExcel.Visible = true;
        }

        //private void exportarExcelTodo()
        //{
        //    SLStyle style;
        //    int fila;
        //    int columna = 0;

        //    btnAbrirExcelGeneral.Visible = false;
        //    // Consulta SQL
        //    conexiones.crearConexion();
        //    conexiones.comando = conexiones.conexion.CreateCommand();
        //    conexiones.comando.CommandText = "sp_ROP_ConfiguracionConsulta";
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;
        //    conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
        //    conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
        //    if (cmbVersionGeneralExportar.Text == "TODOS")
        //        conexiones.comando.Parameters.AddWithValue("@tipo", 0);
        //    else if (cmbVersionGeneralExportar.Text == "TODOS Reales")
        //        conexiones.comando.Parameters.AddWithValue("@tipo", 1);
        //    else
        //        conexiones.comando.Parameters.AddWithValue("@tipo", 2);
        //    SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
        //    DataSet ds = new DataSet();
        //    adaptador.Fill(ds);

        //    // Crear el excel
        //    SLDocument sl = new SLDocument();

        //    sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Configuracion versión");

        //    // Crear el encabezado del informe
        //    sl.SetCellValue(1, 1, "ID");
        //    sl.SetCellValue(1, 2, "Versión");
        //    sl.SetCellValue(1, 3, "Desde");
        //    sl.SetCellValue(1, 4, "Hasta");
        //    sl.SetCellValue(1, 5, "Actualización");
        //    sl.SetCellValue(1, 6, "Subgrupo");
        //    sl.SetCellValue(1, 7, "Concepto");
        //    sl.SetCellValue(1, 8, "Empresa");
        //    sl.SetCellValue(1, 9, "Familia");
        //    sl.SetCellValue(1, 10, "Subfamilia");
        //    sl.SetCellValue(1, 11, "Artículo");
        //    sl.SetCellValue(1, 12, "Valor");

        //    style = sl.CreateStyle();
        //    sl.SetColumnWidth(1, 20);
        //    sl.SetColumnWidth(2, 30);
        //    sl.SetColumnWidth(3, 20);
        //    sl.SetColumnWidth(4, 20);
        //    sl.SetColumnWidth(5, 30);
        //    sl.SetColumnWidth(6, 20);
        //    sl.SetColumnWidth(7, 20);
        //    sl.SetColumnWidth(8, 20);
        //    sl.SetColumnWidth(9, 20);
        //    sl.SetColumnWidth(10, 20);
        //    sl.SetColumnWidth(11, 20);
        //    sl.SetColumnWidth(12, 20);
        //    style.SetFont("Verdana", 10);
        //    style.Font.Bold = true;
        //    style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
        //    style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
        //    style.SetFontColor(System.Drawing.Color.White);
        //    style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //    for (columna = 1; columna <= 12; columna++)
        //        sl.SetCellStyle(1, columna, style);

        //    fila = 2;
        //    foreach (DataRow Row in ds.Tables[0].Rows)
        //    {
        //        // Datos
        //        sl.SetCellValue(fila, 1, Row.ItemArray[COL_Version_ID].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 1, style);

        //        sl.SetCellValue(fila, 2, Row.ItemArray[COL_Version_Version].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 2, style);

        //        sl.SetCellValue(fila, 3, Row.ItemArray[COL_Version_Desde].ToString());
        //        sl.SetCellValue(fila, 4, Row.ItemArray[COL_Version_Hasta].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        style.FormatCode = "dd-MM-yyyy";
        //        sl.SetCellStyle(fila, 3, style);
        //        sl.SetCellStyle(fila, 4, style);

        //        sl.SetCellValue(fila, 5, Row.ItemArray[COL_Version_Grupo].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 5, style);

        //        sl.SetCellValue(fila, 6, Row.ItemArray[COL_Version_Subgrupo].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 6, style);

        //        sl.SetCellValue(fila, 7, Row.ItemArray[COL_Version_Concepto].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 7, style);

        //        sl.SetCellValue(fila, 8, Row.ItemArray[COL_Version_Empresa].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 8, style);

        //        sl.SetCellValue(fila, 9, Row.ItemArray[COL_Version_Familia].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 9, style);

        //        sl.SetCellValue(fila, 10, Row.ItemArray[COL_Version_Subfamilia].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 10, style);

        //        sl.SetCellValue(fila, 11, Row.ItemArray[COL_Version_Articulo].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        //        sl.SetCellStyle(fila, 11, style);

        //        sl.SetCellValue(fila, 12, Row.ItemArray[COL_Version_Valor].ToString());
        //        style = sl.CreateStyle();
        //        style.SetFont("Verdana", 10);
        //        style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
        //        style.FormatCode = "#.##0,00";
        //        sl.SetCellStyle(fila, 12, style);
        //        fila++;
        //    }

        //    sl.SaveAs(nombreInforme);
        //    btnAbrirExcelGeneral.Visible = true;
        //}
        #endregion

        #region "Servicios"

        //protected void rellenarServicio()
        //{
        //    cmbTipo.DataSource = null;
        //    conexiones.crearConexion();
        //    conexiones.consulta = "sp_ROP_servicioConsulta";
        //    SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    adaptador.Fill(dt);
        //    cmbTipo.DataSource = dt;
        //    cmbTipo.DataTextField = "CFGSERV_Tipo";
        //    cmbTipo.DataValueField = "CFGSERV_Tipo";
        //    cmbTipo.DataBind();
        //    conexiones.conexion.Close();
        //}
        //protected void btnLimpiarServicio_Click(object sender, EventArgs e)
        //{
        //    txtFamilia.Text = "";
        //    txtSubfamilia.Text = "";
        //    txtArticulo.Text = "";
        //    cmbTipo.Text = "";
        //}

        //protected void btnAgregarServicio_Click(object sender, EventArgs e)
        //{
        //    if ((txtFamilia.Text != "" || txtSubfamilia.Text != "" || txtArticulo.Text != "") && cmbTipo.Text != "")
        //    {
        //        conexiones.crearConexion();
        //        conexiones.consulta = "sp_ROP_ConfiguracionServicioAgregar";
        //        conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //        conexiones.comando.CommandType = CommandType.StoredProcedure;

        //        SqlParameter parametroID = new SqlParameter("@CFGSERV_ID", SqlDbType.Int);
        //        parametroID.Value = null;
        //        conexiones.comando.Parameters.Add(parametroID);
        //        SqlParameter parametroFamilia = new SqlParameter("@Familia", SqlDbType.VarChar, 20);
        //        if (txtFamilia.Text == "")
        //            parametroFamilia.Value = null;
        //        else
        //            parametroFamilia.Value = txtFamilia.Text;
        //        conexiones.comando.Parameters.Add(parametroFamilia);
        //        SqlParameter parametroSubfamilia = new SqlParameter("@Subfamilia", SqlDbType.VarChar, 20);
        //        if (txtSubfamilia.Text == "")
        //            parametroSubfamilia.Value = null;
        //        else
        //            parametroSubfamilia.Value = txtSubfamilia.Text;
        //        conexiones.comando.Parameters.Add(parametroSubfamilia);
        //        SqlParameter parametroArticulo = new SqlParameter("@ART_ID", SqlDbType.VarChar, 20);
        //        if (txtArticulo.Text == "")
        //            parametroArticulo.Value = null;
        //        else
        //            parametroArticulo.Value = txtArticulo.Text;
        //        conexiones.comando.Parameters.Add(parametroArticulo);
        //        SqlParameter parametroTipo = new SqlParameter("@CFGSERV_Tipo", SqlDbType.VarChar, 50);
        //        parametroTipo.Value = cmbTipo.Text;
        //        conexiones.comando.Parameters.Add(parametroTipo);

        //        SqlDataReader dr = conexiones.comando.ExecuteReader();
        //        conexiones.conexion.Close();
        //    }
        //    else
        //    {
        //        lblTituloError.Text = "Agregar servicio";
        //        lblMensajeError.Text = "Debe indicar la familia/subfamilia/artículo y el tipo de servicio.";
        //        mpeError.Show();
        //    }
        //    txtFamilia.Text = "";
        //    txtSubfamilia.Text = "";
        //    txtArticulo.Text = "";
        //    cmbTipo.Text = "";
        //    rellenarGridServicios();
        //}

        protected void CambioFiltroVersionServicios(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersionServicios"] = cmbFiltroVersion.SelectedValue;
            this.rellenarGridServicios();
        }

        private void rellenarFiltroVersionServicios(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionServiciosVersionConsulta";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CFG_Version";
            cmbFiltroVersion.DataValueField = "CFG_Version";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroVersionServicios"].ToString()).Selected = true;
        }

        private void rellenarGridServicios()
        {
            DropDownList cmbFiltro;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionServicioConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersionServicios"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersionServicios"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvServicios.DataSource = dr;
            grvServicios.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvServicios.HeaderRow.FindControl("FiltroVersionServicios");
            this.rellenarFiltroVersionServicios(cmbFiltro);
        }

        protected void grvServicios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvServicios.EditIndex = e.NewEditIndex;
            rellenarGridServicios();
        }

        protected void grvServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //Find the DropDownList in the Row
            //    DropDownList cmbTipoServicio = (e.Row.FindControl("cmbTipoServicio") as DropDownList);

            //    cmbTipoServicio.DataSource = null;
            //    conexiones.crearConexion();
            //    conexiones.consulta = "sp_ROP_servicioConsulta";
            //    SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            //    System.Data.DataTable dt = new System.Data.DataTable();
            //    adaptador.Fill(dt);
            //    cmbTipoServicio.DataSource = dt;
            //    cmbTipoServicio.DataTextField = "CFGSERV_Tipo";
            //    cmbTipoServicio.DataValueField = "CFGSERV_Tipo";
            //    cmbTipoServicio.DataBind();
            //    conexiones.conexion.Close();
            //    cmbTipoServicio.Items.Insert(0, new ListItem(""));
            //    string tipo = (e.Row.FindControl("lblTipoServicio") as Label).Text;
            //    cmbTipoServicio.Items.FindByValue(tipo).Selected = true;
            //    cmbTipoServicio.Enabled = e.Row.RowIndex == grvServicios.EditIndex;
            //}
        }

        protected void grvServicios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvServicios.Rows[e.RowIndex].Cells[COLGRID_ID].Controls[0]).Text;
            string Familia = ((System.Web.UI.WebControls.TextBox)grvServicios.Rows[e.RowIndex].Cells[COLGRID_Familia].Controls[0]).Text;
            string Subfamilia = ((System.Web.UI.WebControls.TextBox)grvServicios.Rows[e.RowIndex].Cells[COLGRID_Subfamilia].Controls[0]).Text;
            string ART_ID = ((System.Web.UI.WebControls.TextBox)grvServicios.Rows[e.RowIndex].Cells[COLGRID_ART_ID].Controls[0]).Text;
            DropDownList cmbTipoServicio = grvServicios.Rows[e.RowIndex].FindControl("cmbTipoServicio") as DropDownList;
            string tipoServicio = Convert.ToString(cmbTipoServicio.SelectedValue);

            if ((ART_ID != "" || Familia != "" || Subfamilia != "") && tipoServicio != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionServicioAgregar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroID = new SqlParameter("@CFGSERV_ID", SqlDbType.Int);
                parametroID.Value = Convert.ToInt32(ID);
                conexiones.comando.Parameters.Add(parametroID);
                SqlParameter parametroFamilia = new SqlParameter("@Familia", SqlDbType.VarChar, 20);
                if (Familia == "")
                    parametroFamilia.Value = null;
                else
                    parametroFamilia.Value = Familia;
                conexiones.comando.Parameters.Add(parametroFamilia);
                SqlParameter parametroSubfamilia = new SqlParameter("@Subfamilia", SqlDbType.VarChar, 20);
                if (Subfamilia == "")
                    parametroSubfamilia.Value = null;
                else
                    parametroSubfamilia.Value = Subfamilia;
                conexiones.comando.Parameters.Add(parametroSubfamilia);
                SqlParameter parametroArticulo = new SqlParameter("@ART_ID", SqlDbType.VarChar, 20);
                if (ART_ID == "")
                    parametroArticulo.Value = null;
                else
                    parametroArticulo.Value = ART_ID;
                conexiones.comando.Parameters.Add(parametroArticulo);
                SqlParameter parametroTipo = new SqlParameter("@CFGSERV_Tipo", SqlDbType.VarChar, 50);
                parametroTipo.Value = tipoServicio;
                conexiones.comando.Parameters.Add(parametroTipo);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();

                grvServicios.EditIndex = -1;
                rellenarGridServicios();
            }
            else
            {
                lblTituloError.Text = "Modificar servicio";
                lblMensajeError.Text = "Debe indicar la familia/subfamilia/artículo y el tipo de servicio.";
                mpeError.Show();
            }
        }

        protected void grvServicios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvServicios.EditIndex = -1;
            rellenarGridServicios();
        }

        protected void grvServicios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string ID = grvServicios.DataKeys[e.RowIndex].Values["CFGSERV_ID"].ToString();

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionServicioEliminar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@CFGSERV_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            rellenarGridServicios();
        }
        #endregion

        #region "Transporte"

        protected void txtDesvioTransporte_TextChanged(object sender, EventArgs e)
        {
            decimal numero = Convert.ToDecimal(txtDesvioTransporte.Text.Replace(".", ","));
            txtDesvioTransporte.Text = numero.ToString("N2");
        }

        protected void txtCoeficienteVuelta_TextChanged(object sender, EventArgs e)
        {
            decimal numero = Convert.ToDecimal(txtCoeficienteVuelta.Text.Replace(".", ","));
            txtCoeficienteVuelta.Text = numero.ToString("N2");
        }

        protected void txtKilometrosMinimos_TextChanged(object sender, EventArgs e)
        {
            decimal numero = Convert.ToDecimal(txtKilometrosMinimos.Text.Replace(".", ","));
            txtKilometrosMinimos.Text = numero.ToString("N0");
        }
        protected void btnActualizarTransporteGeneral_Click(object sender, EventArgs e)
        {
            conexiones.crearConexion();
            conexiones.consulta = "ROP_Transporte";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            rellenarTransporteGeneral();
            rellenarGridTransporte();
        }

        private void rellenarTransporteGeneral()
        {
            txtFechaActualizar.Text = "";
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteGeneralConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                transporsteID = dr.GetInt32(CAMPO_IDTransporte);
                txtMeses.Text = dr.GetInt32(CAMPO_Meses).ToString("#,##0");
                txtDesvioTransporte.Text = dr.GetDecimal(CAMPO_Desvio).ToString("N2");
                txtCoeficienteVuelta.Text = dr.GetDecimal(CAMPO_CoeficienteVuelta).ToString("N2"); 
                txtFechaActualizar.Text = dr.GetDateTime(CAMPO_Fecha).ToString("dd/MM/yyyy");
                txtKilometrosMinimos.Text = dr.GetInt32(CAMPO_KilometrosMinimos).ToString("#,##0");
            }
            conexiones.conexion.Close();
            txtMeses.Enabled = false;
            txtDesvioTransporte.Enabled = false;
            txtCoeficienteVuelta.Enabled = false;
            txtKilometrosMinimos.Enabled = false;
        }

        protected void CambioFiltroEmpresaTransporte(object sender, EventArgs e)
        {
            DropDownList cmbFiltroEmpresaTransporte = (DropDownList)sender;
            ViewState["FiltroEmpresaTransporte"] = cmbFiltroEmpresaTransporte.SelectedValue;
            this.rellenarGridTransporte();
        }

        private void rellenarFiltroEmpresaTransporte(DropDownList cmbFiltroEmpresa)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTransporteEmpresaConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (!chkBoxActivos.Checked)
                conexiones.comando.Parameters.AddWithValue("@activos", false);
            else
                conexiones.comando.Parameters.AddWithValue("@activos", true); 
            if (ViewState["FiltroEmpresaTransporte"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@empresa", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@empresa", ViewState["FiltroEmpresaTransporte"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroEmpresa.DataSource = dt;
            cmbFiltroEmpresa.DataTextField = "Empresa";
            cmbFiltroEmpresa.DataValueField = "Empresa";
            cmbFiltroEmpresa.DataBind();
            conexiones.conexion.Close();
            cmbFiltroEmpresa.Items.FindByValue(ViewState["FiltroEmpresaTransporte"].ToString()).Selected = true;
        }

        protected void CambioFiltroDelegacionTransporte(object sender, EventArgs e)
        {
            DropDownList cmbFiltroDelegacionTransporte = (DropDownList)sender;
            ViewState["FiltroDelegacionTransporte"] = cmbFiltroDelegacionTransporte.SelectedValue;
            this.rellenarGridTransporte();
        }

        private void rellenarFiltroDelegacionTransporte(DropDownList cmbFiltroDelegacion)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTransporteDelegacionConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (!chkBoxActivos.Checked)
                conexiones.comando.Parameters.AddWithValue("@activos", false);
            else
                conexiones.comando.Parameters.AddWithValue("@activos", true); 
            if (ViewState["FiltroDelegacionTransporte"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@delegacion", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@delegacion", ViewState["FiltroDelegacionTransporte"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroDelegacion.DataSource = dt;
            cmbFiltroDelegacion.DataTextField = "Delegacion";
            cmbFiltroDelegacion.DataValueField = "Delegacion";
            cmbFiltroDelegacion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroDelegacion.Items.FindByValue(ViewState["FiltroDelegacionTransporte"].ToString()).Selected = true;
        }

        protected void CambioFiltroDesdeTransporte(object sender, EventArgs e)
        {
            DropDownList cmbFiltroDesdeTransporte = (DropDownList)sender;
            ViewState["FiltroDesdeTransporte"] = cmbFiltroDesdeTransporte.SelectedValue;
            this.rellenarGridTransporte();
        }

        private void rellenarFiltroDesdeTransporte(DropDownList cmbFiltroDesde)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTransporteDesdeConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (!chkBoxActivos.Checked)
                conexiones.comando.Parameters.AddWithValue("@activos", false);
            else
                conexiones.comando.Parameters.AddWithValue("@activos", true); 
            if (ViewState["FiltroDesdeTransporte"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@desde", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@desde", Convert.ToDateTime(ViewState["FiltroDesdeTransporte"].ToString()));
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroDesde.DataSource = dt;
            cmbFiltroDesde.DataTextField = "Desde";
            cmbFiltroDesde.DataValueField = "Desde";
            cmbFiltroDesde.DataBind();
            conexiones.conexion.Close();
            cmbFiltroDesde.Items.FindByValue(ViewState["FiltroDesdeTransporte"].ToString()).Selected = true;
        }

        protected void CambioFiltroDistanciaTransporte(object sender, EventArgs e)
        {
            DropDownList cmbFiltroDistanciaTransporte = (DropDownList)sender;
            ViewState["FiltroDistanciaTransporte"] = cmbFiltroDistanciaTransporte.SelectedValue;
            this.rellenarGridTransporte();
        }

        private void rellenarFiltroDistanciaTransporte(DropDownList cmbFiltroDistancia)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionTransporteDistanciaConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (!chkBoxActivos.Checked)
                conexiones.comando.Parameters.AddWithValue("@activos", false);
            else
                conexiones.comando.Parameters.AddWithValue("@activos", true);
            if (ViewState["FiltroDistanciaTransporte"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@distancia", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@distancia", ViewState["FiltroDistanciaTransporte"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroDistancia.DataSource = dt;
            cmbFiltroDistancia.DataTextField = "Distancia";
            cmbFiltroDistancia.DataValueField = "Distancia";
            cmbFiltroDistancia.DataBind();
            conexiones.conexion.Close();
            cmbFiltroDistancia.Items.FindByValue(ViewState["FiltroDistanciaTransporte"].ToString()).Selected = true;
        }


        protected void chkBoxActivos_CheckedChanged(object sender, EventArgs e)
        {
            rellenarGridTransporte();
        }
   
        private void rellenarGridTransporte()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroActivos = new SqlParameter("@activos", SqlDbType.Bit);
            if (!chkBoxActivos.Checked)
                parametroActivos.Value = false;
            else
                parametroActivos.Value = true;
            conexiones.comando.Parameters.Add(parametroActivos);
            SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            if (ViewState["FiltroEmpresaTransporte"].ToString() == "")
                parametroEmpresa.Value = null;
            else
                parametroEmpresa.Value = ViewState["FiltroEmpresaTransporte"].ToString();
            conexiones.comando.Parameters.Add(parametroEmpresa);
            SqlParameter parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            if (ViewState["FiltroDelegacionTransporte"].ToString() == "")
                parametroDelegacion.Value = null;
            else
                parametroDelegacion.Value = ViewState["FiltroDelegacionTransporte"].ToString();
            conexiones.comando.Parameters.Add(parametroDelegacion);
            SqlParameter parametroDesde = new SqlParameter("@desde", SqlDbType.DateTime);
            if (ViewState["FiltroDesdeTransporte"].ToString() == "")
                parametroDesde.Value = null;
            else
                parametroDesde.Value = ViewState["FiltroDesdeTransporte"].ToString();
            conexiones.comando.Parameters.Add(parametroDesde);
            SqlParameter parametroDistancia = new SqlParameter("@distancia", SqlDbType.NVarChar, 10);
            if (ViewState["FiltroDistanciaTransporte"].ToString() == "")
                parametroDistancia.Value = null;
            else
                parametroDistancia.Value = ViewState["FiltroDistanciaTransporte"].ToString();
            conexiones.comando.Parameters.Add(parametroDistancia);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvTransporte.DataSource = dr;
            grvTransporte.DataBind();
            conexiones.conexion.Close();

            if (grvTransporte.Rows.Count > 0)
            {
                cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroEmpresaTransporte");
                this.rellenarFiltroEmpresaTransporte(cmbFiltro);

                //cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroDelegacionTransporte");
                //this.rellenarFiltroDelegacionTransporte(cmbFiltro);

                cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroDesdeTransporte");
                this.rellenarFiltroDesdeTransporte(cmbFiltro);

                cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroDistanciaTransporte");
                this.rellenarFiltroDistanciaTransporte(cmbFiltro);
            }
        }

        protected void grvTransporte_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvTransporte.EditIndex = e.NewEditIndex;
            rellenarGridTransporte();
        }

        #endregion

        #region "Ajustes"

        protected void CambioFiltroVersionAjustes(object sender, EventArgs e)
        {
            this.rellenarGridAjustes();
        }

        private void rellenarGridAjustes()
        {
            SqlParameter parametroVersion;
            SqlParameter parametroEmpresa;
            SqlParameter parametroDelegacion;
            SqlDataReader dr;

            // AJUSTES GASTOS FIJOS
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionAjustesGastosFijosDuracionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvAjustesGastosFijos.DataSource = dr;
            grvAjustesGastosFijos.DataBind();
            conexiones.conexion.Close();

            // Ajustes TAX
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionAjustesTaximetroDuracionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
             parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvAjustesTaximetro.DataSource = dr;
            grvAjustesTaximetro.DataBind();
            conexiones.conexion.Close();

            // Ajustes PRODUCTO OCUPACION
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionAjustesOcupacionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvAjustesOcupacion.DataSource = dr;
            grvAjustesOcupacion.DataBind();
            conexiones.conexion.Close();

            // Ajustes CLIENTE VOLUMNE
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionAjustesVolumenClienteConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvAjustesVolumen.DataSource = dr;
            grvAjustesVolumen.DataBind();
            conexiones.conexion.Close();

            // Ajustes PRESUPUESTO
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionAjustesBUConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvAjustesPresupuesto.DataSource = dr;
            grvAjustesPresupuesto.DataBind();
            conexiones.conexion.Close();

            // TOPES AJUSTES
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionAjustesTopesConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvAjustesTopes.DataSource = dr;
            grvAjustesTopes.DataBind();
            conexiones.conexion.Close();

            // FALLIDOS
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionFallidosConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (cmbVersioAjustes.SelectedValue == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = cmbVersioAjustes.SelectedValue.ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroEmpresa = new SqlParameter("@empresa", SqlDbType.NVarChar, 10);
            parametroEmpresa.Value = null;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            parametroDelegacion = new SqlParameter("@delegacion", SqlDbType.NVarChar, 10);
            parametroDelegacion.Value = null;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            dr = conexiones.comando.ExecuteReader();
            grvFallidos.DataSource = dr;
            grvFallidos.DataBind();
            conexiones.conexion.Close();
        }

        #endregion

        #region "BU"
        private void rellenarGridBU()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionBU";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersionBU"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersionBU"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvBU.DataSource = dr;
            grvBU.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvBU.HeaderRow.FindControl("FiltroVersionBU");
            this.rellenarFiltroVersionBU(cmbFiltro);
        }

        private void rellenarFiltroVersionBU(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionBUVersionConsulta";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CFG_Version";
            cmbFiltroVersion.DataValueField = "CFG_Version";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroVersionBU"].ToString()).Selected = true;
        }

        protected void CambioFiltroVersionBU(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersionBU"] = cmbFiltroVersion.SelectedValue;
            this.rellenarGridBU();
        }
        #endregion

        #region "Usuarios"

        protected void btnLimpiarUsuario_Click(object sender, EventArgs e)
        {
            txtUsuarioRed.Text = "";
            chkVisualizar.Checked = false;
            chkExportar.Checked = false;
            chkImportar.Checked = false;
            chkEliminar.Checked = false;
            chkElegirVersion.Checked = false;
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (txtUsuarioRed.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionUsuarioAgregar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroUsuario = new SqlParameter("@USR_UsuarioRed", SqlDbType.VarChar, 100);
                parametroUsuario.Value = txtUsuarioRed.Text;
                conexiones.comando.Parameters.Add(parametroUsuario);
                SqlParameter parametroVisualizar = new SqlParameter("@USR_Visualizar", SqlDbType.Bit);
                parametroVisualizar.Value = chkVisualizar.Checked;
                conexiones.comando.Parameters.Add(parametroVisualizar); 
                SqlParameter parametroExportar= new SqlParameter("@USR_Exportar", SqlDbType.Bit);
                parametroExportar.Value = chkExportar.Checked;
                conexiones.comando.Parameters.Add(parametroExportar);
                SqlParameter parametroImportar = new SqlParameter("@USR_Importar", SqlDbType.Bit);
                parametroImportar.Value = chkImportar.Checked;
                conexiones.comando.Parameters.Add(parametroImportar);
                SqlParameter parametroEliminar = new SqlParameter("@USR_Eliminar", SqlDbType.Bit);
                parametroEliminar.Value = chkEliminar.Checked;
                conexiones.comando.Parameters.Add(parametroEliminar);
                SqlParameter parametroElegir = new SqlParameter("@USR_ElegirVersion", SqlDbType.Bit);
                parametroElegir.Value = chkElegirVersion.Checked;
                conexiones.comando.Parameters.Add(parametroElegir);
                SqlParameter parametroAbono = new SqlParameter("@USR_ABONO", SqlDbType.Bit);
                parametroAbono.Value = chkAbonos.Checked;
                conexiones.comando.Parameters.Add(parametroAbono);
                SqlParameter parametroTecnico = new SqlParameter("@USR_Tecnico", SqlDbType.Bit);
                parametroTecnico.Value = chkTecnico.Checked;
                conexiones.comando.Parameters.Add(parametroTecnico);

                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Agregar usuario";
                lblMensajeError.Text = "Debe indicar el nombre de red del usuario.";
                mpeError.Show();
            }

            txtUsuarioRed.Text = "";
            chkVisualizar.Checked = false;
            chkExportar.Checked = false;
            chkImportar.Checked = false;
            chkEliminar.Checked = false;
            chkElegirVersion.Checked = false;
            rellenarGridUsuarios();
        }

        protected void txtImporteAbono_TextChanged(object sender, EventArgs e)
        {
            decimal numero = Convert.ToDecimal(txtImporteAbono.Text.Replace(".", ","));
            txtImporteAbono.Text = numero.ToString("N2");
        }

        protected void rellenarGridAbonos(int id, string ficha)
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_AbonosROPConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID= new SqlParameter("@id", SqlDbType.Int);
            if (id == -1)
                parametroID.Value = null;
            else
                parametroID.Value = id;
            conexiones.comando.Parameters.Add(parametroID);
            SqlParameter parametroFicha= new SqlParameter("@ficha", SqlDbType.NVarChar, 20);
            if (ficha == "")
                parametroFicha.Value = null;
            else
                parametroFicha.Value = ficha;
            conexiones.comando.Parameters.Add(parametroFicha);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvAbonos.DataSource = dr;
            grvAbonos.DataBind();
            conexiones.conexion.Close();
        }

        protected void grvAbonos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList cmbAfecta = (e.Row.FindControl("cmbAfecta") as DropDownList);

                cmbAfecta.Items.Insert(0, new ListItem("ROP"));
                cmbAfecta.Items.Insert(1, new ListItem("ROP Básico"));
                cmbAfecta.Items.Insert(2, new ListItem("MB"));
                string afecta = (e.Row.FindControl("lblAfecta") as Label).Text;
                cmbAfecta.Items.FindByValue(afecta).Selected = true;
                cmbAfecta.Enabled = e.Row.RowIndex == grvAbonos.EditIndex;
            }
        }

        protected void txtFicha_TextChanged(object sender, EventArgs e)
        {
            if (txtFicha.Text.Length >= 8)
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_FichaMoneda";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroFichaValidar = new SqlParameter("@ABO_Ficha", SqlDbType.VarChar, 20);
                parametroFichaValidar.Value = txtFicha.Text;
                conexiones.comando.Parameters.Add(parametroFichaValidar);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtMoneda.Text = dr["Moneda"].ToString();
                }
                dr.Close();
                conexiones.conexion.Close();
            }
        }

        protected void btnLimpiarAbono_Click(object sender, EventArgs e)
        {
            txtFicha.Text = "";
            txtMoneda.Text = "";
            txtImporteAbono.Text = "";
            txtObservacionesAbono.Text = "";
        }

        protected void btnAgregarAbono_Click(object sender, EventArgs e)
        {
            if (txtFicha.Text != "" || txtImporteAbono.Text.Length ==0)
            {
                // VALIDAR QUE NO EXISTE LA FICHA
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_AbonosROPValidar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroIDValidar = new SqlParameter("@ABO_ID", SqlDbType.Int);
                parametroIDValidar.Value = null;
                conexiones.comando.Parameters.Add(parametroIDValidar);
                SqlParameter parametroFichaValidar = new SqlParameter("@ABO_Ficha", SqlDbType.VarChar, 20);
                parametroFichaValidar.Value = txtFicha.Text;
                conexiones.comando.Parameters.Add(parametroFichaValidar);
                SqlParameter parametroAfectaValidar = new SqlParameter("@ABO_Afecta", SqlDbType.VarChar, 20);
                parametroAfectaValidar.Value = cmbAfectaAbono.Text;
                conexiones.comando.Parameters.Add(parametroAfectaValidar);
                SqlDataReader drValidar = conexiones.comando.ExecuteReader();
                if (drValidar.HasRows)
                {
                    lblTituloError.Text = "I N F O R M A C I Ó N"; //  
                    lblMensajeError.Text = "La ficha indicada ya posee un abono definido del tipo " + cmbAfectaAbono.Text + ".";
                    mpeError.Show();
                    return;
                }
                drValidar.Close();
                conexiones.conexion.Close();

                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_AbonosROPAgregar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroFicha = new SqlParameter("@ABO_Ficha", SqlDbType.VarChar, 20);
                parametroFicha.Value = txtFicha.Text;
                conexiones.comando.Parameters.Add(parametroFicha);
                SqlParameter parametroMoneda = new SqlParameter("@ABO_Moneda", SqlDbType.VarChar, 10);
                if (txtMoneda.Text =="")
                    parametroMoneda.Value = null;
                else
                    parametroMoneda.Value = txtMoneda.Text;
                conexiones.comando.Parameters.Add(parametroMoneda);
                SqlParameter parametroImporte = new SqlParameter("@ABO_Importe", SqlDbType.Decimal);
                parametroImporte.Value =Convert.ToDecimal(txtImporteAbono.Text);
                conexiones.comando.Parameters.Add(parametroImporte);
                SqlParameter parametroAfecta = new SqlParameter("@ABO_Afecta", SqlDbType.VarChar, 20);
                parametroAfecta.Value = cmbAfectaAbono.Text;
                conexiones.comando.Parameters.Add(parametroAfecta);
                SqlParameter parametroObservaciones= new SqlParameter("@ABO_Observaciones", SqlDbType.VarChar,1000);
                if (txtObservacionesAbono.Text.Length == 0)
                    parametroObservaciones.Value = null;
                else
                    parametroObservaciones.Value = txtObservacionesAbono.Text;
                conexiones.comando.Parameters.Add(parametroObservaciones);
                SqlParameter parametroUsuario = new SqlParameter("@ABO_Usuario", SqlDbType.VarChar, 100);
                parametroUsuario.Value = User.Identity.Name.ToString();
                conexiones.comando.Parameters.Add(parametroUsuario);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; //  
                lblMensajeError.Text = "Debe indicar la ficha y el importe del abono.";
                mpeError.Show();
            }

            txtFicha.Text = "";
            txtImporteAbono.Text = "";
            txtObservacionesAbono.Text = "";
            rellenarGridAbonos(-1,"");
        }

        protected void grvAbonos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvAbonos.EditIndex = e.NewEditIndex;
            rellenarGridAbonos(-1,"");
        }

        protected void grvAbonos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvAbonos.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string txtFicha = (grvAbonos.Rows[e.RowIndex].FindControl("lblFicha") as Label).Text;
            //string txtMoneda = (grvAbonos.Rows[e.RowIndex].FindControl("lblMoneda") as Label).Text;
            TextBox importeTexto = grvAbonos.Rows[e.RowIndex].FindControl("txtImporte") as TextBox;
            decimal importe = Convert.ToDecimal(importeTexto.Text);
            DropDownList cmbAfecta = grvAbonos.Rows[e.RowIndex].FindControl("cmbAfecta") as DropDownList;
            TextBox observaciones = grvAbonos.Rows[e.RowIndex].FindControl("txtObservaciones") as TextBox;

            // VALIDAR QUE NO EXISTE LA FICHA
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_AbonosROPValidar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroIDValidar = new SqlParameter("@ABO_ID", SqlDbType.Int);
            parametroIDValidar.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroIDValidar);
            SqlParameter parametroFichaValidar = new SqlParameter("@ABO_Ficha", SqlDbType.VarChar, 20);
            parametroFichaValidar.Value = txtFicha;
            conexiones.comando.Parameters.Add(parametroFichaValidar);
            SqlParameter parametroAfectaValidar = new SqlParameter("@ABO_Afecta", SqlDbType.VarChar, 20);
            parametroAfectaValidar.Value = cmbAfecta.Text;
            conexiones.comando.Parameters.Add(parametroAfectaValidar);
            SqlDataReader drValidar = conexiones.comando.ExecuteReader();
            if (drValidar.HasRows)
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; //  
                lblMensajeError.Text = "La ficha indicada ya posee un abono definido del tipo " + cmbAfecta.Text + ".";
                mpeError.Show();
                return;
            }
            drValidar.Close();
            conexiones.conexion.Close();

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_AbonosROPActualizar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@ABO_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
            SqlParameter parametroFicha = new SqlParameter("@ABO_Ficha", SqlDbType.VarChar, 20);
            parametroFicha.Value = txtFicha;
            conexiones.comando.Parameters.Add(parametroFicha);
            //SqlParameter parametroMoneda = new SqlParameter("@ABO_Moneda", SqlDbType.VarChar, 10);
            //if (txtMoneda == "")
            //    parametroMoneda.Value = null;
            //else
            //    parametroMoneda.Value = txtMoneda;
            //conexiones.comando.Parameters.Add(parametroMoneda);
            SqlParameter parametroImporte = new SqlParameter("@ABO_Importe", SqlDbType.Decimal);
            parametroImporte.Value = importe;
            conexiones.comando.Parameters.Add(parametroImporte);
            SqlParameter parametroAfecta= new SqlParameter("@ABO_Afecta", SqlDbType.VarChar, 20);
            parametroAfecta.Value = cmbAfecta.Text;
            conexiones.comando.Parameters.Add(parametroAfecta);
            SqlParameter parametroObservaciones= new SqlParameter("@ABO_Observaciones", SqlDbType.VarChar, 1000);
            parametroObservaciones.Value = observaciones.Text;
            conexiones.comando.Parameters.Add(parametroObservaciones);
            SqlParameter parametroUsuario = new SqlParameter("@ABO_Usuario", SqlDbType.VarChar, 100);
            parametroUsuario.Value = User.Identity.Name.ToString();
            conexiones.comando.Parameters.Add(parametroUsuario);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            grvAbonos.EditIndex = -1;
            rellenarGridAbonos(-1, "");
        }

        protected void grvAbonos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvAbonos.EditIndex = -1;
            rellenarGridAbonos(-1, "");
        }

        protected void btnOkDecidirBorrarAbono_Click(object sender, EventArgs e)
        {
            eliminarAbono();
        }

        protected void btnBuscarFicha_Click(object sender, EventArgs e)
        {
            rellenarGridAbonos(-1, txtBuscarFicha.Text);
        }
        
        protected void eliminarAbono()
        {

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_AbonosROPEliminar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@ABO_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(IDAbonoBorrar);
            conexiones.comando.Parameters.Add(parametroID);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            rellenarGridAbonos(-1, "");
            IDAbonoBorrar = "";
        }

        protected void grvAbonos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IDAbonoBorrar = grvAbonos.DataKeys[e.RowIndex].Values["ABO_ID"].ToString();
            string ficha = grvAbonos.DataKeys[e.RowIndex].Values["ABO_Ficha"].ToString();
            DropDownList cmbAfecta = grvAbonos.Rows[e.RowIndex].FindControl("cmbAfecta") as DropDownList;

            lblTituloDecidirBorrarAbono.Text = "I N F O R M A C I Ó N";
            lblDecidirBorrarAbono.Text = "Se eliminará el abono de la ficha: " + ficha + " // " + cmbAfecta.Text +".<br /><br />¿Desea continuar?";
            mpeDecidirBorrarAbono.Show();
        }

        private void rellenarGridUsuarios()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioDatosConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroConcepto = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
            parametroConcepto.Value = null;
            conexiones.comando.Parameters.Add(parametroConcepto);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvUsuarios.DataSource = dr;
            grvUsuarios.DataBind();
            conexiones.conexion.Close();
       }

        protected void grvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvUsuarios.EditIndex = e.NewEditIndex;
            rellenarGridUsuarios();
        }

        protected void grvUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_ID].Controls[0]).Text;
            string usuarioRed = ((System.Web.UI.WebControls.TextBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_UsuarioRed].Controls[0]).Text;
            System.Web.UI.WebControls.CheckBox visualizar = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Visualizar].Controls[0]);
            System.Web.UI.WebControls.CheckBox exportar = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Exportar].Controls[0]);
            System.Web.UI.WebControls.CheckBox importar = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Importar].Controls[0]);
            System.Web.UI.WebControls.CheckBox eliminar = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Eliminar].Controls[0]);
            System.Web.UI.WebControls.CheckBox elegir = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Elegir].Controls[0]);
            System.Web.UI.WebControls.CheckBox abono = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Abono].Controls[0]);
            System.Web.UI.WebControls.CheckBox tecnico = ((System.Web.UI.WebControls.CheckBox)grvUsuarios.Rows[e.RowIndex].Cells[COLGRID_USR_Tecnico].Controls[0]);

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioActualizar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@USR_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
            SqlParameter parametroUsuarioRed = new SqlParameter("@USR_UsuarioRed", SqlDbType.VarChar,100);
            parametroUsuarioRed.Value = usuarioRed;
            conexiones.comando.Parameters.Add(parametroUsuarioRed);

            SqlParameter parametroVisualizar = new SqlParameter("@USR_Visualizar", SqlDbType.Bit);
            parametroVisualizar.Value = visualizar.Checked;
            conexiones.comando.Parameters.Add(parametroVisualizar);
            SqlParameter parametroExportar = new SqlParameter("@USR_Exportar", SqlDbType.Bit);
            parametroExportar.Value = exportar.Checked;
            conexiones.comando.Parameters.Add(parametroExportar);
            SqlParameter parametroImportar = new SqlParameter("@USR_Importar", SqlDbType.Bit);
            parametroImportar.Value = importar.Checked;
            conexiones.comando.Parameters.Add(parametroImportar);
            SqlParameter parametroEliminar = new SqlParameter("@USR_Eliminar", SqlDbType.Bit);
            parametroEliminar.Value = eliminar.Checked;
            conexiones.comando.Parameters.Add(parametroEliminar);
            SqlParameter parametroElegir = new SqlParameter("@USR_ElegirVersion", SqlDbType.Bit);
            parametroElegir.Value = elegir.Checked;
            conexiones.comando.Parameters.Add(parametroElegir);
            SqlParameter parametroAbono = new SqlParameter("@USR_ABONO", SqlDbType.Bit);
            parametroAbono.Value = abono.Checked;
            conexiones.comando.Parameters.Add(parametroAbono);
            SqlParameter parametroTecnico = new SqlParameter("@USR_Tecnico", SqlDbType.Bit);
            parametroTecnico.Value = tecnico.Checked;
            conexiones.comando.Parameters.Add(parametroTecnico);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            grvUsuarios.EditIndex = -1;
            rellenarGridUsuarios();
        }

        protected void grvUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvUsuarios.EditIndex = -1;
            rellenarGridUsuarios();
        }

        protected void btnOkDecidirBorrarUsuario_Click(object sender, EventArgs e)
        {
            eliminarUsuario();
        }

        protected void eliminarUsuario()
        {
            
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioEliminar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@USR_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(IDUsuarioBorrar);
            conexiones.comando.Parameters.Add(parametroID);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            rellenarGridUsuarios();
            IDUsuarioBorrar = "";
        }

        protected void grvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IDUsuarioBorrar = grvUsuarios.DataKeys[e.RowIndex].Values["USR_ID"].ToString();
            string usuarioRed = grvUsuarios.DataKeys[e.RowIndex].Values["USR_UsuarioRed"].ToString(); 
            
            
            if (Environment.UserName.ToUpper() == usuarioRed.ToUpper())
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; //  "Eliminar usuario";
                lblMensajeError.Text = "No puede eliminar su propio usuario.";
                mpeError.Show();
                return;
            }

            lblTituloDecidirBorrarUsuario.Text = "I N F O R M A C I Ó N"; //"Versión";
            lblDecidirBorrarUsuario.Text = "Se eliminará el usuario: " + usuarioRed + ".<br /><br />¿Desea continuar?";
            mpeDecidirBorrarUsuario.Show();
        }

        #endregion

        #region "Paneles"

        protected void CambioFiltroVersionPaneles(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersionPaneles"] = cmbFiltroVersion.SelectedValue;
            this.rellenarGridPaneles();
        }

        private void rellenarFiltroVersionPaneles(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionPanelesVersionConsulta";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CFG_Version";
            cmbFiltroVersion.DataValueField = "CFG_Version";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();

            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroVersionPaneles"].ToString()).Selected = true;
        }

       
        private void rellenarGridPaneles()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "ROP_DatosPanelesConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersionPaneles"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersionPaneles"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvPaneles.DataSource = dr;
            grvPaneles.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvPaneles.HeaderRow.FindControl("FiltroVersionPaneles");
            this.rellenarFiltroVersionPaneles(cmbFiltro);
        }

        protected void grvPaneles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvPaneles.EditIndex = e.NewEditIndex;
            rellenarGridPaneles();
        }

        protected void grvPaneles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvPaneles.Rows[e.RowIndex].Cells[COLGRID_PANEL_ID_Indice].Controls[0]).Text;
            System.Web.UI.WebControls.CheckBox estandar = ((System.Web.UI.WebControls.CheckBox)grvPaneles.Rows[e.RowIndex].Cells[COLGRID_PANEL_Estandar].Controls[0]);

            if (ID != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_DatosPanelesActualizar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroID = new SqlParameter("@ItemIdAsset", SqlDbType.VarChar, 70);
                parametroID.Value = ID;
                conexiones.comando.Parameters.Add(parametroID);
                SqlParameter parametroEstandar = new SqlParameter("@Estandar", SqlDbType.Bit);
                parametroEstandar.Value = estandar.Checked;
                conexiones.comando.Parameters.Add(parametroEstandar);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();

                grvPaneles.EditIndex = -1;
                rellenarGridPaneles();
                //rellenarComboPaneles();
            }
        }

        protected void grvPaneles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ((BoundField)grvPaneles.Columns[COLGRID_PANEL_ID]).ReadOnly = true;
            //    ((BoundField)grvPaneles.Columns[COLGRID_PANEL_Descripcion]).ReadOnly = true;

            //    if (e.Row.RowIndex==0)
            //        e.Row.Cells[4].Controls.Clear();
            //}
        }

        protected void grvPaneles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvPaneles.EditIndex = -1;
            rellenarGridPaneles();
        }

        #endregion

        #region "Historico"
        private void rellenarGridHistorico()
        {
            conexiones.crearConexion();
            //if (rdbGFV.Checked == true)
            //    conexiones.consulta = "sp_ROP_ConfiguracionGeneralHistorico";
            //else
            conexiones.consulta = "sp_ROP_ConfiguracionHistorico";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvHistorico.DataSource = dr;
            grvHistorico.DataBind();
            conexiones.conexion.Close();
        }

        protected void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            rellenarGridHistorico();
        }
        #endregion

        #region "Fijo"

        protected void CambioFiltroVersionMovimientosGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersionMovimientosGeneral"] = cmbFiltroVersion.SelectedValue;
            this.rellenarAjustesFechaMovimientos();
        }

        private void rellenarFiltroAjustesFechaMovimientos(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_MovimientosAjusteFecha";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "MOV_TipoDescripcion";
            cmbFiltroVersion.DataValueField = "MOV_TipoDescripcion";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroMovimiento"].ToString()).Selected = true;
        }

        private void rellenarFiltroAjustesFechaVersionMovimientos(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_MovimientosAjusteFechaVersion";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CFG_Version";
            cmbFiltroVersion.DataValueField = "CFG_Version";
            cmbFiltroVersion.DataBind();
            conexiones.conexion.Close();
            cmbFiltroVersion.Items.FindByValue(ViewState["FiltroVersionMovimientosGeneral"].ToString()).Selected = true;
        }

        private void rellenarAjustesFechaMovimientos()
        {
            DropDownList cmbFiltroVersion;
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_MovimientosAjusteFechaConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 16);
            if (ViewState["FiltroVersionMovimientosGeneral"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersionMovimientosGeneral"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);
            SqlParameter parametroMovimiento = new SqlParameter("@movimiento", SqlDbType.NVarChar, 100);
            if (ViewState["FiltroMovimiento"].ToString() == "")
                parametroMovimiento.Value = null;
            else
                parametroMovimiento.Value = ViewState["FiltroMovimiento"].ToString();
            conexiones.comando.Parameters.Add(parametroMovimiento);

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvAjusteFechasMovimientos.DataSource = dr;
            grvAjusteFechasMovimientos.DataBind();
            conexiones.conexion.Close();

            cmbFiltroVersion = (DropDownList)grvAjusteFechasMovimientos.HeaderRow.FindControl("FiltroVersionMovimientosGeneral");
            this.rellenarFiltroAjustesFechaVersionMovimientos(cmbFiltroVersion);

            cmbFiltro = (DropDownList)grvAjusteFechasMovimientos.HeaderRow.FindControl("FiltroMovimiento");
            this.rellenarFiltroAjustesFechaMovimientos(cmbFiltro);
        }

        protected void CambioFiltroMovimiento(object sender, EventArgs e)
        {
            DropDownList cmbFiltroMovimiento = (DropDownList)sender;
            ViewState["FiltroMovimiento"] = cmbFiltroMovimiento.SelectedValue;
            this.rellenarAjustesFechaMovimientos();
        }

        protected void grvAjusteFechasMovimientos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvAjusteFechasMovimientos.EditIndex = e.NewEditIndex;
            rellenarAjustesFechaMovimientos();
        }

        protected void grvAjusteFechasMovimientos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvAjusteFechasMovimientos.Rows[e.RowIndex].Cells[COLGRID_MOV_ID].Controls[0]).Text;
            string signo = ((System.Web.UI.WebControls.TextBox)grvAjusteFechasMovimientos.Rows[e.RowIndex].Cells[COLGRID_MOV_Signo].Controls[0]).Text;
            string dias = ((System.Web.UI.WebControls.TextBox)grvAjusteFechasMovimientos.Rows[e.RowIndex].Cells[COLGRID_MOV_Dias].Controls[0]).Text;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_MovimientosAjusteFechaActualizar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@MOV_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
           
            SqlParameter parametroSigno= new SqlParameter("@MOV_Signo", SqlDbType.Char,1);
            parametroSigno.Value = signo;
            conexiones.comando.Parameters.Add(parametroSigno);
            SqlParameter parametroDias = new SqlParameter("@MOV_Dias", SqlDbType.Int);
            parametroDias.Value = Convert.ToInt32(dias);
            conexiones.comando.Parameters.Add(parametroDias);

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            grvAjusteFechasMovimientos.EditIndex = -1;
            rellenarAjustesFechaMovimientos();
        }

        protected void grvAjusteFechasMovimientos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvAjusteFechasMovimientos.EditIndex = -1;
            rellenarAjustesFechaMovimientos();
        }

        //private void rellenarDatosFijos()
        //{
        //    conexiones.crearConexion();
        //    conexiones.consulta = "sp_ROP_ConfiguracionFijaConsulta";
        //    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;
        //    SqlDataReader dr = conexiones.comando.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        txtDiasCalculo.Text= dr["COF_DiasCalculo"].ToString();
        //        txtDiasFechaOfertaCapitulo.Text = dr["COF_OfertaDiasEntreFechaOfertaFechaCapitulo"].ToString();
        //        txtDiasRetrocederOferta.Text = dr["COF_OfertaDiasRestarFechaCapítulo"].ToString();
        //        txtDiasFechaOfertaPedido.Text = dr["COF_OfertaDiasEntreFechaOfertaFechaPedido"].ToString();
        //        txtDiasRetrocederPedido.Text = dr["COF_OfertaDiasRestarFechaPedido"].ToString();
        //    }
        //    conexiones.conexion.Close();

        //    txtDiasCalculo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        //    txtDiasFechaOfertaCapitulo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        //    txtDiasRetrocederOferta.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        //    txtDiasFechaOfertaPedido.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        //    txtDiasRetrocederPedido.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

        //    txtDiasCalculo.Enabled = false;
        //    txtDiasFechaOfertaCapitulo.Enabled = false;
        //    txtDiasRetrocederOferta.Enabled = false;
        //    txtDiasFechaOfertaPedido.Enabled = false;
        //    txtDiasRetrocederPedido.Enabled = false;
        //    btnEditarFijo.Visible = true;
        //    btnGuardarFijo.Visible = false;
        //    btnCancelarFijo.Visible = false;
        //}

        //protected void btnEditarFijo_Click(object sender, EventArgs e)
        //{
        //    txtDiasCalculo.Enabled = true;
        //    txtDiasFechaOfertaCapitulo.Enabled  = true;
        //    txtDiasRetrocederOferta.Enabled = true;
        //    txtDiasFechaOfertaPedido.Enabled = true;
        //    txtDiasRetrocederPedido.Enabled = true;
        //    btnEditarFijo.Visible = false;
        //    btnGuardarFijo.Visible = true;
        //    btnCancelarFijo.Visible = true;
        //}

        //protected void btnCancelarFijo_Click(object sender, EventArgs e)
        //{
        //    txtDiasCalculo.Enabled = false;
        //    txtDiasFechaOfertaCapitulo.Enabled = false;
        //    txtDiasRetrocederOferta.Enabled = false;
        //    txtDiasFechaOfertaPedido.Enabled = false;
        //    txtDiasRetrocederPedido.Enabled = false;
        //    btnEditarFijo.Visible = true;
        //    btnGuardarFijo.Visible = false;
        //    btnCancelarFijo.Visible = false;
        //}

        //protected void btnGuardarFijo_Click(object sender, EventArgs e)
        //{
        //    conexiones.crearConexion();
        //    conexiones.consulta = "sp_ROP_ConfiguracionFijaActualizar";
        //    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;
        //    conexiones.comando.Parameters.AddWithValue("@COF_DiasCalculo", Convert.ToInt32(txtDiasCalculo.Text));
        //    conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasEntreFechaOfertaFechaCapitulo", Convert.ToInt32(txtDiasFechaOfertaCapitulo.Text));
        //    conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasRestarFechaCapítulo", Convert.ToInt32(txtDiasRetrocederOferta.Text));
        //    conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasEntreFechaOfertaFechaPedido", Convert.ToInt32(txtDiasFechaOfertaPedido.Text));
        //    conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasRestarFechaPedido", Convert.ToInt32(txtDiasRetrocederPedido.Text));
        //    conexiones.comando.ExecuteNonQuery();
        //    conexiones.conexion.Close();

        //    txtDiasCalculo.Enabled = false;
        //    txtDiasFechaOfertaCapitulo.Enabled = false;
        //    txtDiasRetrocederOferta.Enabled = false;
        //    txtDiasFechaOfertaPedido.Enabled = false;
        //    txtDiasRetrocederPedido.Enabled = false;
        //    btnEditarFijo.Visible = true;
        //    btnGuardarFijo.Visible = false;
        //    btnCancelarFijo.Visible = false;
        //}
        #endregion

        public bool validarUsuarioTecnico
        {
            get
            {
                bool devolver;

                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionUsuarioConsulta";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
                parametroUsuario.Value = Session["usuarioEnviado"].ToString(); // Environment.UserName;
                conexiones.comando.Parameters.Add(parametroUsuario);
                conexiones.comando.Parameters.AddWithValue("@incluir", 0);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToBoolean(dr["USR_Tecnico"]))
                        devolver = true;
                    else
                        devolver = false;
                }
                else
                    devolver = false;
                dr.Close();
                conexiones.conexion.Close();
                return devolver;
            }
        }

        protected void btnHilos_Click(object sender, EventArgs e)
        {
            if (txtHilos.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionHilosModificacion";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroHilos = new SqlParameter("@hilos", SqlDbType.Int);
                parametroHilos.Value = Convert.ToInt32(txtHilos.Text);
                conexiones.comando.Parameters.Add(parametroHilos);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; 
                lblMensajeError.Text = "Debe indicar el número de pedidos a procesar de forma paralela.";
                mpeError.Show();
            }
        }

        protected void btnVersion_Click(object sender, EventArgs e)
        {
            if (txtVersion.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionVersionModificacion";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.VarChar, 25);
                parametroVersion.Value = txtVersion.Text;
                conexiones.comando.Parameters.Add(parametroVersion);
                SqlParameter parametroCambios = new SqlParameter("@cambios", SqlDbType.VarChar, 1000);
                parametroCambios.Value = txtCambios.Text;
                conexiones.comando.Parameters.Add(parametroCambios);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N";
                lblMensajeError.Text = "Debe indicar la versión del sofware y los cambios realizados.";
                mpeError.Show();
            }
        }

        protected void btnBarraProgreso_Click(object sender, EventArgs e)
        {
            if (txtBarraProgreso.Text != "" || txtBarraProgresoOfertas.Text != "" || txtBarraProgresoPedidos.Text != "" || txtBarraProgresoFichas.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionBarraProgresoModificacion";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroBarra = new SqlParameter("@barra", SqlDbType.Decimal);
                parametroBarra.Value = Convert.ToDecimal(txtBarraProgreso.Text);
                conexiones.comando.Parameters.Add(parametroBarra);
                parametroBarra = new SqlParameter("@barraProgresoOfertas", SqlDbType.Decimal);
                parametroBarra.Value = Convert.ToDecimal(txtBarraProgresoOfertas.Text);
                conexiones.comando.Parameters.Add(parametroBarra);
                parametroBarra = new SqlParameter("@barraProgresoPedidosFacturas", SqlDbType.Decimal);
                parametroBarra.Value = Convert.ToDecimal(txtBarraProgresoPedidos.Text);
                conexiones.comando.Parameters.Add(parametroBarra);
                parametroBarra = new SqlParameter("@barraProgresoFichas", SqlDbType.Decimal);
                parametroBarra.Value = Convert.ToDecimal(txtBarraProgresoFichas.Text);
                conexiones.comando.Parameters.Add(parametroBarra);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N";
                lblMensajeError.Text = "Debe indicar el factor para el cálculo de la barra de progreso, y los valores para cada tipo de cálculo.";
                mpeError.Show();
            }
        }
        

        private void rellenarHilos()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTecnicoConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                txtHilos.Text = dr["Hilos"].ToString();
                txtBarraProgreso.Text = dr["BarraProgreso"].ToString();
                txtBarraProgresoOfertas.Text = dr["BarraProgresoOfertas"].ToString();
                txtBarraProgresoPedidos.Text = dr["BarraProgresoPedidosFacturas"].ToString();
                txtBarraProgresoFichas.Text = dr["BarraProgresoFichas"].ToString();
                txtUnidadesPorte.Text = dr["MinimoRegistrosTransporte"].ToString();
                txtEmailTransporte.Text = dr["EmailTransporte"].ToString();
                chkPararEjecucion.Checked = Convert.ToBoolean(dr["PararEjecucionSiTablasMal"]);
            }
            else
            {
                txtHilos.Text = "0";
                txtBarraProgreso.Text = "0.00";
                txtBarraProgresoOfertas.Text = "0.00";
                txtBarraProgresoPedidos.Text = "0.00";
                txtBarraProgresoFichas.Text = "0.00";
                txtUnidadesPorte.Text = "0";
                txtEmailTransporte.Text = "";
                chkPararEjecucion.Checked = false;
            }
            dr.Close();
            conexiones.conexion.Close();
            rellenarGridVersiones();
        }

        public string letraExcel(int columna)
        {

            int intPrimeraLetra = ((columna) / 676) + 64;
            int intSegundaLetra = ((columna % 676) / 26) + 64;
            int intTerceraLetra = (columna % 26) + 65;

            char primeraLetra = (intPrimeraLetra > 64) ? (char)intPrimeraLetra : ' ';
            char segundaLetra = (intSegundaLetra > 64) ? (char)intSegundaLetra : ' ';
            char terceraLetras = (char)intTerceraLetra;

            return string.Concat(primeraLetra, segundaLetra, terceraLetras).Trim();
        }

        private void rellenarGridVersiones()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionVersionSoftwareConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvVersiones.DataSource = dr;
            grvVersiones.DataBind();
           
            conexiones.conexion.Close();
        }

        private void rellenarGridDatosTransporte()
        {
            conexiones.crearConexion();
            conexiones.consulta = "ROP_TransporteConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvDatosTransporte.DataSource = dr;
            grvDatosTransporte.DataBind();

            conexiones.conexion.Close();
        }

        protected void btnExportarTransporte_Click(object sender, EventArgs e)
        {
            nombreInforme = Server.MapPath("~/Ficheros excel/DatosTransporte_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
            exportarExcelDatosTransporte();

            lblTituloInformacion.Text = "I N F O R M A C I Ó N"; 
            lblMensajeInformacion.Text = "Fichero excel creado.";
            mpeInformacion.Show();
            btnAbrirExcelTransporte.Visible = true;
        }

        protected void btnAbrirExcelTransporte_Click(object sender, EventArgs e)
        {
            try
            {
                Context.Response.Redirect("Ficheros excel/DatosTransporte_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                btnAbrirExcelTransporte.Visible = false;
            }
            catch (Exception ex)
            {
                lblTituloInformacion.Text = "I N F O R M A C I Ó N"; // "Fichero configuración";
                lblMensajeInformacion.Text = "Error al descargar el fichero: Ficheros excel/DatosTransporte_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx" + " // " + ex.Message;
                mpeInformacion.Show();
            }
        }

        private void exportarExcelDatosTransporte()
        {
            SLStyle style;
            SLStyle styleInt;
            SLStyle styleDec;
            SLStyle styleDate;
            decimal valor;
            int valorEntero;
            DateTime valorFecha;
            SLPageSettings ps;
            int fila = 2;
            int columna = 0;

             System.Data.DataTable dtConceptos = new System.Data.DataTable();
            // Color general de las pestañas de las hojas ocultas
            ps = new SLPageSettings();
            ps.TabColor = System.Drawing.Color.Red;

            btnAbrirExcelTransporte.Visible = false;
            // Crear el excel
            SLDocument sl = new SLDocument();
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "DATOS TRANSPORTE");

            // DATOS GENERALES DE LA VERSIÓN
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "ROP_TransporteConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            sl.SelectWorksheet("DATOS TRANSPORTE");
           
            // estilos
            styleInt = sl.CreateStyle();
            styleInt.SetFont("Verdana", 10);
            styleInt.Alignment.Horizontal = HorizontalAlignmentValues.Right;
            styleInt.FormatCode = "#,##0";

            styleDec = sl.CreateStyle();
            styleDec.SetFont("Verdana", 10);
            styleDec.Alignment.Horizontal = HorizontalAlignmentValues.Right;
            styleDec.FormatCode = "#,##0.00";

            styleDate = sl.CreateStyle();
            styleDate.SetFont("Verdana", 10);
            styleDate.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            styleDate.FormatCode = "dd/MM/yyyy";

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "EMPRESA");
            sl.SetCellValue(1, 2, "UPS");
            sl.SetCellValue(1, 3, "TONELADAS ALBARÁN");
            sl.SetCellValue(1, 4, "KM");
            sl.SetCellValue(1, 5, "DISTANCIA");
            sl.SetCellValue(1, 6, "FECHA");
            //sl.SetCellValue(1, 7, "MONEDA");
            sl.SetCellValue(1, 8, "MONEDA EMPRESA");
            sl.SetCellValue(1, 9, "CAMBIO");
            sl.SetCellValue(1, 10, "MESES PROCESADOS");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 25);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 25);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 10; columna++)
                sl.SetCellStyle(1, columna, style);

            fila = 2;
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                decimal.TryParse(Row.ItemArray[1].ToString(), out valor);
                sl.SetCellValue(fila, 2, valor);
                sl.SetCellStyle(fila, 2, styleDec);
                decimal.TryParse(Row.ItemArray[2].ToString(), out valor);
                sl.SetCellValue(fila, 3, valor);
                sl.SetCellStyle(fila, 3, styleDec);
                decimal.TryParse(Row.ItemArray[3].ToString(), out valor);
                sl.SetCellValue(fila, 4, valor);
                sl.SetCellStyle(fila, 4, styleDec);
                sl.SetCellValue(fila, 5, Row.ItemArray[4].ToString());
                DateTime.TryParse(Row.ItemArray[5].ToString(), out valorFecha);
                sl.SetCellValue(fila, 6, valorFecha);
                sl.SetCellStyle(fila, 6, styleDate);
                sl.SetCellValue(fila, 7, Row.ItemArray[6].ToString());
                sl.SetCellValue(fila, 8, Row.ItemArray[7].ToString());
                decimal.TryParse(Row.ItemArray[8].ToString(), out valor);
                sl.SetCellValue(fila, 9, valor);
                int.TryParse(Row.ItemArray[9].ToString(), out valorEntero);
                sl.SetCellValue(fila, 10, valorEntero);
                sl.SetCellStyle(fila, 10, styleInt);
                fila++;
            }
            sl.AutoFitColumn(1, 10);

            sl.SaveAs(nombreInforme);
            btnAbrirExcelTransporte.Visible = true;
        }

        protected void btnUnidadesPorte_Click(object sender, EventArgs e)
        {
            if (txtUnidadesPorte.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionUnidadesPorteModificacion";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroUnidadesPorte = new SqlParameter("@unidadesPorte", SqlDbType.Int);
                parametroUnidadesPorte.Value = Convert.ToInt32(txtUnidadesPorte.Text);
                conexiones.comando.Parameters.Add(parametroUnidadesPorte);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N";
                lblMensajeError.Text = "Debe indicar el número de unidades porte.";
                mpeError.Show();
            }
        }

        
        protected void btnEmailTransporte_Click(object sender, EventArgs e)
        {
            if (txtEmailTransporte.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionEmailTransporteModificacion";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroEmail= new SqlParameter("@emailTransporte", SqlDbType.NVarChar);
                parametroEmail.Value = txtEmailTransporte.Text;
                conexiones.comando.Parameters.Add(parametroEmail);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N";
                lblMensajeError.Text = "Debe indicar el correo.";
                mpeError.Show();
            }
        }

        protected void chkPararEjecucion_CheckedChanged(object sender, EventArgs e)
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionParaEjecucioneModificacion";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroParar = new SqlParameter("@pararEjecucion", SqlDbType.Bit);
            parametroParar.Value = chkPararEjecucion.Checked;
            conexiones.comando.Parameters.Add(parametroParar);
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();
        }

        protected void btnExportarEtiquetas_Click(object sender, EventArgs e)
        {
            SLStyle style;
            SLStyle styleInt;
            int valorEntero;
            int fila = 0;
            int columna = 0;

            nombreInforme = Server.MapPath("~/Ficheros excel/Etiquetas_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
            btnAbrirExcelEtiquetas.Visible = false;

            // Crear el excel
            SLDocument sl = new SLDocument();
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "ETIQUETAS");

            // DATOS ETIQUETAS
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_Etiquetas_Consulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            sl.SelectWorksheet("ETIQUETAS");

            // estilos
            styleInt = sl.CreateStyle();
            styleInt.SetFont("Verdana", 10);
            styleInt.Alignment.Horizontal = HorizontalAlignmentValues.Right;
            styleInt.FormatCode = "#,##0";

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "ID");
            sl.SetCellValue(1, 2, "CONTROL");
            sl.SetCellValue(1, 3, "ESPAÑOL");
            sl.SetCellValue(1, 4, "CATALÁN");
            sl.SetCellValue(1, 5, "INGLÉS");
    
            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 10);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 50);
            sl.SetColumnWidth(4, 50);
            sl.SetColumnWidth(5, 50);

            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 5; columna++)
                sl.SetCellStyle(1, columna, style);

            fila= 2;
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[0].ToString());
                int.TryParse(Row.ItemArray[0].ToString(), out valorEntero);
                sl.SetCellValue(fila, 1, valorEntero);
                sl.SetCellStyle(fila, 1, styleInt);
   
                sl.SetCellValue(fila, 2, Row.ItemArray[1].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[2].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[3].ToString());
                sl.SetCellValue(fila, 5, Row.ItemArray[4].ToString());

                fila++;
            }
            sl.HideColumn(1, 1);
            sl.AutoFitColumn(1, 5);

            sl.SaveAs(nombreInforme);

            lblTituloInformacion.Text = "I N F O R M A C I Ó N";
            lblMensajeInformacion.Text = "Fichero excel creado.";
            mpeInformacion.Show();
            btnAbrirExcelEtiquetas.Visible = true;
        }

        protected void btnAbrirExcelEtiquetas_Click(object sender, EventArgs e)
        {
            try
            {
                Context.Response.Redirect("Ficheros excel/Etiquetas_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                btnAbrirExcelEtiquetas.Visible = false;
            }
            catch (Exception ex)
            {
                lblTituloInformacion.Text = "I N F O R M A C I Ó N"; // "Fichero configuración";
                lblMensajeInformacion.Text = "Error al descargar el fichero: Ficheros excel/Etiquetas_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx" + " // " + ex.Message;
                mpeInformacion.Show();
            }
        }

        protected void btnSubirExcelEtiquetas_Click(object sender, EventArgs e)
        {
            string directorio;
            String fichero;

            directorio = Server.MapPath("~/Ficheros excel/");

            if (ficheroSeleccionadoEtiquetas.HasFile)
            {
                fichero = ficheroSeleccionadoEtiquetas.FileName;
                directorio += fichero;
                ficheroSeleccionadoEtiquetas.SaveAs(directorio);
                procesarExcelEtiquetas(directorio);
            }
            else
            {
                lblTituloError.Text = "I N F O R M A C I Ó N"; // 
                lblMensajeError.Text = "Debe indicar el fichero de etiquetas a importar.";
                mpeError.Show();
            }
        }

        private void procesarExcelEtiquetas(string fichero)
        {
            SLDocument sl;
            SqlConnection conexion;
            SqlCommand comando;
            System.Data.DataTable dtTablaErrores = new System.Data.DataTable();
            string strConexion;
            DataTable dtEtiquetas;
            int row;
           string dondeVa = "";

            try
            {
                dondeVa = "Error procesando pestaña VERSIÓN";
                sl = new SLDocument(fichero, "ETIQUETAS");

                dondeVa = "Error procesando pestaña ETIQUETAS";
                dtEtiquetas = new DataTable();
                dtEtiquetas.Columns.Add(new DataColumn("ID", typeof(int)));
                dtEtiquetas.Columns.Add(new DataColumn("CONTROL", typeof(string)));
                dtEtiquetas.Columns.Add(new DataColumn("ESPAÑOL", typeof(string)));
                dtEtiquetas.Columns.Add(new DataColumn("CATALÁN", typeof(string)));
                dtEtiquetas.Columns.Add(new DataColumn("INGLÉS", typeof(string)));

                using (sl = new SLDocument(fichero, "ETIQUETAS"))
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;
                    for (row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                    {
                        dtEtiquetas.Rows.Add(
                            sl.GetCellValueAsInt32(row, iStartColumnIndex),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 4)
                         );
                    }
                }

                // PROCESAR DATOS EN EL SQL
                dondeVa = "Error procesando";
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_EtiquetasProcesar";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter //Esta declaracion es Clave en este Metodo
                {
                    SqlDbType = SqlDbType.Structured, //El tipo Structured es el que se tomara la estructura del Data Table para
                    ParameterName = "etiquetas", //Hacer match con el Tipo Table en SQL, respetando el nombre declarado en el StoreProcedure
                    Value = dtEtiquetas
                };
                comando.Parameters.Add(parameter);
                comando.ExecuteNonQuery();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();

                lblTituloInformacion.Text = "I N F O R M A C I Ó N"; 
                lblMensajeInformacion.Text = "Cargados los cambios en las etiquetas";
                mpeInformacion.Show();
             
                btnAbrirExcelEtiquetas.Visible = false;
            }
            catch (Exception ex)
            {
                lblTituloInformacion.Text = "Fichero procesar";
                lblMensajeInformacion.Text = "Error al procesar el fichero: " + fichero + "\r\n" + dondeVa + " \r\n " + ex.Message;
                mpeInformacion.Show();
            }
        }

    }
}