using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

namespace ROP_Informe
{
    public partial class Default : System.Web.UI.Page
    {
        string usuario = Environment.UserName + "_" + DateTime.Now.ToString("hh_mm");
        string nombreFicha = "";
        string delegacion = "";

        List<int> listaBonificacionesDesde = new List<int>();
        List<int> listaBonificacionesHasta = new List<int>();
        List<decimal> listaBonificacionesValor = new List<decimal>();

        int COL_DATA_CAPITULO = 0;
        int COL_DATA_TIPO_CAPITULO = 1;
        int COL_DATA_ITEM = 2;
        int COL_DATA_TIPO_ITEM = 3;
        int COL_DATA_COEFICIENTE_NUEVO = 4;
        int COL_DATA_COEFICIENTE_USADO = 5;
        int COL_DATA_CANTIDAD = 6;
        int COL_DATA_EURO_DIA = 7;
        int COL_DATA_DURACION_ESTIMADA = 8;
        int COL_DATA_IMPORTE_ALQUILER = 9;
        int COL_DATA_IMPORTE_VENTA = 10;
        int COL_DATA_IMPORTE_VENTA_SERVICIO = 11;
        int COL_DATA_IMPORTE_VENTA_PRODUCTO = 12;
        int COL_DATA_CANTIDAD_DIAS = 13;
        int COL_DATA_FECHA = 14;
        int COL_DATA_PRECIO = 15;
        int COL_DATA_PRECIO_ORIGINAL = 16;
        int COL_DATA_PRECIO_CORRECION_NUEVO = 17;
        int COL_DATA_PRECIO_CORRECION_USADO = 18;
        int COL_DATA_FECHA_CAMBIO = 19;
        int COL_DATA_CAMBIO = 20;
        int COL_DATA_TIPO = 21;
        int COL_DATA_SUPERFICIE = 22;
        int COL_DATA_COSTE_SUPERFICIE = 23;
        int COL_DATA_PRECIO_CONSUMIBLE = 24;
        int COL_DATA_PRECIO_NO_CONSUMIBLE = 25;
        int COL_DATA_TAX_CONSUMIBLE = 26;
        int COL_DATA_IMPORTE_TAX = 27;
        int COL_DATA_AJUSTE = 28;
        int COL_DATA_AJUSTE_TAX_CONSUMIBLE = 29;
        int COL_DATA_CANTIDAD_DIAS_NO_TAX = 30;
        int COL_DATA_IMPORTE_NO_TAX_DIAS_DESDE = 31;
        int COL_DATA_IMPORTE_NO_TAX_DIAS_HASTA = 32;
        int COL_DATA_TAX_NO_CONSUMIBLE = 33;
        int COL_DATA_IMPORTE_NO_TAX = 34;
        int COL_DATA_GASTO_VARIABLE = 35;
        int COL_DATA_IMPORTE_GASTO_VARIABLE = 36;
        int COL_DATA_GASTO_FIJO_BU = 37;
        int COL_DATA_IMPORTE_GASTO_FIJO_BU = 38;
        int COL_DATA_GASTO_FIJO_CENTRAL = 39;
        int COL_DATA_IMPORTE_GASTO_FIJO_CENTRAL = 4;

        string nombreInforme;
        DataTable dtTaximetro = new DataTable();
        DataTable dtTaximetroInforme = new DataTable();

        List<string> listaArticulosSinFicha = new List<string>();
        List<string> listaArticulosPedido = new List<string>();
        List<DateTime> listaFechasPedido = new List<DateTime>();
        List<string> listaOfertas = new List<string>();
        List<DateTime> listaFechasOferta = new List<DateTime>();

        DateTime horaTotal_1 = default(DateTime);
        TimeSpan horaTotal_2 = default(TimeSpan);
        DateTime horaLlamada_1 = default(DateTime);
        TimeSpan horaLlamada_2 = default(TimeSpan);
        DateTime horaFechaBase_1 = default(DateTime);
        TimeSpan horaFechaBase_2 = default(TimeSpan);
        DateTime horaPrecios_1 = default(DateTime);
        TimeSpan horaPrecios_2 = default(TimeSpan);
        DateTime horaDatosConfigurados_1 = default(DateTime);
        TimeSpan horaDatosConfigurados_2 = default(TimeSpan);
        DateTime horaDatosConfigurados_3 = default(DateTime);
        TimeSpan horaDatosConfigurados_4 = default(TimeSpan);
        DateTime horaDatosPase1_1 = default(DateTime);
        TimeSpan horaDatosPase1_2 = default(TimeSpan);
        DateTime horaDatosPase2_1 = default(DateTime);
        TimeSpan horaDatosPase2_2 = default(TimeSpan);
        DateTime horaFechaOferta_1 = default(DateTime);
        TimeSpan horaFechaOferta_2 = default(TimeSpan);

        string datosGenerales = "";
        string moneda = "";

        int cantidadAlquiler = 0;
        int cantidadTaximetroNoConsumible = 0;
        int cantidadTaximetroConsumible = 0;
        int cantidadVenta = 0;
        int cantidadVentaServicio = 0;
        int cantidadVentaProducto = 0;
        int cantidadPorte = 0;

        decimal importeFacturacion = 0;
        decimal porcentajeFacturacion = 0;
        decimal importeAlquiler = 0;
        decimal porcentajeAlquiler = 0;
        decimal importeVenta = 0;
        decimal porcentajeVenta = 0;
        decimal importeVentaServicio = 0;
        decimal porcentajeVentaServicio = 0;
        decimal importeVentaProducto = 0;
        decimal porcentajeVentaProducto = 0;
        decimal importePorte = 0;
        decimal porcentajePorte = 0;

        decimal importeCoste = 0;
        decimal porcentajeCoste = 0;
        decimal porcentajeCosteAlquiler = 0;
        decimal importeCosteTaximetros = 0;
        decimal importeCosteTaximetroNoConsumible = 0;
        decimal porcentajeCosteTaximetroNoConsumible = 0;
        decimal importeCosteTotalTaximetroConsumible = 0;
        decimal importeCosteTaximetroConsumible = 0;
        decimal importeCosteBonificacionTaximetroConsumible = 0;
        decimal porcentajeCosteTotalTaximetroConsumible = 0;
        decimal porcentajeCosteTotalTaximetroNoConsumible = 0;
        decimal porcentajeCosteTaximetroConsumible = 0;
        decimal porcentajeCosteBonificacionTaximetroConsumible = 0;
        decimal importeCosteVenta = 0;
        decimal porcentajeCosteVenta = 0;
        decimal importeCosteMPO= 0;
        decimal porcentajeCosteMPO= 0;
        decimal importeCostePorte = 0;
        decimal porcentajeCostePorte = 0;

        decimal importeMargen = 0;
        decimal porcentajeMargen = 0;
        decimal importeMargenAlquiler = 0;
        decimal porcentajeMargenAlquiler = 0;
        decimal importeMargenVenta = 0;
        decimal porcentajeMargenVenta = 0;
        decimal importeMargenPorte = 0;
        decimal porcentajeMargenPorte = 0;

        decimal importeGastosVariables = 0;
        decimal porcentajeGastosVariables = 0;
        decimal importeAlquilerGastosVariables = 0;
        decimal porcentajeAlquilerGastosVariables = 0;
        decimal importeVentaGastosVariables = 0;
        decimal porcentajeVentaGastosVariables = 0;

        decimal importeGastosFijos = 0;
        decimal porcentajeGastosFijos = 0;
        decimal importeGastosFijosBU = 0;
        decimal porcentajeGastosFijosBU = 0;
        decimal importeAlquilerGastosFijosBU = 0;
        decimal porcentajeAlquilerGastosFijosBU = 0;
        decimal importeVentaGastosFijosBU = 0;
        decimal porcentajeVentaGastosFijosBU = 0;

        decimal importeGastosFijosCentrales = 0;
        decimal porcentajeGastosFijosCentrales = 0;
        decimal importeAlquilerGastosFijosCentrales = 0;
        decimal porcentajeAlquilerGastosFijosCentrales = 0;
        decimal importeVentaGastosFijosCentrales = 0;
        decimal porcentajeVentaGastosFijosCentrales = 0;

        decimal importeROPBasico = 0;
        decimal porcentajeROPBasico = 0;
        decimal importeROPBasicoAlquiler = 0;
        decimal porcentajeROPBasicoAlquiler = 0;
        decimal importeROPBasicoVenta = 0;
        decimal porcentajeROPBasicoVenta = 0;


        List<string> capitulosAlquiler = new List<string>();
        List<string> capitulosTaximetroNoConsumible = new List<string>();
        List<string> capitulosTaximetroConsumible = new List<string>();
        List<string> capitulosVenta = new List<string>();
        List<string> capitulosVentaServicio = new List<string>();
        List<string> capitulosVentaProducto = new List<string>();
        List<string> capitulosPorte = new List<string>();

        List<decimal> dec_importeFacturacionAlquilerCapitulos = new List<decimal>();
        List<decimal> dec_importeFacturacionVentaServicioCapitulos = new List<decimal>();
        List<decimal> dec_importeFacturacionVentaProductoCapitulos = new List<decimal>();
        List<decimal> dec_importeFacturacionPorteCapitulos = new List<decimal>();
        List<decimal> dec_importeCosteTaximetroNoConsumibleCapitulos = new List<decimal>();
        List<decimal> dec_importeCosteTaximetroConsumibleCapitulos = new List<decimal>();
        List<decimal> dec_importeCosteBonificacionTaximetroConsumibleCapitulos = new List<decimal>();
        List<decimal> dec_importeCosteVentaCapitulos = new List<decimal>();
        List<decimal> dec_importeCostePorteCapitulos = new List<decimal>();
        List<decimal> dec_importeMargenAlquilerCapitulos = new List<decimal>();
        List<decimal> dec_importeMargenVentaCapitulos = new List<decimal>();
        List<decimal> dec_importeMargenPorteCapitulos = new List<decimal>();
        List<decimal> dec_importeGastosVariablesAlquilerCapitulos = new List<decimal>();
        List<decimal> dec_importeGastosVariablesVentaCapitulos = new List<decimal>();
        List<decimal> dec_importeGastosFijosBUAlquilerCapitulos = new List<decimal>();
        List<decimal> dec_importeGastosFijosBUVentaCapitulos = new List<decimal>();
        List<decimal> dec_importeGastosFijosCentralesAlquilerCapitulos = new List<decimal>();
        List<decimal> dec_importeGastosFijosCentralesVentaCapitulos = new List<decimal>();

        decimal dec_importeCosteMPOMixto = 0;
        decimal dec_importeCosteMPONuevo= 0;
        decimal dec_importeCosteMPOUsado = 0;

        string strHijoFacturacionAlquiler = "";
        string strHijoFacturacionVentaServicio = "";
        string strHijoFacturacionVentaProducto = "";
        string strHijoFacturacionPorte = "";
        string strHijoCosteAlquiler = "";
        string strHijoCosteTaximetroNoConsumible = "";
        string strHijoCosteTaximetroConsumible = "";
        string strHijoCosteBonificacionTaximetroConsumible = "";

        string strHijoCosteVenta = "";
        string strHijoCosteMPO= "";
        string strHijoCostePorte = "";
        string strHijoMargenAlquiler = "";
        string strHijoMargenVenta = "";
        string strHijoMargenPorte = "";

        string strHijoGastosVariablesAlquiler = "";
        string strHijoGastosVariablesVenta = "";

        string strHijoGastosFijosBUAlquiler = "";
        string strHijoGastosFijosBUVenta = "";

        string strHijoGastosFijosCentralesAlquiler = "";
        string strHijoGastosFijosCentralesVenta = "";

        string strHijoROPBasicoAlquiler = "";
        string strHijoROPBasicoVenta = "";


        public string inicioHijo = "           ,\"hijo\": [";
        public string finHijo = "           ]";
        public string primerHijo = "           {" +
           "               \"Concepto\": \"__CAPITULO__\"," +
           "               \"Importe\": \"__importeCapitulo__\"," +
           "               \"%\": \"__porcentajeCapitulo__\"" +
           "           }";
        public string siguienteHijo = "           ,{" +
           "               \"Concepto\": \"__CAPITULO__\"," +
           "               \"Importe\": \"__importeCapitulo__\"," +
           "               \"%\": \"__porcentajeCapitulo__\"" +
           "           }";

        public string baseDatosJson = "[" +
           "   {" +
           "       \"Concepto\": \"FACTURACIÓN\"," +
           "       \"Importe\": \"__importeFacturacion__\"," +
           "       \"%\": \"__porcentajeFacturacion__\"," +
           "       \"colorEncabezado\": \"#267a73\"," +
           "       \"hijo\": [" +
           "       {" +
           "           \"Concepto\": \"ALQUILERES\"," +
           "           \"Importe\": \"__importeAlquiler__\"," +
           "           \"%\": \"__porcentajeAlquiler__\"," +
           "           \"colorEncabezado\": \"#ace5e0\"" +
           "           __HIJO_FACTURACION_ALQUILERES__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"VENTAS\"," +
           "           \"Importe\": \"__importeVenta__\"," +
           "           \"%\": \"__porcentajeVenta__\"," +
           "           \"colorEncabezado\": \"#1edccb\"," +
           "           \"hijo\": [" +
           "           {" +
           "               \"Concepto\": \"SERVICIOS\"," +
           "               \"Importe\": \"__importeServicio__\"," +
           "               \"%\": \"__porcentajeServicio__\"" +
           "               __HIJO_FACTURACION_SERVICIOS__" +
           "           }," +
           "           {" +
           "               \"Concepto\": \"PRODUCTOS\"," +
           "               \"Importe\": \"__importeProducto__\"," +
           "               \"%\": \"__porcentajeProducto__\"" +
           "               __HIJO_FACTURACION_PRODUCTOS__" +
           "           }" +
           "           ]" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"PORTES\"," +
           "           \"Importe\": \"__importePorte__\"," +
           "           \"%\": \"__porcentajePorte__\"," +
           "           \"colorEncabezado\": \"#1edccb\"" +
           "           __HIJO_FACTURACION_PORTES__" +
           "       }" +
           "       ]" +
           "   }," +
           "   {" +
           "       \"Concepto\": \"COSTE\"," +
           "       \"Importe\": \"__importeCoste__\"," +
           "       \"%\": \"__porcentajeCoste__\"," +
           "       \"colorEncabezado\": \"#d61e08\"," +
           "       \"hijo\": [" +
           "       {" +
           "           \"Concepto\": \"TAXÍMETROS\"," +
           "           \"Importe\": \"__importeTaximetros__\"," +
           "           \"%\": \"__porcentajeTaximetros__\"," +
           "           \"colorEncabezado\": \"#fc3e27\"," +
           "           \"hijo\": [" +
           "           {" +
           "               \"Concepto\": \"TAXÍMETRO NO CONSUMIBLE\"," +
           "               \"Importe\": \"__importeTaximetroNoConsumible__\"," +
           "               \"%\": \"__porcentajeTaximetroNoConsumible__\"," +
           "               \"colorEncabezado\": \"#1edccb\"" +
           "               __HIJO_COSTE_TAXIMETRO_NO_CONSUMIBLE__" +
           "           }," +
           "           {" +
           "               \"Concepto\": \"TOTAL TAXÍMETRO CONSUMIBLE\"," +
           "               \"Importe\": \"__importeTotalTaximetroConsumible__\"," +
           "               \"%\": \"__porcentajeTotalTaximetroConsumible__\"," +
           "               \"colorEncabezado\": \"#b081b2\"," +
           "               \"hijo\": [" +
           "               {" +
           "                   \"Concepto\": \"TAXÍMETRO CONSUMIBLE\"," +
           "                   \"Importe\": \"__importeTaximetroConsumible__\"," +
           "                   \"%\": \"__porcentajeTaximetroConsumible__\"," +
           "                   \"colorEncabezado\": \"#e4abe7\"" +
           "                   __HIJO_COSTE_TAXIMETRO_CONSUMIBLE__" +
           "               }," +
           "               {" +
           "                   \"Concepto\": \"AJUSTE TAXÍMETRO CONSUMIBLE\"," +
           "                   \"Importe\": \"__importeTaximetroBonificacionConsumible__\"," +
           "                   \"%\": \"__porcentajeTaximetroBonificacionConsumible__\"," +
           "                   \"colorEncabezado\": \"#f9bcfc\"" +
           "                   __HIJO_COSTE_TAXIMETRO_BONIFICACION_CONSUMIBLE__" +
           "               }" +
           "               ]" +
           "           }" +
           "           ]" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"MPO\"," +
           "           \"Importe\": \"__importeCosteMPO__\"," +
           "           \"%\": \"__porcentajeCosteMPO__\"," +
           "           \"colorEncabezado\": \"#fc9589\"" +
           "           __HIJO_COSTE_MPO__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"VENTAS\"," +
           "           \"Importe\": \"__importeCosteVenta__\"," +
           "           \"%\": \"__porcentajeCosteVenta__\"," +
           "           \"colorEncabezado\": \"#fc9589\"" +
           "           __HIJO_COSTE_VENTAS__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"PORTES\"," +
           "           \"Importe\": \"__importeCostePorte__\"," +
           "           \"%\": \"__porcentajeCostePorte__\"," +
           "           \"colorEncabezado\": \"#fc9589\"" +
           "           __HIJO_COSTE_PORTES__" +
           "       }" +
           "       ]" +
           "   }," +
           "   {" +
           "       \"Concepto\": \"MARGEN\"," +
           "       \"Importe\": \"__importeMargen__\"," +
           "       \"%\": \"__porcentajeMargen__\"," +
           "       \"colorEncabezado\": \"#09a723\"," +
           "       \"hijo\": [" +
           "       {" +
           "           \"Concepto\": \"ALQUILERES\"," +
           "           \"Importe\": \"__importeMargenAlquiler__\"," +
           "           \"%\": \"__porcentajeMargenAlquiler__\"," +
           "           \"colorEncabezado\": \"#bdfdae\"" +
           "           __HIJO_MARGEN_ALQUILERES__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"VENTAS\"," +
           "           \"Importe\": \"__importeMargenVenta__\"," +
           "           \"%\": \"__porcentajeMargenVenta__\"," +
           "           \"colorEncabezado\": \"#7aed8e\"" +
           "           __HIJO_MARGEN_VENTAS__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"PORTES\"," +
           "           \"Importe\": \"__importeMargenPorte__\"," +
           "           \"%\": \"__porcentajeMargenPorte__\"," +
           "           \"colorEncabezado\": \"#7aed8e\"" +
           "           __HIJO_MARGEN_PORTES__" +
           "       }" +
           "       ]" +
           "   }," +
           "   {" +
           "       \"Concepto\": \"GASTOS VARIABLES\"," +
           "       \"Importe\": \"__importeGastoVariable__\"," +
           "       \"%\": \"__porcentajeGastoVariable__\"," +
           "       \"colorEncabezado\": \"#ff9f33\"," +
           "       \"hijo\": [" +
           "       {" +
           "           \"Concepto\": \"ALQUILERES\"," +
           "           \"Importe\": \"__importeGastoVariableAlquiler__\"," +
           "           \"%\": \"__porcentajeGastoVariableAlquiler__\"," +
           "           \"colorEncabezado\": \"#fec27e\"" +
           "           __HIJO_GASTO_VARIABLE_ALQUILER__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"VENTAS\"," +
           "           \"Importe\": \"__importeGastoVariableVenta__\"," +
           "           \"%\": \"__porcentajeGastoVariableVenta__\"," +
           "           \"colorEncabezado\": \"#fec27e\"" +
           "           __HIJO_GASTO_VARIABLE_VENTA__" +
           "       }" +
           "       ]" +
           "   }," +
           "   {" +
           "       \"Concepto\": \"GASTOS FIJOS\"," +
           "       \"Importe\": \"__importeGastoFijo__\"," +
           "       \"%\": \"__porcentajeGastoFijo__\"," +
           "       \"colorEncabezado\": \"#c6473d\"," +
           "       \"hijo\": [" +
           "       {" +
           "           \"Concepto\": \"GASTOS FIJOS BU\"," +
           "           \"Importe\": \"__importeGastoFijoBU__\"," +
           "           \"%\": \"__porcentajeGastoFijoBU__\"," +
           "           \"colorEncabezado\": \"#ee5246\"," +
           "            \"hijo\": [" +
           "           {" +
           "               \"Concepto\": \"ALQUILERES\"," +
           "               \"Importe\": \"__importeGastoFijoBUAlquiler__\"," +
           "               \"%\": \"__porcentajGastoFijoBUAlquiler__\"," +
           "               \"colorEncabezado\": \"#ee5246\"" +
           "               __HIJO_GASTO_FIJO_BU_ALQUILER__" +
           "           }," +
           "           {" +
           "               \"Concepto\": \"VENTAS\"," +
           "               \"Importe\": \"__importeGastoFijoBUVenta__\"," +
           "               \"%\": \"__porcentajeGastoFijoBUVenta__\"," +
           "               \"colorEncabezado\": \"#ee5246\"" +
           "               __HIJO_GASTO_FIJO_BU_VENTA__" +
           "           }" +
           "           ]" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"GASTOS FIJOS CENTRALES\"," +
           "           \"Importe\": \"__importeGastoFijoCentral__\"," +
           "           \"%\": \"__porcentajeGastoFijoCentral__\"," +
           "           \"colorEncabezado\": \"#f3675c\"," +
           "            \"hijo\": [" +
           "           {" +
           "               \"Concepto\": \"ALQUILERES\"," +
           "               \"Importe\": \"__importeGastoFijoCentralAlquiler__\"," +
           "               \"%\": \"__porcentajeGastoFijoCentralAlquiler__\"," +
           "               \"colorEncabezado\": \"#f3675c\"" +
           "               __HIJO_GASTO_FIJO_CENTRAL_ALQUILER__" +
           "           }," +
           "           {" +
           "               \"Concepto\": \"VENTAS\"," +
           "               \"Importe\": \"__importeGastoFijoCentralVenta__\"," +
           "               \"%\": \"__porcentajeGastoFijoCentralVenta__\"," +
           "               \"colorEncabezado\": \"#f3675c\"" +
           "               __HIJO_GASTO_FIJO_CENTRAL_VENTA__" +
           "           }" +
           "           ]" +
           "       }" +
           "       ]" +
           "   }," +
           "   {" +
           "       \"Concepto\": \"ROP BÁSICO\"," +
           "       \"Importe\": \"__importeROPBasico__\"," +
           "       \"%\": \"__porcentajeROPBasico__\"," +
           "       \"colorEncabezado\": \"#52d891\"," +
           "       \"hijo\": [" +
           "       {" +
           "           \"Concepto\": \"ALQUILERES\"," +
           "           \"Importe\": \"__importeROPBasicoAlquiler__\"," +
           "           \"%\": \"__porcentajeROPBasicoAlquiler__\"," +
           "           \"colorEncabezado\": \"#95deb7\"" +
           "           __HIJO_ROP_BASICO_ALQUILER__" +
           "       }," +
           "       {" +
           "           \"Concepto\": \"VENTAS\"," +
           "           \"Importe\": \"__importeROPBasicoVenta__\"," +
           "           \"%\": \"__porcentajeROPBasicoVenta__\"," +
           "           \"colorEncabezado\": \"#95deb7\"" +
           "           __HIJO_ROP_BASICO_VENTA__" +
           "       }" +
           "       ]" +
           "   }" +
           "]";

        public string datosJson = "[]";

        protected override void OnLoad(EventArgs e)
        {
            Page.Title = "INFORME ROP";
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int i;

            if (!this.IsPostBack)
            {
                lblpopup.Visible = false;
                btnAbrirExcel.Visible = false;
                nombreInforme = "";
                lblMensajeError.Visible = false;
                lblMensajeError.Text = "";
                dataTiempos.Visible = false;
                dataDatos.Visible = false;

                cmbConcepto.Items.Clear();
                cmbConcepto.Items.Add("");
                cmbConcepto.Items.Add(new ListItem { Text = "Ficha", Value = "Ficha" });
                cmbConcepto.Items.Add(new ListItem { Text = "Oferta", Value = "Oferta" });
                cmbConcepto.Items.Add(new ListItem { Text = "Pedido", Value = "Pedido" });

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
                cmbEmpresa.Items.Add("PL0");
                cmbEmpresa.Items.Add("PT0");
                cmbEmpresa.Items.Add("PY0");
                cmbEmpresa.Items.Add("RO0");
                cmbEmpresa.Items.Add("SA0");
                cmbEmpresa.Items.Add("US0");
                cmbEmpresa.Items.Add("UY0");
                cmbEmpresa.Items.Add("UY1");

                cmbConcepto.Text = "";
                cmbEmpresa.Text = "";

                cantidadAlquiler = 0;
                cantidadTaximetroNoConsumible = 0;
                cantidadTaximetroConsumible = 0;
                cantidadVentaServicio = 0;
                cantidadVentaProducto = 0;

                importeFacturacion = 0;
                porcentajeFacturacion = 0;
                importeAlquiler = 0;
                porcentajeAlquiler = 0;
                importeVenta = 0;
                porcentajeVenta = 0;
                importeVentaServicio = 0;
                porcentajeVentaServicio = 0;
                importeVentaProducto = 0;
                porcentajeVentaProducto = 0;

                importeCoste = 0;
                porcentajeCoste = 0;
                porcentajeCosteAlquiler = 0;
                importeCosteTaximetroNoConsumible = 0;
                porcentajeCosteTaximetroNoConsumible = 0;
                importeCosteTotalTaximetroConsumible = 0;
                porcentajeCosteTotalTaximetroConsumible = 0;
                importeCosteTaximetroConsumible = 0;
                porcentajeCosteTaximetroConsumible = 0;
                importeCosteBonificacionTaximetroConsumible = 0;
                porcentajeCosteBonificacionTaximetroConsumible = 0;
                importeCosteVenta = 0;
                porcentajeCosteVenta = 0;

                strHijoFacturacionAlquiler = "";
                strHijoFacturacionVentaServicio = "";
                strHijoFacturacionVentaProducto = "";
                strHijoCosteTaximetroNoConsumible = "";
                strHijoCosteTaximetroConsumible = "";
                strHijoCosteBonificacionTaximetroConsumible = "";
                strHijoCosteVenta = "";

                // SI HAY PARAMETROS
                string empresa = Request.QueryString["Empresa"];
                string numero = Request.QueryString["Oferta"];
                if ((!String.IsNullOrEmpty(empresa)) & (!String.IsNullOrEmpty(numero)))
                {
                    cmbConcepto.Text = "Oferta";

                    i = 0;
                    foreach (var itemEmpresa in cmbEmpresa.Items)
                    {
                        if (itemEmpresa.ToString().ToUpper() == empresa.ToUpper())
                            break;
                        i = i + 1;
                    }

                    cmbEmpresa.SelectedIndex = i;
                    txtNumero.Text = numero;
                    calcularOferta();
                }

                armarJson(true);

                //cantidadTaximetroConsumible = 2;
                //capitulosTaximetroConsumible.Add("uno");
                //capitulosTaximetroConsumible.Add("dos");
                //dec_importeCosteTaximetroConsumibleCapitulos.Add(1);
                //dec_importeCosteTaximetroConsumibleCapitulos.Add(2);
                //dec_importeCosteBonificacionTaximetroConsumibleCapitulos.Add(1);
                //dec_importeCosteBonificacionTaximetroConsumibleCapitulos.Add(2);
                //pintarDatos();

            }
        }

        protected void pintarDatos()
        {
            int cantidad;
            decimal porcentaje;

            try
            {
                dataDatos.DataSource = null;
                dataDatos.Columns.Clear();
                dataDatos.DataSource = dtTaximetro;
                dataDatos.DataBind();
                dataDatos.Visible = true;

                txtNombreOferta.Text = datosGenerales;

                // TABLA
                // FACTURACION //
                //importeFacturacion = importeVenta + importeAlquiler + importePorte;
                importeFacturacion = (importeVentaServicio + importeVentaProducto) + importeAlquiler + importePorte;
               
                if (importeFacturacion == 0)
                {
                    porcentajeFacturacion = 0;
                    porcentajeAlquiler = 0;
                    porcentajeVenta = 0;
                    porcentajePorte = 0;
                    porcentajeVentaServicio = 0;
                    porcentajeVentaProducto = 0;
                }
                else
                {
                    porcentajeFacturacion = 100;
                    porcentajeAlquiler = (importeAlquiler * 100 / importeFacturacion);
                    porcentajeVenta = ((importeVentaServicio + importeVentaProducto) * 100 / importeFacturacion);
                    porcentajePorte = (importePorte * 100 / importeFacturacion);
                    porcentajeVentaServicio = (importeVentaServicio * 100 / importeFacturacion);
                    porcentajeVentaProducto = (importeVentaProducto * 100 / importeFacturacion);
                }

                strHijoFacturacionAlquiler = "";
                if (cantidadAlquiler > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadAlquiler - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoFacturacionAlquiler = inicioHijo + primerHijo;
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler.Replace("__importeCapitulo__", dec_importeFacturacionAlquilerCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeAlquiler == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionAlquilerCapitulos.ElementAt(cantidad) * 100 / (importeAlquiler));
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler + siguienteHijo;
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler.Replace("__importeCapitulo__", dec_importeFacturacionAlquilerCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeAlquiler == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionAlquilerCapitulos.ElementAt(cantidad) * 100 / (importeAlquiler));
                            strHijoFacturacionAlquiler = strHijoFacturacionAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoFacturacionAlquiler = strHijoFacturacionAlquiler + finHijo;
                }

                strHijoFacturacionVentaServicio = "";
                if (cantidadVentaServicio > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVentaServicio - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoFacturacionVentaServicio = inicioHijo + primerHijo;
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio.Replace("__CAPITULO__", capitulosVentaServicio.ElementAt(cantidad));
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio.Replace("__importeCapitulo__", dec_importeFacturacionVentaServicioCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionVentaServicioCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio + siguienteHijo;
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio.Replace("__CAPITULO__", capitulosVentaServicio.ElementAt(cantidad));
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio.Replace("__importeCapitulo__", dec_importeFacturacionVentaServicioCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionVentaServicioCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoFacturacionVentaServicio = strHijoFacturacionVentaServicio + finHijo;
                }

                strHijoFacturacionVentaProducto = "";
                if (cantidadVentaProducto > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVentaProducto - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoFacturacionVentaProducto = inicioHijo + primerHijo;
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto.Replace("__CAPITULO__", capitulosVentaProducto.ElementAt(cantidad));
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto.Replace("__importeCapitulo__", dec_importeFacturacionVentaProductoCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionVentaProductoCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto + siguienteHijo;
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto.Replace("__CAPITULO__", capitulosVentaProducto.ElementAt(cantidad));
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto.Replace("__importeCapitulo__", dec_importeFacturacionVentaProductoCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionVentaProductoCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoFacturacionVentaProducto = strHijoFacturacionVentaProducto + finHijo;
                }

                strHijoFacturacionPorte = "";
                if (cantidadPorte > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadPorte - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoFacturacionPorte = inicioHijo + primerHijo;
                            strHijoFacturacionPorte = strHijoFacturacionPorte.Replace("__CAPITULO__", capitulosPorte.ElementAt(cantidad));
                            strHijoFacturacionPorte = strHijoFacturacionPorte.Replace("__importeCapitulo__", dec_importeFacturacionPorteCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionPorteCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoFacturacionPorte = strHijoFacturacionPorte.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoFacturacionPorte = strHijoFacturacionPorte + siguienteHijo;
                            strHijoFacturacionPorte = strHijoFacturacionPorte.Replace("__CAPITULO__", capitulosPorte.ElementAt(cantidad));
                            strHijoFacturacionPorte = strHijoFacturacionPorte.Replace("__importeCapitulo__", dec_importeFacturacionPorteCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeFacturacionPorteCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoFacturacionPorte = strHijoFacturacionPorte.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoFacturacionPorte = strHijoFacturacionPorte + finHijo;
                }

                // COSTE //
                importeCoste = (-1) * (importeCosteVenta + (importeCosteTaximetroNoConsumible + importeCosteTaximetroConsumible - importeCosteBonificacionTaximetroConsumible) + (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado) + importeCostePorte);
                importeCosteTaximetros = (-1) * (importeCosteTaximetroNoConsumible + importeCosteTaximetroConsumible - importeCosteBonificacionTaximetroConsumible);
                importeCosteTotalTaximetroConsumible = (-1) * (importeCosteTaximetroConsumible - importeCosteBonificacionTaximetroConsumible);
                importeCosteMPO = dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado;

                if (importeFacturacion == 0)
                {
                    porcentajeCoste = 0;
                    porcentajeCosteAlquiler = 0;
                    porcentajeCosteTaximetroNoConsumible = 0;
                    porcentajeCosteTotalTaximetroNoConsumible = 0;
                    porcentajeCosteTaximetroConsumible = 0;
                    porcentajeCosteBonificacionTaximetroConsumible = 0;
                    porcentajeCostePorte = 0;
                    porcentajeCosteMPO = 0;
                }
                else
                {
                    porcentajeCoste = (importeCoste * 100) / importeFacturacion;
                    porcentajeCosteVenta= (importeCosteVenta * 100) / importeFacturacion;
                    porcentajeCosteAlquiler = ((importeCosteTaximetroNoConsumible + importeCosteTaximetroConsumible- importeCosteBonificacionTaximetroConsumible) * 100 / importeFacturacion);
                    porcentajeCosteTaximetroNoConsumible = (importeCosteTaximetroNoConsumible * 100) / importeFacturacion;
                    porcentajeCosteTotalTaximetroConsumible = ((importeCosteTaximetroConsumible - importeCosteBonificacionTaximetroConsumible) * 100) / importeFacturacion;
                    porcentajeCosteTaximetroConsumible = (importeCosteTaximetroConsumible * 100) / importeFacturacion;
                    porcentajeCosteBonificacionTaximetroConsumible = (importeCosteBonificacionTaximetroConsumible * 100) / importeFacturacion;
                    porcentajeCostePorte = (importeCostePorte * 100 / importeFacturacion);
                    porcentajeCosteMPO = (importeCosteMPO * 100 / importeFacturacion);
                }

                porcentajeCosteTotalTaximetroNoConsumible = (-1) * porcentajeCosteTotalTaximetroNoConsumible;
                importeCosteTaximetroNoConsumible = (-1) * importeCosteTaximetroNoConsumible;
                importeCosteTaximetroConsumible = (-1) * importeCosteTaximetroConsumible;
                importeCosteVenta = (-1) * importeCosteVenta;
                importeCostePorte = (-1) * importeCostePorte;

                // Taximetro no consumible
                strHijoCosteTaximetroNoConsumible = "";
                if (cantidadTaximetroNoConsumible > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadTaximetroNoConsumible - 1; cantidad++)
                    {

                        if (cantidad == 0)
                        {
                            strHijoCosteTaximetroNoConsumible = inicioHijo + primerHijo;
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible.Replace("__CAPITULO__", capitulosTaximetroNoConsumible.ElementAt(cantidad));
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible.Replace("__importeCapitulo__", ((-1) * dec_importeCosteTaximetroNoConsumibleCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCosteTaximetroNoConsumibleCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible + siguienteHijo;
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible.Replace("__CAPITULO__", capitulosTaximetroNoConsumible.ElementAt(cantidad));
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible.Replace("__importeCapitulo__", ((-1) * dec_importeCosteTaximetroNoConsumibleCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCosteTaximetroNoConsumibleCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoCosteTaximetroNoConsumible = strHijoCosteTaximetroNoConsumible + finHijo;
                }

                // taxímetro consumible
                strHijoCosteTaximetroConsumible = "";
                if (cantidadTaximetroConsumible > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadTaximetroConsumible - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoCosteTaximetroConsumible = inicioHijo + primerHijo;
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible.Replace("__CAPITULO__", capitulosTaximetroConsumible.ElementAt(cantidad));
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible.Replace("__importeCapitulo__", ((-1) * dec_importeCosteTaximetroConsumibleCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCosteTaximetroConsumibleCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible + siguienteHijo;
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible.Replace("__CAPITULO__", capitulosTaximetroConsumible.ElementAt(cantidad));
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible.Replace("__importeCapitulo__", ((-1) * dec_importeCosteTaximetroConsumibleCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCosteTaximetroConsumibleCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoCosteTaximetroConsumible = strHijoCosteTaximetroConsumible + finHijo;
                }

                // Bonificación taxímetro consumible
                strHijoCosteBonificacionTaximetroConsumible = "";
                if (cantidadTaximetroConsumible > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadTaximetroConsumible - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoCosteBonificacionTaximetroConsumible = inicioHijo + primerHijo;
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible.Replace("__CAPITULO__", capitulosTaximetroConsumible.ElementAt(cantidad));
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible.Replace("__importeCapitulo__", (dec_importeCosteBonificacionTaximetroConsumibleCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (-1) * (dec_importeCosteBonificacionTaximetroConsumibleCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible + siguienteHijo;
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible.Replace("__CAPITULO__", capitulosTaximetroConsumible.ElementAt(cantidad));
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible.Replace("__importeCapitulo__", (dec_importeCosteBonificacionTaximetroConsumibleCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (-1) * (dec_importeCosteBonificacionTaximetroConsumibleCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoCosteBonificacionTaximetroConsumible = strHijoCosteBonificacionTaximetroConsumible + finHijo;
                }

                // MPO
                strHijoCosteMPO = "";
               
                strHijoCosteMPO = inicioHijo + primerHijo;
                strHijoCosteMPO = strHijoCosteMPO.Replace("__CAPITULO__", "MIXTO");
                strHijoCosteMPO = strHijoCosteMPO.Replace("__importeCapitulo__", ((-1) * dec_importeCosteMPOMixto).ToString("#,##0.00"));
                if (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado == 0)
                    porcentaje = 0;
                else
                    porcentaje = (dec_importeCosteMPOMixto * 100 / (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado));
                strHijoCosteMPO = strHijoCosteMPO.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));

                strHijoCosteMPO = strHijoCosteMPO + siguienteHijo;
                strHijoCosteMPO = strHijoCosteMPO.Replace("__CAPITULO__", "NUEVO");
                strHijoCosteMPO = strHijoCosteMPO.Replace("__importeCapitulo__", ((-1) * dec_importeCosteMPONuevo).ToString("#,##0.00"));
                if (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado == 0)
                    porcentaje = 0;
                else
                    porcentaje = (dec_importeCosteMPOMixto * 100 / (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado));
                strHijoCosteMPO = strHijoCosteMPO.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));

                strHijoCosteMPO = strHijoCosteMPO + siguienteHijo;
                strHijoCosteMPO = strHijoCosteMPO.Replace("__CAPITULO__", "USADO");
                strHijoCosteMPO = strHijoCosteMPO.Replace("__importeCapitulo__", ((-1) * dec_importeCosteMPOUsado).ToString("#,##0.00"));
                if (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado == 0)
                    porcentaje = 0;
                else
                    porcentaje = (dec_importeCosteMPOMixto * 100 / (dec_importeCosteMPOMixto + dec_importeCosteMPONuevo + dec_importeCosteMPOUsado));
                strHijoCosteMPO = strHijoCosteMPO.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                strHijoCosteMPO = strHijoCosteMPO + finHijo;

                // Venta
                strHijoCosteVenta = "";
                if (cantidadVenta > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVenta - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoCosteVenta = inicioHijo + primerHijo;
                            strHijoCosteVenta = strHijoCosteVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoCosteVenta = strHijoCosteVenta.Replace("__importeCapitulo__", ((-1) * dec_importeCosteVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCosteVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteVenta = strHijoCosteVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoCosteVenta = strHijoCosteVenta + siguienteHijo;
                            strHijoCosteVenta = strHijoCosteVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoCosteVenta = strHijoCosteVenta.Replace("__importeCapitulo__", ((-1) * dec_importeCosteVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCosteVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCosteVenta = strHijoCosteVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoCosteVenta = strHijoCosteVenta + finHijo;
                }

                // Porte
                strHijoCostePorte = "";
                if (cantidadPorte > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadPorte - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoCostePorte = inicioHijo + primerHijo;
                            strHijoCostePorte = strHijoCostePorte.Replace("__CAPITULO__", capitulosPorte.ElementAt(cantidad));
                            strHijoCostePorte = strHijoCostePorte.Replace("__importeCapitulo__", ((-1) * dec_importeCostePorteCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCostePorteCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCostePorte = strHijoCostePorte.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoCostePorte = strHijoCostePorte + siguienteHijo;
                            strHijoCostePorte = strHijoCostePorte.Replace("__CAPITULO__", capitulosPorte.ElementAt(cantidad));
                            strHijoCostePorte = strHijoCostePorte.Replace("__importeCapitulo__", ((-1) * dec_importeCostePorteCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeCostePorteCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoCostePorte = strHijoCostePorte.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoCostePorte = strHijoCostePorte + finHijo;
                }

                // MARGEN //
                importeMargen = importeMargenVenta + importeMargenAlquiler + importeMargenPorte;

                if (importeFacturacion == 0)
                {
                    porcentajeMargen = 0;
                    porcentajeMargenAlquiler = 0;
                    porcentajeMargenVenta = 0;
                    porcentajeMargenPorte = 0;
                }
                else
                {
                    porcentajeMargen = (importeMargen * 100) / importeFacturacion;
                    porcentajeMargenAlquiler = (importeMargenAlquiler * 100 / importeFacturacion);
                    porcentajeMargenVenta = (importeMargenVenta * 100 / importeFacturacion);
                    porcentajeMargenPorte = (importeMargenPorte * 100 / importeFacturacion);
                }

                strHijoMargenAlquiler = "";
                if (cantidadAlquiler > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadAlquiler - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoMargenAlquiler = inicioHijo + primerHijo;
                            strHijoMargenAlquiler = strHijoMargenAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoMargenAlquiler = strHijoMargenAlquiler.Replace("__importeCapitulo__", dec_importeMargenAlquilerCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeMargenAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoMargenAlquiler = strHijoMargenAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoMargenAlquiler = strHijoMargenAlquiler + siguienteHijo;
                            strHijoMargenAlquiler = strHijoMargenAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoMargenAlquiler = strHijoMargenAlquiler.Replace("__importeCapitulo__", dec_importeMargenAlquilerCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeMargenAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoMargenAlquiler = strHijoMargenAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoMargenAlquiler = strHijoMargenAlquiler + finHijo;
                }

                strHijoMargenVenta = "";
                if (cantidadVenta > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVenta - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoMargenVenta = inicioHijo + primerHijo;
                            strHijoMargenVenta = strHijoMargenVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoMargenVenta = strHijoMargenVenta.Replace("__importeCapitulo__", dec_importeMargenVentaCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeMargenVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoMargenVenta = strHijoMargenVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoMargenVenta = strHijoMargenVenta + siguienteHijo;
                            strHijoMargenVenta = strHijoMargenVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoMargenVenta = strHijoMargenVenta.Replace("__importeCapitulo__", dec_importeMargenVentaCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeMargenVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoMargenVenta = strHijoMargenVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoMargenVenta = strHijoMargenVenta + finHijo;
                }


                strHijoMargenPorte = "";
                if (cantidadPorte > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadPorte - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoMargenPorte = inicioHijo + primerHijo;
                            strHijoMargenPorte = strHijoMargenPorte.Replace("__CAPITULO__", capitulosPorte.ElementAt(cantidad));
                            strHijoMargenPorte = strHijoMargenPorte.Replace("__importeCapitulo__", dec_importeMargenPorteCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeMargenPorteCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoMargenPorte = strHijoMargenPorte.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoMargenPorte = strHijoMargenPorte + siguienteHijo;
                            strHijoMargenPorte = strHijoMargenPorte.Replace("__CAPITULO__", capitulosPorte.ElementAt(cantidad));
                            strHijoMargenPorte = strHijoMargenPorte.Replace("__importeCapitulo__", dec_importeMargenPorteCapitulos.ElementAt(cantidad).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeMargenPorteCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoMargenPorte = strHijoMargenPorte.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoMargenPorte = strHijoMargenPorte + finHijo;
                }


                // GASTOS VARIABLES
                importeGastosVariables = (-1) * (importeVentaGastosVariables + importeAlquilerGastosVariables);

                if (importeFacturacion == 0)
                {
                    porcentajeGastosVariables = 0;
                    porcentajeAlquilerGastosVariables = 0;
                    porcentajeVentaGastosVariables = 0;
                }
                else
                {
                    porcentajeGastosVariables = (importeGastosVariables * 100) / importeFacturacion;
                    porcentajeAlquilerGastosVariables = (importeAlquilerGastosVariables * 100 / importeFacturacion);
                    porcentajeVentaGastosVariables = (importeVentaGastosVariables * 100 / importeFacturacion);
                }


                importeAlquilerGastosVariables = (-1) * importeAlquilerGastosVariables;
                importeVentaGastosVariables = (-1) * importeVentaGastosVariables;

                strHijoGastosVariablesAlquiler = "";
                if (cantidadAlquiler > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadAlquiler - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoGastosVariablesAlquiler = inicioHijo + primerHijo;
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler.Replace("__importeCapitulo__", ((-1) * dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler + siguienteHijo;
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler.Replace("__importeCapitulo__", ((-1) * dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoGastosVariablesAlquiler = strHijoGastosVariablesAlquiler + finHijo;
                }

                strHijoGastosVariablesVenta = "";
                if (cantidadVenta > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVenta - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoGastosVariablesVenta = inicioHijo + primerHijo;
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta.Replace("__importeCapitulo__", ((-1) * dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta + siguienteHijo;
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta.Replace("__importeCapitulo__", ((-1) * dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosVariablesVenta = strHijoGastosVariablesVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoGastosVariablesVenta = strHijoGastosVariablesVenta + finHijo;
                }

                // GASTOS FIJOS
                importeGastosFijos = (-1) * (importeVentaGastosFijosBU + importeAlquilerGastosFijosBU + importeVentaGastosFijosCentrales + importeAlquilerGastosFijosCentrales);

                if (importeFacturacion == 0)
                {
                    porcentajeGastosFijos = 0;
                    porcentajeAlquilerGastosFijosBU = 0;
                    porcentajeVentaGastosFijosBU = 0;
                    porcentajeAlquilerGastosFijosCentrales = 0;
                    porcentajeVentaGastosFijosCentrales = 0;
                }
                else
                {
                    porcentajeGastosFijos = (importeGastosFijos * 100) / importeFacturacion;
                    porcentajeAlquilerGastosFijosBU = (-1) * (importeAlquilerGastosFijosBU * 100 / importeFacturacion);
                    porcentajeVentaGastosFijosBU = (-1) * (importeVentaGastosFijosBU * 100 / importeFacturacion);
                    porcentajeAlquilerGastosFijosCentrales = (-1) * (importeAlquilerGastosFijosCentrales * 100 / importeFacturacion);
                    porcentajeVentaGastosFijosCentrales = (-1) * (importeVentaGastosFijosCentrales * 100 / importeFacturacion);
                }

                importeGastosFijosBU = (-1) * (importeVentaGastosFijosBU + importeAlquilerGastosFijosBU);
                importeVentaGastosFijosBU = (-1) * importeVentaGastosFijosBU;
                importeAlquilerGastosFijosBU = (-1) * importeAlquilerGastosFijosBU;

                importeGastosFijosCentrales = (-1) * (importeVentaGastosFijosCentrales + importeAlquilerGastosFijosCentrales);
                importeVentaGastosFijosCentrales = (-1) * importeVentaGastosFijosCentrales;
                importeAlquilerGastosFijosCentrales = (-1) * importeAlquilerGastosFijosCentrales;

                // GASTOS FIJOS BU
                strHijoGastosFijosBUAlquiler = "";
                if (cantidadAlquiler > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadAlquiler - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoGastosFijosBUAlquiler = inicioHijo + primerHijo;
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler + siguienteHijo;
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoGastosFijosBUAlquiler = strHijoGastosFijosBUAlquiler + finHijo;
                }

                strHijoGastosFijosBUVenta = "";
                if (cantidadVenta > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVenta - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoGastosFijosBUVenta = inicioHijo + primerHijo;
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta + siguienteHijo;
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoGastosFijosBUVenta = strHijoGastosFijosBUVenta + finHijo;
                }

                // GASTOS FIJOS CENTRALES
                strHijoGastosFijosCentralesAlquiler = "";
                if (cantidadAlquiler > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadAlquiler - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoGastosFijosCentralesAlquiler = inicioHijo + primerHijo;
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler + siguienteHijo;
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoGastosFijosCentralesAlquiler = strHijoGastosFijosCentralesAlquiler + finHijo;
                }

                strHijoGastosFijosCentralesVenta = "";
                if (cantidadVenta > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVenta - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoGastosFijosCentralesVenta = inicioHijo + primerHijo;
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta + siguienteHijo;
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta.Replace("__importeCapitulo__", ((-1) * dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = (dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad) * 100 / importeFacturacion);
                            strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoGastosFijosCentralesVenta = strHijoGastosFijosCentralesVenta + finHijo;
                }

                // ROP BÁSICO //
                importeROPBasico = importeMargenVenta + importeMargenAlquiler + importeMargenPorte + importeGastosVariables + importeGastosFijosBU + importeGastosFijosCentrales;

                if (importeFacturacion == 0)
                {
                    porcentajeROPBasico = 0;
                    porcentajeROPBasicoAlquiler = 0;
                    porcentajeROPBasicoVenta = 0;
                }
                else
                {
                    porcentajeROPBasico = (importeROPBasico * 100) / importeFacturacion;
                    porcentajeROPBasicoAlquiler = (importeROPBasicoAlquiler * 100 / importeFacturacion);
                    porcentajeROPBasicoVenta = (importeROPBasicoVenta * 100 / importeFacturacion);
                }

                importeROPBasicoAlquiler = importeMargenAlquiler + importeAlquilerGastosVariables + importeAlquilerGastosFijosBU + importeAlquilerGastosFijosCentrales;
                importeROPBasicoVenta = importeMargenVenta + importeVentaGastosVariables + importeVentaGastosFijosBU + importeVentaGastosFijosCentrales;
                
                strHijoROPBasicoAlquiler = "";
                if (cantidadAlquiler > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadAlquiler - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoROPBasicoAlquiler = inicioHijo + primerHijo;
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler.Replace("__importeCapitulo__", (dec_importeMargenAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = ((dec_importeMargenAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad)) * 100 / importeFacturacion);
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler + siguienteHijo;
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler.Replace("__CAPITULO__", capitulosAlquiler.ElementAt(cantidad));
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler.Replace("__importeCapitulo__", (dec_importeMargenAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = ((dec_importeMargenAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUAlquilerCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesAlquilerCapitulos.ElementAt(cantidad)) * 100 / importeFacturacion);
                            strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoROPBasicoAlquiler = strHijoROPBasicoAlquiler + finHijo;
                }

                strHijoROPBasicoVenta = "";
                if (cantidadVenta > 0)
                {
                    for (cantidad = 0; cantidad <= cantidadVenta - 1; cantidad++)
                    {
                        if (cantidad == 0)
                        {
                            strHijoROPBasicoVenta = inicioHijo + primerHijo;
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta.Replace("__importeCapitulo__", (dec_importeMargenVentaCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = ((dec_importeMargenVentaCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad)) * 100 / importeFacturacion);
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                        else
                        {
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta + siguienteHijo;
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta.Replace("__CAPITULO__", capitulosVenta.ElementAt(cantidad));
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta.Replace("__importeCapitulo__", (dec_importeMargenVentaCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad)).ToString("#,##0.00"));
                            if (importeFacturacion == 0)
                                porcentaje = 0;
                            else
                                porcentaje = ((dec_importeMargenVentaCapitulos.ElementAt(cantidad) - dec_importeGastosVariablesVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosBUVentaCapitulos.ElementAt(cantidad) - dec_importeGastosFijosCentralesVentaCapitulos.ElementAt(cantidad)) * 100 / importeFacturacion);
                            strHijoROPBasicoVenta = strHijoROPBasicoVenta.Replace("__porcentajeCapitulo__", Decimal.Round(porcentaje, 2).ToString("#,##0.00"));
                        }
                    }
                    strHijoROPBasicoVenta = strHijoROPBasicoVenta + finHijo;
                }

                armarJson(false);

                //TABLA TIEMPO
                dataTiempos.DataSource = null;
                dataTiempos.Columns.Clear();

                DataTable dtTiempos = new DataTable();

                dtTiempos.Columns.Add("CONCEPTO");
                dtTiempos.Columns.Add("TIEMPO");

                DataRow oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO TOTAL";
                oItemTiempo[1] = horaTotal_2.Minutes.ToString("00") + ":" + horaTotal_2.Seconds.ToString("00") + ":" + horaTotal_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemTiempo);

                DataRow oItemLlamada = dtTiempos.NewRow();
                oItemLlamada[0] = "TIEMPO LLAMADA";
                oItemLlamada[1] = horaLlamada_2.Minutes.ToString("00") + ":" + horaLlamada_2.Seconds.ToString("00") + ":" + horaLlamada_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemLlamada);

                DataRow oItemTiempoFecha = dtTiempos.NewRow();
                oItemTiempoFecha[0] = "TIEMPO FECHA BASE";
                oItemTiempoFecha[1] = horaFechaBase_2.Minutes.ToString("00") + ":" + horaFechaBase_2.Seconds.ToString("00") + ":" + horaFechaBase_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemTiempoFecha);

                DataRow oItemOferta = dtTiempos.NewRow();
                oItemOferta[0] = "TIEMPO FECHA OFERTA";
                oItemOferta[1] = horaFechaOferta_2.Minutes.ToString("00") + ":" + horaFechaOferta_2.Seconds.ToString("00") + ":" + horaFechaOferta_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemOferta);

                DataRow oItemTiempoPrecios = dtTiempos.NewRow();
                oItemTiempoPrecios[0] = "TIEMPO PRECIOS";
                oItemTiempoPrecios[1] = horaPrecios_2.Minutes.ToString("00") + ":" + horaPrecios_2.Seconds.ToString("00") + ":" + horaPrecios_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemTiempoPrecios);

                DataRow oItemConfiguracion = dtTiempos.NewRow();
                oItemConfiguracion[0] = "TIEMPO CONFIGURACION";
                oItemConfiguracion[1] = horaDatosConfigurados_2.Minutes.ToString("00") + ":" + horaDatosConfigurados_2.Seconds.ToString("00") + ":" + horaDatosConfigurados_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemConfiguracion);

                DataRow oItemDatosConfiguracion = dtTiempos.NewRow();
                oItemDatosConfiguracion[0] = "TIEMPO DATOS TAX";
                oItemDatosConfiguracion[1] = horaDatosConfigurados_4.Minutes.ToString("00") + ":" + horaDatosConfigurados_4.Seconds.ToString("00") + ":" + horaDatosConfigurados_4.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemDatosConfiguracion);

                DataRow oItemPase1 = dtTiempos.NewRow();
                oItemPase1[0] = "TIEMPO PASE 1";
                oItemPase1[1] = horaDatosPase1_2.Minutes.ToString("00") + ":" + horaDatosPase1_2.Seconds.ToString("00") + ":" + horaDatosPase1_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemPase1);

                DataRow oItemPase2 = dtTiempos.NewRow();
                oItemPase2[0] = "TIEMPO PASE 2";
                oItemPase2[1] = horaDatosPase2_2.Minutes.ToString("00") + ":" + horaDatosPase2_2.Seconds.ToString("00") + ":" + horaDatosPase2_2.Milliseconds.ToString("00");
                dtTiempos.Rows.Add(oItemPase2);

                dataTiempos.DataSource = dtTiempos;
                dataTiempos.DataBind();

                dataTiempos.Visible = true;
            }
            catch (Exception ex)
            {
                string dondeVa = "";
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Pintar datos // " + dondeVa + " // " + ex.Message;
            }
        }

        protected DataTable localizarPreciosArticulos(string empresa, string delegacion, string articulos, string moneda, string articulosCambio, string ficha)
        {
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosLPA = new System.Data.DataTable();

            try
            {
                horaPrecios_1 = DateTime.Now;
                conexiones.crearConexionBI();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_BI_PreciosArticulos";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@empresa", empresa);
                if (delegacion =="")
                    conexiones.comando.Parameters.AddWithValue("@delegacion", DBNull.Value);
                else
                    conexiones.comando.Parameters.AddWithValue("@delegacion", delegacion);
                conexiones.comando.Parameters.AddWithValue("@articulos", articulos);
                conexiones.comando.Parameters.AddWithValue("@moneda", moneda);
                conexiones.comando.Parameters.AddWithValue("@articulosCambio", articulosCambio);
                if (ficha == "")
                    conexiones.comando.Parameters.AddWithValue("@ficha", DBNull.Value);
                else
                    conexiones.comando.Parameters.AddWithValue("@ficha", ficha);

                adaptadorArticulos = new SqlDataAdapter(conexiones.comando);
                adaptadorArticulos.Fill(dtArticulosLPA);
                adaptadorArticulos.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                horaPrecios_2 = DateTime.Now.Subtract(horaPrecios_1);
                return dtArticulosLPA;
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "ERROR localizar precios articulos // " + ex.Message + " // " + articulos;
                return dtArticulosLPA;
            }
        }

        protected DataTable localizarEstadoArticulos(string usuarioSQL)
        {
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosEstado = new System.Data.DataTable();

            try
            {
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_EstadoArticulos";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@usuario", usuarioSQL);

                adaptadorArticulos = new SqlDataAdapter(conexiones.comando);
                adaptadorArticulos.Fill(dtArticulosEstado);
                adaptadorArticulos.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                return dtArticulosEstado;
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "ERROR localizar estado articulos // " + ex.Message;
                return dtArticulosEstado;
            }
        }


        protected void fechaOferta(ref DateTime fechaPrecios, string idOferta)
        {
            try
            {
                horaFechaOferta_1 = DateTime.Now;
                System.Collections.IEnumerator enumerator_1;

                tablaOfertas.CallContext contexto_1 = new tablaOfertas.CallContext();
                contexto_1.Company = cmbEmpresa.SelectedItem.ToString();

                tablaOfertas.QueryCriteria criterio_1 = new tablaOfertas.QueryCriteria();
                criterio_1.CriteriaElement = new tablaOfertas.CriteriaElement[1];

                criterio_1.CriteriaElement[0] = new tablaOfertas.CriteriaElement();
                criterio_1.CriteriaElement[0].FieldName = "QuotationId";
                criterio_1.CriteriaElement[0].DataSourceName = "SalesQuotationMasterTable";
                criterio_1.CriteriaElement[0].Operator = tablaOfertas.Operator.Equal;
                criterio_1.CriteriaElement[0].Value1 = idOferta;

                tablaOfertas.SalesQuotationsServiceClient proxy_1 = new tablaOfertas.SalesQuotationsServiceClient();
                proxy_1.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_1.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_1.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                tablaOfertas.AxdSalesQuotations axdTablaOfertas_1 = proxy_1.find(contexto_1, criterio_1);
                if (axdTablaOfertas_1.SalesQuotationMasterTable is null)
                {
                    proxy_1.Close();
                }
                else
                {
                    enumerator_1 = axdTablaOfertas_1.SalesQuotationMasterTable.GetEnumerator();

                    while (enumerator_1.MoveNext())
                    {
                        tablaOfertas.AxdEntity_SalesQuotationMasterTable axdEntity_SalesQuotationMasterTable = (tablaOfertas.AxdEntity_SalesQuotationMasterTable)enumerator_1.Current;

                        if (axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value.ToString().Length > 0)
                            fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value);
                        break;
                    }
                    proxy_1.Close();
                }
                horaFechaOferta_2 = DateTime.Now.Subtract(horaFechaOferta_1);
            }
            catch (Exception ex)
            {
                //Session["mensajeError"] = ex.Message;
                //Response.Redirect("ErrorPage.aspx");
            }
        }

        protected void fechaBaseFichaArticulo(string obraID, int diasDiferencia, int diasRestar)
        {
            string whereGo = "";
            bool datosIniciales = false;
            System.Collections.IEnumerator enumerator_1;
            string pedidos = "";
            string pedido = "";
            int lineaCapitulo;
            int numeroArticulos = 0;
            DateTime fechaArticulo;
            DateTime fechaOfertaPedido;

            try
            {
                horaFechaBase_1 = DateTime.Now;

                tablaPedidos.CallContext contexto_1 = new tablaPedidos.CallContext();
                contexto_1.Company = cmbEmpresa.SelectedItem.ToString();

                tablaPedidos.QueryCriteria criterio_1 = new tablaPedidos.QueryCriteria();
                criterio_1.CriteriaElement = new tablaPedidos.CriteriaElement[1];

                criterio_1.CriteriaElement[0] = new tablaPedidos.CriteriaElement();
                criterio_1.CriteriaElement[0].FieldName = "ObraId";
                criterio_1.CriteriaElement[0].DataSourceName = "SalesTable";
                criterio_1.CriteriaElement[0].Operator = tablaPedidos.Operator.Equal;
                criterio_1.CriteriaElement[0].Value1 = obraID;

                tablaPedidos.SalesOrdersServiceClient proxy_1 = new tablaPedidos.SalesOrdersServiceClient();
                proxy_1.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_1.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_1.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                whereGo = " - proxy - ";
                tablaPedidos.AxdSalesOrders axdtablaPedidos_1 = proxy_1.find(contexto_1, criterio_1);
                if (axdtablaPedidos_1.SalesTable is null)
                {
                    proxy_1.Close();
                }
                else
                {
                    whereGo = " - enumerator - ";
                    enumerator_1 = axdtablaPedidos_1.SalesTable.GetEnumerator();

                    while (enumerator_1.MoveNext())
                    {
                        whereGo = " - enumerator 2 - ";
                        tablaPedidos.AxdEntity_SalesTable AxdEntity_SalesTable = (tablaPedidos.AxdEntity_SalesTable)enumerator_1.Current;
                        whereGo = " - pedidos - ";

                        if (!datosIniciales && !String.IsNullOrEmpty(AxdEntity_SalesTable.QuotationId))
                        {
                            whereGo = " - recoge datos - ";
                            fechaOfertaPedido = new DateTime();
                            fechaOferta(ref fechaOfertaPedido, AxdEntity_SalesTable.QuotationId.ToString());
                            listaOfertas.Add(AxdEntity_SalesTable.QuotationId.ToString());
                            listaFechasOferta.Add(fechaOfertaPedido);
                            datosIniciales = true;
                        }

                        pedidos = pedidos + " - " + AxdEntity_SalesTable.SalesId.ToString();
                        pedido = AxdEntity_SalesTable.SalesId.ToString();
                        tablaPedidos.AxdEntity_SalesLine[] axdEntity_SalesQuotationTables = AxdEntity_SalesTable.SalesLine;
                        tablaPedidos.AxdEntity_SalesLine axdEntity_SalesQuotationTable;

                        fechaOfertaPedido = new DateTime();

                        if (!String.IsNullOrEmpty(AxdEntity_SalesTable.QuotationId))
                        {
                            if (!listaOfertas.Contains(AxdEntity_SalesTable.QuotationId.ToString()))
                            {
                                // llamada una sóla vez
                                //fechaOferta(ref fechaOfertaPedido, AxdEntity_SalesTable.QuotationId.ToString());
                                //listaOfertas.Add(AxdEntity_SalesTable.QuotationId.ToString());
                                //listaFechasOferta.Add(fechaOfertaPedido);
                            }
                            else
                            {
                                numeroArticulos = listaOfertas.FindIndex(x => x.Equals(AxdEntity_SalesTable.QuotationId.ToString()));
                                if (numeroArticulos >= 0)
                                    fechaOfertaPedido = listaFechasOferta[numeroArticulos];
                            }
                        }

                        if (axdEntity_SalesQuotationTables is null)
                        {

                        }
                        {
                            for (lineaCapitulo = 0; lineaCapitulo < axdEntity_SalesQuotationTables.Length; lineaCapitulo++)
                            {
                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[lineaCapitulo];
                                if (listaArticulosPedido.Contains(axdEntity_SalesQuotationTable.ItemId.ToString()))
                                {
                                    // calcular la fecha para esta artículo
                                    fechaArticulo = new DateTime();

                                    if (!String.IsNullOrEmpty(AxdEntity_SalesTable.QuotationId))
                                    {
                                        if (fechaOfertaPedido != new DateTime() && axdEntity_SalesQuotationTable.CreatedDateTime.ToString().Length > 0)
                                        {
                                            if (Math.Abs(Convert.ToDateTime(AxdEntity_SalesTable.CreatedDateTime.Value).Subtract(fechaOfertaPedido).Days) > diasDiferencia)
                                            {
                                                fechaArticulo = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value).AddDays((-1) * diasRestar);
                                            }
                                            else
                                            {
                                                fechaArticulo = fechaOfertaPedido;
                                            }
                                        }
                                        else
                                        {
                                            if (axdEntity_SalesQuotationTable.CreatedDateTime.ToString().Length > 0)
                                            {
                                                fechaArticulo = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fechaArticulo = Convert.ToDateTime(AxdEntity_SalesTable.CreatedDateTime.Value);
                                    }

                                    if (fechaArticulo != new DateTime())
                                    {
                                        numeroArticulos = listaArticulosPedido.FindIndex(x => x.Equals(axdEntity_SalesQuotationTable.ItemId.ToString()));
                                        if (numeroArticulos >= 0)
                                            if (listaFechasPedido.ElementAt(numeroArticulos) > fechaArticulo)
                                                listaFechasPedido[numeroArticulos] = fechaArticulo;
                                    }
                                }
                            }
                        }
                    }
                    proxy_1.Close();
                }
                horaFechaBase_2 = DateTime.Now.Subtract(horaFechaBase_1);
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = obraID + " // fechaBaseFichaArticulo // " + whereGo + " // " + ex.Message;
            }
        }

        protected void calcularOferta()
        {
            string dondeVa = "";

            try
            {
                SqlDataAdapter adaptadorDatosConfiguracion;
                System.Data.DataTable dtDatosConfiguracion;
                int lineasBonificacion;
                int duracion = 0;
                int diasCalculados = 0;
                int diasMultiplicar = 0;
                int diasDesdeTaxNoConsumible = 0;
                int diasHastaTaxNoConsumible = 0;
                int diasTaximetroNoConsumibleCalcular = 0;

                dataDatos.DataSource = null;
                dataDatos.Columns.Clear();
                dataTiempos.DataSource = null;
                dataTiempos.Columns.Clear();

                dtTaximetro = new DataTable();

                dtTaximetro.Columns.Add("CAPITULO");
                dtTaximetro.Columns.Add("TIPO CAPITULO");
                dtTaximetro.Columns.Add("ITEM");
                dtTaximetro.Columns.Add("NUEVO/USADO");
                dtTaximetro.Columns.Add("COEF. NUEVO");
                dtTaximetro.Columns.Add("COEF. USADO");
                dtTaximetro.Columns.Add("CANTIDAD");
                dtTaximetro.Columns.Add("EURO/UD/DIA");
                dtTaximetro.Columns.Add("DURACION ESTIMADA");
                dtTaximetro.Columns.Add("FACT ALQUILER");
                dtTaximetro.Columns.Add("FACT TOTAL VENTA");
                dtTaximetro.Columns.Add("FACT VENTA SERVICIO");
                dtTaximetro.Columns.Add("FACT VENTA PRODUCTO");
                dtTaximetro.Columns.Add("CANTIDAD DIAS");
                dtTaximetro.Columns.Add("FECHA PS");
                dtTaximetro.Columns.Add("PS/UD");
                dtTaximetro.Columns.Add("PS/UD ORIGINAL");
                dtTaximetro.Columns.Add("PS Corrección N");
                dtTaximetro.Columns.Add("PS Corrección U");
                dtTaximetro.Columns.Add("FECHA CAMBIO");
                dtTaximetro.Columns.Add("CAMBIO");
                dtTaximetro.Columns.Add("TIPO ARTICULO");
                dtTaximetro.Columns.Add("SUPERFICIE");
                dtTaximetro.Columns.Add("COSTE SUPERFICIE");
                dtTaximetro.Columns.Add("PRECIO CONSUMIBLE");
                dtTaximetro.Columns.Add("PRECIO NO CONSUMIBLE");
                dtTaximetro.Columns.Add("% TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("IMPORTE TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("% AJUSTE");
                dtTaximetro.Columns.Add("AJUSTE TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("CANTIDAD DIAS TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("DIAS DESDE TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("DIAS HASTA TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("% TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("GASTO VARIABLE");
                dtTaximetro.Columns.Add("IMPORTE GASTO VARIABLE");
                dtTaximetro.Columns.Add("GASTO FIJO BU");
                dtTaximetro.Columns.Add("IMPORTE GASTO FIJO BU");
                dtTaximetro.Columns.Add("GASTO FIJO CENTRAL");
                dtTaximetro.Columns.Add("IMPORTE GASTO FIJO CENTRAL");

                decimal coeficienteNuevo = 0;
                decimal coeficienteUsado = 0;
                decimal coeficienteUsar = 0;
                decimal coeficienteMixto = 0;
                string tipoArticulo = "";

                decimal gastosVariablesAlquiler = 0;
                decimal gastosVariablesVenta = 0;
                decimal gastosFijosBUAlquiler = 0;
                decimal gastosFijosBUVenta = 0;
                decimal gastosFijosCentralesAlquiler = 0;
                decimal gastosFijosCentralesVenta = 0;

                DateTime fechaPrecio=DateTime.Now;
                bool metersInvoicing = false;
                string productType = "";
                bool calcularPorSuperficie = false;
                decimal superficie = 0;
                SqlDataReader dr;
                decimal costeSuperficie = 0;
                string articulosConfiguracion = "";
                decimal taximetroNoConsumible = 0;
                decimal taximetroConsumible = 0;
                decimal bonificacionTaximetroConsumible = 0;
                decimal precioTaximetroNoConsumible = 0;
                decimal precioTaximetroConsumible = 0;
                string tabla = "";
                string IDCapitulo = "";
                string campo = "";
                int dias = 0;
                int diasxMes = 0;
                int diasDiferencia = 0;
                int diasRestar = 0;
                DateTime fechaPrecios;
                bool hayPrecio = false;
                bool primeraVez = true;
                int lineaCapitulo;
                int lineaProducto;
                System.Data.DataTable dtArticulos; 
                DataRow[] filaEncontrada;
                string articulos = "";
                string articulosCambio = "";
                decimal precioCoste = 0;
                decimal importeFacturacionAlquilerCapitulos = 0;
                decimal importeCosteTaximetroNoConsumibleCapitulos = 0;
                decimal importeCosteTaximetroConsumibleCapitulos = 0;
                decimal importeCosteBonificacionTaximetroConsumibleCapitulos = 0;
                decimal importeFacturacionVentaCapitulos = 0;
                decimal importeFacturacionVentaServicioCapitulos = 0;
                decimal importeFacturacionVentaProductoCapitulos = 0;
                decimal importeCosteVentaCapitulos = 0;
                decimal importeFacturacionPorteCapitulos = 0;
                decimal importeCostePorteCapitulos = 0;
                decimal importeGastosVariablesVentaCapitulos = 0;
                decimal importeGastosVariablesAlquilerCapitulos = 0;
                decimal importeGastosFijosBUVentaCapitulos = 0;
                decimal importeGastosFijosBUAlquilerCapitulos = 0;
                decimal importeGastosFijosCentralesVentaCapitulos = 0;
                decimal importeGastosFijosCentralesAlquilerCapitulos = 0;

                tablaOfertas.AxdEntity_SalesQuotationTable[] axdEntity_SalesQuotationTables;
                tablaOfertas.AxdEntity_SalesQuotationTable axdEntity_SalesQuotationTable;
                tablaOfertas.AxdEntity_SalesQuotationLine[] axdEntity_SalesQuotationLines;
                tablaOfertas.AxdEntity_SalesQuotationLine axdEntity_SalesQuotationLine;

                lblMensajeError.Visible = false;
                lblMensajeError.Text = "";
                txtNombreOferta.Text = "";
                horaTotal_1 = DateTime.Now;
                horaLlamada_1 = DateTime.Now;

                System.Collections.IEnumerator enumerator_1;

                // Parámetros
                dondeVa = "abrió sp_ROP_ConfiguracionFijaConsulta";
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionFijaConsulta";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                dr = conexiones.comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dondeVa = "dr sp_ROP_ConfiguracionFijaConsulta";
                    dr.Read();
                    diasxMes = Convert.ToInt32(dr["COF_diasCalculo"]);
                    diasDiferencia = Convert.ToInt32(dr["COF_OfertaDiasEntreFechaOfertaFechaCapitulo"]);
                    diasRestar = Convert.ToInt32(dr["COF_OfertaDiasRestarFechaCapítulo"]);
                }
                dr.Close();
                dr.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();
                dondeVa = "cerró sp_ROP_ConfiguracionFijaConsulta";

                // CABECERA
                cantidadAlquiler = 0;
                cantidadTaximetroNoConsumible = 0;
                cantidadTaximetroConsumible = 0;
                cantidadVenta = 0;
                cantidadPorte = 0;
                importeAlquiler = 0;
                importeVenta = 0;
                importeVentaServicio = 0;
                importeVentaProducto = 0;
                importePorte = 0;
                importeCosteTaximetroNoConsumible = 0;
                importeCosteTaximetroConsumible = 0;
                importeCosteVenta = 0;
                importeCostePorte = 0;
                importeMargenAlquiler = 0;
                importeMargenVenta = 0;
                importeMargenPorte = 0;

                fechaPrecios = new DateTime();

                dondeVa = "SalesQuotationMasterTable";
                tablaOfertas.CallContext contexto_1 = new tablaOfertas.CallContext();
                contexto_1.Company = cmbEmpresa.SelectedItem.ToString();

                tablaOfertas.QueryCriteria criterio_1 = new tablaOfertas.QueryCriteria();
                criterio_1.CriteriaElement = new tablaOfertas.CriteriaElement[1];

                criterio_1.CriteriaElement[0] = new tablaOfertas.CriteriaElement();
                criterio_1.CriteriaElement[0].FieldName = "QuotationId";
                criterio_1.CriteriaElement[0].DataSourceName = "SalesQuotationMasterTable";
                criterio_1.CriteriaElement[0].Operator = tablaOfertas.Operator.Equal;
                criterio_1.CriteriaElement[0].Value1 = txtNumero.Text;

                tablaOfertas.SalesQuotationsServiceClient proxy_1 = new tablaOfertas.SalesQuotationsServiceClient();
                proxy_1.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_1.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_1.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                tablaOfertas.AxdSalesQuotations axdTablaOfertas_1 = proxy_1.find(contexto_1, criterio_1);
                if (axdTablaOfertas_1.SalesQuotationMasterTable is null)
                {
                    proxy_1.Close();
                }
                else
                {
                    enumerator_1 = axdTablaOfertas_1.SalesQuotationMasterTable.GetEnumerator();
                    dtArticulos = new System.Data.DataTable();
                    horaLlamada_2 = DateTime.Now.Subtract(horaLlamada_1);

                    while (enumerator_1.MoveNext())
                    {
                        dondeVa = "Oferta";
                        tablaOfertas.AxdEntity_SalesQuotationMasterTable axdEntity_SalesQuotationMasterTable = (tablaOfertas.AxdEntity_SalesQuotationMasterTable)enumerator_1.Current;
                        datosGenerales = axdEntity_SalesQuotationMasterTable.QuotationId + " / " + axdEntity_SalesQuotationMasterTable.QuotationName + " / " + axdEntity_SalesQuotationMasterTable.CurrencyCode + " / " + axdEntity_SalesQuotationMasterTable.Delegation;
                        moneda = axdEntity_SalesQuotationMasterTable.CurrencyCode;
                      
                        // DATOS CONFIGURACION
                        conexiones.crearConexion();
                        conexiones.comando = conexiones.conexion.CreateCommand();
                        conexiones.comando.CommandText = "ROP_DatosConfiguracionGeneral";
                        conexiones.comando.CommandTimeout = 240000;
                        conexiones.comando.CommandType = CommandType.StoredProcedure;
                        conexiones.comando.Parameters.AddWithValue("@empresa", cmbEmpresa.SelectedItem.ToString());
                        conexiones.comando.Parameters.AddWithValue("@delegacion", axdEntity_SalesQuotationMasterTable.Delegation);
                        dr = conexiones.comando.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToString(dr["Concepto"]).ToUpper() == "GASTOS VARIABLES")
                                {
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "ALQUILER")
                                        gastosVariablesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "VENTA")
                                        gastosVariablesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (String.IsNullOrEmpty(Convert.ToString(dr["Tipo"])))
                                    {
                                        gastosVariablesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                        gastosVariablesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    }
                                }
                                if (Convert.ToString(dr["Concepto"]).ToUpper() == "GASTOS FIJOS BU")
                                {
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "ALQUILER")
                                        gastosFijosBUAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "VENTA")
                                        gastosFijosBUVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (String.IsNullOrEmpty(Convert.ToString(dr["Tipo"])))
                                    {
                                        gastosFijosBUAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                        gastosFijosBUVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    }
                                }
                                if (Convert.ToString(dr["Concepto"]).ToUpper() == "GASTOS FIJOS CENTRALES")
                                {
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "ALQUILER")
                                        gastosFijosCentralesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "VENTA")
                                        gastosFijosCentralesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (String.IsNullOrEmpty(Convert.ToString(dr["Tipo"])))
                                    {
                                        gastosFijosCentralesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                        gastosFijosCentralesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    }
                                }
                            }
                        }
                        dr.Close();
                        conexiones.comando.Dispose();
                        conexiones.conexion.Close();
                        conexiones.conexion.Dispose();

                        axdEntity_SalesQuotationTables = axdEntity_SalesQuotationMasterTable.SalesQuotationTable;

                        // Recoger los artículos de toda la oferta
                        if (axdEntity_SalesQuotationTables is null)
                            datosGenerales = "NO HAY DATOS DE LOS CAPÍTULOS";
                        else
                        {
                            articulos = ";";
                            articulosCambio = ";";
                            articulosConfiguracion = "";

                            horaDatosPase1_1 = DateTime.Now;
                            for (int capitulo = 0; capitulo < axdEntity_SalesQuotationTables.Length; capitulo++)
                            {
                                dondeVa = "recorrer capitulos " + capitulo.ToString();
                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[capitulo];

                                axdEntity_SalesQuotationLines = axdEntity_SalesQuotationTable.SalesQuotationLine;

                                if (axdEntity_SalesQuotationLines is null)
                                    datosGenerales = "NO HAY DATOS DE LAS LÍNEAS";
                                else
                                {
                                    for (lineaProducto = 0; lineaProducto < axdEntity_SalesQuotationLines.Length; lineaProducto++)
                                    {
                                        dondeVa = "recorrer productos " + lineaProducto.ToString();
                                        axdEntity_SalesQuotationLine = axdEntity_SalesQuotationLines[lineaProducto];

                                        // artículos si hay ficha
                                        if (!listaArticulosPedido.Contains(axdEntity_SalesQuotationLine.ItemId.ToString()))
                                        {
                                            listaArticulosPedido.Add(axdEntity_SalesQuotationLine.ItemId.ToString());
                                            if (axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value.ToString().Length > 0)
                                                listaFechasPedido.Add(Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value));
                                            else
                                                listaFechasPedido.Add(new DateTime());
                                        }

                                        // artículos si no hay ficha
                                        if (!listaArticulosSinFicha.Contains(axdEntity_SalesQuotationLine.ItemId.ToString()))
                                        {
                                            articulosConfiguracion = articulosConfiguracion + axdEntity_SalesQuotationLine.ItemId.ToString() + "|";
                                            listaArticulosSinFicha.Add(axdEntity_SalesQuotationLine.ItemId.ToString());
                                        }
                                    }
                                    dondeVa = "fin recorrer productos";

                                    // Calculo de la fecha a utilizar
                                    //o   ¿La oferta está asociada a una ficha ?
                                    //	SI:
                                    //•	Fecha base = Fecha base ficha
                                    //•	Consultar tabla BaseDateItemObra
                                    //	NO:
                                    //•	¿Existe cabecera de oferta? (RefQuotationMaster)
                                    //o   NO: Fecha base = Fecha creación capitulo
                                    //o SI: ¿fecha creación capítulo > p1 días fecha creación cabecera?
                                    //	NO: fecha base = fecha creación capitulo
                                    //	SI: fecha base = fecha creación capitulo – p2 días
                                    //if (String.IsNullOrEmpty(axdEntity_SalesQuotationMasterTable.ObraId))
                                    //{
                                        dondeVa = "no hay ficha";
                                        fechaPrecios = new DateTime();
                                        if (axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value.ToString().Length > 0 && axdEntity_SalesQuotationTable.CreatedDateTime.Value.ToString().Length > 0)
                                        {
                                            dias = Math.Abs(Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value).Subtract(Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value)).Days);
                                            if (Math.Abs(Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value).Subtract(Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value)).Days) > diasDiferencia)
                                            {
                                                fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value).AddDays((-1) * diasRestar);
                                                tabla = "SalesQuotationMasterTable";
                                                IDCapitulo = axdEntity_SalesQuotationTable.QuotationId;
                                                campo = "CreatedDateTime - " + diasRestar.ToString();
                                            }
                                            else
                                            {
                                                fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value);
                                                tabla = "SalesQuotationMasterTable";
                                                IDCapitulo = axdEntity_SalesQuotationTable.QuotationId;
                                                campo = "CreatedDateTime";
                                            }
                                        }
                                        else
                                        {
                                            if (axdEntity_SalesQuotationTable.CreatedDateTime.Value.ToString().Length > 0)
                                            {
                                                fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value);
                                                tabla = "SalesQuotationMasterTable";
                                                IDCapitulo = axdEntity_SalesQuotationTable.QuotationId;
                                                campo = "CreatedDateTime";
                                            }
                                        }

                                        for (lineaCapitulo = 0; lineaCapitulo < listaArticulosSinFicha.Count; lineaCapitulo++)
                                        {
                                            if (String.IsNullOrEmpty(axdEntity_SalesQuotationMasterTable.ObraId))
                                                articulos = articulos + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";
                                            articulosCambio = articulosCambio + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";
                                        }
                                        // Fin calculo de la fecha a utilizar
                                    //}
                                }
                            }
                            horaDatosPase1_2 = DateTime.Now.Subtract(horaDatosPase1_1);

                            // Calculo de la fecha a utilizar
                            //o   ¿La oferta está asociada a una ficha ?
                            //	SI:
                            //•	Fecha base = Fecha base ficha
                            //•	Consultar tabla BaseDateItemObra
                            if (!String.IsNullOrEmpty(axdEntity_SalesQuotationMasterTable.ObraId))
                            {
                                fechaPrecios = new DateTime();
                                //if (chkFecha.Checked)
                                //{
                                dondeVa = "fecha base ficha artículo:entró";
                                fechaBaseFichaArticulo(axdEntity_SalesQuotationMasterTable.ObraId.ToString(), diasDiferencia, diasRestar);
                                dondeVa = "fecha base ficha artículo: salió";

                                fechaPrecios = DateTime.Now;
                                tabla = "fechaBaseFichaArticulo";
                                IDCapitulo = "Ficha " + axdEntity_SalesQuotationMasterTable.ObraId;
                                campo = "fechaBaseFichaArticulo";

                                articulos = ";";
                                for (lineaCapitulo = 0; lineaCapitulo < listaArticulosPedido.Count; lineaCapitulo++)
                                    articulos = articulos + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";
                            }

                            if (fechaPrecios != new DateTime())
                            {
                                conexiones.crearConexion();
                                conexiones.comando = conexiones.conexion.CreateCommand();
                                conexiones.comando.CommandText = "sp_ROP_FechaBaseOfertaIncluir";
                                conexiones.comando.CommandType = CommandType.StoredProcedure;
                                conexiones.comando.Parameters.AddWithValue("@FBO_Oferta", txtNumero.Text);
                                conexiones.comando.Parameters.AddWithValue("@FBO_Capitulo", IDCapitulo);
                                conexiones.comando.Parameters.AddWithValue("@FBO_Fecha", fechaPrecios);
                                conexiones.comando.Parameters.AddWithValue("@FBO_Articulo", articulos.Replace("'", ""));
                                conexiones.comando.Parameters.AddWithValue("@FBO_TablaFechaSeleccionada", tabla);
                                conexiones.comando.Parameters.AddWithValue("@FBO_CampoFechaSeleccionada1", campo);
                                conexiones.comando.Parameters.AddWithValue("@FBO_DiasDiferencia", dias);
                                conexiones.comando.Parameters.AddWithValue("@primeraVez", primeraVez);
                                conexiones.comando.ExecuteNonQuery();
                                conexiones.conexion.Close();
                                primeraVez = false;

                                dondeVa = "localizar precios artículos";
                                dtArticulos = localizarPreciosArticulos(cmbEmpresa.SelectedItem.ToString(), axdEntity_SalesQuotationMasterTable.Delegation.ToString(), articulos, moneda, articulosCambio, "");
                                hayPrecio = true;
                            }

                            horaDatosConfigurados_1 = DateTime.Now;
                            datosSQL.datosConfigurados(cmbEmpresa.SelectedItem.ToString(), articulosConfiguracion, usuario);
                            horaDatosConfigurados_2 = DateTime.Now.Subtract(horaDatosConfigurados_1);

                            horaDatosPase2_1 = DateTime.Now;
                            for (int capitulo = 0; capitulo < axdEntity_SalesQuotationTables.Length; capitulo++)
                            {
                                dondeVa = "* recorrer capitulos " + capitulo.ToString();

                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[capitulo];
                                axdEntity_SalesQuotationLines = axdEntity_SalesQuotationTable.SalesQuotationLine;

                                if (axdEntity_SalesQuotationLines is null)
                                    datosGenerales = "NO HAY DATOS DE LAS LÍNEAS";
                                else
                                {
                                    axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[capitulo];

                                    importeFacturacionAlquilerCapitulos = 0;
                                    importeFacturacionVentaCapitulos = 0;
                                    importeFacturacionVentaServicioCapitulos = 0;
                                    importeFacturacionVentaProductoCapitulos = 0;
                                    importeGastosVariablesAlquilerCapitulos = 0;
                                    importeGastosVariablesVentaCapitulos = 0;

                                    importeGastosFijosBUAlquilerCapitulos = 0;
                                    importeGastosFijosBUVentaCapitulos = 0;

                                    importeGastosFijosCentralesAlquilerCapitulos = 0;
                                    importeGastosFijosCentralesVentaCapitulos = 0;

                                    importeCosteVentaCapitulos = 0;
                                    importeCosteTaximetroNoConsumibleCapitulos = 0;
                                    importeCosteTaximetroConsumibleCapitulos = 0;
                                    importeCosteBonificacionTaximetroConsumibleCapitulos = 0;

                                    // RECUPERAR LOS DATOS DE CONFIGURACIÓN: TAXIMETRO - GASTOS - AJUSTES
                                    dtDatosConfiguracion = new System.Data.DataTable();
                                    horaDatosConfigurados_3 = DateTime.Now;
                                    conexiones.crearConexion();
                                    conexiones.comando = conexiones.conexion.CreateCommand();
                                    conexiones.comando.CommandText = "ROP_DatosConfiguracion";
                                    conexiones.comando.CommandTimeout = 240000;
                                    conexiones.comando.CommandType = CommandType.StoredProcedure;
                                    conexiones.comando.Parameters.AddWithValue("@empresa", cmbEmpresa.SelectedItem.ToString());
                                    conexiones.comando.Parameters.AddWithValue("@usuario", usuario); // Environment.UserName);
                                    adaptadorDatosConfiguracion = new SqlDataAdapter(conexiones.comando);
                                    adaptadorDatosConfiguracion.Fill(dtDatosConfiguracion);
                                    adaptadorDatosConfiguracion.Dispose();
                                    conexiones.comando.Dispose();
                                    conexiones.conexion.Close();
                                    conexiones.conexion.Dispose();
                                    horaDatosConfigurados_4 = DateTime.Now.Subtract(horaDatosConfigurados_3);

                                    for (int producto = 0; producto < axdEntity_SalesQuotationLines.Length; producto++)
                                    {
                                        dondeVa = "* recorrer productos " + producto.ToString();

                                        axdEntity_SalesQuotationLine = axdEntity_SalesQuotationLines[producto];

                                        DataRow oItemTaximetro = dtTaximetro.NewRow();
                                        taximetroNoConsumible = 0;
                                        taximetroConsumible = 0;
                                        bonificacionTaximetroConsumible = 0;
                                        precioTaximetroNoConsumible = 0;
                                        precioTaximetroConsumible = 0;
                                        costeSuperficie = 0;
                                        superficie = 0;
                                        calcularPorSuperficie = false;
                                        metersInvoicing = false;
                                        productType = "";

                                        //horaDatosConfigurados_3 = DateTime.Now;
                                        //conexiones.crearConexion();
                                        //conexiones.comando = conexiones.conexion.CreateCommand();
                                        //conexiones.comando.CommandText = "ROP_DatosConfiguracion";
                                        //conexiones.comando.CommandTimeout = 240000;
                                        //conexiones.comando.CommandType = CommandType.StoredProcedure;
                                        //conexiones.comando.Parameters.AddWithValue("@empresa", cmbEmpresa.SelectedItem.ToString());
                                        //conexiones.comando.Parameters.AddWithValue("@usuario",  usuario); // Environment.UserName););
                                        //conexiones.comando.Parameters.AddWithValue("@articulo", axdEntity_SalesQuotationLine.ItemId.ToString());
                                        ////SqlParameter parametroDuracion = new SqlParameter("@duracion", SqlDbType.Int);
                                        ////parametroDuracion.Value = Convert.ToInt32(axdEntity_SalesQuotationLine.DuracionEstimada);
                                        ////conexiones.comando.Parameters.Add(parametroDuracion);
                                        //dr = conexiones.comando.ExecuteReader();
                                        //if (dr.HasRows)
                                        //{
                                        //    while (dr.Read())
                                        //    {
                                        //        if (Convert.ToString(dr["Concepto"]) == "Coste superficie")
                                        //            costeSuperficie = Convert.ToDecimal(dr["Valor"]);
                                        //        if (Convert.ToString(dr["Concepto"]) == "Taxímetro consumible")
                                        //        {
                                        //            taximetroConsumible = Convert.ToDecimal(dr["Valor"]) / 100;
                                        //            bonificacionTaximetroConsumible = taximetroConsumible - (Convert.ToDecimal(dr["Ajuste"]) / 100);
                                        //            calcularPorSuperficie = false;
                                        //            superficie = 0;
                                        //            if (!String.IsNullOrEmpty(Convert.ToString(dr["Valorar"])))
                                        //                if (Convert.ToString(dr["Valorar"]).ToUpper() == "SUPERFICIE")
                                        //                {
                                        //                    calcularPorSuperficie = true;
                                        //                    superficie = Convert.ToDecimal(dr["Superficie"]);
                                        //                }
                                        //        }
                                        //        if (Convert.ToString(dr["Concepto"]) == "Taxímetro no consumible" && (calcularPorSuperficie || taximetroConsumible == 0))
                                        //            taximetroNoConsumible = Convert.ToDecimal(dr["Valor"]) / 100;
                                        //        metersInvoicing = Convert.ToBoolean(dr["MetersInvoicing"]);
                                        //        productType = Convert.ToString(dr["ProductTypeName_es"]);

                                        //        if (Convert.ToString(dr["Concepto"]) == "Coef. Corrección PS Nuevo")
                                        //            coeficienteNuevo= Convert.ToDecimal(dr["Valor"]);
                                        //        if (Convert.ToString(dr["Concepto"]) == "Coef. Corrección PS Usado")
                                        //            coeficienteUsado= Convert.ToDecimal(dr["Valor"]);
                                        //        tipoArticulo = Convert.ToString(dr["TipoArticulo"]);
                                        //    }
                                        //}
                                        //dr.Close();
                                        //conexiones.comando.Dispose();
                                        //conexiones.conexion.Close();
                                        //conexiones.conexion.Dispose();
                                        //horaDatosConfigurados_4 = DateTime.Now.Subtract(horaDatosConfigurados_3);

                                        precioCoste = 0;
                                        if (hayPrecio)
                                        {
                                            dondeVa = "buscar precio dataset";
                                            filaEncontrada = dtArticulos.Select("Articulo = '" + axdEntity_SalesQuotationLine.ItemId.ToString() + "'");
                                            foreach (DataRow fila in filaEncontrada)
                                            {
                                                dondeVa = "encontró precio coste dataset // " + articulosCambio + " // ";
                                                if (!String.IsNullOrEmpty(Convert.ToString(fila["Precio"])))
                                                    precioCoste = Convert.ToDecimal(fila["Precio"]);
                                                dondeVa = "fecha dataset // " + articulosCambio + " // ";
                                                if (!String.IsNullOrEmpty(Convert.ToString(fila["Fecha"])))
                                                    fechaPrecio = Convert.ToDateTime(fila["Fecha"]);

                                                dondeVa = "Precio original // " + articulosCambio + " // ";
                                                if (!String.IsNullOrEmpty(Convert.ToString(fila["PrecioOriginal"])))
                                                    oItemTaximetro[COL_DATA_PRECIO_ORIGINAL] = Convert.ToDecimal(fila["PrecioOriginal"]).ToString("#,##0.00");
                                                dondeVa = "Fecha cambio // " + articulosCambio + " // ";
                                                if (!String.IsNullOrEmpty(Convert.ToString(fila["FechaCambio"])))
                                                    oItemTaximetro[COL_DATA_FECHA_CAMBIO] = Convert.ToDateTime(fila["FechaCambio"]).ToString("dd/MM/yyyy");
                                                dondeVa = "Cambio // " + articulosCambio + " // ";
                                                if (!String.IsNullOrEmpty(Convert.ToString(fila["Cambio"])))
                                                    oItemTaximetro[COL_DATA_CAMBIO] = Convert.ToDecimal(fila["Cambio"]).ToString("#,##0.000000");
                                            }
                                        }

                                        // recuperar datos de configuración del datatable
                                        dondeVa = "";
                                        listaBonificacionesDesde.Clear();
                                        listaBonificacionesHasta.Clear();
                                        listaBonificacionesValor.Clear();
                                        diasDesdeTaxNoConsumible = 0;
                                        diasHastaTaxNoConsumible = 0;
                                        filaEncontrada = dtDatosConfiguracion.Select("Itemid = '" + axdEntity_SalesQuotationLine.ItemId.ToString() + "'");
                                        foreach (DataRow fila in filaEncontrada)
                                        {
                                            dondeVa = "encontró configuracion dataset // " + axdEntity_SalesQuotationLine.ItemId.ToString() + " // ";
                                            dondeVa = "encontró configuracion dataset // Coste superficie";
                                            if (Convert.ToString(fila["Concepto"]) == "Coste superficie")
                                                costeSuperficie = Convert.ToDecimal(fila["Valor"]);
                                            dondeVa = "encontró configuracion dataset // Taxímetro consumible";
                                            if (Convert.ToString(fila["Concepto"]) == "Taxímetro consumible")
                                            {
                                                taximetroConsumible = Convert.ToDecimal(fila["Valor"]) / 100;
                                                dondeVa = "encontró configuracion dataset // Desde Bonificacion";
                                                listaBonificacionesDesde.Add(Convert.ToInt32(fila["Desde"]));
                                                dondeVa = "encontró configuracion dataset // Hasta Bonificacion";
                                                listaBonificacionesHasta.Add(Convert.ToInt32(fila["Hasta"]));
                                                dondeVa = "encontró configuracion dataset // Ajuste Bonificacion";
                                                listaBonificacionesValor.Add(Convert.ToDecimal(fila["Ajuste"]));

                                                calcularPorSuperficie = false;
                                                dondeVa = "encontró configuracion dataset // Valorar";
                                                superficie = 0;
                                                if (!String.IsNullOrEmpty(Convert.ToString(fila["Valorar"])))
                                                    if (Convert.ToString(fila["Valorar"]).ToUpper() == "SUPERFICIE")
                                                    {
                                                        calcularPorSuperficie = true;
                                                        dondeVa = "encontró configuracion dataset // Superficie";
                                                        superficie = Convert.ToDecimal(fila["Superficie"]);
                                                    }
                                            }
                                            dondeVa = "encontró configuracion dataset // Tax no consumible";
                                            diasDesdeTaxNoConsumible = 0;
                                            diasHastaTaxNoConsumible = 0;
                                            if (Convert.ToString(fila["Concepto"]) == "Taxímetro no consumible" && (calcularPorSuperficie || taximetroConsumible == 0))
                                            {
                                                dondeVa = "encontró configuracion dataset // Valor tax no consumible";
                                                taximetroNoConsumible = Convert.ToDecimal(fila["Valor"]) / 100;
                                                diasDesdeTaxNoConsumible = Convert.ToInt32(fila["Desde"]);
                                                diasHastaTaxNoConsumible = Convert.ToInt32(fila["Hasta"]);
                                            }

                                            dondeVa = "encontró configuracion dataset // Meters";
                                            metersInvoicing = Convert.ToBoolean(fila["MetersInvoicing"]);
                                            dondeVa = "encontró configuracion dataset // Tipo producto";
                                            productType = Convert.ToString(fila["ProductTypeName_es"]);

                                            dondeVa = "encontró configuracion dataset // Correcion PS Nuevo";
                                            if (Convert.ToString(fila["Concepto"]) == "Coef. Corrección PS Nuevo")
                                                coeficienteNuevo = Convert.ToDecimal(fila["Valor"]);
                                            dondeVa = "encontró configuracion dataset // Correcion PS Usado";
                                            if (Convert.ToString(fila["Concepto"]) == "Coef. Corrección PS Usado")
                                                coeficienteUsado = Convert.ToDecimal(fila["Valor"]);
                                            dondeVa = "encontró configuracion dataset // Tipo artículo";
                                            tipoArticulo = Convert.ToString(fila["TipoArticulo"]);
                                        }

                                        dondeVa = "salio configuracion dataset";

                                        if (tipoArticulo == "NUEVO")
                                        {
                                            coeficienteUsar = coeficienteNuevo;
                                            oItemTaximetro[COL_DATA_PRECIO_CORRECION_NUEVO] = (precioCoste * coeficienteNuevo).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            coeficienteUsar = coeficienteUsado;
                                            oItemTaximetro[COL_DATA_PRECIO_CORRECION_USADO] = (precioCoste * coeficienteUsado).ToString("#,##0.00");
                                        }

                                        oItemTaximetro[COL_DATA_CAPITULO] = axdEntity_SalesQuotationTable.QuotationId.ToString();
                                        oItemTaximetro[COL_DATA_ITEM] = axdEntity_SalesQuotationLine.ItemId.ToString();
                                        oItemTaximetro[COL_DATA_TIPO_ITEM] = tipoArticulo;
                                       
                                        oItemTaximetro[COL_DATA_PRECIO] = precioCoste.ToString("#,##0.00");
                                        oItemTaximetro[COL_DATA_FECHA] = fechaPrecio.ToString("dd/MM/yyyy");
                                        oItemTaximetro[COL_DATA_TIPO_CAPITULO] = axdEntity_SalesQuotationTable.SalesRental.ToString().ToUpper();
                                        
                                        if (axdEntity_SalesQuotationTable.SalesRental.ToString().ToUpper() == "SALES")
                                        {
                                            if (tipoArticulo == "NUEVO")
                                                oItemTaximetro[COL_DATA_COEFICIENTE_NUEVO] = coeficienteNuevo.ToString("##0.00");
                                            else
                                                oItemTaximetro[COL_DATA_COEFICIENTE_USADO] = coeficienteUsado.ToString("##0.00");

                                            importeVenta = importeVenta + Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount);
                                            importeFacturacionVentaCapitulos = importeFacturacionVentaCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount);

                                            if (productType.Trim().ToUpper() == "SERVICIO")
                                            {
                                                importeVentaServicio = importeVentaServicio + Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount);
                                                importeFacturacionVentaServicioCapitulos = importeFacturacionVentaServicioCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount);

                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA_SERVICIO] = Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount).ToString("#,##0.00");
                                            }
                                            else
                                            {
                                                importeVentaProducto = importeVentaProducto + Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount);
                                                importeFacturacionVentaProductoCapitulos = importeFacturacionVentaProductoCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount);
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA_PRODUCTO] = Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount).ToString("#,##0.00");
                                            }

                                            importeCosteVenta = importeCosteVenta + Convert.ToDecimal(axdEntity_SalesQuotationLine.QtyOrdered) * precioCoste * coeficienteUsar;
                                            importeCosteVentaCapitulos = importeCosteVentaCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationLine.QtyOrdered) * precioCoste * coeficienteUsar;

                                            // Gastos variables
                                            importeVentaGastosVariables = importeVentaGastosVariables + (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosVariablesVenta);
                                            importeGastosVariablesVentaCapitulos = importeGastosVariablesVentaCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosVariablesVenta);

                                            // Gastos fijos BU
                                            importeVentaGastosFijosBU = importeVentaGastosFijosBU + (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosFijosBUVenta);
                                            importeGastosFijosBUVentaCapitulos = importeGastosFijosBUVentaCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosFijosBUVenta);

                                            // Gastos fijos centrales
                                            importeVentaGastosFijosCentrales = importeVentaGastosFijosCentrales + (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosFijosCentralesVenta);
                                            importeGastosFijosCentralesVentaCapitulos = importeGastosFijosCentralesVentaCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosFijosCentralesVenta);

                                            oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_VENTA] = Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount).ToString("#,##0.00");


                                            //oItemTaximetro[COL_DATA_IMPORTE_COSTE] = (Convert.ToDecimal(axdEntity_SalesQuotationLine.QtyOrdered) * precioCoste).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_GASTO_VARIABLE] = gastosVariablesVenta.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_GASTO_VARIABLE] = (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosVariablesVenta).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_GASTO_FIJO_BU] = gastosFijosBUVenta.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_BU] = (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosFijosBUVenta).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_GASTO_FIJO_CENTRAL] = gastosFijosCentralesVenta.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_CENTRAL] = (Convert.ToDecimal(axdEntity_SalesQuotationLine.LineAmount) * gastosFijosCentralesVenta).ToString("#,##0.00");
                                        }
                                        if (axdEntity_SalesQuotationTable.SalesRental.ToString().ToUpper() == "RENTAL")
                                        {
                                            // (unidades* PS* taximetro (consumible o no consumible) % *días) / 30
                                            importeAlquiler = importeAlquiler + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada));
                                            importeFacturacionAlquilerCapitulos = importeFacturacionAlquilerCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada);

                                            // Gastos variables
                                            importeAlquilerGastosVariables = importeAlquilerGastosVariables + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosVariablesAlquiler);
                                            importeGastosVariablesAlquilerCapitulos = importeGastosVariablesAlquilerCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosVariablesAlquiler);

                                            // Gastos fijos BU
                                            importeAlquilerGastosFijosBU = importeAlquilerGastosFijosBU + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosFijosBUAlquiler);
                                            importeGastosFijosBUAlquilerCapitulos = importeGastosFijosBUAlquilerCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosFijosBUAlquiler);

                                            // Gastos fijos centrales
                                            importeAlquilerGastosFijosCentrales = importeAlquilerGastosFijosCentrales + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosFijosCentralesAlquiler);
                                            importeGastosFijosCentralesAlquilerCapitulos = importeGastosFijosCentralesAlquilerCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosFijosCentralesAlquiler);

                                            oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_EURO_DIA] = Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia).ToString("#,##0.0000");
                                            oItemTaximetro[COL_DATA_DURACION_ESTIMADA] = Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_ALQUILER] = (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_GASTO_VARIABLE] = gastosVariablesAlquiler.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_GASTO_VARIABLE] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosVariablesAlquiler).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_GASTO_FIJO_BU] = gastosFijosBUAlquiler.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_BU] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosFijosBUAlquiler).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_GASTO_FIJO_CENTRAL] = gastosFijosCentralesAlquiler.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_CENTRAL] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) * gastosFijosCentralesAlquiler).ToString("#,##0.00");

                                            if (taximetroConsumible == 0)
                                            {
                                                precioTaximetroNoConsumible = precioCoste;
                                            }
                                            else
                                            {
                                                if (calcularPorSuperficie)
                                                {
                                                    if (!metersInvoicing)
                                                    {
                                                        precioTaximetroNoConsumible = (precioCoste - (costeSuperficie * superficie));
                                                        precioTaximetroConsumible = 0;
                                                    }
                                                    else
                                                    {
                                                        precioTaximetroNoConsumible = (precioCoste - (costeSuperficie * superficie));
                                                        precioTaximetroConsumible = (costeSuperficie * superficie);
                                                    }
                                                }
                                                else
                                                {
                                                    precioTaximetroNoConsumible = 0;
                                                    precioTaximetroConsumible = precioCoste;
                                                }
                                            }

                                            if (calcularPorSuperficie)
                                            {
                                                if (!metersInvoicing)
                                                    oItemTaximetro[COL_DATA_TIPO] = "SUPERFICIE / NO meters invoicing";
                                                else
                                                    oItemTaximetro[COL_DATA_TIPO] = "SUPERFICIE";
                                            }
                                            else
                                                oItemTaximetro[COL_DATA_TIPO] = "";

                                            oItemTaximetro[COL_DATA_TAX_CONSUMIBLE] = taximetroConsumible.ToString("#,##0.0000");
                                            oItemTaximetro[COL_DATA_TAX_NO_CONSUMIBLE] = taximetroNoConsumible.ToString("#,##0.0000");
                                            oItemTaximetro[COL_DATA_SUPERFICIE] = superficie.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_COSTE_SUPERFICIE] = (costeSuperficie * superficie).ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_PRECIO_CONSUMIBLE] = precioTaximetroConsumible.ToString("#,##0.00");
                                            oItemTaximetro[COL_DATA_PRECIO_NO_CONSUMIBLE] = precioTaximetroNoConsumible.ToString("#,##0.00");

                                            importeCosteTaximetroConsumible = importeCosteTaximetroConsumible + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) / diasxMes;
                                            importeCosteTaximetroConsumibleCapitulos = importeCosteTaximetroConsumibleCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) / diasxMes;

                                            dondeVa = "Empieza recorrido bonificación";
                                            oItemTaximetro[COL_DATA_AJUSTE] = "";
                                            //oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (0.00).ToString("#,##0.00");

                                            duracion = Convert.ToInt32(axdEntity_SalesQuotationLine.DuracionEstimada);
                                            diasCalculados = 0;
                                            diasMultiplicar = 0;
                                            while (diasCalculados < duracion)
                                            {
                                                for (lineasBonificacion = 0; lineasBonificacion < listaBonificacionesDesde.Count; lineasBonificacion++)
                                                {
                                                    if ((diasCalculados + diasxMes) >= listaBonificacionesDesde.ElementAt(lineasBonificacion) && (diasCalculados + diasxMes) <= listaBonificacionesHasta.ElementAt(lineasBonificacion))
                                                    {
                                                        if (duracion - diasCalculados >= diasxMes)
                                                            diasMultiplicar = diasxMes;
                                                        else
                                                            diasMultiplicar = duracion - diasCalculados;
                                                        bonificacionTaximetroConsumible = taximetroConsumible - (listaBonificacionesValor.ElementAt(lineasBonificacion) / 100);
                                                        oItemTaximetro[COL_DATA_AJUSTE] = oItemTaximetro[COL_DATA_AJUSTE] + "Bonif. (" + bonificacionTaximetroConsumible.ToString("#,##0.0000") + ") * Dias(" + diasMultiplicar.ToString() + ") // ";
                                                        if (oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE].ToString() == "")
                                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes).ToString("#,##0.00");
                                                        else
                                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (Convert.ToDecimal(oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE]) + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes)).ToString("#,##0.00");
                                                        importeCosteBonificacionTaximetroConsumible = importeCosteBonificacionTaximetroConsumible + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                                        importeCosteBonificacionTaximetroConsumibleCapitulos = importeCosteBonificacionTaximetroConsumibleCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                                    }
                                                }
                                                diasCalculados = diasCalculados + diasxMes;
                                            }
                                            dondeVa = "Fin recorrido bonificación";

                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_DESDE] = diasDesdeTaxNoConsumible.ToString();
                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_HASTA] = diasHastaTaxNoConsumible.ToString();
                                            oItemTaximetro[COL_DATA_IMPORTE_TAX] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) / diasxMes).ToString("#,##0.00");

                                            if (Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada) <= diasHastaTaxNoConsumible)
                                                diasTaximetroNoConsumibleCalcular = Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada) - diasDesdeTaxNoConsumible;
                                            if (Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada) >= diasHastaTaxNoConsumible)
                                                diasTaximetroNoConsumibleCalcular = diasHastaTaxNoConsumible - diasDesdeTaxNoConsumible;

                                            importeCosteTaximetroNoConsumible = importeCosteTaximetroNoConsumible + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                            importeCosteTaximetroNoConsumibleCapitulos = importeCosteTaximetroNoConsumibleCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes).ToString("#,##0.00");

                                            //importeCosteTaximetroNoConsumible = importeCosteTaximetroNoConsumible + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) / diasxMes;
                                            //importeCosteTaximetroNoConsumibleCapitulos = importeCosteTaximetroNoConsumibleCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) / diasxMes;
                                            //oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada)) / diasxMes).ToString("#,##0.00");
                                        }
                                      
                                        dtTaximetro.Rows.Add(oItemTaximetro);
                                        oItemTaximetro = null;

                                        importePorte = 0;
                                        importeFacturacionPorteCapitulos = 0;
                                        importeCostePorte = 0;
                                        importeCostePorteCapitulos = 0;
                                    }

                                    importeMargenAlquiler = importeAlquiler - (importeCosteTaximetroNoConsumible + importeCosteTaximetroConsumible - importeCosteBonificacionTaximetroConsumible);
                                    importeMargenVenta = importeVenta - importeCosteVenta;
                                    importeMargenPorte = 0;

                                    if (axdEntity_SalesQuotationTable.SalesRental.ToString().ToUpper() == "SALES")
                                    {
                                        cantidadVenta = cantidadVenta + 1;
                                        capitulosVenta.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());

                                        if (productType.Trim().ToUpper() == "SERVICIO")
                                        {
                                            cantidadVentaServicio = cantidadVentaServicio + 1;
                                            capitulosVentaServicio.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());
                                            dec_importeFacturacionVentaServicioCapitulos.Add(importeFacturacionVentaServicioCapitulos);
                                        }
                                        else
                                        {
                                            cantidadVentaProducto = cantidadVentaProducto + 1;
                                            capitulosVentaProducto.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());
                                            dec_importeFacturacionVentaProductoCapitulos.Add(importeFacturacionVentaProductoCapitulos);
                                        }

                                        dec_importeCosteVentaCapitulos.Add(importeCosteVentaCapitulos);
                                        dec_importeMargenVentaCapitulos.Add(importeFacturacionVentaCapitulos - importeCosteVentaCapitulos);

                                        dec_importeGastosVariablesVentaCapitulos.Add(importeGastosVariablesVentaCapitulos);
                                        dec_importeGastosFijosBUVentaCapitulos.Add(importeGastosFijosBUVentaCapitulos);
                                        dec_importeGastosFijosCentralesVentaCapitulos.Add(importeGastosFijosCentralesVentaCapitulos);
                                    }
                                    if (axdEntity_SalesQuotationTable.SalesRental.ToString().ToUpper() == "RENTAL")
                                    {
                                        cantidadAlquiler = cantidadAlquiler + 1;
                                        cantidadTaximetroConsumible = cantidadTaximetroConsumible + 1;
                                        cantidadTaximetroNoConsumible = cantidadTaximetroNoConsumible + 1;

                                        capitulosAlquiler.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());
                                        dec_importeFacturacionAlquilerCapitulos.Add(importeFacturacionAlquilerCapitulos);
                                        dec_importeMargenAlquilerCapitulos.Add(importeFacturacionAlquilerCapitulos - (importeCosteTaximetroNoConsumibleCapitulos + importeCosteTaximetroConsumibleCapitulos- importeCosteBonificacionTaximetroConsumibleCapitulos));

                                        capitulosTaximetroNoConsumible.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());
                                        dec_importeCosteTaximetroNoConsumibleCapitulos.Add(importeCosteTaximetroNoConsumibleCapitulos);

                                        capitulosTaximetroConsumible.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());
                                        dec_importeCosteTaximetroConsumibleCapitulos.Add(importeCosteTaximetroConsumibleCapitulos);
                                        dec_importeCosteBonificacionTaximetroConsumibleCapitulos.Add(importeCosteBonificacionTaximetroConsumibleCapitulos);

                                        dec_importeGastosVariablesAlquilerCapitulos.Add(importeGastosVariablesAlquilerCapitulos);
                                        dec_importeGastosFijosBUAlquilerCapitulos.Add(importeGastosFijosBUAlquilerCapitulos);
                                        dec_importeGastosFijosCentralesAlquilerCapitulos.Add(importeGastosFijosCentralesAlquilerCapitulos);
                                    }

                                    // Porte
                                    cantidadPorte = cantidadPorte + 1;
                                    capitulosPorte.Add(axdEntity_SalesQuotationTable.QuotationId.ToString());
                                    dec_importeFacturacionPorteCapitulos.Add(importeFacturacionPorteCapitulos);
                                    dec_importeCostePorteCapitulos.Add(importeCostePorteCapitulos);
                                    dec_importeMargenPorteCapitulos.Add(importeFacturacionPorteCapitulos - importeCostePorteCapitulos);
                                    
                                    dtDatosConfiguracion.Dispose();
                                }
                            }
                            horaDatosPase2_2 = DateTime.Now.Subtract(horaDatosPase2_1);
                        }
                    }
                    dtArticulos.Dispose();
                    dondeVa = "finalizó";
                    proxy_1.Close();
                    horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);

                    listaArticulosSinFicha.Clear();
                    listaArticulosPedido.Clear();
                    listaFechasPedido.Clear();
                    listaOfertas.Clear();
                    listaFechasOferta.Clear();
                }

                dondeVa = "nombre del doc";
                nombreInforme = Server.MapPath("~/Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                dondeVa = "nuevo doc";
                SLDocument sl = new SLDocument();
                dondeVa = "importar tabla";
                sl.ImportDataTable(1, 1, dtTaximetro, true);
                dondeVa = "salvar tabla";
                sl.SaveAs(nombreInforme);
                btnAbrirExcel.Visible = true;

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_DatosArticulosFamiliasSubfamiliasEliminar";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@usuario", usuario);
                conexiones.comando.ExecuteNonQuery();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                dondeVa = "pintar datos";
                pintarDatos();
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Calcular oferta // " + dondeVa + " // " + ex.Message;
            }
        }

        protected void calcularPedido()
        {
            string dondeVa = "";
            bool esLiquidacion = false;
            System.Data.DataTable dtPedidos = new System.Data.DataTable();
            System.Data.DataTable dtAlbaranes = new System.Data.DataTable();
            System.Data.DataTable dtArticulosLiquidacion = new System.Data.DataTable();

            DataRow filaPedidos;
            DataRow filaAlbaranes;
            string articulosLiquidacion = "";
            string AAF = "";
            decimal faltan = 0;
            string obraID = "";

            const int dtPedidos_fechaCreacion = 0;
            const int dtPedidos_aaf = 1;
            const int dtPedidos_itemID = 2;
            const int dtPedidos_cantidad = 3;

            const int dtPedidosAgrupados_fechaCreacion = 0;
            const int dtPedidosAgrupados_aaf = 1;
            const int dtPedidosAgrupados_articulo = 2;
            const int dtPedidosAgrupados_cantidad = 3;
            const int dtPedidosAgrupados_usado = 4;
            const int dtPedidosAgrupados_mixto = 5;
            const int dtPedidosAgrupados_nuevo = 6;

            const int dtAlbaranes_fecha = 0;
            const int dtAlbaranes_aaf = 1;
            const int dtAlbaranes_itemID = 2;
            const int dtAlbaranes_cantidad = 3;
            const int dtAlbaranes_estado = 4;

            const int dtAlbaranesAgrupados_fecha = 0;
            const int dtAlbaranesAgrupados_aaf = 1;
            const int dtAlbaranesAgrupados_cantidad = 2;
            const int dtAlbaranesAgrupados_cantidadQueda = 3;
            const int dtAlbaranesAgrupados_estado = 4;

            try
            {
                SqlDataAdapter adaptadorDatosConfiguracion;
                System.Data.DataTable dtDatosConfiguracion=new System.Data.DataTable();
                int lineasBonificacion;
                int duracion = 0;
                int diasCalculados = 0;
                int diasMultiplicar = 0;
                int diasDesdeTaxNoConsumible = 0;
                int diasHastaTaxNoConsumible = 0;
                int diasTaximetroNoConsumibleCalcular = 0;

                dataDatos.DataSource = null;
                dataDatos.Columns.Clear();
                dataTiempos.DataSource = null;
                dataTiempos.Columns.Clear();

                dtTaximetro = new DataTable();

                dtTaximetro.Columns.Add("CAPITULO");
                dtTaximetro.Columns.Add("TIPO CAPITULO");
                dtTaximetro.Columns.Add("ITEM");
                dtTaximetro.Columns.Add("NUEVO/USADO");
                dtTaximetro.Columns.Add("COEF. NUEVO");
                dtTaximetro.Columns.Add("COEF. USADO");
                dtTaximetro.Columns.Add("CANTIDAD");
                dtTaximetro.Columns.Add("EURO/UD/DIA");
                dtTaximetro.Columns.Add("DURACION ESTIMADA");
                dtTaximetro.Columns.Add("FACT ALQUILER");
                dtTaximetro.Columns.Add("FACT TOTAL VENTA");
                dtTaximetro.Columns.Add("FACT VENTA SERVICIO");
                dtTaximetro.Columns.Add("FACT VENTA PRODUCTO");
                dtTaximetro.Columns.Add("CANTIDAD DIAS");
                dtTaximetro.Columns.Add("FECHA PS");
                dtTaximetro.Columns.Add("PS/UD");
                dtTaximetro.Columns.Add("PS/UD ORIGINAL");
                dtTaximetro.Columns.Add("PS Corrección N");
                dtTaximetro.Columns.Add("PS Corrección U");
                dtTaximetro.Columns.Add("FECHA CAMBIO");
                dtTaximetro.Columns.Add("CAMBIO");
                dtTaximetro.Columns.Add("TIPO ARTICULO");
                dtTaximetro.Columns.Add("SUPERFICIE");
                dtTaximetro.Columns.Add("COSTE SUPERFICIE");
                dtTaximetro.Columns.Add("PRECIO CONSUMIBLE");
                dtTaximetro.Columns.Add("PRECIO NO CONSUMIBLE");
                dtTaximetro.Columns.Add("% TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("IMPORTE TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("% AJUSTE");
                dtTaximetro.Columns.Add("AJUSTE TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("CANTIDAD DIAS TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("DIAS DESDE TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("DIAS HASTA TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("% TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("GASTO VARIABLE");
                dtTaximetro.Columns.Add("IMPORTE GASTO VARIABLE");
                dtTaximetro.Columns.Add("GASTO FIJO BU");
                dtTaximetro.Columns.Add("IMPORTE GASTO FIJO BU");
                dtTaximetro.Columns.Add("GASTO FIJO CENTRAL");
                dtTaximetro.Columns.Add("IMPORTE GASTO FIJO CENTRAL");

                dtPedidos.Columns.Add("FECHA_CREACION");
                dtPedidos.Columns.Add("AAF");
                dtPedidos.Columns.Add("ITEM_ID");
                dtPedidos.Columns.Add("SALESQTY");

                dtAlbaranes.Columns.Add("FECHA");
                dtAlbaranes.Columns.Add("AAF");
                dtAlbaranes.Columns.Add("ItemId");
                dtAlbaranes.Columns.Add("Qty");
                dtAlbaranes.Columns.Add("Estado");

                decimal coeficienteNuevo = 0;
                decimal coeficienteUsado = 0;
                decimal coeficienteUsar = 0;
                decimal coeficienteMixto = 0;
                string tipoArticulo = "";

                DateTime fechaPrecio = DateTime.Now;
                decimal gastosVariablesAlquiler = 0;
                decimal gastosVariablesVenta = 0;
                decimal gastosFijosBUAlquiler = 0;
                decimal gastosFijosBUVenta = 0;
                decimal gastosFijosCentralesAlquiler = 0;
                decimal gastosFijosCentralesVenta = 0;
                decimal bonificacionTaximetroConsumible = 0;

                bool metersInvoicing = false;
                string productType = "";
                bool calcularPorSuperficie = false;
                decimal superficie = 0;
                SqlDataReader dr;
                decimal costeSuperficie = 0;
                string articulosConfiguracion = "";
                decimal taximetroNoConsumible = 0;
                decimal taximetroConsumible = 0;
                decimal precioTaximetroNoConsumible = 0;
                decimal precioTaximetroConsumible = 0;
                string IDCapitulo = "";
                string tabla = "";
                string campo = "";
                int dias = 0;
                int diasxMes = 0;
                int diasDiferencia = 0;
                int diasRestar = 0;
                DateTime fechaPrecios;
                DateTime fechaOfertaPedido;
                bool hayPrecio = false;
                System.Data.DataTable dtArticulos = new System.Data.DataTable();
                bool primeraVez = true;
                int lineaCapitulo;
                DataRow[] filaEncontrada;
                string articulos = "";
                string articulosCambio = "";
                decimal precioCoste = 0;
                decimal importeFacturacionAlquilerCapitulos = 0;
                decimal importeCosteTaximetroNoConsumibleCapitulos = 0;
                decimal importeCosteTaximetroConsumibleCapitulos = 0;
                decimal importeFacturacionVentaCapitulos = 0;
                decimal importeFacturacionVentaServicioCapitulos = 0;
                decimal importeFacturacionVentaProductoCapitulos = 0;
                decimal importeCosteVentaCapitulos = 0;
                decimal importeFacturacionPorteCapitulos = 0;
                decimal importeCostePorteCapitulos = 0;
                decimal importeGastosVariablesVentaCapitulos = 0;
                decimal importeGastosVariablesAlquilerCapitulos = 0;
                decimal importeGastosFijosBUVentaCapitulos = 0;
                decimal importeGastosFijosBUAlquilerCapitulos = 0;
                decimal importeGastosFijosCentralesVentaCapitulos = 0;
                decimal importeGastosFijosCentralesAlquilerCapitulos = 0;
                decimal importeCosteBonificacionTaximetroConsumibleCapitulos = 0;

                // Parámetros
                dondeVa = "sp_ROP_ConfiguracionFijaConsulta";
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionFijaConsulta";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                dr = conexiones.comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    diasxMes = Convert.ToInt32(dr["COF_diasCalculo"]);
                    diasDiferencia = Convert.ToInt32(dr["COF_OfertaDiasEntreFechaOfertaFechaPedido"]);
                    diasRestar = Convert.ToInt32(dr["COF_OfertaDiasRestarFechaPedido"]);
                }
                dr.Close();
                dr.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                txtNombreOferta.Text = "";

                System.Collections.IEnumerator enumerator_1;

                horaTotal_1 = DateTime.Now;
                horaLlamada_1 = DateTime.Now;

                // CABECERA
                dondeVa = "inicializando";
                cantidadAlquiler = 0;
                cantidadTaximetroNoConsumible = 0;
                cantidadTaximetroConsumible = 0;
                cantidadVenta = 0;
                cantidadPorte = 0;
                importeAlquiler = 0;
                importeVenta = 0;
                importeVentaServicio = 0;
                importeVentaProducto = 0;
                importePorte = 0;
                importeCosteTaximetroNoConsumible = 0;
                importeCosteTaximetroConsumible = 0;
                importeCosteVenta = 0;
                importeCostePorte = 0;
                importeMargenAlquiler = 0;
                importeMargenVenta = 0;
                importeMargenPorte = 0;

                tablaPedidos.CallContext contexto_1 = new tablaPedidos.CallContext();
                contexto_1.Company = cmbEmpresa.SelectedItem.ToString();

                tablaPedidos.QueryCriteria criterio_1 = new tablaPedidos.QueryCriteria();
                criterio_1.CriteriaElement = new tablaPedidos.CriteriaElement[1];

                criterio_1.CriteriaElement[0] = new tablaPedidos.CriteriaElement();
                criterio_1.CriteriaElement[0].FieldName = "SalesID";
                criterio_1.CriteriaElement[0].DataSourceName = "SalesTable";
                criterio_1.CriteriaElement[0].Operator = tablaPedidos.Operator.Equal;
                criterio_1.CriteriaElement[0].Value1 = txtNumero.Text;

                tablaPedidos.SalesOrdersServiceClient proxy_1 = new tablaPedidos.SalesOrdersServiceClient();
                proxy_1.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_1.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_1.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                dondeVa = "pedidos";
                tablaPedidos.AxdSalesOrders axdtablaPedidos_1 = proxy_1.find(contexto_1, criterio_1);
                if (axdtablaPedidos_1.SalesTable is null)
                {
                    proxy_1.Close();
                }
                else
                {
                    enumerator_1 = axdtablaPedidos_1.SalesTable.GetEnumerator();
                    dtArticulos = new System.Data.DataTable();
                    horaLlamada_2 = DateTime.Now.Subtract(horaLlamada_1);

                    while (enumerator_1.MoveNext())
                    {
                        dondeVa = "datos generales";
                        fechaPrecios = new DateTime();
                        fechaOfertaPedido = new DateTime();
                        articulos = ";";
                        articulosCambio = ";";
                        articulosConfiguracion = "";

                        tablaPedidos.AxdEntity_SalesTable axdEntity_SalesQuotationMasterTable = (tablaPedidos.AxdEntity_SalesTable)enumerator_1.Current;

                        datosGenerales = axdEntity_SalesQuotationMasterTable.SalesId + " / " + axdEntity_SalesQuotationMasterTable.SalesName + " / " + axdEntity_SalesQuotationMasterTable.CurrencyCode + " / " + axdEntity_SalesQuotationMasterTable.Delegation + " / " + axdEntity_SalesQuotationMasterTable.SalesType;
                        moneda = axdEntity_SalesQuotationMasterTable.CurrencyCode;
                        if (axdEntity_SalesQuotationMasterTable.TipoVenta.ToString().ToUpper() == tablaPedidos.AxdEnum_TiposVentas.Liquidacion.ToString().ToUpper())
                            esLiquidacion = true;
                        else
                            esLiquidacion = false;

                        // DATOS CONFIGURACION
                        dondeVa = "ROP_DatosConfiguracionGeneral";
                        conexiones.crearConexion();
                        conexiones.comando = conexiones.conexion.CreateCommand();
                        conexiones.comando.CommandText = "ROP_DatosConfiguracionGeneral";
                        conexiones.comando.CommandTimeout = 240000;
                        conexiones.comando.CommandType = CommandType.StoredProcedure;
                        conexiones.comando.Parameters.AddWithValue("@empresa", cmbEmpresa.SelectedItem.ToString());
                        conexiones.comando.Parameters.AddWithValue("@delegacion", axdEntity_SalesQuotationMasterTable.Delegation);
                        dr = conexiones.comando.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToString(dr["Concepto"]).ToUpper() == "GASTOS VARIABLES")
                                {
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "ALQUILER")
                                        gastosVariablesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "VENTA")
                                        gastosVariablesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (String.IsNullOrEmpty(Convert.ToString(dr["Tipo"])))
                                    {
                                        gastosVariablesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                        gastosVariablesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    }
                                }
                                if (Convert.ToString(dr["Concepto"]).ToUpper() == "GASTOS FIJOS BU")
                                {
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "ALQUILER")
                                        gastosFijosBUAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "VENTA")
                                        gastosFijosBUVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (String.IsNullOrEmpty(Convert.ToString(dr["Tipo"])))
                                    {
                                        gastosFijosBUAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                        gastosFijosBUVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    }
                                }
                                if (Convert.ToString(dr["Concepto"]).ToUpper() == "GASTOS FIJOS CENTRALES")
                                {
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "ALQUILER")
                                        gastosFijosCentralesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (Convert.ToString(dr["Tipo"]).ToUpper() == "VENTA")
                                        gastosFijosCentralesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    if (String.IsNullOrEmpty(Convert.ToString(dr["Tipo"])))
                                    {
                                        gastosFijosCentralesAlquiler = Convert.ToDecimal(dr["Valor"]) / 100;
                                        gastosFijosCentralesVenta = Convert.ToDecimal(dr["Valor"]) / 100;
                                    }
                                }
                            }
                        }
                        dr.Close();
                        conexiones.comando.Dispose();
                        conexiones.conexion.Close();
                        conexiones.conexion.Dispose();

                        if (axdEntity_SalesQuotationMasterTable.ObraId != null)
                            fechaBaseFichaArticulo(axdEntity_SalesQuotationMasterTable.ObraId.ToString(), diasDiferencia, diasRestar);

                        tablaPedidos.AxdEntity_SalesLine[] axdEntity_SalesQuotationTables = axdEntity_SalesQuotationMasterTable.SalesLine;
                        tablaPedidos.AxdEntity_SalesLine axdEntity_SalesQuotationTable;

                        if (axdEntity_SalesQuotationTables is null)
                            datosGenerales = "NO HAY DATOS DE LOS CAPÍTULOS";
                        else
                        {
                            horaDatosPase1_1 = DateTime.Now;
                            dondeVa = "Capítulos";
                            for (int capitulo = 0; capitulo < axdEntity_SalesQuotationTables.Length; capitulo++)
                            {
                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[capitulo];

                                // artículos si hay ficha
                                if (!listaArticulosPedido.Contains(axdEntity_SalesQuotationTable.ItemId.ToString()))
                                {
                                    listaArticulosPedido.Add(axdEntity_SalesQuotationTable.ItemId.ToString());
                                    //if (axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value.ToString().Length > 0)
                                    if (axdEntity_SalesQuotationTable.CreatedDateTime.Value.ToString().Length > 0)
                                        listaFechasPedido.Add(Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value));
                                    else
                                        listaFechasPedido.Add(new DateTime());
                                }

                                // artículos si no hay ficha
                                if (!listaArticulosSinFicha.Contains(axdEntity_SalesQuotationTable.ItemId.ToString()))
                                {
                                    articulosConfiguracion = articulosConfiguracion + axdEntity_SalesQuotationTable.ItemId.ToString() + "|";
                                    listaArticulosSinFicha.Add(axdEntity_SalesQuotationTable.ItemId.ToString());
                                }
                            }
                            horaDatosPase1_2 = DateTime.Now.Subtract(horaDatosPase1_1);

                            // Calculo de la fecha a utilizar
                            //o   ¿La oferta está asociada a una ficha ?
                            //	SI:
                            //•	Fecha base = Fecha base ficha
                            //•	Consultar tabla BaseDateItemObra
                            //	NO:
                            //•	¿Existe cabecera de oferta? (RefQuotationMaster)
                            //o   NO: Fecha base = Fecha creación capitulo
                            //o SI: ¿fecha creación capítulo > p1 días fecha creación cabecera?
                            //	NO: fecha base = fecha creación capitulo
                            //	SI: fecha base = fecha creación capitulo – p2 días
                            if (!String.IsNullOrEmpty(axdEntity_SalesQuotationMasterTable.ObraId))
                            {
                                fechaPrecios = new DateTime();
                                //if (chkFecha.Checked)
                                //{
                                fechaBaseFichaArticulo(axdEntity_SalesQuotationMasterTable.ObraId.ToString(), diasDiferencia, diasRestar);

                                fechaPrecios = DateTime.Now;
                                tabla = "fechaBaseFichaArticulo";
                                IDCapitulo = "Ficha " + axdEntity_SalesQuotationMasterTable.ObraId;
                                campo = "fechaBaseFichaArticulo";

                                articulos = ";";
                                for (lineaCapitulo = 0; lineaCapitulo < listaArticulosPedido.Count; lineaCapitulo++)
                                    articulos = articulos + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";
                            }
                            //else
                            //{
                                if (!String.IsNullOrEmpty(axdEntity_SalesQuotationMasterTable.QuotationId))
                                {
                                    fechaOferta(ref fechaOfertaPedido, axdEntity_SalesQuotationMasterTable.QuotationId.ToString());
                                    axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[0];
                                    if (fechaOfertaPedido != new DateTime() && axdEntity_SalesQuotationTable.CreatedDateTime.ToString().Length > 0)
                                    {
                                        if (Math.Abs(Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value).Subtract(fechaOfertaPedido).Days) > diasDiferencia)
                                        {
                                            fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value).AddDays((-1) * diasRestar);
                                            tabla = "Pedido-SalesQuotationTable";
                                            campo = "CreatedDateTime - " + diasRestar.ToString();
                                        }
                                        else
                                        {
                                            fechaPrecios = fechaOfertaPedido;
                                            tabla = "Oferta-SalesQuotationTable";
                                            campo = "CreatedDateTime";
                                        }
                                    }
                                    else
                                    {
                                        if (axdEntity_SalesQuotationTable.CreatedDateTime.ToString().Length > 0)
                                        {
                                            fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value);
                                            tabla = "Pedido-SalesQuotationTable";
                                            campo = "CreatedDateTime";
                                        }
                                    }
                                }
                                else
                                {
                                    fechaPrecios = Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value);
                                    tabla = "Pedido-SalesQuotationMasterTable";
                                    campo = "CreatedDateTime";
                                }

                                for (lineaCapitulo = 0; lineaCapitulo < listaArticulosSinFicha.Count; lineaCapitulo++)
                                {
                                    if (String.IsNullOrEmpty(axdEntity_SalesQuotationMasterTable.ObraId))
                                        articulos = articulos + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";
                                    articulosCambio = articulosCambio + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";
                                }
                            //}
                            // Fin calculo de la fecha a utilizar                            

                            dondeVa = "sp_ROP_FechaBaseOfertaIncluir";
                            if (fechaPrecios != new DateTime())
                            {
                                conexiones.crearConexion();
                                conexiones.comando = conexiones.conexion.CreateCommand();
                                conexiones.comando.CommandText = "sp_ROP_FechaBaseOfertaIncluir";
                                conexiones.comando.CommandType = CommandType.StoredProcedure;
                                conexiones.comando.Parameters.AddWithValue("@FBO_Oferta", txtNumero.Text);
                                conexiones.comando.Parameters.AddWithValue("@FBO_Capitulo", IDCapitulo);
                                conexiones.comando.Parameters.AddWithValue("@FBO_Fecha", fechaPrecios);
                                conexiones.comando.Parameters.AddWithValue("@FBO_Articulo", articulos.Replace("'", ""));
                                conexiones.comando.Parameters.AddWithValue("@FBO_TablaFechaSeleccionada", tabla);
                                conexiones.comando.Parameters.AddWithValue("@FBO_CampoFechaSeleccionada1", campo);
                                conexiones.comando.Parameters.AddWithValue("@FBO_DiasDiferencia", dias);
                                conexiones.comando.Parameters.AddWithValue("@primeraVez", primeraVez);
                                conexiones.comando.ExecuteNonQuery();
                                conexiones.conexion.Close();
                                primeraVez = false;

                                dtArticulos = localizarPreciosArticulos(cmbEmpresa.SelectedItem.ToString(), axdEntity_SalesQuotationMasterTable.Delegation.ToString(), articulos, moneda, articulosCambio, "");
                                hayPrecio = true;
                            }

                            horaDatosConfigurados_1 = DateTime.Now;
                            datosSQL.datosConfigurados(cmbEmpresa.SelectedItem.ToString(), articulosConfiguracion, usuario);
                            horaDatosConfigurados_2 = DateTime.Now.Subtract(horaDatosConfigurados_1);

                            importeFacturacionAlquilerCapitulos = 0;
                            importeFacturacionVentaCapitulos = 0;
                            importeFacturacionVentaServicioCapitulos = 0;
                            importeFacturacionVentaProductoCapitulos = 0;
                            importeGastosVariablesAlquilerCapitulos = 0;
                            importeGastosVariablesVentaCapitulos = 0;

                            importeGastosFijosBUAlquilerCapitulos = 0;
                            importeGastosFijosBUVentaCapitulos = 0;

                            importeGastosFijosCentralesAlquilerCapitulos = 0;
                            importeGastosFijosCentralesVentaCapitulos = 0;

                            importeCosteVentaCapitulos = 0;
                            importeCosteTaximetroNoConsumibleCapitulos = 0;
                            importeCosteTaximetroConsumibleCapitulos = 0;

                            horaDatosPase2_1 = DateTime.Now;
                            dondeVa = "capítulos cálculos";
                            for (int capitulo = 0; capitulo < axdEntity_SalesQuotationTables.Length; capitulo++)
                            {
                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[capitulo];

                                tablaPedidos.AxdEnum_SalesType AxdEnum_SalesType;
                                AxdEnum_SalesType = axdEntity_SalesQuotationTable.SalesType.Value;
                               
                                if (esLiquidacion)
                                {
                                    dondeVa = "liquidación";
                                    if (axdEntity_SalesQuotationMasterTable.ObraId != null)
                                    {
                                        dondeVa = "liquidación - obra id";
                                        obraID = axdEntity_SalesQuotationMasterTable.ObraId.ToString();
                                    }

                                    dondeVa = "dtPedidos";
                                    filaPedidos = dtPedidos.NewRow();
                                    dondeVa = "dtPedidos_fechaCreacion";
                                    filaPedidos[dtPedidos_fechaCreacion] = Convert.ToString(axdEntity_SalesQuotationTable.CreatedDateTime.Value.ToString("yyyyMMdd"));
                                    dondeVa = "dtPedidos_aaf";
                                    filaPedidos[dtPedidos_aaf] = Convert.ToString("");
                                    dondeVa = "dtPedidos_itemID";
                                    filaPedidos[dtPedidos_itemID] = Convert.ToString(axdEntity_SalesQuotationTable.ItemId.ToString());
                                    dondeVa = "dtPedidos_cantidad";
                                    filaPedidos[dtPedidos_cantidad] = Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty.ToString());
                                    dondeVa = "dtPedidos add row";
                                    dtPedidos.Rows.Add(filaPedidos);
                                    filaPedidos = null;

                                    dondeVa = "articulosLiquidacion";
                                    articulosLiquidacion = articulosLiquidacion + axdEntity_SalesQuotationTable.ItemId.ToString() + "|";
                                }

                                dondeVa = "Cálculos";
                                DataRow oItemTaximetro = dtTaximetro.NewRow();
                                taximetroNoConsumible = 0;
                                taximetroConsumible = 0;
                                precioTaximetroNoConsumible = 0;
                                precioTaximetroConsumible = 0;
                                costeSuperficie = 0;
                                superficie = 0;
                                calcularPorSuperficie = false;
                                metersInvoicing = false;
                                productType = "";

                                dondeVa = "hay precio";
                                precioCoste = 0;
                                if (hayPrecio)
                                {
                                    filaEncontrada = dtArticulos.Select("Articulo = '" + axdEntity_SalesQuotationTable.ItemId.ToString() + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Precio"])))
                                            precioCoste = Convert.ToDecimal(fila["Precio"]);
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Fecha"])))
                                            fechaPrecio = Convert.ToDateTime(fila["Fecha"]);

                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["PrecioOriginal"])))
                                            oItemTaximetro[COL_DATA_PRECIO_ORIGINAL] = Convert.ToDecimal(fila["PrecioOriginal"]).ToString("#,##0.00");
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["FechaCambio"])))
                                            oItemTaximetro[COL_DATA_FECHA_CAMBIO] = Convert.ToDateTime(fila["FechaCambio"]).ToString("dd/MM/yyyy");
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Cambio"])))
                                            oItemTaximetro[COL_DATA_CAMBIO] = Convert.ToDecimal(fila["Cambio"]).ToString("#,##0.000000");
                                    }
                                }

                                // recuperar datos de configuración del datatable
                                // RECUPERAR LOS DATOS DE CONFIGURACIÓN: TAXIMETRO - GASTOS - AJUSTES
                                dondeVa = "ROP_DatosConfiguracion";
                                dtDatosConfiguracion = new System.Data.DataTable();
                                horaDatosConfigurados_3 = DateTime.Now;
                                conexiones.crearConexion();
                                conexiones.comando = conexiones.conexion.CreateCommand();
                                conexiones.comando.CommandText = "ROP_DatosConfiguracion";
                                conexiones.comando.CommandTimeout = 240000;
                                conexiones.comando.CommandType = CommandType.StoredProcedure;
                                conexiones.comando.Parameters.AddWithValue("@empresa", cmbEmpresa.SelectedItem.ToString());
                                conexiones.comando.Parameters.AddWithValue("@usuario", usuario); // Environment.UserName);
                                adaptadorDatosConfiguracion = new SqlDataAdapter(conexiones.comando);
                                adaptadorDatosConfiguracion.Fill(dtDatosConfiguracion);
                                adaptadorDatosConfiguracion.Dispose();
                                conexiones.comando.Dispose();
                                conexiones.conexion.Close();
                                conexiones.conexion.Dispose();
                                horaDatosConfigurados_4 = DateTime.Now.Subtract(horaDatosConfigurados_3);

                                listaBonificacionesDesde.Clear();
                                listaBonificacionesHasta.Clear();
                                listaBonificacionesValor.Clear();
                                diasDesdeTaxNoConsumible = 0;
                                diasHastaTaxNoConsumible = 0;
                                filaEncontrada = dtDatosConfiguracion.Select("Itemid = '" + axdEntity_SalesQuotationTable.ItemId.ToString() + "'");
                                foreach (DataRow fila in filaEncontrada)
                                {
                                    if (Convert.ToString(fila["Concepto"]) == "Coste superficie")
                                        costeSuperficie = Convert.ToDecimal(fila["Valor"]);
                                    if (Convert.ToString(fila["Concepto"]) == "Taxímetro consumible")
                                    {
                                        taximetroConsumible = Convert.ToDecimal(fila["Valor"]) / 100;
                                        listaBonificacionesDesde.Add(Convert.ToInt32(fila["Desde"]));
                                        listaBonificacionesHasta.Add(Convert.ToInt32(fila["Hasta"]));
                                        listaBonificacionesValor.Add(Convert.ToDecimal(fila["Ajuste"]));

                                        calcularPorSuperficie = false;
                                        superficie = 0;
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Valorar"])))
                                            if (Convert.ToString(fila["Valorar"]).ToUpper() == "SUPERFICIE")
                                            {
                                                calcularPorSuperficie = true;
                                                superficie = Convert.ToDecimal(fila["Superficie"]);
                                            }
                                    }

                                    diasDesdeTaxNoConsumible = 0;
                                    diasHastaTaxNoConsumible = 0;
                                    if (Convert.ToString(fila["Concepto"]) == "Taxímetro no consumible" && (calcularPorSuperficie || taximetroConsumible == 0))
                                    { 
                                        taximetroNoConsumible = Convert.ToDecimal(fila["Valor"]) / 100;
                                        diasDesdeTaxNoConsumible = Convert.ToInt32(fila["Desde"]);
                                        diasHastaTaxNoConsumible = Convert.ToInt32(fila["Hasta"]);
                                    }

                                    metersInvoicing = Convert.ToBoolean(fila["MetersInvoicing"]);
                                    productType = Convert.ToString(fila["ProductTypeName_es"]);

                                    if (Convert.ToString(fila["Concepto"]) == "Coef. Corrección PS Nuevo")
                                        coeficienteNuevo = Convert.ToDecimal(fila["Valor"]);
                                    if (Convert.ToString(fila["Concepto"]) == "Coef. Corrección PS Usado")
                                        coeficienteUsado = Convert.ToDecimal(fila["Valor"]);
                                    tipoArticulo = Convert.ToString(fila["TipoArticulo"]);
                                }

                                if (tipoArticulo == "NUEVO")
                                {
                                    coeficienteUsar = coeficienteNuevo;
                                    oItemTaximetro[COL_DATA_PRECIO_CORRECION_NUEVO] = (precioCoste * coeficienteNuevo).ToString("#,##0.00");
                                }
                                else
                                {
                                    coeficienteUsar = coeficienteUsado;
                                    oItemTaximetro[COL_DATA_PRECIO_CORRECION_USADO] = (precioCoste * coeficienteUsado).ToString("#,##0.00");
                                }

                                oItemTaximetro[COL_DATA_CAPITULO] = "";
                                oItemTaximetro[COL_DATA_ITEM] = axdEntity_SalesQuotationTable.ItemId.ToString();
                                oItemTaximetro[COL_DATA_TIPO_ITEM] = tipoArticulo;
                                oItemTaximetro[COL_DATA_PRECIO] = precioCoste.ToString("#,##0.00");
                                oItemTaximetro[COL_DATA_FECHA] = fechaPrecio.ToString("dd/MM/yyyy");
                                oItemTaximetro[COL_DATA_TIPO_CAPITULO] = AxdEnum_SalesType.ToString().ToUpper();

                                if (AxdEnum_SalesType == tablaPedidos.AxdEnum_SalesType.Sales)
                                {
                                    if (tipoArticulo == "NUEVO")
                                        oItemTaximetro[COL_DATA_COEFICIENTE_NUEVO] = coeficienteNuevo.ToString("##0.00");
                                    else
                                        oItemTaximetro[COL_DATA_COEFICIENTE_USADO] = coeficienteUsado.ToString("##0.00");

                                    if (cantidadVenta == 0)
                                    {
                                        cantidadVenta = cantidadVenta + 1;
                                        capitulosVenta.Add(axdEntity_SalesQuotationTable.SalesId.ToString());
                                    }

                                    importeVenta = importeVenta + Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount);
                                    importeFacturacionVentaCapitulos = importeFacturacionVentaCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount);

                                    //if (axdEntity_SalesQuotationTable.ServiceLineType == tablaPedidos.AxdEnum_EcoResProductType.Service)
                                    if (productType.Trim().ToUpper() == "SERVICIO")
                                    {
                                        importeVentaServicio = importeVentaServicio + Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) ;
                                        importeFacturacionVentaServicioCapitulos = importeFacturacionVentaServicioCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount));
                                        if (cantidadVentaServicio == 0)
                                        {
                                            cantidadVentaServicio = cantidadVentaServicio + 1;
                                            capitulosVentaServicio.Add(axdEntity_SalesQuotationTable.SalesId.ToString());
                                        }
                                        oItemTaximetro[COL_DATA_IMPORTE_VENTA_SERVICIO] = Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount).ToString("#,##0.00");
                                    }
                                    else
                                    {
                                        importeVentaProducto = importeVentaProducto + Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount);
                                        importeFacturacionVentaProductoCapitulos = importeFacturacionVentaProductoCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount);
                                        if (cantidadVentaProducto == 0)
                                        {
                                            cantidadVentaProducto = cantidadVentaProducto + 1;
                                            capitulosVentaProducto.Add(axdEntity_SalesQuotationTable.SalesId.ToString());
                                        }
                                        oItemTaximetro[COL_DATA_IMPORTE_VENTA_PRODUCTO] = Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount).ToString("#,##0.00");
                                    }

                                    importeCosteVenta = importeCosteVenta + Convert.ToDecimal(axdEntity_SalesQuotationTable.QtyOrdered) * precioCoste * coeficienteUsar;
                                    importeCosteVentaCapitulos = importeCosteVentaCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationTable.QtyOrdered) * precioCoste * coeficienteUsar;

                                    // Gastos variables
                                    importeVentaGastosVariables = importeVentaGastosVariables + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosVariablesVenta);
                                    importeGastosVariablesVentaCapitulos = importeGastosVariablesVentaCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosVariablesVenta);

                                    // Gastos fijos BU
                                    importeVentaGastosFijosBU = importeVentaGastosFijosBU + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosFijosBUVenta);
                                    importeGastosFijosBUVentaCapitulos = importeGastosFijosBUVentaCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosFijosBUVenta);

                                    // Gastos fijos centrales
                                    importeVentaGastosFijosCentrales = importeVentaGastosFijosCentrales + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosFijosCentralesVenta);
                                    importeGastosFijosCentralesVentaCapitulos = importeGastosFijosCentralesVentaCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosFijosCentralesVenta);

                                    oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_VENTA] = Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount).ToString("#,##0.00");

                                    oItemTaximetro[COL_DATA_GASTO_VARIABLE] = gastosVariablesVenta.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_GASTO_VARIABLE] = (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosVariablesVenta).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_GASTO_FIJO_BU] = gastosFijosBUVenta.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_BU] = (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosFijosBUVenta).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_GASTO_FIJO_CENTRAL] = gastosFijosCentralesVenta.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_CENTRAL] = (Convert.ToDecimal(axdEntity_SalesQuotationTable.LineAmount) * gastosFijosCentralesVenta).ToString("#,##0.00");
                                }

                                if (AxdEnum_SalesType == tablaPedidos.AxdEnum_SalesType.Alquiler)
                                {
                                    if (cantidadAlquiler == 0)
                                    {
                                        cantidadAlquiler = cantidadAlquiler + 1;
                                        capitulosAlquiler.Add(axdEntity_SalesQuotationTable.SalesId.ToString());

                                        cantidadTaximetroConsumible = cantidadTaximetroConsumible + 1;
                                        capitulosTaximetroConsumible.Add(axdEntity_SalesQuotationTable.SalesId.ToString());

                                        cantidadTaximetroNoConsumible = cantidadTaximetroNoConsumible + 1;
                                        capitulosTaximetroNoConsumible.Add(axdEntity_SalesQuotationTable.SalesId.ToString());
                                    }

                                    // (unidades* PS* taximetro (consumible o no consumible) % *días) / 30
                                    importeAlquiler = importeAlquiler + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada));
                                    importeFacturacionAlquilerCapitulos = importeFacturacionAlquilerCapitulos + Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada);

                                    // Gastos variables
                                    importeAlquilerGastosVariables = importeAlquilerGastosVariables + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosVariablesAlquiler);
                                    importeGastosVariablesAlquilerCapitulos = importeGastosVariablesAlquilerCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosVariablesAlquiler);

                                    // Gastos fijos BU
                                    importeAlquilerGastosFijosBU = importeAlquilerGastosFijosBU + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosFijosBUAlquiler);
                                    importeGastosFijosBUAlquilerCapitulos = importeGastosFijosBUAlquilerCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosFijosBUAlquiler);

                                    // Gastos fijos centrales
                                    importeAlquilerGastosFijosCentrales = importeAlquilerGastosFijosCentrales + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosFijosCentralesAlquiler);
                                    importeGastosFijosCentralesAlquilerCapitulos = importeGastosFijosCentralesAlquilerCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosFijosCentralesAlquiler);

                                    oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_EURO_DIA] = Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia).ToString("#,##0.0000");
                                    oItemTaximetro[COL_DATA_DURACION_ESTIMADA] = Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_ALQUILER] = (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_GASTO_VARIABLE] = gastosVariablesAlquiler.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_GASTO_VARIABLE] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosVariablesAlquiler).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_GASTO_FIJO_BU] = gastosFijosBUAlquiler.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_BU] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosFijosBUAlquiler).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_GASTO_FIJO_CENTRAL] = gastosFijosCentralesAlquiler.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_GASTO_FIJO_CENTRAL] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * Convert.ToDecimal(axdEntity_SalesQuotationTable.EurDia) * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) * gastosFijosCentralesAlquiler).ToString("#,##0.00");

                                    if (taximetroConsumible == 0)
                                    {
                                        precioTaximetroNoConsumible = precioCoste;
                                    }
                                    else
                                    {
                                        if (calcularPorSuperficie)
                                        {
                                            if (!metersInvoicing)
                                            {
                                                precioTaximetroNoConsumible = (precioCoste - (costeSuperficie * superficie));
                                                precioTaximetroConsumible = 0;
                                            }
                                            else
                                            {
                                                precioTaximetroNoConsumible = (precioCoste - (costeSuperficie * superficie));
                                                precioTaximetroConsumible = (costeSuperficie * superficie);
                                            }
                                        }
                                        else
                                        {
                                            precioTaximetroNoConsumible = 0;
                                            precioTaximetroConsumible = precioCoste;
                                        }
                                    }

                                    if (calcularPorSuperficie)
                                    {
                                        if (!metersInvoicing)
                                            oItemTaximetro[COL_DATA_TIPO] = "SUPERFICIE / NO meters invoicing";
                                        else
                                            oItemTaximetro[COL_DATA_TIPO] = "SUPERFICIE";
                                    }
                                    else
                                        oItemTaximetro[COL_DATA_TIPO] = "";

                                    oItemTaximetro[COL_DATA_TAX_CONSUMIBLE] = taximetroConsumible.ToString("#,##0.0000");
                                    oItemTaximetro[COL_DATA_TAX_NO_CONSUMIBLE] = taximetroNoConsumible.ToString("#,##0.0000");
                                    oItemTaximetro[COL_DATA_SUPERFICIE] = superficie.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_COSTE_SUPERFICIE] = (costeSuperficie * superficie).ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_PRECIO_CONSUMIBLE] = precioTaximetroConsumible.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_PRECIO_NO_CONSUMIBLE] = precioTaximetroNoConsumible.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_DESDE] = diasDesdeTaxNoConsumible.ToString();
                                    oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_HASTA] = diasHastaTaxNoConsumible.ToString();

                                    oItemTaximetro[COL_DATA_AJUSTE] = "";
                                    //oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (0.00).ToString("#,##0.00");

                                    duracion = Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada);
                                    diasCalculados = 0;
                                    diasMultiplicar = 0;
                                    while (diasCalculados < duracion)
                                    {
                                        for (lineasBonificacion = 0; lineasBonificacion < listaBonificacionesDesde.Count; lineasBonificacion++)
                                        {
                                            if ((diasCalculados + diasxMes) >= listaBonificacionesDesde.ElementAt(lineasBonificacion) && (diasCalculados + diasxMes) <= listaBonificacionesHasta.ElementAt(lineasBonificacion))
                                            {
                                                if (duracion - diasCalculados >= diasxMes)
                                                    diasMultiplicar = diasxMes;
                                                else
                                                    diasMultiplicar = duracion - diasCalculados;
                                                bonificacionTaximetroConsumible = taximetroConsumible - (listaBonificacionesValor.ElementAt(lineasBonificacion) / 100);
                                                oItemTaximetro[COL_DATA_AJUSTE] = oItemTaximetro[COL_DATA_AJUSTE] + "Bonif. (" + bonificacionTaximetroConsumible.ToString("#,##0.0000") + ") * Dias(" + diasMultiplicar.ToString() + ") // ";
                                                if (oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE].ToString() == "")
                                                    oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes).ToString("#,##0.00");
                                                else
                                                    oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (Convert.ToDecimal(oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE]) + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes)).ToString("#,##0.00");
                                                importeCosteBonificacionTaximetroConsumible = importeCosteBonificacionTaximetroConsumible + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                                importeCosteBonificacionTaximetroConsumibleCapitulos = importeCosteBonificacionTaximetroConsumibleCapitulos + ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                            }
                                        }
                                        diasCalculados = diasCalculados + diasxMes;
                                    }
                                  
                                    importeCosteTaximetroConsumible = importeCosteTaximetroConsumible + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) / diasxMes;
                                    importeCosteTaximetroConsumibleCapitulos = importeCosteTaximetroConsumibleCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) / diasxMes;
                                    oItemTaximetro[COL_DATA_IMPORTE_TAX] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) / diasxMes).ToString("#,##0.00");

                                    if (Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada) <= diasHastaTaxNoConsumible)
                                        diasTaximetroNoConsumibleCalcular = Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada) - diasDesdeTaxNoConsumible;
                                    if (Convert.ToInt32(axdEntity_SalesQuotationTable.DuracionEstimada) >= diasHastaTaxNoConsumible)
                                        diasTaximetroNoConsumibleCalcular = diasHastaTaxNoConsumible - diasDesdeTaxNoConsumible;

                                    importeCosteTaximetroNoConsumible = importeCosteTaximetroNoConsumible + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                    importeCosteTaximetroNoConsumibleCapitulos = importeCosteTaximetroNoConsumibleCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                    oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes).ToString("#,##0.00");

                                    //importeCosteTaximetroNoConsumible = importeCosteTaximetroNoConsumible + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) / diasxMes;
                                    //importeCosteTaximetroNoConsumibleCapitulos = importeCosteTaximetroNoConsumibleCapitulos + (Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) / diasxMes;
                                    //oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty) * precioTaximetroNoConsumible * taximetroNoConsumible * Convert.ToDecimal(axdEntity_SalesQuotationTable.DuracionEstimada)) / diasxMes).ToString("#,##0.00");

                                    // Porte
                                    if (cantidadPorte == 0)
                                    {
                                        cantidadPorte = cantidadPorte + 1;
                                        capitulosPorte.Add(axdEntity_SalesQuotationTable.SalesId.ToString());
                                    }
                                }
                                dtTaximetro.Rows.Add(oItemTaximetro);
                                oItemTaximetro = null;
                            }

                            dec_importeCosteVentaCapitulos.Add(importeCosteVentaCapitulos);
                            dec_importeMargenVentaCapitulos.Add(importeFacturacionVentaCapitulos - importeCosteVentaCapitulos);

                            dec_importeFacturacionVentaServicioCapitulos.Add(importeFacturacionVentaServicioCapitulos);
                            dec_importeFacturacionVentaProductoCapitulos.Add(importeFacturacionVentaProductoCapitulos);

                            dec_importeGastosVariablesVentaCapitulos.Add(importeGastosVariablesVentaCapitulos);
                            dec_importeGastosFijosBUVentaCapitulos.Add(importeGastosFijosBUVentaCapitulos);
                            dec_importeGastosFijosCentralesVentaCapitulos.Add(importeGastosFijosCentralesVentaCapitulos);

                            dec_importeFacturacionPorteCapitulos.Add(importeFacturacionPorteCapitulos);
                            dec_importeCostePorteCapitulos.Add(importeCostePorteCapitulos);
                            dec_importeMargenPorteCapitulos.Add(importeFacturacionPorteCapitulos - importeCostePorteCapitulos);

                            dec_importeFacturacionAlquilerCapitulos.Add(importeFacturacionAlquilerCapitulos);
                            dec_importeMargenAlquilerCapitulos.Add(importeFacturacionAlquilerCapitulos - (importeCosteTaximetroNoConsumibleCapitulos + importeCosteTaximetroConsumibleCapitulos - importeCosteBonificacionTaximetroConsumibleCapitulos));

                            dec_importeCosteTaximetroNoConsumibleCapitulos.Add(importeCosteTaximetroNoConsumibleCapitulos);
                            dec_importeCosteBonificacionTaximetroConsumibleCapitulos.Add(importeCosteBonificacionTaximetroConsumibleCapitulos);
                            dec_importeCosteTaximetroConsumibleCapitulos.Add(importeCosteTaximetroConsumibleCapitulos);

                            dec_importeGastosVariablesAlquilerCapitulos.Add(importeGastosVariablesAlquilerCapitulos);
                            dec_importeGastosFijosBUAlquilerCapitulos.Add(importeGastosFijosBUAlquilerCapitulos);
                            dec_importeGastosFijosCentralesAlquilerCapitulos.Add(importeGastosFijosCentralesAlquilerCapitulos);

                            importeMargenAlquiler = importeAlquiler - (importeCosteTaximetroNoConsumible + importeCosteTaximetroConsumible - importeCosteBonificacionTaximetroConsumible);
                            importeMargenVenta = importeVenta - importeCosteVenta;
                            importeMargenPorte = 0;
                            horaDatosPase2_2 = DateTime.Now.Subtract(horaDatosPase2_1);
                        }
                    }
                    proxy_1.Close();
                    horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);
                }

                // SI HAY LIQUIDACIONES --> ALBARANES DE ENTREGA
                dec_importeCosteMPOMixto = 0;
                dec_importeCosteMPONuevo = 0;
                dec_importeCosteMPOUsado = 0;

                if (articulosLiquidacion != "" && obraID != "")
                {
                    // Buscar estado de los artículos 
                    dondeVa = "Datos estado artículos";

                    dtArticulosLiquidacion = localizarEstadoArticulos(usuario);
                    if (dtArticulosLiquidacion.Columns.Count == 0)
                        dondeVa = "va a petar // " + articulosLiquidacion + " // usuario // " + usuario;
                    // Actualizo AAF en el data de pedidos
                    //dondeVa = "Actualizar AAF en dtPedidos";
                    foreach (DataRow filaPedido in dtPedidos.Rows)
                    {
                        filaEncontrada = dtArticulosLiquidacion.Select("ItemId = '" + filaPedido[dtPedidos_itemID].ToString() + "'");
                        foreach (DataRow filaAAF in filaEncontrada)
                        {
                            filaPedido[dtPedidos_aaf] = Convert.ToString(filaAAF["AAF"]);
                            break;
                        }
                    }

                    // 40006251
                    dondeVa = "Datos ws albaranes de entrega";
                    System.Collections.IEnumerator enumerator_AlbaranEntrega;
                    // CABECERA
                    dondeVa = "contexto_AlbaranEntrega";
                    tablaAlbaranesEntrega.CallContext contexto_AlbaranEntrega = new tablaAlbaranesEntrega.CallContext();
                    contexto_AlbaranEntrega.Company = cmbEmpresa.SelectedItem.ToString();

                    dondeVa = "criterio";
                    tablaAlbaranesEntrega.QueryCriteria criterio_AlbaranEntrega = new tablaAlbaranesEntrega.QueryCriteria();
                    criterio_AlbaranEntrega.CriteriaElement = new tablaAlbaranesEntrega.CriteriaElement[1];

                    //ProjTable_AlbaranEntrega
                    criterio_AlbaranEntrega.CriteriaElement[0] = new tablaAlbaranesEntrega.CriteriaElement();
                    criterio_AlbaranEntrega.CriteriaElement[0].DataSourceName = "CustPackingSlipTrans";
                    criterio_AlbaranEntrega.CriteriaElement[0].FieldName = "ObraId";
                    criterio_AlbaranEntrega.CriteriaElement[0].Operator = tablaAlbaranesEntrega.Operator.Equal;
                    criterio_AlbaranEntrega.CriteriaElement[0].Value1 = obraID;

                    dondeVa = "credenciales";
                    tablaAlbaranesEntrega.CustPackingServiceClient proxy_AlbaranEntrega = new tablaAlbaranesEntrega.CustPackingServiceClient();
                    proxy_AlbaranEntrega.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                    proxy_AlbaranEntrega.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                    proxy_AlbaranEntrega.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                    dondeVa = "proxy";
                    tablaAlbaranesEntrega.AxdCustPacking AxdCustPacking = proxy_AlbaranEntrega.find(contexto_AlbaranEntrega, criterio_AlbaranEntrega);
                    datosGenerales = "";
                    dondeVa = "validar";
                    if (AxdCustPacking.CustPackingSlipTrans.Equals(null))
                    {
                        proxy_AlbaranEntrega.Close();
                    }
                    else
                    {
                        dondeVa = "recorrer albaranes";
                        enumerator_AlbaranEntrega = AxdCustPacking.CustPackingSlipTrans.GetEnumerator();

                        while (enumerator_AlbaranEntrega.MoveNext())
                        {
                            tablaAlbaranesEntrega.AxdEntity_CustPackingSlipTrans AxdEntity_CustPackingSlipTrans = (tablaAlbaranesEntrega.AxdEntity_CustPackingSlipTrans)enumerator_AlbaranEntrega.Current;
                            tipoArticulo = "";
                            filaEncontrada = dtArticulosLiquidacion.Select("ItemId = '" + AxdEntity_CustPackingSlipTrans.ItemId.ToString() + "'");
                            foreach (DataRow filaTipo in filaEncontrada)
                            {
                                dondeVa = "ajustar datos AAF y tipo de artículo";
                                tipoArticulo = Convert.ToString(filaTipo["TipoArticulo"]).ToUpper().Trim();
                                AAF = Convert.ToString(filaTipo["AAF"]);
                                //if (tipoArticulo.ToUpper().Trim() != "USADO")
                                //    hayDistintoUsado = true;

                                filaAlbaranes = dtAlbaranes.NewRow();
                                filaAlbaranes[dtAlbaranes_fecha] = Convert.ToString(AxdEntity_CustPackingSlipTrans.DeliveryDate.ToString("yyyyMMdd"));
                                filaAlbaranes[dtAlbaranes_aaf] = AAF;
                                filaAlbaranes[dtAlbaranes_itemID] = Convert.ToString(AxdEntity_CustPackingSlipTrans.ItemId.ToString());
                                filaAlbaranes[dtAlbaranes_cantidad] = Convert.ToDecimal(AxdEntity_CustPackingSlipTrans.Qty.ToString());
                                filaAlbaranes[dtAlbaranes_estado] = tipoArticulo;
                                dtAlbaranes.Rows.Add(filaAlbaranes);
                                filaAlbaranes = null;
                                break;
                            }
                        }
                    }
                    proxy_AlbaranEntrega.Close();

                    // APLASTAR DTPEDIDOS POR FECHA / AAF
                    // 40006251
                    dondeVa = "aplastar dtPedidos";
                    var dataPedidos = from d in dtPedidos.AsEnumerable()
                                      orderby d.Field<string>("FECHA_CREACION"), d.Field<string>("AAF")
                                      group d by new
                                      {
                                          FECHA_CREACION = d.Field<string>("FECHA_CREACION"),
                                          ITEM = d.Field<string>("ITEM_ID"),
                                          CODIGO_AAF = d.Field<string>("AAF"),
                                      } into grupo
                                      select new
                                      {
                                          fecha = grupo.Key.FECHA_CREACION,
                                          aaf = grupo.Key.CODIGO_AAF,
                                          articulo = grupo.Key.ITEM,
                                          cantidad = grupo.Sum(x => Convert.ToDecimal(x.Field<string>("SALESQTY")))
                                      };

                    //var data = from d in dtPedidos.AsEnumerable()
                    //           select d; 

                    dondeVa = "copiar a dtPedidosAgrupado";
                    System.Data.DataTable dtPedidosAgrupado = new System.Data.DataTable();

                    dondeVa = "creando columnas en dtPedidosAgrupado";
                    dtPedidosAgrupado.Columns.Add("FECHA");
                    dtPedidosAgrupado.Columns.Add("AAF");
                    dtPedidosAgrupado.Columns.Add("ARTICULO");
                    dtPedidosAgrupado.Columns.Add("CANTIDAD");
                    dtPedidosAgrupado.Columns.Add("USADO");
                    dtPedidosAgrupado.Columns.Add("MIXTO");
                    dtPedidosAgrupado.Columns.Add("NUEVO");

                    dondeVa = "copiando a dtPedidosAgrupado";
                    foreach (var itemPedidos in dataPedidos)
                        dtPedidosAgrupado.Rows.Add(itemPedidos.fecha.ToString(), itemPedidos.aaf.ToString(), itemPedidos.articulo.ToString(), itemPedidos.cantidad.ToString(), "0", "0", "0");

                    // APLASTAR DTALBARANES POR FECHA / AAF / TIPO
                    // 40006251
                    dondeVa = "aplastar dtAlbaranes";
                    var dataAlbaranes = from d in dtAlbaranes.AsEnumerable()
                                        orderby d.Field<string>("FECHA"), d.Field<string>("AAF"), d.Field<string>("Estado")
                                        group d by new
                                        {
                                            FECHA_ALBARAN = d.Field<string>("FECHA"),
                                            CODIGO_AAF = d.Field<string>("AAF"),
                                            ESTADO_ARTICULO = d.Field<string>("Estado"),
                                        } into grupo
                                        select new
                                        {
                                            fecha = grupo.Key.FECHA_ALBARAN,
                                            aaf = grupo.Key.CODIGO_AAF,
                                            estado = grupo.Key.ESTADO_ARTICULO,
                                            cantidad = grupo.Sum(x => Convert.ToDecimal(x.Field<string>("QTY")))
                                        };

                    dondeVa = "copiar a dtAlbaranesAgrupado";
                    System.Data.DataTable dtAlbaranesAgrupado = new System.Data.DataTable();

                    dondeVa = "creando columnas en dtAlbaranesAgrupado";
                    dtAlbaranesAgrupado.Columns.Add("FECHA");
                    dtAlbaranesAgrupado.Columns.Add("AAF");
                    dtAlbaranesAgrupado.Columns.Add("CANTIDAD");
                    dtAlbaranesAgrupado.Columns.Add("CANTIDAD_QUEDA");
                    dtAlbaranesAgrupado.Columns.Add("ESTADO");

                    dondeVa = "copiando a dtAlbaranesAgrupado";
                    foreach (var itemAlbaran in dataAlbaranes)
                        dtAlbaranesAgrupado.Rows.Add(itemAlbaran.fecha.ToString(), itemAlbaran.aaf.ToString(), itemAlbaran.cantidad.ToString(), itemAlbaran.cantidad.ToString(), itemAlbaran.estado.ToString());

                    // DISTRIBUIR DTPEDIDOS USADO / MIXTO / NUEVO
                    // 40006251
                    dondeVa = "distribuir usado / mixto / nuevo";
                    foreach (DataRow filaPedido in dtPedidosAgrupado.Rows)
                    {
                        faltan = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_cantidad]) - Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) - Convert.ToInt32(filaPedido[dtPedidosAgrupados_mixto]) - Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]);

                        // USADO
                        if (faltan > 0)
                        {
                            filaEncontrada = dtAlbaranesAgrupado.Select("AAF = '" + filaPedido[dtPedidosAgrupados_aaf].ToString() + "' AND FECHA <= '" + filaPedido[dtPedidosAgrupados_fechaCreacion].ToString() + "' AND ESTADO = 'USADO' AND CANTIDAD_QUEDA > '0'");
                            foreach (DataRow filaCantidad in filaEncontrada)
                            {
                                if (faltan > Convert.ToDecimal(filaPedido[dtAlbaranesAgrupados_cantidadQueda]))
                                {
                                    filaPedido[dtPedidosAgrupados_usado] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) + Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                    faltan = faltan - Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                    filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = "0";
                                }
                                if (faltan <= Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]))
                                {
                                    filaPedido[dtPedidosAgrupados_usado] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) + faltan;
                                    filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]) - faltan;
                                    faltan = 0;
                                }
                                if (faltan == 0)
                                    break;
                            }
                        }

                        // MIXTO
                        if (faltan > 0)
                        {
                            filaEncontrada = dtAlbaranesAgrupado.Select("AAF = '" + filaPedido[dtPedidosAgrupados_aaf].ToString() + "' AND FECHA <= '" + filaPedido[dtPedidosAgrupados_fechaCreacion].ToString() + "' AND ESTADO = 'MIXTO' AND CANTIDAD_QUEDA > '0'");
                            foreach (DataRow filaCantidad in filaEncontrada)
                            {
                                if (faltan > Convert.ToDecimal(filaPedido[dtAlbaranesAgrupados_cantidadQueda]))
                                {
                                    filaPedido[dtPedidosAgrupados_mixto] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) + Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                    faltan = faltan - Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                    filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = "0";
                                }
                                if (faltan <= Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]))
                                {
                                    filaPedido[dtPedidosAgrupados_mixto] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) + faltan;
                                    filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]) - faltan;
                                    faltan = 0;
                                }
                                if (faltan == 0)
                                    break;
                            }
                        }

                        // NUEVO
                        if (faltan > 0)
                        {
                            filaEncontrada = dtAlbaranesAgrupado.Select("AAF = '" + filaPedido[dtPedidosAgrupados_aaf].ToString() + "' AND FECHA <= '" + filaPedido[dtPedidosAgrupados_fechaCreacion].ToString() + "' AND ESTADO = 'NUEVO' AND CANTIDAD_QUEDA > '0'");
                            foreach (DataRow filaCantidad in filaEncontrada)
                            {
                                if (faltan > Convert.ToDecimal(filaPedido[dtAlbaranesAgrupados_cantidadQueda]))
                                {
                                    filaPedido[dtPedidosAgrupados_nuevo] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) + Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                    faltan = faltan - Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                    filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = "0";
                                }
                                if (faltan <= Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]))
                                {
                                    filaPedido[dtPedidosAgrupados_nuevo] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) + faltan;
                                    filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]) - faltan;
                                    faltan = 0;
                                }
                                if (faltan == 0)
                                    break;
                            }
                        }

                        // CALCULO filaPedido[dtPedidosAgrupados_usado] / filaPedido[dtPedidosAgrupados_mixto]  / filaPedido[dtPedidosAgrupados_nuevo]
                        dondeVa = "recuperar coeficiente";
                        filaEncontrada = dtDatosConfiguracion.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "' AND Concepto = 'Coef. Corrección PS Nuevo'");
                        foreach (DataRow filaTax in filaEncontrada)
                            coeficienteNuevo = Convert.ToDecimal(filaTax["Valor"]);
                        filaEncontrada = dtDatosConfiguracion.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "' AND Concepto = 'Coef. Corrección PS Usado'");
                        foreach (DataRow filaTax in filaEncontrada)
                            coeficienteUsado = Convert.ToDecimal(filaTax["Valor"]);
                        filaEncontrada = dtDatosConfiguracion.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "' AND Concepto = 'Coef. Corrección PS Mixto'");
                        foreach (DataRow filaTax in filaEncontrada)
                            coeficienteMixto = Convert.ToDecimal(filaTax["Valor"]);

                        precioCoste = 0;
                        dondeVa = "recuperar precio";
                        if (hayPrecio)
                        {
                            dondeVa = "buscar precio dataset // " + articulos;
                            filaEncontrada = dtArticulos.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "'");
                            foreach (DataRow filaprecio in filaEncontrada)
                            {
                                if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["Precio"])))
                                    precioCoste = Convert.ToDecimal(filaprecio["Precio"]);
                            }
                        }

                        dondeVa = "calcular coste MPO";
                        importeCosteVenta = importeCosteVenta + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) * precioCoste * coeficienteUsado);
                        importeCosteVenta = importeCosteVenta + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) * precioCoste * coeficienteMixto);
                        importeCosteVenta = importeCosteVenta + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) * precioCoste * coeficienteNuevo);

                        dec_importeCosteMPOMixto = dec_importeCosteMPOMixto + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) * precioCoste * coeficienteMixto);
                        dec_importeCosteMPONuevo = dec_importeCosteMPONuevo + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) * precioCoste * coeficienteNuevo);
                        dec_importeCosteMPOUsado = dec_importeCosteMPOUsado + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) * precioCoste * coeficienteUsado);
                    }
                }

                nombreInforme = Server.MapPath("~/Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                SLDocument sl = new SLDocument();
                sl.ImportDataTable(1, 1, dtTaximetro, true);
                sl.SaveAs(nombreInforme);
                btnAbrirExcel.Visible = true;

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_DatosArticulosFamiliasSubfamiliasEliminar";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@usuario", usuario);
                conexiones.comando.ExecuteNonQuery();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                pintarDatos();
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Calcular pedido // " + dondeVa + " // " + ex.Message;
            }
        }

        protected void calcularFicha()
        {
            string dondeVa = "";
            System.Data.DataTable dtMovimientos = new System.Data.DataTable();
            System.Data.DataTable dtPedidos = new System.Data.DataTable();
            System.Data.DataTable dtAlbaranes = new System.Data.DataTable();

            DataRow filaMovimiento;
            bool insertar = false;

            const int dtMovimientos_obra = 0;
            const int dtMovimientos_item = 1;
            const int dtMovimientos_fecha = 2;
            const int dtMovimientos_tipoMovimiento = 3;
            const int dtMovimientos_cantidad = 4;
            const int dtMovimientos_dias = 5;
            const int dtMovimientos_cantidadActual = 6;
            const int dtMovimientos_unidadesDias = 7;
            const int dtMovimientos_unidadesDiasNoTax = 8;

            const int dtPedidos_fechaCreacion = 0;
            const int dtPedidos_aaf = 1;
            const int dtPedidos_itemID = 2;
            const int dtPedidos_cantidad = 3;

            const int dtPedidosAgrupados_fechaCreacion = 0;
            const int dtPedidosAgrupados_aaf = 1;
            const int dtPedidosAgrupados_articulo = 2;
            const int dtPedidosAgrupados_cantidad = 3;
            const int dtPedidosAgrupados_usado = 4;
            const int dtPedidosAgrupados_mixto = 5;
            const int dtPedidosAgrupados_nuevo = 6;

            const int dtAlbaranes_fecha = 0;
            const int dtAlbaranes_aaf = 1;
            const int dtAlbaranes_itemID = 2;
            const int dtAlbaranes_cantidad = 3;
            const int dtAlbaranes_estado = 4;

            const int dtAlbaranesAgrupados_fecha = 0;
            const int dtAlbaranesAgrupados_aaf = 1;
            const int dtAlbaranesAgrupados_cantidad = 2;
            const int dtAlbaranesAgrupados_cantidadQueda = 3;
            const int dtAlbaranesAgrupados_estado = 4;

            try
            {
                System.Data.DataTable dtArticulosLiquidacion = new System.Data.DataTable();
                DataRow filaPedidos;
                DataRow filaAlbaranes;
                DataRow[] filaEncontrada;
                int lineaCapitulo;
                string articulosLiquidacion = "";
                string tipoArticulo = "";
                string AAF = "";
                decimal faltan = 0;

                SqlDataAdapter adaptadorDatosConfiguracion;
                System.Data.DataTable dtDatosConfiguracion;
                int lineasBonificacion;
                int duracion = 0;
                int diasCalculados = 0;
                int diasMultiplicar = 0;
                int totalUnidadesDias = 0;
                int totalUnidadesDiasNoTax = 0;
                string obraId="";
                int diasDesdeTaxNoConsumible = 0;
                int diasHastaTaxNoConsumible = 0;
                int diasTaximetroNoConsumibleCalcular = 0;

                dataDatos.DataSource = null;
                dataDatos.Columns.Clear();
                dataTiempos.DataSource = null;
                dataTiempos.Columns.Clear();

                dtMovimientos.Columns.Add("OBRA");
                dtMovimientos.Columns.Add("ITEM");
                dtMovimientos.Columns.Add("FECHA", typeof(DateTime));
                dtMovimientos.Columns.Add("TIPO MOVIMIENTO");
                dtMovimientos.Columns.Add("CANTIDAD");
                dtMovimientos.Columns.Add("DÍAS");
                dtMovimientos.Columns.Add("CANTIDAD ACTUAL");
                dtMovimientos.Columns.Add("UNIDADES DÍA");
                dtMovimientos.Columns.Add("UNIDADES DÍA TAX NO CONSUMIBLE");

                dtTaximetro = new DataTable();

                dtTaximetro.Columns.Add("CAPITULO");
                dtTaximetro.Columns.Add("TIPO CAPITULO");
                dtTaximetro.Columns.Add("ITEM");
                dtTaximetro.Columns.Add("NUEVO/USADO");
                dtTaximetro.Columns.Add("COEF. NUEVO");
                dtTaximetro.Columns.Add("COEF. USADO");
                dtTaximetro.Columns.Add("CANTIDAD");
                dtTaximetro.Columns.Add("EURO/UD/DIA");
                dtTaximetro.Columns.Add("DURACION ESTIMADA");
                dtTaximetro.Columns.Add("FACT ALQUILER");
                dtTaximetro.Columns.Add("FACT TOTAL VENTA");
                dtTaximetro.Columns.Add("FACT VENTA SERVICIO");
                dtTaximetro.Columns.Add("FACT VENTA PRODUCTO");
                dtTaximetro.Columns.Add("CANTIDAD DIAS");
                dtTaximetro.Columns.Add("FECHA PS");
                dtTaximetro.Columns.Add("PS/UD");
                dtTaximetro.Columns.Add("PS/UD ORIGINAL");
                dtTaximetro.Columns.Add("PS Corrección N");
                dtTaximetro.Columns.Add("PS Corrección U");
                dtTaximetro.Columns.Add("FECHA CAMBIO");
                dtTaximetro.Columns.Add("CAMBIO");
                dtTaximetro.Columns.Add("TIPO ARTICULO");
                dtTaximetro.Columns.Add("SUPERFICIE");
                dtTaximetro.Columns.Add("COSTE SUPERFICIE");
                dtTaximetro.Columns.Add("PRECIO CONSUMIBLE");
                dtTaximetro.Columns.Add("PRECIO NO CONSUMIBLE");
                dtTaximetro.Columns.Add("% TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("IMPORTE TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("% AJUSTE");
                dtTaximetro.Columns.Add("AJUSTE TAX CONSUMIBLE");
                dtTaximetro.Columns.Add("CANTIDAD DIAS TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("DIAS DESDE TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("DIAS HASTA TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("% TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("TAX NO CONSUMIBLE");
                dtTaximetro.Columns.Add("GASTO VARIABLE");
                dtTaximetro.Columns.Add("IMPORTE GASTO VARIABLE");
                dtTaximetro.Columns.Add("GASTO FIJO BU");
                dtTaximetro.Columns.Add("IMPORTE GASTO FIJO BU");
                dtTaximetro.Columns.Add("GASTO FIJO CENTRAL");
                dtTaximetro.Columns.Add("IMPORTE GASTO FIJO CENTRAL");

                decimal coeficienteNuevo = 0;
                decimal coeficienteUsado = 0;
                decimal coeficienteUsar = 0;
                decimal coeficienteMixto = 0;

                DateTime fechaPrecio = DateTime.Now;
                decimal gastosVariablesAlquiler = 0;
                decimal gastosVariablesVenta = 0;
                decimal gastosFijosBUAlquiler = 0;
                decimal gastosFijosBUVenta = 0;
                decimal gastosFijosCentralesAlquiler = 0;
                decimal gastosFijosCentralesVenta = 0;
                decimal bonificacionTaximetroConsumible = 0;

                bool metersInvoicing = false;
                string productType = "";
                bool calcularPorSuperficie = false;
                decimal superficie = 0;
                SqlDataReader dr;
                decimal costeSuperficie = 0;
                string articulosConfiguracion = "";
                decimal taximetroNoConsumible = 0;
                decimal taximetroConsumible = 0;
                decimal precioTaximetroNoConsumible = 0;
                decimal precioTaximetroConsumible = 0;
                string IDCapitulo = "";
                string tabla = "";
                string campo = "";
                int dias = 0;
                int diasxMes = 0;
                int diasDiferencia = 0;
                int diasRestar = 0;
                DateTime fechaPrecios;
                DateTime fechaOfertaPedido;
                bool hayPrecio = false;
                System.Data.DataTable dtArticulos = new System.Data.DataTable();
                bool primeraVez = true;
                string articulos = "";
                string articulosCambio = "";
                decimal precioCoste = 0;
                decimal importeFacturacionAlquilerCapitulos = 0;
                decimal importeCosteTaximetroNoConsumibleCapitulos = 0;
                decimal importeCosteTaximetroConsumibleCapitulos = 0;
                decimal importeFacturacionVentaCapitulos = 0;
                decimal importeFacturacionVentaServicioCapitulos = 0;
                decimal importeFacturacionVentaProductoCapitulos = 0;
                decimal importeCosteVentaCapitulos = 0;
                decimal importeFacturacionPorteCapitulos = 0;
                decimal importeCostePorteCapitulos = 0;
                decimal importeGastosVariablesVentaCapitulos = 0;
                decimal importeGastosVariablesAlquilerCapitulos = 0;
                decimal importeGastosFijosBUVentaCapitulos = 0;
                decimal importeGastosFijosBUAlquilerCapitulos = 0;
                decimal importeGastosFijosCentralesVentaCapitulos = 0;
                decimal importeGastosFijosCentralesAlquilerCapitulos = 0;
                decimal importeCosteBonificacionTaximetroConsumibleCapitulos = 0;

                delegacion = "";
                nombreFicha = "";

                dondeVa = "WS FICHA";
                System.Collections.IEnumerator enumerator_Obra;
                // CABECERA
                tablaObras.CallContext contexto_Obra = new tablaObras.CallContext();
                contexto_Obra.Company = cmbEmpresa.SelectedItem.ToString();

                tablaObras.QueryCriteria criterio_Obra = new tablaObras.QueryCriteria();
                criterio_Obra.CriteriaElement = new tablaObras.CriteriaElement[1];

                //ProjTable_1
                dondeVa = "WS FICHA CRITERIOS";
                criterio_Obra.CriteriaElement[0] = new tablaObras.CriteriaElement();
                criterio_Obra.CriteriaElement[0].DataSourceName = "ObraTable_1";
                criterio_Obra.CriteriaElement[0].FieldName = "ObraId";
                criterio_Obra.CriteriaElement[0].Operator = tablaObras.Operator.Equal;
                criterio_Obra.CriteriaElement[0].Value1 = txtNumero.Text;

                dondeVa = "WS FICHA CONEXION";
                tablaObras.ObrasServiceClient proxy_Obra = new tablaObras.ObrasServiceClient();
                proxy_Obra.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_Obra.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_Obra.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                dondeVa = "WS FICHA PROXY";
                tablaObras.AxdObras axdtablaObras_1 = proxy_Obra.find(contexto_Obra, criterio_Obra);
                if (axdtablaObras_1.ObraTable_1.Equals(null))
                {
                    proxy_Obra.Close();
                }
                else
                {
                    enumerator_Obra = axdtablaObras_1.ObraTable_1.GetEnumerator();
                    dondeVa = "WS FICHA RECORRER";
                    while (enumerator_Obra.MoveNext())
                    {
                        dondeVa = "WS FICHA TABLA 1";
                        tablaObras.AxdEntity_ObraTable_1 AxdEntity_ObraTable = (tablaObras.AxdEntity_ObraTable_1)enumerator_Obra.Current;
                        dondeVa = "WS FICHA TABLA 2";
                        moneda = AxdEntity_ObraTable.CurrencyCode;

                        if (AxdEntity_ObraTable.FechaCierre is null)
                        {
                            dondeVa = "WS FICHA SIN FECHA CIERRE";
                            lblTituloInformacion.Text = "Calcular ficha";
                            lblMensajeInformacion.Text = "La ficha no está cerrada, por los momentos no se puede calcular.";
                            mpeInformacion.Show();
                            proxy_Obra.Close();
                            return;
                        }
                        dondeVa = "WS FICHA CON FECHA CIERRE";
                        break;
                    }
                }
                proxy_Obra.Close();
                dondeVa = "WS FICHA SALIR";

                // Parámetros
                conexiones.crearConexion();
                conexiones.consulta = "sp_ROP_ConfiguracionFijaConsulta";
                conexiones.comando = new SqlCommand(conexiones.consulta, conexiones.conexion);
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                dr = conexiones.comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    diasxMes = Convert.ToInt32(dr["COF_diasCalculo"]);
                    diasDiferencia = Convert.ToInt32(dr["COF_OfertaDiasEntreFechaOfertaFechaPedido"]);
                    diasRestar = Convert.ToInt32(dr["COF_OfertaDiasRestarFechaPedido"]);
                }
                dr.Close();
                dr.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                txtNombreOferta.Text = "";

                System.Collections.IEnumerator enumerator_1;

                horaTotal_1 = DateTime.Now;
                horaLlamada_1 = DateTime.Now;

                // CABECERA
                cantidadAlquiler = 0;
                cantidadTaximetroNoConsumible = 0;
                cantidadTaximetroConsumible = 0;
                cantidadVenta = 0;
                cantidadPorte = 0;
                importeAlquiler = 0;
                importeVenta = 0;
                importeVentaServicio = 0;
                importeVentaProducto = 0;
                importePorte = 0;
                importeCosteTaximetroNoConsumible = 0;
                importeCosteTaximetroConsumible = 0;
                importeCosteVenta = 0;
                importeCostePorte = 0;
                importeMargenAlquiler = 0;
                importeMargenVenta = 0;
                importeMargenPorte = 0;

                dondeVa = "RECUPERAR DATOS DE TIPOS MOVIMIENTOS DIAS";
                // RECUPERAR DATOS DE TIPOS MOVIMIENTOS DIAS
                dtDatosConfiguracion = new System.Data.DataTable();
                horaDatosConfigurados_3 = DateTime.Now;
                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "MovimientosAjusteFechaConsulta";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                adaptadorDatosConfiguracion = new SqlDataAdapter(conexiones.comando);
                adaptadorDatosConfiguracion.Fill(dtDatosConfiguracion);
                adaptadorDatosConfiguracion.Dispose();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                dondeVa = "LLAMADA";
                // CABECERA
                tablaInventario.CallContext contexto_1 = new tablaInventario.CallContext();
                contexto_1.Company = cmbEmpresa.SelectedItem.ToString();

                tablaInventario.QueryCriteria criterio_1 = new tablaInventario.QueryCriteria();
                criterio_1.CriteriaElement = new tablaInventario.CriteriaElement[1];

                criterio_1.CriteriaElement[0] = new tablaInventario.CriteriaElement();
                criterio_1.CriteriaElement[0].DataSourceName = "InventTrans_1";
                criterio_1.CriteriaElement[0].FieldName = "obraid";
                criterio_1.CriteriaElement[0].Operator = tablaInventario.Operator.Equal;
                criterio_1.CriteriaElement[0].Value1 = txtNumero.Text;

                tablaInventario.InventTransOriginServiceClient proxy_1 = new tablaInventario.InventTransOriginServiceClient();
                proxy_1.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_1.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_1.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                tablaInventario.AxdInventTrans AxdInventTrans = proxy_1.find(contexto_1, criterio_1);

                if (AxdInventTrans.InventTrans_1.Equals(null))
                {
                    proxy_1.Close();
                }
                else
                {
                    dondeVa = "HAY DATOS";
                    enumerator_1 = AxdInventTrans.InventTrans_1.GetEnumerator();
                    articulosConfiguracion = "";
                    listaArticulosPedido.Clear();
                    while (enumerator_1.MoveNext())
                    {
                        dondeVa = "RECORRER";
                        tablaInventario.AxdEntity_InventTrans_1 AxdEntity_InventTrans_1 = (tablaInventario.AxdEntity_InventTrans_1)enumerator_1.Current;

                        obraId = AxdEntity_InventTrans_1.ObraId;

                        insertar = true;
                        articulosConfiguracion = articulosConfiguracion + AxdEntity_InventTrans_1.ItemId.ToString() + "|";
                        
                        // artículos si hay ficha
                        if (!listaArticulosPedido.Contains(AxdEntity_InventTrans_1.ItemId.ToString()))
                        {
                            listaArticulosPedido.Add(AxdEntity_InventTrans_1.ItemId.ToString());
                            if (AxdEntity_InventTrans_1.ValueDateCashFlow.Value.ToString().Length > 0)
                                listaFechasPedido.Add(Convert.ToDateTime(AxdEntity_InventTrans_1.ValueDateCashFlow.Value));
                            else
                                listaFechasPedido.Add(new DateTime());
                        }

                        filaEncontrada = dtMovimientos.Select("ITEM = '" + AxdEntity_InventTrans_1.ItemId.ToString() + "' AND FECHA = '" + AxdEntity_InventTrans_1.ValueDateCashFlow.ToString() + "' AND [TIPO MOVIMIENTO] = '" + AxdEntity_InventTrans_1.InventTransMovementType.ToString() + "'");
                        foreach (DataRow fila in filaEncontrada)
                        {
                            fila["CANTIDAD"] = (Convert.ToDecimal(fila["CANTIDAD"]) + Convert.ToDecimal(AxdEntity_InventTrans_1.Qty)).ToString();
                            fila.AcceptChanges();
                            insertar = false;
                        }
                        
                        if (insertar)
                        {
                            filaMovimiento = dtMovimientos.NewRow();
                            filaMovimiento[dtMovimientos_obra] = AxdEntity_InventTrans_1.ObraId.ToString();
                            filaMovimiento[dtMovimientos_item] = AxdEntity_InventTrans_1.ItemId.ToString();
                            filaMovimiento[dtMovimientos_fecha] = Convert.ToDateTime(AxdEntity_InventTrans_1.ValueDateCashFlow.ToString());
                            filaMovimiento[dtMovimientos_tipoMovimiento] = AxdEntity_InventTrans_1.InventTransMovementType.ToString();
                            filaMovimiento[dtMovimientos_cantidad] = AxdEntity_InventTrans_1.Qty.ToString();
                            filaMovimiento[dtMovimientos_dias] = "";
                            filaMovimiento[dtMovimientos_cantidadActual] = "";
                            filaMovimiento[dtMovimientos_unidadesDias] = "";
                            filaMovimiento[dtMovimientos_unidadesDiasNoTax] = "";
                            dtMovimientos.Rows.Add(filaMovimiento);
                            filaMovimiento = null;
                        }
                    }

                    fechaPrecios = new DateTime();
                    if (!String.IsNullOrEmpty(obraId))
                    {
                        dondeVa = "fecha base ficha artículo: entró";
                        fechaBaseFichaArticulo(obraId, diasDiferencia, diasRestar);
                        dondeVa = "fecha base ficha artículo: salió";

                        fechaPrecios = DateTime.Now;
                        tabla = "fechaBaseFichaArticulo";
                        IDCapitulo = "Ficha " + obraId;
                        campo = "fechaBaseFichaArticulo";

                        dondeVa = "crear listado artículos entró " + listaArticulosPedido.Count.ToString() + " // " + listaFechasPedido.Count.ToString();
                        articulos = ";";
                        articulosCambio = ";";
                        for (lineaCapitulo = 0; lineaCapitulo < listaArticulosPedido.Count; lineaCapitulo++)
                        {
                            dondeVa = "1 crear listado artículos va en " + lineaCapitulo.ToString();
                            articulos = articulos + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";
                            dondeVa = "2 crear listado artículos va en " + lineaCapitulo.ToString();
                            articulosCambio = articulosCambio + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";
                        }
                        dondeVa = "crear listado artículos salió";
                    }

                    if (fechaPrecios != new DateTime())
                    {
                        dondeVa = "insert log";
                        conexiones.crearConexion();
                        conexiones.comando = conexiones.conexion.CreateCommand();
                        conexiones.comando.CommandText = "sp_ROP_FechaBaseOfertaIncluir";
                        conexiones.comando.CommandType = CommandType.StoredProcedure;
                        conexiones.comando.Parameters.AddWithValue("@FBO_Oferta", txtNumero.Text);
                        conexiones.comando.Parameters.AddWithValue("@FBO_Capitulo", IDCapitulo);
                        conexiones.comando.Parameters.AddWithValue("@FBO_Fecha", fechaPrecios);
                        conexiones.comando.Parameters.AddWithValue("@FBO_Articulo", articulos.Replace("'", ""));
                        conexiones.comando.Parameters.AddWithValue("@FBO_TablaFechaSeleccionada", tabla);
                        conexiones.comando.Parameters.AddWithValue("@FBO_CampoFechaSeleccionada1", campo);
                        conexiones.comando.Parameters.AddWithValue("@FBO_DiasDiferencia", dias);
                        conexiones.comando.Parameters.AddWithValue("@primeraVez", primeraVez);
                        conexiones.comando.ExecuteNonQuery();
                        conexiones.conexion.Close();
                        primeraVez = false;

                        dondeVa = "localizar precios artículos";
                        dtArticulos = localizarPreciosArticulos(cmbEmpresa.SelectedItem.ToString(), "", articulos, moneda, articulosCambio, txtNumero.Text);
                        hayPrecio = true;
                    }


                    dondeVa = "INFORMACION ARTICULOS";
                    // RECUPERAR INFORMACIÓN DE LOS ARTÍCULOS
                    horaDatosConfigurados_1 = DateTime.Now;
                    datosSQL.datosConfigurados(cmbEmpresa.SelectedItem.ToString(), articulosConfiguracion, usuario);
                    horaDatosConfigurados_2 = DateTime.Now.Subtract(horaDatosConfigurados_1);

                    dondeVa = "TAXIMETRO";
                    // RECUPERAR LOS DATOS DE CONFIGURACIÓN: TAXIMETRO - GASTOS - AJUSTES
                    dtDatosConfiguracion = new System.Data.DataTable();
                    horaDatosConfigurados_3 = DateTime.Now;
                    conexiones.crearConexion();
                    conexiones.comando = conexiones.conexion.CreateCommand();
                    conexiones.comando.CommandText = "ROP_DatosConfiguracion";
                    conexiones.comando.CommandTimeout = 240000;
                    conexiones.comando.CommandType = CommandType.StoredProcedure;
                    conexiones.comando.Parameters.AddWithValue("@empresa", cmbEmpresa.SelectedItem.ToString());
                    conexiones.comando.Parameters.AddWithValue("@usuario", usuario); // Environment.UserName);
                    adaptadorDatosConfiguracion = new SqlDataAdapter(conexiones.comando);
                    adaptadorDatosConfiguracion.Fill(dtDatosConfiguracion);
                    adaptadorDatosConfiguracion.Dispose();
                    conexiones.comando.Dispose();
                    conexiones.conexion.Close();
                    conexiones.conexion.Dispose();
                    horaDatosConfigurados_4 = DateTime.Now.Subtract(horaDatosConfigurados_3);

                    dondeVa = "ORDENAR EL DATATABLE";
                    // ORDENAR EL DATATBLE
                    DataView dtV = dtMovimientos.DefaultView;
                    dtV.Sort = "ITEM ASC, FECHA ASC";
                    dtMovimientos = dtV.ToTable();

                    // RECORRERLO Y CALCULAR
                    string item = "";
                    string tipoMovimiento = "";
                    DateTime fecha = DateTime.Today;
                    decimal cantidad = 0;
                    decimal cantidadAnterior = 0;
                    int restarDias = 0;
                    int sumarDias = 0;

                    dondeVa = "CALCULAR CANTIDAD DIAS";
                    foreach (DataRow fila in dtMovimientos.Rows)
                    {
                        if (item != fila[dtMovimientos_item].ToString())
                        {
                            item = fila[dtMovimientos_item].ToString();
                            tipoMovimiento = fila[dtMovimientos_tipoMovimiento].ToString();

                            restarDias = 0;
                            sumarDias = 0;
                            //filaEncontrada = dtDatosConfiguracion.Select("MOV_Tipo = '" + tipoMovimiento + "'");
                            //foreach (DataRow filaMov in filaEncontrada)
                            //{
                            //    if (Convert.ToString(filaMov["MOV_Signo"]) == "+")
                            //        sumarDias = Convert.ToInt32(filaMov["MOV_Dias"]);
                            //    else
                            //        restarDias = (-1) * Convert.ToInt32(filaMov["MOV_Dias"]);
                            //}

                            fecha = Convert.ToDateTime(Convert.ToDateTime(fila[dtMovimientos_fecha].ToString()).ToShortDateString());
                            cantidad = Convert.ToDecimal(fila[dtMovimientos_cantidad].ToString());
                            cantidadAnterior = cantidad;

                            fila[dtMovimientos_cantidadActual] = cantidad;
                            fila[dtMovimientos_dias] = "";
                            fila[dtMovimientos_unidadesDias] = "";
                            fila[dtMovimientos_unidadesDiasNoTax] = "";
                            dias = 0;
                        }
                        else
                        {
                            if (fecha != Convert.ToDateTime(Convert.ToDateTime(fila[dtMovimientos_fecha].ToString()).ToShortDateString()))
                            {
                                fila[dtMovimientos_dias] = Convert.ToInt32(Convert.ToDateTime(Convert.ToDateTime(fila[dtMovimientos_fecha].ToString()).ToShortDateString()).Subtract(fecha).Days) + sumarDias + restarDias.ToString();
                                dias = Convert.ToInt32(fila[dtMovimientos_dias].ToString());
                            }
                            fila[dtMovimientos_cantidadActual] = (cantidad + Convert.ToDecimal(fila[dtMovimientos_cantidad].ToString()));
                            totalUnidadesDias = Convert.ToInt32(cantidadAnterior * dias);
                            fila[dtMovimientos_unidadesDias] = (totalUnidadesDias / 10).ToString();

                            diasDesdeTaxNoConsumible = 0;
                            diasHastaTaxNoConsumible = 0;
                            filaEncontrada = dtDatosConfiguracion.Select("Itemid = '" + fila[dtMovimientos_item].ToString() + "' AND Concepto = 'Taxímetro no consumible'");
                            foreach (DataRow filaTax in filaEncontrada)
                            {
                                diasDesdeTaxNoConsumible = Convert.ToInt32(filaTax["Desde"]);
                                diasHastaTaxNoConsumible = Convert.ToInt32(filaTax["Hasta"]);
                            }

                            if (dias <= diasHastaTaxNoConsumible)
                                diasTaximetroNoConsumibleCalcular = dias - diasDesdeTaxNoConsumible;
                            if (dias >= diasHastaTaxNoConsumible)
                                diasTaximetroNoConsumibleCalcular = diasHastaTaxNoConsumible - diasDesdeTaxNoConsumible;

                            fila[dtMovimientos_unidadesDiasNoTax] = ((Convert.ToInt32(cantidadAnterior * diasTaximetroNoConsumibleCalcular)) / 10).ToString();

                            fecha = Convert.ToDateTime(fila[dtMovimientos_fecha].ToString());
                            cantidadAnterior = Convert.ToDecimal(fila[dtMovimientos_cantidadActual].ToString());
                            cantidad = Convert.ToDecimal(fila[dtMovimientos_cantidadActual].ToString());
                        }
                    }

                    dondeVa = "RECORRER DATASET";
                    // recorrer dataset de artículos / cantidad por dia
                    foreach (DataRow fila in dtMovimientos.Rows)
                    {
                        dondeVa = "RECOGER DURACION";
                        if (fila[dtMovimientos_dias].ToString() != "")
                            duracion =  Convert.ToInt32(fila[dtMovimientos_dias].ToString()) / 10;

                        if (fila[dtMovimientos_unidadesDias].ToString() != "")
                        {
                            dondeVa = "LINEA A TOMAR EN CUENTA";
                            DataRow oItemTaximetro = dtTaximetro.NewRow();
                            taximetroNoConsumible = 0;
                            taximetroConsumible = 0;
                            precioTaximetroNoConsumible = 0;
                            precioTaximetroConsumible = 0;
                            costeSuperficie = 0;
                            superficie = 0;
                            calcularPorSuperficie = false;
                            metersInvoicing = false;
                            productType = "";

                            precioCoste = 0;
                            if (hayPrecio)
                            {
                                dondeVa = "buscar precio dataset // " + articulos;
                                filaEncontrada = dtArticulos.Select("Articulo = '" + fila[dtMovimientos_item].ToString() + "'");
                                foreach (DataRow filaprecio in filaEncontrada)
                                {
                                    dondeVa = "encontró precio coste dataset // delegacion y nombre";
                                    if (delegacion =="")
                                    {
                                        delegacion = Convert.ToString(filaprecio["Delegacion"]);
                                        nombreFicha = Convert.ToString(filaprecio["NombreFicha"]);
                                    }

                                    dondeVa = "encontró precio coste dataset // " + articulosCambio + " // ";
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["Precio"])))
                                        precioCoste = Convert.ToDecimal(filaprecio["Precio"]);
                                    dondeVa = "fecha dataset // " + articulosCambio + " // ";
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["Fecha"])))
                                        fechaPrecio = Convert.ToDateTime(filaprecio["Fecha"]);

                                    dondeVa = "Precio original // " + articulosCambio + " // ";
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["PrecioOriginal"])))
                                        oItemTaximetro[COL_DATA_PRECIO_ORIGINAL] = Convert.ToDecimal(filaprecio["PrecioOriginal"]).ToString("#,##0.00");
                                    dondeVa = "Fecha cambio // " + articulosCambio + " // ";
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["FechaCambio"])))
                                        oItemTaximetro[COL_DATA_FECHA_CAMBIO] = Convert.ToDateTime(filaprecio["FechaCambio"]).ToString("dd/MM/yyyy");
                                    dondeVa = "Cambio // " + articulosCambio + " // ";
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["Cambio"])))
                                        oItemTaximetro[COL_DATA_CAMBIO] = Convert.ToDecimal(filaprecio["Cambio"]).ToString("#,##0.000000");
                                }
                            }

                            oItemTaximetro[COL_DATA_CAPITULO] = "";
                            oItemTaximetro[COL_DATA_ITEM] = fila[dtMovimientos_item].ToString();
                            oItemTaximetro[COL_DATA_PRECIO] = "";
                            oItemTaximetro[COL_DATA_FECHA] = "";
                            oItemTaximetro[COL_DATA_TIPO_CAPITULO] = "";
                            dondeVa = "Cantidad días variable // ";
                            oItemTaximetro[COL_DATA_CANTIDAD_DIAS] = fila[dtMovimientos_unidadesDias].ToString();
                            oItemTaximetro[COL_DATA_CANTIDAD_DIAS_NO_TAX] = fila[dtMovimientos_unidadesDiasNoTax].ToString();
                            dondeVa = "Cantidad días FIN // ";

                            // recuperar datos de configuración del datatable
                            listaBonificacionesDesde.Clear();
                            listaBonificacionesHasta.Clear();
                            listaBonificacionesValor.Clear();
                            diasDesdeTaxNoConsumible = 0;
                            diasHastaTaxNoConsumible = 0;
                            filaEncontrada = dtDatosConfiguracion.Select("Itemid = '" + fila[dtMovimientos_item].ToString() + "'");
                            foreach (DataRow filaTax in filaEncontrada)
                            {
                                dondeVa = "ENCONTRO TAX";

                                if (Convert.ToString(filaTax["Concepto"]) == "Coste superficie")
                                    costeSuperficie = Convert.ToDecimal(filaTax["Valor"]);
                                if (Convert.ToString(filaTax["Concepto"]) == "Taxímetro consumible")
                                {
                                    taximetroConsumible = Convert.ToDecimal(filaTax["Valor"]) / 100;
                                    listaBonificacionesDesde.Add(Convert.ToInt32(filaTax["Desde"]));
                                    listaBonificacionesHasta.Add(Convert.ToInt32(filaTax["Hasta"]));
                                    listaBonificacionesValor.Add(Convert.ToDecimal(filaTax["Ajuste"]));

                                    calcularPorSuperficie = false;
                                    superficie = 0;
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaTax["Valorar"])))
                                        if (Convert.ToString(filaTax["Valorar"]).ToUpper() == "SUPERFICIE")
                                        {
                                            calcularPorSuperficie = true;
                                            superficie = Convert.ToDecimal(filaTax["Superficie"]);
                                        }
                                }

                                diasDesdeTaxNoConsumible = 0;
                                diasHastaTaxNoConsumible = 0;
                                if (Convert.ToString(filaTax["Concepto"]) == "Taxímetro no consumible" && (calcularPorSuperficie || taximetroConsumible == 0))
                                {
                                    taximetroNoConsumible = Convert.ToDecimal(filaTax["Valor"]) / 100;
                                    diasDesdeTaxNoConsumible = Convert.ToInt32(filaTax["Desde"]);
                                    diasHastaTaxNoConsumible = Convert.ToInt32(filaTax["Hasta"]);
                                }
                                metersInvoicing = Convert.ToBoolean(filaTax["MetersInvoicing"]);
                                productType = Convert.ToString(filaTax["ProductTypeName_es"]);

                                if (Convert.ToString(filaTax["Concepto"]) == "Coef. Corrección PS Nuevo")
                                    coeficienteNuevo = Convert.ToDecimal(filaTax["Valor"]);
                                if (Convert.ToString(filaTax["Concepto"]) == "Coef. Corrección PS Usado")
                                    coeficienteUsado = Convert.ToDecimal(filaTax["Valor"]);
                                tipoArticulo = Convert.ToString(filaTax["TipoArticulo"]);
                            }

                            oItemTaximetro[COL_DATA_TIPO_ITEM] = tipoArticulo;
                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_DESDE] = diasDesdeTaxNoConsumible.ToString();
                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_HASTA] = diasHastaTaxNoConsumible.ToString();
                            if (tipoArticulo == "NUEVO")
                            {
                                coeficienteUsar = coeficienteNuevo;
                                oItemTaximetro[COL_DATA_PRECIO_CORRECION_NUEVO] = (precioCoste * coeficienteNuevo).ToString("#,##0.00");
                            }
                            else
                            {
                                coeficienteUsar = coeficienteUsado;
                                oItemTaximetro[COL_DATA_PRECIO_CORRECION_USADO] = (precioCoste * coeficienteUsado).ToString("#,##0.00");
                            }

                            if (taximetroConsumible == 0)
                            {
                                precioTaximetroNoConsumible = precioCoste;
                            }
                            else
                            {
                                if (calcularPorSuperficie)
                                {
                                    if (!metersInvoicing)
                                    {
                                        precioTaximetroNoConsumible = (precioCoste - (costeSuperficie * superficie));
                                        precioTaximetroConsumible = 0;
                                    }
                                    else
                                    {
                                        precioTaximetroNoConsumible = (precioCoste - (costeSuperficie * superficie));
                                        precioTaximetroConsumible = (costeSuperficie * superficie);
                                    }
                                }
                                else
                                {
                                    precioTaximetroNoConsumible = 0;
                                    precioTaximetroConsumible = precioCoste;
                                }
                            }

                            if (calcularPorSuperficie)
                            {
                                if (!metersInvoicing)
                                    oItemTaximetro[COL_DATA_TIPO] = "SUPERFICIE / NO meters invoicing";
                                else
                                    oItemTaximetro[COL_DATA_TIPO] = "SUPERFICIE";
                            }
                            else
                                oItemTaximetro[COL_DATA_TIPO] = "";

                            dondeVa = "CALCULAR TAX";
                            oItemTaximetro[COL_DATA_TAX_CONSUMIBLE] = taximetroConsumible.ToString("#,##0.0000");
                            oItemTaximetro[COL_DATA_TAX_NO_CONSUMIBLE] = taximetroNoConsumible.ToString("#,##0.0000");
                            oItemTaximetro[COL_DATA_SUPERFICIE] = superficie.ToString("#,##0.00");
                            oItemTaximetro[COL_DATA_COSTE_SUPERFICIE] = (costeSuperficie * superficie).ToString("#,##0.00");
                            oItemTaximetro[COL_DATA_PRECIO_CONSUMIBLE] = precioTaximetroConsumible.ToString("#,##0.00");
                            oItemTaximetro[COL_DATA_PRECIO_NO_CONSUMIBLE] = precioTaximetroNoConsumible.ToString("#,##0.00");

                            dondeVa = "Variable unidades días";
                            totalUnidadesDias = Convert.ToInt32(fila[dtMovimientos_unidadesDias].ToString());
                            dondeVa = "Variable unidades días no tax";
                            if (fila[dtMovimientos_unidadesDiasNoTax].ToString() != "")
                            {
                                dondeVa = "Variable unidades días no tax configurado";
                                totalUnidadesDiasNoTax = Convert.ToInt32(fila[dtMovimientos_unidadesDiasNoTax].ToString());
                            }
                            else
                            {
                                dondeVa = "Variable unidades días no tax configurado a cero";
                                totalUnidadesDiasNoTax = 0;
                            }

                            dondeVa = "IMPORTES TAX";
                            importeCosteTaximetroConsumible = importeCosteTaximetroConsumible + ((totalUnidadesDias * precioTaximetroConsumible * taximetroConsumible) / diasxMes);
                            oItemTaximetro[COL_DATA_IMPORTE_TAX] = ((totalUnidadesDias * precioTaximetroConsumible * taximetroConsumible) / diasxMes).ToString("#,##0.00");

                            importeCosteTaximetroNoConsumible = importeCosteTaximetroNoConsumible + ((totalUnidadesDiasNoTax * precioTaximetroNoConsumible * taximetroNoConsumible) / diasxMes);
                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((totalUnidadesDiasNoTax * precioTaximetroNoConsumible * taximetroNoConsumible) / diasxMes).ToString("#,##0.00");

                            dondeVa = "CALCULAR BONIFICACION";
                            oItemTaximetro[COL_DATA_AJUSTE] = "";
                            dondeVa = "CALCULAR BONIFICACION 1";
                            diasCalculados = 0;
                            diasMultiplicar = 0;
                            dondeVa = "CALCULAR BONIFICACION 2";
                            while (diasCalculados < duracion)
                            {
                                dondeVa = "CALCULAR BONIFICACION 3";
                                for (lineasBonificacion = 0; lineasBonificacion < listaBonificacionesDesde.Count; lineasBonificacion++)
                                {
                                    dondeVa = "CALCULAR BONIFICACION 4";
                                    if ((diasCalculados + diasxMes) >= listaBonificacionesDesde.ElementAt(lineasBonificacion) && (diasCalculados + diasxMes) <= listaBonificacionesHasta.ElementAt(lineasBonificacion))
                                    {
                                        dondeVa = "CALCULAR BONIFICACION 5";
                                        if (duracion - diasCalculados >= diasxMes)
                                            diasMultiplicar = diasxMes;
                                        else
                                            diasMultiplicar = duracion - diasCalculados;
                                        dondeVa = "CALCULAR BONIFICACION 6";
                                        bonificacionTaximetroConsumible = taximetroConsumible - (listaBonificacionesValor.ElementAt(lineasBonificacion) / 100);
                                        dondeVa = "CALCULAR BONIFICACION 7";
                                        oItemTaximetro[COL_DATA_AJUSTE] = oItemTaximetro[COL_DATA_AJUSTE] + "Bonif. (" + bonificacionTaximetroConsumible.ToString("#,##0.0000") + ") * Dias(" + diasMultiplicar.ToString() + ") // ";
                                        dondeVa = "CALCULAR BONIFICACION 8";
                                        if (oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE].ToString() == "")
                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = ((Convert.ToDecimal(fila[dtMovimientos_cantidadActual]) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes).ToString("#,##0.00");
                                        else
                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (Convert.ToDecimal(oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE]) + ((Convert.ToDecimal(fila[dtMovimientos_cantidadActual]) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes)).ToString("#,##0.00");
                                        dondeVa = "CALCULAR BONIFICACION 9";
                                        importeCosteBonificacionTaximetroConsumible = importeCosteBonificacionTaximetroConsumible + ((Convert.ToDecimal(fila[dtMovimientos_cantidadActual]) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                        dondeVa = "CALCULAR BONIFICACION 10";
                                        importeCosteBonificacionTaximetroConsumibleCapitulos = importeCosteBonificacionTaximetroConsumibleCapitulos + ((Convert.ToDecimal(fila[dtMovimientos_cantidadActual]) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                        dondeVa = "CALCULAR BONIFICACION 11";
                                    }
                                }
                                dondeVa = "CALCULAR BONIFICACION 12";
                                diasCalculados = diasCalculados + diasxMes;
                                dondeVa = "CALCULAR BONIFICACION 13";
                            }

                            dtTaximetro.Rows.Add(oItemTaximetro);
                            oItemTaximetro = null;
                        }
                    }
                }
                proxy_1.Close();

                // VALES TRANSPORTE
                System.Collections.IEnumerator enumerator_VT;
                // CABECERA
                dondeVa = "contexto_1";
                tablaValesTransporte.CallContext contexto_VT = new tablaValesTransporte.CallContext();
                contexto_VT.Company = cmbEmpresa.SelectedItem.ToString();

                dondeVa = "criterio";
                tablaValesTransporte.QueryCriteria criterio_VT = new tablaValesTransporte.QueryCriteria();
                criterio_VT.CriteriaElement = new tablaValesTransporte.CriteriaElement[1];

                //ProjTable_1
                criterio_VT.CriteriaElement[0] = new tablaValesTransporte.CriteriaElement();
                criterio_VT.CriteriaElement[0].DataSourceName = "TransportTicketTable";
                criterio_VT.CriteriaElement[0].FieldName = "obraid";
                criterio_VT.CriteriaElement[0].Operator = tablaValesTransporte.Operator.Equal;
                criterio_VT.CriteriaElement[0].Value1 = txtNumero.Text;

                dondeVa = "credenciales";
                tablaValesTransporte.TransportTicketServiceClient proxy_VT = new tablaValesTransporte.TransportTicketServiceClient();
                proxy_1.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_1.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_1.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                dondeVa = "proxy";
                tablaValesTransporte.AxdTransportTicket AxdTransportTicket = proxy_VT.find(contexto_VT, criterio_VT);
                datosGenerales = "";
                dondeVa = "validar";
                cantidadPorte = 0;
                importeCostePorteCapitulos = 0;
                importeCostePorte = 0;
                //if (AxdTransportTicket.TransportTicketTable.Equals(null))
                if (AxdTransportTicket.TransportTicketTable is null)
                {
                    proxy_VT.Close();
                }
                else
                {
                    dondeVa = "primer data";
                    enumerator_VT = AxdTransportTicket.TransportTicketTable.GetEnumerator();
                    while (enumerator_VT.MoveNext())
                    {
                        tablaValesTransporte.AxdEntity_TransportTicketTable AxdEntity_TransportTicketTable = (tablaValesTransporte.AxdEntity_TransportTicketTable)enumerator_VT.Current;

                        dondeVa = "RECORRER";
                        cantidadPorte = cantidadPorte + 1;
                        capitulosPorte.Add(AxdEntity_TransportTicketTable.TypeTransportTicket.ToString());
                        dec_importeFacturacionPorteCapitulos.Add(0);
                        dec_importeCostePorteCapitulos.Add(Convert.ToDecimal(AxdEntity_TransportTicketTable.UPS));
                        dec_importeMargenPorteCapitulos.Add(0- Convert.ToDecimal(AxdEntity_TransportTicketTable.UPS));
                        importeCostePorte = importeCostePorte + Convert.ToDecimal(AxdEntity_TransportTicketTable.UPS);
                    }
                }
                proxy_VT.Close();

                // PEDIDOS DE LIQUIDACIÓN DE LA OBRA
                dondeVa = "Pedidos";
                System.Collections.IEnumerator enumerator_Pedido;
                // CABECERA
                tablaPedidos.CallContext contexto_Pedido = new tablaPedidos.CallContext();
                contexto_Pedido.Company = cmbEmpresa.SelectedItem.ToString();

                tablaPedidos.QueryCriteria criterio_Pedido = new tablaPedidos.QueryCriteria();
                criterio_Pedido.CriteriaElement = new tablaPedidos.CriteriaElement[2];

                criterio_Pedido.CriteriaElement[0] = new tablaPedidos.CriteriaElement();
                criterio_Pedido.CriteriaElement[0].DataSourceName = "SalesTable";
                criterio_Pedido.CriteriaElement[0].FieldName = "OBRAID";
                criterio_Pedido.CriteriaElement[0].Operator = tablaPedidos.Operator.Equal;
                criterio_Pedido.CriteriaElement[0].Value1 = txtNumero.Text;
                criterio_Pedido.CriteriaElement[0].Value1 = txtNumero.Text;

                tablaPedidos.SalesOrdersServiceClient proxy_Pedido = new tablaPedidos.SalesOrdersServiceClient();
                proxy_Pedido.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                proxy_Pedido.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                proxy_Pedido.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                tablaPedidos.AxdSalesOrders axdtablaPedidos_Pedido = proxy_Pedido.find(contexto_Pedido, criterio_Pedido);
                if (axdtablaPedidos_Pedido.SalesTable.Equals(null))
                {
                    proxy_Pedido.Close();
                }
                else
                {
                    dondeVa = "Datos ws pedidos";
                    dtPedidos.Columns.Add("FECHA_CREACION");
                    dtPedidos.Columns.Add("AAF");
                    dtPedidos.Columns.Add("ITEM_ID");
                    dtPedidos.Columns.Add("SALESQTY");

                    dtAlbaranes.Columns.Add("FECHA");
                    dtAlbaranes.Columns.Add("AAF");
                    dtAlbaranes.Columns.Add("ItemId");
                    dtAlbaranes.Columns.Add("Qty");
                    dtAlbaranes.Columns.Add("Estado");

                    enumerator_Pedido = axdtablaPedidos_Pedido.SalesTable.GetEnumerator();

                    while (enumerator_Pedido.MoveNext())
                    {
                        tablaPedidos.AxdEntity_SalesTable axdEntity_SalesQuotationMasterTable = (tablaPedidos.AxdEntity_SalesTable)enumerator_Pedido.Current;
                        tablaPedidos.AxdEntity_SalesLine[] axdEntity_SalesQuotationTables = axdEntity_SalesQuotationMasterTable.SalesLine;
                        tablaPedidos.AxdEntity_SalesLine axdEntity_SalesQuotationTable;

                        if (axdEntity_SalesQuotationMasterTable.TipoVenta.ToString().ToUpper() == tablaPedidos.AxdEnum_TiposVentas.Liquidacion.ToString().ToUpper())
                        {
                            for (lineaCapitulo = 0; lineaCapitulo < axdEntity_SalesQuotationTables.Length; lineaCapitulo++)
                            {
                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[lineaCapitulo];
                                filaPedidos = dtPedidos.NewRow();
                                filaPedidos[dtPedidos_fechaCreacion] = Convert.ToString(axdEntity_SalesQuotationTable.CreatedDateTime.Value.ToString("yyyyMMdd"));
                                filaPedidos[dtPedidos_aaf] = Convert.ToString("");
                                filaPedidos[dtPedidos_itemID] = Convert.ToString(axdEntity_SalesQuotationTable.ItemId.ToString());
                                filaPedidos[dtPedidos_cantidad] = Convert.ToDecimal(axdEntity_SalesQuotationTable.SalesQty.ToString());
                                dtPedidos.Rows.Add(filaPedidos);
                                filaPedidos = null;

                                articulosLiquidacion = articulosLiquidacion + axdEntity_SalesQuotationTable.ItemId.ToString() + "|";
                            }
                        }
                        //datosSQL.datosConfigurados(cmbEmpresa.SelectedItem.ToString(), articulosLiquidacion, usuario);
                    }
                    proxy_Pedido.Close();

                    // SI HAY LIQUIDACIONES --> ALBARANES DE ENTREGA
                    dec_importeCosteMPOMixto = 0;
                    dec_importeCosteMPONuevo = 0;
                    dec_importeCosteMPOUsado = 0;

                    if (articulosLiquidacion != "")
                    {
                        // Buscar estado de los artículos 
                        dondeVa = "Datos estado artículos";

                        dtArticulosLiquidacion = localizarEstadoArticulos(usuario);
                        if (dtArticulosLiquidacion.Columns.Count == 0)
                            dondeVa ="va a petar // " + articulosLiquidacion + " // usuario // " + usuario;
                        // Actualizo AAF en el data de pedidos
                        //dondeVa = "Actualizar AAF en dtPedidos";
                        foreach (DataRow filaPedido in dtPedidos.Rows)
                        {
                            filaEncontrada = dtArticulosLiquidacion.Select("ItemId = '" + filaPedido[dtPedidos_itemID].ToString() + "'");
                            foreach (DataRow filaAAF in filaEncontrada)
                            {
                                filaPedido[dtPedidos_aaf] = Convert.ToString(filaAAF["AAF"]);
                                break;
                            }
                        }

                        // 40006251
                        dondeVa = "Datos ws albaranes de entrega";
                        System.Collections.IEnumerator enumerator_AlbaranEntrega;
                        // CABECERA
                        dondeVa = "contexto_AlbaranEntrega";
                        tablaAlbaranesEntrega.CallContext contexto_AlbaranEntrega = new tablaAlbaranesEntrega.CallContext();
                        contexto_AlbaranEntrega.Company = cmbEmpresa.SelectedItem.ToString();

                        dondeVa = "criterio";
                        tablaAlbaranesEntrega.QueryCriteria criterio_AlbaranEntrega = new tablaAlbaranesEntrega.QueryCriteria();
                        criterio_AlbaranEntrega.CriteriaElement = new tablaAlbaranesEntrega.CriteriaElement[1];

                        //ProjTable_AlbaranEntrega
                        criterio_AlbaranEntrega.CriteriaElement[0] = new tablaAlbaranesEntrega.CriteriaElement();
                        criterio_AlbaranEntrega.CriteriaElement[0].DataSourceName = "CustPackingSlipTrans";
                        criterio_AlbaranEntrega.CriteriaElement[0].FieldName = "ObraId";
                        criterio_AlbaranEntrega.CriteriaElement[0].Operator = tablaAlbaranesEntrega.Operator.Equal;
                        criterio_AlbaranEntrega.CriteriaElement[0].Value1 = txtNumero.Text;

                        dondeVa = "credenciales";
                        tablaAlbaranesEntrega.CustPackingServiceClient proxy_AlbaranEntrega = new tablaAlbaranesEntrega.CustPackingServiceClient();
                        proxy_AlbaranEntrega.ClientCredentials.Windows.ClientCredential.Domain = "ALSINA";
                        proxy_AlbaranEntrega.ClientCredentials.Windows.ClientCredential.UserName = "cromlec3";
                        proxy_AlbaranEntrega.ClientCredentials.Windows.ClientCredential.Password = "CroAls19";

                        dondeVa = "proxy";
                        tablaAlbaranesEntrega.AxdCustPacking AxdCustPacking = proxy_AlbaranEntrega.find(contexto_AlbaranEntrega, criterio_AlbaranEntrega);
                        datosGenerales = "";
                        dondeVa = "validar";
                        if (AxdCustPacking.CustPackingSlipTrans.Equals(null))
                        {
                            proxy_AlbaranEntrega.Close();
                        }
                        else
                        {
                            dondeVa = "recorrer albaranes";
                            enumerator_AlbaranEntrega = AxdCustPacking.CustPackingSlipTrans.GetEnumerator();

                            while (enumerator_AlbaranEntrega.MoveNext())
                            {
                                tablaAlbaranesEntrega.AxdEntity_CustPackingSlipTrans AxdEntity_CustPackingSlipTrans = (tablaAlbaranesEntrega.AxdEntity_CustPackingSlipTrans)enumerator_AlbaranEntrega.Current;
                                tipoArticulo = "";
                                filaEncontrada = dtArticulosLiquidacion.Select("ItemId = '" + AxdEntity_CustPackingSlipTrans.ItemId.ToString() + "'");
                                foreach (DataRow filaTipo in filaEncontrada)
                                {
                                    dondeVa = "ajustar datos AAF y tipo de artículo";
                                    tipoArticulo = Convert.ToString(filaTipo["TipoArticulo"]).ToUpper().Trim();
                                    AAF = Convert.ToString(filaTipo["AAF"]);
                                    //if (tipoArticulo.ToUpper().Trim() != "USADO")
                                    //    hayDistintoUsado = true;

                                    filaAlbaranes = dtAlbaranes.NewRow();
                                    filaAlbaranes[dtAlbaranes_fecha] = Convert.ToString(AxdEntity_CustPackingSlipTrans.DeliveryDate.ToString("yyyyMMdd"));
                                    filaAlbaranes[dtAlbaranes_aaf] = AAF;
                                    filaAlbaranes[dtAlbaranes_itemID] = Convert.ToString(AxdEntity_CustPackingSlipTrans.ItemId.ToString());
                                    filaAlbaranes[dtAlbaranes_cantidad] = Convert.ToDecimal(AxdEntity_CustPackingSlipTrans.Qty.ToString());
                                    filaAlbaranes[dtAlbaranes_estado] = tipoArticulo;
                                    dtAlbaranes.Rows.Add(filaAlbaranes);
                                    filaAlbaranes = null;
                                    break;
                                }
                            }
                        }
                        proxy_AlbaranEntrega.Close();

                        // APLASTAR DTPEDIDOS POR FECHA / AAF
                        // 40006251
                        dondeVa = "aplastar dtPedidos";
                        var dataPedidos = from d in dtPedidos.AsEnumerable()
                                          orderby d.Field<string>("FECHA_CREACION"), d.Field<string>("AAF")
                                          group d by new
                                          {
                                              FECHA_CREACION = d.Field<string>("FECHA_CREACION"),
                                              ITEM = d.Field<string>("ITEM_ID"),
                                              CODIGO_AAF = d.Field<string>("AAF"),
                                          } into grupo
                                          select new
                                          {
                                              fecha = grupo.Key.FECHA_CREACION,
                                              aaf = grupo.Key.CODIGO_AAF,
                                              articulo = grupo.Key.ITEM,
                                              cantidad = grupo.Sum(x => Convert.ToDecimal(x.Field<string>("SALESQTY")))
                                          };

                        //var data = from d in dtPedidos.AsEnumerable()
                        //           select d; 

                        dondeVa = "copiar a dtPedidosAgrupado";
                        System.Data.DataTable dtPedidosAgrupado = new System.Data.DataTable();

                        dondeVa = "creando columnas en dtPedidosAgrupado";
                        dtPedidosAgrupado.Columns.Add("FECHA");
                        dtPedidosAgrupado.Columns.Add("AAF");
                        dtPedidosAgrupado.Columns.Add("ARTICULO");
                        dtPedidosAgrupado.Columns.Add("CANTIDAD");
                        dtPedidosAgrupado.Columns.Add("USADO");
                        dtPedidosAgrupado.Columns.Add("MIXTO");
                        dtPedidosAgrupado.Columns.Add("NUEVO");

                        dondeVa = "copiando a dtPedidosAgrupado";
                        foreach (var itemPedidos in dataPedidos)
                            dtPedidosAgrupado.Rows.Add(itemPedidos.fecha.ToString(), itemPedidos.aaf.ToString(), itemPedidos.articulo.ToString(), itemPedidos.cantidad.ToString(), "0", "0", "0");

                        // APLASTAR DTALBARANES POR FECHA / AAF / TIPO
                        // 40006251
                        dondeVa = "aplastar dtAlbaranes";
                        var dataAlbaranes = from d in dtAlbaranes.AsEnumerable()
                                            orderby d.Field<string>("FECHA"), d.Field<string>("AAF"), d.Field<string>("Estado")
                                            group d by new
                                            {
                                                FECHA_ALBARAN = d.Field<string>("FECHA"),
                                                CODIGO_AAF = d.Field<string>("AAF"),
                                                ESTADO_ARTICULO = d.Field<string>("Estado"),
                                            } into grupo
                                            select new
                                            {
                                                fecha = grupo.Key.FECHA_ALBARAN,
                                                aaf = grupo.Key.CODIGO_AAF,
                                                estado = grupo.Key.ESTADO_ARTICULO,
                                                cantidad = grupo.Sum(x => Convert.ToDecimal(x.Field<string>("QTY")))
                                            };

                        dondeVa = "copiar a dtAlbaranesAgrupado";
                        System.Data.DataTable dtAlbaranesAgrupado = new System.Data.DataTable();

                        dondeVa = "creando columnas en dtAlbaranesAgrupado";
                        dtAlbaranesAgrupado.Columns.Add("FECHA");
                        dtAlbaranesAgrupado.Columns.Add("AAF");
                        dtAlbaranesAgrupado.Columns.Add("CANTIDAD");
                        dtAlbaranesAgrupado.Columns.Add("CANTIDAD_QUEDA");
                        dtAlbaranesAgrupado.Columns.Add("ESTADO");

                        dondeVa = "copiando a dtAlbaranesAgrupado";
                        foreach (var itemAlbaran in dataAlbaranes)
                            dtAlbaranesAgrupado.Rows.Add(itemAlbaran.fecha.ToString(), itemAlbaran.aaf.ToString() , itemAlbaran.cantidad.ToString(), itemAlbaran.cantidad.ToString(), itemAlbaran.estado.ToString());

                        // DISTRIBUIR DTPEDIDOS USADO / MIXTO / NUEVO
                        // 40006251
                        dondeVa = "distribuir usado / mixto / nuevo";
                        foreach (DataRow filaPedido in dtPedidosAgrupado.Rows)
                        {
                            faltan = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_cantidad]) - Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) - Convert.ToInt32(filaPedido[dtPedidosAgrupados_mixto]) - Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]);

                            // USADO
                            if (faltan > 0)
                            {
                                filaEncontrada = dtAlbaranesAgrupado.Select("AAF = '" + filaPedido[dtPedidosAgrupados_aaf].ToString() + "' AND FECHA <= '" + filaPedido[dtPedidosAgrupados_fechaCreacion].ToString() + "' AND ESTADO = 'USADO' AND CANTIDAD_QUEDA > '0'");
                                foreach (DataRow filaCantidad in filaEncontrada)
                                {
                                    if (faltan > Convert.ToDecimal(filaPedido[dtAlbaranesAgrupados_cantidadQueda]))
                                    {
                                        filaPedido[dtPedidosAgrupados_usado] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) + Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                        faltan = faltan - Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                        filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = "0";
                                    }
                                    if (faltan <= Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]))
                                    {
                                        filaPedido[dtPedidosAgrupados_usado] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) + faltan;
                                        filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]) - faltan;
                                        faltan = 0;
                                    }
                                    if (faltan == 0)
                                        break;
                                }
                            }

                            // MIXTO
                            if (faltan > 0)
                            {
                                filaEncontrada = dtAlbaranesAgrupado.Select("AAF = '" + filaPedido[dtPedidosAgrupados_aaf].ToString() + "' AND FECHA <= '" + filaPedido[dtPedidosAgrupados_fechaCreacion].ToString() + "' AND ESTADO = 'MIXTO' AND CANTIDAD_QUEDA > '0'");
                                foreach (DataRow filaCantidad in filaEncontrada)
                                {
                                    if (faltan > Convert.ToDecimal(filaPedido[dtAlbaranesAgrupados_cantidadQueda]))
                                    {
                                        filaPedido[dtPedidosAgrupados_mixto] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) + Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                        faltan = faltan - Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                        filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = "0";
                                    }
                                    if (faltan <= Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]))
                                    {
                                        filaPedido[dtPedidosAgrupados_mixto] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) + faltan;
                                        filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]) - faltan;
                                        faltan = 0;
                                    }
                                    if (faltan == 0)
                                        break;
                                }
                            }

                            // NUEVO
                            if (faltan > 0)
                            {
                                filaEncontrada = dtAlbaranesAgrupado.Select("AAF = '" + filaPedido[dtPedidosAgrupados_aaf].ToString() + "' AND FECHA <= '" + filaPedido[dtPedidosAgrupados_fechaCreacion].ToString() + "' AND ESTADO = 'NUEVO' AND CANTIDAD_QUEDA > '0'");
                                foreach (DataRow filaCantidad in filaEncontrada)
                                {
                                    if (faltan > Convert.ToDecimal(filaPedido[dtAlbaranesAgrupados_cantidadQueda]))
                                    {
                                        filaPedido[dtPedidosAgrupados_nuevo] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) + Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                        faltan = faltan - Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]);
                                        filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = "0";
                                    }
                                    if (faltan <= Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]))
                                    {
                                        filaPedido[dtPedidosAgrupados_nuevo] = Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) + faltan;
                                        filaCantidad[dtAlbaranesAgrupados_cantidadQueda] = Convert.ToDecimal(filaCantidad[dtAlbaranesAgrupados_cantidadQueda]) - faltan;
                                        faltan = 0;
                                    }
                                    if (faltan == 0)
                                        break;
                                }
                            }

                            // CALCULO filaPedido[dtPedidosAgrupados_usado] / filaPedido[dtPedidosAgrupados_mixto]  / filaPedido[dtPedidosAgrupados_nuevo]
                            dondeVa = "recuperar coeficiente";
                            filaEncontrada = dtDatosConfiguracion.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "' AND Concepto = 'Coef. Corrección PS Nuevo'");
                            foreach (DataRow filaTax in filaEncontrada)
                                coeficienteNuevo = Convert.ToDecimal(filaTax["Valor"]);
                            filaEncontrada = dtDatosConfiguracion.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "' AND Concepto = 'Coef. Corrección PS Usado'");
                            foreach (DataRow filaTax in filaEncontrada)
                                coeficienteUsado = Convert.ToDecimal(filaTax["Valor"]);
                            filaEncontrada = dtDatosConfiguracion.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "' AND Concepto = 'Coef. Corrección PS Mixto'");
                            foreach (DataRow filaTax in filaEncontrada)
                                coeficienteMixto= Convert.ToDecimal(filaTax["Valor"]);

                            precioCoste = 0;
                            dondeVa = "recuperar precio";
                            if (hayPrecio)
                            {
                                dondeVa = "buscar precio dataset // " + articulos;
                                filaEncontrada = dtArticulos.Select("Articulo = '" + filaPedido[dtPedidosAgrupados_articulo].ToString() + "'");
                                foreach (DataRow filaprecio in filaEncontrada)
                                {
                                    if (!String.IsNullOrEmpty(Convert.ToString(filaprecio["Precio"])))
                                        precioCoste = Convert.ToDecimal(filaprecio["Precio"]);
                                }
                            }

                            dondeVa = "calcular coste MPO";
                            importeCosteVenta = importeCosteVenta + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) * precioCoste * coeficienteUsado);
                            importeCosteVenta = importeCosteVenta + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) * precioCoste * coeficienteMixto);
                            importeCosteVenta = importeCosteVenta + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) * precioCoste * coeficienteNuevo);

                            dec_importeCosteMPOMixto = dec_importeCosteMPOMixto + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_mixto]) * precioCoste * coeficienteMixto);
                            dec_importeCosteMPONuevo = dec_importeCosteMPONuevo + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_nuevo]) * precioCoste * coeficienteNuevo);
                            dec_importeCosteMPOUsado = dec_importeCosteMPOUsado + (Convert.ToDecimal(filaPedido[dtPedidosAgrupados_usado]) * precioCoste * coeficienteUsado);
                        }
                    }
                }

                horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);

                datosGenerales = txtNumero.Text + " / " + nombreFicha + " / " + moneda + " / " + delegacion;

                nombreInforme = Server.MapPath("~/Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                SLDocument sl = new SLDocument();
                sl.ImportDataTable(1, 1, dtTaximetro, true);
                sl.SaveAs(nombreInforme);
                btnAbrirExcel.Visible = true;

                conexiones.crearConexion();
                conexiones.comando = conexiones.conexion.CreateCommand();
                conexiones.comando.CommandText = "ROP_DatosArticulosFamiliasSubfamiliasEliminar";
                conexiones.comando.CommandTimeout = 240000;
                conexiones.comando.CommandType = CommandType.StoredProcedure;
                conexiones.comando.Parameters.AddWithValue("@usuario", usuario);
                conexiones.comando.ExecuteNonQuery();
                conexiones.comando.Dispose();
                conexiones.conexion.Close();
                conexiones.conexion.Dispose();

                pintarDatos();
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Calcular ficha // " + dondeVa + " // " + ex.Message;
            }
        }

        protected void btnBuscarInformacion_Click(object sender, EventArgs e)
        {
            try
            {
                btnAbrirExcel.Visible = false;
                nombreInforme = "";
                lblMensajeError.Visible = false;
                lblMensajeError.Text = "";
                if ((cmbConcepto.SelectedItem.ToString() == "") || (cmbEmpresa.SelectedItem.ToString() == "") || (txtNumero.Text.Length == 0))
                {
                    lblMensajeError.Visible = true;
                    lblMensajeError.Text = "Debe indicar los datos que desea buscar: concepto - empresa - número";

                    txtNombreOferta.Text = "";
                    armarJson(true);
                    dataDatos.DataSource = null;
                    dataDatos.DataBind();
                    dataDatos.Columns.Clear();
                    dataDatos.Visible = false;

                    dataTiempos.DataSource = null;
                    dataTiempos.DataBind();
                    dataTiempos.Columns.Clear();
                    dataTiempos.Visible = false;
                }
                else
                {
                    if (cmbConcepto.SelectedItem.ToString() == "Oferta")
                        calcularOferta();
                    else if (cmbConcepto.SelectedItem.ToString() == "Pedido")
                        calcularPedido();
                    else if (cmbConcepto.SelectedItem.ToString() == "Ficha")
                        calcularFicha();
                }
            }
            catch (Exception ex)
            {
                //Session["mensajeError"] = ex.Message;
                //Response.Redirect("ErrorPage.aspx");
            }
        }

        private void armarJson(bool inicial)
        {
            try
            {
                if (inicial)
                {
                    porcentajeVenta = 89;
                    datosJson = baseDatosJson;
                    datosJson = datosJson.Replace("__importeFacturacion__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeFacturacion__", "0.00");
                    datosJson = datosJson.Replace("__importeAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeVenta__", "0.00");
                    datosJson = datosJson.Replace("__importeServicio__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeServicio__", "0.00");
                    datosJson = datosJson.Replace("__importeProducto__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeProducto__", "0.00");

                    datosJson = datosJson.Replace("__importePorte__", "0.00");
                    datosJson = datosJson.Replace("__porcentajePorte__", "0.00");

                    datosJson = datosJson.Replace("__HIJO_FACTURACION_ALQUILERES__", "");
                    datosJson = datosJson.Replace("__HIJO_FACTURACION_SERVICIOS__", "");
                    datosJson = datosJson.Replace("__HIJO_FACTURACION_PRODUCTOS__", "");
                    datosJson = datosJson.Replace("__HIJO_FACTURACION_PORTES__", "");

                    datosJson = datosJson.Replace("__importeCoste__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeCoste__", "0.00");

                    datosJson = datosJson.Replace("__importeTaximetros__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeTaximetros__", "0.00");
                    datosJson = datosJson.Replace("__importeTaximetroNoConsumible__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeTaximetroNoConsumible__", "0.00");
                    datosJson = datosJson.Replace("__importeTotalTaximetroConsumible__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeTotalTaximetroConsumible__", "0.00");
                    datosJson = datosJson.Replace("__HIJO_COSTE_TAXIMETRO_NO_CONSUMIBLE__", "");

                    datosJson = datosJson.Replace("__HIJO_COSTE_TAXIMETRO_CONSUMIBLE__", "");
                    datosJson = datosJson.Replace("__importeTaximetroConsumible__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeTaximetroConsumible__", "0.00");
                    datosJson = datosJson.Replace("__HIJO_COSTE_TAXIMETRO_BONIFICACION_CONSUMIBLE__", "");
                    datosJson = datosJson.Replace("__importeTaximetroBonificacionConsumible__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeTaximetroBonificacionConsumible__", "0.00");

                    datosJson = datosJson.Replace("__importeCosteAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeCosteAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeCosteVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeCosteVenta__", "0.00");
                    datosJson = datosJson.Replace("__importeCosteMPO__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeCosteMPO__", "0.00");
                    datosJson = datosJson.Replace("__importeCostePorte__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeCostePorte__", "0.00");

                    datosJson = datosJson.Replace("__HIJO_COSTE_ALQUILERES__", "");
                    datosJson = datosJson.Replace("__HIJO_COSTE_VENTAS__", "");
                    datosJson = datosJson.Replace("__HIJO_COSTE_MPO__", "");
                    datosJson = datosJson.Replace("__HIJO_COSTE_PORTES__", "");

                    datosJson = datosJson.Replace("__importeMargen__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeMargen__", "0.00");
                    datosJson = datosJson.Replace("__importeMargenAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeMargenAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeMargenVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeMargenVenta__", "0.00");
                    datosJson = datosJson.Replace("__importeMargenPorte__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeMargenPorte__", "0.00");

                    datosJson = datosJson.Replace("__HIJO_MARGEN_ALQUILERES__", "");
                    datosJson = datosJson.Replace("__HIJO_MARGEN_VENTAS__", "");
                    datosJson = datosJson.Replace("__HIJO_MARGEN_PORTES__", "");

                    datosJson = datosJson.Replace("__importeGastoVariable__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoVariable__", "0.00");
                    datosJson = datosJson.Replace("__importeGastoVariableAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoVariableAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeGastoVariableVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoVariableVenta__", "0.00");

                    datosJson = datosJson.Replace("__HIJO_GASTO_VARIABLE_ALQUILER__", "");
                    datosJson = datosJson.Replace("__HIJO_GASTO_VARIABLE_VENTA__", "");

                    datosJson = datosJson.Replace("__importeGastoFijo__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoFijo__", "0.00");

                    datosJson = datosJson.Replace("__importeGastoFijoBU__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoFijoBU__", "0.00");
                    datosJson = datosJson.Replace("__importeGastoFijoBUAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajGastoFijoBUAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeGastoFijoBUVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoFijoBUVenta__", "0.00");
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_BU_ALQUILER__", "");
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_BU_VENTA__", "");

                    datosJson = datosJson.Replace("__importeGastoFijoCentral__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoFijoCentral__", "0.00");
                    datosJson = datosJson.Replace("__importeGastoFijoCentralAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoFijoCentralAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeGastoFijoCentralVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeGastoFijoCentralVenta__", "0.00");
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_CENTRAL_ALQUILER__", "");
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_CENTRAL_VENTA__", "");

                    datosJson = datosJson.Replace("__importeROPBasico__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeROPBasico__", "0.00");
                    datosJson = datosJson.Replace("__importeROPBasicoAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeROPBasicoAlquiler__", "0.00");
                    datosJson = datosJson.Replace("__importeROPBasicoVenta__", "0.00");
                    datosJson = datosJson.Replace("__porcentajeROPBasicoVenta__", "0.00");

                    datosJson = datosJson.Replace("__HIJO_ROP_BASICO_ALQUILER__", "");
                    datosJson = datosJson.Replace("__HIJO_ROP_BASICO_VENTA__", "");
                }
                else
                {
                    datosJson = baseDatosJson;
                    datosJson = datosJson.Replace("__importeFacturacion__", importeFacturacion.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeFacturacion__", porcentajeFacturacion.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeAlquiler__", importeAlquiler.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeAlquiler__", porcentajeAlquiler.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeVenta__", importeVenta.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeVenta__", porcentajeVenta.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeServicio__", importeVentaServicio.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeServicio__", porcentajeVentaServicio.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeProducto__", importeVentaProducto.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeProducto__", porcentajeVentaProducto.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importePorte__", importePorte.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajePorte__", porcentajePorte.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__HIJO_FACTURACION_ALQUILERES__", strHijoFacturacionAlquiler);
                    datosJson = datosJson.Replace("__HIJO_FACTURACION_SERVICIOS__", strHijoFacturacionVentaServicio);
                    datosJson = datosJson.Replace("__HIJO_FACTURACION_PRODUCTOS__", strHijoFacturacionVentaProducto);
                    datosJson = datosJson.Replace("__HIJO_FACTURACION_PORTES__", strHijoFacturacionPorte);

                    datosJson = datosJson.Replace("__importeCoste__", importeCoste.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeCoste__", porcentajeCoste.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__importeTaximetros__", (importeCosteTaximetros).ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeTaximetros__", porcentajeCosteAlquiler.ToString("#,##0.00"));
                    
                    datosJson = datosJson.Replace("__importeTaximetroNoConsumible__", importeCosteTaximetroNoConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeTaximetroNoConsumible__", porcentajeCosteTaximetroNoConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__HIJO_COSTE_TAXIMETRO_NO_CONSUMIBLE__", strHijoCosteTaximetroNoConsumible);

                    datosJson = datosJson.Replace("__importeTotalTaximetroConsumible__", importeCosteTotalTaximetroConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeTotalTaximetroConsumible__", porcentajeCosteTotalTaximetroConsumible.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__importeTaximetroConsumible__", importeCosteTaximetroConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeTaximetroConsumible__", porcentajeCosteTaximetroConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__HIJO_COSTE_TAXIMETRO_CONSUMIBLE__", strHijoCosteTaximetroConsumible);

                    datosJson = datosJson.Replace("__importeTaximetroBonificacionConsumible__", importeCosteBonificacionTaximetroConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeTaximetroBonificacionConsumible__", porcentajeCosteBonificacionTaximetroConsumible.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__HIJO_COSTE_TAXIMETRO_BONIFICACION_CONSUMIBLE__", strHijoCosteBonificacionTaximetroConsumible);

                    datosJson = datosJson.Replace("__importeCosteVenta__", importeCosteVenta.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeCosteVenta__", porcentajeCosteVenta.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__importeCosteMPO__", importeCosteMPO.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeCosteMPO__", porcentajeCosteMPO.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__HIJO_COSTE_MPO__", strHijoCosteMPO);

                    datosJson = datosJson.Replace("__importeCostePorte__", importeCostePorte.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeCostePorte__", porcentajeCostePorte.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__HIJO_COSTE_ALQUILERES__", strHijoCosteAlquiler);
                    datosJson = datosJson.Replace("__HIJO_COSTE_VENTAS__", strHijoCosteVenta);
                    datosJson = datosJson.Replace("__HIJO_COSTE_MPO__", strHijoCosteMPO);
                    datosJson = datosJson.Replace("__HIJO_COSTE_PORTES__", strHijoCostePorte);

                    datosJson = datosJson.Replace("__importeMargen__", importeMargen.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeMargen__", porcentajeMargen.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeMargenAlquiler__", importeMargenAlquiler.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeMargenAlquiler__", porcentajeMargenAlquiler.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeMargenVenta__", importeMargenVenta.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeMargenVenta__", porcentajeMargenVenta.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeMargenPorte__", importeMargenPorte.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeMargenPorte__", porcentajeMargenPorte.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__HIJO_MARGEN_ALQUILERES__", strHijoMargenAlquiler);
                    datosJson = datosJson.Replace("__HIJO_MARGEN_VENTAS__", strHijoMargenVenta);
                    datosJson = datosJson.Replace("__HIJO_MARGEN_PORTES__", strHijoMargenPorte);

                    datosJson = datosJson.Replace("__importeGastoVariable__", importeGastosVariables.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoVariable__", porcentajeGastosVariables.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeGastoVariableAlquiler__", importeAlquilerGastosVariables.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoVariableAlquiler__", porcentajeAlquilerGastosVariables.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeGastoVariableVenta__", importeVentaGastosVariables.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoVariableVenta__", porcentajeVentaGastosVariables.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__HIJO_GASTO_VARIABLE_ALQUILER__", strHijoGastosVariablesAlquiler);
                    datosJson = datosJson.Replace("__HIJO_GASTO_VARIABLE_VENTA__", strHijoGastosVariablesVenta);

                    datosJson = datosJson.Replace("__importeGastoFijo__", importeGastosFijos.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoFijo__", porcentajeGastosFijos.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__importeGastoFijoBU__", importeGastosFijosBU.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoFijoBU__", porcentajeGastosFijosBU.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeGastoFijoBUAlquiler__", importeAlquilerGastosFijosBU.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajGastoFijoBUAlquiler__", porcentajeAlquilerGastosFijosBU.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeGastoFijoBUVenta__", importeVentaGastosFijosBU.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoFijoBUVenta__", porcentajeVentaGastosFijosBU.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_BU_ALQUILER__", strHijoGastosFijosBUAlquiler);
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_BU_VENTA__", strHijoGastosFijosBUVenta);

                    datosJson = datosJson.Replace("__importeGastoFijoCentral__", importeGastosFijosCentrales.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoFijoCentral__", porcentajeGastosFijosCentrales.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeGastoFijoCentralAlquiler__", importeAlquilerGastosFijosCentrales.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoFijoCentralAlquiler__", porcentajeAlquilerGastosFijosCentrales.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeGastoFijoCentralVenta__", importeVentaGastosFijosCentrales.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeGastoFijoCentralVenta__", porcentajeVentaGastosFijosCentrales.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_CENTRAL_ALQUILER__", strHijoGastosFijosCentralesAlquiler);
                    datosJson = datosJson.Replace("__HIJO_GASTO_FIJO_CENTRAL_VENTA__", strHijoGastosFijosCentralesVenta);

                    datosJson = datosJson.Replace("__importeROPBasico__", importeROPBasico.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeROPBasico__", porcentajeROPBasico.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeROPBasicoAlquiler__", importeROPBasicoAlquiler.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeROPBasicoAlquiler__", porcentajeROPBasicoAlquiler.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__importeROPBasicoVenta__", importeROPBasicoVenta.ToString("#,##0.00"));
                    datosJson = datosJson.Replace("__porcentajeROPBasicoVenta__", porcentajeROPBasicoVenta.ToString("#,##0.00"));

                    datosJson = datosJson.Replace("__HIJO_ROP_BASICO_ALQUILER__", strHijoROPBasicoAlquiler);
                    datosJson = datosJson.Replace("__HIJO_ROP_BASICO_VENTA__", strHijoROPBasicoVenta);
                }
            }
            catch (Exception ex)
            {
                string dondeVa = "";
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Armar Json // " + dondeVa + " // " + ex.Message;
            }
        }

        //protected void btnExportar_Click(object sender, EventArgs e)
        //{
        //    string dondeVa = "";
        //    try
        //    {
        //        //dondeVa = "copia del datatable";
        //        //dtTaximetroInforme = dtTaximetro.Copy();
        //        dondeVa = "nombre del doc";
        //        nombreInforme = Server.MapPath("~/Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
        //        dondeVa = "nuevo doc";
        //        SLDocument sl = new SLDocument();
                
        //        dondeVa ="importar tabla";
        //        //sl.ImportDataTable(1, 1, dtTaximetro, true);
        //        sl.ImportDataTable(1, 1, dtTaximetroInforme, true);

        //        dondeVa = "salvar tabla";
        //        sl.SaveAs(nombreInforme);
        //        btnAbrirExcel.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensajeError.Visible = true;
        //        lblMensajeError.Text = "ERROR exportando datatable a excel // " + va + " // " + nombreInforme + ex.Message;
        //    }
        //}

        protected void btnAbrirExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Context.Response.Redirect("Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                btnAbrirExcel.Visible = false;
            }
            catch (Exception ex)
            {
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "ERROR exportando datatable a excel // " + nombreInforme + ex.Message;
            }
        }
    }
}