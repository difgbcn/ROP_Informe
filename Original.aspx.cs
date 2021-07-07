using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

using System.Security.Principal;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ROP_Informe
{
    public partial class Default : Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //conexion.ImpersonateUser("cromlec3", "ALSINA", "CroAls19");
                ////conexion.UndoImpersonation();

                dataDatos.Visible = false;

                lblMensajeError.Visible = false;
                lblMensajeError.Text = Page.User.Identity.AuthenticationType + " - Usuario: " + Page.User.Identity.Name + " / " + User.Identity.Name + " // " + User.Identity.IsAuthenticated;

                cmbEmpresa.Items.Clear();
                cmbEmpresa.Items.Add("");
                cmbEmpresa.Items.Add(new ListItem { Text = "AE1", Value = "GR22" });
                cmbEmpresa.Items.Add(new ListItem { Text = "CL0", Value = "GR06" });
                cmbEmpresa.Items.Add(new ListItem { Text = "CO0", Value = "GR10" });
                cmbEmpresa.Items.Add(new ListItem { Text = "ES0", Value = "GR01" });
                cmbEmpresa.Items.Add(new ListItem { Text = "ES1", Value = "GR01" });
                cmbEmpresa.Items.Add(new ListItem { Text = "IN1", Value = "GR15" });
                cmbEmpresa.Items.Add(new ListItem { Text = "IT0", Value = "GR01" });
                cmbEmpresa.Items.Add(new ListItem { Text = "MA1", Value = "GR05" });
                cmbEmpresa.Items.Add(new ListItem { Text = "MX0", Value = "GR19" });
                cmbEmpresa.Items.Add(new ListItem { Text = "PA0", Value = "GR11" });
                cmbEmpresa.Items.Add(new ListItem { Text = "PE0", Value = "GR07" });
                cmbEmpresa.Items.Add(new ListItem { Text = "PH0", Value = "GR20" });
                cmbEmpresa.Items.Add(new ListItem { Text = "PL0", Value = "GR03" });
                cmbEmpresa.Items.Add(new ListItem { Text = "PT0", Value = "GR01" });
                cmbEmpresa.Items.Add(new ListItem { Text = "PY0", Value = "GR09" });
                cmbEmpresa.Items.Add(new ListItem { Text = "RO0", Value = "GR04" });
                cmbEmpresa.Items.Add(new ListItem { Text = "SA0", Value = "GR21" });
                cmbEmpresa.Items.Add(new ListItem { Text = "US0", Value = "GR12" });
                cmbEmpresa.Items.Add(new ListItem { Text = "UY0", Value = "GR08" });

                cmbEmpresa.Text = "";
            }
        }

        
        protected void btnBuscarInformacion_Click(object sender, EventArgs e)
        {
            System.Collections.IEnumerator enumerator_1;
            System.Collections.IEnumerator enumerator_2;
            System.Collections.IEnumerator enumerator_3;
            System.Collections.IEnumerator enumerator_articulo;
            System.Collections.IEnumerator enumerator_inventario;
            string producto = "";
            decimal importeFacturacion = 0;
            decimal importeVenta = 0;
            decimal importeAlquiler = 0;
            decimal importeProducto = 0;
            decimal importeServicio = 0;

            try
            {
                dataDatos.Visible = true;

                lblMensajeError.Visible = false;
                lblMensajeError.Text = cmbEmpresa.SelectedItem  + " // " + cmbEmpresa.SelectedValue;

                lblMensajeError.Visible = false;
                lblMensajeError.Text = "";

                if ((cmbEmpresa.SelectedItem.ToString() == "") || (txtNumero.Text.Length == 0))
                {
                    lblMensajeError.Visible = false;
                    lblMensajeError.Text = "Debe indicar los datos que desea buscar: empresa - número";
                }
                else
                {
                    // CABECERA
                    tablaOfertasMaster.CallContext contexto_1 = new tablaOfertasMaster.CallContext();
                    contexto_1.Company = cmbEmpresa.SelectedItem.ToString();

                    tablaOfertasMaster.QueryCriteria criterio_1 = new tablaOfertasMaster.QueryCriteria();
                    criterio_1.CriteriaElement = new tablaOfertasMaster.CriteriaElement[1];

                    criterio_1.CriteriaElement[0] = new tablaOfertasMaster.CriteriaElement();
                    criterio_1.CriteriaElement[0].FieldName = "QuotationId";
                    criterio_1.CriteriaElement[0].DataSourceName = "SalesQuotationMasterTable";
                    criterio_1.CriteriaElement[0].Operator = tablaOfertasMaster.Operator.Equal;
                    criterio_1.CriteriaElement[0].Value1 = txtNumero.Text;

                    tablaOfertasMaster.SalesQuotationMasterTableServiceClient proxy_1 = new tablaOfertasMaster.SalesQuotationMasterTableServiceClient();

                    tablaOfertasMaster.AxdSalesQuotationMasterTable axdTablaOfertas_1 = proxy_1.find(contexto_1, criterio_1);
                    if (axdTablaOfertas_1.SalesQuotationMasterTable is null)
                    {
                        proxy_1.Close();
                    }
                    else
                    {
                        enumerator_1 = axdTablaOfertas_1.SalesQuotationMasterTable.GetEnumerator();

                        while (enumerator_1.MoveNext())
                        {
                            tablaOfertasMaster.AxdEntity_SalesQuotationMasterTable axdEntity_OfertaTable_1 = (tablaOfertasMaster.AxdEntity_SalesQuotationMasterTable)enumerator_1.Current;
                            txtNombreOferta.Text = axdEntity_OfertaTable_1.QuotationId + " / " + axdEntity_OfertaTable_1.QuotationName;
                        }

                        proxy_1.Close();

                        // CAPITULOS
                        tablaOfertasTable.CallContext contexto_2 = new tablaOfertasTable.CallContext();
                        contexto_2.Company = cmbEmpresa.SelectedItem.ToString();

                        tablaOfertasTable.QueryCriteria criterio_2 = new tablaOfertasTable.QueryCriteria();
                        criterio_2.CriteriaElement = new tablaOfertasTable.CriteriaElement[1];

                        criterio_2.CriteriaElement[0] = new tablaOfertasTable.CriteriaElement();
                        criterio_2.CriteriaElement[0].FieldName = "QuotationId";
                        criterio_2.CriteriaElement[0].DataSourceName = "SalesQuotationTable";
                        criterio_2.CriteriaElement[0].Operator = tablaOfertasTable.Operator.Range;
                        criterio_2.CriteriaElement[0].Value1 = txtNumero.Text + "/01";
                        criterio_2.CriteriaElement[0].Value2 = txtNumero.Text + "/99";

                        tablaOfertasTable.SalesQuotationTableServiceClient proxy_2 = new tablaOfertasTable.SalesQuotationTableServiceClient();

                        tablaOfertasTable.AxdSalesQuotationTable axdTablaOfertas_2 = proxy_2.find(contexto_2, criterio_2);
                        if (axdTablaOfertas_2.SalesQuotationTable is null)
                        { }
                        else
                        {
                            enumerator_2 = axdTablaOfertas_2.SalesQuotationTable.GetEnumerator();

                            while (enumerator_2.MoveNext())
                            {
                                tablaOfertasTable.AxdEntity_SalesQuotationTable axdEntity_OfertaTable_2 = (tablaOfertasTable.AxdEntity_SalesQuotationTable)enumerator_2.Current;

                                //LINEAS
                                tablaOfertasLine.CallContext contexto_3 = new tablaOfertasLine.CallContext();
                                contexto_3.Company = cmbEmpresa.SelectedItem.ToString();

                                tablaOfertasLine.QueryCriteria criterio_3 = new tablaOfertasLine.QueryCriteria();
                                criterio_3.CriteriaElement = new tablaOfertasLine.CriteriaElement[1];

                                criterio_3.CriteriaElement[0] = new tablaOfertasLine.CriteriaElement();
                                criterio_3.CriteriaElement[0].FieldName = "QuotationId";
                                criterio_3.CriteriaElement[0].DataSourceName = "SalesQuotationLine";
                                criterio_3.CriteriaElement[0].Operator = tablaOfertasLine.Operator.Equal;
                                criterio_3.CriteriaElement[0].Value1 = axdEntity_OfertaTable_2.QuotationId.ToString(); // "811000042088/06"; 

                                tablaOfertasLine.SalesQuotationLineServiceClient proxy_3 = new tablaOfertasLine.SalesQuotationLineServiceClient();
                                tablaOfertasLine.AxdSalesQuotationLine axdTablaOfertas_3 = proxy_3.find(contexto_3, criterio_3);

                                if (axdTablaOfertas_3.SalesQuotationLine is null)
                                { }
                                else
                                {
                                    enumerator_3 = axdTablaOfertas_3.SalesQuotationLine.GetEnumerator();
                                    while (enumerator_3.MoveNext())
                                    {
                                        tablaOfertasLine.AxdEntity_SalesQuotationLine axdEntity_OfertaTable_3 = (tablaOfertasLine.AxdEntity_SalesQuotationLine)enumerator_3.Current;

                                        // INVENTARIO
                                        producto = "";
                                        tablaInventario.CallContext contexto_inventario = new tablaInventario.CallContext();
                                        contexto_inventario.Company = cmbEmpresa.SelectedItem.ToString();

                                        tablaInventario.QueryCriteria criterio_inventario = new tablaInventario.QueryCriteria();
                                        criterio_inventario.CriteriaElement = new tablaInventario.CriteriaElement[1];

                                        criterio_inventario.CriteriaElement[0] = new tablaInventario.CriteriaElement();
                                        criterio_inventario.CriteriaElement[0].FieldName = "ItemId";
                                        criterio_inventario.CriteriaElement[0].DataSourceName = "InventTable";
                                        criterio_inventario.CriteriaElement[0].Operator = tablaInventario.Operator.Equal;
                                        criterio_inventario.CriteriaElement[0].Value1 = axdEntity_OfertaTable_3.ItemId.ToString();

                                        tablaInventario.ItemServiceClient proxy_inventario = new tablaInventario.ItemServiceClient();
                                        tablaInventario.AxdItem AxdItem_Inventario = proxy_inventario.find(contexto_inventario, criterio_inventario);

                                        if (AxdItem_Inventario.InventTable is null)
                                        { }
                                        else
                                        {
                                            enumerator_inventario = AxdItem_Inventario.InventTable.GetEnumerator();
                                            while (enumerator_inventario.MoveNext())
                                            {
                                                tablaInventario.AxdEntity_InventTable axdEntity_InventTable = (tablaInventario.AxdEntity_InventTable)enumerator_inventario.Current;
                                                //producto= axdEntity_InventTable.Product.ToString();
                                                producto = axdEntity_InventTable.RecId.ToString();
                                            }
                                        }
                                        proxy_inventario.Close();
                                        //txtNombreOferta.Text = txtNombreOferta.Text + " / " + producto;

                                        if (producto != "")
                                        {
                                            //ARTICULO
                                            tablaArticulos.CallContext contexto_articulo = new tablaArticulos.CallContext();
                                            contexto_articulo.Company = cmbEmpresa.SelectedItem.ToString();

                                            tablaArticulos.QueryCriteria criterio_articulo = new tablaArticulos.QueryCriteria();
                                            criterio_articulo.CriteriaElement = new tablaArticulos.CriteriaElement[1];

                                            criterio_articulo.CriteriaElement[0] = new tablaArticulos.CriteriaElement();
                                            criterio_articulo.CriteriaElement[0].FieldName = "RecId"; //"DISPLAYPRODUCTNUMBER";
                                            criterio_articulo.CriteriaElement[0].DataSourceName = "Product";
                                            criterio_articulo.CriteriaElement[0].Operator = tablaArticulos.Operator.Equal;
                                            criterio_articulo.CriteriaElement[0].Value1 = producto; // axdEntity_OfertaTable_3.ItemId.ToString();  //

                                            tablaArticulos.EcoResProductServiceClient proxy_articulo = new tablaArticulos.EcoResProductServiceClient();
                                            tablaArticulos.AxdEcoResProduct AxdEcoResProduct_1 = proxy_articulo.find(contexto_articulo, criterio_articulo);

                                            if (AxdEcoResProduct_1.Product is null)
                                            { }
                                            else
                                            {
                                                enumerator_articulo = AxdEcoResProduct_1.Product.GetEnumerator();
                                                while (enumerator_articulo.MoveNext())
                                                {
                                                    tablaArticulos.AxdEntity_Product_EcoResProduct axdEntity_Articulo = (tablaArticulos.AxdEntity_Product_EcoResProduct)enumerator_articulo.Current;
                                                    if (axdEntity_OfertaTable_2.SalesRental.ToString().ToUpper() == "SALES")
                                                    {
                                                        if (axdEntity_Articulo.ProductType.ToString().ToUpper() == "ITEM")
                                                            importeProducto = importeProducto + Convert.ToDecimal(axdEntity_OfertaTable_3.LineAmount);
                                                        else
                                                            importeServicio = importeServicio + Convert.ToDecimal(axdEntity_OfertaTable_3.LineAmount);
                                                    }
                                                }
                                            }
                                            proxy_articulo.Close();
                                        }

                                        if (axdEntity_OfertaTable_2.SalesRental.ToString().ToUpper() == "RENTAL")
                                            importeAlquiler = importeAlquiler + (Convert.ToDecimal(axdEntity_OfertaTable_3.SalesQty) * Convert.ToDecimal(axdEntity_OfertaTable_3.EurDia) * Convert.ToDecimal(axdEntity_OfertaTable_3.DuracionEstimada));
                                        else
                                            importeVenta = importeVenta + Convert.ToDecimal(axdEntity_OfertaTable_3.LineAmount);
                                    }
                                }
                                proxy_3.Close();
                            }
                        }
                        proxy_2.Close();
                    }

                    // TABLA
                    importeFacturacion = importeVenta + importeAlquiler;

                    dataDatos.DataSource = null;
                    dataDatos.Columns.Clear();

                    DataTable dt = new DataTable();

                    dt.Columns.Add("CONCEPTO");
                    dt.Columns.Add("IMPORTE", typeof(System.Decimal));
                    dt.Columns.Add("%", typeof(System.Decimal));

                    DataRow oItem = dt.NewRow();
                    oItem[0] = "FACTURACIÓN";
                    oItem[1] = importeFacturacion;
                    if (importeFacturacion == 0)
                        oItem[2] = String.Format("0", "#,###.00");
                    else
                        oItem[2] = String.Format("100", "#,###.00"); 
                    dt.Rows.Add(oItem);

                    oItem = dt.NewRow();
                    oItem[0] = "ALQUILERES";
                    oItem[1] = importeAlquiler;
                    if (importeFacturacion == 0)
                        oItem[2] = String.Format("0", "#,###.00");
                    else
                        oItem[2] = String.Format((importeAlquiler * 100 / (importeFacturacion)).ToString(), "#,###.00");
                    dt.Rows.Add(oItem);

                    oItem = dt.NewRow();
                    oItem[0] = "VENTAS";
                    oItem[1] = importeVenta;
                    if (importeFacturacion == 0)
                        oItem[2] = String.Format("0", "#,###.00");
                    else
                        oItem[2] = String.Format((importeVenta * 100 / (importeFacturacion)).ToString(), "#,###.00");
                    dt.Rows.Add(oItem);

                    oItem = dt.NewRow();
                    oItem[0] = "PRODUCTOS";
                    oItem[1] = importeProducto;
                    if (importeVenta == 0)
                        oItem[2] = String.Format("0", "#,###.00");
                    else
                        oItem[2] = String.Format((importeProducto * 100 / (importeVenta)).ToString(), "#,###.00"); 
                    dt.Rows.Add(oItem);

                    oItem = dt.NewRow();
                    oItem[0] = "SERVICIOS";
                    oItem[1] = importeServicio;
                    if (importeVenta == 0)
                        oItem[2] = String.Format("0", "#,###.00");
                    else
                        oItem[2] = String.Format((importeServicio * 100 / (importeVenta)).ToString(), "#,###.00");
                    dt.Rows.Add(oItem);

                    dataDatos.DataSource = dt;
                    dataDatos.DataBind();
                }
            }
            catch (System.IO.IOException eM)    
            {
                Server.Transfer("ErrorPage.aspx", true);
            }
        }

        private void creaColumnasGrid()
        {
            dataDatos.Visible = false;

            dataDatos.DataSource = null;
            dataDatos.Columns.Clear();

            DataTable dt = new DataTable();

            dt.Columns.Add("CONCEPTO");
            dt.Columns.Add("IMPORTE", typeof(System.Decimal));
            dt.Columns.Add("%", typeof(System.Decimal));

                        DataRow oItem = dt.NewRow();
            oItem[0] = "FACTURACIÓN";
            oItem[1] = 0;
            oItem[2] = 0;
            dt.Rows.Add(oItem);
            
            oItem = dt.NewRow();
            oItem[0] = "ALQUILERES";
            oItem[1] = 0;
            oItem[2] = 0;
            dt.Rows.Add(oItem);

            oItem = dt.NewRow();
            oItem[0] = "VENTAS";
            oItem[1] = 0;
            oItem[2] = 0;
            dt.Rows.Add(oItem);

            oItem = dt.NewRow();
            oItem[0] = "PRODUCTOS";
            oItem[1] = 0;
            oItem[2] = 0;
            dt.Rows.Add(oItem);

            oItem = dt.NewRow();
            oItem[0] = "SERVICIOS";
            oItem[1] = 0;
            oItem[2] = 0;
            dt.Rows.Add(oItem);

            //oItem = dt.NewRow();
            //oItem[0] = "PORTES";
            //oItem[1] = 0;
            //oItem[2] = 0;
            //dt.Rows.Add(oItem);

            dataDatos.DataSource = dt;
            dataDatos.DataBind();
        }

        protected void dataDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string concepto = DataBinder.Eval(e.Row.DataItem, "CONCEPTO").ToString();
                if (concepto == "FACTURACIÓN")
                {
                    e.Row.BackColor = System.Drawing.Color.DarkGreen;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
                else if (concepto == "ALQUILERES")
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else if (concepto == "VENTAS")
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else if (concepto == "PRODUCTOS")
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                else if (concepto == "SERVICIOS")
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }
}