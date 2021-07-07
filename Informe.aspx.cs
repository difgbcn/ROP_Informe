using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace ROP_Informe
{
    public partial class Informe : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Usuario: " + Page.User.Identity.Name;

                //lblMensajeError.Visible = false;
                //lblMensajeError.Text = "";
                cmbEmpresa.Items.Clear();
                cmbEmpresa.Items.Add("");
                cmbEmpresa.Items.Add("AE1");
                cmbEmpresa.Items.Add("CL0");
                cmbEmpresa.Items.Add("CO0");
                cmbEmpresa.Items.Add("ES0");
                cmbEmpresa.Items.Add("ES1");
                cmbEmpresa.Items.Add("IN1");
                cmbEmpresa.Items.Add("IT0");
                cmbEmpresa.Items.Add("MA1");
                cmbEmpresa.Items.Add("MX0");
                cmbEmpresa.Items.Add("PA0");
                cmbEmpresa.Items.Add("PE0");
                cmbEmpresa.Items.Add("PH0");
                cmbEmpresa.Items.Add("PT0");
                cmbEmpresa.Items.Add("PY0");
                cmbEmpresa.Items.Add("RO0");
                cmbEmpresa.Items.Add("SA0");
                cmbEmpresa.Items.Add("US0");
                cmbEmpresa.Items.Add("UY0");

                cmbEmpresa.Text = "";
            }
        }

        protected void btnBuscarInformacion_Click(object sender, EventArgs e)
        {
            System.Collections.IEnumerator enumerator_1;
            string va = "";

            try
            {
                dataDatos_1.DataSource = null;
                dataDatos_1.Columns.Clear();
                //lblMensajeError.Visible = false;
                lblMensajeError.Text = "";

                if ((cmbEmpresa.Text == "") || (txtNumero.Text.Length == 0))
                {
                    lblMensajeError.Visible = true;
                    lblMensajeError.Text = "Debe indicar los datos que desea buscar: empresa - número";
                }
                else
                {
                    va = "Abrir CallContext";
                    tablaOfertasMaster.CallContext contexto_1 = new tablaOfertasMaster.CallContext();
                    va = "contexto.Company " + cmbEmpresa.Text;
                    contexto_1.Company = "ES0"; // cmbEmpresa.Text;

                    va = "criterio QuotationId";
                    tablaOfertasMaster.QueryCriteria criterio_1 = new tablaOfertasMaster.QueryCriteria();
                    criterio_1.CriteriaElement = new tablaOfertasMaster.CriteriaElement[1];

                    criterio_1.CriteriaElement[0] = new tablaOfertasMaster.CriteriaElement();
                    criterio_1.CriteriaElement[0].FieldName = "QuotationId";
                    criterio_1.CriteriaElement[0].DataSourceName = "SalesQuotationMasterTable";
                    criterio_1.CriteriaElement[0].Operator = tablaOfertasMaster.Operator.Equal;
                    criterio_1.CriteriaElement[0].Value1 = "811000042088"; // txtNumero.Text;

                    va = "proxy";
                    tablaOfertasMaster.SalesQuotationMasterTableServiceClient proxy_1 = new tablaOfertasMaster.SalesQuotationMasterTableServiceClient();

                    va = "proxy.find";
                    tablaOfertasMaster.AxdSalesQuotationMasterTable axdTablaOfertas_1 = proxy_1.find(contexto_1, criterio_1);
                    va = "GetEnumerator";
                    enumerator_1 = axdTablaOfertas_1.SalesQuotationMasterTable.GetEnumerator();
                    dataDatos_1.DataSource = axdTablaOfertas_1.SalesQuotationMasterTable;
                    dataDatos_1.DataBind();

                    lblMensajeError.Visible = true;
                    lblMensajeError.Text = "Recorrer...";
                    while (enumerator_1.MoveNext())
                    {
                        lblMensajeError.Visible = true;
                        lblMensajeError.Text = "Recorriendo...";
                        lblMensajeError.Visible = true;
                        tablaOfertasMaster.AxdEntity_SalesQuotationMasterTable axdEntity_OfertaTable_1 = (tablaOfertasMaster.AxdEntity_SalesQuotationMasterTable)enumerator_1.Current;
                        lblMensajeError.Text = "Oferta: " + axdEntity_OfertaTable_1.QuotationId + " / " + axdEntity_OfertaTable_1.QuotationName;
                    }

                    va = "cierra proxy";
                    proxy_1.Close();
                    //lblMensajeError.Visible = true;
                    //lblMensajeError.Text = "Finalizado...";
                }
            }
            catch (System.IO.IOException eM)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = va + " / " + eM.Message;
            }
        }
    }
}