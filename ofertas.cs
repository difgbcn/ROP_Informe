using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SpreadsheetLight;

namespace ROP_Informe
{
    public static class ofertas
    {
        public static void estructuraOfertas(ref string error, ref List<string> listaObras, ref DataTable dtCabeceraOferta, ref DataTable dtCapitulosOferta, ref DataTable dtLineasOferta, string empresa, List<string> listaNumeros)
        {
            DataTable dtTiempos = new DataTable();
            dtTiempos = (DataTable)Session["dtTiempos"];

            System.Collections.IEnumerator enumerator_Oferta;
            tablaOfertas.AxdEntity_SalesQuotationTable[] axdEntity_SalesQuotationTables;
            tablaOfertas.AxdEntity_SalesQuotationTable axdEntity_SalesQuotationTable;
            tablaOfertas.AxdEntity_SalesQuotationLine[] axdEntity_SalesQuotationLines;
            tablaOfertas.AxdEntity_SalesQuotationLine axdEntity_SalesQuotationLine;

            DataRow oOferta;
            string dondeVa = "";
            bool puedeConvertir;
            decimal numeroDecimal;
            string formaPago;
            string multivencimiento;
            int n = 0;
            string PeriodUnit = "";
            decimal QtyUnit = 0;
            decimal Qty = 0;
            decimal Valor = 0;

            int cantidad = -1;
            List<int> listaCantidades = new List<int>();

            try
            {
                tablaOfertas.CallContext contexto_Oferta = new tablaOfertas.CallContext();
                contexto_Oferta.Company = empresa;
                dondeVa = "tablaOfertas";
                tablaOfertas.QueryCriteria criterio_Oferta = new tablaOfertas.QueryCriteria();
                criterio_Oferta.CriteriaElement = new tablaOfertas.CriteriaElement[listaNumeros.Count];
                int numOferta = 0;
                foreach (string nOferta in listaNumeros)
                {
                    criterio_Oferta.CriteriaElement[numOferta] = new tablaOfertas.CriteriaElement();
                    criterio_Oferta.CriteriaElement[numOferta].FieldName = "QuotationId";
                    criterio_Oferta.CriteriaElement[numOferta].DataSourceName = "SalesQuotationMasterTable";
                    criterio_Oferta.CriteriaElement[numOferta].Operator = tablaOfertas.Operator.Equal;
                    criterio_Oferta.CriteriaElement[numOferta].Value1 = nOferta;
                    numOferta = numOferta + 1;
                }

                tablaOfertas.SalesQuotationsServiceClient proxy_Oferta = new tablaOfertas.SalesQuotationsServiceClient();
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                tablaOfertas.AxdSalesQuotations axdTablaOfertas_1 = proxy_Oferta.find(contexto_Oferta, criterio_Oferta);
                if (axdTablaOfertas_1.SalesQuotationMasterTable is null)
                {
                    proxy_Oferta.Close();
                }
                else
                {
                    enumerator_Oferta = axdTablaOfertas_1.SalesQuotationMasterTable.GetEnumerator();
                    proxy_Oferta.Close();
                    while (enumerator_Oferta.MoveNext())
                    {
                        tablaOfertas.AxdEntity_SalesQuotationMasterTable axdEntity_SalesQuotationMasterTable = (tablaOfertas.AxdEntity_SalesQuotationMasterTable)enumerator_Oferta.Current;
                        axdEntity_SalesQuotationTables = axdEntity_SalesQuotationMasterTable.SalesQuotationTable;
                        oOferta = dtCabeceraOferta.NewRow();
                        oOferta["OFERTA"] = axdEntity_SalesQuotationMasterTable.QuotationId.ToString();
                        oOferta["NOMBRE OFERTA"] = axdEntity_SalesQuotationMasterTable.QuotationName.ToString();
                        if (axdEntity_SalesQuotationMasterTable.CreatedDateTime is null)
                            oOferta["FECHA"] = "";
                        else
                            oOferta["FECHA"] = Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value.ToString());
                        if (axdEntity_SalesQuotationMasterTable.MasterObrasId is null)
                            oOferta["MASTER OBRA"] = "";
                        else
                            oOferta["MASTER OBRA"] = axdEntity_SalesQuotationMasterTable.MasterObrasId.ToString();
                        if (axdEntity_SalesQuotationMasterTable.ObraId is null)
                            oOferta["OBRA"] = "";
                        else
                        {
                            oOferta["OBRA"] = axdEntity_SalesQuotationMasterTable.ObraId.ToString();
                            if (!listaObras.Contains(axdEntity_SalesQuotationMasterTable.ObraId.ToString()))
                                listaObras.Add(axdEntity_SalesQuotationMasterTable.ObraId.ToString());
                        }
                        if (axdEntity_SalesQuotationMasterTable.CurrencyCode is null)
                            oOferta["MONEDA"] = "";
                        else
                            oOferta["MONEDA"] = axdEntity_SalesQuotationMasterTable.CurrencyCode.ToString();
                        if (axdEntity_SalesQuotationMasterTable.CustAccount is null)
                            oOferta["CUENTA CLIENTE"] = "";
                        else
                            oOferta["CUENTA CLIENTE"] = axdEntity_SalesQuotationMasterTable.CustAccount.ToString();
                        if (axdEntity_SalesQuotationMasterTable.CostCenter is null)
                            oOferta["CENTRO COSTE"] = "";
                        else
                            oOferta["CENTRO COSTE"] = axdEntity_SalesQuotationMasterTable.CostCenter.ToString();
                        if (axdEntity_SalesQuotationMasterTable.Delegation is null)
                            oOferta["DELEGACION"] = "";
                        else
                            oOferta["DELEGACION"] = axdEntity_SalesQuotationMasterTable.Delegation.ToString();
                        if (axdEntity_SalesQuotationMasterTable.SalesRental.ToString() is null)
                            oOferta["TIPO"] = "";
                        else
                            oOferta["TIPO"] = axdEntity_SalesQuotationMasterTable.SalesRental.ToString().ToUpper();
                        puedeConvertir = decimal.TryParse(axdEntity_SalesQuotationMasterTable.Kilometer.ToString(), out numeroDecimal);
                        if (puedeConvertir == true)
                            oOferta["KILOMETRO"] = Convert.ToDecimal(axdEntity_SalesQuotationMasterTable.Kilometer);
                        else
                            oOferta["KILOMETRO"] = 0;
                        if (axdEntity_SalesQuotationMasterTable.DlvMode is null)
                            oOferta["MODO ENTREGA"] = "";
                        else
                            oOferta["MODO ENTREGA"] = axdEntity_SalesQuotationMasterTable.DlvMode;
                        //puedeConvertir = Int32.TryParse(axdEntity_SalesQuotationMasterTable.GestionPorte.ToString(), out numeroEntero);
                        if (axdEntity_SalesQuotationMasterTable.GestionPorte.HasValue)
                            oOferta["GESTION PORTE"] = Convert.ToInt32(axdEntity_SalesQuotationMasterTable.GestionPorte.GetHashCode());
                        else
                            oOferta["GESTION PORTE"] = 0;
                        if (axdEntity_SalesQuotationMasterTable.TipoPorte is null)
                            oOferta["TIPO PORTE"] = "";
                        else
                            oOferta["TIPO PORTE"] = Convert.ToInt32(axdEntity_SalesQuotationMasterTable.TipoPorte.GetHashCode());
                        if (axdEntity_SalesQuotationMasterTable.Payment is null)
                        {
                            oOferta["FORMA PAGO"] = "";
                            formaPago = "";
                        }
                        else
                        {
                            oOferta["FORMA PAGO"] = axdEntity_SalesQuotationMasterTable.Payment.ToString();
                            formaPago = axdEntity_SalesQuotationMasterTable.Payment.ToString();
                        }
                        if (axdEntity_SalesQuotationMasterTable.PaymSched is null)
                        {
                            oOferta["MULTIVENCIMIENTO"] = "";
                            multivencimiento = "";
                        }
                        else
                        {
                            oOferta["MULTIVENCIMIENTO"] = axdEntity_SalesQuotationMasterTable.PaymSched.ToString();
                            multivencimiento = axdEntity_SalesQuotationMasterTable.PaymSched.ToString();
                        }

                        oOferta["NUMERO MESES"] = 0;
                        oOferta["NUMERO DIAS"] = 0;
                        oOferta["DIAS ADD"] = 0;
                        oOferta["DIAS PAGO"] = 0;

                        // PAGOS
                        dondeVa = "tablaPagos";
                        System.Collections.IEnumerator enumeratorPagos;
                        tablaPagos.CallContext contextoPagos = new tablaPagos.CallContext();
                        contextoPagos.Company = empresa;

                        tablaPagos.QueryCriteria criterioPagos = new tablaPagos.QueryCriteria();
                        criterioPagos.CriteriaElement = new tablaPagos.CriteriaElement[1];

                        criterioPagos.CriteriaElement[0] = new tablaPagos.CriteriaElement();
                        criterioPagos.CriteriaElement[0].FieldName = "PaymTermId";
                        criterioPagos.CriteriaElement[0].DataSourceName = "PaymTerm";
                        criterioPagos.CriteriaElement[0].Operator = tablaPagos.Operator.Equal;
                        criterioPagos.CriteriaElement[0].Value1 = formaPago;

                        tablaPagos.PagosServiceClient proxyPagos = new tablaPagos.PagosServiceClient();
                        proxyPagos.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                        proxyPagos.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                        proxyPagos.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                        tablaPagos.AxdPagos AxdPagos = proxyPagos.find(contextoPagos, criterioPagos);
                        if (AxdPagos.PaymTerm is null)
                        {
                            proxyPagos.Close();
                        }
                        else
                        {
                            enumeratorPagos = AxdPagos.PaymTerm.GetEnumerator();
                            while (enumeratorPagos.MoveNext())
                            {
                                tablaPagos.AxdEntity_PaymTerm AxdEntity_PaymTerm = (tablaPagos.AxdEntity_PaymTerm)enumeratorPagos.Current;
                                if (AxdEntity_PaymTerm.NumOfMonths is null)
                                    oOferta["NUMERO MESES"] = 0;
                                else
                                {
                                    oOferta["NUMERO MESES"] = Convert.ToInt32(AxdEntity_PaymTerm.NumOfMonths);
                                    oOferta["DIAS PAGO"] = Convert.ToInt32(AxdEntity_PaymTerm.NumOfMonths) * 30;
                                }
                                if (AxdEntity_PaymTerm.NumOfDays is null)
                                    oOferta["NUMERO DIAS"] = 0;
                                else
                                {
                                    oOferta["NUMERO DIAS"] = Convert.ToInt32(AxdEntity_PaymTerm.NumOfDays);
                                    oOferta["DIAS PAGO"] = Convert.ToInt32(oOferta["DIAS PAGO"]) + Convert.ToInt32(AxdEntity_PaymTerm.NumOfDays);
                                }
                                oOferta["DIAS ADD"] = 0;
                                //if (AxdEntity_PaymTerm.PaymMethod is null)
                                //    oOferta["DIAS ADD"] = 0;
                                //else
                                //{
                                //    if (AxdEntity_PaymTerm.PaymMethod.ToString().ToUpper() == "COD")
                                //        oOferta["DIAS ADD"] = 0;
                                //    else if (AxdEntity_PaymTerm.PaymMethod.ToString().ToUpper() == "NET")
                                //        oOferta["DIAS ADD"] = 0;
                                //    else if (AxdEntity_PaymTerm.PaymMethod.ToString().ToUpper() == "CURRENTMTH")
                                //    {
                                //        DateTime fecha = Convert.ToDateTime(axdEntity_SalesQuotationMasterTable.CreatedDateTime.Value.ToString());
                                //        DateTime fechaFinMes = new DateTime(fecha.Year, fecha.Month, 1).AddMonths(1).AddDays(-1);
                                //        oOferta["DIAS ADD"] = fechaFinMes.Subtract(fecha).Days;
                                //    }
                                //    else
                                //        oOferta["DIAS ADD"] = 0;
                                //}
                            }
                        }

                        // MULTIVENCIMIENTOS CABECERA
                        dondeVa = "tablaVencimientosCabecera";
                        oOferta["PERIOD UNIT"] = "";
                        oOferta["QTY UNIT"] = 0;
                        oOferta["QTY"] = 0;
                        oOferta["DIAS TOTAL"] = 0;
                        oOferta["VALUE"] = 0;
                        System.Collections.IEnumerator enumeratorVencimientoCabecera;
                        tablaVencimientosCabecera.CallContext contextoVencimientoCabecera = new tablaVencimientosCabecera.CallContext();
                        contextoVencimientoCabecera.Company = empresa;

                        tablaVencimientosCabecera.QueryCriteria criterioVencimientoCabecera = new tablaVencimientosCabecera.QueryCriteria();
                        criterioVencimientoCabecera.CriteriaElement = new tablaVencimientosCabecera.CriteriaElement[1];

                        criterioVencimientoCabecera.CriteriaElement[0] = new tablaVencimientosCabecera.CriteriaElement();
                        criterioVencimientoCabecera.CriteriaElement[0].FieldName = "Name";
                        criterioVencimientoCabecera.CriteriaElement[0].DataSourceName = "PaymSched";
                        criterioVencimientoCabecera.CriteriaElement[0].Operator = tablaVencimientosCabecera.Operator.Equal;
                        criterioVencimientoCabecera.CriteriaElement[0].Value1 = multivencimiento;

                        tablaVencimientosCabecera.VencimientosCabeceraServiceClient proxyVencimientoCabecera = new tablaVencimientosCabecera.VencimientosCabeceraServiceClient();
                        proxyVencimientoCabecera.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                        proxyVencimientoCabecera.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                        proxyVencimientoCabecera.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                        tablaVencimientosCabecera.AxdVencimientosCabecera AxdVencimientosCabecera = proxyVencimientoCabecera.find(contextoVencimientoCabecera, criterioVencimientoCabecera);
                        if (AxdVencimientosCabecera.PaymSched is null)
                        {
                            proxyVencimientoCabecera.Close();
                        }
                        else
                        {
                            enumeratorVencimientoCabecera = AxdVencimientosCabecera.PaymSched.GetEnumerator();
                            while (enumeratorVencimientoCabecera.MoveNext())
                            {
                                tablaVencimientosCabecera.AxdEntity_PaymSched AxdEntity_PaymSched = (tablaVencimientosCabecera.AxdEntity_PaymSched)enumeratorVencimientoCabecera.Current;

                                if (AxdEntity_PaymSched.NumOfPayment is null)
                                    n = 0;
                                else
                                    n = Convert.ToInt32(AxdEntity_PaymSched.NumOfPayment.Value);

                                if (AxdEntity_PaymSched.PeriodUnit is null)
                                    oOferta["PERIOD UNIT"] = "";
                                else
                                {
                                    oOferta["PERIOD UNIT"] = AxdEntity_PaymSched.PeriodUnit.ToString().ToUpper();
                                    PeriodUnit = AxdEntity_PaymSched.PeriodUnit.ToString().ToUpper();
                                }
                                if (AxdEntity_PaymSched.QtyUnit is null)
                                    oOferta["QTY UNIT"] = 0;
                                else
                                {
                                    oOferta["QTY UNIT"] = Convert.ToInt32(AxdEntity_PaymSched.QtyUnit.ToString());
                                    QtyUnit = Convert.ToInt32(AxdEntity_PaymSched.QtyUnit.ToString());
                                }

                                oOferta["DIAS TOTAL"] = 0;
                                if (AxdEntity_PaymSched.PaymBy.ToString().ToUpper() == "TOTAL")
                                    oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"]);
                                else if (AxdEntity_PaymSched.PaymBy.ToString().ToUpper() == "NUMOFPAYMENT" || AxdEntity_PaymSched.PaymBy.ToString().ToUpper() == "AMOUNTBYPAYMENT")
                                {
                                    for (int vencimiento = 0; vencimiento <= n - 1; vencimiento++)
                                    {
                                        if (PeriodUnit == "MONTH")
                                            oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + (((vencimiento * (QtyUnit * 30)) + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"])) / n);
                                        else if (PeriodUnit == "DAY")
                                            oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + (((vencimiento * QtyUnit) + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"])) / n);
                                        else
                                            oOferta["DIAS TOTAL"] = 0;
                                    }
                                }
                                else if (AxdEntity_PaymSched.PaymBy.ToString().ToUpper() == "SPECIFIED")
                                {
                                    // MULTIVENCIMIENTOS LÍNEAS
                                    dondeVa = "tablaVencimientosLineas";
                                    System.Collections.IEnumerator enumeratorVencimientoLineas;

                                    tablaVencimientosLineas.CallContext contextoVencimientoLineas = new tablaVencimientosLineas.CallContext();
                                    contextoVencimientoLineas.Company = empresa;

                                    tablaVencimientosLineas.QueryCriteria criterioVencimientoLineas = new tablaVencimientosLineas.QueryCriteria();
                                    criterioVencimientoLineas.CriteriaElement = new tablaVencimientosLineas.CriteriaElement[1];

                                    criterioVencimientoLineas.CriteriaElement[0] = new tablaVencimientosLineas.CriteriaElement();
                                    criterioVencimientoLineas.CriteriaElement[0].FieldName = "Name";
                                    criterioVencimientoLineas.CriteriaElement[0].DataSourceName = "PaymSchedLine";
                                    criterioVencimientoLineas.CriteriaElement[0].Operator = tablaVencimientosLineas.Operator.Equal;
                                    criterioVencimientoLineas.CriteriaElement[0].Value1 = multivencimiento;

                                    tablaVencimientosLineas.VencimientosLineasServiceClient proxyVencimientoLineas = new tablaVencimientosLineas.VencimientosLineasServiceClient();
                                    proxyVencimientoLineas.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                                    proxyVencimientoLineas.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                                    proxyVencimientoLineas.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                                    tablaVencimientosLineas.AxdVencimientosLineas AxdVencimientosLineas = proxyVencimientoLineas.find(contextoVencimientoLineas, criterioVencimientoLineas);
                                    if (AxdVencimientosLineas.PaymSchedLine is null)
                                    {
                                        proxyVencimientoLineas.Close();
                                    }
                                    else
                                    {
                                        enumeratorVencimientoLineas = AxdVencimientosLineas.PaymSchedLine.GetEnumerator();
                                        while (enumeratorVencimientoLineas.MoveNext())
                                        {
                                            tablaVencimientosLineas.AxdEntity_PaymSchedLine AxdEntity_PaymSchedLine = (tablaVencimientosLineas.AxdEntity_PaymSchedLine)enumeratorVencimientoLineas.Current;

                                            if (AxdEntity_PaymSchedLine.Qty is null)
                                                cantidad = 0;
                                            else
                                                cantidad = Convert.ToInt32(AxdEntity_PaymSchedLine.Qty.ToString());

                                            // valida si ya existe este modo de pago, en cuyo caso se ignora
                                            if (!listaCantidades.Contains(cantidad))
                                            {
                                                listaCantidades.Add(cantidad);
                                                if (AxdEntity_PaymSchedLine.Qty is null)
                                                {
                                                    oOferta["QTY"] = 0;
                                                    Qty = 0;
                                                }
                                                else
                                                {
                                                    oOferta["QTY"] = Convert.ToInt32(AxdEntity_PaymSchedLine.Qty.ToString());
                                                    Qty = Convert.ToInt32(AxdEntity_PaymSchedLine.Qty.ToString());
                                                }

                                                if (AxdEntity_PaymSchedLine.Value is null)
                                                {
                                                    oOferta["VALUE"] = 0;
                                                    Valor = 0;
                                                }
                                                else
                                                {
                                                    oOferta["VALUE"] = Convert.ToDecimal(AxdEntity_PaymSchedLine.Value.ToString());
                                                    Valor = Convert.ToDecimal(AxdEntity_PaymSchedLine.Value.ToString());
                                                }

                                                if (PeriodUnit == "MONTH")
                                                    oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + ((Valor * ((Qty * 30) + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"]))) / 100);
                                                else if (PeriodUnit == "DAY")
                                                    oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + ((Valor * (Qty + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"]))) / 100);
                                                else
                                                    oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]);
                                            }
                                        }
                                    }
                                    proxyVencimientoLineas.Close();
                                    criterioVencimientoLineas = null;
                                    enumeratorVencimientoLineas = null;
                                }
                            }
                        }
                        proxyVencimientoCabecera.Close();
                        criterioVencimientoCabecera = null;
                        contextoVencimientoCabecera = null;

                        dtCabeceraOferta.Rows.Add(oOferta);
                        oOferta = null;

                        if (!(axdEntity_SalesQuotationTables is null))
                        {
                            for (int capitulo = 0; capitulo < axdEntity_SalesQuotationTables.Length; capitulo++)
                            {
                                axdEntity_SalesQuotationTable = axdEntity_SalesQuotationTables[capitulo];
                                axdEntity_SalesQuotationLines = axdEntity_SalesQuotationTable.SalesQuotationLine;

                                oOferta = dtCapitulosOferta.NewRow();
                                oOferta["OFERTA"] = axdEntity_SalesQuotationMasterTable.QuotationId.ToString();
                                if (axdEntity_SalesQuotationTable.QuotationTitle is null)
                                    oOferta["CAPITULO"] = axdEntity_SalesQuotationTable.QuotationId.ToString().Replace("'", "");
                                else
                                    oOferta["CAPITULO"] = axdEntity_SalesQuotationTable.QuotationId.ToString().Replace("'", "") + " / " + axdEntity_SalesQuotationTable.QuotationTitle.ToString().Replace("'", "");
                                oOferta["FECHA"] = Convert.ToDateTime(axdEntity_SalesQuotationTable.CreatedDateTime.Value.ToString());
                                oOferta["TIPO"] = axdEntity_SalesQuotationTable.SalesRental.ToString().ToUpper();
                                dtCapitulosOferta.Rows.Add(oOferta);
                                oOferta = null;

                                if (axdEntity_SalesQuotationLines is null)
                                    oOferta = null;
                                else
                                {
                                    for (int lineaProducto = 0; lineaProducto < axdEntity_SalesQuotationLines.Length; lineaProducto++)
                                    {
                                        axdEntity_SalesQuotationLine = axdEntity_SalesQuotationLines[lineaProducto];
                                        oOferta = dtLineasOferta.NewRow();
                                        oOferta["OFERTA"] = axdEntity_SalesQuotationMasterTable.QuotationId.ToString();
                                        if (axdEntity_SalesQuotationTable.QuotationTitle is null)
                                            oOferta["CAPITULO"] = axdEntity_SalesQuotationTable.QuotationId.ToString().Replace("'", "");
                                        else
                                            oOferta["CAPITULO"] = axdEntity_SalesQuotationTable.QuotationId.ToString().Replace("'", "") + " / " + axdEntity_SalesQuotationTable.QuotationTitle.ToString().Replace("'", "");
                                        oOferta["ITEM"] = axdEntity_SalesQuotationLine.ItemId.ToString();
                                        puedeConvertir = decimal.TryParse(axdEntity_SalesQuotationLine.SalesQty.ToString(), out numeroDecimal);
                                        if (puedeConvertir == true)
                                            oOferta["CANTIDAD"] = axdEntity_SalesQuotationLine.SalesQty.ToString();
                                        else
                                            oOferta["CANTIDAD"] = 0;
                                        puedeConvertir = decimal.TryParse(axdEntity_SalesQuotationLine.LineAmount.ToString(), out numeroDecimal);
                                        if (puedeConvertir == true)
                                            oOferta["IMPORTE"] = axdEntity_SalesQuotationLine.LineAmount.ToString();
                                        else
                                            oOferta["IMPORTE"] = 0;
                                        puedeConvertir = decimal.TryParse(axdEntity_SalesQuotationLine.DuracionEstimada.ToString(), out numeroDecimal);
                                        if (puedeConvertir == true)
                                            oOferta["DURACION"] = Convert.ToDecimal(axdEntity_SalesQuotationLine.DuracionEstimada.ToString());
                                        else
                                            oOferta["DURACION"] = 0;
                                        oOferta["CANON FENOLICO"] = 0;
                                        oOferta["CANON IMPORTE"] = 0;
                                        oOferta["CANON"] = 0;
                                        if (axdEntity_SalesQuotationLine.CanonFenolico.HasValue && axdEntity_SalesQuotationLine.CanonFenolico != 0)
                                            oOferta["CANON FENOLICO"] = axdEntity_SalesQuotationLine.CanonFenolico;
                                        if (axdEntity_SalesQuotationLine.CanonImport.HasValue && axdEntity_SalesQuotationLine.CanonImport != 0)
                                            oOferta["CANON IMPORTE"] = Convert.ToDecimal(axdEntity_SalesQuotationLine.CanonImport);
                                        if (axdEntity_SalesQuotationLine.Canon.HasValue && axdEntity_SalesQuotationLine.Canon != 0)
                                            oOferta["CANON"] = Convert.ToDecimal(axdEntity_SalesQuotationLine.Canon);
                                        puedeConvertir = decimal.TryParse(axdEntity_SalesQuotationLine.EurDia.ToString(), out numeroDecimal);
                                        if (puedeConvertir == true)
                                            oOferta["EURO DIA"] = Convert.ToDecimal(axdEntity_SalesQuotationLine.EurDia);
                                        else
                                            oOferta["EURO DIA"] = 0;
                                        oOferta["PRECIO"] = Convert.ToDecimal(axdEntity_SalesQuotationLine.SalesPrice);

                                        dtLineasOferta.Rows.Add(oOferta);
                                        oOferta = null;
                                    }
                                }
                            }
                        }
                    }
                }
                error = "";
            }
            catch (Exception ex)
            {
                error = dondeVa + " // " + ex.Message;
                Informe.mostrarMensaje("estructuraOfertas // " + dondeVa + " // " + ex.Message);
            }
            finally
            {
                Session["dtTiempos"] = dtTiempos;
            }
        }

        public static void estructuraOfertasNuevo(ref string error, ref List<string> listaObras, ref DataTable dtCabeceraOferta, ref DataTable dtCapitulosOferta, ref DataTable dtLineasOferta, string empresa, List<string> listaNumeros)
        {
            DataTable dtTiempos = new DataTable();
            dtTiempos = (DataTable)Session["dtTiempos"];
            DataRow oItemTiempo;
            DataRow oOferta;
            string dondeVa = "";
            bool puedeConvertir;
            decimal numeroDecimal;
            string formaPago;
            string multivencimiento;
            int n = 0;
            string PeriodUnit = "";
            decimal QtyUnit = 0;
            decimal Qty = 0;
            decimal Valor = 0;
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            int cantidad = -1;
            List<int> listaCantidades = new List<int>();

            try
            {
                hora_1 = DateTime.Now;
                WSGeneral.CallContext contexto_Oferta = new WSGeneral.CallContext();
                contexto_Oferta.Company = empresa.ToString();

                WSGeneral.QueryCriteria criterio_Oferta = new WSGeneral.QueryCriteria();
                criterio_Oferta.CriteriaElement = new WSGeneral.CriteriaElement[listaNumeros.Count];
                int numOferta = 0;
                foreach (string nOferta in listaNumeros)
                {
                    criterio_Oferta.CriteriaElement[numOferta] = new WSGeneral.CriteriaElement();
                    criterio_Oferta.CriteriaElement[numOferta].FieldName = "QuotationId";
                    criterio_Oferta.CriteriaElement[numOferta].DataSourceName = "SalesQuotationMasterTable";
                    criterio_Oferta.CriteriaElement[numOferta].Operator = WSGeneral.Operator.Equal;
                    criterio_Oferta.CriteriaElement[numOferta].Value1 = nOferta;
                    numOferta = numOferta + 1;
                }

                WSGeneral.WSROPServiceClient proxy_Oferta = new WSGeneral.WSROPServiceClient();
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                WSGeneral.WSSalesQuotationMasterTableContract[] axdtablaOfertas = proxy_Oferta.getQuotations(contexto_Oferta, criterio_Oferta);

                IEnumerable<WSGeneral.WSSalesQuotationMasterTableContract> enumerableOferta = axdtablaOfertas;
                IEnumerator<WSGeneral.WSSalesQuotationMasterTableContract> enumeratorOferta = enumerableOferta.GetEnumerator();


                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al ws Ofertas con lista de ofertas (" + listaNumeros.Count.ToString() + ")";
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                dtTiempos.Rows.Add(oItemTiempo);

                while (enumeratorOferta.MoveNext())
                {
                    WSGeneral.WSSalesQuotationMasterTableContract enumerator_MasterOferta = enumeratorOferta.Current;

                    oOferta = dtCabeceraOferta.NewRow();
                    oOferta["OFERTA"] = enumerator_MasterOferta.parmQuotationId.ToString();
                    oOferta["NOMBRE OFERTA"] = enumerator_MasterOferta.parmQuotationName.ToString();
                    oOferta["FECHA"] = Convert.ToDateTime(enumerator_MasterOferta.parmCreatedDateTime.ToString());
                    if (enumerator_MasterOferta.parmMasterObrasId is null)
                        oOferta["MASTER OBRA"] = "";
                    else
                        oOferta["MASTER OBRA"] = enumerator_MasterOferta.parmMasterObrasId.ToString();
                    if (enumerator_MasterOferta.parmObraId is null)
                        oOferta["OBRA"] = "";
                    else
                    {
                        oOferta["OBRA"] = enumerator_MasterOferta.parmObraId.ToString();
                        if (!listaObras.Contains(enumerator_MasterOferta.parmObraId.ToString()))
                            listaObras.Add(enumerator_MasterOferta.parmObraId.ToString());
                    }
                    if (enumerator_MasterOferta.parmCurrencyCode is null)
                        oOferta["MONEDA"] = "";
                    else
                        oOferta["MONEDA"] = enumerator_MasterOferta.parmCurrencyCode.ToString();
                    if (enumerator_MasterOferta.parmCustAccount is null)
                        oOferta["CUENTA CLIENTE"] = "";
                    else
                        oOferta["CUENTA CLIENTE"] = enumerator_MasterOferta.parmCustAccount.ToString();
                    if (enumerator_MasterOferta.parmCostCenter is null)
                        oOferta["CENTRO COSTE"] = "";
                    else
                        oOferta["CENTRO COSTE"] = enumerator_MasterOferta.parmCostCenter.ToString();
                    if (enumerator_MasterOferta.parmDelegation is null)
                        oOferta["DELEGACION"] = "";
                    else
                        oOferta["DELEGACION"] = enumerator_MasterOferta.parmDelegation.ToString();
                    if (enumerator_MasterOferta.parmSalesRental.ToString() is null)
                        oOferta["TIPO"] = "";
                    else
                        oOferta["TIPO"] = enumerator_MasterOferta.parmSalesRental.ToString().ToUpper();
                    puedeConvertir = decimal.TryParse(enumerator_MasterOferta.parmKilometer.ToString(), out numeroDecimal);
                    if (puedeConvertir == true)
                        oOferta["KILOMETRO"] = Convert.ToDecimal(enumerator_MasterOferta.parmKilometer);
                    else
                        oOferta["KILOMETRO"] = 0;
                    if (enumerator_MasterOferta.parmDlvMode is null)
                        oOferta["MODO ENTREGA"] = "";
                    else
                        oOferta["MODO ENTREGA"] = enumerator_MasterOferta.parmDlvMode;
                    oOferta["GESTION PORTE"] = Convert.ToInt32(enumerator_MasterOferta.parmGestionPorte.GetHashCode());
                    oOferta["TIPO PORTE"] = Convert.ToInt32(enumerator_MasterOferta.parmTipoPorte.GetHashCode());
                    if (enumerator_MasterOferta.parmPayment is null)
                    {
                        oOferta["FORMA PAGO"] = "";
                        formaPago = "";
                    }
                    else
                    {
                        oOferta["FORMA PAGO"] = enumerator_MasterOferta.parmPayment.ToString();
                        formaPago = enumerator_MasterOferta.parmPayment.ToString();
                    }
                    if (enumerator_MasterOferta.parmPaymSched is null)
                    {
                        oOferta["MULTIVENCIMIENTO"] = "";
                        multivencimiento = "";
                    }
                    else
                    {
                        oOferta["MULTIVENCIMIENTO"] = enumerator_MasterOferta.parmPaymSched.ToString();
                        multivencimiento = enumerator_MasterOferta.parmPaymSched.ToString();
                    }

                    oOferta["NUMERO MESES"] = 0;
                    oOferta["NUMERO DIAS"] = 0;
                    oOferta["DIAS ADD"] = 0;
                    oOferta["DIAS PAGO"] = 0;

                    // PAGOS
                    hora_1 = DateTime.Now;
                    WSGeneral.CallContext contextoPagos = new WSGeneral.CallContext();
                    contextoPagos.Company = empresa;

                    WSGeneral.QueryCriteria criterioPagos = new WSGeneral.QueryCriteria();
                    criterioPagos.CriteriaElement = new WSGeneral.CriteriaElement[1];

                    criterioPagos.CriteriaElement[0] = new WSGeneral.CriteriaElement();
                    criterioPagos.CriteriaElement[0].DataSourceName = "PaymTermId";
                    criterioPagos.CriteriaElement[0].FieldName = "PaymTerm";
                    criterioPagos.CriteriaElement[0].Operator = WSGeneral.Operator.Equal;
                    criterioPagos.CriteriaElement[0].Value1 = formaPago;

                    WSGeneral.WSROPServiceClient proxyPagos = new WSGeneral.WSROPServiceClient();
                    proxyPagos.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                    proxyPagos.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                    proxyPagos.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                    WSGeneral.WSPaymTermContract[] axtablaPagos = proxyPagos.getPaymTerm(contextoPagos, criterioPagos);

                    IEnumerable<WSGeneral.WSPaymTermContract> enumerablePago = axtablaPagos;
                    IEnumerator<WSGeneral.WSPaymTermContract> enumeratorPago = enumerablePago.GetEnumerator();

                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al WSGeneral /  tablaPagos con PaytemId = " + formaPago;
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    while (enumeratorPago.MoveNext())
                    {
                        WSGeneral.WSPaymTermContract enumerator_Pago = enumeratorPago.Current;

                        oOferta["NUMERO MESES"] = Convert.ToInt32(enumerator_Pago.parmNumOfMonths);
                        oOferta["DIAS PAGO"] = Convert.ToInt32(enumerator_Pago.parmNumOfMonths) * 30;

                        oOferta["NUMERO DIAS"] = Convert.ToInt32(enumerator_Pago.parmNumOfDays);
                        oOferta["DIAS PAGO"] = Convert.ToInt32(oOferta["DIAS PAGO"]) + Convert.ToInt32(enumerator_Pago.parmNumOfDays);

                        oOferta["DIAS ADD"] = 0;
                    }
                    proxyPagos.Close();

                    // MULTIVENCIMIENTOS 
                    hora_1 = DateTime.Now;
                    WSGeneral.CallContext contextoVencimientoCabecera = new WSGeneral.CallContext();
                    contextoVencimientoCabecera.Company = empresa;

                    WSGeneral.QueryCriteria criterioVencimientoCabecera = new WSGeneral.QueryCriteria();
                    criterioVencimientoCabecera.CriteriaElement = new WSGeneral.CriteriaElement[1];

                    criterioVencimientoCabecera.CriteriaElement[0] = new WSGeneral.CriteriaElement();
                    criterioVencimientoCabecera.CriteriaElement[0].DataSourceName = "PaymSched";
                    criterioVencimientoCabecera.CriteriaElement[0].FieldName = "Name";
                    criterioVencimientoCabecera.CriteriaElement[0].Operator = WSGeneral.Operator.Equal;
                    criterioVencimientoCabecera.CriteriaElement[0].Value1 = multivencimiento;

                    WSGeneral.WSROPServiceClient proxyVencimientoCabecera = new WSGeneral.WSROPServiceClient();
                    proxyVencimientoCabecera.ClientCredentials.Windows.ClientCredential.Domain = Informe.WSDominio;
                    proxyVencimientoCabecera.ClientCredentials.Windows.ClientCredential.UserName = Informe.WSUsuario;
                    proxyVencimientoCabecera.ClientCredentials.Windows.ClientCredential.Password = Informe.WSContrasena;

                    WSGeneral.WSPaymSchedContract[] AxdVencimientosCabecera = proxyVencimientoCabecera.getPaymSched(contextoVencimientoCabecera, criterioVencimientoCabecera);

                    IEnumerable<WSGeneral.WSPaymSchedContract> enumerableVencimientoCabecera = AxdVencimientosCabecera;
                    IEnumerator<WSGeneral.WSPaymSchedContract> enumeratorVencimientoCabecera = enumerableVencimientoCabecera.GetEnumerator();

                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al ws WSGeneral / tablaVencimientosCabecera con PaymSchedName = " + multivencimiento;
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    while (enumeratorVencimientoCabecera.MoveNext())
                    {
                        WSGeneral.WSPaymSchedContract enumerator_VencimientoCabecera = enumeratorVencimientoCabecera.Current;

                        n = Convert.ToInt32(enumerator_VencimientoCabecera.parmNumOfPayment);

                        oOferta["PERIOD UNIT"] = enumerator_VencimientoCabecera.parmPeriodUnit.ToString().ToUpper();
                        PeriodUnit = enumerator_VencimientoCabecera.parmPeriodUnit.ToString().ToUpper();
                        oOferta["QTY UNIT"] = Convert.ToInt32(enumerator_VencimientoCabecera.parmQtyUnit.ToString());
                        QtyUnit = Convert.ToInt32(enumerator_VencimientoCabecera.parmQtyUnit.ToString());

                        oOferta["DIAS TOTAL"] = 0;
                        if (enumerator_VencimientoCabecera.parmPaymBy.ToString().ToUpper() == "TOTAL")
                            oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"]);
                        else if (enumerator_VencimientoCabecera.parmPaymBy.ToString().ToUpper() == "NUMOFPAYMENT" || enumerator_VencimientoCabecera.parmPaymBy.ToString().ToUpper() == "AMOUNTBYPAYMENT")
                        {
                            for (int vencimiento = 0; vencimiento <= n - 1; vencimiento++)
                            {
                                if (PeriodUnit == "MONTH")
                                    oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + (((vencimiento * (QtyUnit * 30)) + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"])) / n);
                                else if (PeriodUnit == "DAY")
                                    oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + (((vencimiento * QtyUnit) + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"])) / n);
                                else
                                    oOferta["DIAS TOTAL"] = 0;
                            }
                        }
                        else if (enumerator_VencimientoCabecera.parmPaymBy.ToString().ToUpper() == "SPECIFIED")
                        {
                            // MULTIVENCIMIENTOS LÍNEAS
                            IEnumerable<WSGeneral.WSPaymSchedLineContract> enumerableVencimientoLineas = enumerator_VencimientoCabecera.parmPaymSchedLines;
                            IEnumerator<WSGeneral.WSPaymSchedLineContract> enumeratorVencimientoLineas = enumerableVencimientoLineas.GetEnumerator();

                            while (enumeratorVencimientoLineas.MoveNext())
                            {
                                WSGeneral.WSPaymSchedLineContract enumerator_VencimientoLineas = enumeratorVencimientoLineas.Current;

                                cantidad = Convert.ToInt32(enumerator_VencimientoLineas.parmQty.ToString());

                                // valida si ya existe este modo de pago, en cuyo caso se ignora
                                if (!listaCantidades.Contains(cantidad))
                                {
                                    listaCantidades.Add(cantidad);

                                    oOferta["QTY"] = Convert.ToInt32(enumerator_VencimientoLineas.parmQty.ToString());
                                    Qty = Convert.ToInt32(enumerator_VencimientoLineas.parmQty.ToString());

                                    oOferta["VALUE"] = Convert.ToDecimal(enumerator_VencimientoLineas.parmValue.ToString());
                                    Valor = Convert.ToDecimal(enumerator_VencimientoLineas.parmValue.ToString());

                                    if (PeriodUnit == "MONTH")
                                        oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + ((Valor * ((Qty * 30) + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"]))) / 100);
                                    else if (PeriodUnit == "DAY")
                                        oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]) + ((Valor * (Qty + Convert.ToDecimal(oOferta["DIAS PAGO"]) + Convert.ToDecimal(oOferta["DIAS ADD"]))) / 100);
                                    else
                                        oOferta["DIAS TOTAL"] = Convert.ToDecimal(oOferta["DIAS TOTAL"]);
                                }
                            }

                        }
                    }
                    proxyVencimientoCabecera.Close();
                    criterioVencimientoCabecera = null;
                    contextoVencimientoCabecera = null;

                    dtCabeceraOferta.Rows.Add(oOferta);
                    oOferta = null;

                    IEnumerable<WSGeneral.WSSalesQuotationTableContract> enumerableCapitulos = enumerator_MasterOferta.parmQuotationTable;
                    IEnumerator<WSGeneral.WSSalesQuotationTableContract> enumeratorCapitulos = enumerableCapitulos.GetEnumerator();

                    dondeVa = "Líneas ofertas";
                    while (enumeratorCapitulos.MoveNext())
                    {
                        WSGeneral.WSSalesQuotationTableContract enumerator_Capitulos = enumeratorCapitulos.Current;

                        oOferta = dtCapitulosOferta.NewRow();
                        oOferta["OFERTA"] = enumerator_MasterOferta.parmQuotationId.ToString();
                        if (enumerator_Capitulos.parmQuotationTitle is null)
                            oOferta["CAPITULO"] = enumerator_Capitulos.parmQuotationId.ToString().Replace("'", "");
                        else
                            oOferta["CAPITULO"] = enumerator_Capitulos.parmQuotationId.ToString().Replace("'", "") + " / " + enumerator_Capitulos.parmQuotationTitle.ToString().Replace("'", "");
                        oOferta["FECHA"] = Convert.ToDateTime(enumerator_Capitulos.parmCreatedDateTime.ToString());
                        oOferta["TIPO"] = enumerator_Capitulos.parmSalesRental.ToString().ToUpper();
                        dtCapitulosOferta.Rows.Add(oOferta);
                        oOferta = null;

                        IEnumerable<WSGeneral.WSSalesQuotationLineContract> enumerableLineas = enumerator_Capitulos.parmQuotationLine;
                        IEnumerator<WSGeneral.WSSalesQuotationLineContract> enumeratorLineas = enumerableLineas.GetEnumerator();

                        dondeVa = "Líneas pedidos";
                        while (enumeratorLineas.MoveNext())
                        {
                            WSGeneral.WSSalesQuotationLineContract enumerator_OfertaLineas = enumeratorLineas.Current;

                            oOferta = dtLineasOferta.NewRow();
                            oOferta["OFERTA"] = enumerator_MasterOferta.parmQuotationId.ToString();
                            if (enumerator_Capitulos.parmQuotationTitle == "")
                                oOferta["CAPITULO"] = enumerator_Capitulos.parmQuotationId.ToString().Replace("'", "");
                            else
                                oOferta["CAPITULO"] = enumerator_Capitulos.parmQuotationId.ToString().Replace("'", "") + " / " + enumerator_Capitulos.parmQuotationTitle.ToString().Replace("'", "");
                            oOferta["ITEM"] = enumerator_OfertaLineas.parmItemId.ToString();
                            puedeConvertir = decimal.TryParse(enumerator_OfertaLineas.parmSalesQty.ToString(), out numeroDecimal);
                            if (puedeConvertir == true)
                                oOferta["CANTIDAD"] = enumerator_OfertaLineas.parmSalesQty.ToString();
                            else
                                oOferta["CANTIDAD"] = 0;
                            puedeConvertir = decimal.TryParse(enumerator_OfertaLineas.parmLineAmount.ToString(), out numeroDecimal);
                            if (puedeConvertir == true)
                                oOferta["IMPORTE"] = enumerator_OfertaLineas.parmLineAmount.ToString();
                            else
                                oOferta["IMPORTE"] = 0;
                            puedeConvertir = decimal.TryParse(enumerator_OfertaLineas.parmDuracionEstimada.ToString(), out numeroDecimal);
                            if (puedeConvertir == true)
                                oOferta["DURACION"] = Convert.ToDecimal(enumerator_OfertaLineas.parmDuracionEstimada.ToString());
                            else
                                oOferta["DURACION"] = 0;
                            oOferta["CANON FENOLICO"] = 0;
                            oOferta["CANON IMPORTE"] = 0;
                            oOferta["CANON"] = 0;
                            if (enumerator_OfertaLineas.parmCanonFenolico != 0)
                                oOferta["CANON FENOLICO"] = enumerator_OfertaLineas.parmCanonFenolico;
                            if (enumerator_OfertaLineas.parmCanonImport != 0)
                                oOferta["CANON IMPORTE"] = Convert.ToDecimal(enumerator_OfertaLineas.parmCanonImport);
                            if (enumerator_OfertaLineas.parmCanon != 0)
                                oOferta["CANON"] = Convert.ToDecimal(enumerator_OfertaLineas.parmCanon);
                            puedeConvertir = decimal.TryParse(enumerator_OfertaLineas.parmEurDia.ToString(), out numeroDecimal);
                            if (puedeConvertir == true)
                                oOferta["EURO DIA"] = Convert.ToDecimal(enumerator_OfertaLineas.parmEurDia);
                            else
                                oOferta["EURO DIA"] = 0;
                            oOferta["PRECIO"] = Convert.ToDecimal(enumerator_OfertaLineas.parmSalesPrice);

                            dtLineasOferta.Rows.Add(oOferta);
                            oOferta = null;
                        }
                    }
                }
                error = "";
            }
            catch (Exception ex)
            {
                error = dondeVa + " // " + ex.Message;
                Informe.mostrarMensaje("estructuraOfertas // " + dondeVa + " // " + ex.Message);
            }
            finally
            {
                Session["dtTiempos"] = dtTiempos;
            }
        }

        public static void calcularOferta(ref DataTable dtResultados, int cual, string empresa, List<string> listaNumeros, string version, bool calcularConPortes, bool calcularConFenolico)
        {
            string usuarioRed = Session["usuarioRed"].ToString();
            string idioma = Session["idioma"].ToString();
            DataTable dtTiempos = new DataTable();
            dtTiempos = (DataTable)Session["dtTiempos"];
            DataRow oItemTiempo;

            //string idioma = "";
            //HttpCookie nameCookie = Request.Cookies["idioma"];
            //if (nameCookie != null)
            //    idioma = nameCookie.Value.ToString();
            string usuario = "";
            List<string> listaObras = new List<string>();

            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            DateTime horaLlamada_1 = default(DateTime);
            TimeSpan horaLlamada_2 = default(TimeSpan);
            DateTime horaProcesar_1 = default(DateTime);
            TimeSpan horaProcesar_2 = default(TimeSpan);
            string dondeVa = "";
            string numeros = "";

            string numeroOferta = "";

            DataTable dtFechasBase = new DataTable();
            DataRow oFechaBase;
            dtFechasBase.Columns.Add("ARTICULO");
            dtFechasBase.Columns.Add("FECHA", typeof(DateTime));
            decimal importeAlquilerCapitulo = 0;
            decimal importeVentaCapitulo = 0;
            decimal dec_importeCosteVCH = 0;

            try
            {
                //DataTable dtTiempos = new DataTable();
                //DataRow oItemTiempo;
                //dtTiempos.Columns.Add("CONCEPTO");
                //dtTiempos.Columns.Add("TIEMPO");

                DataTable dtFenolicoNuevo = new DataTable();
                DataRow oItemFenolicoNuevo;
                dtFenolicoNuevo.Columns.Add("ITEM", typeof(string));
                dtFenolicoNuevo.Columns.Add("TIPO", typeof(string));
                dtFenolicoNuevo.Columns.Add("PS", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("CANTIDAD", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE PS", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE FENOLICO ESTANDAR", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE FENOLICO ESPECIAL", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("METROS", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE FENOLICO", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("% COSTE MATERIAL NUEVO", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE MATERIAL NUEVO", typeof(decimal));

                DataTable dtPedidoFechaBase = new DataTable();
                dtPedidoFechaBase.Columns.Add("PEDIDO", typeof(string));
                dtPedidoFechaBase.Columns.Add("FECHA PEDIDO", typeof(DateTime));
                dtPedidoFechaBase.Columns.Add("OFERTA", typeof(string));
                dtPedidoFechaBase.Columns.Add("SALES STATUS", typeof(string));
                dtPedidoFechaBase.Columns.Add("FECHA CAPITULO", typeof(DateTime));
                dtPedidoFechaBase.Columns.Add("ITEM", typeof(string));

                DataTable dtOfertasFechaBase = new DataTable();
                dtOfertasFechaBase.Columns.Add("OFERTA", typeof(string));
                dtOfertasFechaBase.Columns.Add("FECHA", typeof(DateTime));
                dtOfertasFechaBase.Columns.Add("OBRA", typeof(string));
                dtOfertasFechaBase.Columns.Add("PEDIDO", typeof(string));

                DataTable dtCabeceraOferta = new DataTable();
                dtCabeceraOferta.Columns.Add("OFERTA", typeof(string));
                dtCabeceraOferta.Columns.Add("NOMBRE OFERTA", typeof(string));
                dtCabeceraOferta.Columns.Add("FECHA", typeof(DateTime));
                dtCabeceraOferta.Columns.Add("MONEDA", typeof(string));
                dtCabeceraOferta.Columns.Add("CUENTA CLIENTE", typeof(string));
                dtCabeceraOferta.Columns.Add("OBRA", typeof(string));
                dtCabeceraOferta.Columns.Add("MASTER OBRA", typeof(string));
                dtCabeceraOferta.Columns.Add("CENTRO COSTE", typeof(string));
                dtCabeceraOferta.Columns.Add("DELEGACION", typeof(string));
                dtCabeceraOferta.Columns.Add("TIPO", typeof(string));
                dtCabeceraOferta.Columns.Add("KILOMETRO", typeof(decimal));
                dtCabeceraOferta.Columns.Add("MODO ENTREGA", typeof(string));
                dtCabeceraOferta.Columns.Add("GESTION PORTE", typeof(int));
                dtCabeceraOferta.Columns.Add("TIPO PORTE", typeof(int));
                dtCabeceraOferta.Columns.Add("FORMA PAGO", typeof(string));
                dtCabeceraOferta.Columns.Add("NUMERO MESES", typeof(int));
                dtCabeceraOferta.Columns.Add("NUMERO DIAS", typeof(int));
                dtCabeceraOferta.Columns.Add("MULTIVENCIMIENTO", typeof(string));
                dtCabeceraOferta.Columns.Add("DIAS PAGO", typeof(decimal));
                dtCabeceraOferta.Columns.Add("DIAS ADD", typeof(decimal));
                dtCabeceraOferta.Columns.Add("DIAS TOTAL", typeof(decimal));
                dtCabeceraOferta.Columns.Add("PERIOD UNIT", typeof(string));
                dtCabeceraOferta.Columns.Add("QTY UNIT", typeof(int));
                dtCabeceraOferta.Columns.Add("QTY", typeof(int));
                dtCabeceraOferta.Columns.Add("VALUE", typeof(decimal));

                DataTable dtCapitulosOferta = new DataTable();
                dtCapitulosOferta.Columns.Add("OFERTA", typeof(string));
                dtCapitulosOferta.Columns.Add("CAPITULO", typeof(string));
                dtCapitulosOferta.Columns.Add("FECHA", typeof(DateTime));
                dtCapitulosOferta.Columns.Add("TIPO", typeof(string));

                DataTable dtLineasOferta = new DataTable();
                dtLineasOferta.Columns.Add("OFERTA", typeof(string));
                dtLineasOferta.Columns.Add("CAPITULO", typeof(string));
                dtLineasOferta.Columns.Add("ITEM", typeof(string));
                dtLineasOferta.Columns.Add("CANTIDAD", typeof(decimal));
                dtLineasOferta.Columns.Add("IMPORTE", typeof(decimal));
                dtLineasOferta.Columns.Add("DURACION", typeof(decimal));
                dtLineasOferta.Columns.Add("CANON FENOLICO", typeof(decimal));
                dtLineasOferta.Columns.Add("CANON IMPORTE", typeof(decimal));
                dtLineasOferta.Columns.Add("CANON", typeof(decimal));
                dtLineasOferta.Columns.Add("EURO DIA", typeof(decimal));
                dtLineasOferta.Columns.Add("PRECIO", typeof(decimal));

                DataRow[] filasCapitulo;
                DataRow[] filasLinea;
                DataRow[] filasOcupacion;

                // GASTOS FIJOS
                decimal bonificacionBU = 0;
                decimal coeficienteGastosFijosAlquilerVenta = 0;
                DataTable dtDatosGastosFijos = new DataTable();
                DataTable dtGastosFijos = new DataTable();
                DataRow oItemGastosFijos;
                dtGastosFijos.Columns.Add("fact_anual_empresa", typeof(Decimal));
                dtGastosFijos.Columns.Add("facturacion_alquiler", typeof(Decimal));
                dtGastosFijos.Columns.Add("facturacion_venta", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_admin", typeof(Decimal));
                dtGastosFijos.Columns.Add("var_admin", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_comerc", typeof(Decimal));
                dtGastosFijos.Columns.Add("var_comerc", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_gf_bu", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_gf_hq", typeof(Decimal));
                dtGastosFijos.Columns.Add("KVBU", typeof(Decimal));
                dtGastosFijos.Columns.Add("KABU", typeof(Decimal));
                dtGastosFijos.Columns.Add("KVHQ", typeof(Decimal));
                dtGastosFijos.Columns.Add("KAHQ", typeof(Decimal));
                dtGastosFijos.Columns.Add("ITEM");
                //dtGastosFijos.Columns.Add("TIPO");
                //dtGastosFijos.Columns.Add("IMPORTE TOTAL");
                //dtGastosFijos.Columns.Add("IMPORTE", typeof(Decimal));
                dtGastosFijos.Columns.Add("FACTURACION ALQUILER", typeof(Decimal));
                dtGastosFijos.Columns.Add("FACTURACION VENTA", typeof(Decimal));
                dtGastosFijos.Columns.Add("COSTE ALQUILER", typeof(Decimal));
                dtGastosFijos.Columns.Add("COSTE VENTA", typeof(Decimal));
                //dtGastosFijos.Columns.Add("BU ALQUILER", typeof(Decimal));
                //dtGastosFijos.Columns.Add("BU VENTA", typeof(Decimal));
                //dtGastosFijos.Columns.Add("CENTRAL ALQUILER", typeof(Decimal));
                //dtGastosFijos.Columns.Add("CENTRAL VENTA", typeof(Decimal));
                dtGastosFijos.Columns.Add("TOTAL BU", typeof(Decimal));
                dtGastosFijos.Columns.Add("TOTAL CENTRAL", typeof(Decimal));
                dtGastosFijos.Columns.Add("BONIFICACION BU", typeof(Decimal));
                dtGastosFijos.Columns.Add("BONIFICACION BU CENTRAL", typeof(Decimal));

                decimal fact_anual_empresa = 0;
                decimal facturacion_alquiler = 0;
                decimal facturacion_venta = 0;
                decimal total_admin = 0;
                decimal var_admin = 0;
                decimal total_comerc = 0;
                decimal var_comerc = 0;
                decimal total_gf_bu = 0;
                decimal total_gf_hq = 0;
                decimal KVBU = 0;
                decimal KABU = 0;
                decimal KVHQ = 0;
                decimal KAHQ = 0;

                decimal totalGastosFijosBU = 0;
                decimal totalGastosFijosCentral = 0;

                // GASTOS FINANCIACION
                decimal financiacionBU = 0;
                decimal condicionesBU = 0;
                decimal condicionesCentral = 0;
                //decimal condicionesAplicar = 0;

                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var seed = int.Parse(justNumbers.Substring(0, 4));
                var rnd = new Random(seed);
                //Random rnd = new Random(10);
                int numeroRandom;

                string codigosTransporte = "";
                DataRow filaValores;

                List<string> listaArticulosSinFicha = new List<string>();
                List<string> listaArticulosPedido = new List<string>();
                List<DateTime> listaFechasPedido = new List<DateTime>();
                List<string> listaOfertas = new List<string>();
                List<DateTime> listaFechasOferta = new List<DateTime>();

                hora_1 = DateTime.Now;

                string strConexion;
                strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();

                SqlConnection conexion;

                SqlDataAdapter adaptador;
                System.Data.DataTable dtTabla = new System.Data.DataTable();
                SqlCommand comando;

                DateTime horaTotal_1 = default(DateTime);
                TimeSpan horaTotal_2 = default(TimeSpan);

                string moneda = "";
                string masterObra = "";
                decimal total;

                DataTable dtArticulosPaneles;

                SqlDataAdapter adaptadorDatosConfiguracion;
                System.Data.DataTable dtDatosConfiguracion;

                List<int> listaBonificacionesDesde = new List<int>();
                List<int> listaBonificacionesHasta = new List<int>();
                List<decimal> listaBonificacionesValor = new List<decimal>();

                decimal pesoArticulo = 0;
                int vocacionChatarra = 0;
                decimal kilometros = 0;
                decimal cambio = 1;
                //bool hayTR001 = false;
                bool existeTR001 = false;
                int lineasBonificacion;
                int duracion = 0;
                int diasCalculados = 0;
                int diasMultiplicar = 0;
                int diasDesdeTaxNoConsumible = 0;
                int diasHastaTaxNoConsumible = 0;
                int diasTaximetroNoConsumibleCalcular = 0;
                decimal importeCosteBonificacionTaximetroConsumible = 0;

                string tipoServicio = "";
                string tituloCapitulo = "";
                string tituloCapituloAsterisco = "";

                System.Data.DataTable dtValores;

                //dataTiempos.DataSource = null;
                //dataTiempos.Columns.Clear();

                DataTable dtTaximetro;
                dtTaximetro = new DataTable();
                dtTaximetro.Columns.Add("FECHA DOC");
                dtTaximetro.Columns.Add("FECHA CAPITULO");
                dtTaximetro.Columns.Add("CAPITULO");
                dtTaximetro.Columns.Add("TIPO CAPITULO");
                dtTaximetro.Columns.Add("MOVIMIENTO");
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
                dtTaximetro.Columns.Add("PRECIO CAMBIO");
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
                dtTaximetro.Columns.Add("FECHA_CALCULADA");
                dtTaximetro.Columns.Add("ALQUILER ENTREGA");
                dtTaximetro.Columns.Add("ALQUILER DEVOLUCION");
                dtTaximetro.Columns.Add("ALQUILER ENTREGA NAVIERA");
                dtTaximetro.Columns.Add("ALQUILER DEVOLUCION NAVIERA");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR REPARAR DIRECTO");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR DIRECTO");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR INDIRECTO");
                dtTaximetro.Columns.Add("ALQUILER REPARAR DIRECTO");
                dtTaximetro.Columns.Add("ALQUILER REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("ALQUILER GASTOS PATIO");
                dtTaximetro.Columns.Add("VENTA ENTREGA");
                dtTaximetro.Columns.Add("VENTA DEVOLUCION");
                dtTaximetro.Columns.Add("VENTA ENTREGA NAVIERA");
                dtTaximetro.Columns.Add("VENTA DEVOLUCION NAVIERA");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR REPARAR DIRECTO");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR DIRECTO");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR INDIRECTO");
                dtTaximetro.Columns.Add("VENTA REPARAR DIRECTO");
                dtTaximetro.Columns.Add("VENTA REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("VENTA GASTOS PATIO");
                dtTaximetro.Columns.Add("PRECIO GASTOS PATIO");
                dtTaximetro.Columns.Add("FECHA PRECIO GASTOS PATIO");
                dtTaximetro.Columns.Add("% DURACION AJUSTE", typeof(decimal));
                dtTaximetro.Columns.Add("DURACION AJUSTE", typeof(decimal));
                dtTaximetro.Columns.Add("AJUSTE OBRA DURACION", typeof(decimal));
                dtTaximetro.Columns.Add("% OCUPACION AJUSTE", typeof(decimal));
                dtTaximetro.Columns.Add("AJUSTE PRODUCTO", typeof(decimal));
                dtTaximetro.Columns.Add("AJUSTE PRODUCTO SOBRE");

                // gastos variables oficina
                int mesPresupuestoValido = 0;
                decimal porcentajeGastoAdmministrativoVariable = 0;
                decimal porcentajeGastoComercialVariable = 0;
                decimal porcentajeMaximoTotalFacturacion = 0;
                System.Data.DataTable dtDatosGastosOficina = new System.Data.DataTable();
                System.Data.DataTable dtGastosOficina = new System.Data.DataTable();
                dtGastosOficina.Columns.Add("CAPITULO");
                dtGastosOficina.Columns.Add("ITEM");
                dtGastosOficina.Columns.Add("MES PRESUPUESTO VALIDO");
                dtGastosOficina.Columns.Add("PORCENTAJE GASTO ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("PORCENTAJE GASTO COMERCIAL");
                dtGastosOficina.Columns.Add("PORCENTAJE MAXIMO TOTAL FACTURACON");
                dtGastosOficina.Columns.Add("TOTAL ANUAL");
                dtGastosOficina.Columns.Add("TOTAL ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("VAR ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("GASTOS ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("TOTAL COMERCIAL");
                dtGastosOficina.Columns.Add("VAR COMERCIAL");
                dtGastosOficina.Columns.Add("GASTOS COMERCIAL");
                dtGastosOficina.Columns.Add("TOTAL FACTURACION");
                dtGastosOficina.Columns.Add("MAX FACTURACION");
                dtGastosOficina.Columns.Add("IMPORTE");
                dtGastosOficina.Columns.Add("COSTE DT");
                dtGastosOficina.Columns.Add("GASTOS DT");
                decimal totalAnual = 0;
                decimal totalAdministrativo = 0;
                decimal varAdministrativo = 0;
                decimal totalComercial = 0;
                decimal varComercial = 0;
                decimal costeDT = 0;
                decimal preciosEnCero = 0;

                // gastos variables patio
                decimal clasificarRepararDirecto;
                decimal clasificarRepararIndirecto;
                decimal clasificarDirecto;
                decimal clasificarIndirecto;
                decimal repararDirecto;
                decimal repararIndirecto;
                decimal entrega = 0;
                decimal devolucion = 0;
                decimal naviera = 0;
                decimal precioGastosPatio = 0;

                decimal pesoCapitulo = 0;
                decimal pesoTotal = 0;
                decimal coeficienteNuevo = 0;
                decimal coeficienteUsadoChatarra = 0;
                decimal coeficienteUsadoMPOLiquidacion = 0;
                decimal coeficienteUsadoVenta = 0;
                decimal coeficienteUsar = 0;
                //decimal coeficienteMixto = 0;
                string tipoArticulo = "";
                string tipoArticuloFenolico = "";
                decimal porcentajeCosteMaterialNuevo = 0;
                decimal costeFenolicoNuevoEstandar = 0;
                decimal costeFenolicoNuevoEspecial = 0;
                decimal canonFenolicoNuevoEstandar = 0;
                decimal canonFenolicoNuevoEspecial = 0;

                decimal importeCosteMaterialNuevo = 0;
                decimal importeCosteMaterialFenolico = 0;

                DateTime fechaPrecio = DateTime.Now;

                bool metersInvoicing = false;
                string productType = "";
                bool calcularPorSuperficie = false;
                decimal superficie = 0;
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
                bool gastosFijosPorFacturacion = true;
                int diasxMes = 0;
                int diasDiferencia = 0;
                int diasPagoDefecto = 0;
                int diasRestar = 0;
                DateTime fechaPrecios;
                bool hayPrecio = false;
                bool primeraVez = true;
                int lineaCapitulo;
                System.Data.DataTable dtArticulos;
                System.Data.DataTable dtGastosPatio;
                System.Data.DataTable dtOcupacion;
                DataRow[] filaEncontrada;
                DataRow[] filaEncontradaFenolico;
                string articulos = "";
                string articulosCambio = "";
                decimal precioCoste = 0;

                decimal importeGastosPatioEntregaDevolucionVentas = 0;
                decimal importeGastosPatioEntregaDevolucionAlquiler = 0;
                decimal importeGastosPatioClasificacionReparacionAlquiler = 0;

                decimal importeGastosVariablesEntregaDevolucionAlquilerCapitulos = 0;
                decimal importeGastosVariablesEntregaDevolucionVentaCapitulos = 0;
                decimal importeGastosVariablesClaisificacionReparacionAlquilerCapitulos = 0;
                decimal importeGastosVariablesClaisificacionReparacionVentaCapitulos = 0;

                decimal importeFacturacionAlquilerCapitulos = 0;
                decimal importeCosteTaximetroNoConsumibleCapitulos = 0;
                decimal importeCosteTaximetroConsumibleCapitulos = 0;
                decimal importeCosteBonificacionTaximetroConsumibleCapitulos = 0;
                decimal importeFacturacionVentaCapitulos = 0;
                decimal importeFacturacionVentaDepartamentoTecnicoCapitulos = 0;
                decimal importeFacturacionVentaFenolicoNuevoCapitulos = 0;
                decimal importeFacturacionVentaUneCifCapitulos = 0;
                decimal importeFacturacionVentaMontajesCapitulos = 0;
                decimal importeFacturacionVentaProductoCapitulos = 0;
                decimal importeCosteVentaCapitulos = 0;
                decimal importeFacturacionVentaOtrosServiciosCapitulos = 0;
                decimal importeCosteVentaOtrosServiciosCapitulos = 0;

                decimal importeCosteVentaDepartamentoTecnicoCapitulos = 0;
                decimal importeCosteVentaFenolicoNuevoCapitulos = 0;
                decimal importeCosteVentaNuevoCapitulos = 0;
                decimal importeGastosVariablesVentaFenolicoNuevoCapitulos = 0;
                decimal importeGastosFijosBUVentaFenolicoNuevoCapitulos = 0;
                decimal importeGastosFijosCentralesVentaFenolicoNuevoCapitulos = 0;

                decimal importeCosteVentaUneCifCapitulos = 0;
                decimal importeGastosVariablesVentaUneCifCapitulos = 0;
                decimal importeGastosFijosBUVentaUneCifCapitulos = 0;
                decimal importeGastosFijosCentralesVentaUneCifCapitulos = 0;

                decimal importeCosteVentaMontajesCapitulos = 0;
                decimal importeGastosVariablesVentaMontajesCapitulos = 0;
                decimal importeGastosFijosBUVentaMontajesCapitulos = 0;
                decimal importeGastosFijosCentralesVentaMontajesCapitulos = 0;

                decimal importeGastosFinanciacionCapitulos = 0;

                decimal porcentajeCobrados = 0;
                decimal porcentajeFallidos = 0;
                decimal importeFallidosCapitulos = 0;

                datosGenerales = "";

                horaTotal_1 = DateTime.Now;
                fechaPrecios = new DateTime();

                // CABECERA
                decimal importeAlquiler = 0;
                decimal importeVenta = 0;
                decimal importePorte = 0;
                decimal importePorte_TR001 = 0;
                decimal importeCosteTaximetroNoConsumible = 0;
                decimal importeCosteTaximetroConsumible = 0;
                //decimal importeCosteVenta = 0;
                decimal importeCostePorte = 0;

                // AJUSTES
                string ajustesAlcance = "";
                decimal ajustesPocentajeCargoMaximo = 0;
                int ajustesValorObraCorta = 0;
                decimal ajustesPorcentajeCorreccionObraCorta = 0;
                int ajustesValorObraLarga = 0;
                decimal ajustesPorcentajeCorreccionObraLarga = 0;
                int ajustesValorMaximo = 0;
                decimal ajustesPorcentajeCorreccionMaximo = 0;
                decimal GF = 0;
                decimal BUimporteLinea = 0;
                decimal centralImporteLinea = 0;
                decimal importeVentaLinea = 0;
                decimal importeAlquilerLinea = 0;
                decimal porcentajeAjustesObraDuracionGFCapitulo = 0;
                DataTable dtAjustesDuracionGF = new DataTable();
                DataRow oAjustesDuracionGF;
                dtAjustesDuracionGF.Columns.Add("DURACION", typeof(int));
                dtAjustesDuracionGF.Columns.Add("AJUSTE", typeof(decimal));
                decimal costeAlquiler = 0;
                decimal costeVentas = 0;

                decimal importeAjustesObraDuracionGFCapitulo = 0;
                decimal ajustesTaxDuracionMinima = 0;
                decimal ajustesTaxDuracionMaxima = 0;
                decimal ajustesTaxPorcentajeAbonoMensual = 0;
                decimal duracionAjuste = 0;
                decimal importeAjustesObraDuracionTaxCapitulo = 0;

                decimal importeAjustesProductoTaxCapitulo = 0;
                decimal ocupacionAjuste = 0;

                //string ajusteAlcanceOcupacion = "";
                //int ajustePeriodoOcupacion = 0;
                //decimal ajusteValorOcupacionMuyBaja = 0;
                //decimal ajustePorcentajeAbonoMaximo = 0;
                //decimal ajusteValorOcupacionBaja = 0;
                //decimal ajustePorcentajeAbonoOcupacionBaja = 0;
                //decimal ajusteValorOcupacionMedia = 0;
                //decimal ajustePorcentajeAbonoOcupacionMedia = 0;
                //decimal ajusteValorOcupacionAlta = 0;
                //decimal ajustePorcentajeAbonoOcupacionAlta = 0;

                string ajusteAlcanceCliente = "";
                int ajusteValorClienteMediano = 0;
                decimal ajustePorcentajeCorreccionClienteMediano = 0;
                int ajusteValorClienteGrande = 0;
                decimal ajustePorcentajeCorreccionClienteGrande = 0;

                // AJUSTES BU
                DataTable dtAjustesBU = new DataTable();
                DataTable dtAjustesDatosBU = new DataTable();

                string cumplimientoBajoAfectaGFCentral = "";
                string cumplimientoBajoAfectaGFBU = "";
                string cumplimientoAltoAfectaGFCentral = "";
                string cumplimientoAltoAfectaGFBU = "";
                decimal cumplimientoBajoPorcentaje = 0;
                decimal bonificacionCumplimientoBajoPorcentaje = 0;
                decimal cumplimientoAltoPorcentaje = 0;
                decimal bonificacionCumplimientoAltoPorcentaje = 0;

                string error = "";
                horaProcesar_1 = DateTime.Now;

                numeroRandom = rnd.Next();
                numeroRandom = rnd.Next();
                //usuario = User.Identity.Name.ToString() + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();
                //if (User.Identity.Name.ToString() == "")
                //    usuario = "ROP" + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();

                usuario = usuarioRed + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();
                //if (Environment.UserName == "")
                //    usuario = "ROP" + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();

                horaLlamada_1 = DateTime.Now;

                hora_1 = DateTime.Now;
                estructuraOfertas(ref error, ref listaObras, ref dtCabeceraOferta, ref dtCapitulosOferta, ref dtLineasOferta, empresa, listaNumeros);
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO generar estructuras ofertas";
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                dtTiempos.Rows.Add(oItemTiempo);
                if (error != "")
                {
                    funcionalidades.guardarIncidencia(empresa, "OFERTA", "", listaNumeros.ElementAt(0), "Error al crear las estructuras de datos para la oferta", 1, usuario, User.Identity.Name.ToString());
                    Informe.mostrarMensaje("Error al crear las estructuras de datos para la oferta oferta // " + error);
                    return;
                }

                horaLlamada_2 = DateTime.Now.Subtract(horaLlamada_1);
                if (dtCabeceraOferta.Rows.Count == 0)
                {
                    funcionalidades.guardarIncidencia(empresa, "OFERTA", "", listaNumeros.ElementAt(0), "NO se ha encontrado la oferta indicada", 1, usuario, User.Identity.Name.ToString());
                    Informe.mostrarMensaje("NO se ha encontrado la oferta indicada");
                    return;
                }

                foreach (DataRow filaCabecera in dtCabeceraOferta.Rows)
                {
                    if ((bool)Session["datosTransporteOriginales"])
                    {
                        Session["datosTransporteOriginales"] = false;
                        chkBoxSoloEntrega.Checked = false;
                        chkBoxSoloDevolucion.Checked = false;
                        if (filaCabecera["DELEGACION"].ToString().ToUpper() != "EX" && Convert.ToInt32(filaCabecera["GESTION PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_GestionPorte.Alsina.GetHashCode()))
                        {
                            if (Convert.ToInt32(filaCabecera["TIPO PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_TipoPorte.Todo))
                            {
                                chkBoxSoloEntrega.Checked = true;
                                chkBoxSoloDevolucion.Checked = true;
                            }
                            else if (Convert.ToInt32(filaCabecera["TIPO PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_TipoPorte.SoloEntrega.GetHashCode()))
                                chkBoxSoloEntrega.Checked = true;
                            else if (Convert.ToInt32(filaCabecera["TIPO PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_TipoPorte.SoloDevolucion.GetHashCode()))
                                chkBoxSoloDevolucion.Checked = true;
                        }
                    }
                    calcularConPortes = (chkBoxSoloEntrega.Checked || chkBoxSoloDevolucion.Checked);

                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "OFERTA " + filaCabecera["OFERTA"].ToString();
                    dtTiempos.Rows.Add(oItemTiempo);

                    dtValores = new System.Data.DataTable();
                    dtValores.Columns.Add("ETIQUETA");
                    dtValores.Columns.Add("CONCEPTO");
                    dtValores.Columns.Add("IMPORTE");
                    dtValores.Columns.Add("PORCENTAJE");
                    dtValores.Columns.Add("IMPORTE PREVISTO");
                    dtValores.Columns.Add("PORCENTAJE PREVISTO");
                    dtValores.Columns.Add("COLOR");

                    dtArticulos = new System.Data.DataTable();
                    dtGastosPatio = new System.Data.DataTable();
                    dtOcupacion = new System.Data.DataTable();

                    dondeVa = "Oferta";
                    datosGenerales = filaCabecera["OFERTA"].ToString() + " / " + filaCabecera["CUENTA CLIENTE"].ToString() + " / " + filaCabecera["NOMBRE OFERTA"].ToString() + " / " + filaCabecera["MASTER OBRA"].ToString() + " // " + filaCabecera["CENTRO COSTE"].ToString() + " // " + filaCabecera["MONEDA"].ToString();

                    codigosTransporte = "";
                    funcionalidades.calcularCodigosTransporte(ref dtTiempos, ref codigosTransporte, empresa, filaCabecera["DELEGACION"].ToString(), "", usuarioRed);

                    moneda = filaCabecera["MONEDA"].ToString();
                    masterObra = filaCabecera["MASTER OBRA"].ToString();
                    numeroOferta = filaCabecera["OFERTA"].ToString();

                    Informe.txtObra.Text = filaCabecera["OFERTA"].ToString();
                    txtNombreObra.Text = filaCabecera["NOMBRE OFERTA"].ToString();
                    txtCuentaCliente.Text = filaCabecera["CUENTA CLIENTE"].ToString();
                    txtMasterObra.Text = filaCabecera["MASTER OBRA"].ToString();
                    txtCentroCoste.Text = filaCabecera["CENTRO COSTE"].ToString();
                    txtMoneda.Text = filaCabecera["MONEDA"].ToString();

                    porcentajeCobrados = 0;
                    porcentajeFallidos = 0;
                    DataTable dtFallidos = new DataTable();
                    dtFallidos = funcionalidades.fallidos(ref dtTiempos, masterObra, Convert.ToDateTime(filaCabecera["FECHA"].ToString()), version, filaCabecera["CUENTA CLIENTE"].ToString(), "", usuarioRed);
                    foreach (DataRow fila in dtFallidos.Rows)
                    {
                        porcentajeCobrados = Convert.ToDecimal(fila["CFGFAL_PorcentajeCobrados"].ToString());
                        porcentajeFallidos = Convert.ToDecimal(fila["CFGFAL_PorcentajeFallidos"].ToString());
                        break;
                    }

                    dondeVa = "Recuperar versión a aplicar";
                    string versionUtilizada = "";
                    funcionalidades.localizarVersion(ref dtTiempos, ref versionUtilizada, Convert.ToDateTime(filaCabecera["FECHA"].ToString()), "", usuarioRed);
                    lblVersionUtilizada.Text = versionUtilizada;

                    // DATOS PANELES
                    dtArticulosPaneles = new DataTable();
                    dtArticulosPaneles = funcionalidades.localizarPaneles(ref dtTiempos, Convert.ToDateTime(filaCabecera["FECHA"].ToString()), version, "", usuarioRed);

                    // DATOS CONFIGURACION
                    hora_1 = DateTime.Now;
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear conexion";
                    comando = conexion.CreateCommand();
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear comando";
                    comando.CommandText = "ROP_DatosConfiguracionGeneral";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    comando.Parameters.AddWithValue("@delegacion", filaCabecera["DELEGACION"].ToString());
                    if (cmbVersion.SelectedValue == "")
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else
                        comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear adaptador";
                    adaptador.Fill(dtTabla);
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear datatable";
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    dondeVa = "fin recuperar datos configuracion ROP_DatosConfiguracionGeneral";
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracionGeneral";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    for (int i = 0; i < dtTabla.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CALCULAR GASTOS FIJOS POR FACTURACION/COSTE")
                        {
                            if (Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"])) == 0)
                                gastosFijosPorFacturacion = false;
                            else
                                gastosFijosPorFacturacion = true;
                        }
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "DÍAS POR MES")
                            diasxMes = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CÁLCULO FECHA BASE: DÍAS ENTRE FECHA OFERTA Y FECHA CAPÍTULO")
                            diasDiferencia = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CÁLCULO FECHA BASE: DÍAS A RETROCEDER FECHA CREACIÓN DEL CAPÍTULO DE LA OFERTA")
                            diasRestar = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "MES PRESUPUESTO VÁLIDO")
                            mesPresupuestoValido = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "PORCENTAJE GASTO PERSONAL ADMINISTRATIVO EN VARIABLE")
                            porcentajeGastoAdmministrativoVariable = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "PORCENTAJE GASTO PERSONAL COMERCIAL EN VARIABLE")
                            porcentajeGastoComercialVariable = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "PORCENTAJE MÁXIMO TOTAL FACTURACIÓN")
                            porcentajeMaximoTotalFacturacion = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);

                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "COEFICIENTE DE GASTOS FIJOS ALQUILER/VENTA")
                            coeficienteGastosFijosAlquilerVenta = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));

                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "%FINANCIACIÓN BU")
                            financiacionBU = (Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CONDICIONES FINANCIACIÓN BU")
                            condicionesBU = (Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CONDICIONES FINANCIACIÓN CENTRAL")
                            condicionesCentral = (Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        condicionesAplicar = (financiacionBU * condicionesBU) + ((1 - financiacionBU) * condicionesCentral);

                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "DÍAS PAGO POR DEFECTO")
                            diasPagoDefecto = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                    }

                    // DATOS CONFIGURACION AJUSTES
                    hora_1 = DateTime.Now;
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ROP_DatosConfiguracionAjustesConsulta";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    comando.Parameters.AddWithValue("@delegacion", filaCabecera["DELEGACION"].ToString());
                    if (cmbVersion.SelectedValue == "")
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else
                        comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtTabla);
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracionAjustesConsulta";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    for (int i = 0; i < dtTabla.Rows.Count; i++)
                    {
                        // gastos fijos
                        ajustesAlcance = dtTabla.Rows[i]["CFGAJU_Alcance"].ToString();
                        ajustesPocentajeCargoMaximo = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PocentajeCargoMaximo"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = 0;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPocentajeCargoMaximo;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        ajustesValorObraCorta = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorObraCorta"]);
                        ajustesPorcentajeCorreccionObraCorta = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionObraCorta"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = ajustesValorObraCorta;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPorcentajeCorreccionObraCorta;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        ajustesValorObraLarga = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorObraLarga"]);
                        ajustesPorcentajeCorreccionObraLarga = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionObraLarga"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = ajustesValorObraLarga;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPorcentajeCorreccionObraLarga;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        ajustesValorMaximo = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorMaximo"]);
                        ajustesPorcentajeCorreccionMaximo = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionMaximo"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = ajustesValorMaximo;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPorcentajeCorreccionMaximo;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        // taxímetro
                        ajustesTaxDuracionMinima = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_DuracionMinima"]);
                        ajustesTaxDuracionMaxima = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_DuracionMaxima"]);
                        ajustesTaxPorcentajeAbonoMensual = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoMensual"]) / 100;

                        //// Ocupacion
                        //ajusteAlcanceOcupacion = dtTabla.Rows[i]["CFGAJU_AlcanceOcupacion"].ToString(); 
                        //ajustePeriodoOcupacion = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_PeriodoOcupacion"]);
                        //ajusteValorOcupacionMuyBaja = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionMuyBaja"]) / 100;
                        //ajustePorcentajeAbonoMaximo = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoMaximo"]) / 100;
                        //ajusteValorOcupacionBaja = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionBaja"]) / 100;
                        //ajustePorcentajeAbonoOcupacionBaja = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoOcupacionBaja"]) / 100;
                        //ajusteValorOcupacionMedia = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionMedia"]) / 100;
                        //ajustePorcentajeAbonoOcupacionMedia =Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoOcupacionMedia"]) / 100;
                        //ajusteValorOcupacionAlta = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionAlta"]) / 100;
                        //ajustePorcentajeAbonoOcupacionAlta = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoOcupacionAlta"]) / 100;

                        // Cliente
                        ajusteAlcanceCliente = dtTabla.Rows[i]["CFGAJU_AlcanceVolumenCliente"].ToString(); ;
                        ajusteValorClienteMediano = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorClienteMediano"]);
                        ajustePorcentajeCorreccionClienteMediano = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionClienteMediano"]) / 100;
                        ajusteValorClienteGrande = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorClienteGrande"]);
                        ajustePorcentajeCorreccionClienteGrande = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionClienteGrande"]) / 100;
                    }

                    // DATOS COSTE TRANSPORTE
                    dondeVa = "Recuperar datos transporte ";
                    decimal valorCostePorte = 0;
                    decimal margenPorte = 0;
                    decimal coeficienteTransporte = 0;

                    hora_1 = DateTime.Now;
                    strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ROP_TransporteCambiarAKilometros";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    comando.Parameters.AddWithValue("@unidadMedida", null);
                    comando.Parameters.AddWithValue("@medida", filaCabecera["KILOMETRO"]);
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtTabla);
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_TransporteCambiarAKilometros";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    if (dtTabla.Rows.Count > 0)
                        kilometros = Convert.ToDecimal(dtTabla.Rows[0]["medida"]);

                    dondeVa = "Salió de conversion transporte";

                    dondeVa = "ROP_DatosConfiguracionTransporte";
                    hora_1 = DateTime.Now;
                    strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ROP_DatosConfiguracionTransporte";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    if (filaCabecera["DELEGACION"].ToString() == "")
                        comando.Parameters.AddWithValue("@delegacion", null);
                    else
                        comando.Parameters.AddWithValue("@delegacion", filaCabecera["DELEGACION"].ToString());
                    if (kilometros <= 120)
                        comando.Parameters.AddWithValue("@distancia", "<= 120");
                    else
                        if (kilometros >= 120)
                        comando.Parameters.AddWithValue("@distancia", "> 120");
                    else
                        comando.Parameters.AddWithValue("@distancia", DBNull.Value);
                    if (cmbVersion.SelectedValue == "")
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else
                        comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                    comando.Parameters.AddWithValue("@monedaA", moneda);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtTabla);
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracionTransporte";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    dondeVa = "ROP_DatosConfiguracionTransporte salio";
                    if (dtTabla.Rows.Count > 0)
                    {
                        dondeVa = "ROP_DatosConfiguracionTransporte valorCostePorte";
                        valorCostePorte = (Convert.ToDecimal(dtTabla.Rows[0]["Valor"]) / 100);
                        dondeVa = "ROP_DatosConfiguracionTransporte Margen";
                        margenPorte = Convert.ToDecimal(dtTabla.Rows[0]["Margen"]) / 100;

                        coeficienteTransporte = Convert.ToDecimal(dtTabla.Rows[0]["Coeficiente"]);
                    }
                    dondeVa = "Salió de ROP_DatosConfiguracionTransporte";

                    filasCapitulo = dtCapitulosOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "'");
                    if (filasCapitulo.Length == 0)
                        datosGenerales = "NO HAY DATOS DE LOS CAPÍTULOS";
                    else
                    {
                        articulos = ";";
                        articulosCambio = ";";
                        articulosConfiguracion = "|";

                        DateTime horaDatosPase1_1 = DateTime.Now;
                        foreach (DataRow filaCapitulo in filasCapitulo)
                        {
                            dondeVa = "recorrer capitulos";

                            filasLinea = dtLineasOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "' AND CAPITULO = '" + filaCapitulo["CAPITULO"].ToString() + "'");
                            if (filasLinea.Length == 0)
                                datosGenerales = "NO HAY DATOS DE LAS LÍNEAS";
                            else
                            {
                                foreach (DataRow filaLinea in filasLinea)
                                {
                                    dondeVa = "recorrer productos";
                                    if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) >= 0)
                                        existeTR001 = true;

                                    dondeVa = "recorrer productos + recuperar axdEntity_SalesQuotationLine.ItemId -sale-";
                                    // artículos si hay ficha
                                    if (!listaArticulosPedido.Contains(filaLinea["ITEM"].ToString()))
                                    {
                                        dondeVa = "recorrer productos + en listaArticulosPedido";
                                        listaArticulosPedido.Add(filaLinea["ITEM"].ToString());
                                        if (filaCabecera["FECHA"].ToString().Length > 0)
                                            listaFechasPedido.Add(Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                                        else
                                            listaFechasPedido.Add(new DateTime());
                                    }

                                    // artículos si no hay ficha
                                    if (!listaArticulosSinFicha.Contains(filaLinea["ITEM"].ToString()))
                                    {
                                        dondeVa = "recorrer productos + en listaArticulosSinFicha";
                                        articulosConfiguracion = articulosConfiguracion + filaLinea["ITEM"].ToString() + "|";
                                        listaArticulosSinFicha.Add(filaLinea["ITEM"].ToString());
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
                                if (String.IsNullOrEmpty(filaCabecera["OBRA"].ToString()))
                                {
                                    dondeVa = "no hay ficha";
                                    fechaPrecios = new DateTime();
                                    if (filaCabecera["FECHA"].ToString().Length > 0 && filaCapitulo["FECHA"].ToString().Length > 0)
                                    {
                                        dias = Math.Abs(Convert.ToDateTime(filaCabecera["FECHA"].ToString()).Subtract(Convert.ToDateTime(filaCapitulo["FECHA"].ToString())).Days);
                                        if (Math.Abs(Convert.ToDateTime(filaCabecera["FECHA"].ToString()).Subtract(Convert.ToDateTime(filaCapitulo["FECHA"].ToString())).Days) > diasDiferencia)
                                        {
                                            fechaPrecios = Convert.ToDateTime(filaCapitulo["FECHA"].ToString()).AddDays((-1) * diasRestar);
                                            tabla = "SalesQuotationMasterTable";
                                            IDCapitulo = filaCapitulo["CAPITULO"].ToString();
                                            campo = "CreatedDateTime - " + diasRestar.ToString();
                                        }
                                        else
                                        {
                                            fechaPrecios = Convert.ToDateTime(filaCapitulo["FECHA"].ToString());
                                            tabla = "SalesQuotationMasterTable";
                                            IDCapitulo = filaCapitulo["CAPITULO"].ToString();
                                            campo = "CreatedDateTime";
                                        }
                                    }
                                    else
                                    {
                                        if (filaCapitulo["FECHA"].ToString().Length > 0)
                                        {
                                            fechaPrecios = Convert.ToDateTime(filaCapitulo["FECHA"].ToString());
                                            tabla = "SalesQuotationMasterTable";
                                            IDCapitulo = filaCapitulo["CAPITULO"].ToString();
                                            campo = "CreatedDateTime";
                                        }
                                    }

                                    for (lineaCapitulo = 0; lineaCapitulo < listaArticulosSinFicha.Count; lineaCapitulo++)
                                    {
                                        if (String.IsNullOrEmpty(filaCabecera["OBRA"].ToString()))
                                        {
                                            if (articulos.IndexOf(listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";") == -1)
                                                articulos = articulos + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";

                                            oFechaBase = dtFechasBase.NewRow();
                                            oFechaBase[0] = listaArticulosSinFicha.ElementAt(lineaCapitulo);
                                            oFechaBase[1] = Convert.ToDateTime(fechaPrecios);
                                            dtFechasBase.Rows.Add(oFechaBase);
                                        }
                                        articulosCambio = articulosCambio + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";
                                    }
                                    // Fin calculo de la fecha a utilizar
                                }
                            }
                        }
                        TimeSpan horaDatosPase1_2 = DateTime.Now.Subtract(horaDatosPase1_1);

                        // Calculo de la fecha a utilizar
                        //o   ¿La oferta está asociada a una ficha ?
                        //	SI:
                        //•	Fecha base = Fecha base ficha
                        //•	Consultar tabla BaseDateItemObra
                        if (!String.IsNullOrEmpty(filaCabecera["OBRA"].ToString()))
                        {
                            fechaPrecios = new DateTime();
                            fechaPrecios = DateTime.Now;
                            dondeVa = "fecha base ficha artículo:entró";

                            hora_1 = DateTime.Now;
                            funcionalidades.fechaBaseFichaArticulo(ref dtTiempos, ref listaOfertas, ref listaFechasOferta, ref listaArticulosPedido, ref listaFechasPedido, empresa, filaCabecera["OBRA"].ToString(), diasDiferencia, diasRestar, Informe.WSDominio, Informe.WSUsuario, Informe.WSContrasena, "", usuarioRed);
                            dondeVa = "fecha base ficha artículo: salió";
                            hora_2 = DateTime.Now.Subtract(hora_1);
                            oItemTiempo = dtTiempos.NewRow();
                            oItemTiempo[0] = "TIEMPO fechaBaseFichaArticulo";
                            oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                            dtTiempos.Rows.Add(oItemTiempo);

                            tabla = "fechaBaseFichaArticulo";
                            IDCapitulo = "Ficha " + filaCabecera["OBRA"];
                            campo = "fechaBaseFichaArticulo";

                            articulos = ";";
                            articulosCambio = ";";
                            for (lineaCapitulo = 0; lineaCapitulo < listaArticulosPedido.Count; lineaCapitulo++)
                            {
                                if (articulos.IndexOf(listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";") == -1)
                                    articulos = articulos + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";
                                articulosCambio = articulosCambio + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";

                                oFechaBase = dtFechasBase.NewRow();
                                oFechaBase[0] = listaArticulosPedido.ElementAt(lineaCapitulo);
                                oFechaBase[1] = Convert.ToDateTime(listaFechasPedido.ElementAt(lineaCapitulo).ToString());
                                dtFechasBase.Rows.Add(oFechaBase);

                                if (listaFechasPedido.ElementAt(lineaCapitulo) < fechaPrecios)
                                    fechaPrecios = listaFechasPedido.ElementAt(lineaCapitulo);
                            }
                        }

                        if (fechaPrecios != new DateTime())
                        {
                            hora_1 = DateTime.Now;
                            strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
                            conexion = new SqlConnection(strConexion);
                            conexion.Open();
                            comando = conexion.CreateCommand();
                            comando.CommandText = "sp_ROP_FechaBaseOfertaIncluir";
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.Parameters.AddWithValue("@FBO_Oferta", numeros);
                            comando.Parameters.AddWithValue("@FBO_Capitulo", IDCapitulo);
                            comando.Parameters.AddWithValue("@FBO_Fecha", fechaPrecios);
                            comando.Parameters.AddWithValue("@FBO_Articulo", articulos.Replace("'", ""));
                            comando.Parameters.AddWithValue("@FBO_TablaFechaSeleccionada", tabla);
                            comando.Parameters.AddWithValue("@FBO_CampoFechaSeleccionada1", campo);
                            comando.Parameters.AddWithValue("@FBO_DiasDiferencia", dias);
                            comando.Parameters.AddWithValue("@primeraVez", primeraVez);
                            comando.ExecuteNonQuery();
                            conexion.Close();
                            primeraVez = false;
                            hora_2 = DateTime.Now.Subtract(hora_1);
                            oItemTiempo = dtTiempos.NewRow();
                            oItemTiempo[0] = "TIEMPO llamar al SQL sp_ROP_FechaBaseOfertaIncluir";
                            oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                            dtTiempos.Rows.Add(oItemTiempo);

                            dondeVa = "localizar precios artículos";

                            dtArticulos = funcionalidades.localizarPreciosArticulos(ref dtTiempos, empresa, filaCabecera["DELEGACION"].ToString(), articulos, moneda, articulosCambio, "", usuario, version, "", usuarioRed);
                            hayPrecio = true;
                        }
                        else
                        {
                            dtArticulos = funcionalidades.localizarPreciosArticulos(ref dtTiempos, empresa, filaCabecera["DELEGACION"].ToString(), articulos, moneda, articulosCambio, "", usuario, version, "", usuarioRed);
                            hayPrecio = true;
                        }

                        // GASTOS OFICINA 
                        if (dtDatosGastosOficina.Rows.Count == 0)
                        {
                            dtDatosGastosOficina = funcionalidades.gastosOficina(ref dtTiempos, empresa, masterObra, fechaPrecios, moneda, mesPresupuestoValido, porcentajeGastoAdmministrativoVariable, porcentajeGastoComercialVariable, usuario, "", usuarioRed);
                            foreach (DataRow fila in dtDatosGastosOficina.Rows)
                            {
                                dondeVa = "encontró precio coste dataset dtDatosGastosOficina";
                                totalAnual = Convert.ToDecimal(fila["fact_anual_empresa"]);
                                totalAdministrativo = Convert.ToDecimal(fila["total_admin"]);
                                varAdministrativo = Convert.ToDecimal(fila["var_admin"]);
                                totalComercial = Convert.ToDecimal(fila["total_comerc"]);
                                varComercial = Convert.ToDecimal(fila["var_comerc"]);
                                costeDT = Convert.ToDecimal(fila["costeDT"]);
                                preciosEnCero = Convert.ToInt32(fila["preciosEnCero"]);
                            }
                            if (preciosEnCero != 0)
                            {
                                funcionalidades.guardarIncidencia(empresa, "OFERTA", numeroOferta, "", "En el cálculo de coste gastos oficina DT hay precios en cero.", 0, usuario, User.Identity.Name.ToString());
                                Informe.mostrarMensaje("En el cálculo de coste gastos oficina DT hay precios en cero.");
                            }
                        }

                        // GASTOS FIJOS
                        if (dtDatosGastosFijos.Rows.Count == 0)
                        {
                            dtDatosGastosFijos = funcionalidades.gastosFijos(ref dtTiempos, empresa, gastosFijosPorFacturacion, masterObra, fechaPrecios, moneda, mesPresupuestoValido, porcentajeGastoAdmministrativoVariable, porcentajeGastoComercialVariable, coeficienteGastosFijosAlquilerVenta, usuario, "", usuarioRed);
                            foreach (DataRow fila in dtDatosGastosFijos.Rows)
                            {
                                dondeVa = "encontró precio coste dataset dtDatosGastosFijos";
                                fact_anual_empresa = Convert.ToDecimal(fila["fact_anual_empresa"]);
                                facturacion_alquiler = Convert.ToDecimal(fila["facturacion_alquiler"]);
                                facturacion_venta = Convert.ToDecimal(fila["facturacion_venta"]);
                                total_admin = Convert.ToDecimal(fila["total_admin"]);
                                var_admin = Convert.ToDecimal(fila["var_admin"]);
                                total_comerc = Convert.ToDecimal(fila["total_comerc"]);
                                var_comerc = Convert.ToDecimal(fila["var_comerc"]);
                                total_gf_bu = Convert.ToDecimal(fila["total_gf_bu"]);
                                total_gf_hq = Convert.ToDecimal(fila["total_gf_hq"]);
                                KVBU = Convert.ToDecimal(fila["KVBU"]);
                                KABU = Convert.ToDecimal(fila["KABU"]);
                                KVHQ = Convert.ToDecimal(fila["KVHQ"]);
                                KAHQ = Convert.ToDecimal(fila["KAHQ"]);
                            }
                        }

                        DateTime horaDatosConfigurados_1 = DateTime.Now;
                        dtGastosPatio = funcionalidades.gastosPatio(ref dtTiempos, empresa, articulos, fechaPrecios, moneda, usuario, "", usuarioRed);
                        dtOcupacion = funcionalidades.ocupacion(ref dtTiempos, version, articulos, masterObra, false, fechaPrecios, "", usuarioRed);
                        funcionalidades.ajustesBU(ref dtTiempos, ref dtAjustesDatosBU, ref dtAjustesBU, masterObra, fechaPrecios, moneda, articulos, version, false, "", usuarioRed);
                        foreach (DataRow fila in dtAjustesBU.Rows)
                        {
                            dondeVa = "datos ajustesBU";
                            cumplimientoBajoAfectaGFCentral = Convert.ToString(fila["CFGAJU_CumplimientoBajoAfectaGFCentral"]);
                            cumplimientoBajoAfectaGFBU = Convert.ToString(fila["CFGAJU_CumplimientoBajoAfectaGFBU"]);
                            cumplimientoAltoAfectaGFCentral = Convert.ToString(fila["CFGAJU_CumplimientoAltoAfectaGFCentral"]);
                            cumplimientoAltoAfectaGFBU = Convert.ToString(fila["CFGAJU_CumplimientoAltoAfectaGFBU"]);
                            cumplimientoBajoPorcentaje = Convert.ToDecimal(fila["CFGAJU_CumplimientoBajoPorcentaje"]) / 100;
                            bonificacionCumplimientoBajoPorcentaje = Convert.ToDecimal(fila["CFGAJU_BonificacionCumplimientoBajoPorcentaje"]) / 100;
                            cumplimientoAltoPorcentaje = Convert.ToDecimal(fila["CFGAJU_CumplimientoAltoPorcentaje"]) / 100;
                            bonificacionCumplimientoAltoPorcentaje = Convert.ToDecimal(fila["CFGAJU_BonificacionCumplimientoAltoPorcentaje"]) / 100;
                        }

                        TimeSpan horaDatosConfigurados_2 = DateTime.Now.Subtract(horaDatosConfigurados_1);
                        DateTime horaDatosPase2_1 = DateTime.Now;

                        // RECUPERAR LOS DATOS DE CONFIGURACIÓN: TAXIMETRO - GASTOS - AJUSTES 
                        dondeVa = "ROP_DatosConfiguracion";
                        hora_1 = DateTime.Now;
                        dtDatosConfiguracion = new System.Data.DataTable();
                        conexion = new SqlConnection(strConexion);
                        conexion.Open();
                        comando = conexion.CreateCommand();
                        comando.CommandText = "ROP_DatosConfiguracion";
                        comando.CommandTimeout = 240000;
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@empresa", empresa);
                        comando.Parameters.AddWithValue("@usuario", usuario); // Environment.UserName);
                        if (cmbVersion.SelectedValue == "")
                            comando.Parameters.AddWithValue("@CFG_ID", null);
                        else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                            comando.Parameters.AddWithValue("@CFG_ID", null);
                        else
                            comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                        comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"]));
                        comando.Parameters.AddWithValue("@moneda", moneda);
                        comando.Parameters.AddWithValue("@articulos", articulosConfiguracion);
                        if (idioma == "INGLES")
                            comando.Parameters.AddWithValue("@idioma", "IN");
                        else
                            comando.Parameters.AddWithValue("@idioma", "ES");
                        adaptadorDatosConfiguracion = new SqlDataAdapter(comando);
                        adaptadorDatosConfiguracion.Fill(dtDatosConfiguracion);
                        adaptadorDatosConfiguracion.Dispose();
                        comando.Dispose();
                        conexion.Close();
                        conexion.Dispose();
                        hora_2 = DateTime.Now.Subtract(hora_1);
                        oItemTiempo = dtTiempos.NewRow();
                        oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracion";
                        oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                        dtTiempos.Rows.Add(oItemTiempo);

                        filasCapitulo = dtCapitulosOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "'");
                        foreach (DataRow filaCapitulo in filasCapitulo)
                        {
                            filasLinea = dtLineasOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "' AND CAPITULO = '" + filaCapitulo["CAPITULO"].ToString() + "'");
                            if (filasLinea.Length == 0)
                                datosGenerales = "NO HAY DATOS DE LAS LÍNEAS";
                            else
                            {
                                tituloCapitulo = filaCapitulo["CAPITULO"].ToString();

                                importeAlquilerCapitulo = 0;
                                importeVentaCapitulo = 0;

                                importePorte = 0;

                                importeFacturacionAlquilerCapitulos = 0;
                                importeFacturacionVentaCapitulos = 0;
                                dec_importeCosteVCH = 0;
                                importeFacturacionVentaDepartamentoTecnicoCapitulos = 0;
                                importeFacturacionVentaFenolicoNuevoCapitulos = 0;
                                importeFacturacionVentaUneCifCapitulos = 0;
                                importeFacturacionVentaMontajesCapitulos = 0;
                                importeFacturacionVentaProductoCapitulos = 0;
                                importeCosteVentaDepartamentoTecnicoCapitulos = 0;
                                importeFacturacionVentaOtrosServiciosCapitulos = 0;
                                importeCosteVentaOtrosServiciosCapitulos = 0;

                                importeGastosVariablesEntregaDevolucionAlquilerCapitulos = 0;
                                importeGastosVariablesEntregaDevolucionVentaCapitulos = 0;
                                importeGastosVariablesClaisificacionReparacionAlquilerCapitulos = 0;
                                importeGastosVariablesClaisificacionReparacionVentaCapitulos = 0;
                                importeCosteVentaFenolicoNuevoCapitulos = 0;
                                importeGastosVariablesVentaFenolicoNuevoCapitulos = 0;
                                importeGastosFijosBUVentaFenolicoNuevoCapitulos = 0;
                                importeGastosFijosCentralesVentaFenolicoNuevoCapitulos = 0;
                                importeCosteVentaNuevoCapitulos = 0;
                                importeCosteVentaUneCifCapitulos = 0;
                                importeGastosVariablesVentaUneCifCapitulos = 0;
                                importeGastosFijosBUVentaUneCifCapitulos = 0;
                                importeGastosFijosCentralesVentaUneCifCapitulos = 0;

                                importeCosteVentaMontajesCapitulos = 0;
                                importeGastosVariablesVentaMontajesCapitulos = 0;
                                importeGastosFijosBUVentaMontajesCapitulos = 0;
                                importeGastosFijosCentralesVentaMontajesCapitulos = 0;

                                importeCosteVentaCapitulos = 0;
                                importeCosteTaximetroNoConsumibleCapitulos = 0;
                                importeCosteTaximetroConsumibleCapitulos = 0;
                                importeCosteBonificacionTaximetroConsumibleCapitulos = 0;

                                importeAjustesObraDuracionGFCapitulo = 0;
                                importeAjustesObraDuracionTaxCapitulo = 0;
                                importeAjustesProductoTaxCapitulo = 0;
                                importeGastosFinanciacionCapitulos = 0;

                                totalGastosFijosBU = 0;
                                totalGastosFijosCentral = 0;

                                pesoCapitulo = 0;
                                importePorte_TR001 = 0;
                                //hayTR001 = false;
                                existeTR001 = false; ;
                                foreach (DataRow filaLinea in filasLinea)
                                {
                                    dondeVa = "Fenólico";
                                    if ((bool)Session["datosFenolicosOriginales"])
                                    {
                                        if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                        {
                                            chkBoxFenolico.Checked = true;
                                            calcularConFenolico = true;
                                        }
                                    }
                                    else
                                    {
                                        calcularConFenolico = chkBoxFenolico.Checked;
                                    }

                                    // GASTOS FIJOS
                                    oItemGastosFijos = dtGastosFijos.NewRow();
                                    oItemGastosFijos["FACTURACION ALQUILER"] = 0;
                                    oItemGastosFijos["FACTURACION VENTA"] = 0;
                                    oItemGastosFijos["COSTE ALQUILER"] = 0;
                                    oItemGastosFijos["COSTE VENTA"] = 0;

                                    oItemGastosFijos["fact_anual_empresa"] = fact_anual_empresa;
                                    oItemGastosFijos["facturacion_alquiler"] = facturacion_alquiler;
                                    oItemGastosFijos["facturacion_venta"] = facturacion_venta;
                                    oItemGastosFijos["total_admin"] = total_admin;
                                    oItemGastosFijos["var_admin"] = var_admin;

                                    oItemGastosFijos["total_comerc"] = fact_anual_empresa;
                                    oItemGastosFijos["var_comerc"] = facturacion_alquiler;
                                    oItemGastosFijos["total_gf_bu"] = facturacion_venta;
                                    oItemGastosFijos["total_gf_hq"] = total_admin;
                                    oItemGastosFijos["KVBU"] = KVBU;
                                    oItemGastosFijos["KABU"] = KABU;
                                    oItemGastosFijos["KVHQ"] = KVHQ;
                                    oItemGastosFijos["KAHQ"] = KAHQ;

                                    BUimporteLinea = 0;
                                    centralImporteLinea = 0;
                                    importeVentaLinea = 0;
                                    importeAlquilerLinea = 0;
                                    porcentajeAjustesObraDuracionGFCapitulo = 0;

                                    dondeVa = "* recorrer productos " + filaLinea["ITEM"].ToString();

                                    if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) >= 0)
                                        //hayTR001 = true;
                                        existeTR001 = true;

                                    DataRow oItemTaximetro = dtTaximetro.NewRow();
                                    oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = "0";
                                    oItemTaximetro["IMPORTE TAX CONSUMIBLE"] = "0";
                                    oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()).ToString("#,##0.00");
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
                                    tipoServicio = "";

                                    precioCoste = 0;
                                    if (hayPrecio)
                                    {
                                        dondeVa = "buscar precio dataset";
                                        filaEncontrada = dtArticulos.Select("Articulo = '" + filaLinea["ITEM"].ToString().Trim() + "'");
                                        if (filaEncontrada.Count() == 0 && codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) == -1)
                                        {
                                            funcionalidades.guardarIncidencia(empresa, "OFERTA", numeroOferta, filaLinea["ITEM"].ToString(), "No hay PS del artículo.", 0, usuario, User.Identity.Name.ToString());
                                            Informe.mostrarMensaje("(PED_1) No se ha localizado el precio del articulo '" + filaLinea["ITEM"].ToString() + "'");
                                        }
                                        foreach (DataRow fila in filaEncontrada)
                                        {
                                            dondeVa = "encontró precio coste dataset // " + articulosCambio + " // ";
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["Precio"])))
                                            {
                                                precioCoste = Convert.ToDecimal(fila["Precio"]);
                                                oItemTaximetro[COL_DATA_PRECIO_CAMBIO] = Convert.ToDecimal(fila["Precio"]).ToString("#,##0.00");
                                            }
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

                                    // gastos variables patio
                                    clasificarRepararDirecto = 0;
                                    clasificarRepararIndirecto = 0;
                                    clasificarDirecto = 0;
                                    clasificarIndirecto = 0;
                                    repararDirecto = 0;
                                    repararIndirecto = 0;
                                    entrega = 0;
                                    devolucion = 0;
                                    naviera = 0;
                                    string aafBuscar = "";

                                    aafBuscar = filaLinea["ITEM"].ToString();
                                    filaEncontrada = dtGastosPatio.Select("ProductId = '" + filaLinea["ITEM"].ToString() + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["AAF"])))
                                            aafBuscar = Convert.ToString(fila["AAF"]);
                                    }

                                    //filaEncontrada = dtGastosPatio.Select("ProductId = '" + filaLinea["ITEM"].ToString() + "'");
                                    filaEncontrada = dtGastosPatio.Select("ProductId = '" + aafBuscar + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Entrega"])))
                                            entrega = Convert.ToDecimal(fila["Entrega"]);
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Devolucion"])))
                                            devolucion = Convert.ToDecimal(fila["Devolucion"]);
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Naviera"])))
                                            naviera = Convert.ToDecimal(fila["Naviera"]);

                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarReparar"])))
                                        {
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarRepararDirecto"])))
                                                clasificarRepararDirecto = Convert.ToDecimal(fila["ClasificarRepararDirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarRepararIndirecto"])))
                                                clasificarRepararIndirecto = Convert.ToDecimal(fila["ClasificarRepararIndirecto"]);
                                        }
                                        else
                                        {
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarDirecto"])))
                                                clasificarDirecto = Convert.ToDecimal(fila["ClasificarDirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarIndirecto"])))
                                                clasificarIndirecto = Convert.ToDecimal(fila["ClasificarIndirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["RepararDirecto"])))
                                                repararDirecto = Convert.ToDecimal(fila["RepararDirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["RepararIndirecto"])))
                                                repararIndirecto = Convert.ToDecimal(fila["RepararIndirecto"]);
                                        }
                                        precioGastosPatio = Convert.ToDecimal(fila["Precio"]);
                                    }

                                    // recuperar datos de configuración del datatable
                                    dondeVa = "";
                                    listaBonificacionesDesde.Clear();
                                    listaBonificacionesHasta.Clear();
                                    listaBonificacionesValor.Clear();
                                    diasDesdeTaxNoConsumible = 0;
                                    diasHastaTaxNoConsumible = 0;
                                    pesoArticulo = 0;
                                    vocacionChatarra = 0;
                                    filaEncontrada = dtDatosConfiguracion.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        dondeVa = "encontró configuracion dataset // " + filaLinea["ITEM"].ToString() + " // ";

                                        dondeVa = "Peso del artículo";
                                        pesoArticulo = Convert.ToDecimal(fila["WeightVolumeKg"]);
                                        vocacionChatarra = Convert.ToInt32(fila["SpecialItemId"]);
                                        dondeVa = "encontró configuracion dataset // Tipo Servicio ";
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["CFGSERV_Tipo"])))
                                            tipoServicio = Convert.ToString(fila["CFGSERV_Tipo"]);
                                        dondeVa = "encontró configuracion dataset // Coste superficie";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE SUPERFICIE")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            costeSuperficie = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        dondeVa = "encontró configuracion dataset // Taxímetro consumible";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "TAXÍMETRO CONSUMIBLE")
                                        {
                                            taximetroConsumible = (Convert.ToDecimal(fila["Valor"]) / 100);
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
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "TAXÍMETRO NO CONSUMIBLE" && (calcularPorSuperficie || taximetroConsumible == 0))
                                        {
                                            dondeVa = "encontró configuracion dataset // Valor tax no consumible";
                                            taximetroNoConsumible = (Convert.ToDecimal(fila["Valor"]) / 100);
                                            diasDesdeTaxNoConsumible = Convert.ToInt32(fila["Desde"]);
                                            diasHastaTaxNoConsumible = Convert.ToInt32(fila["Hasta"]);
                                        }

                                        dondeVa = "encontró configuracion dataset // Meters";
                                        metersInvoicing = Convert.ToBoolean(fila["MetersInvoicing"]);
                                        dondeVa = "encontró configuracion dataset // Tipo producto";
                                        productType = Convert.ToString(fila["ProductTypeName_es"]);

                                        dondeVa = "encontró configuracion dataset // Correcion PS Nuevo";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COEF. CORRECCIÓN PS NUEVO")
                                            coeficienteNuevo = Convert.ToDecimal(fila["Valor"]);
                                        dondeVa = "encontró configuracion dataset // Correcion PS Usado";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COEF. CORRECCIÓN PS USADO")
                                        {
                                            if (Convert.ToString(fila["Valorar"]).ToUpper() == "")
                                            {
                                                coeficienteUsadoVenta = Convert.ToDecimal(fila["Valor"]);
                                                coeficienteUsadoMPOLiquidacion = Convert.ToDecimal(fila["Valor"]);
                                                coeficienteUsadoChatarra = Convert.ToDecimal(fila["Valor"]);
                                            }
                                            else if (Convert.ToString(fila["Valorar"]).ToUpper() == "VENTA")
                                                coeficienteUsadoVenta = Convert.ToDecimal(fila["Valor"]);
                                            else if (Convert.ToString(fila["Valorar"]).ToUpper() == "MPO_LIQ")
                                                coeficienteUsadoMPOLiquidacion = Convert.ToDecimal(fila["Valor"]);
                                            else if (Convert.ToString(fila["Valorar"]).ToUpper() == "CHATARRA")
                                                coeficienteUsadoChatarra = Convert.ToDecimal(fila["Valor"]);
                                        }
                                        dondeVa = "encontró configuracion dataset // Correcion PS servicio";
                                        //if (Convert.ToString(fila["Concepto"]) == "Coef. Corrección PS Servicio")
                                        //    coeficienteServicio = Convert.ToDecimal(fila["Valor"]);
                                        dondeVa = "encontró configuracion dataset // Tipo artículo " + filaLinea["ITEM"].ToString();
                                        tipoArticulo = Convert.ToString(fila["TipoArticulo"]);
                                        tipoArticuloFenolico = Convert.ToString(fila["tipoArticuloFenolico"]);

                                        dondeVa = "encontró configuracion dataset // Porcentaje coste material nuevo " + filaLinea["ITEM"].ToString();
                                        // Fenólico / Nuevo
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE (%) ENTREGA MATERIAL NUEVO")
                                            porcentajeCosteMaterialNuevo = Convert.ToDecimal(fila["Valor"]) / 100;
                                        dondeVa = "encontró configuracion dataset // Coste fenólico nuevo estandar " + filaLinea["ITEM"].ToString();
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE FENÓLICO NUEVO ESTÁNDAR")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            costeFenolicoNuevoEstandar = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        dondeVa = "encontró configuracion dataset // Coste fenólico nuevo especial " + filaLinea["ITEM"].ToString();
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE FENÓLICO NUEVO ESPECIAL")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            costeFenolicoNuevoEspecial = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "CANON FENÓLICO NUEVO PANEL ESTÁNDAR")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            canonFenolicoNuevoEstandar = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        dondeVa = "encontró configuracion dataset // Canon fenólico nuevo panel especial  " + filaLinea["ITEM"].ToString();
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "CANON FENÓLICO NUEVO PANEL ESPECIAL")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            canonFenolicoNuevoEspecial = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                    }

                                    dondeVa = "salio configuracion dataset";

                                    dondeVa = "Artículo nuevo/usado";

                                    importeCosteMaterialNuevo = 0;
                                    importeCosteMaterialFenolico = 0;

                                    // Vocacion chatarra
                                    if (vocacionChatarra == 2)
                                        dec_importeCosteVCH = dec_importeCosteVCH + (Convert.ToDecimal(filaLinea["CANTIDAD"]) * precioCoste);
                                    else
                                    {
                                        if (tipoArticulo == "NUEVO")
                                        {
                                            coeficienteUsar = coeficienteNuevo;
                                            oItemTaximetro[COL_DATA_PRECIO_CORRECION_NUEVO] = (precioCoste * coeficienteNuevo).ToString("#,##0.00");
                                        }
                                        if (tipoArticulo == "USADO")
                                        {
                                            coeficienteUsar = coeficienteUsadoChatarra;
                                            oItemTaximetro[COL_DATA_PRECIO_CORRECION_USADO] = (precioCoste * coeficienteUsadoChatarra).ToString("#,##0.00");
                                        }
                                    }

                                    dondeVa = "Guarda datos taxímetro";
                                    oItemTaximetro[COL_DATA_FECHA_DOC] = filaCabecera["FECHA"].ToString();
                                    oItemTaximetro[COL_DATA_FECHA_CAPITULO] = filaCapitulo["FECHA"].ToString();
                                    oItemTaximetro[COL_DATA_CAPITULO] = tituloCapitulo;
                                    oItemTaximetro[COL_DATA_ITEM] = filaLinea["ITEM"].ToString();
                                    oItemTaximetro[COL_DATA_TIPO_ITEM] = tipoArticulo;
                                    oItemTaximetro[COL_DATA_IMPORTE_ALQUILER] = 0;
                                    oItemTaximetro[COL_DATA_IMPORTE_VENTA] = 0;

                                    oItemTaximetro[COL_DATA_PRECIO] = precioCoste.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_FECHA] = fechaPrecio.ToString("dd/MM/yyyy");
                                    oItemTaximetro[COL_DATA_TIPO_CAPITULO] = filaCapitulo["TIPO"].ToString();
                                    oItemTaximetro[COL_DATA_PRECIO_GASTOS_PATIO] = precioGastosPatio.ToString("##0.00");

                                    if (filaCapitulo["TIPO"].ToString() == "RENTAL")
                                    {
                                        dondeVa = "Fenólico / material nuevo";
                                        if (tipoArticuloFenolico == "NUEVO" || tipoArticuloFenolico == "MIXTO")
                                        {
                                            filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                            foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                            {
                                                if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                    importeCosteMaterialFenolico = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * costeFenolicoNuevoEstandar);
                                                else
                                                    importeCosteMaterialFenolico = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * costeFenolicoNuevoEspecial);
                                            }
                                        }
                                        if (tipoArticuloFenolico == "NUEVO")
                                            importeCosteMaterialNuevo = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * (precioCoste * coeficienteNuevo) - importeCosteMaterialFenolico) * porcentajeCosteMaterialNuevo;

                                        oItemFenolicoNuevo = dtFenolicoNuevo.NewRow();
                                        oItemFenolicoNuevo["ITEM"] = filaLinea["ITEM"].ToString();
                                        oItemFenolicoNuevo["TIPO"] = tipoArticuloFenolico;
                                        oItemFenolicoNuevo["PS"] = precioCoste;
                                        oItemFenolicoNuevo["CANTIDAD"] = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString());
                                        oItemFenolicoNuevo["COSTE PS"] = (precioCoste * coeficienteNuevo);
                                        oItemFenolicoNuevo["COSTE FENOLICO ESTANDAR"] = costeFenolicoNuevoEstandar;
                                        oItemFenolicoNuevo["COSTE FENOLICO ESPECIAL"] = costeFenolicoNuevoEspecial;
                                        oItemFenolicoNuevo["METROS"] = superficie;
                                        oItemFenolicoNuevo["COSTE FENOLICO"] = importeCosteMaterialFenolico;
                                        oItemFenolicoNuevo["% COSTE MATERIAL NUEVO"] = porcentajeCosteMaterialNuevo;
                                        oItemFenolicoNuevo["COSTE MATERIAL NUEVO"] = importeCosteMaterialNuevo;
                                        dtFenolicoNuevo.Rows.Add(oItemFenolicoNuevo);

                                        importeCosteVentaFenolicoNuevoCapitulos = importeCosteVentaFenolicoNuevoCapitulos + importeCosteMaterialFenolico;
                                    }

                                    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + importeCosteMaterialFenolico;

                                    if (filaCapitulo["TIPO"].ToString() == "SALES")
                                    {
                                        dondeVa = "VENTAS";

                                        dondeVa = "VENTAS: Gastos variables";
                                        importeGastosPatioEntregaDevolucionVentas = 0;
                                        importeGastosPatioEntregaDevolucionVentas = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio;
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                            {
                                                importeGastosPatioEntregaDevolucionVentas = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio;
                                                //oItemTaximetro["NAVIERA"] = naviera.ToString("##0.00");
                                            }
                                        importeGastosVariablesEntregaDevolucionVentaCapitulos = importeGastosVariablesEntregaDevolucionVentaCapitulos + importeGastosPatioEntregaDevolucionVentas;

                                        oItemTaximetro[COL_DATA_VENTAS_ENTREGA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_VENTAS_DEVOLUCION] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_ENTREGA_NAVIERA] = "0.00";
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                                oItemTaximetro[COL_DATA_VENTAS_ENTREGA_NAVIERA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio).ToString("##0.00");

                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_REPARAR_DIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_REPARAR_INDIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_DIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_INDIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_REPARAR_DIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_REPARAR_INDIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_GASTOS_PATIO] = importeGastosPatioEntregaDevolucionVentas.ToString("##0.00");

                                        dondeVa = "VENTAS: Guardar coeficiente nuevo/usado";
                                        if (tipoArticulo == "NUEVO")
                                            oItemTaximetro[COL_DATA_COEFICIENTE_NUEVO] = coeficienteNuevo.ToString("##0.00");
                                        if (tipoArticulo == "USADO")
                                            oItemTaximetro[COL_DATA_COEFICIENTE_USADO] = coeficienteUsadoVenta.ToString("##0.00");

                                        dondeVa = "VENTAS: Calcular importe venta";
                                        importeVenta = importeVenta + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                        importeVentaCapitulo = importeVentaCapitulo + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                        importeVentaLinea = Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                        importeFacturacionVentaCapitulos = importeFacturacionVentaCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());

                                        if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) < 0)
                                        {
                                            // mail 29/07/2021 Nacho: configuración manda sobre tipo producto
                                            if (tipoServicio.ToUpper() == "DPTO_TECNICO")
                                            {
                                                importeFacturacionVentaDepartamentoTecnicoCapitulos = importeFacturacionVentaDepartamentoTecnicoCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                importeCosteVentaDepartamentoTecnicoCapitulos = importeCosteVentaDepartamentoTecnicoCapitulos + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;
                                                oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar);
                                            }
                                            else if (tipoServicio.ToUpper() == "MONTAJES")
                                            {
                                                importeFacturacionVentaMontajesCapitulos = importeFacturacionVentaMontajesCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                // COSTE
                                                importeCosteVentaMontajesCapitulos = importeCosteVentaMontajesCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * precioCoste;
                                                oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + (Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * precioCoste);
                                            }
                                            else if (productType.Trim().ToUpper() == "SERVICIO")
                                            {
                                                dondeVa = "VENTAS: Servicio";
                                                // SVP/CIF
                                                if (Convert.ToDecimal(filaLinea["CANON IMPORTE"]) != 0)
                                                {
                                                    importeFacturacionVentaUneCifCapitulos = importeFacturacionVentaUneCifCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                    // COSTE
                                                    importeCosteVentaUneCifCapitulos = importeCosteVentaUneCifCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                                    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                                }
                                                // OTROS SERVICIOS
                                                else
                                                {
                                                    importeFacturacionVentaOtrosServiciosCapitulos = importeFacturacionVentaOtrosServiciosCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                    // COSTE
                                                    importeCosteVentaOtrosServiciosCapitulos = importeCosteVentaOtrosServiciosCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                                    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                                }
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA_SERVICIO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                            }
                                            else
                                            {
                                                dondeVa = "VENTAS: Producto";
                                                importeFacturacionVentaProductoCapitulos = importeFacturacionVentaProductoCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA_PRODUCTO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");

                                                dondeVa = "Coste venta";
                                                if (tipoArticuloFenolico == "MIXTO")
                                                {
                                                    decimal costeFenolico = 0;
                                                    decimal costePS = 0;
                                                    decimal costeMixto = 0;
                                                    filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                                    foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                                    {
                                                        if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                            costeFenolico = superficie * costeFenolicoNuevoEstandar;
                                                        else
                                                            costeFenolico = superficie * costeFenolicoNuevoEspecial;
                                                    }
                                                    costePS = precioCoste;
                                                    costeMixto = ((costePS - costeFenolico) * coeficienteUsar) + costeFenolico;
                                                    importeCosteVentaCapitulos = importeCosteVentaCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * costeMixto);
                                                }
                                                else
                                                    importeCosteVentaCapitulos = importeCosteVentaCapitulos + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;

                                                oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;
                                            }

                                            // Fenólico
                                            if (calcularConFenolico)
                                            {
                                                if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                                    importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                else
                                                {

                                                    filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                                    foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                                    {
                                                        if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                            importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEstandar);
                                                        else
                                                            importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEspecial);
                                                    }
                                                }
                                            }
                                            oItemTaximetro[COL_DATA_IMPORTE_VENTA] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                        }
                                        dondeVa = "VENTAS: Datos en datatble taximetro";
                                        if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * (Convert.ToDecimal(diasPagoDefecto) / Convert.ToDecimal(365)));
                                        else
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));
                                    }

                                    //else 
                                    //{
                                    //    if (productType.Trim().ToUpper() == "SERVICIO")
                                    //    {
                                    //        dondeVa = "VENTAS: Servicio";
                                    //        // Fenólico
                                    //        if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                    //        {
                                    //            if (calcularConFenolico)
                                    //                importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                    //        }
                                    //        // SVP/CIF
                                    //        else if (Convert.ToDecimal(filaLinea["CANON IMPORTE"]) != 0)
                                    //        {
                                    //            importeFacturacionVentaUneCifCapitulos = importeFacturacionVentaUneCifCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    //            // COSTE
                                    //            importeCosteVentaUneCifCapitulos = importeCosteVentaUneCifCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                    //            oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                    //        }
                                    //        // OTROS SERVICIOS
                                    //        else
                                    //        {
                                    //            importeFacturacionVentaOtrosServiciosCapitulos = importeFacturacionVentaOtrosServiciosCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    //            // COSTE
                                    //            importeCosteVentaOtrosServiciosCapitulos = importeCosteVentaOtrosServiciosCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                    //            oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                    //        }
                                    //        oItemTaximetro[COL_DATA_IMPORTE_VENTA_SERVICIO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                    //    }
                                    //}
                                    //if (productType.Trim().ToUpper() != "SERVICIO")
                                    //{
                                    //    dondeVa = "VENTAS: Producto";
                                    //    importeFacturacionVentaProductoCapitulos = importeFacturacionVentaProductoCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    //    oItemTaximetro[COL_DATA_IMPORTE_VENTA_PRODUCTO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");

                                    //    dondeVa = "Coste venta";
                                    //    importeCosteVentaCapitulos = importeCosteVentaCapitulos + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;

                                    //    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;
                                    //}
                                    //        oItemTaximetro[COL_DATA_IMPORTE_VENTA] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                    //    }
                                    //    dondeVa = "VENTAS: Datos en datatble taximetro";
                                    //    importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));
                                    //}

                                    dondeVa = "Alquiler";
                                    if (filaCapitulo["TIPO"].ToString() == "RENTAL")
                                    {
                                        // SVP/CIF
                                        if (Convert.ToDecimal(filaLinea["CANON IMPORTE"]) != 0)
                                        {
                                            oItemTaximetro[COL_DATA_IMPORTE_VENTA] = ((Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_VENTA].ToString())) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)).ToString("#,##0.00");
                                            importeVenta = importeVenta + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            importeVentaCapitulo = importeVentaCapitulo + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            importeVentaLinea = importeVentaLinea + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            importeFacturacionVentaUneCifCapitulos = importeFacturacionVentaUneCifCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                                importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)) * (Convert.ToDecimal(diasPagoDefecto) / Convert.ToDecimal(365));
                                            else
                                                importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));

                                            //importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));
                                        }
                                        //if (calcularConFenolico && filaLinea["CANON FENOLICO"].ToString().HasValue && filaLinea["CANON FENOLICO"].ToString() != 0)
                                        if (calcularConFenolico)
                                        {
                                            if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                            {
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA] = ((Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_VENTA].ToString())) + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))).ToString("#,##0.00");
                                                importeVenta = importeVenta + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                importeVentaCapitulo = importeVentaCapitulo + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                importeVentaLinea = importeVentaLinea + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                                    importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))) * ((diasPagoDefecto) / Convert.ToDecimal(365));
                                                else
                                                    importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));

                                                //importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));
                                            }
                                            else
                                            {
                                                filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                                foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                                {
                                                    if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                        importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEstandar);
                                                    else
                                                        importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEspecial);
                                                }
                                            }
                                        }
                                        dondeVa = "ALQUILER";
                                        importeAlquiler = importeAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));
                                        importeAlquilerCapitulo = importeAlquilerCapitulo + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));
                                        importeAlquilerLinea = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));
                                        importeFacturacionAlquilerCapitulos = importeFacturacionAlquilerCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));

                                        oItemGastosFijos["FACTURACION ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["FACTURACION ALQUILER"]) + importeAlquilerLinea;

                                        if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * importeAlquilerLinea * ((diasPagoDefecto) / Convert.ToDecimal(365)));
                                        else
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * importeAlquilerLinea * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));

                                        dondeVa = "ALQUILER: Gastos variables";
                                        importeGastosPatioEntregaDevolucionAlquiler = 0;
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                        {
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                            {
                                                importeGastosPatioEntregaDevolucionAlquiler = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio;
                                                importeGastosPatioEntregaDevolucionAlquiler = importeGastosPatioEntregaDevolucionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * naviera * precioGastosPatio);
                                            }
                                            else
                                            {
                                                importeGastosPatioEntregaDevolucionAlquiler = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio;
                                                importeGastosPatioEntregaDevolucionAlquiler = importeGastosPatioEntregaDevolucionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * precioGastosPatio);

                                            }
                                        }
                                        else
                                        {
                                            importeGastosPatioEntregaDevolucionAlquiler = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio;
                                            importeGastosPatioEntregaDevolucionAlquiler = importeGastosPatioEntregaDevolucionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * precioGastosPatio);

                                        }
                                        importeGastosVariablesEntregaDevolucionAlquilerCapitulos = importeGastosVariablesEntregaDevolucionAlquilerCapitulos + importeGastosPatioEntregaDevolucionAlquiler;

                                        importeGastosPatioClasificacionReparacionAlquiler = 0;
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararDirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararIndirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarDirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarIndirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararDirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararIndirecto * precioGastosPatio);
                                        importeGastosVariablesClaisificacionReparacionAlquilerCapitulos = importeGastosVariablesClaisificacionReparacionAlquilerCapitulos + importeGastosPatioClasificacionReparacionAlquiler;

                                        oItemTaximetro[COL_DATA_ALQUILER_ENTREGA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_DEVOLUCION] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * precioGastosPatio).ToString("##0.00");
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                        {
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                            {
                                                oItemTaximetro[COL_DATA_ALQUILER_ENTREGA_NAVIERA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio).ToString("##0.00");
                                                oItemTaximetro[COL_DATA_ALQUILER_DEVOLUCION_NAVIERA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * naviera * precioGastosPatio).ToString("##0.00");
                                            }
                                        }
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_REPARAR_DIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararDirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_REPARAR_INDIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararIndirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_DIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarDirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_INDIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarIndirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_REPARAR_DIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararDirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_REPARAR_INDIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararIndirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_GASTOS_PATIO] = (importeGastosPatioEntregaDevolucionAlquiler + importeGastosPatioClasificacionReparacionAlquiler).ToString("##0.00");

                                        dondeVa = "ALQUILER: Datos en datatble taximetro";
                                        oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()).ToString("#,##0.00");
                                        oItemTaximetro[COL_DATA_EURO_DIA] = Convert.ToDecimal(filaLinea["EURO DIA"].ToString()).ToString("#,##0.0000");
                                        oItemTaximetro[COL_DATA_DURACION_ESTIMADA] = Convert.ToDecimal(filaLinea["DURACION"].ToString()).ToString("#,##0.00");
                                        oItemTaximetro[COL_DATA_IMPORTE_ALQUILER] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString())).ToString("#,##0.00");

                                        if (vocacionChatarra != 2)
                                        {
                                            dondeVa = "Calcular datos taxímetros";
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
                                                        precioTaximetroNoConsumible = precioCoste; //(precioCoste - (costeSuperficie * superficie));
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

                                            dondeVa = "Calcular datos taxímetros: superficie";
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

                                            importeCosteTaximetroConsumible = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(filaLinea["DURACION"].ToString())) / diasxMes;
                                            importeCosteTaximetroConsumibleCapitulos = importeCosteTaximetroConsumibleCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(filaLinea["DURACION"].ToString())) / diasxMes;
                                            oItemGastosFijos["COSTE ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]) + importeCosteTaximetroConsumible;

                                            dondeVa = "Empieza recorrido bonificación";
                                            oItemTaximetro[COL_DATA_AJUSTE] = "";

                                            duracion = Convert.ToInt32(filaLinea["DURACION"].ToString());
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
                                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes).ToString("#,##0.00");
                                                        else
                                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (Convert.ToDecimal(oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes)).ToString("#,##0.00");
                                                        importeCosteBonificacionTaximetroConsumible = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                                        importeCosteBonificacionTaximetroConsumibleCapitulos = importeCosteBonificacionTaximetroConsumibleCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);

                                                        oItemGastosFijos["COSTE ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]) - importeCosteBonificacionTaximetroConsumible;
                                                    }
                                                }
                                                diasCalculados = diasCalculados + diasxMes;
                                            }
                                            dondeVa = "Fin recorrido bonificación";

                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_DESDE] = diasDesdeTaxNoConsumible.ToString();
                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_HASTA] = diasHastaTaxNoConsumible.ToString();
                                            oItemTaximetro[COL_DATA_IMPORTE_TAX] = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(filaLinea["DURACION"].ToString())) / diasxMes).ToString("#,##0.00");

                                            if (Convert.ToInt32(filaLinea["DURACION"].ToString()) <= diasHastaTaxNoConsumible)
                                                diasTaximetroNoConsumibleCalcular = Convert.ToInt32(filaLinea["DURACION"].ToString()) - diasDesdeTaxNoConsumible;
                                            if (Convert.ToInt32(filaLinea["DURACION"].ToString()) >= diasHastaTaxNoConsumible)
                                                diasTaximetroNoConsumibleCalcular = diasHastaTaxNoConsumible - diasDesdeTaxNoConsumible;

                                            importeCosteTaximetroNoConsumible = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                            importeCosteTaximetroNoConsumibleCapitulos = importeCosteTaximetroNoConsumibleCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes).ToString("#,##0.00");

                                            oItemGastosFijos["COSTE ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]) + importeCosteTaximetroNoConsumible;
                                        }
                                    }

                                    oItemGastosFijos["FACTURACION VENTA"] = Convert.ToDecimal(oItemGastosFijos["FACTURACION VENTA"]) + importeVenta;

                                    //if ((importeCosteMaterialNuevo != 0) && (importeCosteMaterialFenolico != 0))
                                    importeCosteVentaNuevoCapitulos = importeCosteVentaNuevoCapitulos + (importeCosteMaterialNuevo + importeCosteMaterialFenolico);
                                    //else
                                    //    importeCosteVentaNuevoCapitulos = importeCosteVentaNuevoCapitulos + importeCosteMaterialNuevo;


                                    pesoCapitulo = pesoCapitulo + (pesoArticulo * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));
                                    pesoTotal = pesoTotal + (pesoArticulo * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));

                                    dondeVa = "Codigos transporte";
                                    if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) >= 0)
                                    {
                                        dondeVa = "VALIDAR TRANSPORTE TR-001";
                                        importePorte_TR001 = importePorte_TR001 + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    }

                                    importePorte = importePorte + (valorCostePorte * Convert.ToDecimal(kilometros) * (pesoArticulo / 1000) * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));
                                    importeCostePorte = importeCostePorte + (valorCostePorte * Convert.ToDecimal(kilometros) * (pesoArticulo / 1000) * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));

                                    // CALCULO GASTOS FIJOS Y AJUSTES DURACION OBRA POR LINEA
                                    dondeVa = "Gastos fijos";
                                    if (gastosFijosPorFacturacion)
                                    {
                                        BUimporteLinea = (KABU * importeAlquilerLinea);
                                        centralImporteLinea = (KAHQ * importeAlquilerLinea);
                                    }
                                    else
                                    {
                                        costeAlquiler = importeCosteTaximetroConsumible + importeCosteTaximetroNoConsumible - importeCosteBonificacionTaximetroConsumible;
                                        costeAlquiler = (-1) * costeAlquiler;
                                        BUimporteLinea = (KABU * costeAlquiler);
                                        centralImporteLinea = (KAHQ * costeAlquiler);
                                    }

                                    GF = 0;
                                    if (ajustesAlcance == "GF BU")
                                        GF = BUimporteLinea;
                                    else
                                        GF = BUimporteLinea + centralImporteLinea;

                                    dias = Convert.ToInt32(filaLinea["DURACION"].ToString());
                                    if (dias < 0)
                                        porcentajeAjustesObraDuracionGFCapitulo = 0;
                                    else if (dias == 0)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPocentajeCargoMaximo;
                                    else if (dias == ajustesValorObraCorta)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPorcentajeCorreccionObraCorta;
                                    else if (dias == ajustesValorObraLarga)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPorcentajeCorreccionObraLarga;
                                    else if (dias > ajustesValorMaximo)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPorcentajeCorreccionMaximo;
                                    else
                                        porcentajeAjustesObraDuracionGFCapitulo = funcionalidades.interpolacion(dtAjustesDuracionGF, dias);

                                    importeAjustesObraDuracionGFCapitulo = importeAjustesObraDuracionGFCapitulo + (GF * porcentajeAjustesObraDuracionGFCapitulo);

                                    // CALCULO AJUSTES TAXIMETRO DURACION
                                    dondeVa = "Ajustes tax duracion";
                                    duracionAjuste = 0;
                                    if (Convert.ToInt32(filaLinea["DURACION"].ToString()) < ajustesTaxDuracionMinima)
                                        duracionAjuste = 0;
                                    else
                                    {
                                        if (Convert.ToInt32(filaLinea["DURACION"].ToString()) > ajustesTaxDuracionMaxima)
                                            duracionAjuste = ajustesTaxDuracionMaxima;
                                        else
                                            duracionAjuste = Convert.ToInt32(filaLinea["DURACION"].ToString());
                                        duracionAjuste = duracionAjuste - ajustesTaxDuracionMinima;
                                        if (diasxMes != 0)
                                            duracionAjuste = duracionAjuste / diasxMes;
                                        else
                                            duracionAjuste = 0;
                                        importeAjustesObraDuracionTaxCapitulo = importeAjustesObraDuracionTaxCapitulo + (duracionAjuste * Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_NO_TAX]) * ajustesTaxPorcentajeAbonoMensual);
                                        oItemTaximetro["% DURACION AJUSTE"] = ajustesTaxPorcentajeAbonoMensual;
                                        oItemTaximetro["DURACION AJUSTE"] = duracionAjuste;
                                        oItemTaximetro["AJUSTE OBRA DURACION"] = (duracionAjuste * Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_NO_TAX]) * ajustesTaxPorcentajeAbonoMensual);
                                    }

                                    // CALCULO AJUSTES TAXIMETRO OCUPACION
                                    dondeVa = "Ajustes tax ocupacion";
                                    decimal ajusteOcupacion = 0;
                                    if (dtOcupacion.Rows.Count > 0)
                                    {
                                        aafBuscar = "";
                                        aafBuscar = filaLinea["ITEM"].ToString();
                                        // Buscar el AAF
                                        filaEncontrada = dtGastosPatio.Select("ProductId = '" + filaLinea["ITEM"].ToString() + "'");
                                        foreach (DataRow fila in filaEncontrada)
                                        {
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["AAF"])))
                                                aafBuscar = Convert.ToString(fila["AAF"]);
                                        }
                                        //filasOcupacion = dtOcupacion.Select("Articulo = '" + filaLinea["ITEM"].ToString() + "'");
                                        filasOcupacion = dtOcupacion.Select("Articulo = '" + aafBuscar + "'");
                                        foreach (DataRow filaOcupacion in filasOcupacion)
                                        {
                                            ocupacionAjuste = Convert.ToDecimal(filaOcupacion["PorcentajeAplicar"].ToString());
                                            if (filaOcupacion["Alcance"].ToString().ToUpper() == "TODOS")
                                            {
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["TAX NO CONSUMIBLE"]) / 100);
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["IMPORTE TAX CONSUMIBLE"]) / 100);
                                            }
                                            else if (filaOcupacion["Alcance"].ToString().ToUpper() == "CONSUMIBLE")
                                            {
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["IMPORTE TAX CONSUMIBLE"]) / 100);
                                            }
                                            else if (filaOcupacion["Alcance"].ToString().ToUpper() == "NO CONSUMIBLE")
                                            {
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["TAX NO CONSUMIBLE"]) / 100);
                                            }
                                            importeAjustesProductoTaxCapitulo = importeAjustesProductoTaxCapitulo + ajusteOcupacion;
                                            oItemTaximetro["% OCUPACION AJUSTE"] = ocupacionAjuste;
                                            oItemTaximetro["AJUSTE PRODUCTO"] = ajusteOcupacion;
                                            oItemTaximetro["AJUSTE PRODUCTO SOBRE"] = filaOcupacion["Alcance"].ToString().ToUpper();
                                        }
                                    }
                                    dtTaximetro.Rows.Add(oItemTaximetro);
                                    oItemTaximetro = null;

                                    // GASTOS FIJOS x ARTICULO
                                    dondeVa = "gastos fijos x articulo";
                                    oItemGastosFijos["ITEM"] = filaLinea["ITEM"].ToString();
                                    oItemGastosFijos["COSTE ALQUILER"] = (-1) * Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]);
                                    oItemGastosFijos["COSTE VENTA"] = (-1) * Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]);
                                    if (gastosFijosPorFacturacion)
                                    {
                                        oItemGastosFijos["TOTAL BU"] = (Convert.ToDecimal(oItemGastosFijos["KABU"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION VENTA"]));
                                        oItemGastosFijos["TOTAL CENTRAL"] = (Convert.ToDecimal(oItemGastosFijos["KAHQ"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION VENTA"]));
                                    }
                                    else
                                    {
                                        oItemGastosFijos["TOTAL BU"] = (Convert.ToDecimal(oItemGastosFijos["KABU"]) * Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]));
                                        oItemGastosFijos["TOTAL CENTRAL"] = (Convert.ToDecimal(oItemGastosFijos["KAHQ"]) * Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]));
                                    }
                                    oItemGastosFijos["BONIFICACION BU CENTRAL"] = 0;
                                    oItemGastosFijos["BONIFICACION BU"] = 0;

                                    // BONIFICACION BU
                                    dondeVa = "Bonificacion BU";
                                    decimal real = 0;
                                    decimal presupuesto = 0;
                                    //bonificacionBU = 0;
                                    filasOcupacion = dtAjustesDatosBU.Select("Articulo = '" + filaLinea["ITEM"].ToString() + "'");
                                    foreach (DataRow filaOcupacion in filasOcupacion)
                                    {
                                        if (filaOcupacion["Escenario"].ToString() == "0")
                                            real = Convert.ToDecimal(filaOcupacion["Facturacion"].ToString());
                                        if (filaOcupacion["Escenario"].ToString() == "1")
                                            presupuesto = Convert.ToDecimal(filaOcupacion["Facturacion"].ToString());
                                    }
                                    if (presupuesto != 0)
                                    {
                                        if (Math.Abs(real / presupuesto) < cumplimientoBajoPorcentaje)
                                        {
                                            if (cumplimientoBajoAfectaGFCentral.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU CENTRAL"] = Convert.ToDecimal(oItemGastosFijos["TOTAL CENTRAL"]) * (bonificacionCumplimientoBajoPorcentaje); // / 100;
                                            if (cumplimientoBajoAfectaGFBU.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU"] = Convert.ToDecimal(oItemGastosFijos["TOTAL BU"]) * (bonificacionCumplimientoBajoPorcentaje); // / 100;
                                        }
                                        if (Math.Abs(real / presupuesto) > cumplimientoAltoPorcentaje)
                                        {
                                            if (cumplimientoAltoAfectaGFCentral.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU CENTRAL"] = Convert.ToDecimal(oItemGastosFijos["TOTAL CENTRAL"]) * (bonificacionCumplimientoAltoPorcentaje); // / 100;
                                            if (cumplimientoAltoAfectaGFBU.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU"] = Convert.ToDecimal(oItemGastosFijos["TOTAL BU"]) * (bonificacionCumplimientoAltoPorcentaje); // / 100;
                                        }
                                    }
                                    bonificacionBU = bonificacionBU + Convert.ToDecimal(oItemGastosFijos["BONIFICACION BU CENTRAL"]) + Convert.ToDecimal(oItemGastosFijos["BONIFICACION BU"]);

                                    dtGastosFijos.Rows.Add(oItemGastosFijos);
                                    oItemGastosFijos = null;
                                }

                                dondeVa = "FIN RECORRIDO CAPÍTULO";

                                // INICIO PORTES POR CAPÍTULO

                                dondeVa = "Portes";
                                if (calcularConPortes && filaCabecera["DELEGACION"].ToString().ToUpper() != "EX" && Convert.ToInt32(filaCabecera["GESTION PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_GestionPorte.Alsina.GetHashCode()) && (chkBoxSoloEntrega.Checked || chkBoxSoloDevolucion.Checked))
                                {
                                    if (existeTR001 && importePorte_TR001 > 0)
                                    {
                                        // se calcula solo un tramo del transporte durante el proceso, si es solo entrega o solo devolución es lo que tenemos, si es entrega+devolucion doblamos ese importe
                                        if (!chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                        {
                                            importePorte_TR001 = (importePorte_TR001 * coeficienteTransporte);
                                            importeCostePorte = (importeCostePorte * coeficienteTransporte);
                                        }
                                        if (chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                        {
                                            importePorte_TR001 = importePorte_TR001 + (importePorte_TR001 * coeficienteTransporte);
                                            importeCostePorte = importeCostePorte + (importeCostePorte * coeficienteTransporte);
                                        }

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "FACTURACION_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = importePorte_TR001.ToString("#,##0.00");
                                        filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        importeCostePorte = (-1) * (importePorte_TR001 * (1 - margenPorte));
                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "COSTE_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = importeCostePorte.ToString("#,##0.00");
                                        if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte_TR001 != 0)
                                            filaValores["PORCENTAJE"] = (importeCostePorte / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte_TR001) * 100).ToString("#,##0.00");
                                        else
                                            filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "MARGEN_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = (importePorte_TR001 + importeCostePorte).ToString("#,##0.00");
                                        if (((importePorte_TR001) / (1 - margenPorte)) == 0)
                                            filaValores["PORCENTAJE"] = "0.00";
                                        else
                                            filaValores["PORCENTAJE"] = (((importePorte_TR001 + importeCostePorte) / importePorte_TR001) * 100).ToString("#,##0.00");
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        importePorte = importePorte_TR001;
                                    }

                                    if (!existeTR001)
                                    {
                                        // se calcula solo un tramo del transporte durante el proceso, si es solo entrega o solo devolución es lo que tenemos, si es entrega+devolucion doblamos ese importe
                                        if (!chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                            importePorte = (importePorte * coeficienteTransporte);
                                        if (chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                            importePorte = importePorte + (importePorte * coeficienteTransporte);

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "FACTURACION_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        if (1 - margenPorte != 0)
                                            filaValores["IMPORTE"] = ((importePorte * 100) / (1 - margenPorte)).ToString("#,##0.00");
                                        else
                                            filaValores["IMPORTE"] = "0.00";
                                        filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "COSTE_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = ((-1) * importePorte * 100).ToString("#,##0.00");
                                        if (importeAlquilerCapitulo + importeVentaCapitulo + (importePorte * 100) / (1 - margenPorte) != 0)
                                            filaValores["PORCENTAJE"] = ((((-1) * importePorte * 100) / (importeAlquilerCapitulo + importeVentaCapitulo + (importePorte * 100) / (1 - margenPorte))) * 100).ToString("#,##0.00");
                                        //(((-1) * importePorte * 100) / (importeAlquiler + importeVenta + ((importePorte * 100) / (1 - margenPorte))) * 100).ToString("#,##0.00");
                                        else
                                            filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "MARGEN_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = (((importePorte * 100) / (1 - margenPorte)) - (importePorte * 100)).ToString("#,##0.00");
                                        if (((importePorte * 100) / (1 - margenPorte)) == 0)
                                            filaValores["PORCENTAJE"] = "0.00";
                                        else
                                            filaValores["PORCENTAJE"] = ((((importePorte * 100) / (1 - margenPorte)) - (importePorte * 100)) / ((importePorte * 100) / (1 - margenPorte)) * 100).ToString("#,##0.00");
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        importePorte = (importePorte * 100) / (1 - margenPorte);
                                    }
                                }
                                else
                                {
                                    importePorte = 0;
                                    importePorte_TR001 = 0;
                                }
                                // FIN PORTES POR CAPÍTULO

                                // GASTOS FIJOS
                                dondeVa = "Gastos fijos";
                                if (gastosFijosPorFacturacion)
                                {
                                    totalGastosFijosBU = (KABU * importeAlquilerCapitulo) + (KVBU * importeVentaCapitulo);
                                    totalGastosFijosCentral = (KAHQ * importeAlquilerCapitulo) + (KVHQ * importeVentaCapitulo);
                                }
                                else
                                {
                                    if (calcularConPortes)
                                        costeVentas = importeCostePorte + importeCosteVentaDepartamentoTecnicoCapitulos + importeCosteVentaFenolicoNuevoCapitulos + importeCosteVentaUneCifCapitulos + importeCosteVentaOtrosServiciosCapitulos + importeCosteVentaMontajesCapitulos + importeCosteVentaCapitulos;
                                    else
                                        costeVentas = importeCosteVentaDepartamentoTecnicoCapitulos + importeCosteVentaFenolicoNuevoCapitulos + importeCosteVentaUneCifCapitulos + importeCosteVentaOtrosServiciosCapitulos + importeCosteVentaMontajesCapitulos + importeCosteVentaCapitulos; ;
                                    costeVentas = (-1) * costeVentas;
                                    costeAlquiler = importeCosteTaximetroNoConsumibleCapitulos + importeCosteTaximetroConsumibleCapitulos - importeCosteBonificacionTaximetroConsumibleCapitulos;
                                    costeAlquiler = (-1) * costeAlquiler;
                                    totalGastosFijosBU = (KABU * costeAlquiler) + (KVBU * costeVentas);
                                    totalGastosFijosCentral = (KAHQ * costeAlquiler) + (KVHQ * costeVentas);
                                }
                                if (totalGastosFijosBU != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_BU";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = totalGastosFijosBU.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (totalGastosFijosBU / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (totalGastosFijosCentral != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_CENTRAL";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = totalGastosFijosCentral.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (totalGastosFijosCentral / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES OBRA DURACION GASTOS FIJOS
                                dondeVa = "Ajustes duracion gastos fijos";
                                if (importeAjustesObraDuracionGFCapitulo != 0)
                                {
                                    importeAjustesObraDuracionGFCapitulo = Math.Abs(importeAjustesObraDuracionGFCapitulo);
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_OBRA_DURACION_GF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeAjustesObraDuracionGFCapitulo).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeAjustesObraDuracionGFCapitulo / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES OBRA DURACION TAX
                                if (importeAjustesObraDuracionTaxCapitulo != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_OBRA_DURACION_TAXIMETRO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeAjustesObraDuracionTaxCapitulo.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeAjustesObraDuracionTaxCapitulo / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES PRODUCTOS
                                if (importeAjustesProductoTaxCapitulo != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_TAX_PRODUCTO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeAjustesProductoTaxCapitulo).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = Math.Abs((importeAjustesProductoTaxCapitulo / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100)).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES BU
                                if (bonificacionBU != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_BU";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * (bonificacionBU)).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (((-1) * (bonificacionBU)) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // GASTOS FINANCIACION
                                decimal portes = dtValores.AsEnumerable()
                                               .Where(y => y.Field<string>("ETIQUETA") == "FACTURACION_PORTES" && y.Field<string>("CONCEPTO") == tituloCapitulo)
                                               .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                                //decimal totalCapitulo = importeFacturacionAlquilerCapitulos + importeFacturacionVentaProductoCapitulos + importeFacturacionVentaMontajesCapitulos + importeFacturacionVentaDepartamentoTecnicoCapitulos + importeFacturacionVentaFenolicoNuevoCapitulos + importeFacturacionVentaUneCifCapitulos + importeFacturacionVentaOtrosServiciosCapitulos + portes;
                                decimal totalCapitulo = importeFacturacionAlquilerCapitulos + importeFacturacionVentaMontajesCapitulos + importeFacturacionVentaDepartamentoTecnicoCapitulos + importeFacturacionVentaFenolicoNuevoCapitulos + importeFacturacionVentaUneCifCapitulos + importeFacturacionVentaOtrosServiciosCapitulos + portes;
                                if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                    importeGastosFinanciacionCapitulos = (condicionesAplicar * totalCapitulo * ((diasPagoDefecto) / Convert.ToDecimal(365)));
                                else
                                    importeGastosFinanciacionCapitulos = (condicionesAplicar * totalCapitulo * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));
                                if (importeGastosFinanciacionCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FINANCIACION";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFinanciacionCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFinanciacionCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // GASTOS FALLIDOS
                                importeFallidosCapitulos = (totalCapitulo) * (porcentajeCobrados / 100) * (porcentajeFallidos / 100);
                                if (importeFallidosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FALLIDOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeFallidosCapitulos).ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosVariablesEntregaDevolucionAlquilerCapitulos != 0 || importeGastosVariablesEntregaDevolucionVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 6";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLES_ENTREGA_DEVOLUCION";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * (importeGastosVariablesEntregaDevolucionAlquilerCapitulos + importeGastosVariablesEntregaDevolucionVentaCapitulos)).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * (importeGastosVariablesEntregaDevolucionAlquilerCapitulos + importeGastosVariablesEntregaDevolucionVentaCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosVariablesClaisificacionReparacionAlquilerCapitulos != 0 || importeGastosVariablesClaisificacionReparacionVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 6";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLES_CLASIFICACION_REPARACION";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * (importeGastosVariablesClaisificacionReparacionAlquilerCapitulos + importeGastosVariablesClaisificacionReparacionVentaCapitulos)).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * (importeGastosVariablesClaisificacionReparacionAlquilerCapitulos + importeGastosVariablesClaisificacionReparacionVentaCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                dondeVa = "Importes/cantidades venta 1";
                                if (importeFacturacionVentaDepartamentoTecnicoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "DEPARTAMENTO_TECNICO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaDepartamentoTecnicoCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                dondeVa = "Importes/dec_importeCosteVCH ";
                                if (dec_importeCosteVCH != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_VCH";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * dec_importeCosteVCH).ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaDepartamentoTecnicoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_DEPARTAMENTO_TECNICO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaDepartamentoTecnicoCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaDepartamentoTecnicoCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaDepartamentoTecnicoCapitulos != 0 || importeCosteVentaDepartamentoTecnicoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_DEPARTAMENTO_TECNICO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaDepartamentoTecnicoCapitulos - importeCosteVentaDepartamentoTecnicoCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaDepartamentoTecnicoCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaDepartamentoTecnicoCapitulos - importeCosteVentaDepartamentoTecnicoCapitulos) / importeFacturacionVentaDepartamentoTecnicoCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaFenolicoNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "FENOLICO_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaFenolicoNuevoCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_FENOLICO_NUEVO_MATERIAL_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaNuevoCapitulos).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaFenolicoNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_FENOLICO_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaFenolicoNuevoCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaFenolicoNuevoCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaFenolicoNuevoCapitulos) / importeFacturacionVentaFenolicoNuevoCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }


                                if (importeFacturacionVentaFenolicoNuevoCapitulos != 0 || importeGastosVariablesVentaFenolicoNuevoCapitulos != 0 || importeGastosFijosBUVentaFenolicoNuevoCapitulos != 0 || importeGastosFijosCentralesVentaFenolicoNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ROP_BASICO_FENOLICO_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaFenolicoNuevoCapitulos - importeGastosVariablesVentaFenolicoNuevoCapitulos - importeGastosFijosBUVentaFenolicoNuevoCapitulos - importeGastosFijosCentralesVentaFenolicoNuevoCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((importeFacturacionVentaFenolicoNuevoCapitulos - importeGastosVariablesVentaFenolicoNuevoCapitulos - importeGastosFijosBUVentaFenolicoNuevoCapitulos - importeGastosFijosCentralesVentaFenolicoNuevoCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "SVP_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaUneCifCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_SVP_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaUneCifCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;

                                }

                                if (importeFacturacionVentaUneCifCapitulos != 0 || importeCosteVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_SVP_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaUneCifCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos) / importeFacturacionVentaUneCifCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaOtrosServiciosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "OTROS_SERVICIOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaOtrosServiciosCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaOtrosServiciosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_OTROS_SERVICIOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaOtrosServiciosCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaOtrosServiciosCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaOtrosServiciosCapitulos != 0 || importeCosteVentaOtrosServiciosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_OTROS_SERVICIOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaOtrosServiciosCapitulos - importeCosteVentaOtrosServiciosCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaOtrosServiciosCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaOtrosServiciosCapitulos - importeCosteVentaOtrosServiciosCapitulos) / importeFacturacionVentaOtrosServiciosCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosBUVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_BU_UNE_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosBUVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosBUVentaUneCifCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosCentralesVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_CENTRALES_UNE_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosCentralesVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosCentralesVentaUneCifCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaUneCifCapitulos != 0 || importeCosteVentaUneCifCapitulos != 0 || importeGastosVariablesVentaUneCifCapitulos != 0 || importeGastosFijosBUVentaUneCifCapitulos != 0 || importeGastosFijosCentralesVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ROP_BASICO_UNE_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos - importeGastosVariablesVentaUneCifCapitulos - importeGastosFijosBUVentaUneCifCapitulos - importeGastosFijosCentralesVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos - importeGastosVariablesVentaUneCifCapitulos - importeGastosFijosBUVentaUneCifCapitulos - importeGastosFijosCentralesVentaUneCifCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaMontajesCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaMontajesCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaMontajesCapitulos != 0 || importeCosteVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaMontajesCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos) / importeFacturacionVentaMontajesCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosBUVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_BU_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosBUVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosBUVentaMontajesCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosCentralesVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_CENTRALES_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosCentralesVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosCentralesVentaMontajesCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaMontajesCapitulos != 0 || importeCosteVentaMontajesCapitulos != 0 || importeGastosVariablesVentaMontajesCapitulos != 0 || importeGastosFijosBUVentaMontajesCapitulos != 0 || importeGastosFijosCentralesVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ROP_BASICO_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos - importeGastosVariablesVentaMontajesCapitulos - importeGastosFijosBUVentaMontajesCapitulos - importeGastosFijosCentralesVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos - importeGastosVariablesVentaMontajesCapitulos - importeGastosFijosBUVentaMontajesCapitulos - importeGastosFijosCentralesVentaMontajesCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaProductoCapitulos != 0)
                                {

                                    dondeVa = "Importes/cantidades venta productos 1";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "VENTAS_DIRECTAS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaProductoCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;

                                }

                                if (importeCosteVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades venta 2";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_VENTAS_DIRECTAS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaProductoCapitulos != 0 || importeCosteVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades venta 3";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_VENTAS_DIRECTAS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaProductoCapitulos - importeCosteVentaCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaProductoCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaProductoCapitulos - importeCosteVentaCapitulos) / importeFacturacionVentaProductoCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                dondeVa = "Importes/cantidades alquiler 1";
                                if (importeFacturacionAlquilerCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ALQUILERES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionAlquilerCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteTaximetroNoConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 2";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "TAXIMETRO_NO_CONSUMIBLE";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteTaximetroNoConsumibleCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeCosteTaximetroNoConsumibleCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteTaximetroConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 3";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "TAXIMETRO_CONSUMIBLE";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteTaximetroConsumibleCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteTaximetroConsumibleCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteBonificacionTaximetroConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 4";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTE_TAXIMETRO_CONSUMIBLE";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeCosteBonificacionTaximetroConsumibleCapitulos.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeCosteBonificacionTaximetroConsumibleCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionAlquilerCapitulos != 0 || importeCosteTaximetroNoConsumibleCapitulos != 0 || importeCosteTaximetroConsumibleCapitulos != 0 || importeCosteBonificacionTaximetroConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 5";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_ALQUILER";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionAlquilerCapitulos - importeCosteTaximetroNoConsumibleCapitulos - importeCosteTaximetroConsumibleCapitulos + importeCosteBonificacionTaximetroConsumibleCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionAlquilerCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionAlquilerCapitulos - importeCosteTaximetroNoConsumibleCapitulos - importeCosteTaximetroConsumibleCapitulos + importeCosteBonificacionTaximetroConsumibleCapitulos) / importeFacturacionAlquilerCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }
                                dtDatosConfiguracion.Dispose();
                            }
                        }
                        TimeSpan horaDatosPase2_2 = DateTime.Now.Subtract(horaDatosPase2_1);

                        // CALCULAR GASTOS OFICINA 
                        dondeVa = "Gastos oficina";
                        decimal totalFacturacion = 0;

                        decimal totalAlquiler = 0;
                        decimal totalVenta = 0;
                        decimal totalPortes = 0;
                        decimal costeDTPorratear = 0;
                        decimal totalPortesLinea = 0;
                        string tituloAnterior = "";
                        decimal totalGastosOficinaDToCapitulo = 0;
                        decimal totalGastosOficinaAdministrativoCapitulo = 0;
                        decimal totalGastosOficinaComercialCapitulo = 0;

                        totalAlquiler = dtValores.AsEnumerable()
                                .Where(y => y.Field<string>("ETIQUETA") == "ALQUILERES")
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        totalVenta = dtValores.AsEnumerable()
                               .Where(y => y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS" || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES" || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO" || y.Field<string>("ETIQUETA") == "MONTAJES" || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO" || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO" || y.Field<string>("ETIQUETA") == "SVP_CIF" || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS")
                               .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        totalPortes = dtValores.AsEnumerable()
                              .Where(y => y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                              .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        if (calcularConPortes)
                            total = totalAlquiler + totalVenta + totalPortes;
                        else
                            total = totalAlquiler + totalVenta;

                        if (total * porcentajeMaximoTotalFacturacion > costeDT)
                            costeDTPorratear = costeDT;
                        else
                            costeDTPorratear = total * porcentajeMaximoTotalFacturacion;

                        tituloCapitulo = "";
                        foreach (DataRow fila in dtTaximetro.Rows)
                        {
                            if (fila["CAPITULO"].ToString() != tituloAnterior)
                            {
                                // importe facturación capitulo
                                totalFacturacion = dtValores.AsEnumerable()
                                    .Where(y => y.Field<string>("CONCEPTO") == tituloAnterior && (
                                                        y.Field<string>("ETIQUETA") == "ALQUILERES"
                                                        || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                                        || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                        || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                                        || y.Field<string>("ETIQUETA") == "MONTAJES"
                                                        || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                                        || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                                        || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                                        || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                                        || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                                        )
                                    .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                                if (totalGastosOficinaAdministrativoCapitulo != 0 || totalGastosOficinaComercialCapitulo != 0)
                                {
                                    dondeVa = "Gastos Oficina Administrativo y Comercial";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLES_ADMINISTRATIVO_COMERCIAL";
                                    filaValores["CONCEPTO"] = tituloAnterior;
                                    filaValores["IMPORTE"] = (totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo).ToString("#,##0.00");
                                    if (totalFacturacion != 0)
                                        filaValores["PORCENTAJE"] = ((totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo) / totalFacturacion * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (totalGastosOficinaDToCapitulo != 0)
                                {
                                    dondeVa = "Gastos Oficina Tecnico";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLIES_TECNICO";
                                    filaValores["CONCEPTO"] = tituloAnterior;
                                    filaValores["IMPORTE"] = ((-1) * totalGastosOficinaDToCapitulo).ToString("#,##0.00");
                                    if (totalFacturacion != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * totalGastosOficinaDToCapitulo / totalFacturacion * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                totalGastosOficinaAdministrativoCapitulo = 0;
                                totalGastosOficinaComercialCapitulo = 0;
                                totalGastosOficinaDToCapitulo = 0;
                                tituloAnterior = fila["CAPITULO"].ToString();
                            }

                            dondeVa = "dtGastosOficina";
                            DataRow oItemGastosOficina = dtGastosOficina.NewRow();
                            oItemGastosOficina["CAPITULO"] = fila["CAPITULO"].ToString();
                            oItemGastosOficina["ITEM"] = fila["ITEM"].ToString();
                            oItemGastosOficina["MES PRESUPUESTO VALIDO"] = mesPresupuestoValido.ToString("#,##0");
                            oItemGastosOficina["PORCENTAJE GASTO ADMINISTRATIVO"] = porcentajeGastoAdmministrativoVariable.ToString("#,##0.00");
                            oItemGastosOficina["PORCENTAJE GASTO COMERCIAL"] = porcentajeGastoComercialVariable.ToString("#,##0.00");
                            oItemGastosOficina["PORCENTAJE MAXIMO TOTAL FACTURACON"] = porcentajeMaximoTotalFacturacion.ToString("#,##0.00");
                            oItemGastosOficina["TOTAL ANUAL"] = totalAnual.ToString("#,##0.00");
                            oItemGastosOficina["TOTAL ADMINISTRATIVO"] = totalAdministrativo.ToString("#,##0.00");
                            oItemGastosOficina["VAR ADMINISTRATIVO"] = varAdministrativo.ToString("#,##0.00");
                            oItemGastosOficina["TOTAL COMERCIAL"] = totalComercial.ToString("#,##0.00");
                            oItemGastosOficina["VAR COMERCIAL"] = varComercial.ToString("#,##0.00");
                            oItemGastosOficina["IMPORTE"] = "0.00";

                            oItemGastosOficina["TOTAL FACTURACION"] = total.ToString("#,##0.00");
                            oItemGastosOficina["MAX FACTURACION"] = (total * porcentajeMaximoTotalFacturacion).ToString("#,##0.00");
                            oItemGastosOficina["COSTE DT"] = costeDT.ToString("#,##0.00");

                            if (calcularConPortes)
                            {
                                if (totalAlquiler + totalVenta != 0)
                                    totalPortesLinea = ((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"])) * totalPortes) / (totalAlquiler + totalVenta);
                                else
                                    totalPortesLinea = 0;
                            }

                            // ADMINISTRACION
                            dondeVa = "ADMINISTRACION";
                            if (totalAnual != 0)
                            {
                                if (calcularConPortes)
                                {
                                    // gastos oficina venta
                                    totalGastosOficinaAdministrativoCapitulo = totalGastosOficinaAdministrativoCapitulo + ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual);
                                    totalGastosOficinaComercialCapitulo = totalGastosOficinaComercialCapitulo + ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual);
                                    oItemGastosOficina["GASTOS ADMINISTRATIVO"] = ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS COMERCIAL"] = ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual).ToString("#,##0.00");
                                }
                                else
                                {
                                    // gastos oficina venta
                                    totalGastosOficinaAdministrativoCapitulo = totalGastosOficinaAdministrativoCapitulo + ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual);
                                    totalGastosOficinaComercialCapitulo = totalGastosOficinaComercialCapitulo + ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual);
                                    oItemGastosOficina["GASTOS ADMINISTRATIVO"] = ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS COMERCIAL"] = ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual).ToString("#,##0.00");
                                }
                            }

                            // DT
                            dondeVa = "DT";
                            if (total != 0)
                            {
                                if (calcularConPortes)
                                {
                                    oItemGastosOficina["IMPORTE"] = (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS DT"] = (((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea) * costeDTPorratear) / total).ToString("#,##0.00");
                                    totalGastosOficinaDToCapitulo = totalGastosOficinaDToCapitulo + (((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea) * costeDTPorratear) / total);
                                }
                                else
                                {
                                    oItemGastosOficina["IMPORTE"] = (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"])).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS DT"] = ((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) * costeDTPorratear) / total).ToString("#,##0.00");
                                    totalGastosOficinaDToCapitulo = totalGastosOficinaDToCapitulo + ((Convert.ToDecimal(oItemGastosOficina["IMPORTE"]) * costeDTPorratear) / total);
                                }
                            }
                            dtGastosOficina.Rows.Add(oItemGastosOficina);
                            oItemGastosOficina = null;
                        }

                        if (tituloAnterior != "")
                        {
                            // importe facturación capitulo
                            totalFacturacion = dtValores.AsEnumerable()
                                .Where(y => y.Field<string>("CONCEPTO") == tituloAnterior && (
                                                    y.Field<string>("ETIQUETA") == "ALQUILERES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                                    || y.Field<string>("ETIQUETA") == "MONTAJES"
                                                    || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                                    || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                                    || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                                    || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                                    )
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));


                            dondeVa = "Gastos Oficina Administrativo y Comercial";
                            if (totalGastosOficinaAdministrativoCapitulo != 0 || totalGastosOficinaComercialCapitulo != 0)
                            {
                                dondeVa = "Gastos Oficina Administrativo y Comercial";
                                filaValores = dtValores.NewRow();
                                filaValores["ETIQUETA"] = "GASTOS_VARIABLES_ADMINISTRATIVO_COMERCIAL";
                                filaValores["CONCEPTO"] = tituloAnterior;
                                filaValores["IMPORTE"] = (totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo).ToString("#,##0.00");
                                if (totalFacturacion != 0)
                                    filaValores["PORCENTAJE"] = ((totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo) / totalFacturacion * 100).ToString("#,##0.00");
                                else
                                    filaValores["PORCENTAJE"] = "0.00";
                                dtValores.Rows.Add(filaValores);
                                filaValores = null;
                            }

                            if (totalGastosOficinaDToCapitulo != 0)
                            {
                                dondeVa = "Gastos Oficina Tecnico";
                                filaValores = dtValores.NewRow();
                                filaValores["ETIQUETA"] = "GASTOS_VARIABLIES_TECNICO";
                                filaValores["CONCEPTO"] = tituloAnterior;
                                filaValores["IMPORTE"] = ((-1) * totalGastosOficinaDToCapitulo).ToString("#,##0.00");
                                if (totalFacturacion != 0)
                                    filaValores["PORCENTAJE"] = ((-1) * totalGastosOficinaDToCapitulo / totalFacturacion * 100).ToString("#,##0.00");
                                else
                                    filaValores["PORCENTAJE"] = "0.00";
                                dtValores.Rows.Add(filaValores);
                                filaValores = null;
                            }
                        }
                        dtArticulos.Dispose();

                        // ROP BASICO // ROP
                        dondeVa = "ROP BASICO";
                        tituloCapitulo = "";
                        tituloCapituloAsterisco = "";
                        foreach (DataRow filaCapitulo in filasCapitulo)
                        {
                            tituloCapitulo = filaCapitulo["CAPITULO"].ToString().Trim();
                            tituloCapituloAsterisco = "* " + filaCapitulo["CAPITULO"].ToString().Trim();

                            // importe facturación capitulo
                            totalFacturacion = dtValores.AsEnumerable()
                                .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                                    y.Field<string>("ETIQUETA") == "ALQUILERES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                                    || y.Field<string>("ETIQUETA") == "MONTAJES"
                                                    || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                                    || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                                    || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                                    || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                                    )
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                            // ROP BASICO
                            total = dtValores.AsEnumerable()
                                    .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                                    y.Field<string>("ETIQUETA") == "MARGEN_ALQUILER"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_VENTAS_DIRECTAS"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_MONTAJES"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_DEPARTAMENTO_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_FENOLICO_NUEVO"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_SVP_CIF"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_OTROS_SERVICIOS"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_PORTES"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLES_ENTREGA_DEVOLUCION"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLES_CLASIFICACION_REPARACION"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLES_ADMINISTRATIVO_COMERCIAL"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLIES_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FINANCIACION"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FALLIDOS"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FIJOS_BU"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FIJOS_CENTRAL")
                                    )
                                    .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                            filaValores = dtValores.NewRow();
                            filaValores["ETIQUETA"] = "ROP_BASICO";
                            filaValores["CONCEPTO"] = tituloCapitulo;
                            filaValores["IMPORTE"] = total.ToString("#,##0.00");
                            if (totalFacturacion != 0)
                                filaValores["PORCENTAJE"] = (total / totalFacturacion * 100).ToString("#,##0.00");
                            else
                                filaValores["PORCENTAJE"] = "0.00";
                            dtValores.Rows.Add(filaValores);
                            filaValores = null;

                            //// ROP
                            //total = dtValores.AsEnumerable()
                            //         .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                            //                        y.Field<string>("ETIQUETA") == "ROP_BASICO"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_TAXIMETRO"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_GF"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_TAX_PRODUCTO"
                            //                        || y.Field<string>("ETIQUETA") == "COSTE_FENOLICO_NUEVO_MATERIAL_NUEVO"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_BU")
                            //        )
                            //        .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                            //filaValores = dtValores.NewRow();
                            //filaValores["ETIQUETA"] = "ROP";
                            //filaValores["CONCEPTO"] = tituloCapitulo;
                            //filaValores["IMPORTE"] = total.ToString("#,##0.00");
                            //if (totalFacturacion != 0)
                            //    filaValores["PORCENTAJE"] =(total / totalFacturacion * 100).ToString("#,##0.00");
                            //else
                            //    filaValores["PORCENTAJE"] = "0.00";
                            //dtValores.Rows.Add(filaValores);
                            //filaValores = null;
                        }
                    }

                    dondeVa = "finalizó";
                    //proxy_1.Close();
                    horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);

                    listaArticulosSinFicha.Clear();
                    listaArticulosPedido.Clear();
                    listaFechasPedido.Clear();
                    listaOfertas.Clear();
                    listaFechasOferta.Clear();

                    hora_2 = DateTime.Now.Subtract(hora_1);

                    dondeVa = "nombre del doc";
                    string nombreInforme = Server.MapPath("~/Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text.Replace("/", "_") + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                    dondeVa = "nuevo doc";
                    SLDocument sl = new SLDocument();
                    dondeVa = "importar tabla";
                    sl.ImportDataTable(1, 1, dtTaximetro, true);
                    dondeVa = "salvar tabla";
                    sl.SaveAs(nombreInforme);
                    btnAbrirExcel.Visible = true;

                    dondeVa = "pintar datos";
                    hora_1 = DateTime.Now;
                    pintarArbolNUEVO(ref dtValores, false, false, false);
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO pintar árbol";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    // TOPES AJUSTES
                    dondeVa = "ajustes";
                    DataTable dtTopesValores = new DataTable();
                    dtTopesValores.Clear();
                    dtTopesValores.Columns.Add("APLICA SOBRE", typeof(string));
                    dtTopesValores.Columns.Add("IMPORTE APLICA SOBRE", typeof(decimal));
                    dtTopesValores.Columns.Add("PORCENTAJE CONFIGURADO", typeof(decimal));
                    dtTopesValores.Columns.Add("TOPE", typeof(decimal));
                    dtTopesValores.Columns.Add("ETIQUETA", typeof(string));
                    dtTopesValores.Columns.Add("IMPORTE ETIQUETA", typeof(decimal));
                    dtTopesValores.Columns.Add("IMPORTE TOTAL ETIQUETAS", typeof(decimal));
                    dtTopesValores.Columns.Add("PORCENTAJE APLICAR", typeof(decimal));
                    dtTopesValores.Columns.Add("IMPORTE TOPADO", typeof(decimal));

                    DataTable dtTopes = new DataTable();
                    dtTopes = funcionalidades.topesAjustes(ref dtTiempos, version, masterObra, fechaPrecios, "", usuarioRed);
                    foreach (DataRow fila in dtTopes.Rows)
                    {
                        string concepto = fila["CFGAJU_Concepto"].ToString();
                        string aplicarSobre = fila["CFGAJU_AplicarSobre"].ToString();
                        string aplicar = fila["CFGAJU_AplicarA"].ToString();
                        decimal porcentajeTotal = Convert.ToDecimal(fila["CFGAJU_ValorMaximoPorcentaje"].ToString());

                        total = dtValores.AsEnumerable()
                                    .Where(y => y.Field<string>("ETIQUETA") == "TOTAL_FACTURACION")
                                    .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        decimal sumaTotalesSobre = dtValores.AsEnumerable()
                                   .Where(y => aplicarSobre.Contains(y.Field<string>("ETIQUETA")))
                                   .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        decimal sumaTotalesA = dtValores.AsEnumerable()
                                   .Where(y => aplicar.Contains(y.Field<string>("ETIQUETA")))
                                   .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        decimal tope = Math.Abs(sumaTotalesSobre) * porcentajeTotal / 100;

                        string[] aplica;
                        //if (Math.Abs(tope) < Math.Abs(sumaTotalesA))
                        //{
                        aplica = aplicar.Split('|');
                        while (aplica.GetUpperBound(0) != 0)
                        {
                            aplica = aplicar.Split('|');
                            filaEncontrada = dtValores.Select("ETIQUETA = '" + aplica[0] + "'");
                            foreach (DataRow filaDT in filaEncontrada)
                            {
                                //if (sumaTotalesA > 0)
                                if (Math.Abs(tope) < Math.Abs(sumaTotalesA) && Math.Abs(sumaTotalesA) > 0)
                                {
                                    filaValores = dtTopesValores.NewRow();
                                    filaValores["APLICA SOBRE"] = aplicarSobre;
                                    filaValores["IMPORTE APLICA SOBRE"] = sumaTotalesSobre.ToString("#,##0.00");
                                    filaValores["PORCENTAJE CONFIGURADO"] = porcentajeTotal.ToString("#,##0.0000");
                                    filaValores["TOPE"] = tope.ToString("#,##0.00");
                                    filaValores["ETIQUETA"] = aplica[0];
                                    filaValores["IMPORTE ETIQUETA"] = filaDT["IMPORTE"];
                                    filaValores["IMPORTE TOTAL ETIQUETAS"] = sumaTotalesA.ToString("#,##0.00");

                                    filaDT["CONCEPTO"] = "* " + filaDT["CONCEPTO"].ToString().Trim();

                                    decimal porcentajeAplicar = Convert.ToDecimal(filaDT["IMPORTE"].ToString()) * 100 / sumaTotalesA;
                                    filaDT["IMPORTE"] = (tope * porcentajeAplicar / 100).ToString("##0.00");
                                    if (total != 0)
                                        filaDT["PORCENTAJE"] = (Convert.ToDecimal(filaDT["IMPORTE"].ToString()) * 100 / total).ToString("##0.00");
                                    else
                                        filaDT["PORCENTAJE"] = 0.00;
                                    filaDT["COLOR"] = "text_custom";
                                    filaDT.AcceptChanges();

                                    filaValores["PORCENTAJE APLICAR"] = porcentajeAplicar.ToString("#,##0.0000");
                                    filaValores["IMPORTE TOPADO"] = filaDT["IMPORTE"];
                                    dtTopesValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }
                                else
                                {
                                    filaValores = dtTopesValores.NewRow();
                                    filaValores["APLICA SOBRE"] = aplicarSobre;
                                    filaValores["IMPORTE APLICA SOBRE"] = sumaTotalesSobre.ToString("#,##0.00");
                                    filaValores["PORCENTAJE CONFIGURADO"] = porcentajeTotal.ToString("#,##0.0000");
                                    filaValores["TOPE"] = tope.ToString("#,##0.00");
                                    filaValores["ETIQUETA"] = aplica[0];
                                    filaValores["IMPORTE ETIQUETA"] = filaDT["IMPORTE"];
                                    filaValores["IMPORTE TOTAL ETIQUETAS"] = sumaTotalesA.ToString("#,##0.00");
                                    filaValores["PORCENTAJE APLICAR"] = "0.0000";
                                    filaValores["IMPORTE TOPADO"] = "0.00";
                                    dtTopesValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }
                            }
                            aplicar = aplicar.Replace(aplica[0] + "|", "");
                            if (aplica.GetUpperBound(0) == 0)
                                break;
                        }
                    }

                    // ROP
                    dondeVa = "ROP";
                    tituloCapitulo = "";
                    tituloCapituloAsterisco = "";
                    foreach (DataRow filaCapitulo in filasCapitulo)
                    {
                        tituloCapitulo = filaCapitulo["CAPITULO"].ToString().Trim();
                        tituloCapituloAsterisco = "* " + filaCapitulo["CAPITULO"].ToString().Trim();

                        // importe facturación capitulo
                        decimal totalFacturacion = dtValores.AsEnumerable()
                        .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                            y.Field<string>("ETIQUETA") == "ALQUILERES"
                                            || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                            || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                            || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                            || y.Field<string>("ETIQUETA") == "MONTAJES"
                                            || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                            || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                            || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                            || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                            || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                            )
                        .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        // ROP
                        total = dtValores.AsEnumerable()
                                 .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                                y.Field<string>("ETIQUETA") == "ROP_BASICO"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_TAXIMETRO"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_GF"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_TAX_PRODUCTO"
                                                || y.Field<string>("ETIQUETA") == "COSTE_FENOLICO_NUEVO_MATERIAL_NUEVO"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_BU")
                                )
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        filaValores = dtValores.NewRow();
                        filaValores["ETIQUETA"] = "ROP";
                        filaValores["CONCEPTO"] = tituloCapitulo;
                        filaValores["IMPORTE"] = total.ToString("#,##0.00");
                        if (totalFacturacion != 0)
                            filaValores["PORCENTAJE"] = (total / totalFacturacion * 100).ToString("#,##0.00");
                        else
                            filaValores["PORCENTAJE"] = "0.00";
                        dtValores.Rows.Add(filaValores);
                        filaValores = null;
                    }

                    hora_1 = DateTime.Now;
                    pintarArbolNUEVO(ref dtValores, false, false, false);
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO pintar árbol final";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    horaProcesar_2 = DateTime.Now.Subtract(horaProcesar_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO total cálculo";
                    oItemTiempo[1] = horaProcesar_2.Minutes.ToString("00") + ":" + horaProcesar_2.Seconds.ToString("00") + ":" + horaProcesar_2.Milliseconds.ToString("00");
                    dtTiempos.Rows.Add(oItemTiempo);

                    Session["datosFenolicosOriginales"] = false;

                    dataTiempos.Visible = true;
                    dataTax.Visible = true;
                    dataGastosPatio.Visible = true;
                    dataGastosOficina.Visible = true;
                    dataGastosFijos.Visible = true;
                    dataAbonoMPO.Visible = true;
                    dataBuenTrato.Visible = true;
                    //dataChatarra.Visible = true;
                    //dataLiquidacion.Visible = true;
                    //dataDevoluciones.Visible = true;
                    //dataFacturas.Visible = true;
                    dataTiempos.Columns.Clear();
                    dataTax.Columns.Clear();
                    dataGastosPatio.Columns.Clear();
                    dataGastosOficina.Columns.Clear();
                    dataGastosFijos.Columns.Clear();
                    dataAbonoMPO.Columns.Clear();
                    dataBuenTrato.Columns.Clear();
                    //dataChatarra.Columns.Clear();
                    //dataLiquidacion.Columns.Clear();
                    //dataDevoluciones.Columns.Clear();
                    //dataFacturas.Columns.Clear();
                    dataTiempos.DataSource = dtTiempos;
                    dataTax.DataSource = dtValores; // dtFenolicoNuevo; // dtValores; // dtTaximetro;
                    dataGastosPatio.DataSource = dtTaximetro; // dtAjustesBU; // dtGastosPatioCargaDescarga; // dtGastosPatioCalculo; //
                    dataGastosOficina.DataSource = dtDatosGastosFijos; // dtFechasBase; // dtCabeceraOferta; //dtDatosGastosOficina; //
                    dataGastosFijos.DataSource = dtGastosOficina; // dtGastosFijos;// dtDatosGastosFijos; 
                    dataAbonoMPO.DataSource = dtAjustesDatosBU; // dtAbonoMPO;
                    dataBuenTrato.DataSource = dtGastosFijos;
                    //dataLiquidacion.DataSource = dtAjustesBU;
                    //dataDevoluciones.DataSource = dtAjustesDatosBU; // dtAjustesGF; // dtDevoluciones;
                    //dataChatarra.DataSource = dtArticulosPaneles; // dtTopesValores; // dtChatarra;
                    //dataLiquidacion.DataSource = dtVentaLiquidacion;
                    //dataDevoluciones.DataSource = dtDevoluciones;
                    //dataFacturas.DataSource = dtFacturasMO;
                    dataTiempos.DataBind();
                    dataTax.DataBind();
                    dataGastosPatio.DataBind();
                    dataGastosOficina.DataBind();
                    dataGastosFijos.DataBind();
                    dataAbonoMPO.DataBind();
                    dataBuenTrato.DataBind();
                    dataChatarra.DataBind();
                    //dataLiquidacion.DataBind();
                    //dataDevoluciones.DataBind();
                    //dataFacturas.DataBind();
                }
            }
            catch (Exception ex)
            {
                funcionalidades.guardarIncidencia(empresa, "OFERTA", numeroOferta, "", "Calcular oferta // " + dondeVa + " // " + ex.Message, 1, usuario, User.Identity.Name.ToString());
                Informe.mostrarMensaje("Calcular oferta // " + dondeVa + " // " + ex.Message);
            }
            finally
            {
                Session["dtTiempos"] = dtTiempos;
            }
        }

        public static void calcularOfertaNUEVO(ref DataTable dtResultados, int cual, string empresa, List<string> listaNumeros, string version, bool calcularConPortes, bool calcularConFenolico)
        {
            string usuarioRed = Session["usuarioRed"].ToString();
            string idioma = Session["idioma"].ToString();
            DataTable dtTiempos = new DataTable();
            dtTiempos = (DataTable)Session["dtTiempos"];
            DataRow oItemTiempo;

            //string idioma = "";
            //HttpCookie nameCookie = Request.Cookies["idioma"];
            //if (nameCookie != null)
            //    idioma = nameCookie.Value.ToString();
            string usuario = "";
            List<string> listaObras = new List<string>();

            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            DateTime horaLlamada_1 = default(DateTime);
            TimeSpan horaLlamada_2 = default(TimeSpan);
            DateTime horaProcesar_1 = default(DateTime);
            TimeSpan horaProcesar_2 = default(TimeSpan);
            string dondeVa = "";
            string numeros = "";

            string numeroOferta = "";

            DataTable dtFechasBase = new DataTable();
            DataRow oFechaBase;
            dtFechasBase.Columns.Add("ARTICULO");
            dtFechasBase.Columns.Add("FECHA", typeof(DateTime));
            decimal importeAlquilerCapitulo = 0;
            decimal importeVentaCapitulo = 0;
            decimal dec_importeCosteVCH = 0;

            try
            {
                //DataTable dtTiempos = new DataTable();
                //DataRow oItemTiempo;
                //dtTiempos.Columns.Add("CONCEPTO");
                //dtTiempos.Columns.Add("TIEMPO");

                DataTable dtFenolicoNuevo = new DataTable();
                DataRow oItemFenolicoNuevo;
                dtFenolicoNuevo.Columns.Add("ITEM", typeof(string));
                dtFenolicoNuevo.Columns.Add("TIPO", typeof(string));
                dtFenolicoNuevo.Columns.Add("PS", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("CANTIDAD", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE PS", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE FENOLICO ESTANDAR", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE FENOLICO ESPECIAL", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("METROS", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE FENOLICO", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("% COSTE MATERIAL NUEVO", typeof(decimal));
                dtFenolicoNuevo.Columns.Add("COSTE MATERIAL NUEVO", typeof(decimal));

                DataTable dtPedidoFechaBase = new DataTable();
                dtPedidoFechaBase.Columns.Add("PEDIDO", typeof(string));
                dtPedidoFechaBase.Columns.Add("FECHA PEDIDO", typeof(DateTime));
                dtPedidoFechaBase.Columns.Add("OFERTA", typeof(string));
                dtPedidoFechaBase.Columns.Add("SALES STATUS", typeof(string));
                dtPedidoFechaBase.Columns.Add("FECHA CAPITULO", typeof(DateTime));
                dtPedidoFechaBase.Columns.Add("ITEM", typeof(string));

                DataTable dtOfertasFechaBase = new DataTable();
                dtOfertasFechaBase.Columns.Add("OFERTA", typeof(string));
                dtOfertasFechaBase.Columns.Add("FECHA", typeof(DateTime));
                dtOfertasFechaBase.Columns.Add("OBRA", typeof(string));
                dtOfertasFechaBase.Columns.Add("PEDIDO", typeof(string));

                DataTable dtCabeceraOferta = new DataTable();
                dtCabeceraOferta.Columns.Add("OFERTA", typeof(string));
                dtCabeceraOferta.Columns.Add("NOMBRE OFERTA", typeof(string));
                dtCabeceraOferta.Columns.Add("FECHA", typeof(DateTime));
                dtCabeceraOferta.Columns.Add("MONEDA", typeof(string));
                dtCabeceraOferta.Columns.Add("CUENTA CLIENTE", typeof(string));
                dtCabeceraOferta.Columns.Add("OBRA", typeof(string));
                dtCabeceraOferta.Columns.Add("MASTER OBRA", typeof(string));
                dtCabeceraOferta.Columns.Add("CENTRO COSTE", typeof(string));
                dtCabeceraOferta.Columns.Add("DELEGACION", typeof(string));
                dtCabeceraOferta.Columns.Add("TIPO", typeof(string));
                dtCabeceraOferta.Columns.Add("KILOMETRO", typeof(decimal));
                dtCabeceraOferta.Columns.Add("MODO ENTREGA", typeof(string));
                dtCabeceraOferta.Columns.Add("GESTION PORTE", typeof(int));
                dtCabeceraOferta.Columns.Add("TIPO PORTE", typeof(int));
                dtCabeceraOferta.Columns.Add("FORMA PAGO", typeof(string));
                dtCabeceraOferta.Columns.Add("NUMERO MESES", typeof(int));
                dtCabeceraOferta.Columns.Add("NUMERO DIAS", typeof(int));
                dtCabeceraOferta.Columns.Add("MULTIVENCIMIENTO", typeof(string));
                dtCabeceraOferta.Columns.Add("DIAS PAGO", typeof(decimal));
                dtCabeceraOferta.Columns.Add("DIAS ADD", typeof(decimal));
                dtCabeceraOferta.Columns.Add("DIAS TOTAL", typeof(decimal));
                dtCabeceraOferta.Columns.Add("PERIOD UNIT", typeof(string));
                dtCabeceraOferta.Columns.Add("QTY UNIT", typeof(int));
                dtCabeceraOferta.Columns.Add("QTY", typeof(int));
                dtCabeceraOferta.Columns.Add("VALUE", typeof(decimal));

                DataTable dtCapitulosOferta = new DataTable();
                dtCapitulosOferta.Columns.Add("OFERTA", typeof(string));
                dtCapitulosOferta.Columns.Add("CAPITULO", typeof(string));
                dtCapitulosOferta.Columns.Add("FECHA", typeof(DateTime));
                dtCapitulosOferta.Columns.Add("TIPO", typeof(string));

                DataTable dtLineasOferta = new DataTable();
                dtLineasOferta.Columns.Add("OFERTA", typeof(string));
                dtLineasOferta.Columns.Add("CAPITULO", typeof(string));
                dtLineasOferta.Columns.Add("ITEM", typeof(string));
                dtLineasOferta.Columns.Add("CANTIDAD", typeof(decimal));
                dtLineasOferta.Columns.Add("IMPORTE", typeof(decimal));
                dtLineasOferta.Columns.Add("DURACION", typeof(decimal));
                dtLineasOferta.Columns.Add("CANON FENOLICO", typeof(decimal));
                dtLineasOferta.Columns.Add("CANON IMPORTE", typeof(decimal));
                dtLineasOferta.Columns.Add("CANON", typeof(decimal));
                dtLineasOferta.Columns.Add("EURO DIA", typeof(decimal));
                dtLineasOferta.Columns.Add("PRECIO", typeof(decimal));

                DataRow[] filasCapitulo;
                DataRow[] filasLinea;
                DataRow[] filasOcupacion;

                // GASTOS FIJOS
                decimal bonificacionBU = 0;
                decimal coeficienteGastosFijosAlquilerVenta = 0;
                DataTable dtDatosGastosFijos = new DataTable();
                DataTable dtGastosFijos = new DataTable();
                DataRow oItemGastosFijos;
                dtGastosFijos.Columns.Add("fact_anual_empresa", typeof(Decimal));
                dtGastosFijos.Columns.Add("facturacion_alquiler", typeof(Decimal));
                dtGastosFijos.Columns.Add("facturacion_venta", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_admin", typeof(Decimal));
                dtGastosFijos.Columns.Add("var_admin", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_comerc", typeof(Decimal));
                dtGastosFijos.Columns.Add("var_comerc", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_gf_bu", typeof(Decimal));
                dtGastosFijos.Columns.Add("total_gf_hq", typeof(Decimal));
                dtGastosFijos.Columns.Add("KVBU", typeof(Decimal));
                dtGastosFijos.Columns.Add("KABU", typeof(Decimal));
                dtGastosFijos.Columns.Add("KVHQ", typeof(Decimal));
                dtGastosFijos.Columns.Add("KAHQ", typeof(Decimal));
                dtGastosFijos.Columns.Add("ITEM");
                //dtGastosFijos.Columns.Add("TIPO");
                //dtGastosFijos.Columns.Add("IMPORTE TOTAL");
                //dtGastosFijos.Columns.Add("IMPORTE", typeof(Decimal));
                dtGastosFijos.Columns.Add("FACTURACION ALQUILER", typeof(Decimal));
                dtGastosFijos.Columns.Add("FACTURACION VENTA", typeof(Decimal));
                dtGastosFijos.Columns.Add("COSTE ALQUILER", typeof(Decimal));
                dtGastosFijos.Columns.Add("COSTE VENTA", typeof(Decimal));
                //dtGastosFijos.Columns.Add("BU ALQUILER", typeof(Decimal));
                //dtGastosFijos.Columns.Add("BU VENTA", typeof(Decimal));
                //dtGastosFijos.Columns.Add("CENTRAL ALQUILER", typeof(Decimal));
                //dtGastosFijos.Columns.Add("CENTRAL VENTA", typeof(Decimal));
                dtGastosFijos.Columns.Add("TOTAL BU", typeof(Decimal));
                dtGastosFijos.Columns.Add("TOTAL CENTRAL", typeof(Decimal));
                dtGastosFijos.Columns.Add("BONIFICACION BU", typeof(Decimal));
                dtGastosFijos.Columns.Add("BONIFICACION BU CENTRAL", typeof(Decimal));

                decimal fact_anual_empresa = 0;
                decimal facturacion_alquiler = 0;
                decimal facturacion_venta = 0;
                decimal total_admin = 0;
                decimal var_admin = 0;
                decimal total_comerc = 0;
                decimal var_comerc = 0;
                decimal total_gf_bu = 0;
                decimal total_gf_hq = 0;
                decimal KVBU = 0;
                decimal KABU = 0;
                decimal KVHQ = 0;
                decimal KAHQ = 0;

                decimal totalGastosFijosBU = 0;
                decimal totalGastosFijosCentral = 0;

                // GASTOS FINANCIACION
                decimal financiacionBU = 0;
                decimal condicionesBU = 0;
                decimal condicionesCentral = 0;
                //decimal condicionesAplicar = 0;

                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var seed = int.Parse(justNumbers.Substring(0, 4));
                var rnd = new Random(seed);
                //Random rnd = new Random(10);
                int numeroRandom;

                string codigosTransporte = "";
                DataRow filaValores;

                List<string> listaArticulosSinFicha = new List<string>();
                List<string> listaArticulosPedido = new List<string>();
                List<DateTime> listaFechasPedido = new List<DateTime>();
                List<string> listaOfertas = new List<string>();
                List<DateTime> listaFechasOferta = new List<DateTime>();

                hora_1 = DateTime.Now;

                string strConexion;
                strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();

                SqlConnection conexion;

                SqlDataAdapter adaptador;
                System.Data.DataTable dtTabla = new System.Data.DataTable();
                SqlCommand comando;

                DateTime horaTotal_1 = default(DateTime);
                TimeSpan horaTotal_2 = default(TimeSpan);

                string moneda = "";
                string masterObra = "";
                decimal total;

                DataTable dtArticulosPaneles;

                SqlDataAdapter adaptadorDatosConfiguracion;
                System.Data.DataTable dtDatosConfiguracion;

                List<int> listaBonificacionesDesde = new List<int>();
                List<int> listaBonificacionesHasta = new List<int>();
                List<decimal> listaBonificacionesValor = new List<decimal>();

                decimal pesoArticulo = 0;
                int vocacionChatarra = 0;
                decimal kilometros = 0;
                decimal cambio = 1;
                //bool hayTR001 = false;
                bool existeTR001 = false;
                int lineasBonificacion;
                int duracion = 0;
                int diasCalculados = 0;
                int diasMultiplicar = 0;
                int diasDesdeTaxNoConsumible = 0;
                int diasHastaTaxNoConsumible = 0;
                int diasTaximetroNoConsumibleCalcular = 0;
                decimal importeCosteBonificacionTaximetroConsumible = 0;

                string tipoServicio = "";
                string tituloCapitulo = "";
                string tituloCapituloAsterisco = "";

                System.Data.DataTable dtValores;

                //dataTiempos.DataSource = null;
                //dataTiempos.Columns.Clear();

                DataTable dtTaximetro;
                dtTaximetro = new DataTable();
                dtTaximetro.Columns.Add("FECHA DOC");
                dtTaximetro.Columns.Add("FECHA CAPITULO");
                dtTaximetro.Columns.Add("CAPITULO");
                dtTaximetro.Columns.Add("TIPO CAPITULO");
                dtTaximetro.Columns.Add("MOVIMIENTO");
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
                dtTaximetro.Columns.Add("PRECIO CAMBIO");
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
                dtTaximetro.Columns.Add("FECHA_CALCULADA");
                dtTaximetro.Columns.Add("ALQUILER ENTREGA");
                dtTaximetro.Columns.Add("ALQUILER DEVOLUCION");
                dtTaximetro.Columns.Add("ALQUILER ENTREGA NAVIERA");
                dtTaximetro.Columns.Add("ALQUILER DEVOLUCION NAVIERA");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR REPARAR DIRECTO");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR DIRECTO");
                dtTaximetro.Columns.Add("ALQUILER CLASIFICAR INDIRECTO");
                dtTaximetro.Columns.Add("ALQUILER REPARAR DIRECTO");
                dtTaximetro.Columns.Add("ALQUILER REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("ALQUILER GASTOS PATIO");
                dtTaximetro.Columns.Add("VENTA ENTREGA");
                dtTaximetro.Columns.Add("VENTA DEVOLUCION");
                dtTaximetro.Columns.Add("VENTA ENTREGA NAVIERA");
                dtTaximetro.Columns.Add("VENTA DEVOLUCION NAVIERA");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR REPARAR DIRECTO");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR DIRECTO");
                dtTaximetro.Columns.Add("VENTA CLASIFICAR INDIRECTO");
                dtTaximetro.Columns.Add("VENTA REPARAR DIRECTO");
                dtTaximetro.Columns.Add("VENTA REPARAR INDIRECTO");
                dtTaximetro.Columns.Add("VENTA GASTOS PATIO");
                dtTaximetro.Columns.Add("PRECIO GASTOS PATIO");
                dtTaximetro.Columns.Add("FECHA PRECIO GASTOS PATIO");
                dtTaximetro.Columns.Add("% DURACION AJUSTE", typeof(decimal));
                dtTaximetro.Columns.Add("DURACION AJUSTE", typeof(decimal));
                dtTaximetro.Columns.Add("AJUSTE OBRA DURACION", typeof(decimal));
                dtTaximetro.Columns.Add("% OCUPACION AJUSTE", typeof(decimal));
                dtTaximetro.Columns.Add("AJUSTE PRODUCTO", typeof(decimal));
                dtTaximetro.Columns.Add("AJUSTE PRODUCTO SOBRE");

                // gastos variables oficina
                int mesPresupuestoValido = 0;
                decimal porcentajeGastoAdmministrativoVariable = 0;
                decimal porcentajeGastoComercialVariable = 0;
                decimal porcentajeMaximoTotalFacturacion = 0;
                System.Data.DataTable dtDatosGastosOficina = new System.Data.DataTable();
                System.Data.DataTable dtGastosOficina = new System.Data.DataTable();
                dtGastosOficina.Columns.Add("CAPITULO");
                dtGastosOficina.Columns.Add("ITEM");
                dtGastosOficina.Columns.Add("MES PRESUPUESTO VALIDO");
                dtGastosOficina.Columns.Add("PORCENTAJE GASTO ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("PORCENTAJE GASTO COMERCIAL");
                dtGastosOficina.Columns.Add("PORCENTAJE MAXIMO TOTAL FACTURACON");
                dtGastosOficina.Columns.Add("TOTAL ANUAL");
                dtGastosOficina.Columns.Add("TOTAL ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("VAR ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("GASTOS ADMINISTRATIVO");
                dtGastosOficina.Columns.Add("TOTAL COMERCIAL");
                dtGastosOficina.Columns.Add("VAR COMERCIAL");
                dtGastosOficina.Columns.Add("GASTOS COMERCIAL");
                dtGastosOficina.Columns.Add("TOTAL FACTURACION");
                dtGastosOficina.Columns.Add("MAX FACTURACION");
                dtGastosOficina.Columns.Add("IMPORTE");
                dtGastosOficina.Columns.Add("COSTE DT");
                dtGastosOficina.Columns.Add("GASTOS DT");
                decimal totalAnual = 0;
                decimal totalAdministrativo = 0;
                decimal varAdministrativo = 0;
                decimal totalComercial = 0;
                decimal varComercial = 0;
                decimal costeDT = 0;
                decimal preciosEnCero = 0;

                // gastos variables patio
                decimal clasificarRepararDirecto;
                decimal clasificarRepararIndirecto;
                decimal clasificarDirecto;
                decimal clasificarIndirecto;
                decimal repararDirecto;
                decimal repararIndirecto;
                decimal entrega = 0;
                decimal devolucion = 0;
                decimal naviera = 0;
                decimal precioGastosPatio = 0;

                decimal pesoCapitulo = 0;
                decimal pesoTotal = 0;
                decimal coeficienteNuevo = 0;
                decimal coeficienteUsadoChatarra = 0;
                decimal coeficienteUsadoMPOLiquidacion = 0;
                decimal coeficienteUsadoVenta = 0;
                decimal coeficienteUsar = 0;
                //decimal coeficienteMixto = 0;
                string tipoArticulo = "";
                string tipoArticuloFenolico = "";
                decimal porcentajeCosteMaterialNuevo = 0;
                decimal costeFenolicoNuevoEstandar = 0;
                decimal costeFenolicoNuevoEspecial = 0;
                decimal canonFenolicoNuevoEstandar = 0;
                decimal canonFenolicoNuevoEspecial = 0;

                decimal importeCosteMaterialNuevo = 0;
                decimal importeCosteMaterialFenolico = 0;

                DateTime fechaPrecio = DateTime.Now;

                bool metersInvoicing = false;
                string productType = "";
                bool calcularPorSuperficie = false;
                decimal superficie = 0;
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
                bool gastosFijosPorFacturacion = true;
                int diasxMes = 0;
                int diasDiferencia = 0;
                int diasPagoDefecto = 0;
                int diasRestar = 0;
                DateTime fechaPrecios;
                bool hayPrecio = false;
                bool primeraVez = true;
                int lineaCapitulo;
                System.Data.DataTable dtArticulos;
                System.Data.DataTable dtGastosPatio;
                System.Data.DataTable dtOcupacion;
                DataRow[] filaEncontrada;
                DataRow[] filaEncontradaFenolico;
                string articulos = "";
                string articulosCambio = "";
                decimal precioCoste = 0;

                decimal importeGastosPatioEntregaDevolucionVentas = 0;
                decimal importeGastosPatioEntregaDevolucionAlquiler = 0;
                decimal importeGastosPatioClasificacionReparacionAlquiler = 0;

                decimal importeGastosVariablesEntregaDevolucionAlquilerCapitulos = 0;
                decimal importeGastosVariablesEntregaDevolucionVentaCapitulos = 0;
                decimal importeGastosVariablesClaisificacionReparacionAlquilerCapitulos = 0;
                decimal importeGastosVariablesClaisificacionReparacionVentaCapitulos = 0;

                decimal importeFacturacionAlquilerCapitulos = 0;
                decimal importeCosteTaximetroNoConsumibleCapitulos = 0;
                decimal importeCosteTaximetroConsumibleCapitulos = 0;
                decimal importeCosteBonificacionTaximetroConsumibleCapitulos = 0;
                decimal importeFacturacionVentaCapitulos = 0;
                decimal importeFacturacionVentaDepartamentoTecnicoCapitulos = 0;
                decimal importeFacturacionVentaFenolicoNuevoCapitulos = 0;
                decimal importeFacturacionVentaUneCifCapitulos = 0;
                decimal importeFacturacionVentaMontajesCapitulos = 0;
                decimal importeFacturacionVentaProductoCapitulos = 0;
                decimal importeCosteVentaCapitulos = 0;
                decimal importeFacturacionVentaOtrosServiciosCapitulos = 0;
                decimal importeCosteVentaOtrosServiciosCapitulos = 0;

                decimal importeCosteVentaDepartamentoTecnicoCapitulos = 0;
                decimal importeCosteVentaFenolicoNuevoCapitulos = 0;
                decimal importeCosteVentaNuevoCapitulos = 0;
                decimal importeGastosVariablesVentaFenolicoNuevoCapitulos = 0;
                decimal importeGastosFijosBUVentaFenolicoNuevoCapitulos = 0;
                decimal importeGastosFijosCentralesVentaFenolicoNuevoCapitulos = 0;

                decimal importeCosteVentaUneCifCapitulos = 0;
                decimal importeGastosVariablesVentaUneCifCapitulos = 0;
                decimal importeGastosFijosBUVentaUneCifCapitulos = 0;
                decimal importeGastosFijosCentralesVentaUneCifCapitulos = 0;

                decimal importeCosteVentaMontajesCapitulos = 0;
                decimal importeGastosVariablesVentaMontajesCapitulos = 0;
                decimal importeGastosFijosBUVentaMontajesCapitulos = 0;
                decimal importeGastosFijosCentralesVentaMontajesCapitulos = 0;

                decimal importeGastosFinanciacionCapitulos = 0;

                decimal porcentajeCobrados = 0;
                decimal porcentajeFallidos = 0;
                decimal importeFallidosCapitulos = 0;

                datosGenerales = "";

                horaTotal_1 = DateTime.Now;
                fechaPrecios = new DateTime();

                // CABECERA
                decimal importeAlquiler = 0;
                decimal importeVenta = 0;
                decimal importePorte = 0;
                decimal importePorte_TR001 = 0;
                decimal importeCosteTaximetroNoConsumible = 0;
                decimal importeCosteTaximetroConsumible = 0;
                //decimal importeCosteVenta = 0;
                decimal importeCostePorte = 0;

                // AJUSTES
                string ajustesAlcance = "";
                decimal ajustesPocentajeCargoMaximo = 0;
                int ajustesValorObraCorta = 0;
                decimal ajustesPorcentajeCorreccionObraCorta = 0;
                int ajustesValorObraLarga = 0;
                decimal ajustesPorcentajeCorreccionObraLarga = 0;
                int ajustesValorMaximo = 0;
                decimal ajustesPorcentajeCorreccionMaximo = 0;
                decimal GF = 0;
                decimal BUimporteLinea = 0;
                decimal centralImporteLinea = 0;
                decimal importeVentaLinea = 0;
                decimal importeAlquilerLinea = 0;
                decimal porcentajeAjustesObraDuracionGFCapitulo = 0;
                DataTable dtAjustesDuracionGF = new DataTable();
                DataRow oAjustesDuracionGF;
                dtAjustesDuracionGF.Columns.Add("DURACION", typeof(int));
                dtAjustesDuracionGF.Columns.Add("AJUSTE", typeof(decimal));
                decimal costeAlquiler = 0;
                decimal costeVentas = 0;

                decimal importeAjustesObraDuracionGFCapitulo = 0;
                decimal ajustesTaxDuracionMinima = 0;
                decimal ajustesTaxDuracionMaxima = 0;
                decimal ajustesTaxPorcentajeAbonoMensual = 0;
                decimal duracionAjuste = 0;
                decimal importeAjustesObraDuracionTaxCapitulo = 0;

                decimal importeAjustesProductoTaxCapitulo = 0;
                decimal ocupacionAjuste = 0;

                //string ajusteAlcanceOcupacion = "";
                //int ajustePeriodoOcupacion = 0;
                //decimal ajusteValorOcupacionMuyBaja = 0;
                //decimal ajustePorcentajeAbonoMaximo = 0;
                //decimal ajusteValorOcupacionBaja = 0;
                //decimal ajustePorcentajeAbonoOcupacionBaja = 0;
                //decimal ajusteValorOcupacionMedia = 0;
                //decimal ajustePorcentajeAbonoOcupacionMedia = 0;
                //decimal ajusteValorOcupacionAlta = 0;
                //decimal ajustePorcentajeAbonoOcupacionAlta = 0;

                string ajusteAlcanceCliente = "";
                int ajusteValorClienteMediano = 0;
                decimal ajustePorcentajeCorreccionClienteMediano = 0;
                int ajusteValorClienteGrande = 0;
                decimal ajustePorcentajeCorreccionClienteGrande = 0;

                // AJUSTES BU
                DataTable dtAjustesBU = new DataTable();
                DataTable dtAjustesDatosBU = new DataTable();

                string cumplimientoBajoAfectaGFCentral = "";
                string cumplimientoBajoAfectaGFBU = "";
                string cumplimientoAltoAfectaGFCentral = "";
                string cumplimientoAltoAfectaGFBU = "";
                decimal cumplimientoBajoPorcentaje = 0;
                decimal bonificacionCumplimientoBajoPorcentaje = 0;
                decimal cumplimientoAltoPorcentaje = 0;
                decimal bonificacionCumplimientoAltoPorcentaje = 0;

                string error = "";
                horaProcesar_1 = DateTime.Now;

                numeroRandom = rnd.Next();
                numeroRandom = rnd.Next();
                //usuario = User.Identity.Name.ToString() + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();
                //if (User.Identity.Name.ToString() == "")
                //    usuario = "ROP" + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();

                usuario = usuarioRed + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();
                //if (Environment.UserName == "")
                //    usuario = "ROP" + "_" + hora_1.Minute.ToString("00") + "_" + hora_1.Second.ToString("00") + "_" + hora_1.Millisecond.ToString("00") + "_" + numeroRandom.ToString();

                horaLlamada_1 = DateTime.Now;

                hora_1 = DateTime.Now;
                estructuraOfertasNuevo(ref error, ref listaObras, ref dtCabeceraOferta, ref dtCapitulosOferta, ref dtLineasOferta, empresa, listaNumeros);
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO generar estructuras ofertas";
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                dtTiempos.Rows.Add(oItemTiempo);
                if (error != "")
                {
                    funcionalidades.guardarIncidencia(empresa, "OFERTA", "", listaNumeros.ElementAt(0), "Error al crear las estructuras de datos para la oferta", 1, usuario, User.Identity.Name.ToString());
                    Informe.mostrarMensaje("Error al crear las estructuras de datos para la oferta oferta // " + error);
                    return;
                }

                horaLlamada_2 = DateTime.Now.Subtract(horaLlamada_1);
                if (dtCabeceraOferta.Rows.Count == 0)
                {
                    funcionalidades.guardarIncidencia(empresa, "OFERTA", "", listaNumeros.ElementAt(0), "NO se ha encontrado la oferta indicada", 1, usuario, User.Identity.Name.ToString());
                    Informe.mostrarMensaje("NO se ha encontrado la oferta indicada");
                    return;
                }

                foreach (DataRow filaCabecera in dtCabeceraOferta.Rows)
                {
                    if ((bool)Session["datosTransporteOriginales"])
                    {
                        Session["datosTransporteOriginales"] = false;
                        chkBoxSoloEntrega.Checked = false;
                        chkBoxSoloDevolucion.Checked = false;
                        if (filaCabecera["DELEGACION"].ToString().ToUpper() != "EX" && Convert.ToInt32(filaCabecera["GESTION PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_GestionPorte.Alsina.GetHashCode()))
                        {
                            if (Convert.ToInt32(filaCabecera["TIPO PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_TipoPorte.Todo))
                            {
                                chkBoxSoloEntrega.Checked = true;
                                chkBoxSoloDevolucion.Checked = true;
                            }
                            else if (Convert.ToInt32(filaCabecera["TIPO PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_TipoPorte.SoloEntrega.GetHashCode()))
                                chkBoxSoloEntrega.Checked = true;
                            else if (Convert.ToInt32(filaCabecera["TIPO PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_TipoPorte.SoloDevolucion.GetHashCode()))
                                chkBoxSoloDevolucion.Checked = true;
                        }
                    }
                    calcularConPortes = (chkBoxSoloEntrega.Checked || chkBoxSoloDevolucion.Checked);

                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "OFERTA " + filaCabecera["OFERTA"].ToString();
                    dtTiempos.Rows.Add(oItemTiempo);

                    dtValores = new System.Data.DataTable();
                    dtValores.Columns.Add("ETIQUETA");
                    dtValores.Columns.Add("CONCEPTO");
                    dtValores.Columns.Add("IMPORTE");
                    dtValores.Columns.Add("PORCENTAJE");
                    dtValores.Columns.Add("IMPORTE PREVISTO");
                    dtValores.Columns.Add("PORCENTAJE PREVISTO");
                    dtValores.Columns.Add("COLOR");

                    dtArticulos = new System.Data.DataTable();
                    dtGastosPatio = new System.Data.DataTable();
                    dtOcupacion = new System.Data.DataTable();

                    dondeVa = "Oferta";
                    datosGenerales = filaCabecera["OFERTA"].ToString() + " / " + filaCabecera["CUENTA CLIENTE"].ToString() + " / " + filaCabecera["NOMBRE OFERTA"].ToString() + " / " + filaCabecera["MASTER OBRA"].ToString() + " // " + filaCabecera["CENTRO COSTE"].ToString() + " // " + filaCabecera["MONEDA"].ToString();

                    codigosTransporte = "";
                    funcionalidades.calcularCodigosTransporte(ref dtTiempos, ref codigosTransporte, empresa, filaCabecera["DELEGACION"].ToString(), "", usuarioRed);

                    moneda = filaCabecera["MONEDA"].ToString();
                    masterObra = filaCabecera["MASTER OBRA"].ToString();
                    numeroOferta = filaCabecera["OFERTA"].ToString();

                    txtObra.Text = filaCabecera["OFERTA"].ToString();
                    txtNombreObra.Text = filaCabecera["NOMBRE OFERTA"].ToString();
                    txtCuentaCliente.Text = filaCabecera["CUENTA CLIENTE"].ToString();
                    txtMasterObra.Text = filaCabecera["MASTER OBRA"].ToString();
                    txtCentroCoste.Text = filaCabecera["CENTRO COSTE"].ToString();
                    txtMoneda.Text = filaCabecera["MONEDA"].ToString();

                    porcentajeCobrados = 0;
                    porcentajeFallidos = 0;
                    DataTable dtFallidos = new DataTable();
                    dtFallidos = funcionalidades.fallidos(ref dtTiempos, masterObra, Convert.ToDateTime(filaCabecera["FECHA"].ToString()), version, filaCabecera["CUENTA CLIENTE"].ToString(), "", usuarioRed);
                    foreach (DataRow fila in dtFallidos.Rows)
                    {
                        porcentajeCobrados = Convert.ToDecimal(fila["CFGFAL_PorcentajeCobrados"].ToString());
                        porcentajeFallidos = Convert.ToDecimal(fila["CFGFAL_PorcentajeFallidos"].ToString());
                        break;
                    }

                    dondeVa = "Recuperar versión a aplicar";
                    string versionUtilizada = "";
                    funcionalidades.localizarVersion(ref dtTiempos, ref versionUtilizada, Convert.ToDateTime(filaCabecera["FECHA"].ToString()), "", usuarioRed);
                    lblVersionUtilizada.Text = versionUtilizada;

                    // DATOS PANELES
                    dtArticulosPaneles = new DataTable();
                    dtArticulosPaneles = funcionalidades.localizarPaneles(ref dtTiempos, Convert.ToDateTime(filaCabecera["FECHA"].ToString()), version, "", usuarioRed);

                    // DATOS CONFIGURACION
                    hora_1 = DateTime.Now;
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear conexion";
                    comando = conexion.CreateCommand();
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear comando";
                    comando.CommandText = "ROP_DatosConfiguracionGeneral";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    comando.Parameters.AddWithValue("@delegacion", filaCabecera["DELEGACION"].ToString());
                    if (cmbVersion.SelectedValue == "")
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else
                        comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear adaptador";
                    adaptador.Fill(dtTabla);
                    dondeVa = "Recuperar datos configuracion ROP_DatosConfiguracionGeneral crear datatable";
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    dondeVa = "fin recuperar datos configuracion ROP_DatosConfiguracionGeneral";
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracionGeneral";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    for (int i = 0; i < dtTabla.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CALCULAR GASTOS FIJOS POR FACTURACION/COSTE")
                        {
                            if (Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"])) == 0)
                                gastosFijosPorFacturacion = false;
                            else
                                gastosFijosPorFacturacion = true;
                        }
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "DÍAS POR MES")
                            diasxMes = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CÁLCULO FECHA BASE: DÍAS ENTRE FECHA OFERTA Y FECHA CAPÍTULO")
                            diasDiferencia = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CÁLCULO FECHA BASE: DÍAS A RETROCEDER FECHA CREACIÓN DEL CAPÍTULO DE LA OFERTA")
                            diasRestar = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "MES PRESUPUESTO VÁLIDO")
                            mesPresupuestoValido = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "PORCENTAJE GASTO PERSONAL ADMINISTRATIVO EN VARIABLE")
                            porcentajeGastoAdmministrativoVariable = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "PORCENTAJE GASTO PERSONAL COMERCIAL EN VARIABLE")
                            porcentajeGastoComercialVariable = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "PORCENTAJE MÁXIMO TOTAL FACTURACIÓN")
                            porcentajeMaximoTotalFacturacion = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);

                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "COEFICIENTE DE GASTOS FIJOS ALQUILER/VENTA")
                            coeficienteGastosFijosAlquilerVenta = Convert.ToDecimal(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));

                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "%FINANCIACIÓN BU")
                            financiacionBU = (Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CONDICIONES FINANCIACIÓN BU")
                            condicionesBU = (Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "CONDICIONES FINANCIACIÓN CENTRAL")
                            condicionesCentral = (Convert.ToDecimal(dtTabla.Rows[i]["Valor"]) / 100);
                        condicionesAplicar = (financiacionBU * condicionesBU) + ((1 - financiacionBU) * condicionesCentral);

                        if (Convert.ToString(dtTabla.Rows[i]["Concepto"]).ToUpper() == "DÍAS PAGO POR DEFECTO")
                            diasPagoDefecto = Convert.ToInt32(Convert.ToDecimal(dtTabla.Rows[i]["Valor"]));
                    }

                    // DATOS CONFIGURACION AJUSTES
                    hora_1 = DateTime.Now;
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ROP_DatosConfiguracionAjustesConsulta";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    comando.Parameters.AddWithValue("@delegacion", filaCabecera["DELEGACION"].ToString());
                    if (cmbVersion.SelectedValue == "")
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else
                        comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtTabla);
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracionAjustesConsulta";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    for (int i = 0; i < dtTabla.Rows.Count; i++)
                    {
                        // gastos fijos
                        ajustesAlcance = dtTabla.Rows[i]["CFGAJU_Alcance"].ToString();
                        ajustesPocentajeCargoMaximo = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PocentajeCargoMaximo"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = 0;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPocentajeCargoMaximo;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        ajustesValorObraCorta = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorObraCorta"]);
                        ajustesPorcentajeCorreccionObraCorta = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionObraCorta"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = ajustesValorObraCorta;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPorcentajeCorreccionObraCorta;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        ajustesValorObraLarga = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorObraLarga"]);
                        ajustesPorcentajeCorreccionObraLarga = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionObraLarga"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = ajustesValorObraLarga;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPorcentajeCorreccionObraLarga;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        ajustesValorMaximo = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorMaximo"]);
                        ajustesPorcentajeCorreccionMaximo = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionMaximo"]) / 100;
                        oAjustesDuracionGF = dtAjustesDuracionGF.NewRow();
                        oAjustesDuracionGF["DURACION"] = ajustesValorMaximo;
                        oAjustesDuracionGF["AJUSTE"] = ajustesPorcentajeCorreccionMaximo;
                        dtAjustesDuracionGF.Rows.Add(oAjustesDuracionGF);
                        oAjustesDuracionGF = null;

                        // taxímetro
                        ajustesTaxDuracionMinima = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_DuracionMinima"]);
                        ajustesTaxDuracionMaxima = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_DuracionMaxima"]);
                        ajustesTaxPorcentajeAbonoMensual = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoMensual"]) / 100;

                        //// Ocupacion
                        //ajusteAlcanceOcupacion = dtTabla.Rows[i]["CFGAJU_AlcanceOcupacion"].ToString(); 
                        //ajustePeriodoOcupacion = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_PeriodoOcupacion"]);
                        //ajusteValorOcupacionMuyBaja = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionMuyBaja"]) / 100;
                        //ajustePorcentajeAbonoMaximo = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoMaximo"]) / 100;
                        //ajusteValorOcupacionBaja = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionBaja"]) / 100;
                        //ajustePorcentajeAbonoOcupacionBaja = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoOcupacionBaja"]) / 100;
                        //ajusteValorOcupacionMedia = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionMedia"]) / 100;
                        //ajustePorcentajeAbonoOcupacionMedia =Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoOcupacionMedia"]) / 100;
                        //ajusteValorOcupacionAlta = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_ValorOcupacionAlta"]) / 100;
                        //ajustePorcentajeAbonoOcupacionAlta = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeAbonoOcupacionAlta"]) / 100;

                        // Cliente
                        ajusteAlcanceCliente = dtTabla.Rows[i]["CFGAJU_AlcanceVolumenCliente"].ToString(); ;
                        ajusteValorClienteMediano = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorClienteMediano"]);
                        ajustePorcentajeCorreccionClienteMediano = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionClienteMediano"]) / 100;
                        ajusteValorClienteGrande = Convert.ToInt32(dtTabla.Rows[i]["CFGAJU_ValorClienteGrande"]);
                        ajustePorcentajeCorreccionClienteGrande = Convert.ToDecimal(dtTabla.Rows[i]["CFGAJU_PorcentajeCorreccionClienteGrande"]) / 100;
                    }

                    // DATOS COSTE TRANSPORTE
                    dondeVa = "Recuperar datos transporte ";
                    decimal valorCostePorte = 0;
                    decimal margenPorte = 0;
                    decimal coeficienteTransporte = 0;

                    hora_1 = DateTime.Now;
                    strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ROP_TransporteCambiarAKilometros";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    comando.Parameters.AddWithValue("@unidadMedida", null);
                    comando.Parameters.AddWithValue("@medida", filaCabecera["KILOMETRO"]);
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtTabla);
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_TransporteCambiarAKilometros";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    if (dtTabla.Rows.Count > 0)
                        kilometros = Convert.ToDecimal(dtTabla.Rows[0]["medida"]);

                    dondeVa = "Salió de conversion transporte";

                    dondeVa = "ROP_DatosConfiguracionTransporte";
                    hora_1 = DateTime.Now;
                    strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
                    conexion = new SqlConnection(strConexion);
                    conexion.Open();
                    comando = conexion.CreateCommand();
                    comando.CommandText = "ROP_DatosConfiguracionTransporte";
                    comando.CommandTimeout = 240000;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@empresa", empresa);
                    if (filaCabecera["DELEGACION"].ToString() == "")
                        comando.Parameters.AddWithValue("@delegacion", null);
                    else
                        comando.Parameters.AddWithValue("@delegacion", filaCabecera["DELEGACION"].ToString());
                    if (kilometros <= 120)
                        comando.Parameters.AddWithValue("@distancia", "<= 120");
                    else
                        if (kilometros >= 120)
                        comando.Parameters.AddWithValue("@distancia", "> 120");
                    else
                        comando.Parameters.AddWithValue("@distancia", DBNull.Value);
                    if (cmbVersion.SelectedValue == "")
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                        comando.Parameters.AddWithValue("@CFG_ID", null);
                    else
                        comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                    comando.Parameters.AddWithValue("@monedaA", moneda);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    dtTabla = new System.Data.DataTable();
                    adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtTabla);
                    adaptador.Dispose();
                    comando.Dispose();
                    conexion.Close();
                    conexion.Dispose();
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracionTransporte";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    dondeVa = "ROP_DatosConfiguracionTransporte salio";
                    if (dtTabla.Rows.Count > 0)
                    {
                        dondeVa = "ROP_DatosConfiguracionTransporte valorCostePorte";
                        valorCostePorte = (Convert.ToDecimal(dtTabla.Rows[0]["Valor"]) / 100);
                        dondeVa = "ROP_DatosConfiguracionTransporte Margen";
                        margenPorte = Convert.ToDecimal(dtTabla.Rows[0]["Margen"]) / 100;

                        coeficienteTransporte = Convert.ToDecimal(dtTabla.Rows[0]["Coeficiente"]);
                    }
                    dondeVa = "Salió de ROP_DatosConfiguracionTransporte";

                    filasCapitulo = dtCapitulosOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "'");
                    if (filasCapitulo.Length == 0)
                        datosGenerales = "NO HAY DATOS DE LOS CAPÍTULOS";
                    else
                    {
                        articulos = ";";
                        articulosCambio = ";";
                        articulosConfiguracion = "|";

                        DateTime horaDatosPase1_1 = DateTime.Now;
                        foreach (DataRow filaCapitulo in filasCapitulo)
                        {
                            dondeVa = "recorrer capitulos";

                            filasLinea = dtLineasOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "' AND CAPITULO = '" + filaCapitulo["CAPITULO"].ToString() + "'");
                            if (filasLinea.Length == 0)
                                datosGenerales = "NO HAY DATOS DE LAS LÍNEAS";
                            else
                            {
                                foreach (DataRow filaLinea in filasLinea)
                                {
                                    dondeVa = "recorrer productos";
                                    if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) >= 0)
                                        existeTR001 = true;

                                    dondeVa = "recorrer productos + recuperar axdEntity_SalesQuotationLine.ItemId -sale-";
                                    // artículos si hay ficha
                                    if (!listaArticulosPedido.Contains(filaLinea["ITEM"].ToString()))
                                    {
                                        dondeVa = "recorrer productos + en listaArticulosPedido";
                                        listaArticulosPedido.Add(filaLinea["ITEM"].ToString());
                                        if (filaCabecera["FECHA"].ToString().Length > 0)
                                            listaFechasPedido.Add(Convert.ToDateTime(filaCabecera["FECHA"].ToString()));
                                        else
                                            listaFechasPedido.Add(new DateTime());
                                    }

                                    // artículos si no hay ficha
                                    if (!listaArticulosSinFicha.Contains(filaLinea["ITEM"].ToString()))
                                    {
                                        dondeVa = "recorrer productos + en listaArticulosSinFicha";
                                        articulosConfiguracion = articulosConfiguracion + filaLinea["ITEM"].ToString() + "|";
                                        listaArticulosSinFicha.Add(filaLinea["ITEM"].ToString());
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
                                if (String.IsNullOrEmpty(filaCabecera["OBRA"].ToString()))
                                {
                                    dondeVa = "no hay ficha";
                                    fechaPrecios = new DateTime();
                                    if (filaCabecera["FECHA"].ToString().Length > 0 && filaCapitulo["FECHA"].ToString().Length > 0)
                                    {
                                        dias = Math.Abs(Convert.ToDateTime(filaCabecera["FECHA"].ToString()).Subtract(Convert.ToDateTime(filaCapitulo["FECHA"].ToString())).Days);
                                        if (Math.Abs(Convert.ToDateTime(filaCabecera["FECHA"].ToString()).Subtract(Convert.ToDateTime(filaCapitulo["FECHA"].ToString())).Days) > diasDiferencia)
                                        {
                                            fechaPrecios = Convert.ToDateTime(filaCapitulo["FECHA"].ToString()).AddDays((-1) * diasRestar);
                                            tabla = "SalesQuotationMasterTable";
                                            IDCapitulo = filaCapitulo["CAPITULO"].ToString();
                                            campo = "CreatedDateTime - " + diasRestar.ToString();
                                        }
                                        else
                                        {
                                            fechaPrecios = Convert.ToDateTime(filaCapitulo["FECHA"].ToString());
                                            tabla = "SalesQuotationMasterTable";
                                            IDCapitulo = filaCapitulo["CAPITULO"].ToString();
                                            campo = "CreatedDateTime";
                                        }
                                    }
                                    else
                                    {
                                        if (filaCapitulo["FECHA"].ToString().Length > 0)
                                        {
                                            fechaPrecios = Convert.ToDateTime(filaCapitulo["FECHA"].ToString());
                                            tabla = "SalesQuotationMasterTable";
                                            IDCapitulo = filaCapitulo["CAPITULO"].ToString();
                                            campo = "CreatedDateTime";
                                        }
                                    }

                                    for (lineaCapitulo = 0; lineaCapitulo < listaArticulosSinFicha.Count; lineaCapitulo++)
                                    {
                                        if (String.IsNullOrEmpty(filaCabecera["OBRA"].ToString()))
                                        {
                                            if (articulos.IndexOf(listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";") == -1)
                                                articulos = articulos + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";

                                            oFechaBase = dtFechasBase.NewRow();
                                            oFechaBase[0] = listaArticulosSinFicha.ElementAt(lineaCapitulo);
                                            oFechaBase[1] = Convert.ToDateTime(fechaPrecios);
                                            dtFechasBase.Rows.Add(oFechaBase);
                                        }
                                        articulosCambio = articulosCambio + listaArticulosSinFicha.ElementAt(lineaCapitulo) + "|" + fechaPrecios + ";";
                                    }
                                    // Fin calculo de la fecha a utilizar
                                }
                            }
                        }
                        TimeSpan horaDatosPase1_2 = DateTime.Now.Subtract(horaDatosPase1_1);

                        // Calculo de la fecha a utilizar
                        //o   ¿La oferta está asociada a una ficha ?
                        //	SI:
                        //•	Fecha base = Fecha base ficha
                        //•	Consultar tabla BaseDateItemObra
                        if (!String.IsNullOrEmpty(filaCabecera["OBRA"].ToString()))
                        {
                            fechaPrecios = new DateTime();
                            fechaPrecios = DateTime.Now;
                            dondeVa = "fecha base ficha artículo:entró";

                            hora_1 = DateTime.Now;
                            funcionalidades.fechaBaseFichaArticuloNUEVO(ref dtTiempos, ref listaOfertas, ref listaFechasOferta, ref listaArticulosPedido, ref listaFechasPedido, empresa, filaCabecera["OBRA"].ToString(), diasDiferencia, diasRestar, Informe.WSDominio, Informe.WSUsuario, Informe.WSContrasena, "", usuarioRed);
                            dondeVa = "fecha base ficha artículo: salió";
                            hora_2 = DateTime.Now.Subtract(hora_1);
                            oItemTiempo = dtTiempos.NewRow();
                            oItemTiempo[0] = "TIEMPO fechaBaseFichaArticuloNUEVO";
                            oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                            dtTiempos.Rows.Add(oItemTiempo);

                            tabla = "fechaBaseFichaArticulo";
                            IDCapitulo = "Ficha " + filaCabecera["OBRA"];
                            campo = "fechaBaseFichaArticulo";

                            articulos = ";";
                            articulosCambio = ";";
                            for (lineaCapitulo = 0; lineaCapitulo < listaArticulosPedido.Count; lineaCapitulo++)
                            {
                                if (articulos.IndexOf(listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";") == -1)
                                    articulos = articulos + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";
                                articulosCambio = articulosCambio + listaArticulosPedido.ElementAt(lineaCapitulo) + "|" + listaFechasPedido.ElementAt(lineaCapitulo).ToString() + ";";

                                oFechaBase = dtFechasBase.NewRow();
                                oFechaBase[0] = listaArticulosPedido.ElementAt(lineaCapitulo);
                                oFechaBase[1] = Convert.ToDateTime(listaFechasPedido.ElementAt(lineaCapitulo).ToString());
                                dtFechasBase.Rows.Add(oFechaBase);

                                if (listaFechasPedido.ElementAt(lineaCapitulo) < fechaPrecios)
                                    fechaPrecios = listaFechasPedido.ElementAt(lineaCapitulo);
                            }
                        }

                        if (fechaPrecios != new DateTime())
                        {
                            hora_1 = DateTime.Now;
                            strConexion = ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString();
                            conexion = new SqlConnection(strConexion);
                            conexion.Open();
                            comando = conexion.CreateCommand();
                            comando.CommandText = "sp_ROP_FechaBaseOfertaIncluir";
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.Parameters.AddWithValue("@FBO_Oferta", numeros);
                            comando.Parameters.AddWithValue("@FBO_Capitulo", IDCapitulo);
                            comando.Parameters.AddWithValue("@FBO_Fecha", fechaPrecios);
                            comando.Parameters.AddWithValue("@FBO_Articulo", articulos.Replace("'", ""));
                            comando.Parameters.AddWithValue("@FBO_TablaFechaSeleccionada", tabla);
                            comando.Parameters.AddWithValue("@FBO_CampoFechaSeleccionada1", campo);
                            comando.Parameters.AddWithValue("@FBO_DiasDiferencia", dias);
                            comando.Parameters.AddWithValue("@primeraVez", primeraVez);
                            comando.ExecuteNonQuery();
                            conexion.Close();
                            primeraVez = false;
                            hora_2 = DateTime.Now.Subtract(hora_1);
                            oItemTiempo = dtTiempos.NewRow();
                            oItemTiempo[0] = "TIEMPO llamar al SQL sp_ROP_FechaBaseOfertaIncluir";
                            oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                            dtTiempos.Rows.Add(oItemTiempo);

                            dondeVa = "localizar precios artículos";

                            dtArticulos = funcionalidades.localizarPreciosArticulos(ref dtTiempos, empresa, filaCabecera["DELEGACION"].ToString(), articulos, moneda, articulosCambio, "", usuario, version, "", usuarioRed);
                            hayPrecio = true;
                        }
                        else
                        {
                            dtArticulos = funcionalidades.localizarPreciosArticulos(ref dtTiempos, empresa, filaCabecera["DELEGACION"].ToString(), articulos, moneda, articulosCambio, "", usuario, version, "", usuarioRed);
                            hayPrecio = true;
                        }

                        // GASTOS OFICINA 
                        if (dtDatosGastosOficina.Rows.Count == 0)
                        {
                            dtDatosGastosOficina = funcionalidades.gastosOficina(ref dtTiempos, empresa, masterObra, fechaPrecios, moneda, mesPresupuestoValido, porcentajeGastoAdmministrativoVariable, porcentajeGastoComercialVariable, usuario, "", usuarioRed);
                            foreach (DataRow fila in dtDatosGastosOficina.Rows)
                            {
                                dondeVa = "encontró precio coste dataset dtDatosGastosOficina";
                                totalAnual = Convert.ToDecimal(fila["fact_anual_empresa"]);
                                totalAdministrativo = Convert.ToDecimal(fila["total_admin"]);
                                varAdministrativo = Convert.ToDecimal(fila["var_admin"]);
                                totalComercial = Convert.ToDecimal(fila["total_comerc"]);
                                varComercial = Convert.ToDecimal(fila["var_comerc"]);
                                costeDT = Convert.ToDecimal(fila["costeDT"]);
                                preciosEnCero = Convert.ToInt32(fila["preciosEnCero"]);
                            }
                            if (preciosEnCero != 0)
                            {
                                funcionalidades.guardarIncidencia(empresa, "OFERTA", numeroOferta, "", "En el cálculo de coste gastos oficina DT hay precios en cero.", 0, usuario, User.Identity.Name.ToString());
                                Informe.mostrarMensaje("En el cálculo de coste gastos oficina DT hay precios en cero.");
                            }
                        }

                        // GASTOS FIJOS
                        if (dtDatosGastosFijos.Rows.Count == 0)
                        {
                            dtDatosGastosFijos = funcionalidades.gastosFijos(ref dtTiempos, empresa, gastosFijosPorFacturacion, masterObra, fechaPrecios, moneda, mesPresupuestoValido, porcentajeGastoAdmministrativoVariable, porcentajeGastoComercialVariable, coeficienteGastosFijosAlquilerVenta, usuario, "", usuarioRed);
                            foreach (DataRow fila in dtDatosGastosFijos.Rows)
                            {
                                dondeVa = "encontró precio coste dataset dtDatosGastosFijos";
                                fact_anual_empresa = Convert.ToDecimal(fila["fact_anual_empresa"]);
                                facturacion_alquiler = Convert.ToDecimal(fila["facturacion_alquiler"]);
                                facturacion_venta = Convert.ToDecimal(fila["facturacion_venta"]);
                                total_admin = Convert.ToDecimal(fila["total_admin"]);
                                var_admin = Convert.ToDecimal(fila["var_admin"]);
                                total_comerc = Convert.ToDecimal(fila["total_comerc"]);
                                var_comerc = Convert.ToDecimal(fila["var_comerc"]);
                                total_gf_bu = Convert.ToDecimal(fila["total_gf_bu"]);
                                total_gf_hq = Convert.ToDecimal(fila["total_gf_hq"]);
                                KVBU = Convert.ToDecimal(fila["KVBU"]);
                                KABU = Convert.ToDecimal(fila["KABU"]);
                                KVHQ = Convert.ToDecimal(fila["KVHQ"]);
                                KAHQ = Convert.ToDecimal(fila["KAHQ"]);
                            }
                        }

                        DateTime horaDatosConfigurados_1 = DateTime.Now;
                        dtGastosPatio = funcionalidades.gastosPatio(ref dtTiempos, empresa, articulos, fechaPrecios, moneda, usuario, "", usuarioRed);
                        dtOcupacion = funcionalidades.ocupacion(ref dtTiempos, version, articulos, masterObra, false, fechaPrecios, "", usuarioRed);
                        funcionalidades.ajustesBU(ref dtTiempos, ref dtAjustesDatosBU, ref dtAjustesBU, masterObra, fechaPrecios, moneda, articulos, version, false, "", usuarioRed);
                        foreach (DataRow fila in dtAjustesBU.Rows)
                        {
                            dondeVa = "datos ajustesBU";
                            cumplimientoBajoAfectaGFCentral = Convert.ToString(fila["CFGAJU_CumplimientoBajoAfectaGFCentral"]);
                            cumplimientoBajoAfectaGFBU = Convert.ToString(fila["CFGAJU_CumplimientoBajoAfectaGFBU"]);
                            cumplimientoAltoAfectaGFCentral = Convert.ToString(fila["CFGAJU_CumplimientoAltoAfectaGFCentral"]);
                            cumplimientoAltoAfectaGFBU = Convert.ToString(fila["CFGAJU_CumplimientoAltoAfectaGFBU"]);
                            cumplimientoBajoPorcentaje = Convert.ToDecimal(fila["CFGAJU_CumplimientoBajoPorcentaje"]) / 100;
                            bonificacionCumplimientoBajoPorcentaje = Convert.ToDecimal(fila["CFGAJU_BonificacionCumplimientoBajoPorcentaje"]) / 100;
                            cumplimientoAltoPorcentaje = Convert.ToDecimal(fila["CFGAJU_CumplimientoAltoPorcentaje"]) / 100;
                            bonificacionCumplimientoAltoPorcentaje = Convert.ToDecimal(fila["CFGAJU_BonificacionCumplimientoAltoPorcentaje"]) / 100;
                        }

                        TimeSpan horaDatosConfigurados_2 = DateTime.Now.Subtract(horaDatosConfigurados_1);
                        DateTime horaDatosPase2_1 = DateTime.Now;

                        // RECUPERAR LOS DATOS DE CONFIGURACIÓN: TAXIMETRO - GASTOS - AJUSTES 
                        dondeVa = "ROP_DatosConfiguracion";
                        hora_1 = DateTime.Now;
                        dtDatosConfiguracion = new System.Data.DataTable();
                        conexion = new SqlConnection(strConexion);
                        conexion.Open();
                        comando = conexion.CreateCommand();
                        comando.CommandText = "ROP_DatosConfiguracion";
                        comando.CommandTimeout = 240000;
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@empresa", empresa);
                        comando.Parameters.AddWithValue("@usuario", usuario); // Environment.UserName);
                        if (cmbVersion.SelectedValue == "")
                            comando.Parameters.AddWithValue("@CFG_ID", null);
                        else if (Convert.ToInt32(cmbVersion.SelectedValue) == -1)
                            comando.Parameters.AddWithValue("@CFG_ID", null);
                        else
                            comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(cmbVersion.SelectedValue));
                        comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(filaCabecera["FECHA"]));
                        comando.Parameters.AddWithValue("@moneda", moneda);
                        comando.Parameters.AddWithValue("@articulos", articulosConfiguracion);
                        if (idioma == "INGLES")
                            comando.Parameters.AddWithValue("@idioma", "IN");
                        else
                            comando.Parameters.AddWithValue("@idioma", "ES");
                        adaptadorDatosConfiguracion = new SqlDataAdapter(comando);
                        adaptadorDatosConfiguracion.Fill(dtDatosConfiguracion);
                        adaptadorDatosConfiguracion.Dispose();
                        comando.Dispose();
                        conexion.Close();
                        conexion.Dispose();
                        hora_2 = DateTime.Now.Subtract(hora_1);
                        oItemTiempo = dtTiempos.NewRow();
                        oItemTiempo[0] = "TIEMPO llamar al SQL ROP_DatosConfiguracion";
                        oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                        dtTiempos.Rows.Add(oItemTiempo);

                        filasCapitulo = dtCapitulosOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "'");
                        foreach (DataRow filaCapitulo in filasCapitulo)
                        {
                            filasLinea = dtLineasOferta.Select("OFERTA = '" + filaCabecera["OFERTA"].ToString() + "' AND CAPITULO = '" + filaCapitulo["CAPITULO"].ToString() + "'");
                            if (filasLinea.Length == 0)
                                datosGenerales = "NO HAY DATOS DE LAS LÍNEAS";
                            else
                            {
                                tituloCapitulo = filaCapitulo["CAPITULO"].ToString();

                                importeAlquilerCapitulo = 0;
                                importeVentaCapitulo = 0;

                                importePorte = 0;

                                importeFacturacionAlquilerCapitulos = 0;
                                importeFacturacionVentaCapitulos = 0;
                                dec_importeCosteVCH = 0;
                                importeFacturacionVentaDepartamentoTecnicoCapitulos = 0;
                                importeFacturacionVentaFenolicoNuevoCapitulos = 0;
                                importeFacturacionVentaUneCifCapitulos = 0;
                                importeFacturacionVentaMontajesCapitulos = 0;
                                importeFacturacionVentaProductoCapitulos = 0;
                                importeCosteVentaDepartamentoTecnicoCapitulos = 0;
                                importeFacturacionVentaOtrosServiciosCapitulos = 0;
                                importeCosteVentaOtrosServiciosCapitulos = 0;

                                importeGastosVariablesEntregaDevolucionAlquilerCapitulos = 0;
                                importeGastosVariablesEntregaDevolucionVentaCapitulos = 0;
                                importeGastosVariablesClaisificacionReparacionAlquilerCapitulos = 0;
                                importeGastosVariablesClaisificacionReparacionVentaCapitulos = 0;
                                importeCosteVentaFenolicoNuevoCapitulos = 0;
                                importeGastosVariablesVentaFenolicoNuevoCapitulos = 0;
                                importeGastosFijosBUVentaFenolicoNuevoCapitulos = 0;
                                importeGastosFijosCentralesVentaFenolicoNuevoCapitulos = 0;
                                importeCosteVentaNuevoCapitulos = 0;
                                importeCosteVentaUneCifCapitulos = 0;
                                importeGastosVariablesVentaUneCifCapitulos = 0;
                                importeGastosFijosBUVentaUneCifCapitulos = 0;
                                importeGastosFijosCentralesVentaUneCifCapitulos = 0;

                                importeCosteVentaMontajesCapitulos = 0;
                                importeGastosVariablesVentaMontajesCapitulos = 0;
                                importeGastosFijosBUVentaMontajesCapitulos = 0;
                                importeGastosFijosCentralesVentaMontajesCapitulos = 0;

                                importeCosteVentaCapitulos = 0;
                                importeCosteTaximetroNoConsumibleCapitulos = 0;
                                importeCosteTaximetroConsumibleCapitulos = 0;
                                importeCosteBonificacionTaximetroConsumibleCapitulos = 0;

                                importeAjustesObraDuracionGFCapitulo = 0;
                                importeAjustesObraDuracionTaxCapitulo = 0;
                                importeAjustesProductoTaxCapitulo = 0;
                                importeGastosFinanciacionCapitulos = 0;

                                totalGastosFijosBU = 0;
                                totalGastosFijosCentral = 0;

                                pesoCapitulo = 0;
                                importePorte_TR001 = 0;
                                //hayTR001 = false;
                                existeTR001 = false; ;
                                foreach (DataRow filaLinea in filasLinea)
                                {
                                    dondeVa = "Fenólico";
                                    if ((bool)Session["datosFenolicosOriginales"])
                                    {
                                        if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                        {
                                            chkBoxFenolico.Checked = true;
                                            calcularConFenolico = true;
                                        }
                                    }
                                    else
                                    {
                                        calcularConFenolico = chkBoxFenolico.Checked;
                                    }

                                    // GASTOS FIJOS
                                    oItemGastosFijos = dtGastosFijos.NewRow();
                                    oItemGastosFijos["FACTURACION ALQUILER"] = 0;
                                    oItemGastosFijos["FACTURACION VENTA"] = 0;
                                    oItemGastosFijos["COSTE ALQUILER"] = 0;
                                    oItemGastosFijos["COSTE VENTA"] = 0;

                                    oItemGastosFijos["fact_anual_empresa"] = fact_anual_empresa;
                                    oItemGastosFijos["facturacion_alquiler"] = facturacion_alquiler;
                                    oItemGastosFijos["facturacion_venta"] = facturacion_venta;
                                    oItemGastosFijos["total_admin"] = total_admin;
                                    oItemGastosFijos["var_admin"] = var_admin;

                                    oItemGastosFijos["total_comerc"] = fact_anual_empresa;
                                    oItemGastosFijos["var_comerc"] = facturacion_alquiler;
                                    oItemGastosFijos["total_gf_bu"] = facturacion_venta;
                                    oItemGastosFijos["total_gf_hq"] = total_admin;
                                    oItemGastosFijos["KVBU"] = KVBU;
                                    oItemGastosFijos["KABU"] = KABU;
                                    oItemGastosFijos["KVHQ"] = KVHQ;
                                    oItemGastosFijos["KAHQ"] = KAHQ;

                                    BUimporteLinea = 0;
                                    centralImporteLinea = 0;
                                    importeVentaLinea = 0;
                                    importeAlquilerLinea = 0;
                                    porcentajeAjustesObraDuracionGFCapitulo = 0;

                                    dondeVa = "* recorrer productos " + filaLinea["ITEM"].ToString();

                                    if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) >= 0)
                                        //hayTR001 = true;
                                        existeTR001 = true;

                                    DataRow oItemTaximetro = dtTaximetro.NewRow();
                                    oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = "0";
                                    oItemTaximetro["IMPORTE TAX CONSUMIBLE"] = "0";
                                    oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()).ToString("#,##0.00");
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
                                    tipoServicio = "";

                                    precioCoste = 0;
                                    if (hayPrecio)
                                    {
                                        dondeVa = "buscar precio dataset";
                                        filaEncontrada = dtArticulos.Select("Articulo = '" + filaLinea["ITEM"].ToString().Trim() + "'");
                                        if (filaEncontrada.Count() == 0 && codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) == -1)
                                        {
                                            funcionalidades.guardarIncidencia(empresa, "OFERTA", numeroOferta, filaLinea["ITEM"].ToString(), "No hay PS del artículo.", 0, usuario, User.Identity.Name.ToString());
                                            Informe.mostrarMensaje("(PED_1) No se ha localizado el precio del articulo '" + filaLinea["ITEM"].ToString() + "'");
                                        }
                                        foreach (DataRow fila in filaEncontrada)
                                        {
                                            dondeVa = "encontró precio coste dataset // " + articulosCambio + " // ";
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["Precio"])))
                                            {
                                                precioCoste = Convert.ToDecimal(fila["Precio"]);
                                                oItemTaximetro[COL_DATA_PRECIO_CAMBIO] = Convert.ToDecimal(fila["Precio"]).ToString("#,##0.00");
                                            }
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

                                    // gastos variables patio
                                    clasificarRepararDirecto = 0;
                                    clasificarRepararIndirecto = 0;
                                    clasificarDirecto = 0;
                                    clasificarIndirecto = 0;
                                    repararDirecto = 0;
                                    repararIndirecto = 0;
                                    entrega = 0;
                                    devolucion = 0;
                                    naviera = 0;
                                    string aafBuscar = "";

                                    aafBuscar = filaLinea["ITEM"].ToString();
                                    filaEncontrada = dtGastosPatio.Select("ProductId = '" + filaLinea["ITEM"].ToString() + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["AAF"])))
                                            aafBuscar = Convert.ToString(fila["AAF"]);
                                    }

                                    //filaEncontrada = dtGastosPatio.Select("ProductId = '" + filaLinea["ITEM"].ToString() + "'");
                                    filaEncontrada = dtGastosPatio.Select("ProductId = '" + aafBuscar + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Entrega"])))
                                            entrega = Convert.ToDecimal(fila["Entrega"]);
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Devolucion"])))
                                            devolucion = Convert.ToDecimal(fila["Devolucion"]);
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["Naviera"])))
                                            naviera = Convert.ToDecimal(fila["Naviera"]);

                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarReparar"])))
                                        {
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarRepararDirecto"])))
                                                clasificarRepararDirecto = Convert.ToDecimal(fila["ClasificarRepararDirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarRepararIndirecto"])))
                                                clasificarRepararIndirecto = Convert.ToDecimal(fila["ClasificarRepararIndirecto"]);
                                        }
                                        else
                                        {
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarDirecto"])))
                                                clasificarDirecto = Convert.ToDecimal(fila["ClasificarDirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["ClasificarIndirecto"])))
                                                clasificarIndirecto = Convert.ToDecimal(fila["ClasificarIndirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["RepararDirecto"])))
                                                repararDirecto = Convert.ToDecimal(fila["RepararDirecto"]);
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["RepararIndirecto"])))
                                                repararIndirecto = Convert.ToDecimal(fila["RepararIndirecto"]);
                                        }
                                        precioGastosPatio = Convert.ToDecimal(fila["Precio"]);
                                    }

                                    // recuperar datos de configuración del datatable
                                    dondeVa = "";
                                    listaBonificacionesDesde.Clear();
                                    listaBonificacionesHasta.Clear();
                                    listaBonificacionesValor.Clear();
                                    diasDesdeTaxNoConsumible = 0;
                                    diasHastaTaxNoConsumible = 0;
                                    pesoArticulo = 0;
                                    vocacionChatarra = 0;
                                    filaEncontrada = dtDatosConfiguracion.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                    foreach (DataRow fila in filaEncontrada)
                                    {
                                        dondeVa = "encontró configuracion dataset // " + filaLinea["ITEM"].ToString() + " // ";

                                        dondeVa = "Peso del artículo";
                                        pesoArticulo = Convert.ToDecimal(fila["WeightVolumeKg"]);
                                        vocacionChatarra = Convert.ToInt32(fila["SpecialItemId"]);
                                        dondeVa = "encontró configuracion dataset // Tipo Servicio ";
                                        if (!String.IsNullOrEmpty(Convert.ToString(fila["CFGSERV_Tipo"])))
                                            tipoServicio = Convert.ToString(fila["CFGSERV_Tipo"]);
                                        dondeVa = "encontró configuracion dataset // Coste superficie";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE SUPERFICIE")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            costeSuperficie = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        dondeVa = "encontró configuracion dataset // Taxímetro consumible";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "TAXÍMETRO CONSUMIBLE")
                                        {
                                            taximetroConsumible = (Convert.ToDecimal(fila["Valor"]) / 100);
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
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "TAXÍMETRO NO CONSUMIBLE" && (calcularPorSuperficie || taximetroConsumible == 0))
                                        {
                                            dondeVa = "encontró configuracion dataset // Valor tax no consumible";
                                            taximetroNoConsumible = (Convert.ToDecimal(fila["Valor"]) / 100);
                                            diasDesdeTaxNoConsumible = Convert.ToInt32(fila["Desde"]);
                                            diasHastaTaxNoConsumible = Convert.ToInt32(fila["Hasta"]);
                                        }

                                        dondeVa = "encontró configuracion dataset // Meters";
                                        metersInvoicing = Convert.ToBoolean(fila["MetersInvoicing"]);
                                        dondeVa = "encontró configuracion dataset // Tipo producto";
                                        productType = Convert.ToString(fila["ProductTypeName_es"]);

                                        dondeVa = "encontró configuracion dataset // Correcion PS Nuevo";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COEF. CORRECCIÓN PS NUEVO")
                                            coeficienteNuevo = Convert.ToDecimal(fila["Valor"]);
                                        dondeVa = "encontró configuracion dataset // Correcion PS Usado";
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COEF. CORRECCIÓN PS USADO")
                                        {
                                            if (Convert.ToString(fila["Valorar"]).ToUpper() == "")
                                            {
                                                coeficienteUsadoVenta = Convert.ToDecimal(fila["Valor"]);
                                                coeficienteUsadoMPOLiquidacion = Convert.ToDecimal(fila["Valor"]);
                                                coeficienteUsadoChatarra = Convert.ToDecimal(fila["Valor"]);
                                            }
                                            else if (Convert.ToString(fila["Valorar"]).ToUpper() == "VENTA")
                                                coeficienteUsadoVenta = Convert.ToDecimal(fila["Valor"]);
                                            else if (Convert.ToString(fila["Valorar"]).ToUpper() == "MPO_LIQ")
                                                coeficienteUsadoMPOLiquidacion = Convert.ToDecimal(fila["Valor"]);
                                            else if (Convert.ToString(fila["Valorar"]).ToUpper() == "CHATARRA")
                                                coeficienteUsadoChatarra = Convert.ToDecimal(fila["Valor"]);
                                        }
                                        dondeVa = "encontró configuracion dataset // Correcion PS servicio";
                                        //if (Convert.ToString(fila["Concepto"]) == "Coef. Corrección PS Servicio")
                                        //    coeficienteServicio = Convert.ToDecimal(fila["Valor"]);
                                        dondeVa = "encontró configuracion dataset // Tipo artículo " + filaLinea["ITEM"].ToString();
                                        tipoArticulo = Convert.ToString(fila["TipoArticulo"]);
                                        tipoArticuloFenolico = Convert.ToString(fila["tipoArticuloFenolico"]);

                                        dondeVa = "encontró configuracion dataset // Porcentaje coste material nuevo " + filaLinea["ITEM"].ToString();
                                        // Fenólico / Nuevo
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE (%) ENTREGA MATERIAL NUEVO")
                                            porcentajeCosteMaterialNuevo = Convert.ToDecimal(fila["Valor"]) / 100;
                                        dondeVa = "encontró configuracion dataset // Coste fenólico nuevo estandar " + filaLinea["ITEM"].ToString();
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE FENÓLICO NUEVO ESTÁNDAR")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            costeFenolicoNuevoEstandar = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        dondeVa = "encontró configuracion dataset // Coste fenólico nuevo especial " + filaLinea["ITEM"].ToString();
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "COSTE FENÓLICO NUEVO ESPECIAL")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            costeFenolicoNuevoEspecial = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "CANON FENÓLICO NUEVO PANEL ESTÁNDAR")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            canonFenolicoNuevoEstandar = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                        dondeVa = "encontró configuracion dataset // Canon fenólico nuevo panel especial  " + filaLinea["ITEM"].ToString();
                                        if (Convert.ToString(fila["Concepto"]).ToUpper() == "CANON FENÓLICO NUEVO PANEL ESPECIAL")
                                        {
                                            cambio = Convert.ToDecimal(fila["Cambio"]);
                                            canonFenolicoNuevoEspecial = Convert.ToDecimal(fila["Valor"]) * cambio;
                                        }
                                    }

                                    dondeVa = "salio configuracion dataset";

                                    dondeVa = "Artículo nuevo/usado";

                                    importeCosteMaterialNuevo = 0;
                                    importeCosteMaterialFenolico = 0;

                                    // Vocacion chatarra
                                    if (vocacionChatarra == 2)
                                        dec_importeCosteVCH = dec_importeCosteVCH + (Convert.ToDecimal(filaLinea["CANTIDAD"]) * precioCoste);
                                    else
                                    {
                                        if (tipoArticulo == "NUEVO")
                                        {
                                            coeficienteUsar = coeficienteNuevo;
                                            oItemTaximetro[COL_DATA_PRECIO_CORRECION_NUEVO] = (precioCoste * coeficienteNuevo).ToString("#,##0.00");
                                        }
                                        if (tipoArticulo == "USADO")
                                        {
                                            coeficienteUsar = coeficienteUsadoChatarra;
                                            oItemTaximetro[COL_DATA_PRECIO_CORRECION_USADO] = (precioCoste * coeficienteUsadoChatarra).ToString("#,##0.00");
                                        }
                                    }

                                    dondeVa = "Guarda datos taxímetro";
                                    oItemTaximetro[COL_DATA_FECHA_DOC] = filaCabecera["FECHA"].ToString();
                                    oItemTaximetro[COL_DATA_FECHA_CAPITULO] = filaCapitulo["FECHA"].ToString();
                                    oItemTaximetro[COL_DATA_CAPITULO] = tituloCapitulo;
                                    oItemTaximetro[COL_DATA_ITEM] = filaLinea["ITEM"].ToString();
                                    oItemTaximetro[COL_DATA_TIPO_ITEM] = tipoArticulo;
                                    oItemTaximetro[COL_DATA_IMPORTE_ALQUILER] = 0;
                                    oItemTaximetro[COL_DATA_IMPORTE_VENTA] = 0;

                                    oItemTaximetro[COL_DATA_PRECIO] = precioCoste.ToString("#,##0.00");
                                    oItemTaximetro[COL_DATA_FECHA] = fechaPrecio.ToString("dd/MM/yyyy");
                                    oItemTaximetro[COL_DATA_TIPO_CAPITULO] = filaCapitulo["TIPO"].ToString();
                                    oItemTaximetro[COL_DATA_PRECIO_GASTOS_PATIO] = precioGastosPatio.ToString("##0.00");

                                    if (filaCapitulo["TIPO"].ToString() == "RENTAL")
                                    {
                                        dondeVa = "Fenólico / material nuevo";
                                        if (tipoArticuloFenolico == "NUEVO" || tipoArticuloFenolico == "MIXTO")
                                        {
                                            filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                            foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                            {
                                                if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                    importeCosteMaterialFenolico = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * costeFenolicoNuevoEstandar);
                                                else
                                                    importeCosteMaterialFenolico = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * costeFenolicoNuevoEspecial);
                                            }
                                        }
                                        if (tipoArticuloFenolico == "NUEVO")
                                            importeCosteMaterialNuevo = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * (precioCoste * coeficienteNuevo) - importeCosteMaterialFenolico) * porcentajeCosteMaterialNuevo;

                                        oItemFenolicoNuevo = dtFenolicoNuevo.NewRow();
                                        oItemFenolicoNuevo["ITEM"] = filaLinea["ITEM"].ToString();
                                        oItemFenolicoNuevo["TIPO"] = tipoArticuloFenolico;
                                        oItemFenolicoNuevo["PS"] = precioCoste;
                                        oItemFenolicoNuevo["CANTIDAD"] = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString());
                                        oItemFenolicoNuevo["COSTE PS"] = (precioCoste * coeficienteNuevo);
                                        oItemFenolicoNuevo["COSTE FENOLICO ESTANDAR"] = costeFenolicoNuevoEstandar;
                                        oItemFenolicoNuevo["COSTE FENOLICO ESPECIAL"] = costeFenolicoNuevoEspecial;
                                        oItemFenolicoNuevo["METROS"] = superficie;
                                        oItemFenolicoNuevo["COSTE FENOLICO"] = importeCosteMaterialFenolico;
                                        oItemFenolicoNuevo["% COSTE MATERIAL NUEVO"] = porcentajeCosteMaterialNuevo;
                                        oItemFenolicoNuevo["COSTE MATERIAL NUEVO"] = importeCosteMaterialNuevo;
                                        dtFenolicoNuevo.Rows.Add(oItemFenolicoNuevo);

                                        importeCosteVentaFenolicoNuevoCapitulos = importeCosteVentaFenolicoNuevoCapitulos + importeCosteMaterialFenolico;
                                    }

                                    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + importeCosteMaterialFenolico;

                                    if (filaCapitulo["TIPO"].ToString() == "SALES")
                                    {
                                        dondeVa = "VENTAS";

                                        dondeVa = "VENTAS: Gastos variables";
                                        importeGastosPatioEntregaDevolucionVentas = 0;
                                        importeGastosPatioEntregaDevolucionVentas = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio;
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                            {
                                                importeGastosPatioEntregaDevolucionVentas = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio;
                                                //oItemTaximetro["NAVIERA"] = naviera.ToString("##0.00");
                                            }
                                        importeGastosVariablesEntregaDevolucionVentaCapitulos = importeGastosVariablesEntregaDevolucionVentaCapitulos + importeGastosPatioEntregaDevolucionVentas;

                                        oItemTaximetro[COL_DATA_VENTAS_ENTREGA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_VENTAS_DEVOLUCION] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_ENTREGA_NAVIERA] = "0.00";
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                                oItemTaximetro[COL_DATA_VENTAS_ENTREGA_NAVIERA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio).ToString("##0.00");

                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_REPARAR_DIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_REPARAR_INDIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_DIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_CLASIFICAR_INDIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_REPARAR_DIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_REPARAR_INDIRECTO] = "0.00";
                                        oItemTaximetro[COL_DATA_VENTAS_GASTOS_PATIO] = importeGastosPatioEntregaDevolucionVentas.ToString("##0.00");

                                        dondeVa = "VENTAS: Guardar coeficiente nuevo/usado";
                                        if (tipoArticulo == "NUEVO")
                                            oItemTaximetro[COL_DATA_COEFICIENTE_NUEVO] = coeficienteNuevo.ToString("##0.00");
                                        if (tipoArticulo == "USADO")
                                            oItemTaximetro[COL_DATA_COEFICIENTE_USADO] = coeficienteUsadoVenta.ToString("##0.00");

                                        dondeVa = "VENTAS: Calcular importe venta";
                                        importeVenta = importeVenta + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                        importeVentaCapitulo = importeVentaCapitulo + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                        importeVentaLinea = Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                        importeFacturacionVentaCapitulos = importeFacturacionVentaCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());

                                        if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) < 0)
                                        {
                                            // mail 29/07/2021 Nacho: configuración manda sobre tipo producto
                                            if (tipoServicio.ToUpper() == "DPTO_TECNICO")
                                            {
                                                importeFacturacionVentaDepartamentoTecnicoCapitulos = importeFacturacionVentaDepartamentoTecnicoCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                importeCosteVentaDepartamentoTecnicoCapitulos = importeCosteVentaDepartamentoTecnicoCapitulos + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;
                                                oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar);
                                            }
                                            else if (tipoServicio.ToUpper() == "MONTAJES")
                                            {
                                                importeFacturacionVentaMontajesCapitulos = importeFacturacionVentaMontajesCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                // COSTE
                                                importeCosteVentaMontajesCapitulos = importeCosteVentaMontajesCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * precioCoste;
                                                oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + (Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * precioCoste);
                                            }
                                            else if (productType.Trim().ToUpper() == "SERVICIO")
                                            {
                                                dondeVa = "VENTAS: Servicio";
                                                // SVP/CIF
                                                if (Convert.ToDecimal(filaLinea["CANON IMPORTE"]) != 0)
                                                {
                                                    importeFacturacionVentaUneCifCapitulos = importeFacturacionVentaUneCifCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                    // COSTE
                                                    importeCosteVentaUneCifCapitulos = importeCosteVentaUneCifCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                                    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                                }
                                                // OTROS SERVICIOS
                                                else
                                                {
                                                    importeFacturacionVentaOtrosServiciosCapitulos = importeFacturacionVentaOtrosServiciosCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                    // COSTE
                                                    importeCosteVentaOtrosServiciosCapitulos = importeCosteVentaOtrosServiciosCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                                    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                                }
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA_SERVICIO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                            }
                                            else
                                            {
                                                dondeVa = "VENTAS: Producto";
                                                importeFacturacionVentaProductoCapitulos = importeFacturacionVentaProductoCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA_PRODUCTO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");

                                                dondeVa = "Coste venta";
                                                if (tipoArticuloFenolico == "MIXTO")
                                                {
                                                    decimal costeFenolico = 0;
                                                    decimal costePS = 0;
                                                    decimal costeMixto = 0;
                                                    filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                                    foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                                    {
                                                        if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                            costeFenolico = superficie * costeFenolicoNuevoEstandar;
                                                        else
                                                            costeFenolico = superficie * costeFenolicoNuevoEspecial;
                                                    }
                                                    costePS = precioCoste;
                                                    costeMixto = ((costePS - costeFenolico) * coeficienteUsar) + costeFenolico;
                                                    importeCosteVentaCapitulos = importeCosteVentaCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * costeMixto);
                                                }
                                                else
                                                    importeCosteVentaCapitulos = importeCosteVentaCapitulos + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;

                                                oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;
                                            }

                                            // Fenólico
                                            if (calcularConFenolico)
                                            {
                                                if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                                    importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                else
                                                {

                                                    filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                                    foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                                    {
                                                        if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                            importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEstandar);
                                                        else
                                                            importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEspecial);
                                                    }
                                                }
                                            }
                                            oItemTaximetro[COL_DATA_IMPORTE_VENTA] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                        }
                                        dondeVa = "VENTAS: Datos en datatble taximetro";
                                        if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * (Convert.ToDecimal(diasPagoDefecto) / Convert.ToDecimal(365)));
                                        else
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));
                                    }

                                    //else 
                                    //{
                                    //    if (productType.Trim().ToUpper() == "SERVICIO")
                                    //    {
                                    //        dondeVa = "VENTAS: Servicio";
                                    //        // Fenólico
                                    //        if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                    //        {
                                    //            if (calcularConFenolico)
                                    //                importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                    //        }
                                    //        // SVP/CIF
                                    //        else if (Convert.ToDecimal(filaLinea["CANON IMPORTE"]) != 0)
                                    //        {
                                    //            importeFacturacionVentaUneCifCapitulos = importeFacturacionVentaUneCifCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    //            // COSTE
                                    //            importeCosteVentaUneCifCapitulos = importeCosteVentaUneCifCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                    //            oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste) * (Convert.ToDecimal(filaLinea["CANON"]) / 100));
                                    //        }
                                    //        // OTROS SERVICIOS
                                    //        else
                                    //        {
                                    //            importeFacturacionVentaOtrosServiciosCapitulos = importeFacturacionVentaOtrosServiciosCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    //            // COSTE
                                    //            importeCosteVentaOtrosServiciosCapitulos = importeCosteVentaOtrosServiciosCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                    //            oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste));
                                    //        }
                                    //        oItemTaximetro[COL_DATA_IMPORTE_VENTA_SERVICIO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                    //    }
                                    //}
                                    //if (productType.Trim().ToUpper() != "SERVICIO")
                                    //{
                                    //    dondeVa = "VENTAS: Producto";
                                    //    importeFacturacionVentaProductoCapitulos = importeFacturacionVentaProductoCapitulos + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    //    oItemTaximetro[COL_DATA_IMPORTE_VENTA_PRODUCTO] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");

                                    //    dondeVa = "Coste venta";
                                    //    importeCosteVentaCapitulos = importeCosteVentaCapitulos + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;

                                    //    oItemGastosFijos["COSTE VENTA"] = Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]) + Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioCoste * coeficienteUsar;
                                    //}
                                    //        oItemTaximetro[COL_DATA_IMPORTE_VENTA] = Convert.ToDecimal(filaLinea["IMPORTE"].ToString()).ToString("#,##0.00");
                                    //    }
                                    //    dondeVa = "VENTAS: Datos en datatble taximetro";
                                    //    importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * Convert.ToDecimal(filaLinea["IMPORTE"].ToString()) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));
                                    //}

                                    dondeVa = "Alquiler";
                                    if (filaCapitulo["TIPO"].ToString() == "RENTAL")
                                    {
                                        // SVP/CIF
                                        if (Convert.ToDecimal(filaLinea["CANON IMPORTE"]) != 0)
                                        {
                                            oItemTaximetro[COL_DATA_IMPORTE_VENTA] = ((Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_VENTA].ToString())) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)).ToString("#,##0.00");
                                            importeVenta = importeVenta + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            importeVentaCapitulo = importeVentaCapitulo + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            importeVentaLinea = importeVentaLinea + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            importeFacturacionVentaUneCifCapitulos = importeFacturacionVentaUneCifCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100);
                                            if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                                importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)) * (Convert.ToDecimal(diasPagoDefecto) / Convert.ToDecimal(365));
                                            else
                                                importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));

                                            //importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["PRECIO"].ToString()) * Convert.ToDecimal(filaLinea["CANON"])) / 100)) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));
                                        }
                                        //if (calcularConFenolico && filaLinea["CANON FENOLICO"].ToString().HasValue && filaLinea["CANON FENOLICO"].ToString() != 0)
                                        if (calcularConFenolico)
                                        {
                                            if (Convert.ToDecimal(filaLinea["CANON FENOLICO"]) != 0)
                                            {
                                                oItemTaximetro[COL_DATA_IMPORTE_VENTA] = ((Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_VENTA].ToString())) + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))).ToString("#,##0.00");
                                                importeVenta = importeVenta + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                importeVentaCapitulo = importeVentaCapitulo + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                importeVentaLinea = importeVentaLinea + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()));
                                                if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                                    importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))) * ((diasPagoDefecto) / Convert.ToDecimal(365));
                                                else
                                                    importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));

                                                //importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["CANON FENOLICO"].ToString()))) * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365));
                                            }
                                            else
                                            {
                                                filaEncontradaFenolico = dtArticulosPaneles.Select("Itemid = '" + filaLinea["ITEM"].ToString() + "'");
                                                foreach (DataRow filaFenolico in filaEncontradaFenolico)
                                                {
                                                    if (Convert.ToBoolean(filaFenolico["Estandar"]))
                                                        importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEstandar);
                                                    else
                                                        importeFacturacionVentaFenolicoNuevoCapitulos = importeFacturacionVentaFenolicoNuevoCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * superficie * canonFenolicoNuevoEspecial);
                                                }
                                            }
                                        }
                                        dondeVa = "ALQUILER";
                                        importeAlquiler = importeAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));
                                        importeAlquilerCapitulo = importeAlquilerCapitulo + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));
                                        importeAlquilerLinea = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));
                                        importeFacturacionAlquilerCapitulos = importeFacturacionAlquilerCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString()));

                                        oItemGastosFijos["FACTURACION ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["FACTURACION ALQUILER"]) + importeAlquilerLinea;

                                        if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * importeAlquilerLinea * ((diasPagoDefecto) / Convert.ToDecimal(365)));
                                        else
                                            importeGastosFinanciacionCapitulos = importeGastosFinanciacionCapitulos + (condicionesAplicar * importeAlquilerLinea * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));

                                        dondeVa = "ALQUILER: Gastos variables";
                                        importeGastosPatioEntregaDevolucionAlquiler = 0;
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                        {
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                            {
                                                importeGastosPatioEntregaDevolucionAlquiler = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio;
                                                importeGastosPatioEntregaDevolucionAlquiler = importeGastosPatioEntregaDevolucionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * naviera * precioGastosPatio);
                                            }
                                            else
                                            {
                                                importeGastosPatioEntregaDevolucionAlquiler = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio;
                                                importeGastosPatioEntregaDevolucionAlquiler = importeGastosPatioEntregaDevolucionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * precioGastosPatio);

                                            }
                                        }
                                        else
                                        {
                                            importeGastosPatioEntregaDevolucionAlquiler = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio;
                                            importeGastosPatioEntregaDevolucionAlquiler = importeGastosPatioEntregaDevolucionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * precioGastosPatio);

                                        }
                                        importeGastosVariablesEntregaDevolucionAlquilerCapitulos = importeGastosVariablesEntregaDevolucionAlquilerCapitulos + importeGastosPatioEntregaDevolucionAlquiler;

                                        importeGastosPatioClasificacionReparacionAlquiler = 0;
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararDirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararIndirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarDirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarIndirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararDirecto * precioGastosPatio);
                                        importeGastosPatioClasificacionReparacionAlquiler = importeGastosPatioClasificacionReparacionAlquiler + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararIndirecto * precioGastosPatio);
                                        importeGastosVariablesClaisificacionReparacionAlquilerCapitulos = importeGastosVariablesClaisificacionReparacionAlquilerCapitulos + importeGastosPatioClasificacionReparacionAlquiler;

                                        oItemTaximetro[COL_DATA_ALQUILER_ENTREGA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * precioGastosPatio).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_DEVOLUCION] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * precioGastosPatio).ToString("##0.00");
                                        if (!String.IsNullOrEmpty(filaCabecera["MODO ENTREGA"].ToString()))
                                        {
                                            if (filaCabecera["MODO ENTREGA"].ToString() == "1")
                                            {
                                                oItemTaximetro[COL_DATA_ALQUILER_ENTREGA_NAVIERA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * entrega * naviera * precioGastosPatio).ToString("##0.00");
                                                oItemTaximetro[COL_DATA_ALQUILER_DEVOLUCION_NAVIERA] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * devolucion * naviera * precioGastosPatio).ToString("##0.00");
                                            }
                                        }
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_REPARAR_DIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararDirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_REPARAR_INDIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarRepararIndirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_DIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarDirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_CLASIFICAR_INDIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * clasificarIndirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_REPARAR_DIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararDirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_REPARAR_INDIRECTO] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * repararIndirecto).ToString("##0.00");
                                        oItemTaximetro[COL_DATA_ALQUILER_GASTOS_PATIO] = (importeGastosPatioEntregaDevolucionAlquiler + importeGastosPatioClasificacionReparacionAlquiler).ToString("##0.00");

                                        dondeVa = "ALQUILER: Datos en datatble taximetro";
                                        oItemTaximetro[COL_DATA_CANTIDAD] = Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()).ToString("#,##0.00");
                                        oItemTaximetro[COL_DATA_EURO_DIA] = Convert.ToDecimal(filaLinea["EURO DIA"].ToString()).ToString("#,##0.0000");
                                        oItemTaximetro[COL_DATA_DURACION_ESTIMADA] = Convert.ToDecimal(filaLinea["DURACION"].ToString()).ToString("#,##0.00");
                                        oItemTaximetro[COL_DATA_IMPORTE_ALQUILER] = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * Convert.ToDecimal(filaLinea["EURO DIA"].ToString()) * Convert.ToDecimal(filaLinea["DURACION"].ToString())).ToString("#,##0.00");

                                        if (vocacionChatarra != 2)
                                        {
                                            dondeVa = "Calcular datos taxímetros";
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
                                                        precioTaximetroNoConsumible = precioCoste; //(precioCoste - (costeSuperficie * superficie));
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

                                            dondeVa = "Calcular datos taxímetros: superficie";
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

                                            importeCosteTaximetroConsumible = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(filaLinea["DURACION"].ToString())) / diasxMes;
                                            importeCosteTaximetroConsumibleCapitulos = importeCosteTaximetroConsumibleCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(filaLinea["DURACION"].ToString())) / diasxMes;
                                            oItemGastosFijos["COSTE ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]) + importeCosteTaximetroConsumible;

                                            dondeVa = "Empieza recorrido bonificación";
                                            oItemTaximetro[COL_DATA_AJUSTE] = "";

                                            duracion = Convert.ToInt32(filaLinea["DURACION"].ToString());
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
                                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes).ToString("#,##0.00");
                                                        else
                                                            oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE] = (Convert.ToDecimal(oItemTaximetro[COL_DATA_AJUSTE_TAX_CONSUMIBLE]) + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes)).ToString("#,##0.00");
                                                        importeCosteBonificacionTaximetroConsumible = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);
                                                        importeCosteBonificacionTaximetroConsumibleCapitulos = importeCosteBonificacionTaximetroConsumibleCapitulos + ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * bonificacionTaximetroConsumible * diasMultiplicar) / diasxMes);

                                                        oItemGastosFijos["COSTE ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]) - importeCosteBonificacionTaximetroConsumible;
                                                    }
                                                }
                                                diasCalculados = diasCalculados + diasxMes;
                                            }
                                            dondeVa = "Fin recorrido bonificación";

                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_DESDE] = diasDesdeTaxNoConsumible.ToString();
                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX_DIAS_HASTA] = diasHastaTaxNoConsumible.ToString();
                                            oItemTaximetro[COL_DATA_IMPORTE_TAX] = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroConsumible * taximetroConsumible * Convert.ToDecimal(filaLinea["DURACION"].ToString())) / diasxMes).ToString("#,##0.00");

                                            if (Convert.ToInt32(filaLinea["DURACION"].ToString()) <= diasHastaTaxNoConsumible)
                                                diasTaximetroNoConsumibleCalcular = Convert.ToInt32(filaLinea["DURACION"].ToString()) - diasDesdeTaxNoConsumible;
                                            if (Convert.ToInt32(filaLinea["DURACION"].ToString()) >= diasHastaTaxNoConsumible)
                                                diasTaximetroNoConsumibleCalcular = diasHastaTaxNoConsumible - diasDesdeTaxNoConsumible;

                                            importeCosteTaximetroNoConsumible = (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                            importeCosteTaximetroNoConsumibleCapitulos = importeCosteTaximetroNoConsumibleCapitulos + (Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes;
                                            oItemTaximetro[COL_DATA_IMPORTE_NO_TAX] = ((Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()) * precioTaximetroNoConsumible * taximetroNoConsumible * diasTaximetroNoConsumibleCalcular) / diasxMes).ToString("#,##0.00");

                                            oItemGastosFijos["COSTE ALQUILER"] = Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]) + importeCosteTaximetroNoConsumible;
                                        }
                                    }

                                    oItemGastosFijos["FACTURACION VENTA"] = Convert.ToDecimal(oItemGastosFijos["FACTURACION VENTA"]) + importeVenta;

                                    //if ((importeCosteMaterialNuevo != 0) && (importeCosteMaterialFenolico != 0))
                                    importeCosteVentaNuevoCapitulos = importeCosteVentaNuevoCapitulos + (importeCosteMaterialNuevo + importeCosteMaterialFenolico);
                                    //else
                                    //    importeCosteVentaNuevoCapitulos = importeCosteVentaNuevoCapitulos + importeCosteMaterialNuevo;


                                    pesoCapitulo = pesoCapitulo + (pesoArticulo * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));
                                    pesoTotal = pesoTotal + (pesoArticulo * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));

                                    dondeVa = "Codigos transporte";
                                    if (codigosTransporte.IndexOf(filaLinea["ITEM"].ToString().ToUpper()) >= 0)
                                    {
                                        dondeVa = "VALIDAR TRANSPORTE TR-001";
                                        importePorte_TR001 = importePorte_TR001 + Convert.ToDecimal(filaLinea["IMPORTE"].ToString());
                                    }

                                    importePorte = importePorte + (valorCostePorte * Convert.ToDecimal(kilometros) * (pesoArticulo / 1000) * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));
                                    importeCostePorte = importeCostePorte + (valorCostePorte * Convert.ToDecimal(kilometros) * (pesoArticulo / 1000) * Convert.ToDecimal(filaLinea["CANTIDAD"].ToString()));

                                    // CALCULO GASTOS FIJOS Y AJUSTES DURACION OBRA POR LINEA
                                    dondeVa = "Gastos fijos";
                                    if (gastosFijosPorFacturacion)
                                    {
                                        BUimporteLinea = (KABU * importeAlquilerLinea);
                                        centralImporteLinea = (KAHQ * importeAlquilerLinea);
                                    }
                                    else
                                    {
                                        costeAlquiler = importeCosteTaximetroConsumible + importeCosteTaximetroNoConsumible - importeCosteBonificacionTaximetroConsumible;
                                        costeAlquiler = (-1) * costeAlquiler;
                                        BUimporteLinea = (KABU * costeAlquiler);
                                        centralImporteLinea = (KAHQ * costeAlquiler);
                                    }

                                    GF = 0;
                                    if (ajustesAlcance == "GF BU")
                                        GF = BUimporteLinea;
                                    else
                                        GF = BUimporteLinea + centralImporteLinea;

                                    dias = Convert.ToInt32(filaLinea["DURACION"].ToString());
                                    if (dias < 0)
                                        porcentajeAjustesObraDuracionGFCapitulo = 0;
                                    else if (dias == 0)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPocentajeCargoMaximo;
                                    else if (dias == ajustesValorObraCorta)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPorcentajeCorreccionObraCorta;
                                    else if (dias == ajustesValorObraLarga)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPorcentajeCorreccionObraLarga;
                                    else if (dias > ajustesValorMaximo)
                                        porcentajeAjustesObraDuracionGFCapitulo = ajustesPorcentajeCorreccionMaximo;
                                    else
                                        porcentajeAjustesObraDuracionGFCapitulo = funcionalidades.interpolacion(dtAjustesDuracionGF, dias);

                                    importeAjustesObraDuracionGFCapitulo = importeAjustesObraDuracionGFCapitulo + (GF * porcentajeAjustesObraDuracionGFCapitulo);

                                    // CALCULO AJUSTES TAXIMETRO DURACION
                                    dondeVa = "Ajustes tax duracion";
                                    duracionAjuste = 0;
                                    if (Convert.ToInt32(filaLinea["DURACION"].ToString()) < ajustesTaxDuracionMinima)
                                        duracionAjuste = 0;
                                    else
                                    {
                                        if (Convert.ToInt32(filaLinea["DURACION"].ToString()) > ajustesTaxDuracionMaxima)
                                            duracionAjuste = ajustesTaxDuracionMaxima;
                                        else
                                            duracionAjuste = Convert.ToInt32(filaLinea["DURACION"].ToString());
                                        duracionAjuste = duracionAjuste - ajustesTaxDuracionMinima;
                                        if (diasxMes != 0)
                                            duracionAjuste = duracionAjuste / diasxMes;
                                        else
                                            duracionAjuste = 0;
                                        importeAjustesObraDuracionTaxCapitulo = importeAjustesObraDuracionTaxCapitulo + (duracionAjuste * Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_NO_TAX]) * ajustesTaxPorcentajeAbonoMensual);
                                        oItemTaximetro["% DURACION AJUSTE"] = ajustesTaxPorcentajeAbonoMensual;
                                        oItemTaximetro["DURACION AJUSTE"] = duracionAjuste;
                                        oItemTaximetro["AJUSTE OBRA DURACION"] = (duracionAjuste * Convert.ToDecimal(oItemTaximetro[COL_DATA_IMPORTE_NO_TAX]) * ajustesTaxPorcentajeAbonoMensual);
                                    }

                                    // CALCULO AJUSTES TAXIMETRO OCUPACION
                                    dondeVa = "Ajustes tax ocupacion";
                                    decimal ajusteOcupacion = 0;
                                    if (dtOcupacion.Rows.Count > 0)
                                    {
                                        aafBuscar = "";
                                        aafBuscar = filaLinea["ITEM"].ToString();
                                        // Buscar el AAF
                                        filaEncontrada = dtGastosPatio.Select("ProductId = '" + filaLinea["ITEM"].ToString() + "'");
                                        foreach (DataRow fila in filaEncontrada)
                                        {
                                            if (!String.IsNullOrEmpty(Convert.ToString(fila["AAF"])))
                                                aafBuscar = Convert.ToString(fila["AAF"]);
                                        }
                                        //filasOcupacion = dtOcupacion.Select("Articulo = '" + filaLinea["ITEM"].ToString() + "'");
                                        filasOcupacion = dtOcupacion.Select("Articulo = '" + aafBuscar + "'");
                                        foreach (DataRow filaOcupacion in filasOcupacion)
                                        {
                                            ocupacionAjuste = Convert.ToDecimal(filaOcupacion["PorcentajeAplicar"].ToString());
                                            if (filaOcupacion["Alcance"].ToString().ToUpper() == "TODOS")
                                            {
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["TAX NO CONSUMIBLE"]) / 100);
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["IMPORTE TAX CONSUMIBLE"]) / 100);
                                            }
                                            else if (filaOcupacion["Alcance"].ToString().ToUpper() == "CONSUMIBLE")
                                            {
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["IMPORTE TAX CONSUMIBLE"]) / 100);
                                            }
                                            else if (filaOcupacion["Alcance"].ToString().ToUpper() == "NO CONSUMIBLE")
                                            {
                                                ajusteOcupacion = ajusteOcupacion + (ocupacionAjuste * Convert.ToDecimal(oItemTaximetro["TAX NO CONSUMIBLE"]) / 100);
                                            }
                                            importeAjustesProductoTaxCapitulo = importeAjustesProductoTaxCapitulo + ajusteOcupacion;
                                            oItemTaximetro["% OCUPACION AJUSTE"] = ocupacionAjuste;
                                            oItemTaximetro["AJUSTE PRODUCTO"] = ajusteOcupacion;
                                            oItemTaximetro["AJUSTE PRODUCTO SOBRE"] = filaOcupacion["Alcance"].ToString().ToUpper();
                                        }
                                    }
                                    dtTaximetro.Rows.Add(oItemTaximetro);
                                    oItemTaximetro = null;

                                    // GASTOS FIJOS x ARTICULO
                                    dondeVa = "gastos fijos x articulo";
                                    oItemGastosFijos["ITEM"] = filaLinea["ITEM"].ToString();
                                    oItemGastosFijos["COSTE ALQUILER"] = (-1) * Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"]);
                                    oItemGastosFijos["COSTE VENTA"] = (-1) * Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]);
                                    if (gastosFijosPorFacturacion)
                                    {
                                        oItemGastosFijos["TOTAL BU"] = (Convert.ToDecimal(oItemGastosFijos["KABU"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION VENTA"]));
                                        oItemGastosFijos["TOTAL CENTRAL"] = (Convert.ToDecimal(oItemGastosFijos["KAHQ"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["FACTURACION VENTA"]));
                                    }
                                    else
                                    {
                                        oItemGastosFijos["TOTAL BU"] = (Convert.ToDecimal(oItemGastosFijos["KABU"]) * Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]));
                                        oItemGastosFijos["TOTAL CENTRAL"] = (Convert.ToDecimal(oItemGastosFijos["KAHQ"]) * Convert.ToDecimal(oItemGastosFijos["COSTE ALQUILER"])) + (Convert.ToDecimal(oItemGastosFijos["KVHQ"]) * Convert.ToDecimal(oItemGastosFijos["COSTE VENTA"]));
                                    }
                                    oItemGastosFijos["BONIFICACION BU CENTRAL"] = 0;
                                    oItemGastosFijos["BONIFICACION BU"] = 0;

                                    // BONIFICACION BU
                                    dondeVa = "Bonificacion BU";
                                    decimal real = 0;
                                    decimal presupuesto = 0;
                                    //bonificacionBU = 0;
                                    filasOcupacion = dtAjustesDatosBU.Select("Articulo = '" + filaLinea["ITEM"].ToString() + "'");
                                    foreach (DataRow filaOcupacion in filasOcupacion)
                                    {
                                        if (filaOcupacion["Escenario"].ToString() == "0")
                                            real = Convert.ToDecimal(filaOcupacion["Facturacion"].ToString());
                                        if (filaOcupacion["Escenario"].ToString() == "1")
                                            presupuesto = Convert.ToDecimal(filaOcupacion["Facturacion"].ToString());
                                    }
                                    if (presupuesto != 0)
                                    {
                                        if (Math.Abs(real / presupuesto) < cumplimientoBajoPorcentaje)
                                        {
                                            if (cumplimientoBajoAfectaGFCentral.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU CENTRAL"] = Convert.ToDecimal(oItemGastosFijos["TOTAL CENTRAL"]) * (bonificacionCumplimientoBajoPorcentaje); // / 100;
                                            if (cumplimientoBajoAfectaGFBU.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU"] = Convert.ToDecimal(oItemGastosFijos["TOTAL BU"]) * (bonificacionCumplimientoBajoPorcentaje); // / 100;
                                        }
                                        if (Math.Abs(real / presupuesto) > cumplimientoAltoPorcentaje)
                                        {
                                            if (cumplimientoAltoAfectaGFCentral.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU CENTRAL"] = Convert.ToDecimal(oItemGastosFijos["TOTAL CENTRAL"]) * (bonificacionCumplimientoAltoPorcentaje); // / 100;
                                            if (cumplimientoAltoAfectaGFBU.ToUpper() == "SI")
                                                oItemGastosFijos["BONIFICACION BU"] = Convert.ToDecimal(oItemGastosFijos["TOTAL BU"]) * (bonificacionCumplimientoAltoPorcentaje); // / 100;
                                        }
                                    }
                                    bonificacionBU = bonificacionBU + Convert.ToDecimal(oItemGastosFijos["BONIFICACION BU CENTRAL"]) + Convert.ToDecimal(oItemGastosFijos["BONIFICACION BU"]);

                                    dtGastosFijos.Rows.Add(oItemGastosFijos);
                                    oItemGastosFijos = null;
                                }

                                dondeVa = "FIN RECORRIDO CAPÍTULO";

                                // INICIO PORTES POR CAPÍTULO

                                dondeVa = "Portes";
                                if (calcularConPortes && filaCabecera["DELEGACION"].ToString().ToUpper() != "EX" && Convert.ToInt32(filaCabecera["GESTION PORTE"].ToString()) == Convert.ToInt32(tablaOfertas.AxdEnum_GestionPorte.Alsina.GetHashCode()) && (chkBoxSoloEntrega.Checked || chkBoxSoloDevolucion.Checked))
                                {
                                    if (existeTR001 && importePorte_TR001 > 0)
                                    {
                                        // se calcula solo un tramo del transporte durante el proceso, si es solo entrega o solo devolución es lo que tenemos, si es entrega+devolucion doblamos ese importe
                                        if (!chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                        {
                                            importePorte_TR001 = (importePorte_TR001 * coeficienteTransporte);
                                            importeCostePorte = (importeCostePorte * coeficienteTransporte);
                                        }
                                        if (chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                        {
                                            importePorte_TR001 = importePorte_TR001 + (importePorte_TR001 * coeficienteTransporte);
                                            importeCostePorte = importeCostePorte + (importeCostePorte * coeficienteTransporte);
                                        }

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "FACTURACION_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = importePorte_TR001.ToString("#,##0.00");
                                        filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        importeCostePorte = (-1) * (importePorte_TR001 * (1 - margenPorte));
                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "COSTE_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = importeCostePorte.ToString("#,##0.00");
                                        if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte_TR001 != 0)
                                            filaValores["PORCENTAJE"] = (importeCostePorte / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte_TR001) * 100).ToString("#,##0.00");
                                        else
                                            filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "MARGEN_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = (importePorte_TR001 + importeCostePorte).ToString("#,##0.00");
                                        if (((importePorte_TR001) / (1 - margenPorte)) == 0)
                                            filaValores["PORCENTAJE"] = "0.00";
                                        else
                                            filaValores["PORCENTAJE"] = (((importePorte_TR001 + importeCostePorte) / importePorte_TR001) * 100).ToString("#,##0.00");
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        importePorte = importePorte_TR001;
                                    }

                                    if (!existeTR001)
                                    {
                                        // se calcula solo un tramo del transporte durante el proceso, si es solo entrega o solo devolución es lo que tenemos, si es entrega+devolucion doblamos ese importe
                                        if (!chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                            importePorte = (importePorte * coeficienteTransporte);
                                        if (chkBoxSoloEntrega.Checked && chkBoxSoloDevolucion.Checked)
                                            importePorte = importePorte + (importePorte * coeficienteTransporte);

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "FACTURACION_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        if (1 - margenPorte != 0)
                                            filaValores["IMPORTE"] = ((importePorte * 100) / (1 - margenPorte)).ToString("#,##0.00");
                                        else
                                            filaValores["IMPORTE"] = "0.00";
                                        filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "COSTE_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = ((-1) * importePorte * 100).ToString("#,##0.00");
                                        if (importeAlquilerCapitulo + importeVentaCapitulo + (importePorte * 100) / (1 - margenPorte) != 0)
                                            filaValores["PORCENTAJE"] = ((((-1) * importePorte * 100) / (importeAlquilerCapitulo + importeVentaCapitulo + (importePorte * 100) / (1 - margenPorte))) * 100).ToString("#,##0.00");
                                        //(((-1) * importePorte * 100) / (importeAlquiler + importeVenta + ((importePorte * 100) / (1 - margenPorte))) * 100).ToString("#,##0.00");
                                        else
                                            filaValores["PORCENTAJE"] = "0.00";
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        filaValores = dtValores.NewRow();
                                        filaValores["ETIQUETA"] = "MARGEN_PORTES";
                                        filaValores["CONCEPTO"] = tituloCapitulo;
                                        filaValores["IMPORTE"] = (((importePorte * 100) / (1 - margenPorte)) - (importePorte * 100)).ToString("#,##0.00");
                                        if (((importePorte * 100) / (1 - margenPorte)) == 0)
                                            filaValores["PORCENTAJE"] = "0.00";
                                        else
                                            filaValores["PORCENTAJE"] = ((((importePorte * 100) / (1 - margenPorte)) - (importePorte * 100)) / ((importePorte * 100) / (1 - margenPorte)) * 100).ToString("#,##0.00");
                                        dtValores.Rows.Add(filaValores);
                                        filaValores = null;

                                        importePorte = (importePorte * 100) / (1 - margenPorte);
                                    }
                                }
                                else
                                {
                                    importePorte = 0;
                                    importePorte_TR001 = 0;
                                }
                                // FIN PORTES POR CAPÍTULO

                                // GASTOS FIJOS
                                dondeVa = "Gastos fijos";
                                if (gastosFijosPorFacturacion)
                                {
                                    totalGastosFijosBU = (KABU * importeAlquilerCapitulo) + (KVBU * importeVentaCapitulo);
                                    totalGastosFijosCentral = (KAHQ * importeAlquilerCapitulo) + (KVHQ * importeVentaCapitulo);
                                }
                                else
                                {
                                    if (calcularConPortes)
                                        costeVentas = importeCostePorte + importeCosteVentaDepartamentoTecnicoCapitulos + importeCosteVentaFenolicoNuevoCapitulos + importeCosteVentaUneCifCapitulos + importeCosteVentaOtrosServiciosCapitulos + importeCosteVentaMontajesCapitulos + importeCosteVentaCapitulos;
                                    else
                                        costeVentas = importeCosteVentaDepartamentoTecnicoCapitulos + importeCosteVentaFenolicoNuevoCapitulos + importeCosteVentaUneCifCapitulos + importeCosteVentaOtrosServiciosCapitulos + importeCosteVentaMontajesCapitulos + importeCosteVentaCapitulos; ;
                                    costeVentas = (-1) * costeVentas;
                                    costeAlquiler = importeCosteTaximetroNoConsumibleCapitulos + importeCosteTaximetroConsumibleCapitulos - importeCosteBonificacionTaximetroConsumibleCapitulos;
                                    costeAlquiler = (-1) * costeAlquiler;
                                    totalGastosFijosBU = (KABU * costeAlquiler) + (KVBU * costeVentas);
                                    totalGastosFijosCentral = (KAHQ * costeAlquiler) + (KVHQ * costeVentas);
                                }
                                if (totalGastosFijosBU != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_BU";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = totalGastosFijosBU.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (totalGastosFijosBU / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (totalGastosFijosCentral != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_CENTRAL";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = totalGastosFijosCentral.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (totalGastosFijosCentral / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES OBRA DURACION GASTOS FIJOS
                                dondeVa = "Ajustes duracion gastos fijos";
                                if (importeAjustesObraDuracionGFCapitulo != 0)
                                {
                                    importeAjustesObraDuracionGFCapitulo = Math.Abs(importeAjustesObraDuracionGFCapitulo);
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_OBRA_DURACION_GF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeAjustesObraDuracionGFCapitulo).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeAjustesObraDuracionGFCapitulo / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES OBRA DURACION TAX
                                if (importeAjustesObraDuracionTaxCapitulo != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_OBRA_DURACION_TAXIMETRO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeAjustesObraDuracionTaxCapitulo.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeAjustesObraDuracionTaxCapitulo / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES PRODUCTOS
                                if (importeAjustesProductoTaxCapitulo != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_TAX_PRODUCTO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeAjustesProductoTaxCapitulo).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = Math.Abs((importeAjustesProductoTaxCapitulo / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100)).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // AJUSTES BU
                                if (bonificacionBU != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTES_BU";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * (bonificacionBU)).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (((-1) * (bonificacionBU)) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // GASTOS FINANCIACION
                                decimal portes = dtValores.AsEnumerable()
                                               .Where(y => y.Field<string>("ETIQUETA") == "FACTURACION_PORTES" && y.Field<string>("CONCEPTO") == tituloCapitulo)
                                               .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                                //decimal totalCapitulo = importeFacturacionAlquilerCapitulos + importeFacturacionVentaProductoCapitulos + importeFacturacionVentaMontajesCapitulos + importeFacturacionVentaDepartamentoTecnicoCapitulos + importeFacturacionVentaFenolicoNuevoCapitulos + importeFacturacionVentaUneCifCapitulos + importeFacturacionVentaOtrosServiciosCapitulos + portes;
                                decimal totalCapitulo = importeFacturacionAlquilerCapitulos + importeFacturacionVentaMontajesCapitulos + importeFacturacionVentaDepartamentoTecnicoCapitulos + importeFacturacionVentaFenolicoNuevoCapitulos + importeFacturacionVentaUneCifCapitulos + importeFacturacionVentaOtrosServiciosCapitulos + portes;
                                if (Convert.ToDecimal(filaCabecera["DIAS TOTAL"]) == 0)
                                    importeGastosFinanciacionCapitulos = (condicionesAplicar * totalCapitulo * ((diasPagoDefecto) / Convert.ToDecimal(365)));
                                else
                                    importeGastosFinanciacionCapitulos = (condicionesAplicar * totalCapitulo * ((Convert.ToDecimal(filaCabecera["DIAS TOTAL"])) / Convert.ToDecimal(365)));
                                if (importeGastosFinanciacionCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FINANCIACION";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFinanciacionCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFinanciacionCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                // GASTOS FALLIDOS
                                importeFallidosCapitulos = (totalCapitulo) * (porcentajeCobrados / 100) * (porcentajeFallidos / 100);
                                if (importeFallidosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FALLIDOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeFallidosCapitulos).ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosVariablesEntregaDevolucionAlquilerCapitulos != 0 || importeGastosVariablesEntregaDevolucionVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 6";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLES_ENTREGA_DEVOLUCION";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * (importeGastosVariablesEntregaDevolucionAlquilerCapitulos + importeGastosVariablesEntregaDevolucionVentaCapitulos)).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * (importeGastosVariablesEntregaDevolucionAlquilerCapitulos + importeGastosVariablesEntregaDevolucionVentaCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosVariablesClaisificacionReparacionAlquilerCapitulos != 0 || importeGastosVariablesClaisificacionReparacionVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 6";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLES_CLASIFICACION_REPARACION";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * (importeGastosVariablesClaisificacionReparacionAlquilerCapitulos + importeGastosVariablesClaisificacionReparacionVentaCapitulos)).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * (importeGastosVariablesClaisificacionReparacionAlquilerCapitulos + importeGastosVariablesClaisificacionReparacionVentaCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                dondeVa = "Importes/cantidades venta 1";
                                if (importeFacturacionVentaDepartamentoTecnicoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "DEPARTAMENTO_TECNICO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaDepartamentoTecnicoCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                dondeVa = "Importes/dec_importeCosteVCH ";
                                if (dec_importeCosteVCH != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_VCH";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * dec_importeCosteVCH).ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaDepartamentoTecnicoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_DEPARTAMENTO_TECNICO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaDepartamentoTecnicoCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaDepartamentoTecnicoCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaDepartamentoTecnicoCapitulos != 0 || importeCosteVentaDepartamentoTecnicoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_DEPARTAMENTO_TECNICO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaDepartamentoTecnicoCapitulos - importeCosteVentaDepartamentoTecnicoCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaDepartamentoTecnicoCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaDepartamentoTecnicoCapitulos - importeCosteVentaDepartamentoTecnicoCapitulos) / importeFacturacionVentaDepartamentoTecnicoCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaFenolicoNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "FENOLICO_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaFenolicoNuevoCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_FENOLICO_NUEVO_MATERIAL_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaNuevoCapitulos).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaFenolicoNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_FENOLICO_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaFenolicoNuevoCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaFenolicoNuevoCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaFenolicoNuevoCapitulos) / importeFacturacionVentaFenolicoNuevoCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }


                                if (importeFacturacionVentaFenolicoNuevoCapitulos != 0 || importeGastosVariablesVentaFenolicoNuevoCapitulos != 0 || importeGastosFijosBUVentaFenolicoNuevoCapitulos != 0 || importeGastosFijosCentralesVentaFenolicoNuevoCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ROP_BASICO_FENOLICO_NUEVO";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaFenolicoNuevoCapitulos - importeGastosVariablesVentaFenolicoNuevoCapitulos - importeGastosFijosBUVentaFenolicoNuevoCapitulos - importeGastosFijosCentralesVentaFenolicoNuevoCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((importeFacturacionVentaFenolicoNuevoCapitulos - importeGastosVariablesVentaFenolicoNuevoCapitulos - importeGastosFijosBUVentaFenolicoNuevoCapitulos - importeGastosFijosCentralesVentaFenolicoNuevoCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "SVP_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaUneCifCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_SVP_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaUneCifCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;

                                }

                                if (importeFacturacionVentaUneCifCapitulos != 0 || importeCosteVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_SVP_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaUneCifCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos) / importeFacturacionVentaUneCifCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaOtrosServiciosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "OTROS_SERVICIOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaOtrosServiciosCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaOtrosServiciosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_OTROS_SERVICIOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaOtrosServiciosCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaOtrosServiciosCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaOtrosServiciosCapitulos != 0 || importeCosteVentaOtrosServiciosCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_OTROS_SERVICIOS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaOtrosServiciosCapitulos - importeCosteVentaOtrosServiciosCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaOtrosServiciosCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaOtrosServiciosCapitulos - importeCosteVentaOtrosServiciosCapitulos) / importeFacturacionVentaOtrosServiciosCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosBUVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_BU_UNE_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosBUVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosBUVentaUneCifCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosCentralesVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_CENTRALES_UNE_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosCentralesVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosCentralesVentaUneCifCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaUneCifCapitulos != 0 || importeCosteVentaUneCifCapitulos != 0 || importeGastosVariablesVentaUneCifCapitulos != 0 || importeGastosFijosBUVentaUneCifCapitulos != 0 || importeGastosFijosCentralesVentaUneCifCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ROP_BASICO_UNE_CIF";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos - importeGastosVariablesVentaUneCifCapitulos - importeGastosFijosBUVentaUneCifCapitulos - importeGastosFijosCentralesVentaUneCifCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((importeFacturacionVentaUneCifCapitulos - importeCosteVentaUneCifCapitulos - importeGastosVariablesVentaUneCifCapitulos - importeGastosFijosBUVentaUneCifCapitulos - importeGastosFijosCentralesVentaUneCifCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaMontajesCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaMontajesCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaMontajesCapitulos != 0 || importeCosteVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaMontajesCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos) / importeFacturacionVentaMontajesCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosBUVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_BU_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosBUVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosBUVentaMontajesCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeGastosFijosCentralesVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_FIJOS_CENTRALES_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeGastosFijosCentralesVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeGastosFijosCentralesVentaMontajesCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaMontajesCapitulos != 0 || importeCosteVentaMontajesCapitulos != 0 || importeGastosVariablesVentaMontajesCapitulos != 0 || importeGastosFijosBUVentaMontajesCapitulos != 0 || importeGastosFijosCentralesVentaMontajesCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ROP_BASICO_MONTAJES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos - importeGastosVariablesVentaMontajesCapitulos - importeGastosFijosBUVentaMontajesCapitulos - importeGastosFijosCentralesVentaMontajesCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((importeFacturacionVentaMontajesCapitulos - importeCosteVentaMontajesCapitulos - importeGastosVariablesVentaMontajesCapitulos - importeGastosFijosBUVentaMontajesCapitulos - importeGastosFijosCentralesVentaMontajesCapitulos) / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaProductoCapitulos != 0)
                                {

                                    dondeVa = "Importes/cantidades venta productos 1";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "VENTAS_DIRECTAS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionVentaProductoCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;

                                }

                                if (importeCosteVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades venta 2";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "COSTE_VENTAS_DIRECTAS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteVentaCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteVentaCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionVentaProductoCapitulos != 0 || importeCosteVentaCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades venta 3";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_VENTAS_DIRECTAS";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionVentaProductoCapitulos - importeCosteVentaCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionVentaProductoCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionVentaProductoCapitulos - importeCosteVentaCapitulos) / importeFacturacionVentaProductoCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                dondeVa = "Importes/cantidades alquiler 1";
                                if (importeFacturacionAlquilerCapitulos != 0)
                                {
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "ALQUILERES";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeFacturacionAlquilerCapitulos.ToString("#,##0.00");
                                    filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteTaximetroNoConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 2";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "TAXIMETRO_NO_CONSUMIBLE";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteTaximetroNoConsumibleCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeCosteTaximetroNoConsumibleCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteTaximetroConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 3";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "TAXIMETRO_CONSUMIBLE";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = ((-1) * importeCosteTaximetroConsumibleCapitulos).ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * importeCosteTaximetroConsumibleCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeCosteBonificacionTaximetroConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 4";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "AJUSTE_TAXIMETRO_CONSUMIBLE";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = importeCosteBonificacionTaximetroConsumibleCapitulos.ToString("#,##0.00");
                                    if (importeAlquilerCapitulo + importeVentaCapitulo + importePorte != 0)
                                        filaValores["PORCENTAJE"] = (importeCosteBonificacionTaximetroConsumibleCapitulos / (importeAlquilerCapitulo + importeVentaCapitulo + importePorte) * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (importeFacturacionAlquilerCapitulos != 0 || importeCosteTaximetroNoConsumibleCapitulos != 0 || importeCosteTaximetroConsumibleCapitulos != 0 || importeCosteBonificacionTaximetroConsumibleCapitulos != 0)
                                {
                                    dondeVa = "Importes/cantidades alquiler 5";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "MARGEN_ALQUILER";
                                    filaValores["CONCEPTO"] = tituloCapitulo;
                                    filaValores["IMPORTE"] = (importeFacturacionAlquilerCapitulos - importeCosteTaximetroNoConsumibleCapitulos - importeCosteTaximetroConsumibleCapitulos + importeCosteBonificacionTaximetroConsumibleCapitulos).ToString("#,##0.00");
                                    if (importeFacturacionAlquilerCapitulos == 0)
                                        filaValores["PORCENTAJE"] = "0.00";
                                    else
                                        filaValores["PORCENTAJE"] = (((importeFacturacionAlquilerCapitulos - importeCosteTaximetroNoConsumibleCapitulos - importeCosteTaximetroConsumibleCapitulos + importeCosteBonificacionTaximetroConsumibleCapitulos) / importeFacturacionAlquilerCapitulos) * 100).ToString("#,##0.00");
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }
                                dtDatosConfiguracion.Dispose();
                            }
                        }
                        TimeSpan horaDatosPase2_2 = DateTime.Now.Subtract(horaDatosPase2_1);

                        // CALCULAR GASTOS OFICINA 
                        dondeVa = "Gastos oficina";
                        decimal totalFacturacion = 0;

                        decimal totalAlquiler = 0;
                        decimal totalVenta = 0;
                        decimal totalPortes = 0;
                        decimal costeDTPorratear = 0;
                        decimal totalPortesLinea = 0;
                        string tituloAnterior = "";
                        decimal totalGastosOficinaDToCapitulo = 0;
                        decimal totalGastosOficinaAdministrativoCapitulo = 0;
                        decimal totalGastosOficinaComercialCapitulo = 0;

                        totalAlquiler = dtValores.AsEnumerable()
                                .Where(y => y.Field<string>("ETIQUETA") == "ALQUILERES")
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        totalVenta = dtValores.AsEnumerable()
                               .Where(y => y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS" || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES" || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO" || y.Field<string>("ETIQUETA") == "MONTAJES" || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO" || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO" || y.Field<string>("ETIQUETA") == "SVP_CIF" || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS")
                               .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        totalPortes = dtValores.AsEnumerable()
                              .Where(y => y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                              .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        if (calcularConPortes)
                            total = totalAlquiler + totalVenta + totalPortes;
                        else
                            total = totalAlquiler + totalVenta;

                        if (total * porcentajeMaximoTotalFacturacion > costeDT)
                            costeDTPorratear = costeDT;
                        else
                            costeDTPorratear = total * porcentajeMaximoTotalFacturacion;

                        tituloCapitulo = "";
                        foreach (DataRow fila in dtTaximetro.Rows)
                        {
                            if (fila["CAPITULO"].ToString() != tituloAnterior)
                            {
                                // importe facturación capitulo
                                totalFacturacion = dtValores.AsEnumerable()
                                    .Where(y => y.Field<string>("CONCEPTO") == tituloAnterior && (
                                                        y.Field<string>("ETIQUETA") == "ALQUILERES"
                                                        || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                                        || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                        || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                                        || y.Field<string>("ETIQUETA") == "MONTAJES"
                                                        || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                                        || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                                        || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                                        || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                                        || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                                        )
                                    .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                                if (totalGastosOficinaAdministrativoCapitulo != 0 || totalGastosOficinaComercialCapitulo != 0)
                                {
                                    dondeVa = "Gastos Oficina Administrativo y Comercial";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLES_ADMINISTRATIVO_COMERCIAL";
                                    filaValores["CONCEPTO"] = tituloAnterior;
                                    filaValores["IMPORTE"] = (totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo).ToString("#,##0.00");
                                    if (totalFacturacion != 0)
                                        filaValores["PORCENTAJE"] = ((totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo) / totalFacturacion * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                if (totalGastosOficinaDToCapitulo != 0)
                                {
                                    dondeVa = "Gastos Oficina Tecnico";
                                    filaValores = dtValores.NewRow();
                                    filaValores["ETIQUETA"] = "GASTOS_VARIABLIES_TECNICO";
                                    filaValores["CONCEPTO"] = tituloAnterior;
                                    filaValores["IMPORTE"] = ((-1) * totalGastosOficinaDToCapitulo).ToString("#,##0.00");
                                    if (totalFacturacion != 0)
                                        filaValores["PORCENTAJE"] = ((-1) * totalGastosOficinaDToCapitulo / totalFacturacion * 100).ToString("#,##0.00");
                                    else
                                        filaValores["PORCENTAJE"] = "0.00";
                                    dtValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }

                                totalGastosOficinaAdministrativoCapitulo = 0;
                                totalGastosOficinaComercialCapitulo = 0;
                                totalGastosOficinaDToCapitulo = 0;
                                tituloAnterior = fila["CAPITULO"].ToString();
                            }

                            dondeVa = "dtGastosOficina";
                            DataRow oItemGastosOficina = dtGastosOficina.NewRow();
                            oItemGastosOficina["CAPITULO"] = fila["CAPITULO"].ToString();
                            oItemGastosOficina["ITEM"] = fila["ITEM"].ToString();
                            oItemGastosOficina["MES PRESUPUESTO VALIDO"] = mesPresupuestoValido.ToString("#,##0");
                            oItemGastosOficina["PORCENTAJE GASTO ADMINISTRATIVO"] = porcentajeGastoAdmministrativoVariable.ToString("#,##0.00");
                            oItemGastosOficina["PORCENTAJE GASTO COMERCIAL"] = porcentajeGastoComercialVariable.ToString("#,##0.00");
                            oItemGastosOficina["PORCENTAJE MAXIMO TOTAL FACTURACON"] = porcentajeMaximoTotalFacturacion.ToString("#,##0.00");
                            oItemGastosOficina["TOTAL ANUAL"] = totalAnual.ToString("#,##0.00");
                            oItemGastosOficina["TOTAL ADMINISTRATIVO"] = totalAdministrativo.ToString("#,##0.00");
                            oItemGastosOficina["VAR ADMINISTRATIVO"] = varAdministrativo.ToString("#,##0.00");
                            oItemGastosOficina["TOTAL COMERCIAL"] = totalComercial.ToString("#,##0.00");
                            oItemGastosOficina["VAR COMERCIAL"] = varComercial.ToString("#,##0.00");
                            oItemGastosOficina["IMPORTE"] = "0.00";

                            oItemGastosOficina["TOTAL FACTURACION"] = total.ToString("#,##0.00");
                            oItemGastosOficina["MAX FACTURACION"] = (total * porcentajeMaximoTotalFacturacion).ToString("#,##0.00");
                            oItemGastosOficina["COSTE DT"] = costeDT.ToString("#,##0.00");

                            if (calcularConPortes)
                            {
                                if (totalAlquiler + totalVenta != 0)
                                    totalPortesLinea = ((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"])) * totalPortes) / (totalAlquiler + totalVenta);
                                else
                                    totalPortesLinea = 0;
                            }

                            // ADMINISTRACION
                            dondeVa = "ADMINISTRACION";
                            if (totalAnual != 0)
                            {
                                if (calcularConPortes)
                                {
                                    // gastos oficina venta
                                    totalGastosOficinaAdministrativoCapitulo = totalGastosOficinaAdministrativoCapitulo + ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual);
                                    totalGastosOficinaComercialCapitulo = totalGastosOficinaComercialCapitulo + ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual);
                                    oItemGastosOficina["GASTOS ADMINISTRATIVO"] = ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS COMERCIAL"] = ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea)) / totalAnual).ToString("#,##0.00");
                                }
                                else
                                {
                                    // gastos oficina venta
                                    totalGastosOficinaAdministrativoCapitulo = totalGastosOficinaAdministrativoCapitulo + ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual);
                                    totalGastosOficinaComercialCapitulo = totalGastosOficinaComercialCapitulo + ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual);
                                    oItemGastosOficina["GASTOS ADMINISTRATIVO"] = ((varAdministrativo * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS COMERCIAL"] = ((varComercial * (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]))) / totalAnual).ToString("#,##0.00");
                                }
                            }

                            // DT
                            dondeVa = "DT";
                            if (total != 0)
                            {
                                if (calcularConPortes)
                                {
                                    oItemGastosOficina["IMPORTE"] = (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS DT"] = (((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea) * costeDTPorratear) / total).ToString("#,##0.00");
                                    totalGastosOficinaDToCapitulo = totalGastosOficinaDToCapitulo + (((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) + totalPortesLinea) * costeDTPorratear) / total);
                                }
                                else
                                {
                                    oItemGastosOficina["IMPORTE"] = (Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"])).ToString("#,##0.00");
                                    oItemGastosOficina["GASTOS DT"] = ((Convert.ToDecimal(fila["FACT TOTAL VENTA"]) + Convert.ToDecimal(fila["FACT ALQUILER"]) * costeDTPorratear) / total).ToString("#,##0.00");
                                    totalGastosOficinaDToCapitulo = totalGastosOficinaDToCapitulo + ((Convert.ToDecimal(oItemGastosOficina["IMPORTE"]) * costeDTPorratear) / total);
                                }
                            }
                            dtGastosOficina.Rows.Add(oItemGastosOficina);
                            oItemGastosOficina = null;
                        }

                        if (tituloAnterior != "")
                        {
                            // importe facturación capitulo
                            totalFacturacion = dtValores.AsEnumerable()
                                .Where(y => y.Field<string>("CONCEPTO") == tituloAnterior && (
                                                    y.Field<string>("ETIQUETA") == "ALQUILERES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                                    || y.Field<string>("ETIQUETA") == "MONTAJES"
                                                    || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                                    || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                                    || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                                    || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                                    )
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));


                            dondeVa = "Gastos Oficina Administrativo y Comercial";
                            if (totalGastosOficinaAdministrativoCapitulo != 0 || totalGastosOficinaComercialCapitulo != 0)
                            {
                                dondeVa = "Gastos Oficina Administrativo y Comercial";
                                filaValores = dtValores.NewRow();
                                filaValores["ETIQUETA"] = "GASTOS_VARIABLES_ADMINISTRATIVO_COMERCIAL";
                                filaValores["CONCEPTO"] = tituloAnterior;
                                filaValores["IMPORTE"] = (totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo).ToString("#,##0.00");
                                if (totalFacturacion != 0)
                                    filaValores["PORCENTAJE"] = ((totalGastosOficinaAdministrativoCapitulo + totalGastosOficinaComercialCapitulo) / totalFacturacion * 100).ToString("#,##0.00");
                                else
                                    filaValores["PORCENTAJE"] = "0.00";
                                dtValores.Rows.Add(filaValores);
                                filaValores = null;
                            }

                            if (totalGastosOficinaDToCapitulo != 0)
                            {
                                dondeVa = "Gastos Oficina Tecnico";
                                filaValores = dtValores.NewRow();
                                filaValores["ETIQUETA"] = "GASTOS_VARIABLIES_TECNICO";
                                filaValores["CONCEPTO"] = tituloAnterior;
                                filaValores["IMPORTE"] = ((-1) * totalGastosOficinaDToCapitulo).ToString("#,##0.00");
                                if (totalFacturacion != 0)
                                    filaValores["PORCENTAJE"] = ((-1) * totalGastosOficinaDToCapitulo / totalFacturacion * 100).ToString("#,##0.00");
                                else
                                    filaValores["PORCENTAJE"] = "0.00";
                                dtValores.Rows.Add(filaValores);
                                filaValores = null;
                            }
                        }
                        dtArticulos.Dispose();

                        // ROP BASICO // ROP
                        dondeVa = "ROP BASICO";
                        tituloCapitulo = "";
                        tituloCapituloAsterisco = "";
                        foreach (DataRow filaCapitulo in filasCapitulo)
                        {
                            tituloCapitulo = filaCapitulo["CAPITULO"].ToString().Trim();
                            tituloCapituloAsterisco = "* " + filaCapitulo["CAPITULO"].ToString().Trim();

                            // importe facturación capitulo
                            totalFacturacion = dtValores.AsEnumerable()
                                .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                                    y.Field<string>("ETIQUETA") == "ALQUILERES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                    || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                                    || y.Field<string>("ETIQUETA") == "MONTAJES"
                                                    || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                                    || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                                    || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                                    || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                                    )
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                            // ROP BASICO
                            total = dtValores.AsEnumerable()
                                    .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                                    y.Field<string>("ETIQUETA") == "MARGEN_ALQUILER"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_VENTAS_DIRECTAS"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_MONTAJES"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_DEPARTAMENTO_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_FENOLICO_NUEVO"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_SVP_CIF"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_OTROS_SERVICIOS"
                                                    || y.Field<string>("ETIQUETA") == "MARGEN_PORTES"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLES_ENTREGA_DEVOLUCION"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLES_CLASIFICACION_REPARACION"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLES_ADMINISTRATIVO_COMERCIAL"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_VARIABLIES_TECNICO"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FINANCIACION"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FALLIDOS"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FIJOS_BU"
                                                    || y.Field<string>("ETIQUETA") == "GASTOS_FIJOS_CENTRAL")
                                    )
                                    .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                            filaValores = dtValores.NewRow();
                            filaValores["ETIQUETA"] = "ROP_BASICO";
                            filaValores["CONCEPTO"] = tituloCapitulo;
                            filaValores["IMPORTE"] = total.ToString("#,##0.00");
                            if (totalFacturacion != 0)
                                filaValores["PORCENTAJE"] = (total / totalFacturacion * 100).ToString("#,##0.00");
                            else
                                filaValores["PORCENTAJE"] = "0.00";
                            dtValores.Rows.Add(filaValores);
                            filaValores = null;

                            //// ROP
                            //total = dtValores.AsEnumerable()
                            //         .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                            //                        y.Field<string>("ETIQUETA") == "ROP_BASICO"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_TAXIMETRO"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_GF"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_TAX_PRODUCTO"
                            //                        || y.Field<string>("ETIQUETA") == "COSTE_FENOLICO_NUEVO_MATERIAL_NUEVO"
                            //                        || y.Field<string>("ETIQUETA") == "AJUSTES_BU")
                            //        )
                            //        .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                            //filaValores = dtValores.NewRow();
                            //filaValores["ETIQUETA"] = "ROP";
                            //filaValores["CONCEPTO"] = tituloCapitulo;
                            //filaValores["IMPORTE"] = total.ToString("#,##0.00");
                            //if (totalFacturacion != 0)
                            //    filaValores["PORCENTAJE"] =(total / totalFacturacion * 100).ToString("#,##0.00");
                            //else
                            //    filaValores["PORCENTAJE"] = "0.00";
                            //dtValores.Rows.Add(filaValores);
                            //filaValores = null;
                        }
                    }

                    dondeVa = "finalizó";
                    //proxy_1.Close();
                    horaTotal_2 = DateTime.Now.Subtract(horaTotal_1);

                    listaArticulosSinFicha.Clear();
                    listaArticulosPedido.Clear();
                    listaFechasPedido.Clear();
                    listaOfertas.Clear();
                    listaFechasOferta.Clear();

                    hora_2 = DateTime.Now.Subtract(hora_1);

                    dondeVa = "nombre del doc";
                    string nombreInforme = Server.MapPath("~/Ficheros excel/" + cmbConcepto.Text + "_" + txtNumero.Text.Replace("/", "_") + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx");
                    dondeVa = "nuevo doc";
                    SLDocument sl = new SLDocument();
                    dondeVa = "importar tabla";
                    sl.ImportDataTable(1, 1, dtTaximetro, true);
                    dondeVa = "salvar tabla";
                    sl.SaveAs(nombreInforme);
                    btnAbrirExcel.Visible = true;

                    dondeVa = "pintar datos";
                    hora_1 = DateTime.Now;
                    pintarArbolNUEVO(ref dtValores, false, false, false);
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO pintar árbol";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    // TOPES AJUSTES
                    dondeVa = "ajustes";
                    DataTable dtTopesValores = new DataTable();
                    dtTopesValores.Clear();
                    dtTopesValores.Columns.Add("APLICA SOBRE", typeof(string));
                    dtTopesValores.Columns.Add("IMPORTE APLICA SOBRE", typeof(decimal));
                    dtTopesValores.Columns.Add("PORCENTAJE CONFIGURADO", typeof(decimal));
                    dtTopesValores.Columns.Add("TOPE", typeof(decimal));
                    dtTopesValores.Columns.Add("ETIQUETA", typeof(string));
                    dtTopesValores.Columns.Add("IMPORTE ETIQUETA", typeof(decimal));
                    dtTopesValores.Columns.Add("IMPORTE TOTAL ETIQUETAS", typeof(decimal));
                    dtTopesValores.Columns.Add("PORCENTAJE APLICAR", typeof(decimal));
                    dtTopesValores.Columns.Add("IMPORTE TOPADO", typeof(decimal));

                    DataTable dtTopes = new DataTable();
                    dtTopes = funcionalidades.topesAjustes(ref dtTiempos, version, masterObra, fechaPrecios, "", usuarioRed);
                    foreach (DataRow fila in dtTopes.Rows)
                    {
                        string concepto = fila["CFGAJU_Concepto"].ToString();
                        string aplicarSobre = fila["CFGAJU_AplicarSobre"].ToString();
                        string aplicar = fila["CFGAJU_AplicarA"].ToString();
                        decimal porcentajeTotal = Convert.ToDecimal(fila["CFGAJU_ValorMaximoPorcentaje"].ToString());

                        total = dtValores.AsEnumerable()
                                    .Where(y => y.Field<string>("ETIQUETA") == "TOTAL_FACTURACION")
                                    .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        decimal sumaTotalesSobre = dtValores.AsEnumerable()
                                   .Where(y => aplicarSobre.Contains(y.Field<string>("ETIQUETA")))
                                   .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        decimal sumaTotalesA = dtValores.AsEnumerable()
                                   .Where(y => aplicar.Contains(y.Field<string>("ETIQUETA")))
                                   .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        decimal tope = Math.Abs(sumaTotalesSobre) * porcentajeTotal / 100;

                        string[] aplica;
                        //if (Math.Abs(tope) < Math.Abs(sumaTotalesA))
                        //{
                        aplica = aplicar.Split('|');
                        while (aplica.GetUpperBound(0) != 0)
                        {
                            aplica = aplicar.Split('|');
                            filaEncontrada = dtValores.Select("ETIQUETA = '" + aplica[0] + "'");
                            foreach (DataRow filaDT in filaEncontrada)
                            {
                                //if (sumaTotalesA > 0)
                                if (Math.Abs(tope) < Math.Abs(sumaTotalesA) && Math.Abs(sumaTotalesA) > 0)
                                {
                                    filaValores = dtTopesValores.NewRow();
                                    filaValores["APLICA SOBRE"] = aplicarSobre;
                                    filaValores["IMPORTE APLICA SOBRE"] = sumaTotalesSobre.ToString("#,##0.00");
                                    filaValores["PORCENTAJE CONFIGURADO"] = porcentajeTotal.ToString("#,##0.0000");
                                    filaValores["TOPE"] = tope.ToString("#,##0.00");
                                    filaValores["ETIQUETA"] = aplica[0];
                                    filaValores["IMPORTE ETIQUETA"] = filaDT["IMPORTE"];
                                    filaValores["IMPORTE TOTAL ETIQUETAS"] = sumaTotalesA.ToString("#,##0.00");

                                    filaDT["CONCEPTO"] = "* " + filaDT["CONCEPTO"].ToString().Trim();

                                    decimal porcentajeAplicar = Convert.ToDecimal(filaDT["IMPORTE"].ToString()) * 100 / sumaTotalesA;
                                    filaDT["IMPORTE"] = (tope * porcentajeAplicar / 100).ToString("##0.00");
                                    if (total != 0)
                                        filaDT["PORCENTAJE"] = (Convert.ToDecimal(filaDT["IMPORTE"].ToString()) * 100 / total).ToString("##0.00");
                                    else
                                        filaDT["PORCENTAJE"] = 0.00;
                                    filaDT["COLOR"] = "text_custom";
                                    filaDT.AcceptChanges();

                                    filaValores["PORCENTAJE APLICAR"] = porcentajeAplicar.ToString("#,##0.0000");
                                    filaValores["IMPORTE TOPADO"] = filaDT["IMPORTE"];
                                    dtTopesValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }
                                else
                                {
                                    filaValores = dtTopesValores.NewRow();
                                    filaValores["APLICA SOBRE"] = aplicarSobre;
                                    filaValores["IMPORTE APLICA SOBRE"] = sumaTotalesSobre.ToString("#,##0.00");
                                    filaValores["PORCENTAJE CONFIGURADO"] = porcentajeTotal.ToString("#,##0.0000");
                                    filaValores["TOPE"] = tope.ToString("#,##0.00");
                                    filaValores["ETIQUETA"] = aplica[0];
                                    filaValores["IMPORTE ETIQUETA"] = filaDT["IMPORTE"];
                                    filaValores["IMPORTE TOTAL ETIQUETAS"] = sumaTotalesA.ToString("#,##0.00");
                                    filaValores["PORCENTAJE APLICAR"] = "0.0000";
                                    filaValores["IMPORTE TOPADO"] = "0.00";
                                    dtTopesValores.Rows.Add(filaValores);
                                    filaValores = null;
                                }
                            }
                            aplicar = aplicar.Replace(aplica[0] + "|", "");
                            if (aplica.GetUpperBound(0) == 0)
                                break;
                        }
                    }

                    // ROP
                    dondeVa = "ROP";
                    tituloCapitulo = "";
                    tituloCapituloAsterisco = "";
                    foreach (DataRow filaCapitulo in filasCapitulo)
                    {
                        tituloCapitulo = filaCapitulo["CAPITULO"].ToString().Trim();
                        tituloCapituloAsterisco = "* " + filaCapitulo["CAPITULO"].ToString().Trim();

                        // importe facturación capitulo
                        decimal totalFacturacion = dtValores.AsEnumerable()
                        .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                            y.Field<string>("ETIQUETA") == "ALQUILERES"
                                            || y.Field<string>("ETIQUETA") == "VENTAS_DIRECTAS"
                                            || y.Field<string>("ETIQUETA") == "VENTAS_MATERIAL_ALQUILADO_LIQUIDACIONES"
                                            || y.Field<string>("ETIQUETA") == "VENTAS_MALUSO"
                                            || y.Field<string>("ETIQUETA") == "MONTAJES"
                                            || y.Field<string>("ETIQUETA") == "DEPARTAMENTO_TECNICO"
                                            || y.Field<string>("ETIQUETA") == "FENOLICO_NUEVO"
                                            || y.Field<string>("ETIQUETA") == "SVP_CIF"
                                            || y.Field<string>("ETIQUETA") == "OTROS_SERVICIOS"
                                            || y.Field<string>("ETIQUETA") == "FACTURACION_PORTES")
                            )
                        .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        // ROP
                        total = dtValores.AsEnumerable()
                                 .Where(y => (y.Field<string>("CONCEPTO") == tituloCapitulo || y.Field<string>("CONCEPTO") == tituloCapituloAsterisco) && (
                                                y.Field<string>("ETIQUETA") == "ROP_BASICO"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_TAXIMETRO"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_OBRA_DURACION_GF"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_TAX_PRODUCTO"
                                                || y.Field<string>("ETIQUETA") == "COSTE_FENOLICO_NUEVO_MATERIAL_NUEVO"
                                                || y.Field<string>("ETIQUETA") == "AJUSTES_BU")
                                )
                                .Sum(x => Convert.ToDecimal(x.Field<string>("IMPORTE")));

                        filaValores = dtValores.NewRow();
                        filaValores["ETIQUETA"] = "ROP";
                        filaValores["CONCEPTO"] = tituloCapitulo;
                        filaValores["IMPORTE"] = total.ToString("#,##0.00");
                        if (totalFacturacion != 0)
                            filaValores["PORCENTAJE"] = (total / totalFacturacion * 100).ToString("#,##0.00");
                        else
                            filaValores["PORCENTAJE"] = "0.00";
                        dtValores.Rows.Add(filaValores);
                        filaValores = null;
                    }

                    hora_1 = DateTime.Now;
                    pintarArbolNUEVO(ref dtValores, false, false, false);
                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO pintar árbol final";
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    dtTiempos.Rows.Add(oItemTiempo);

                    horaProcesar_2 = DateTime.Now.Subtract(horaProcesar_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO total cálculo";
                    oItemTiempo[1] = horaProcesar_2.Minutes.ToString("00") + ":" + horaProcesar_2.Seconds.ToString("00") + ":" + horaProcesar_2.Milliseconds.ToString("00");
                    dtTiempos.Rows.Add(oItemTiempo);

                    Session["datosFenolicosOriginales"] = false;

                    dataTiempos.Visible = true;
                    dataTax.Visible = true;
                    dataGastosPatio.Visible = true;
                    dataGastosOficina.Visible = true;
                    dataGastosFijos.Visible = true;
                    dataAbonoMPO.Visible = true;
                    dataBuenTrato.Visible = true;
                    //dataChatarra.Visible = true;
                    //dataLiquidacion.Visible = true;
                    //dataDevoluciones.Visible = true;
                    //dataFacturas.Visible = true;
                    dataTiempos.Columns.Clear();
                    dataTax.Columns.Clear();
                    dataGastosPatio.Columns.Clear();
                    dataGastosOficina.Columns.Clear();
                    dataGastosFijos.Columns.Clear();
                    dataAbonoMPO.Columns.Clear();
                    dataBuenTrato.Columns.Clear();
                    //dataChatarra.Columns.Clear();
                    //dataLiquidacion.Columns.Clear();
                    //dataDevoluciones.Columns.Clear();
                    //dataFacturas.Columns.Clear();
                    dataTiempos.DataSource = dtTiempos;
                    dataTax.DataSource = dtValores; // dtFenolicoNuevo; // dtValores; // dtTaximetro;
                    dataGastosPatio.DataSource = dtTaximetro; // dtAjustesBU; // dtGastosPatioCargaDescarga; // dtGastosPatioCalculo; //
                    dataGastosOficina.DataSource = dtDatosGastosFijos; // dtFechasBase; // dtCabeceraOferta; //dtDatosGastosOficina; //
                    dataGastosFijos.DataSource = dtGastosOficina; // dtGastosFijos;// dtDatosGastosFijos; 
                    dataAbonoMPO.DataSource = dtAjustesDatosBU; // dtAbonoMPO;
                    dataBuenTrato.DataSource = dtGastosFijos;
                    //dataLiquidacion.DataSource = dtAjustesBU;
                    //dataDevoluciones.DataSource = dtAjustesDatosBU; // dtAjustesGF; // dtDevoluciones;
                    //dataChatarra.DataSource = dtArticulosPaneles; // dtTopesValores; // dtChatarra;
                    //dataLiquidacion.DataSource = dtVentaLiquidacion;
                    //dataDevoluciones.DataSource = dtDevoluciones;
                    //dataFacturas.DataSource = dtFacturasMO;
                    dataTiempos.DataBind();
                    dataTax.DataBind();
                    dataGastosPatio.DataBind();
                    dataGastosOficina.DataBind();
                    dataGastosFijos.DataBind();
                    dataAbonoMPO.DataBind();
                    dataBuenTrato.DataBind();
                    dataChatarra.DataBind();
                    //dataLiquidacion.DataBind();
                    //dataDevoluciones.DataBind();
                    //dataFacturas.DataBind();
                }
            }
            catch (Exception ex)
            {
                funcionalidades.guardarIncidencia(empresa, "OFERTA", numeroOferta, "", "Calcular oferta // " + dondeVa + " // " + ex.Message, 1, usuario, User.Identity.Name.ToString());
                Informe.mostrarMensaje("Calcular oferta // " + dondeVa + " // " + ex.Message);
            }
            finally
            {
                Session["dtTiempos"] = dtTiempos;
            }
        }

    }
}