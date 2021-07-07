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
        int COLGRID_USR_Eliminar = 5;
        int COLGRID_USR_btnEditar = 6;
        int COLGRID_USR_btnEliminar = 7;

        int COLGRID_MOV_ID = 0;
        int COLGRID_MOV_Signo = 3;
        int COLGRID_MOV_Dias = 4;

        // EXCELS
        int COL_Version = 0;
        int COL_Desde = 1;
        int COL_Hasta= 2;
        int COL_Grupo= 3;
        int COL_Subgrupo= 4;
        int COL_Concepto= 5;
        int COL_Empresa = 6;
        int COL_Familia = 7;
        int COL_Subfamilia = 8;
        int COL_Articulo = 9;
        int COL_Valor = 10;

        int COL_Version_ID = 0;
        int COL_Version_Version = 1;
        int COL_Version_Prueba = 2;
        int COL_Version_Desde = 3;
        int COL_Version_Hasta = 4;
        int COL_Version_Grupo = 5;
        int COL_Version_Subgrupo = 6;
        int COL_Version_Concepto = 7;
        int COL_Version_Empresa = 8;
        int COL_Version_Familia = 9;
        int COL_Version_Subfamilia = 10;
        int COL_Version_Articulo = 11;
        int COL_Version_Valor = 12;

        int COL_GeneralID = 0;
        int COL_GeneralPrueba = 1;
        int COL_GeneralVersion = 2;
        int COL_GeneralDesde = 3;
        int COL_GeneralHasta = 4;
        int COL_GeneralConcepto = 5;
        int COL_GeneralEmpresa = 6;
        int COL_GeneralValor = 7;

        int COL_FicheroGeneralConcepto = 1;
        int COL_FicheroGeneralEmpresa = 2;
        int COL_FicheroGeneralValor = 3;

        int FILA_GENERAL_TITULO = 1;
        int FILA_GENERAL_IDVERSION = 3;
        int FILA_GENERAL_VERSION = 4;
        int FILA_GENERAL_FECHA_DESDE = 5;
        int FILA_GENERAL_FECHA_HASTA = 6;
        int FILA_GENERAL_DATOS = 9;

        // Usuarios
        int CAMPO_USR_ID = 0;
        int CAMPO_USR_UsuarioRed = 1;
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
                txtUsuarioRed.Text = "";
                chkVisualizar.Checked = true;
                chkExportar.Checked = false;
                chkImportar.Checked = false;
                chkEliminar.Checked = false;
                ViewState["FiltroVersion"] = "";
                ViewState["FiltroConcepto"] = "";
                ViewState["FiltroVersionGeneral"] = "";
                ViewState["FiltroConceptoGeneral"] = "";
                ViewState["FiltroEmpresaGeneral"] = "";
                ViewState["FiltroMovimiento"] = "";
                rdbOperativoGeneral.Checked = true;
                rdbOperativo.Checked = true;
                rellenarGridGeneral();
                rellenarCombosVersionGeneral();
                rellenarGrid();
                rellenarCombosVersion();
                rellenarGridUsuarios();
                rellenarDatosFijos();
                validarAccionUsuario();
                rellenarAjustesFechaMovimientos();
                rdbGFV.Checked = true;
                rellenarGridHistorico();
                btnAbrirExcelGeneral.Visible = false;
                btnAbrirExcel.Visible = false;
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
                cmbVersionGeneralExportar.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                btnExcelGeneral.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                btnAbrirExcelGeneral.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                rdbOperativoGeneral.Visible= dr.GetBoolean(CAMPO_USR_Importar);
                rdbPruebaGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                ficheroSeleccionadoGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                btnImportarExcelGeneral.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                cmbVersionGeneralEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                btnExcelGeneralEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                cmbVersionGeneralPruebas.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                btnVersionGeneralReal.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);

                cmbVersionExportar.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                btnExcel.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                btnAbrirExcel.Visible = dr.GetBoolean(CAMPO_USR_Exportar);
                rdbOperativo.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                rdbPrueba.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                ficheroSeleccionado.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                btnImportarExcel.Visible = dr.GetBoolean(CAMPO_USR_Importar);
                cmbVersionEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                btnExcelEliminar.Visible = dr.GetBoolean(CAMPO_USR_Eliminar);
                cmbVersionPruebas.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                btnVersionReal.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);

                btnLimpiarUsuario.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                btnAgregarUsuario.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                lblUsuario.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar); 
                txtUsuarioRed.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                lblVisualizar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                chkVisualizar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                lblExportar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                chkExportar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                lblImportar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                chkImportar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                lblEliminar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                chkEliminar.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);

                btnEditarFijo.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                btnGuardarFijo.Visible = false;
                btnCancelarFijo.Visible = false;
                
                foreach (GridViewRow myRow in grvUsuarios.Rows)
                {
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_USR_btnEditar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                    imgBoton = (ImageButton)myRow.Cells[COLGRID_USR_btnEliminar].Controls[0];
                    if (imgBoton != null)
                    {
                        imgBoton.Visible = !dr.GetBoolean(CAMPO_USR_Visualizar);
                    }
                }
                conexiones.conexion.Close();
            }
            else 
            {
                cmbVersionGeneralExportar.Visible = false;
                btnExcelGeneral.Visible = false;
                btnAbrirExcelGeneral.Visible = false;
                rdbOperativoGeneral.Visible = false;
                rdbPruebaGeneral.Visible = false;
                ficheroSeleccionadoGeneral.Visible = false;
                btnImportarExcelGeneral.Visible = false;
                cmbVersionGeneralEliminar.Visible = false;
                btnExcelGeneralEliminar.Visible = false;
                cmbVersionGeneralPruebas.Visible = false;
                btnVersionGeneralReal.Visible = false;

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

                btnEditarFijo.Visible = false;
                btnGuardarFijo.Visible = false;
                btnCancelarFijo.Visible = false;

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
        protected void btnExportarGeneral_Click(object sender, EventArgs e)
        {
            if (cmbVersionGeneralExportar.Text == "")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar el tipo de fichero a exportar.');", true);
                lblTituloError.Text = "Exportar excel GFV";
                lblMensajeError.Text = "Debe indicar el tipo de fichero a exportar.";
                mpeError.Show();
                return;
            }
            if (cmbVersionGeneralExportar.Text == "TODOS" || cmbVersionGeneralExportar.Text == "TODOS Reales" || cmbVersionGeneralExportar.Text == "TODOS Pruebas")
            {
                nombreInforme = Server.MapPath("~/Ficheros excel/ConfiguracionGFV_TODO_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                exportarExcelGeneralTodo();
            }
            else
            {
                nombreInforme = Server.MapPath("~/Ficheros excel/ConfiguracionGFV_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                exportarExcelGeneral();
            }
            //MessageBox.Show("Fichero excel generado.", "Exportar GFV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Fichero excel generado.');", true);
            lblTituloInformacion.Text = "Exportar GFV";
            lblMensajeInformacion.Text = "Fichero excel generado.";
            mpeInformacion.Show();
        }

        protected void btnAbrirExcelGeneral_Click(object sender, EventArgs e)
        {
            if (cmbVersionGeneralExportar.Text == "TODOS" || cmbVersionGeneralExportar.Text == "TODOS Reales" || cmbVersionGeneralExportar.Text == "TODOS Pruebas")
                    Context.Response.Redirect("Ficheros excel/ConfiguracionGFV_TODO_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
            else
                    Context.Response.Redirect("Ficheros excel/ConfiguracionGFV_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
            btnAbrirExcelGeneral.Visible = false;
        }

        protected void btnEliminarGeneral_Click(object sender, EventArgs e)
        {
            if (cmbVersionGeneralEliminar.Text == "")
            {
                //MessageBox.Show("Debe indicar la versión a eliminar.", "Eliminar GFV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar la versión a eliminar.');", true);
                lblTituloError.Text = "Eliminar versión GFV";
                lblMensajeError.Text = "Debe indicar la versión a eliminar.";
                mpeError.Show();
                return;
            }
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralEliminar";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@CGE_Version", cmbVersionGeneralEliminar.Text);
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            rellenarGridGeneral();
            rellenarCombosVersionGeneral();

            lblTituloError.Text = "Eliminar GFV";
            lblMensajeError.Text = "Versión GFV eliminada.";
            mpeError.Show();
        }

        protected void btnRealGeneral_Click(object sender, EventArgs e)
        {
            if (cmbVersionGeneralPruebas.Text == "")
            {
                //MessageBox.Show("Debe indicar la versión a pasar a real.", "Pasar GFV a real", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar la versión a pasar a real.');", true);
                lblTituloError.Text = "Pasar GFV a real";
                lblMensajeError.Text = "Debe indicar la versión a pasar a real.";
                mpeError.Show();
                return;
            }
            else
            {
                txtObservaciones.Text = "";
                mpePruebaReal.Show();
            }
        }

        protected void btnOkPruebaReal_Click(object sender, EventArgs e)
        {
            string observaciones = txtObservaciones.Text;
            lblTituloInformacion.Text = "Observaciones";
            lblMensajeInformacion.Text = observaciones;
            mpeInformacion.Show();
        }

        protected void btnSubirExcelGeneral_Click(object sender, EventArgs e)
        {
            string directorio;
            String fichero;

            directorio = Server.MapPath("~/Ficheros excel/");

            if (ficheroSeleccionadoGeneral.HasFile)
            {
                fichero = ficheroSeleccionadoGeneral.FileName;
                directorio += fichero;
                ficheroSeleccionadoGeneral.SaveAs(directorio);
                procesarExcelGeneral(directorio);
            }
            else
            {
                lblTituloError.Text = "Importar fichero excel GFV";
                lblMensajeError.Text = "Debe indicar el fichero a importar.";
                mpeError.Show();
            }
        }

        protected void CambioFiltroVersionGeneral(object sender, EventArgs e)
        {
            DropDownList cmbFiltroVersion = (DropDownList)sender;
            ViewState["FiltroVersionGeneral"] = cmbFiltroVersion.SelectedValue;
            this.rellenarGridGeneral();
        }

        private void rellenarCombosVersionGeneral()
        {
            // para importar: reales y pruebas
            cmbVersionGeneralExportar.Items.Clear();
            cmbVersionGeneralExportar.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralVersionConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.Parameters.AddWithValue("@selector", 1);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cmbVersionGeneralExportar.Items.Add(dr.GetString(0));
                }
            }
            conexiones.conexion.Close();
            cmbVersionGeneralExportar.Text = "TODOS";

            // para eliminar: a futuro y pruebas
            cmbVersionGeneralEliminar.Items.Clear();
            cmbVersionGeneralEliminar.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralVersionEliminarConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader drEliminar = conexiones.comando.ExecuteReader();
            if (drEliminar.HasRows)
            {
                while (drEliminar.Read())
                {
                    cmbVersionGeneralEliminar.Items.Add(drEliminar.GetString(0));
                }
            }
            conexiones.conexion.Close();
            cmbVersionGeneralEliminar.Text = "";

            // para pasar a real: solo pruebas
            cmbVersionGeneralPruebas.Items.Clear();
            cmbVersionGeneralPruebas.Items.Add("");

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralVersionPruebasConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader drPruebas= conexiones.comando.ExecuteReader();
            if (drPruebas.HasRows)
            {
                while (drPruebas.Read())
                {
                    cmbVersionGeneralPruebas.Items.Add(drPruebas.GetString(0));
                }
            }
            conexiones.conexion.Close();
            cmbVersionGeneralPruebas.Text = "";
            
        }
            
        private void rellenarFiltroVersionGeneral(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralVersionConsulta";
            //conexiones.comando.Parameters.AddWithValue("@selector", 0);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dt = new System.Data.DataTable();
            adaptador.Fill(dt);
            cmbFiltroVersion.DataSource = dt;
            cmbFiltroVersion.DataTextField = "CGE_Version";
            cmbFiltroVersion.DataValueField = "CGE_Version";
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
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralConceptoConsulta";
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
        }

        private void procesarExcelGeneral(string fichero)
        {
            int fila;
            string version;
            DateTime fechaDesde;
            DateTime fechaHasta;

            SqlParameter parametroVersion;
            SqlParameter parametroPrueba;
            SqlParameter parametroObservaciones;
            SqlParameter parametroFechaDesde;
            SqlParameter parametroFechaHasta;
            SqlParameter parametroConcepto;
            SqlParameter parametroEmpresa;
            SqlParameter parametroValor;

            SLDocument sl = new SLDocument(fichero, "Configuracion general");
         
            if (sl.GetCellValueAsString(FILA_GENERAL_TITULO, 1).ToString().ToUpper() != "CONFIGURACIÓN GENERAL")
            {
                //MessageBox.Show("El fichero no parece tener el formato correcto." + Environment.NewLine + "Por favor, verifique el fichero e intente procesarlo de nuevo.", "Configuración parámetros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('El fichero no parece tener el formato correcto. Por favor, verifique el fichero e intente procesarlo de nuevo.');", true);
                lblTituloError.Text = "Configuración parámetros";
                lblMensajeError.Text = "El fichero no parece tener el formato correcto."+ "<br /> &nbsp;" + "Por favor, verifique el fichero e intente procesarlo de nuevo.";
                mpeError.Show();
                return;
            }

            // Validar valores cabecera 
            if ((sl.GetCellValueAsString(FILA_GENERAL_VERSION, 2).Length == 0) || (sl.GetCellValueAsString(FILA_GENERAL_FECHA_DESDE, 2).Length == 0))
            {
                //MessageBox.Show("Debe indicar la versión, y la fecha desde de la misma." + Environment.NewLine + "Por favor, verifique el fichero e intente procesarlo de nuevo", "Configuración parámetros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar la versión, y la fecha desde de la misma. Por favor, verifique el fichero e intente procesarlo de nuevo.');", true);
                lblTituloError.Text = "Configuración parámetros";
                lblMensajeError.Text = "Debe indicar la versión, y la fecha desde de la misma." + "<br /> &nbsp;" + "Por favor, verifique el fichero e intente procesarlo de nuevo.";
                mpeError.Show();
                return;
            }

            version = sl.GetCellValueAsString(FILA_GENERAL_VERSION, 2);
            fechaDesde = sl.GetCellValueAsDateTime(FILA_GENERAL_FECHA_DESDE, 2);
            fechaHasta = sl.GetCellValueAsDateTime(FILA_GENERAL_FECHA_HASTA, 2);
            
            // Validar versión y fechas
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionGeneralValidar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

            parametroVersion = new SqlParameter("@CGE_Version", SqlDbType.VarChar, 10);
            parametroVersion.Value = version;
            conexiones.comando.Parameters.Add(parametroVersion);
            parametroFechaDesde = new SqlParameter("@CGE_FechaDesde", SqlDbType.DateTime);
            if (sl.GetCellValueAsString(FILA_GENERAL_FECHA_DESDE, 2).Length == 0 || rdbPruebaGeneral.Checked)
                parametroFechaDesde.Value = DBNull.Value;
            else
                parametroFechaDesde.Value = fechaDesde;
            conexiones.comando.Parameters.Add(parametroFechaDesde);
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (Convert.ToString(dr["CGE_Version"]).Length > 0)
                {
                    lblMensajeError.Text = "La versión indicada ya existe." + "<br /> &nbsp;" + "Por favor, indique un nuevo número de versión e intente procesar el fichero de nuevo.";
                    mpeError.Show();
                    return;
                }
                if (Convert.ToString(dr["CGE_FechaDesde"]).Length > 0)
                {
                    lblMensajeError.Text = "La versión indicada tiene una fecha que coincide con otra versión." + "<br /> &nbsp;" + "Por favor, indique otra fecha e intente procesar el fichero de nuevo.";
                    mpeError.Show();
                    return;
                }
            }

            conexiones.crearConexion();
            fila = FILA_GENERAL_DATOS;
            while (sl.GetCellValueAsString(fila, 1) != "")
            {
                conexiones.consulta = "sp_ROP_ConfiguracionGeneralIncluir";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;

                parametroVersion = new SqlParameter("@CGE_Version", SqlDbType.VarChar, 10);
                if (version.Length == 0)
                    parametroVersion.Value = DBNull.Value;
                else
                    parametroVersion.Value = version;
                conexiones.comando.Parameters.Add(parametroVersion);

                parametroPrueba = new SqlParameter("@CGE_VersionPrueba", SqlDbType.Bit);
                parametroPrueba.Value = rdbPruebaGeneral.Checked;
                conexiones.comando.Parameters.Add(parametroPrueba);

                parametroObservaciones = new SqlParameter("@CGE_Observaciones", SqlDbType.VarChar,4000);
                parametroObservaciones.Value = DBNull.Value;
                conexiones.comando.Parameters.Add(parametroObservaciones);

                parametroFechaDesde = new SqlParameter("@CGE_FechaDesde", SqlDbType.DateTime);
                if (sl.GetCellValueAsString(FILA_GENERAL_FECHA_DESDE, 2).Length == 0 || rdbPruebaGeneral.Checked)
                    parametroFechaDesde.Value = DBNull.Value;
                else
                    parametroFechaDesde.Value = fechaDesde;
                conexiones.comando.Parameters.Add(parametroFechaDesde);

                parametroFechaHasta= new SqlParameter("@CGE_FechaHasta", SqlDbType.DateTime);
                if (sl.GetCellValueAsString(FILA_GENERAL_FECHA_HASTA, 2).Length == 0 || rdbPruebaGeneral.Checked)
                    parametroFechaHasta.Value = DBNull.Value;
                else
                    parametroFechaHasta.Value = fechaHasta;
                conexiones.comando.Parameters.Add(parametroFechaHasta);

                parametroConcepto = new SqlParameter("@CGE_Concepto", SqlDbType.VarChar,100);
                if (sl.GetCellValueAsString(fila, COL_FicheroGeneralConcepto).Length == 0)
                    parametroConcepto.Value = DBNull.Value;
                else
                    parametroConcepto.Value = sl.GetCellValueAsString(fila, COL_FicheroGeneralConcepto);
                conexiones.comando.Parameters.Add(parametroConcepto);

                parametroEmpresa= new SqlParameter("@CGE_Empresa", SqlDbType.VarChar,5);
                if (sl.GetCellValueAsString(fila, COL_FicheroGeneralEmpresa).Length == 0)
                    parametroEmpresa.Value = DBNull.Value;
                else
                    parametroEmpresa.Value = sl.GetCellValueAsString(fila, COL_FicheroGeneralEmpresa);
                conexiones.comando.Parameters.Add(parametroEmpresa);

                parametroValor= new SqlParameter("@CGE_Valor", SqlDbType.Decimal);
                parametroValor.Precision = 18;
                parametroValor.Scale = 2;
                if (sl.GetCellValueAsString(fila, COL_FicheroGeneralValor).ToString().Length == 0)
                    parametroValor.Value = DBNull.Value;
                else
                    parametroValor.Value = sl.GetCellValueAsDecimal(fila, COL_FicheroGeneralValor);
                conexiones.comando.Parameters.Add(parametroValor);

                conexiones.comando.ExecuteNonQuery();

                fila = fila + 1;
            }
            conexiones.conexion.Close();
            //MessageBox.Show("Fichero procesado", "Agregar versión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Fichero procesado.');", true);
            lblTituloInformacion.Text = "Agregar versión";
            lblMensajeInformacion.Text = "Fichero procesado.";
            mpeInformacion.Show();

            rellenarGridGeneral();
            rellenarCombosVersionGeneral();
        }

        private void exportarExcelGeneral()
        {
            SLStyle style;
            SLDataValidation dv;
            int fila = 3;
            int columna = 0;
            int cantidadEmpresas = 0;
            bool encabezado = false;
          
            btnAbrirExcelGeneral.Visible = false;
            // Consulta SQL
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@version", cmbVersionGeneralExportar.Text);
            conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            // Crear el excel
            SLDocument sl = new SLDocument();

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Configuracion general");
           
            // Hojas ocultas
            sl.AddWorksheet("Empresas");
            conexiones.consulta = "sp_ROP_EmpresasListado";
            SqlDataAdapter adaptadorEmpresa = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
            System.Data.DataTable dtEmpresas = new System.Data.DataTable();
            adaptadorEmpresa.Fill(dtEmpresas);
            sl.ImportDataTable(1, 1, dtEmpresas, false);
            sl.HideWorksheet("Empresas");
            cantidadEmpresas = dtEmpresas.Rows.Count;

            sl.SelectWorksheet("Configuracion general");
            sl.SetCellValue(1, 1, "CONFIGURACIÓN GENERAL");

            encabezado = false;
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                if (!encabezado)
                {
                    // VERSIÓN
                    sl.SetCellValue(FILA_GENERAL_IDVERSION, 1, "ID");
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 12);
                    style.Font.Bold = true;
                    sl.SetCellStyle(FILA_GENERAL_IDVERSION, 1, style);

                    sl.SetCellValue(FILA_GENERAL_IDVERSION, 2, Row.ItemArray[COL_GeneralID].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    sl.SetCellStyle(FILA_GENERAL_IDVERSION, 2, style);

                    sl.SetCellValue(FILA_GENERAL_VERSION, 1, "Versión");
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 12);
                    style.Font.Bold = true;
                    sl.SetCellStyle(FILA_GENERAL_VERSION, 1, style);

                    sl.SetCellValue(FILA_GENERAL_VERSION, 2, Row.ItemArray[COL_GeneralVersion].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    sl.SetCellStyle(FILA_GENERAL_VERSION, 2, style);

                    // FECHA DESDE 
                    sl.SetCellValue(FILA_GENERAL_FECHA_DESDE, 1, "Desde");
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 12);
                    style.Font.Bold = true;
                    style.FormatCode = "dd-MM-yyyy";
                    sl.SetCellStyle(FILA_GENERAL_FECHA_DESDE, 1, style);

                    sl.SetCellValue(FILA_GENERAL_FECHA_DESDE, 2, Row.ItemArray[COL_GeneralDesde].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    sl.SetCellStyle(FILA_GENERAL_FECHA_DESDE, 2, style);

                    // FECHA HASTA
                    sl.SetCellValue(FILA_GENERAL_FECHA_HASTA, 1, "Hasta");
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 12);
                    style.Font.Bold = true;
                    style.FormatCode = "dd-MM-yyyy";
                    sl.SetCellStyle(FILA_GENERAL_FECHA_HASTA, 1, style);

                    sl.SetCellValue(FILA_GENERAL_FECHA_HASTA, 2, Row.ItemArray[COL_GeneralHasta].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    sl.SetCellStyle(FILA_GENERAL_FECHA_HASTA, 2, style);

                    sl.AutoFitColumn(1, 2);

                    sl.MergeWorksheetCells("A1", "C1");
                    style = sl.CreateStyle();
                    style.Font.Bold = true;
                    style.Font.Italic = true;
                    style.SetFont("Verdana", 12);
                    sl.SetCellStyle(1, 1, style);

                    // Crear el encabezado del informe
                    sl.SetCellValue(FILA_GENERAL_DATOS-1, 1, "Concepto");
                    sl.SetCellValue(FILA_GENERAL_DATOS-1, 2, "Empresa");
                    sl.SetCellValue(FILA_GENERAL_DATOS-1, 3, "Valor");

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
                        sl.SetCellStyle(FILA_GENERAL_DATOS - 1, columna, style);

                    sl.FreezePanes(FILA_GENERAL_DATOS - 1, 3);
                    fila = FILA_GENERAL_DATOS;

                    encabezado = true;
                }

                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_GeneralConcepto].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_GeneralEmpresa].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_GeneralValor].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 3, style);

                fila++;
            }

            if (fila >= FILA_GENERAL_DATOS)
            {
                sl.Filter("A" +(FILA_GENERAL_DATOS-1).ToString(), "B" + (fila - 1).ToString());
               
                dv = sl.CreateDataValidation("B"+(FILA_GENERAL_DATOS).ToString(), "B1000");
                dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
                sl.AddDataValidation(dv);

                dv = sl.CreateDataValidation("C"+(FILA_GENERAL_DATOS).ToString(), "C1000");
                dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.0, false);
                sl.AddDataValidation(dv);
            }

            sl.SaveAs(nombreInforme);
            btnAbrirExcelGeneral.Visible = true;
        }

        private void exportarExcelGeneralTodo()
        {
            SLStyle style;
            int fila;
            int columna = 0;
 
            btnAbrirExcelGeneral.Visible = false;
            // Consulta SQL
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionGeneralConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            conexiones.comando.Parameters.AddWithValue("@empresa", DBNull.Value);
            if (cmbVersionGeneralExportar.Text == "TODOS")
                conexiones.comando.Parameters.AddWithValue("@tipo", 0);
            else if (cmbVersionGeneralExportar.Text == "TODOS Reales")
                conexiones.comando.Parameters.AddWithValue("@tipo", 1);
            else
                conexiones.comando.Parameters.AddWithValue("@tipo", 2);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            // Crear el excel
            SLDocument sl = new SLDocument();

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Configuracion general");

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "ID");
            sl.SetCellValue(1, 2, "Versión");
            sl.SetCellValue(1, 3, "Desde");
            sl.SetCellValue(1, 4, "Hasta");
            sl.SetCellValue(1, 5, "Concepto");
            sl.SetCellValue(1, 6, "Empresa");
            sl.SetCellValue(1, 7, "Valor");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 20);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 20);
            sl.SetColumnWidth(7, 20);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 7; columna++)
                sl.SetCellStyle(1, columna, style);

            fila = 2;
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_GeneralID].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 1, style);

                sl.SetCellValue(fila, 2, Row.ItemArray[COL_GeneralVersion].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 2, style);

                sl.SetCellValue(fila, 3, Row.ItemArray[COL_GeneralDesde].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_GeneralHasta].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                style.FormatCode = "dd-MM-yyyy";
                sl.SetCellStyle(fila, 3, style);
                sl.SetCellStyle(fila, 4, style);

                sl.SetCellValue(fila, 5, Row.ItemArray[COL_GeneralConcepto].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 5, style);

                sl.SetCellValue(fila, 6, Row.ItemArray[COL_GeneralEmpresa].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 6, style);

                sl.SetCellValue(fila, 7, Row.ItemArray[COL_GeneralValor].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 7, style);
                fila++;
            }

            sl.SaveAs(nombreInforme);
            btnAbrirExcelGeneral.Visible = true;
        }
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
            cmbVersionExportar.Text = "TODOS";

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
                exportarExcelTodo();
            }
            else
            {
                nombreInforme = Server.MapPath("~/Ficheros excel/Configuracion_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                exportarExcel();
            }
            lblTituloInformacion.Text = "Exportar GFV";
            lblMensajeInformacion.Text = "Fichero excel generado.";
            mpeInformacion.Show();
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
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            grvDatos.DataSource = dr;
            grvDatos.DataBind();
            conexiones.conexion.Close();

            cmbFiltro = (DropDownList)grvDatos.HeaderRow.FindControl("FiltroVersion");
            this.rellenarFiltroVersion(cmbFiltro);

            cmbFiltro = (DropDownList)grvDatos.HeaderRow.FindControl("FiltroConcepto");
            this.rellenarFiltroConcepto(cmbFiltro);
        }

        private void exportarExcel()
        {
            SLStyle style;
            SLDataValidation dv;
            string nombreGrupo = "";
            int hojas = 1;
            int fila = 5;
            int columna = 0;
            int cantidadEmpresas = 0;
            int cantidadFamilias = 0;
            int cantidadSubfamilias = 0;
            SqlDataAdapter adaptadorSubgrupos;
            System.Data.DataTable dtSubgrupos;
            int cantidadSubgrupos = 0;
            int columnaSubgrupo = 0;

            btnAbrirExcel.Visible = false;
            // Consulta SQL
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            if (ViewState["FiltroVersion"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@version", ViewState["FiltroVersion"].ToString());
            if (ViewState["FiltroConcepto"].ToString() == "")
                conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            else
                conexiones.comando.Parameters.AddWithValue("@concepto", ViewState["FiltroConcepto"].ToString());
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            // Crear el excel
            SLDocument sl = new SLDocument();

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Datos Generales");

            hojas = 0;
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                if (nombreGrupo != Row.ItemArray[COL_Grupo].ToString())
                {
                    // Creamos una instancia de la primera hoja de trabajo de excel  
                    if (hojas == 0)
                    {
                        sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Datos Generales");

                        // VERSIÓN
                        sl.SetCellValue(1, 1, "Versión");
                        style = sl.CreateStyle();
                        style.SetFont("Verdana", 12);
                        style.Font.Bold  = true;
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

                        // Hojas ocultas
                        sl.AddWorksheet("Empresas");
                        conexiones.consulta = "sp_ROP_EmpresasListado";
                        SqlDataAdapter adaptadorEmpresa = new SqlDataAdapter(conexiones.consulta, conexiones.conexion);
                        System.Data.DataTable dtEmpresas = new System.Data.DataTable();
                        adaptadorEmpresa.Fill(dtEmpresas);
                        sl.ImportDataTable(1, 1, dtEmpresas, false);
                        sl.HideWorksheet("Empresas");
                        cantidadEmpresas= dtEmpresas.Rows.Count;
                        
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
                        sl.ImportDataTable(1, 1, dtSubfamilias,false);
                        sl.HideWorksheet("Subfamilias");
                        cantidadSubfamilias = dtSubfamilias.Rows.Count;

                        sl.AddWorksheet("Subgrupos");
                        sl.HideWorksheet("Subgrupos");
                    }
                    else
                    {
                        sl.Filter("A4", "F" + (fila - 1).ToString());

                        dv = sl.CreateDataValidation("A5", "A1000");
                        dv.AllowList("'Subgrupos'!$" + letraExcel(columnaSubgrupo-1) + "$1:$" + letraExcel(columnaSubgrupo-1) + "$" + cantidadSubgrupos.ToString(), true, true);
                        sl.AddDataValidation(dv);

                        dv = sl.CreateDataValidation("C5", "C1000");
                        dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
                        sl.AddDataValidation(dv);

                        dv = sl.CreateDataValidation("D5", "D1000");
                        dv.AllowList("'Familias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
                        sl.AddDataValidation(dv);

                        dv = sl.CreateDataValidation("E5", "E1000");
                        dv.AllowList("'Subfamilias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
                        sl.AddDataValidation(dv);

                        dv = sl.CreateDataValidation("G5", "G1000");
                        dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.0, false);
                        sl.AddDataValidation(dv);
                    }

                    // Hoja Subgrupos 
                    columnaSubgrupo = columnaSubgrupo + 1;
                    sl.SelectWorksheet("Subgrupos");
                    conexiones.comando = conexiones.conexion.CreateCommand();
                    conexiones.comando.CommandText = "sp_ROP_ConfiguracionSubconceptosConsulta";
                    conexiones.comando.CommandType = CommandType.StoredProcedure;
                    if (Row.ItemArray[COL_Grupo].ToString() == "")
                        conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
                    else
                        conexiones.comando.Parameters.AddWithValue("@concepto", Row.ItemArray[COL_Grupo].ToString());
                    adaptadorSubgrupos = new SqlDataAdapter(conexiones.comando);
                    dtSubgrupos = new System.Data.DataTable();
                    adaptadorSubgrupos.Fill(dtSubgrupos);
                    sl.ImportDataTable(1, columnaSubgrupo, dtSubgrupos, false);
                    cantidadSubgrupos = dtSubgrupos.Rows.Count;

                    // Cabecera general: Nombre del grupo / hoja
                    if (Row.ItemArray[COL_Grupo].ToString() == "")
                        sl.AddWorksheet("Sin nombre de grupo");
                    else
                        sl.AddWorksheet(Row.ItemArray[COL_Grupo].ToString());

                    if (Row.ItemArray[COL_Grupo].ToString() == "")
                        sl.SetCellValue(1, 1, "Sin nombre de grupo");
                    else
                        sl.SetCellValue(1, 1, Row.ItemArray[COL_Grupo].ToString());
                    
                    sl.MergeWorksheetCells("A1", "H1");
                    style = sl.CreateStyle();
                    style.Font.Bold = true;
                    style.Font.Italic = true;
                    style.SetFont("Verdana", 12);
                    sl.SetCellStyle(1, 1, style);

                    // Cabecera general: Versión
                    sl.SetCellValue(2, 1, "Versión " + Row.ItemArray[COL_Version].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    style.Font.Bold = true;
                    sl.SetCellStyle(2, 1, style);

                    // Cabecera general: Desde
                    sl.SetCellValue(2, 3, "Desde " + Row.ItemArray[COL_Desde].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    style.Font.Bold = true;
                    sl.SetCellStyle(2, 3, style);

                    // Cabecera general: Hasta
                    sl.SetCellValue(2, 5, "Hasta " + Row.ItemArray[COL_Hasta].ToString());
                    style = sl.CreateStyle();
                    style.SetFont("Verdana", 11);
                    style.Font.Bold = true;
                    sl.SetCellStyle(2, 5, style);

                    // Crear el encabezado del informe
                    sl.SetCellValue(4, 1, "Subgrupo");
                    sl.SetCellValue(4, 2, "Concepto");
                    sl.SetCellValue(4, 3, "Empresa");
                    sl.SetCellValue(4, 4, "Familia");
                    sl.SetCellValue(4, 5, "Subfamilia");
                    sl.SetCellValue(4, 6, "Artículo");
                    sl.SetCellValue(4, 7, "Valor");

                    style = sl.CreateStyle();
                    sl.SetColumnWidth(1, 30);
                    sl.SetColumnWidth(2, 30);
                    sl.SetColumnWidth(3, 30);
                    sl.SetColumnWidth(4, 25);
                    sl.SetColumnWidth(5, 25);
                    sl.SetColumnWidth(6, 25);
                    sl.SetColumnWidth(7, 20);
                    sl.SetColumnWidth(8, 20);
                    style.SetFont("Verdana", 10);
                    style.Font.Bold = true;
                    style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
                    style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
                    style.SetFontColor(System.Drawing.Color.White);
                    style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                    for (columna = 1; columna <= 7; columna++)
                        sl.SetCellStyle(4, columna, style);

                    sl.FreezePanes(4, 8);

                    nombreGrupo = Row.ItemArray[COL_Grupo].ToString();
                    hojas++;
                    fila = 5;
                }
                
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Subgrupo].ToString());
                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Concepto].ToString());
                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Empresa].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_Familia].ToString());
                sl.SetCellValue(fila, 5, Row.ItemArray[COL_Subfamilia].ToString());
                sl.SetCellValue(fila, 6, Row.ItemArray[COL_Articulo].ToString());
                sl.SetCellValue(fila, 7, Row.ItemArray[COL_Valor].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 7, style);

                fila++;
            }

            if (hojas != 0)
            {
                sl.Filter("A4", "F" + (fila-1).ToString());

                dv = sl.CreateDataValidation("A5", "A1000");
                dv.AllowList("'Subgrupos'!$"+ letraExcel(columnaSubgrupo-1) + "$1:$" +letraExcel(columnaSubgrupo-1)+ "$" + cantidadSubgrupos.ToString(), true, true);
                sl.AddDataValidation(dv);

                dv = sl.CreateDataValidation("C5", "C1000");
                dv.AllowList("'Empresas'!$A$1:$A$" + cantidadEmpresas.ToString(), true, true);
                sl.AddDataValidation(dv);

                dv = sl.CreateDataValidation("D5", "D1000");
                dv.AllowList("'Familias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
                sl.AddDataValidation(dv);

                dv = sl.CreateDataValidation("E5", "E1000");
                dv.AllowList("'Subfamilias'!$A$1:$A$" + cantidadFamilias.ToString(), true, true);
                sl.AddDataValidation(dv);

                dv = sl.CreateDataValidation("G5", "G1000");
                dv.AllowDecimal(SLDataValidationSingleOperandValues.GreaterThanOrEqual, 0.0, false);
                sl.AddDataValidation(dv);
            }

            sl.SaveAs(nombreInforme);
            btnAbrirExcel.Visible = true;
        }

        private void exportarExcelTodo()
        {
            SLStyle style;
            int fila;
            int columna = 0;

            btnAbrirExcelGeneral.Visible = false;
            // Consulta SQL
            conexiones.crearConexion();
            conexiones.comando = conexiones.conexion.CreateCommand();
            conexiones.comando.CommandText = "sp_ROP_ConfiguracionConsulta";
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@version", DBNull.Value);
            conexiones.comando.Parameters.AddWithValue("@concepto", DBNull.Value);
            if (cmbVersionGeneralExportar.Text == "TODOS")
                conexiones.comando.Parameters.AddWithValue("@tipo", 0);
            else if (cmbVersionGeneralExportar.Text == "TODOS Reales")
                conexiones.comando.Parameters.AddWithValue("@tipo", 1);
            else
                conexiones.comando.Parameters.AddWithValue("@tipo", 2);
            SqlDataAdapter adaptador = new SqlDataAdapter(conexiones.comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            // Crear el excel
            SLDocument sl = new SLDocument();

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Configuracion versión");

            // Crear el encabezado del informe
            sl.SetCellValue(1, 1, "ID");
            sl.SetCellValue(1, 2, "Versión");
            sl.SetCellValue(1, 3, "Desde");
            sl.SetCellValue(1, 4, "Hasta");
            sl.SetCellValue(1, 5, "Actualización");
            sl.SetCellValue(1, 6, "Subgrupo");
            sl.SetCellValue(1, 7, "Concepto");
            sl.SetCellValue(1, 8, "Empresa");
            sl.SetCellValue(1, 9, "Familia");
            sl.SetCellValue(1, 10, "Subfamilia");
            sl.SetCellValue(1, 11, "Artículo");
            sl.SetCellValue(1, 12, "Valor");

            style = sl.CreateStyle();
            sl.SetColumnWidth(1, 20);
            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 20);
            sl.SetColumnWidth(7, 20);
            sl.SetColumnWidth(8, 20);
            sl.SetColumnWidth(9, 20);
            sl.SetColumnWidth(10, 20);
            sl.SetColumnWidth(11, 20);
            sl.SetColumnWidth(12, 20);
            style.SetFont("Verdana", 10);
            style.Font.Bold = true;
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkBlue, System.Drawing.Color.DarkBlue);
            style.SetFontColor(System.Drawing.Color.White);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            for (columna = 1; columna <= 12; columna++)
                sl.SetCellStyle(1, columna, style);

            fila = 2;
            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                // Datos
                sl.SetCellValue(fila, 1, Row.ItemArray[COL_Version_ID].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 1, style);

                sl.SetCellValue(fila, 2, Row.ItemArray[COL_Version_Version].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 2, style);

                sl.SetCellValue(fila, 3, Row.ItemArray[COL_Version_Desde].ToString());
                sl.SetCellValue(fila, 4, Row.ItemArray[COL_Version_Hasta].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                style.FormatCode = "dd-MM-yyyy";
                sl.SetCellStyle(fila, 3, style);
                sl.SetCellStyle(fila, 4, style);

                sl.SetCellValue(fila, 5, Row.ItemArray[COL_Version_Grupo].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 5, style);

                sl.SetCellValue(fila, 6, Row.ItemArray[COL_Version_Subgrupo].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 6, style);

                sl.SetCellValue(fila, 7, Row.ItemArray[COL_Version_Concepto].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 7, style);

                sl.SetCellValue(fila, 8, Row.ItemArray[COL_Version_Empresa].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 8, style);

                sl.SetCellValue(fila, 9, Row.ItemArray[COL_Version_Familia].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 9, style);

                sl.SetCellValue(fila, 10, Row.ItemArray[COL_Version_Subfamilia].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 10, style);

                sl.SetCellValue(fila, 11, Row.ItemArray[COL_Version_Articulo].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle(fila, 11, style);
                
                sl.SetCellValue(fila, 12, Row.ItemArray[COL_Version_Valor].ToString());
                style = sl.CreateStyle();
                style.SetFont("Verdana", 10);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                style.FormatCode = "#,##0.00";
                sl.SetCellStyle(fila, 12, style);
                fila++;
            }

            sl.SaveAs(nombreInforme);
            btnAbrirExcelGeneral.Visible = true;
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

                SqlDataReader dr = conexiones.comando.ExecuteReader();
                conexiones.conexion.Close();
            }
            else
            {
                //MessageBox.Show("Debe indicar el nombre de red del usuario.", "Agregar usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaMensaje", "alert('Debe indicar el nombre de red del usuario.');", true);
                lblTituloError.Text = "Agregar usuario";
                lblMensajeError.Text = "Debe indicar el nombre de red del usuario.";
                mpeError.Show();
            }

            txtUsuarioRed.Text = "";
            chkVisualizar.Checked = false;
            chkExportar.Checked = false;
            chkImportar.Checked = false;
            chkEliminar.Checked = false;
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

        #region "Historico"
        private void rellenarGridHistorico()
        {
            conexiones.crearConexion();
            if (rdbGFV.Checked == true)
                conexiones.consulta = "sp_ROP_ConfiguracionGeneralHistorico";
            else
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

        private void rellenarFiltroAjustesFechaMovimientos(DropDownList cmbFiltroVersion)
        {
            cmbFiltroVersion.DataSource = null;
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_MovimientosAjusteFecha";
            //conexiones.comando.Parameters.AddWithValue("@selector", 0);
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

        private void rellenarAjustesFechaMovimientos()
        {
            DropDownList cmbFiltro;

            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_MovimientosAjusteFechaConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;

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

        private void rellenarDatosFijos()
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionFijaConsulta";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = conexiones.comando.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtDiasCalculo.Text= dr["COF_DiasCalculo"].ToString();
                txtDiasFechaOfertaCapitulo.Text = dr["COF_OfertaDiasEntreFechaOfertaFechaCapitulo"].ToString();
                txtDiasRetrocederOferta.Text = dr["COF_OfertaDiasRestarFechaCapítulo"].ToString();
                txtDiasFechaOfertaPedido.Text = dr["COF_OfertaDiasEntreFechaOfertaFechaPedido"].ToString();
                txtDiasRetrocederPedido.Text = dr["COF_OfertaDiasRestarFechaPedido"].ToString();
            }
            conexiones.conexion.Close();

            txtDiasCalculo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            txtDiasFechaOfertaCapitulo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            txtDiasRetrocederOferta.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            txtDiasFechaOfertaPedido.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            txtDiasRetrocederPedido.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

            txtDiasCalculo.Enabled = false;
            txtDiasFechaOfertaCapitulo.Enabled = false;
            txtDiasRetrocederOferta.Enabled = false;
            txtDiasFechaOfertaPedido.Enabled = false;
            txtDiasRetrocederPedido.Enabled = false;
            btnEditarFijo.Visible = true;
            btnGuardarFijo.Visible = false;
            btnCancelarFijo.Visible = false;
        }

        protected void btnEditarFijo_Click(object sender, EventArgs e)
        {
            txtDiasCalculo.Enabled = true;
            txtDiasFechaOfertaCapitulo.Enabled  = true;
            txtDiasRetrocederOferta.Enabled = true;
            txtDiasFechaOfertaPedido.Enabled = true;
            txtDiasRetrocederPedido.Enabled = true;
            btnEditarFijo.Visible = false;
            btnGuardarFijo.Visible = true;
            btnCancelarFijo.Visible = true;
        }

        protected void btnCancelarFijo_Click(object sender, EventArgs e)
        {
            txtDiasCalculo.Enabled = false;
            txtDiasFechaOfertaCapitulo.Enabled = false;
            txtDiasRetrocederOferta.Enabled = false;
            txtDiasFechaOfertaPedido.Enabled = false;
            txtDiasRetrocederPedido.Enabled = false;
            btnEditarFijo.Visible = true;
            btnGuardarFijo.Visible = false;
            btnCancelarFijo.Visible = false;
        }

        protected void btnGuardarFijo_Click(object sender, EventArgs e)
        {
            conexiones.crearConexion();
            conexiones.consulta = "sp_ROP_ConfiguracionFijaActualizar";
            conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
            conexiones.comando.CommandType = CommandType.StoredProcedure;
            conexiones.comando.Parameters.AddWithValue("@COF_DiasCalculo", Convert.ToInt32(txtDiasCalculo.Text));
            conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasEntreFechaOfertaFechaCapitulo", Convert.ToInt32(txtDiasFechaOfertaCapitulo.Text));
            conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasRestarFechaCapítulo", Convert.ToInt32(txtDiasRetrocederOferta.Text));
            conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasEntreFechaOfertaFechaPedido", Convert.ToInt32(txtDiasFechaOfertaPedido.Text));
            conexiones.comando.Parameters.AddWithValue("@COF_OfertaDiasRestarFechaPedido", Convert.ToInt32(txtDiasRetrocederPedido.Text));
            conexiones.comando.ExecuteNonQuery();
            conexiones.conexion.Close();

            txtDiasCalculo.Enabled = false;
            txtDiasFechaOfertaCapitulo.Enabled = false;
            txtDiasRetrocederOferta.Enabled = false;
            txtDiasFechaOfertaPedido.Enabled = false;
            txtDiasRetrocederPedido.Enabled = false;
            btnEditarFijo.Visible = true;
            btnGuardarFijo.Visible = false;
            btnCancelarFijo.Visible = false;
        }
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