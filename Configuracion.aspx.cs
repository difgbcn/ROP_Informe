using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

namespace ROP_Informe
{

    public partial class Configuracion : System.Web.UI.Page
    {
        string nombreInforme = "";

        // GRIDS
        int COLGRID_USR_ID = 0;
        int COLGRID_USR_UsuarioRed = 1;
        int COLGRID_USR_Visualizar = 2;
        int COLGRID_USR_Exportar = 3;
        int COLGRID_USR_Importar = 4;
        int COLGRID_USR_Eliminar;
        int COLGRID_USR_Elegir = 6;
        int COLGRID_USR_btnEditar = 7;
        int COLGRID_USR_btnEliminar = 8;

        int COLGRID_ID = 0;
        int COLGRID_Familia = 1;
        int COLGRID_Subfamilia = 2;
        int COLGRID_ART_ID = 3;
        //int COLGRID_CFGSERV_Tipo = 4;
        int COLGRID_SER_btnEditar = 5;
        int COLGRID_SER_btnEliminar = 6;

        // Transporte
        public static int transporsteID = -1;
        int CAMPO_IDTransporte = 0;
        int CAMPO_Meses = 1;
        int CAMPO_Desvio = 2;
        int CAMPO_Fecha = 3;

        int COLGRID_CFGTRA_ID = 0;
        //int COLGRID_BU = 1;
        //int COLGRID_Empresa = 2;
        //int COLGRID_Delegacion = 3;
        int COLGRID_Margen = 4;
        //int COLGRID_Desde = 5;
        //int COLGRID_Hasta = 6;
        //int COLGRID_Distancia = 7;
        int COLGRID_Prop = 8;
        int COLGRID_Valor = 9;
        int COLGRID_Desvio = 10;
        int COLGRID_TRA_btnEditar = 11;
        int COLGRID_TRA_btnEliminar = 12;

        int COLGRID_TRANS_ID = 0;
        int COLGRID_TRANS_Subfamilia = 4;
        int COLGRID_TRANS_btnEditar = 5;
        int COLGRID_TRANS_btnEliminar=6;

        int COLGRID_MOV_ID = 0;
        int COLGRID_MOV_Signo = 3;
        int COLGRID_MOV_Dias = 4;

        int COLGRID_PANEL_ID_Indice = 0;
        int COLGRID_PANEL_ID = 1;
        int COLGRID_PANEL_Descripcion = 2;
        int COLGRID_PANEL_Estandar = 3;

        // EXCELS
        int COL_Version = 0;
        int COL_Desde = 1;
        int COL_Hasta = 2;
        
        int COL_Concepto = 0;
        int COL_Empresa = 1;
        int COL_Familia = 2;
        int COL_Subfamilia = 3;
        int COL_Articulo = 4;
        int COL_ValorDesde = 5;
        int COL_ValorHasta = 6;
        int COL_Valor = 7;
        int COL_Moneda = 8;

        int COL_Concepto_Nivel2 = 0;
        int COL_Empresa_Nivel2 = 1;
        int COL_Valor_Nivel2 = 2;

        int COL_Concepto_Nivel3 = 0;
        int COL_Signo_Nivel3 = 1;
        int COL_Dias_Nivel3 = 2;

        int COL_Concepto_Nivel4 = 0;
        int COL_Valor_Nivel4 = 1;

        int COL_FamiliaServicios = 0;
        int COL_SubfamiliaServicios = 1;
        int COL_ArticuloServicios = 2;
        int COL_TipoServicio = 3;

        int COL_AAFPaneles = 0;
        int COL_EstandarPaneles = 1;

        //int COL_Version_ID = 0;
        //int COL_Version_Version = 1;
        //int COL_Version_Prueba = 2;
        //int COL_Version_Desde = 3;
        //int COL_Version_Hasta = 4;
        //int COL_Version_Grupo = 5;
        //int COL_Version_Subgrupo = 6;
        //int COL_Version_Concepto = 7;
        //int COL_Version_Empresa = 8;
        //int COL_Version_Familia = 9;
        //int COL_Version_Subfamilia = 10;
        //int COL_Version_Articulo = 11;
        //int COL_Version_Valor = 12;

        //int COL_GeneralID = 0;
        //int COL_GeneralPrueba = 1;
        //int COL_GeneralVersion = 2;
        //int COL_GeneralDesde = 3;
        //int COL_GeneralHasta = 4;
        //int COL_GeneralConcepto = 5;
        //int COL_GeneralEmpresa = 6;
        //int COL_GeneralValor = 7;

        //int COL_FicheroGeneralConcepto = 1;
        //int COL_FicheroGeneralEmpresa = 2;
        //int COL_FicheroGeneralValor = 3;

        //int FILA_GENERAL_TITULO = 1;
        //int FILA_GENERAL_IDVERSION = 3;
        //int FILA_GENERAL_VERSION = 4;
        //int FILA_GENERAL_FECHA_DESDE = 5;
        //int FILA_GENERAL_FECHA_HASTA = 6;
        //int FILA_GENERAL_DATOS = 9;

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
                chkBoxActivos.Checked = true;
                txtPropuesto.Enabled = false;
                txtUsuarioRed.Text = "";
                chkVisualizar.Checked = true;
                chkExportar.Checked = false;
                chkImportar.Checked = false;
                chkEliminar.Checked = false;
                chkElegirVersion.Checked = false;
                ViewState["FiltroVersion"] = "";
                ViewState["FiltroConcepto"] = "";
                ViewState["FiltroConceptoValor"] = "";
                ViewState["FiltroVersionGeneral"] = "";
                ViewState["FiltroConceptoGeneral"] = "";
                ViewState["FiltroBUGeneral"] = "";
                ViewState["FiltroEmpresaGeneral"] = "";
                ViewState["FiltroVersionMovimientosGeneral"]="";
                ViewState["FiltroMovimiento"] = "";
                ViewState["FiltroEmpresaTransporte"] = "";
                ViewState["FiltroDelegacionTransporte"] = "";
                ViewState["FiltroDesdeTransporte"] = "";
                ViewState["FiltroDistanciaTransporte"] = "";
                ViewState["FiltroVersionBU"] = "";
                rdbOperativo.Checked = true;
                rellenarGridGeneral();
                rellenarGrid();
                rellenarCombosVersion();
                rellenarGridServicios();
                rellenarServicio();
                rellenarBU();
                rellenarEmpresa();
                rellenarDelegacion();
                rellenarGridTransporte();
                rellenarGridTransporteCodigos();
                rellenarGridBU();
                rellenarTransporte();
                rellenarTransporteGeneral();
                //datosPaneles();
                rellenarGridPaneles();
                rellenarGridUsuarios();
                validarAccionUsuario();
                rellenarAjustesFechaMovimientos();
                //rdbGFV.Checked = true;
                rellenarGridHistorico();
                btnAbrirExcel.Visible = false;
                txtDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void validarAccionUsuario()
        {
            ImageButton imgBoton;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
            parametroUsuario.Value = Environment.UserName;
            conexiones.comando.Parameters.Add(parametroUsuario);
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
                rdbOperativo.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                rdbPrueba.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                ficheroSeleccionado.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                btnImportarExcel.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                cmbVersionEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                btnExcelEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                cmbVersionPruebas.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                btnVersionReal.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);

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

                btnLimpiarServicio.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                btnAgregarServicio.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);

                foreach (GridViewRow myRow in grvServicios.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_SER_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible =dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_SER_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                }

                btnLimpiarTransporte.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                btnAgregarTransporte.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);

                foreach (GridViewRow myRow in grvTransporte.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRA_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRA_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                }

                btnLimpiarTransporteSubfamilias.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                btnAgregarTransporteSubfamilias.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);

                foreach (GridViewRow myRow in grvTransporteSubfamilias.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRANS_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRANS_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                }

                //btnEditarFijo.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                //btnGuardarFijo.Visible = false;
                //btnCancelarFijo.Visible = false;

                foreach (GridViewRow myRow in grvUsuarios.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_USR_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_USR_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                }
                conexiones.conexion.Close();
            }
            else
            {
                //cmbVersionGeneralExportar.Visible = false;
                //btnExcelGeneral.Visible = false;
                //btnAbrirExcelGeneral.Visible = false;
                //rdbOperativoGeneral.Visible = false;
                //rdbPruebaGeneral.Visible = false;
                //ficheroSeleccionadoGeneral.Visible = false;
                //btnImportarExcelGeneral.Visible = false;
                //cmbVersionGeneralEliminar.Visible = false;
                //btnExcelGeneralEliminar.Visible = false;
                //cmbVersionGeneralPruebas.Visible = false;
                //btnVersionGeneralReal.Visible = false;

                cmbVersionExportar.Visible = false;
                btnExcel.Visible = false;
                btnAbrirExcel.Visible = false;
                rdbOperativo.Visible = false;
                rdbPrueba.Visible = false;
                ficheroSeleccionado.Visible = false;
                btnImportarExcel.Visible = false;
                cmbVersionEliminar.Visible = false;
                btnExcelEliminar.Visible = false;
                cmbVersionPruebas.Visible = false;
                btnVersionReal.Visible = false;

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

                btnLimpiarServicio.Visible = false;
                btnAgregarServicio.Visible = false;

                foreach (GridViewRow myRow in grvServicios.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_SER_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_SER_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                }

                btnLimpiarTransporte.Visible = false;
                btnAgregarTransporte.Visible = false;

                foreach (GridViewRow myRow in grvTransporte.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRA_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRA_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                }

                btnLimpiarTransporteSubfamilias.Visible = false;
                btnAgregarTransporteSubfamilias.Visible = false;
                foreach (GridViewRow myRow in grvTransporteSubfamilias.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRANS_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_TRANS_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                }

                //btnEditarFijo.Visible = false;
                //btnGuardarFijo.Visible = false;
                //btnCancelarFijo.Visible = false;

                foreach (GridViewRow myRow in grvUsuarios.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_USR_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_USR_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = false;
                    }
                }
            }
        }

        #region ConfiguracionGeneral
        protected void btnOkPruebaReal_Click(object sender, EventArgs e)
        {
            string observaciones = txtObservaciones.Text;
            lblTituloInformacion.Text = "Observaciones";
            lblMensajeInformacion.Text = observaciones;
            mpeInformacion.Show();
        }

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

        protected void CambioFiltroBUGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroBU = (DropDownList)sender;
            ViewState["FiltroBUGeneral"] = cmbFiltroBU.SelectedValue;
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

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 10);
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

            cmbFiltro = (DropDownList)grvDatosGenerales.HeaderRow.FindControl("FiltroBUGeneral");
            this.rellenarFiltroBUGeneral(cmbFiltro);
        }

        //private void procesarExcelGeneral(string fichero)
        //{
        //    int fila;
        //    string version;
        //    DateTime fechaDesde;
        //    DateTime fechaHasta;

        //    SqlParameter parametroVersion;
        //    SqlParameter parametroPrueba;
        //    SqlParameter parametroObservaciones;
        //    SqlParameter parametroFechaDesde;
        //    SqlParameter parametroFechaHasta;
        //    SqlParameter parametroConcepto;
        //    SqlParameter parametroEmpresa;
        //    SqlParameter parametroValor;

        //    SLDocument sl = new SLDocument(fichero, "Configuracion general");

        //    if (sl.GetCellValueAsString(FILA_GENERAL_TITULO, 1).ToString().ToUpper() != "CONFIGURACIÓN GENERAL")
        //    {
        //        //MessageBox.Show("El fichero no parece tener el formato correcto." + Environment.NewLine + "Por favor, verifique el fichero e intente procesarlo de nuevo.", "Configuración parámetros", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('El fichero no parece tener el formato correcto. Por favor, verifique el fichero e intente procesarlo de nuevo.');", true);
        //        lblTituloError.Text = "Configuración parámetros";
        //        lblMensajeError.Text = "El fichero no parece tener el formato correcto." + "<br /> &nbsp;" + "Por favor, verifique el fichero e intente procesarlo de nuevo.";
        //        mpeError.Show();
        //        return;
        //    }

        //    // Validar valores cabecera 
        //    if ((sl.GetCellValueAsString(FILA_GENERAL_VERSION, 2).Length == 0) || (sl.GetCellValueAsString(FILA_GENERAL_FECHA_DESDE, 2).Length == 0))
        //    {
        //        //MessageBox.Show("Debe indicar la versión, y la fecha desde de la misma." + Environment.NewLine + "Por favor, verifique el fichero e intente procesarlo de nuevo", "Configuración parámetros", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar la versión, y la fecha desde de la misma. Por favor, verifique el fichero e intente procesarlo de nuevo.');", true);
        //        lblTituloError.Text = "Configuración parámetros";
        //        lblMensajeError.Text = "Debe indicar la versión, y la fecha desde de la misma." + "<br /> &nbsp;" + "Por favor, verifique el fichero e intente procesarlo de nuevo.";
        //        mpeError.Show();
        //        return;
        //    }

        //    version = sl.GetCellValueAsString(FILA_GENERAL_VERSION, 2);
        //    fechaDesde = sl.GetCellValueAsDateTime(FILA_GENERAL_FECHA_DESDE, 2);
        //    fechaHasta = sl.GetCellValueAsDateTime(FILA_GENERAL_FECHA_HASTA, 2);

        //    // Validar versión y fechas
        //    conexiones.crearConexion();
        //    conexiones.consulta = "sp_ROP_ConfiguracionGeneralValidar";
        //    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;

        //    parametroVersion = new SqlParameter("@CFG_Version", SqlDbType.VarChar, 10);
        //    parametroVersion.Value = version;
        //    conexiones.comando.Parameters.Add(parametroVersion);
        //    parametroFechaDesde = new SqlParameter("@CGE_FechaDesde", SqlDbType.DateTime);
        //    if (sl.GetCellValueAsString(FILA_GENERAL_FECHA_DESDE, 2).Length == 0 || rdbPruebaGeneral.Checked)
        //        parametroFechaDesde.Value = DBNull.Value;
        //    else
        //        parametroFechaDesde.Value = fechaDesde;
        //    conexiones.comando.Parameters.Add(parametroFechaDesde);
        //    SqlDataReader dr = conexiones.comando.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        if (Convert.ToString(dr["CFG_Version"]).Length > 0)
        //        {
        //            lblMensajeError.Text = "La versión indicada ya existe." + "<br /> &nbsp;" + "Por favor, indique un nuevo número de versión e intente procesar el fichero de nuevo.";
        //            mpeError.Show();
        //            return;
        //        }
        //        if (Convert.ToString(dr["CGE_FechaDesde"]).Length > 0)
        //        {
        //            lblMensajeError.Text = "La versión indicada tiene una fecha que coincide con otra versión." + "<br /> &nbsp;" + "Por favor, indique otra fecha e intente procesar el fichero de nuevo.";
        //            mpeError.Show();
        //            return;
        //        }
        //    }

        //    conexiones.crearConexion();
        //    fila = FILA_GENERAL_DATOS;
        //    while (sl.GetCellValueAsString(fila, 1) != "")
        //    {
        //        conexiones.consulta = "sp_ROP_ConfiguracionGeneralIncluir";
        //        conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //        conexiones.comando.CommandType = CommandType.StoredProcedure;

        //        parametroVersion = new SqlParameter("@CFG_Version", SqlDbType.VarChar, 10);
        //        if (version.Length == 0)
        //            parametroVersion.Value = DBNull.Value;
        //        else
        //            parametroVersion.Value = version;
        //        conexiones.comando.Parameters.Add(parametroVersion);

        //        parametroPrueba = new SqlParameter("@CFG_VersionPrueba", SqlDbType.Bit);
        //        parametroPrueba.Value = rdbPruebaGeneral.Checked;
        //        conexiones.comando.Parameters.Add(parametroPrueba);

        //        parametroObservaciones = new SqlParameter("@CGE_Observaciones", SqlDbType.VarChar, 4000);
        //        parametroObservaciones.Value = DBNull.Value;
        //        conexiones.comando.Parameters.Add(parametroObservaciones);

        //        parametroFechaDesde = new SqlParameter("@CGE_FechaDesde", SqlDbType.DateTime);
        //        if (sl.GetCellValueAsString(FILA_GENERAL_FECHA_DESDE, 2).Length == 0 || rdbPruebaGeneral.Checked)
        //            parametroFechaDesde.Value = DBNull.Value;
        //        else
        //            parametroFechaDesde.Value = fechaDesde;
        //        conexiones.comando.Parameters.Add(parametroFechaDesde);

        //        parametroFechaHasta = new SqlParameter("@CGE_FechaHasta", SqlDbType.DateTime);
        //        if (sl.GetCellValueAsString(FILA_GENERAL_FECHA_HASTA, 2).Length == 0 || rdbPruebaGeneral.Checked)
        //            parametroFechaHasta.Value = DBNull.Value;
        //        else
        //            parametroFechaHasta.Value = fechaHasta;
        //        conexiones.comando.Parameters.Add(parametroFechaHasta);

        //        parametroConcepto = new SqlParameter("@CGE_Concepto", SqlDbType.VarChar, 100);
        //        if (sl.GetCellValueAsString(fila, COL_FicheroGeneralConcepto).Length == 0)
        //            parametroConcepto.Value = DBNull.Value;
        //        else
        //            parametroConcepto.Value = sl.GetCellValueAsString(fila, COL_FicheroGeneralConcepto);
        //        conexiones.comando.Parameters.Add(parametroConcepto);

        //        parametroEmpresa = new SqlParameter("@CGE_Empresa", SqlDbType.VarChar, 5);
        //        if (sl.GetCellValueAsString(fila, COL_FicheroGeneralEmpresa).Length == 0)
        //            parametroEmpresa.Value = DBNull.Value;
        //        else
        //            parametroEmpresa.Value = sl.GetCellValueAsString(fila, COL_FicheroGeneralEmpresa);
        //        conexiones.comando.Parameters.Add(parametroEmpresa);

        //        parametroValor = new SqlParameter("@CGE_Valor", SqlDbType.Decimal);
        //        parametroValor.Precision = 18;
        //        parametroValor.Scale = 2;
        //        if (sl.GetCellValueAsString(fila, COL_FicheroGeneralValor).ToString().Length == 0)
        //            parametroValor.Value = DBNull.Value;
        //        else
        //            parametroValor.Value = sl.GetCellValueAsDecimal(fila, COL_FicheroGeneralValor);
        //        conexiones.comando.Parameters.Add(parametroValor);

        //        conexiones.comando.ExecuteNonQuery();

        //        fila = fila + 1;
        //    }
        //    conexiones.conexion.Close();
        //    //MessageBox.Show("Fichero procesado", "Agregar versión", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Fichero procesado.');", true);
        //    lblTituloInformacion.Text = "Agregar versión";
        //    lblMensajeInformacion.Text = "Fichero procesado.";
        //    mpeInformacion.Show();

        //    rellenarGridGeneral();
        //    //rellenarCombosVersionGeneral();
        //}

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
            // para importar: reales y pruebas
            cmbVersionExportar.Items.Clear();
            cmbVersionExportar.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionVersionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.Parameters.AddWithValue("@selector", 1);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cmbVersionExportar.Items.Add(dr.GetString(0));
                }
            }
            conexiones.conexion.Close();
            //cmbVersionExportar.Text = "TODOS";
            cmbVersionExportar.Text = "";

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
            cmbVersionPruebas.Items.Clear();
            cmbVersionPruebas.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionVersionPruebasConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader drPruebas = conexiones.comando.ExecuteReader();
            if (drPruebas.HasRows)
            {
                while (drPruebas.Read())
                {
                    cmbVersionPruebas.Items.Add(drPruebas.GetString(0));
                }
            }
            conexiones.conexion.Close();
            cmbVersionPruebas.Text = "";
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (cmbVersionExportar.Text == "")
            {
                lblTituloError.Text = "Exportar excel GFV";
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
                nombreInforme = Server.MapPath("~/Ficheros excel/Configuracion_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                exportarExcel();
            }
            lblTituloInformacion.Text = "Exportar configuración";
            lblMensajeInformacion.Text = "Fichero excel generado.";
            mpeInformacion.Show();
            btnAbrirExcel.Visible = true;
        }

        protected void btnAbrirExcel_Click(object sender, EventArgs e)
        {
            if (cmbVersionExportar.Text == "TODOS" || cmbVersionExportar.Text == "TODOS Reales" || cmbVersionExportar.Text == "TODOS Pruebas")
            {
                Context.Response.Redirect("Ficheros excel/Configuracion__TODO_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
            }
            else
            {
                Context.Response.Redirect("Ficheros excel/Configuracion_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
            }
            btnAbrirExcel.Visible = false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cmbVersionEliminar.Text == "")
            {
                lblTituloError.Text = "Eliminar versión";
                lblMensajeError.Text = "Debe indicar la versión a eliminar.";
                mpeError.Show();
                return;
            }
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionEliminar";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@CFG_Version", cmbVersionEliminar.Text);
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            rellenarGrid();

            lblTituloError.Text = "Eliminar GFV";
            lblMensajeError.Text = "Versión eliminada.";
            mpeError.Show();
        }

        protected void btnReal_Click(object sender, EventArgs e)
        {
            if (cmbVersionPruebas.Text == "")
            {
                //MessageBox.Show("Debe indicar la versión a pasar a real.", "Pasar GFV a real", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar la versión a pasar a real.');", true);
                lblTituloError.Text = "Pasar a real";
                lblMensajeError.Text = "Debe indicar la versión a pasar a real.";
                mpeError.Show();
                return;
            }
        }

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
                // procesarExcel(directorio);
                lblTituloError.Text = "Importar fichero excel";
                lblMensajeError.Text = "-- PENDIENTE DE PROGRAMAR --";
                mpeError.Show();
            }
            else
            {
                lblTituloError.Text = "Importar fichero excel";
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
            //conexiones.comando.Parameters.AddWithValue("@selector", 0);
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

        protected void CambioFiltroConcepto(object sender, EventArgs e)
        {
            DropDownList cmbFiltroConcepto = (DropDownList)sender;
            ViewState["FiltroConcepto"] = cmbFiltroConcepto.SelectedValue;
            this.rellenarGrid();
        }

        private void rellenarFiltroConcepto(DropDownList cmbFiltroConcepto)
        {
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionConceptosConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroVersion"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", ViewState["FiltroVersion"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroConcepto.DataSource = dt;
            cmbFiltroConcepto.DataTextField = "CFGCON_Concepto";
            cmbFiltroConcepto.DataValueField = "CFGCON_Concepto";
            cmbFiltroConcepto.DataBind();
            conexiones.conexion.Close();
            cmbFiltroConcepto.Items.FindByValue(ViewState["FiltroConcepto"].ToString()).Selected = true;
        }
        private void rellenarGrid()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 10);
            if (ViewState["FiltroVersion"].ToString() == "")
                parametroVersion.Value = null;
            else
                parametroVersion.Value = ViewState["FiltroVersion"].ToString();
            conexiones.comando.Parameters.Add(parametroVersion);

            SqlParameter parametroConcepto = new SqlParameter("@concepto", SqlDbType.NVarChar, 100);
            if (ViewState["FiltroConcepto"].ToString() == "")
                parametroConcepto.Value = null;
            else
                parametroConcepto.Value = ViewState["FiltroConcepto"].ToString();
            conexiones.comando.Parameters.Add(parametroConcepto);

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

            cmbFiltro = (DropDownList)grvDatos.HeaderRow.FindControl("FiltroConcepto");
            this.rellenarFiltroConcepto(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatos.HeaderRow.FindControl("FiltroConceptoValor");
            this.rellenarFiltroConceptoValor(cmbFiltro);
        }

        private void exportarExcel()
        {
            SLStyle style;
            SLStyle styleDec;
            SLDataValidation dv;
            int fila = 2;
            int columna = 0;
            decimal valor = 0;
            int cantidadConceptosNivel1 = 0;
            int cantidadConceptosNivel2 = 0;
            int cantidadConceptosNivel3 = 0;
            int cantidadConceptosNivel4 = 0;
            int cantidadValoresNivel4 = 0;
            int cantidadEmpresas = 0;
            int cantidadFamilias = 0;
            int cantidadSubfamilias = 0;
            int cantidadMonedas = 0;
            int cantidadSignos = 2;
            int cantidadTiposServicios = 0;
            int cantidadBooleano = 2;

            btnAbrirExcel.Visible = false;
            // Consulta SQL
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

            // Crear el excel
            SLDocument sl = new SLDocument();

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Datos Generales");

            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Datos Generales");

                // VERSIÓN
                sl.SetCellValue(1, 1, "Versión");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                sl.SetCellStyle(1, 1, style);

                sl.SetCellValue(1, 2, Row.ItemArray[COL_Version].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                sl.SetCellStyle(1, 2, style);

                // FECHA DESDE 
                sl.SetCellValue(2, 1, "Desde");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                style.FormatCode = "dd-MM-yyyy";
                sl.SetCellStyle(2, 1, style);

                sl.SetCellValue(2, 2, Row.ItemArray[COL_Desde].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                sl.SetCellStyle(2, 2, style);

                // FECHA HASTA
                sl.SetCellValue(3, 1, "Hasta");
                style = sl.CreateStyle();
                style.SetFont("Verdana", 12);
                style.Font.Bold = true;
                style.FormatCode = "dd-MM-yyyy";
                sl.SetCellStyle(3, 1, style);

                sl.SetCellValue(3, 2, Row.ItemArray[COL_Hasta].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 11);
                sl.SetCellStyle(3, 2, style);

                sl.AutoFitColumn(1, 2);
            }

            // HOJAS OCULTAS
            sl.AddWorksheet("Conceptos Nivel 1");
            conexiones.consulta = "sp_ROP_ConceptosNivel1Listado";
            SqlDataAdapter adaptadorConceptosNivel1 = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtConceptosNivel1 = new System.Data.DataTable();
            adaptadorConceptosNivel1.Fill(dtConceptosNivel1);
            sl.ImportDataTable(1, 1, dtConceptosNivel1, false);
            sl.HideWorksheet("Conceptos Nivel 1");
            cantidadConceptosNivel1= dtConceptosNivel1.Rows.Count;

            sl.AddWorksheet("Conceptos Nivel 2");
            conexiones.consulta = "sp_ROP_ConceptosNivel2Listado";
            SqlDataAdapter adaptadorConceptosNivel2 = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtConceptosNivel2 = new System.Data.DataTable();
            adaptadorConceptosNivel2.Fill(dtConceptosNivel2);
            sl.ImportDataTable(1, 1, dtConceptosNivel2, false);
            sl.HideWorksheet("Conceptos Nivel 2");
            cantidadConceptosNivel2 = dtConceptosNivel2.Rows.Count;

            sl.AddWorksheet("Conceptos Nivel 3");
            conexiones.consulta = "sp_ROP_ConceptosNivel3Listado";
            SqlDataAdapter adaptadorConceptosNivel3 = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtConceptosNivel3 = new System.Data.DataTable();
            adaptadorConceptosNivel3.Fill(dtConceptosNivel3);
            sl.ImportDataTable(1, 1, dtConceptosNivel3, false);
            sl.HideWorksheet("Conceptos Nivel 3");
            cantidadConceptosNivel3 = dtConceptosNivel3.Rows.Count;

            sl.AddWorksheet("Conceptos Nivel 4");
            conexiones.consulta = "sp_ROP_ConceptosNivel4Listado";
            SqlDataAdapter adaptadorConceptosNivel4 = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtConceptosNivel4 = new System.Data.DataTable();
            adaptadorConceptosNivel4.Fill(dtConceptosNivel4);
            sl.ImportDataTable(1, 1, dtConceptosNivel4, false);
            sl.HideWorksheet("Conceptos Nivel 4");
            cantidadConceptosNivel4 = dtConceptosNivel4.Rows.Count;

            sl.AddWorksheet("Valores Nivel 4");
            conexiones.consulta = "sp_ROP_ValoresNivel4Listado";
            SqlDataAdapter adaptadorValoresNivel4 = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtValoresNivel4 = new System.Data.DataTable();
            adaptadorConceptosNivel4.Fill(dtValoresNivel4);
            sl.ImportDataTable(1, 1, dtValoresNivel4, false);
            sl.HideWorksheet("Valores Nivel 4");
            cantidadValoresNivel4 = dtValoresNivel4.Rows.Count;

            sl.AddWorksheet("Empresas");
            conexiones.consulta = "sp_ROP_EmpresasListado";
            SqlDataAdapter adaptadorEmpresa = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtEmpresas = new System.Data.DataTable();
            adaptadorEmpresa.Fill(dtEmpresas);
            sl.ImportDataTable(1, 1, dtEmpresas, false);
            sl.HideWorksheet("Empresas");
            cantidadEmpresas = dtEmpresas.Rows.Count;

            sl.AddWorksheet("Familias");
            conexiones.consulta = "sp_ROP_FamiliasListado";
            SqlDataAdapter adaptadorFamilia = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtFamilias = new System.Data.DataTable();
            adaptadorFamilia.Fill(dtFamilias);
            sl.ImportDataTable(1, 1, dtFamilias, false);
            sl.HideWorksheet("Familias");
            cantidadFamilias = dtFamilias.Rows.Count;

            sl.AddWorksheet("Subfamilias");
            conexiones.consulta = "sp_ROP_SubfamiliasListado";
            SqlDataAdapter adaptadorSubfamilia = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtSubfamilias = new System.Data.DataTable();
            adaptadorSubfamilia.Fill(dtSubfamilias);
            sl.ImportDataTable(1, 1, dtSubfamilias, false);
            sl.HideWorksheet("Subfamilias");
            cantidadSubfamilias = dtSubfamilias.Rows.Count;

            sl.AddWorksheet("Monedas");
            conexiones.consulta = "sp_ROP_MonedasListado";
            SqlDataAdapter adaptadorMonedas = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtMonedas = new System.Data.DataTable();
            adaptadorMonedas.Fill(dtMonedas);
            sl.ImportDataTable(1, 1, dtMonedas, false);
            sl.HideWorksheet("Monedas");
            cantidadMonedas= dtMonedas.Rows.Count;

            sl.AddWorksheet("Signos");
            sl.SetCellValue(1, 1, "+");
            sl.SetCellValue(2, 1, "-");
            sl.HideWorksheet("Signos");

            sl.AddWorksheet("Tipos servicio");
            conexiones.consulta = "sp_ROP_servicioConsulta";
            SqlDataAdapter adaptadorTiposServicio = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtTiposServicio = new System.Data.DataTable();
            adaptadorTiposServicio.Fill(dtTiposServicio);
            sl.ImportDataTable(1, 1, dtTiposServicio, false);
            sl.HideWorksheet("Tipos servicio");
            cantidadTiposServicios = dtTiposServicio.Rows.Count;

            sl.AddWorksheet("Booleano");
            sl.SetCellValue(1, 1, "SI");
            sl.SetCellValue(2, 1, "NO");
            sl.HideWorksheet("Booleano");

            // Consulta SQL NIVEL 1
            conexiones.crearConexion();
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
            sl.Filter("A1", "I1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Nivel 1'!$A$1:$A$" + cantidadConceptosNivel1.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowList("'Familias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("D2", "D1000");
            dv.AllowList("'Subfamilias'!$A$1:$A$" + cantidadSubfamilias.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("F2", "G1000");
            dv.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("H2", "H1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("I2", "I1000");
            dv.AllowList("'Monedas'!$A$1:$A$" + cantidadMonedas.ToString(), true, true);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "Empresa");
            sl.SetCellValue(1, 3, "Familia");
            sl.SetCellValue(1, 4, "Subfamilia");
            sl.SetCellValue(1, 5, "Artículo");
            sl.SetCellValue(1, 6, "Desde");
            sl.SetCellValue(1, 7, "Hasta");
            sl.SetCellValue(1, 8, "Valor");
            sl.SetCellValue(1, 9, "Moneda");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 30);
            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 25);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 20);
            sl.SetColumnWidth(7, 20);
            sl.SetColumnWidth(8, 20);
            sl.SetColumnWidth(9, 20);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 9; columna++)
                sl.SetCellStyle(1, columna, style);

            //sl.FreezePanes(1, 1);

            fila = 2;
            foreach (DataRow Row in dsNivel1.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Empresa].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Familia].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_Subfamilia].ToString());
                sl.SetCellValue(fila, 5, Row.ItemArray[COL_Articulo].ToString());
                sl.SetCellValue(fila, 6, Row.ItemArray[COL_ValorDesde].ToString());
                sl.SetCellValue(fila, 7, Row.ItemArray[COL_ValorHasta].ToString());
                decimal.TryParse(Row.ItemArray[COL_Valor].ToString(), out valor);
                sl.SetCellValue(fila, 8, valor.ToString("#,##0.00"));
                sl.SetCellValue(fila, 9, Row.ItemArray[COL_Moneda].ToString());
                
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0";
                sl.SetCellStyle(fila, 6, style);
                sl.SetCellStyle(fila, 7, style);
                styleDec = sl.CreateStyle();
                styleDec.SetFont("Verdana", 10);
                styleDec.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                styleDec.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 8, styleDec);
                fila++;
            }
            sl.AutoFitColumn(1, 9);

            // Consulta SQL NIVEL 2
            conexiones.crearConexion();
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
            sl.Filter("A1", "C1");

            dv = sl.CreateDataValidation("A2", "A1000");
            dv.AllowList("'Conceptos Nivel 2'!$A$1:$A$" + cantidadConceptosNivel2.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("B2", "B1000");
            dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
            sl.AddDataValidation(dv);

            dv = sl.CreateDataValidation("C2", "C1000");
            dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.00, false);
            sl.AddDataValidation(dv);

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "Concepto");
            sl.SetCellValue(1, 2, "Empresa");
            sl.SetCellValue(1, 3, "Valor");

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
            foreach (DataRow Row in dsNivel2.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Concepto_Nivel2].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Empresa_Nivel2].ToString());
                decimal.TryParse(Row.ItemArray[COL_Valor_Nivel2].ToString(), out valor);
                sl.SetCellValue(fila, 3, valor.ToString("#,##0.00"));

                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 3, style);
                fila++;
            }
            sl.AutoFitColumn(1, 3);

            // Consulta SQL NIVEL 3
            conexiones.crearConexion();
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
                style.FormatCode = "#,##0";
                sl.SetCellStyle(fila, 2, style);

                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0";
                sl.SetCellStyle(fila, 3, style);
                fila++;
            }
            sl.AutoFitColumn(1, 3);

            // Consulta SQL NIVEL 4
            conexiones.crearConexion();
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
            conexiones.crearConexion();
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

            // Consulta SQL PANELES
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_DatosPanelesConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (cmbVersionExportar.Text == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", cmbVersionExportar.Text);
            SqlDataAdapter adaptadorPanales= new SqlDataAdapter(conexiones.comando);
            DataSet dsPaneles = new DataSet();
            adaptadorPanales.Fill(dsPaneles);

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

        protected void rellenarServicio()
        {
            cmbTipo.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_servicioConsulta";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbTipo.DataSource = dt;
            cmbTipo.DataTextField = "CFGSERV_Tipo";
            cmbTipo.DataValueField = "CFGSERV_Tipo";
            cmbTipo.DataBind();
            conexiones.conexion.Close();
        }
        protected void btnLimpiarServicio_Click(object sender, EventArgs e)
        {
            txtFamilia.Text = "";
            txtSubfamilia.Text = "";
            txtArticulo.Text = "";
            cmbTipo.Text = "";
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            if ((txtFamilia.Text != "" || txtSubfamilia.Text != "" || txtArticulo.Text != "") && cmbTipo.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionServicioAgregar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroID = new SqlParameter("@CFGSERV_ID", SqlDbType.Int);
                parametroID.Value = null;
                conexiones.comando.Parameters.Add(parametroID);
                SqlParameter parametroFamilia = new SqlParameter("@Familia", SqlDbType.VarChar, 20);
                if (txtFamilia.Text == "")
                    parametroFamilia.Value = null;
                else
                    parametroFamilia.Value = txtFamilia.Text;
                conexiones.comando.Parameters.Add(parametroFamilia);
                SqlParameter parametroSubfamilia = new SqlParameter("@Subfamilia", SqlDbType.VarChar, 20);
                if (txtSubfamilia.Text == "")
                    parametroSubfamilia.Value = null;
                else
                    parametroSubfamilia.Value = txtSubfamilia.Text;
                conexiones.comando.Parameters.Add(parametroSubfamilia);
                SqlParameter parametroArticulo = new SqlParameter("@ART_ID", SqlDbType.VarChar, 20);
                if (txtArticulo.Text == "")
                    parametroArticulo.Value = null;
                else
                    parametroArticulo.Value = txtArticulo.Text;
                conexiones.comando.Parameters.Add(parametroArticulo);
                SqlParameter parametroTipo = new SqlParameter("@CFGSERV_Tipo", SqlDbType.VarChar, 50);
                parametroTipo.Value = cmbTipo.Text;
                conexiones.comando.Parameters.Add(parametroTipo);

                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "Agregar servicio";
                lblMensajeError.Text = "Debe indicar la familia/subfamilia/artículo y el tipo de servicio.";
                mpeError.Show();
            }
            txtFamilia.Text = "";
            txtSubfamilia.Text = "";
            txtArticulo.Text = "";
            cmbTipo.Text = "";
            rellenarGridServicios();
        }

        private void rellenarGridServicios()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionServicioConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvServicios.DataSource = dr;
            grvServicios.DataBind();
            conexiones.conexion.Close();
        }

        protected void grvServicios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvServicios.EditIndex = e.NewEditIndex;
            rellenarGridServicios();
        }

        protected void grvServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList cmbTipoServicio = (e.Row.FindControl("cmbTipoServicio") as DropDownList);

                cmbTipoServicio.DataSource = null;
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_servicioConsulta";
                SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                adaptador.Fill(dt);
                cmbTipoServicio.DataSource = dt;
                cmbTipoServicio.DataTextField = "CFGSERV_Tipo";
                cmbTipoServicio.DataValueField = "CFGSERV_Tipo";
                cmbTipoServicio.DataBind();
                conexiones.conexion.Close();
                cmbTipoServicio.Items.Insert(0, new ListItem(""));
                string tipo = (e.Row.FindControl("lblTipoServicio") as Label).Text;
                cmbTipoServicio.Items.FindByValue(tipo).Selected = true;
                cmbTipoServicio.Enabled = e.Row.RowIndex == grvServicios.EditIndex;
            }
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

        protected void txtMargen_TextChanged(object sender, EventArgs e)
        {
            decimal numero = Convert.ToDecimal(txtMargen.Text.Replace(".",","));
            txtMargen.Text = numero.ToString("N2");
        }

        protected void txtValor_TextChanged(object sender, EventArgs e)
        {
            decimal numero = Convert.ToDecimal(txtValor.Text.Replace(".", ","));
            txtValor.Text = numero.ToString("N6");
        }

        protected void rellenarBU()
        {
            cmbBUCodigos.Items.Clear();
            cmbBUCodigos.DataSource = null;
            cmbBUCodigos.DataBind();

            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_BUs";
            conexiones.comando.CommandTimeout = 240000;
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbBUCodigos.DataSource = dt;
            cmbBUCodigos.DataTextField = "BU";
            cmbBUCodigos.DataValueField = "BU";
            cmbBUCodigos.DataBind();
            conexiones.conexion.Close();
        }

        protected void rellenarEmpresa()
        {
            cmbEmpresa.Items.Clear();
            cmbEmpresa.DataSource = null;
            cmbEmpresa.DataBind();

            cmbEmpresaCodigos.Items.Clear();
            cmbEmpresaCodigos.DataSource = null;
            cmbEmpresaCodigos.DataBind();

            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_Empresas";
            conexiones.comando.CommandTimeout = 240000;
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroBU = new SqlParameter("@BU", SqlDbType.VarChar, 10);
            parametroBU.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroBU);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbEmpresa.DataSource = dt;
            cmbEmpresa.DataTextField = "Empresa";
            cmbEmpresa.DataValueField = "Empresa";
            cmbEmpresa.DataBind();

            cmbEmpresaCodigos.DataSource = dt;
            cmbEmpresaCodigos.DataTextField = "Empresa";
            cmbEmpresaCodigos.DataValueField = "Empresa";
            cmbEmpresaCodigos.DataBind();
            conexiones.conexion.Close();
        }

        protected void rellenarDelegacion()
        {
            cmbDelegacion.Items.Clear();
            cmbDelegacion.DataSource = null;
            cmbDelegacion.DataBind();

            cmbDelegacionCodigos.Items.Clear();
            cmbDelegacionCodigos.DataSource = null;
            cmbDelegacionCodigos.DataBind();

            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_Delegaciones";
            conexiones.comando.CommandTimeout = 240000;
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.VarChar, 5);
            parametroEmpresa.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbDelegacion.DataSource = dt;
            cmbDelegacion.DataTextField = "Delegacion";
            cmbDelegacion.DataValueField = "Delegacion";
            cmbDelegacion.DataBind();

            cmbDelegacionCodigos.DataSource = dt;
            cmbDelegacionCodigos.DataTextField = "Delegacion";
            cmbDelegacionCodigos.DataValueField = "Delegacion";
            cmbDelegacionCodigos.DataBind();

            adaptador.Dispose();
            conexiones.comando.Dispose();
            conexiones.conexion.Close();
            conexiones.conexion.Dispose();
        }

        protected void rellenarTransporte()
        {
            cmbDistancia.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_transporteConsulta";
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbDistancia.DataSource = dt;
            cmbDistancia.DataTextField = "CFGTRA_Distancia";
            cmbDistancia.DataValueField = "CFGTRA_Distancia";
            cmbDistancia.DataBind();
            conexiones.conexion.Close();
        }

        protected void btnLimpiarTransporteGeneral_Click(object sender, EventArgs e)
        {
            txtMeses.Text = "";
            txtDesvioTransporte.Text = "";
        }

        protected void cmbEmpresa_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList dropEmpresa = (DropDownList)sender;
            string selectedEmpresa = (string)dropEmpresa.SelectedValue;

            if (selectedEmpresa != "")
            {
                cmbDelegacion.Items.Clear();
                cmbDelegacion.DataSource = null;
                cmbDelegacion.DataBind();

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Delegaciones";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.VarChar, 5);
                parametroEmpresa.Value = selectedEmpresa.ToString();
                conexiones.comando.Parameters.Add(parametroEmpresa);
                SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dt = new System.Data.DataTable();
                adaptador.Fill(dt);
                cmbDelegacion.DataSource = dt;
                cmbDelegacion.DataTextField = "Delegacion";
                cmbDelegacion.DataValueField = "Delegacion";
                cmbDelegacion.DataBind();
                adaptador.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();
            }
        }

        protected void btnEditarTransporteGeneral_Click(object sender, EventArgs e)
        {
            txtMeses.Enabled = true;
            txtDesvioTransporte.Enabled = true;
            btnEditarTransporteGeneral.Visible = false;
            btnGuardarTransporteGeneral.Visible = true;
            btnCancelarTransporteGeneral.Visible = true;
            btnctualizarTransporte.Enabled = false;
        }

        protected void btnCancelarTransporteGeneral_Click(object sender, EventArgs e)
        {
            txtMeses.Enabled = false;
            txtDesvioTransporte.Enabled = false;
            btnEditarTransporteGeneral.Visible = true;
            btnGuardarTransporteGeneral.Visible = false;
            btnCancelarTransporteGeneral.Visible = false;
            btnctualizarTransporte.Enabled = true;
        }

        protected void btnGuardarTransporteGeneral_Click(object sender, EventArgs e)
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteGeneral";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@CFGTRA_Meses", Convert.ToInt32(txtMeses.Text));
            conexiones.comando.Parameters.AddWithValue("@CFGTRA_Desvio", Convert.ToDecimal(txtDesvioTransporte.Text));
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            txtMeses.Enabled = false;
            txtDesvioTransporte.Enabled = false;
            btnEditarTransporteGeneral.Visible = true;
            btnGuardarTransporteGeneral.Visible = false;
            btnCancelarTransporteGeneral.Visible = false;
            btnctualizarTransporte.Enabled = true;

            conexiones.crearConexion();
            conexiones.consulta = "ROP_Transporte";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            rellenarGridTransporte();
        }

        protected void btnActualizarTransporteGeneral_Click(object sender, EventArgs e)
        {
            //DateTime horaTotal_1 = default(DateTime);
            //TimeSpan horaTotal_2 = default(TimeSpan);


            //horaTotal_1 = DateTime.Now;
            conexiones.crearConexion();
            conexiones.consulta = "ROP_Transporte";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            //horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);
            //lbltiempo.Text = horaTotal_2.Minutes.ToString("00") + ":" + horaTotal_2.Seconds.ToString("00") + ":" + horaTotal_2.Milliseconds.ToString("00");

            rellenarTransporteGeneral();
            rellenarGridTransporte();
        }

        protected void cmbBUCodigos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList dropBU = (DropDownList)sender;
            string selectedBU = (string)dropBU.SelectedValue;

            if (selectedBU != "")
            {
                cmbEmpresaCodigos.Items.Clear();
                cmbEmpresaCodigos.DataSource = null;
                cmbEmpresaCodigos.DataBind();

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Delegaciones";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroBU= new SqlParameter("@BU", SqlDbType.VarChar, 5);
                parametroBU.Value = selectedBU.ToString();
                conexiones.comando.Parameters.Add(parametroBU);
                SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dt = new System.Data.DataTable();
                adaptador.Fill(dt);
                cmbEmpresaCodigos.DataSource = dt;
                cmbEmpresaCodigos.DataTextField = "Empresa";
                cmbEmpresaCodigos.DataValueField = "Empresa";
                cmbEmpresaCodigos.DataBind();
                adaptador.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();
            }
        }

        protected void cmbEmpresaCodigos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList dropEmpresa = (DropDownList)sender;
            string selectedEmpresa = (string)dropEmpresa.SelectedValue;

            if (selectedEmpresa != "")
            {
                cmbDelegacionCodigos.Items.Clear();
                cmbDelegacionCodigos.DataSource = null;
                cmbDelegacionCodigos.DataBind();

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Delegaciones";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.VarChar, 5);
                parametroEmpresa.Value = selectedEmpresa.ToString();
                conexiones.comando.Parameters.Add(parametroEmpresa);
                SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dt = new System.Data.DataTable();
                adaptador.Fill(dt);
                cmbDelegacionCodigos.DataSource = dt;
                cmbDelegacionCodigos.DataTextField = "Delegacion";
                cmbDelegacionCodigos.DataValueField = "Delegacion";
                cmbDelegacionCodigos.DataBind();
                adaptador.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();
            }
        }


        //protected void btnActualizarTransporteGeneralPRUEBA_Click(object sender, EventArgs e)
        //{
        //    DateTime horaTotal_1 = default(DateTime);
        //    TimeSpan horaTotal_2 = default(TimeSpan);


        //    horaTotal_1 = DateTime.Now;
        //    conexiones.crearConexion();
        //    conexiones.consulta = "ROP_TransportePRUEBA";
        //    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //    conexiones.comando.CommandType = CommandType.StoredProcedure;
        //    conexiones.comando.CommandTimeout = 100000;
        //    conexiones.comando.ExecuteNonQuery();
        //    conexiones.conexion.Close();

        //    horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);
        //    lbltiempo.Text = horaTotal_2.Minutes.ToString("00") + ":" + horaTotal_2.Seconds.ToString("00") + ":" + horaTotal_2.Milliseconds.ToString("00");

        //    rellenarTransporteGeneral();
        //    rellenarGridTransporte();
        //}

        protected void btnLimpiarTransporte_Click(object sender, EventArgs e)
        {
            cmbEmpresa.Text = "";
            cmbDelegacion.Text = "";
            txtDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtHasta.Text = "";
            txtMargen.Text = "";
            cmbDistancia.Text = "";
            txtPropuesto.Text = "";
            txtPropuesto.Enabled = false;
            txtValor.Text = "";
            txtDesvio.Text = "";
            cmbEmpresa.Enabled = true;
            cmbDelegacion.Enabled = true;
            cmbDistancia.Enabled = true;
        }

        protected void btnPropuesta_Click(object sender, EventArgs e)
        {
            txtPropuesto.Text = "0,00";

            if (cmbEmpresa.Text == "" && cmbDistancia.Text == "")
            {
                lblTituloError.Text = "Calcular propuesta";
                lblMensajeError.Text = "Debe indicar la empresa y la distancia para poder calcular la propuesta.";
                mpeError.Show();
                return;
            }

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteCalcularProp";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroEmpresa = new SqlParameter("@Empresa", SqlDbType.VarChar, 10);
            if (cmbEmpresa.Text != "")
                parametroEmpresa.Value = cmbEmpresa.Text;
            else
                parametroEmpresa.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            SqlParameter parametroDelegacion = new SqlParameter("@Delegacion", SqlDbType.VarChar, 10);
            if (cmbDelegacion.Text != "")
                parametroDelegacion.Value = cmbDelegacion.Text;
            else
                parametroDelegacion.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroDelegacion);

            SqlParameter parametroDistancia = new SqlParameter("@Distancia", SqlDbType.VarChar, 10);
            if (cmbDistancia.Text != "")
                parametroDistancia.Value = cmbDistancia.Text;
            else
                parametroDistancia.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroDistancia);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtPropuesto.Text = Convert.ToDecimal(dr["Prop"]).ToString("N6");
                txtValor.Text = Convert.ToDecimal(dr["Prop"]).ToString("N6");
            }
            conexiones.conexion.Close();

            cmbEmpresa.Enabled = false;
            cmbDelegacion.Enabled = false;
            cmbDistancia.Enabled = false;
        }

        protected void btnAgregarTransporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEmpresa.Text != "" && txtMargen.Text != "" && txtValor.Text != "") // && txtDesvio.Text != "")
                {
                    conexiones.crearConexion();
                    conexiones.consulta = "sp_ROP_ConfiguracionTransporteAgregar";
                    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                    conexiones.comando.CommandType = CommandType.StoredProcedure;

                    SqlParameter parametroEmpresa = new SqlParameter("@Empresa", SqlDbType.VarChar, 10);
                    if (cmbEmpresa.Text != "")
                        parametroEmpresa.Value = cmbEmpresa.Text;
                    else
                        parametroEmpresa.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroEmpresa);
                    SqlParameter parametroDelegacion = new SqlParameter("@Delegacion", SqlDbType.VarChar, 10);
                    if (cmbDelegacion.Text != "")
                        parametroDelegacion.Value = cmbDelegacion.Text;
                    else
                        parametroDelegacion.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroDelegacion);
                    SqlParameter parametroMargen = new SqlParameter("@Margen", SqlDbType.Decimal);
                    if (txtMargen.Text != "")
                        parametroMargen.Value = Convert.ToDecimal(txtMargen.Text);
                    else
                        parametroMargen.Value = DBNull.Value;
                    parametroMargen.Precision = 18;
                    parametroMargen.Scale = 2;
                    conexiones.comando.Parameters.Add(parametroMargen);
                    SqlParameter parametroDesde = new SqlParameter("@Desde", SqlDbType.DateTime);
                    if (txtDesde.Text != "")
                        parametroDesde.Value = Convert.ToDateTime(txtDesde.Text);
                    else
                        parametroDesde.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroDesde);
                    SqlParameter parametroHasta = new SqlParameter("@Hasta", SqlDbType.DateTime);
                    if (txtHasta.Text != "")
                        parametroHasta.Value = Convert.ToDateTime(txtHasta.Text);
                    else
                        parametroHasta.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroHasta);
                    SqlParameter parametroDistancia = new SqlParameter("@Distancia", SqlDbType.VarChar, 10);
                    if (cmbDistancia.Text != "")
                        parametroDistancia.Value = cmbDistancia.Text;
                    else
                        parametroDistancia.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroDistancia);
                    SqlParameter parametroProp = new SqlParameter("@Prop", SqlDbType.Decimal);
                    if (txtValor.Text != "")
                        parametroProp.Value = Convert.ToDecimal(txtPropuesto.Text);
                    else
                        parametroProp.Value = DBNull.Value;
                    parametroProp.Precision = 36;
                    parametroProp.Scale = 18;
                    conexiones.comando.Parameters.Add(parametroProp);
                    SqlParameter parametroValor = new SqlParameter("@Valor", SqlDbType.Decimal);
                    if (txtValor.Text != "")
                        parametroValor.Value = Convert.ToDecimal(txtValor.Text);
                    else
                        parametroValor.Value = DBNull.Value;
                    parametroValor.Precision = 36;
                    parametroValor.Scale = 18;
                    conexiones.comando.Parameters.Add(parametroValor);
                    conexiones.comando.ExecuteNonQuery();
                    conexiones.conexion.Close();
                }
                else
                {
                    lblTituloError.Text = "Agregar transporte";
                    lblMensajeError.Text = "Debe indicar la empresa/margen/valor y la distancia.";
                    mpeError.Show();
                }

                cmbEmpresa.Text = "";
                cmbDelegacion.Text = "";
                txtDesde.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtHasta.Text = "";
                txtMargen.Text = "";
                cmbDistancia.Text = "";
                txtPropuesto.Text = "";
                txtPropuesto.Enabled = false;
                txtValor.Text = "";
                txtDesvio.Text = "";

                cmbEmpresa.Enabled = true;
                cmbDelegacion.Enabled = true;
                cmbDistancia.Enabled = true;

                rellenarGridTransporte();

                //lblTituloError.Text = "Agregar transporte";
                //lblMensajeError.Text = "Se creará el registro con los datos indicados. Es posible que se cierre algún registro con los datos actuales que ya no son válidos.";
                //mpeError.Show();
            }
            catch (Exception ex)
            {
                lblTituloError.Text = "Agregar transporte";
                lblMensajeError.Text = ex.Message;
                mpeError.Show();
            }
        }

        protected void btnLimpiarTransporteSubfamilias_Click(object sender, EventArgs e)
        {
            cmbBUCodigos.Text = "";
            cmbEmpresaCodigos.Text = "";
            cmbDelegacionCodigos.Text = "";
            txtSubfamillia.Text = "";
        }

        protected void btnAgregarTransporteSubfamilias_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSubfamillia.Text != "") 
                {
                    conexiones.crearConexion();
                    conexiones.consulta = "sp_ROP_ConfiguracionTransporteSubfamiliaAgregar";
                    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                    conexiones.comando.CommandType = CommandType.StoredProcedure;
                    SqlParameter parametroBU = new SqlParameter("@BU", SqlDbType.VarChar, 10);
                    if (cmbBUCodigos.Text != "")
                        parametroBU.Value = cmbBUCodigos.Text;
                    else
                        parametroBU.Value = DBNull.Value;
                    SqlParameter parametroEmpresa = new SqlParameter("@Empresa", SqlDbType.VarChar, 10);
                    if (cmbEmpresaCodigos.Text != "")
                        parametroEmpresa.Value = cmbEmpresaCodigos.Text;
                    else
                        parametroEmpresa.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroEmpresa);
                    SqlParameter parametroDelegacion = new SqlParameter("@Delegacion", SqlDbType.VarChar, 10);
                    if (cmbDelegacionCodigos.Text != "")
                        parametroDelegacion.Value = cmbDelegacionCodigos.Text;
                    else
                        parametroDelegacion.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroDelegacion);
                    SqlParameter parametroCodigos = new SqlParameter("@Subfamilia", SqlDbType.VarChar, 50);
                    if (txtSubfamillia.Text != "")
                        parametroCodigos.Value = txtSubfamillia.Text;
                    else
                        parametroCodigos.Value = DBNull.Value;
                    conexiones.comando.Parameters.Add(parametroCodigos);
                    conexiones.comando.ExecuteNonQuery();
                    conexiones.conexion.Close();
                }
                else
                {
                    lblTituloError.Text = "Agregar códigos transporte";
                    lblMensajeError.Text = "Debe indicar los códigos.";
                    mpeError.Show();
                }

                cmbBUCodigos.Text = "";
                cmbEmpresaCodigos.Text = "";
                cmbDelegacionCodigos.Text = "";
                txtSubfamilia.Text = "";
                
                cmbBUCodigos.Enabled=true;
                cmbEmpresaCodigos.Enabled = true;
                cmbDelegacionCodigos.Enabled = true;
                txtSubfamilia.Enabled = true;

                rellenarGridTransporteCodigos();
            }
            catch (Exception ex)
            {
                lblTituloError.Text = "Agregar transporte códigos";
                lblMensajeError.Text = ex.Message;
                mpeError.Show();
            }
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
                txtFechaActualizar.Text = dr.GetDateTime(CAMPO_Fecha).ToString("dd/MM/yyyy");
            }
            conexiones.conexion.Close();
            txtMeses.Enabled = false;
            txtDesvioTransporte.Enabled = false;
            btnEditarTransporteGeneral.Visible = true;
            btnGuardarTransporteGeneral.Visible = false;
            btnCancelarTransporteGeneral.Visible = false;
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

        private void rellenarGridTransporteCodigos()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteSubfamilia";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroEmpresa = new SqlParameter("@Empresa", SqlDbType.VarChar, 10);
            parametroEmpresa.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroEmpresa);
            SqlParameter parametroDelegacion = new SqlParameter("@Delegacion", SqlDbType.VarChar, 10);
            parametroDelegacion.Value = DBNull.Value;
            conexiones.comando.Parameters.Add(parametroDelegacion);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvTransporteSubfamilias.DataSource = dr;
            grvTransporteSubfamilias.DataBind();
            conexiones.conexion.Close();
        }


        protected void grvTransporteSubfamilias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvTransporteSubfamilias.EditIndex = e.NewEditIndex;
            rellenarGridTransporteCodigos();
        }

        protected void grvTransporteSubfamilias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cmbBU = (e.Row.FindControl("cmbBU") as DropDownList);

                cmbBU.DataSource = null;
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_BUs";
                SqlDataAdapter adaptadorBU= new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
                System.Data.DataTable dtBU = new System.Data.DataTable();
                adaptadorBU.Fill(dtBU);
                cmbBU.DataSource = dtBU;
                cmbBU.DataTextField = "BU";
                cmbBU.DataValueField = "BU";
                cmbBU.DataBind();
                conexiones.conexion.Close();
               
                //cmbBU.Items.Insert(0, new ListItem(""));
                string BU = (e.Row.FindControl("lblBU") as Label).Text;
                cmbBU.Items.FindByValue(BU).Selected = true;
                cmbBU.Enabled = e.Row.RowIndex == grvTransporteSubfamilias.EditIndex;

                DropDownList cmbEmpresa = (e.Row.FindControl("cmbEmpresa") as DropDownList);

                cmbEmpresa.DataSource = null;
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Empresas";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroBU = new SqlParameter("@BU", SqlDbType.VarChar, 10);
                parametroBU.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroBU);
                SqlDataAdapter adaptadorEmpresa = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dtEmpresa = new System.Data.DataTable();
                adaptadorEmpresa.Fill(dtEmpresa);
                cmbEmpresa.DataSource = dtEmpresa;
                cmbEmpresa.DataTextField = "Empresa";
                cmbEmpresa.DataValueField = "Empresa";
                cmbEmpresa.DataBind();
                conexiones.conexion.Close();
                //cmbEmpresa.Items.Insert(0, new ListItem(""));
                string empresa = (e.Row.FindControl("lblEmpresa") as Label).Text;
                cmbEmpresa.Items.FindByValue(empresa).Selected = true;
                cmbEmpresa.Enabled = e.Row.RowIndex == grvTransporteSubfamilias.EditIndex;

                DropDownList cmbDelegacion = (e.Row.FindControl("cmbDelegacion") as DropDownList);

                cmbEmpresa.DataSource = null;
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Delegaciones";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.VarChar, 5);
                parametroEmpresa.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroEmpresa);
                SqlDataAdapter adaptadorDelegacion = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dtDelegacion = new System.Data.DataTable();
                adaptadorDelegacion.Fill(dtDelegacion);
                cmbDelegacion.DataSource = dtDelegacion;
                cmbDelegacion.DataTextField = "Delegacion";
                cmbDelegacion.DataValueField = "Delegacion";
                cmbDelegacion.DataBind();
                adaptadorDelegacion.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                //cmbDelegacion.Items.Insert(0, new ListItem(""));
                string delegacion = (e.Row.FindControl("lblDelegacion") as Label).Text;
                cmbDelegacion.Items.FindByValue(delegacion).Selected = true;
                cmbDelegacion.Enabled = e.Row.RowIndex == grvTransporteSubfamilias.EditIndex;
            }
        }

        protected void grvTransporteSubfamilias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvTransporteSubfamilias.Rows[e.RowIndex].Cells[COLGRID_TRANS_ID].Controls[0]).Text;
            DropDownList cmbBU = grvTransporteSubfamilias.Rows[e.RowIndex].FindControl("cmbBU") as DropDownList;
            string BU = Convert.ToString(cmbBU.SelectedValue);
            DropDownList cmbEmpresa = grvTransporteSubfamilias.Rows[e.RowIndex].FindControl("cmbEmpresa") as DropDownList;
            string Empresa = Convert.ToString(cmbEmpresa.SelectedValue);
            DropDownList cmbDelegacion = grvTransporteSubfamilias.Rows[e.RowIndex].FindControl("cmbDelegacion") as DropDownList;
            string Delegacion = Convert.ToString(cmbDelegacion.SelectedValue);
            string Subfamilia = ((System.Web.UI.WebControls.TextBox)grvTransporteSubfamilias.Rows[e.RowIndex].Cells[COLGRID_TRANS_Subfamilia].Controls[0]).Text;
           
            if (Subfamilia != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionTransporteSubfamiliaModificar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroID = new SqlParameter("@CFGTCO_ID", SqlDbType.Int);
                parametroID.Value = Convert.ToInt32(ID);
                conexiones.comando.Parameters.Add(parametroID);
                SqlParameter parametroBU= new SqlParameter("@BU", SqlDbType.VarChar, 10);
                if (Empresa != "")
                    parametroBU.Value = BU;
                else
                    parametroBU.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroBU);
                SqlParameter parametroEmpresa = new SqlParameter("@Empresa", SqlDbType.VarChar, 10);
                if (Empresa != "")
                    parametroEmpresa.Value = Empresa;
                else
                    parametroEmpresa.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroEmpresa);
                SqlParameter parametroDelegacion = new SqlParameter("@Delegacion", SqlDbType.VarChar, 10);
                if (Delegacion != "")
                    parametroDelegacion.Value = Delegacion;
                else
                    parametroDelegacion.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroDelegacion);
                SqlParameter parametroCodigo = new SqlParameter("@Subfamilia", SqlDbType.VarChar, 10);
                if (Subfamilia != "")
                    parametroCodigo.Value = Subfamilia;
                else
                    parametroCodigo.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroCodigo);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();

                grvTransporteSubfamilias.EditIndex = -1;
                rellenarGridTransporte();
            }
            else
            {
                lblTituloError.Text = "Modificar códigos transporte";
                lblMensajeError.Text = "Debe indicar el código de transporte.";
                mpeError.Show();
            }
        }

        protected void grvTransporteSubfamilias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvTransporteSubfamilias.EditIndex = -1;
            rellenarGridTransporteCodigos();
        }

        protected void grvTransporteSubfamilias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grvTransporteSubfamilias.DataKeys[e.RowIndex].Values["CFGTCO_ID"].ToString();

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteSubfamiliaEliminar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@CFGTCO_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            rellenarGridTransporteCodigos();
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

            cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroEmpresaTransporte");
            this.rellenarFiltroEmpresaTransporte(cmbFiltro);

            cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroDelegacionTransporte");
            this.rellenarFiltroDelegacionTransporte(cmbFiltro);

            cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroDesdeTransporte");
            this.rellenarFiltroDesdeTransporte(cmbFiltro);

            cmbFiltro = (DropDownList)grvTransporte.HeaderRow.FindControl("FiltroDistanciaTransporte");
            this.rellenarFiltroDistanciaTransporte(cmbFiltro);
        }

        protected void grvTransporte_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvTransporte.EditIndex = e.NewEditIndex;
            rellenarGridTransporte();
        }

        protected void grvTransporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cmbBU = (e.Row.FindControl("cmbBU") as DropDownList);

                cmbBU.DataSource = null;
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_BUs";
                SqlDataAdapter adaptadorBU = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
                System.Data.DataTable dtBU = new System.Data.DataTable();
                adaptadorBU.Fill(dtBU);
                cmbBU.DataSource = dtBU;
                cmbBU.DataTextField = "BU";
                cmbBU.DataValueField = "BU";
                cmbBU.DataBind();
                conexiones.conexion.Close();

                //cmbBU.Items.Insert(0, new ListItem(""));
                string BU = (e.Row.FindControl("lblBU") as Label).Text;
                cmbBU.Items.FindByValue(BU).Selected = true;
                cmbBU.Enabled = e.Row.RowIndex == grvTransporte.EditIndex;

                DropDownList cmbEmpresa = (e.Row.FindControl("cmbEmpresa") as DropDownList);

                cmbEmpresa.DataSource = null;
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Empresas";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroBU = new SqlParameter("@BU", SqlDbType.VarChar, 10);
                parametroBU.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroBU);
                SqlDataAdapter adaptadorEmpresa = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dtEmpresa = new System.Data.DataTable();
                adaptadorEmpresa.Fill(dtEmpresa);
                cmbEmpresa.DataSource = dtEmpresa;
                cmbEmpresa.DataTextField = "Empresa";
                cmbEmpresa.DataValueField = "Empresa";
                cmbEmpresa.DataBind();
                conexiones.conexion.Close();
                //cmbEmpresa.Items.Insert(0, new ListItem(""));
                string empresa = (e.Row.FindControl("lblEmpresa") as Label).Text;
                cmbEmpresa.Items.FindByValue(empresa).Selected = true;
                cmbEmpresa.Enabled = e.Row.RowIndex == grvTransporte.EditIndex;

                DropDownList cmbDelegacion = (e.Row.FindControl("cmbDelegacion") as DropDownList);

                cmbEmpresa.DataSource = null;
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "sp_ROP_Delegaciones";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                SqlParameter parametroEmpresa = new SqlParameter("@empresa", SqlDbType.VarChar, 5);
                parametroEmpresa.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroEmpresa);
                SqlDataAdapter adaptadorDelegacion = new SqlDataAdapter(conexiones.comando);
                System.Data.DataTable dtDelegacion = new System.Data.DataTable();
                adaptadorDelegacion.Fill(dtDelegacion);
                cmbDelegacion.DataSource = dtDelegacion;
                cmbDelegacion.DataTextField = "Delegacion";
                cmbDelegacion.DataValueField = "Delegacion";
                cmbDelegacion.DataBind();
                adaptadorDelegacion.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                //cmbDelegacion.Items.Insert(0, new ListItem(""));
                string delegacion = (e.Row.FindControl("lblDelegacion") as Label).Text;
                cmbDelegacion.Items.FindByValue(delegacion).Selected = true;
                cmbDelegacion.Enabled = e.Row.RowIndex == grvTransporte.EditIndex;

                DropDownList cmbDistancia = (e.Row.FindControl("cmbDistancia") as DropDownList);

                cmbDistancia.DataSource = null;
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_transporteConsulta";
                SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                adaptador.Fill(dt);
                cmbDistancia.DataSource = dt;
                cmbDistancia.DataTextField = "CFGTRA_Distancia";
                cmbDistancia.DataValueField = "CFGTRA_Distancia";
                cmbDistancia.DataBind();
                conexiones.conexion.Close();
                cmbDistancia.Items.Insert(0, new ListItem(""));
                string tipo = (e.Row.FindControl("lblDistancia") as Label).Text;
                cmbDistancia.Items.FindByValue(tipo).Selected = true;
                cmbDistancia.Enabled = e.Row.RowIndex == grvTransporte.EditIndex;

                if (e.Row.Cells[COLGRID_Desvio].Text != "" && txtDesvio.Text != "")
                    if (Convert.ToDecimal(e.Row.Cells[COLGRID_Desvio].Text) > Convert.ToDecimal(txtDesvio.Text))
                        e.Row.Cells[COLGRID_Desvio].BackColor = System.Drawing.Color.LightCoral;

                ((BoundField)grvTransporte.Columns[COLGRID_Prop]).ReadOnly = true;
                ((BoundField)grvTransporte.Columns[COLGRID_Desvio]).ReadOnly = true;
            }
        }

        protected void grvTransporte_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = ((System.Web.UI.WebControls.TextBox)grvTransporte.Rows[e.RowIndex].Cells[COLGRID_CFGTRA_ID].Controls[0]).Text;
            DropDownList cmbBU = grvTransporteSubfamilias.Rows[e.RowIndex].FindControl("cmbBU") as DropDownList;
            string BU = Convert.ToString(cmbBU.SelectedValue);
            DropDownList cmbEmpresa = grvTransporte.Rows[e.RowIndex].FindControl("cmbEmpresa") as DropDownList;
            string Empresa = Convert.ToString(cmbEmpresa.SelectedValue);
            DropDownList cmbDelegacion = grvTransporte.Rows[e.RowIndex].FindControl("cmbDelegacion") as DropDownList;
            string Delegacion = Convert.ToString(cmbDelegacion.SelectedValue);
            string Margen = ((System.Web.UI.WebControls.TextBox)grvTransporte.Rows[e.RowIndex].Cells[COLGRID_Margen].Controls[0]).Text;
            string Desde = ((System.Web.UI.WebControls.TextBox)grvTransporte.Rows[e.RowIndex].FindControl("txtDesde")).Text;
            string Hasta = ((System.Web.UI.WebControls.TextBox)grvTransporte.Rows[e.RowIndex].FindControl("txtHasta")).Text;
            DropDownList cmbDistancia = grvTransporte.Rows[e.RowIndex].FindControl("cmbDistancia") as DropDownList;
            string Distancia = Convert.ToString(cmbDistancia.SelectedValue);
            string Valor = ((System.Web.UI.WebControls.TextBox)grvTransporte.Rows[e.RowIndex].Cells[COLGRID_Valor].Controls[0]).Text;
          
            if (Empresa != "" && Margen != "" && Valor != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionTransporteModificar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroID= new SqlParameter("@CFGTRA_ID", SqlDbType.Int);
                parametroID.Value = Convert.ToInt32(ID);
                conexiones.comando.Parameters.Add(parametroID);
                SqlParameter parametroBU = new SqlParameter("@BU", SqlDbType.VarChar, 10);
                if (Empresa != "")
                    parametroBU.Value = BU;
                else
                    parametroBU.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroBU);
                SqlParameter parametroEmpresa = new SqlParameter("@Empresa", SqlDbType.VarChar, 10);
                if (Empresa != "")
                    parametroEmpresa.Value = Empresa;
                else
                    parametroEmpresa.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroEmpresa);
                SqlParameter parametroDelegacion = new SqlParameter("@Delegacion", SqlDbType.VarChar, 10);
                if (Delegacion != "")
                    parametroDelegacion.Value = Delegacion;
                else
                    parametroDelegacion.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroDelegacion);
                SqlParameter parametroMargen = new SqlParameter("@Margen", SqlDbType.Decimal);
                if (Margen != "")
                    parametroMargen.Value = Convert.ToDecimal(Margen);
                else
                    parametroMargen.Value = DBNull.Value;
                parametroMargen.Precision = 18;
                parametroMargen.Scale = 2;
                conexiones.comando.Parameters.Add(parametroMargen);
                SqlParameter parametroDesde = new SqlParameter("@Desde", SqlDbType.DateTime);
                if (Desde != "")
                    parametroDesde.Value = Convert.ToDateTime(Desde);
                else
                    parametroDesde.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroDesde);
                SqlParameter parametroHasta = new SqlParameter("@Hasta", SqlDbType.DateTime);
                if (Hasta!= "")
                    parametroHasta.Value = Convert.ToDateTime(Hasta);
                else
                    parametroHasta.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroHasta);
                SqlParameter parametroDistancia = new SqlParameter("@Distancia", SqlDbType.VarChar, 10);
                if (Distancia != "")
                    parametroDistancia.Value = Distancia;
                else
                    parametroDistancia.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroDistancia);
                SqlParameter parametroValor = new SqlParameter("@Valor", SqlDbType.Decimal);
                if (Valor != "")
                    parametroValor.Value = Convert.ToDecimal(Valor);
                else
                    parametroValor.Value = DBNull.Value;
                parametroValor.Precision = 36;
                parametroValor.Scale = 18;
                conexiones.comando.Parameters.Add(parametroValor);
                conexiones.comando.ExecuteNonQuery();
                conexiones.conexion.Close();
    
                grvTransporte.EditIndex = -1;
                rellenarGridTransporte();
            }
            else
            {
                lblTituloError.Text = "Modificar transporte";
                lblMensajeError.Text = "Debe indicar la empresa/margen/valor y la distancia.";
                mpeError.Show();
            }
        }

        protected void grvTransporte_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvTransporte.EditIndex = -1;
            rellenarGridTransporte();
        }

        protected void grvTransporte_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grvTransporte.DataKeys[e.RowIndex].Values["CFGTRA_ID"].ToString();

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionTransporteEliminar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@CFGTRA_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            rellenarGridTransporte();

            //lblTituloError.Text = "Eliminar transporte";
            //lblMensajeError.Text = "Se eliminará el registro indicado. Es posible que se actualice algún registro con los datos actuales.";
            //mpeError.Show();
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
            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 10);
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

                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();
            }
            else
            {
                lblTituloError.Text = "Agregar usuario";
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

        private void rellenarGridUsuarios()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioConsulta";
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

        protected void grvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = grvUsuarios.DataKeys[e.RowIndex].Values["USR_ID"].ToString();
            string usuarioRed = grvUsuarios.DataKeys[e.RowIndex].Values["USR_UsuarioRed"].ToString(); 
            if (Environment.UserName.ToUpper() == usuarioRed.ToUpper())
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('No puede eliminar su propio usuario.');", true);
                //MessageBox.Show("No puede eliminar su propio usuario.", "Eliminar usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblTituloError.Text = "Eliminar usuario";
                lblMensajeError.Text = "No puede eliminar su propio usuario.";
                mpeError.Show();
                return;
            }

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionUsuarioEliminar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametroID = new SqlParameter("@USR_ID", SqlDbType.Int);
            parametroID.Value = Convert.ToInt32(ID);
            conexiones.comando.Parameters.Add(parametroID);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            conexiones.conexion.Close();

            rellenarGridUsuarios();
        }

        #endregion

        #region "Paneles"

        //private void rellenarComboPaneles()
        //{
        //    cmbPaneles.Items.Clear();
        //    cmbPaneles.Items.Add("");

        //    conexiones.crearConexion();
        //    conexiones.consulta = "sp_ROP_DatosPanelesCombo";
        //    conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
        //    conexiones.comando.CommandType = CommandType.StoredProcmbPanelescedure;
        //    SqlDataReader dr = conexiones.comando.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            cmbPaneles.Items.Add(dr.GetString(0));
        //        }
        //    }
        //    conexiones.conexion.Close();
        //    cmbPaneles.Text = "";
        //}

        protected void btnIncluirPanel_Click(object sender, EventArgs e)
        {
            if (txtPanel.Text != "")
            {
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_DatosPanelesActualizar";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parametroID = new SqlParameter("@ItemIdAsset", SqlDbType.VarChar, 70);
                parametroID.Value = txtPanel.Text;
                conexiones.comando.Parameters.Add(parametroID);
                SqlParameter parametroEstandar = new SqlParameter("@Estandar", SqlDbType.Bit);
                parametroEstandar.Value = true;
                conexiones.comando.Parameters.Add(parametroEstandar);
                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();

                rellenarGridPaneles();
                //rellenarComboPaneles();
            }
            txtPanel.Text = "";
        }

        private void rellenarGridPaneles()
        {
            conexiones.crearConexion();
            conexiones.consulta = "ROP_DatosPaneles";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvPaneles.DataSource = dr;
            grvPaneles.DataBind();
            conexiones.conexion.Close();
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((BoundField)grvPaneles.Columns[COLGRID_PANEL_ID]).ReadOnly = true;
                ((BoundField)grvPaneles.Columns[COLGRID_PANEL_Descripcion]).ReadOnly = true;

                if (e.Row.RowIndex==0)
                    e.Row.Cells[4].Controls.Clear();
            }
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

            SqlParameter parametroVersion = new SqlParameter("@version", SqlDbType.NVarChar, 10);
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
      
    }
}