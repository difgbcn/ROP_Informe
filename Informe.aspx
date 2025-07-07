<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Informe.aspx.cs" Inherits="ROP_Informe.Informe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server" onload="setVScroll()">
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.css">
    <%--    <link href="/css/bootstrap-responsive.min.css" rel="stylesheet" media="screen" />
    <link rel="stylesheet" type="text/css" media="print" href="print.css">--%>
    <script src="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.js"></script>

    <style type="text/css">
        input[type=button] {
            background-color: #057384;
            border: none;
            color: white;
            padding: 16px 16px;
            text-decoration: none;
            margin: 2px 2px;
            cursor: pointer;
        }

        .headerSticky {
            position: sticky;
            top: 0px;
        }

        .imgSimulacionExpandirClass {
            background-image: url("Img/expand.png");
        }

        .imgSimulacionComprimirClass {
            background-image: url("Img/collapse.png");
        }

        /*Aca param cambiar ellipse estilo defs label fila*/
        .text_custom {
            background-color: rgb(252, 230, 199);
        }

        input[type=checkbox] {
            transform: scale(1.5);
        }

        .icon-rojo {
            color: red;
        }

        .modalPopupInformacio {
            background-color: #FFFFFF;
            width: 400px;
            border: 3px solid #1BEBF0;
            height: 150px;
        }

        .modalPopupInformacion .headerInformacion {
            background-color: #1BEBF0;
            height: 30px;
            color: white;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }

        .modalPopupInformacion .footerInformacion {
            padding: 3px;
            align-items: center;
            align-content: center;
        }

        .modalPopupInformacion .buttonInformacion {
            height: 25px;
            color: black;
            line-height: 23px;
            text-align: center;
            font-weight: normal;
            cursor: pointer;
            background-color: #D3CECD;
            border: 1px solid #5C5C5C;
            margin-left: 50%;
            transform: translateX(-50%);
        }

        h2.red-text {
            color: red;
        }

        .auto-style1 {
            width: 328px;
        }

        .auto-style3 {
            width: 749px;
        }

        .auto-style4 {
            width: 206px;
        }

        .auto-style5 {
            width: 204px;
        }

        .progress {
            height: 35px;
            width: 100%;
            border: 1px solid #428bca;
            border-radius: 5px;
            background-color: #e6f3fa;
            margin-bottom: 15px;
        }

        .progress-bar {
            height: 100%;
            background: #428bca;
            display: flex;
            align-items: center;
            transition: width 0.25s;
            border-radius: 5px;
        }

        .progress-bar-text {
            margin-left: 10px;
            font-weight: bold;
            color: #cce7f5;
        }

        .btnBlue {
            background-color: blue
        }

        .rojo {
            color: red
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="lblpopup" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Display="None" Text="no mostrar" Style="margin-left: 0px"></asp:Label>
    <asp:Label ID="lblIdiomaSeleccionado" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Display="None" Text="ESPAÑOL" Style="margin-left: 0px"></asp:Label>
    <asp:Label ID="lblTiempo" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White" Height="18px" Text="0"></asp:Label>
    <br />
    <div class="d-print-none">
        <div id="PREGUNTAR_MOSTRAR" runat="server" visible="false" style="border: 1px solid silver">
            <br />
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkMostrarPedidos" runat="server" Font-Bold="True" />
                    <asp:Label ID="lblMostrarPedidos" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                    <asp:Label ID="lblMostrarPedidosTexto" runat="server" Font-Size="Medium" Text="¿Mostrar pedidos?"></asp:Label>
                </div>
                <div class="col-md-3">
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="imgCatalonianInicio" runat="server" ImageAlign="right" ImageUrl="Img/catalonia.png" OnClick="imgCatalonia_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgEnglishInicio" runat="server" ImageAlign="right" ImageUrl="Img/unitedKingdom.png" OnClick="imgUnited_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgSpainInicio" runat="server" ImageAlign="right" ImageUrl="Img/spain.png" OnClick="imgSpain_Click" />
                </div>
                <div class="col-md-1">
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblDatosSolicitados" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Datos solicitados:"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblIdioma" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Idioma:"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-5">
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnContinuarCalculo" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClientClick="return simulateProgress()" OnClick="btnContinuarCalculo_Click"><span class="glyphicon glyphicon-cog">C O N T I N U A R</span></asp:LinkButton>
                </div>
                <div class="col-md-5">
                </div>
            </div>
            <br />
        </div>
        <div runat="server">
            <div id="CABECERA_MOSTRAR" runat="server">
                <%--  <div id="cronometro" class="col-md-2"></div>--%>
                <div class="row">
                    <div class="col-md-4">
                        <asp:ImageButton ID="imgLogoROP" runat="server" ImageAlign="Left" ImageUrl="Img/logoROP.png" Width="150" />
                    </div>
                    <div class="col-md-3">
                        <%--<asp:Label ID="lblVersionUtilizada" runat="server" Text="" ForeColor="Red" Visible="true" Font-Size="Small"></asp:Label>--%>
                    </div>
                    <div class="col-md-2" id="Version" runat="server">
                        <asp:Label ID="lblVersion" runat="server" Text="Versión" Visible="true" Style="margin-right: 0px"></asp:Label>
                        <asp:DropDownList ID="cmbVersion" Width="180px" runat="server" CssClass="form-control" OnSelectedIndexChanged="CambioVersionSeleccion" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="imgCatalonian" runat="server" ImageAlign="right" ImageUrl="Img/catalonia.png" OnClick="imgCatalonia_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="imgEnglish" runat="server" ImageAlign="right" ImageUrl="Img/unitedKingdom.png" OnClick="imgUnited_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="imgSpain" runat="server" ImageAlign="right" ImageUrl="Img/spain.png" OnClick="imgSpain_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblTitulo" runat="server" Text="" Visible="true" Font-Bold="true" ForeColor="Red" Font-Size="X-Large" Style="margin-right: 0px"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblVersionUtilizada" runat="server" Text="" ForeColor="Red" Visible="true" Font-Size="Small"></asp:Label>
                    </div>
                </div>
                <br />
                <address>
                    <hr />
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblConcepto" runat="server" Text="Concepto" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="cmbConcepto" runat="server" CssClass="form-control" OnTextChanged="CambioConceptoSeleccion" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="cmbEmpresa" runat="server" OnTextChanged="CambioConceptoEmpresa" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblNumero" runat="server" Text="Número" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtNumero" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="cambioNumero"></asp:TextBox>
                        </div>
                        <div class="col-md-1 align-middle-middle">
                             <asp:LinkButton class="align-middle" ID="btnBuscarInformacion" usesubmitbehavior="false" CssClass="btn btn-success" tag="Procesar" runat="server" OnClientClick="return simulateProgress();" OnClick="btnBuscarInformacion_Click"></asp:LinkButton>
                        </div>
                        <div class="col-md-1">
                            <input runat="server" id="btnImprimir" type="button" onclick="ImprimeDiv('zonaImprimir')" value="Imprimir" />
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="btnAbrirExcel" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnAbrirExcel_Click"><span class="glyphicon glyphicon-cog">Bajar excel</span></asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    <div id="TransporteFenolico" runat="server">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkBoxSoloEntrega" runat="server" Font-Bold="True" />
                                <asp:Label ID="Label7" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                                <asp:Label ID="lblSoloEntrega" runat="server" Font-Size="Medium" Text="Portes entrega"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkBoxSoloDevolucion" runat="server" Font-Bold="True" />
                                <asp:Label ID="Label8" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                                <asp:Label ID="lblSoloDevolucion" runat="server" Font-Size="Medium" Text="Portes devolución"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <%--<asp:CheckBox ID="chkBoxFenolico" runat="server" Checked="false" Font-Bold="True" />
                                <asp:Label ID="Label3" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>--%>
                                <asp:Label ID="lblFenolico" runat="server" Font-Size="Medium" Text="Con fenólico"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cmbFenolico" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div id="PreguntaFicha" runat="server" class="row" visible="false">
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkBoxCalcularPedidos" runat="server" Font-Bold="True" />
                            <asp:Label ID="lblCalcularPedidosPrevio" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                            <asp:Label ID="lblCalcularPedidos" runat="server" Font-Size="Medium" Text="Calcular ROP pedidos"></asp:Label>
                            <hr />
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkParalelo" runat="server" Font-Bold="True" Checked="true" />
                            <asp:Label ID="Label4" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                            <asp:Label ID="Label9" runat="server" Font-Size="Medium" Text="CALCULAR PEDIDOS EN PARALELO"></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="LblHilos" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Hilos: &nbsp;"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtHilos" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" aria-readonly="true">
                        <div class="col-md-1">
                            <asp:Label ID="lblCuentaCliente" runat="server" Text="Código cliente" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtCuentaCliente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblNombreCliente" runat="server" Text="Nombre cliente" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row" aria-readonly="true">
                        <div class="col-md-1">
                            <asp:Label ID="lblNombreObra" runat="server" Text="Nombre obra" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-11">
                            <asp:TextBox ID="txtNombreObra" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            <asp:TextBox ID="txtObra" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row" aria-readonly="true">
                        <div class="col-md-1">
                            <asp:Label ID="lblMasterObra" runat="server" Text="Master obra" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtMasterObra" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblCentroCoste" runat="server" Text="Centro coste" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtCentroCoste" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblMoneda" runat="server" Text="Moneda" Style="margin-left: 0px"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                </address>
            </div>
            <br />

            <div id="MENSAJE_ERROR" runat="server">
                <address>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:ImageButton ID="imgWarning" runat="server" ImageAlign="left" ImageUrl="Img/warning.png" OnClick="imgWarning_Click" />
                            &nbsp;
                            <asp:Label ID="lblMensajeError" runat="server" BackColor="#FF9900" BorderStyle="Groove" BorderColor="DarkGray" Font-Bold="False" Font-Size="Medium" Text="..." Width="96%" Style="text-align: center" ForeColor="Black"></asp:Label>
                        </div>
                    </div>
                </address>
                <br />
            </div>
            <div class="progress">
                <div id="myProgressBar" class="progress-bar active" role="progressbar" style="width: 0%;">
                    <span id="myProgressBarValue" class="progress-bar-text">0%</span>
                </div>
            </div>

            <address>
                <div id="SIMULACION" runat="server">
                    <div class="row">
                        <div class="col-md-10">
                            <asp:Label ID="lblTituloMPO" runat="server" Text='SIMULACIÓN MPO' ForeColor="Navy" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                        </div>
                        <div id="cronometro" class="col-md-2">
                        </div>
                    </div>
                    <div class="row" id="MensajeSimulacionFacturacion" runat="server">
                        <div class="col-md-10">
                            <asp:Label ID="lblMensajeSimulacionFacturacion" runat="server" Text='' BackColor="Yellow" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 align-middle-middle">
                            <asp:Label ID="lblFechaSimulacion" runat="server" AutoPostBack="true" Text="Fecha simulación" Style="margin-left: 0px" OnTextChanged="txtFechaSimulacion_TextChanged"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFechaSimulacion" runat="server" AutoPostBack="true" Width="200px" autocomplete="off" DataFormatString="{0:dd/MM/yyyy}" MaxLength="10" TextMode="Date" value="null" CssClass="form-control" OnTextChanged="txtFechaSimulacion_TextChanged"></asp:TextBox>
                            <br />
                            <asp:CheckBox ID="chkFacturarSimulacion" runat="server" AutoPostBack="True" Text="" Font-Bold="true" OnCheckedChanged="chkFacturarSimulacion_CheckedChanged" />
                            <asp:Label ID="Label1" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                            <asp:Label ID="lblFacturarSimulacion" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Facturar alquiler hasta última devolución"></asp:Label>
                            <br />
                            <br />
                            <asp:CheckBox ID="chkFacturarDiasMinimo" runat="server" AutoPostBack="True" Text="" Font-Bold="true" OnCheckedChanged="chkFacturarDiasMinimosSimulacion_CheckedChanged" />
                            <asp:Label ID="Label2" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                            <asp:Label ID="lblFacturarDiasMinimo" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Aplicar dias minimos de facturacion"></asp:Label>
                            <br />
                            <br />
                            <asp:CheckBox ID="chkFacturarNegativos" runat="server" AutoPostBack="True" Text="" Enabled="false" Font-Bold="true" OnCheckedChanged="chkFacturarNegativos_CheckedChanged" />
                            <asp:Label ID="Label5" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                            <asp:Label ID="lblFacturarNegativosSimulacion" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Facturar cantidades negativas..."></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblFacturacionOtrosConceptos" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="false" Text="Facturacion pendiente de scrap y otros conceptos"></asp:Label>
                            <asp:TextBox ID="txtFacturacionOtrosConceptos" runat="server" AutoPostBack="true" autocomplete="off" CssClass="form-control" onkeypress="return numerosDecimales(event,this);" OnTextChanged="txtFacturacionOtrosConceptos_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-md-1 align-middle-middle">
                            <br />
                            <asp:LinkButton class="align-middle" ID="btnSimular" usesubmitbehavior="false" CssClass="btn btn-danger" runat="server" OnClientClick="this.disabled = true; this.value = '...';" OnClick="btnSimular_Click" Width="80px" Height="35px" Text="SIMULAR"></asp:LinkButton>
                            <%--<asp:LinkButton class="align-middle" ID="btnSimularNuevo" usesubmitbehavior="false" CssClass="btn btn-danger" runat="server" OnClientClick="this.disabled = true; this.value = '...';" OnClick="btnSimularNuevo_Click" Width="80px" Height="35px" Text="NEW"></asp:LinkButton>--%>
                        </div>
                        <div class="col-md-1 align-middle-middle">
                        </div>
                        <div class="col-md-5 align-middle-middle">
                            <asp:GridView ID="grvSimulacion" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnRowDataBound="grvSimulacion_RowDataBound" Font-Size="Small" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CONCEPTOIDIOMA" HeaderStyle-BackColor="white" HeaderText="" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15px" ItemStyle-Width="15px" ControlStyle-Width="15px" />
                                    <asp:BoundField DataField="PREVISTA" HeaderStyle-BackColor="white" HeaderText="PREVISTA" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.0000}" />
                                    <asp:BoundField DataField="REAL SIMULADA" HeaderStyle-BackColor="white" HeaderText="REAL (SIMULADA)" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.0000}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-5 align-middle-middle">
                            <asp:Label ID="lblcomboFechas" runat="server" AutoPostBack="true" Text="Valor pvp" Style="margin-left: 0px"></asp:Label>
                            <asp:DropDownList ID="cmbFechasSimulacion" runat="server" CssClass="form-control" OnSelectedIndexChanged="cambioFechaSimulacionOpcion" AutoPostBack="true"></asp:DropDownList>
                            <br />
                            <asp:CheckBox ID="chkNegativos" runat="server" AutoPostBack="True" Text="" Font-Bold="true" OnCheckedChanged="chkNegativos_CheckedChanged" />
                            <asp:Label ID="Label6" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="&nbsp;"></asp:Label>
                            <asp:Label ID="lblNegativos" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="NEGATIVOSSSSS"></asp:Label>
                        </div>
                        <div class="col-md-2 align-middle-middle">
                        </div>
                        <div class="col-md-5 align-middle-middle">
                            <br />
                            <asp:GridView ID="grvMB" runat="server" AutoGenerateColumns="False" ShowHeader="false" CssClass="table table-hover" OnRowDataBound="grvMB_RowDataBound" Font-Size="Small" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CONCEPTO" HeaderStyle-BackColor="white" HeaderText="" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15px" ItemStyle-Width="15px" ControlStyle-Width="15px" />
                                    <asp:BoundField DataField="IMPORTE" HeaderStyle-BackColor="white" HeaderText="" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.0000}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    <div id="SIMULACIONCUADRO" runat="server">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnExcelSimulacion" Width="120px" usesubmitbehavior="false" CssClass="btn btn-primary" runat="server" OnClick="btnExcelSimulacion_Click"><span class="glyphicon glyphicon-cog">Exportar</span></asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnAbrirExcelSimulacion" Width="120px" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnAbrirExcelSimulacion_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar</span></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnExpandirSimulacion" Width="120px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnExpandirSimulacion_Click"><span class="glyphicon glyphicon-cog">Expandir</span></asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnContraerSimulacion" Width="120px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnContraerSimulacion_Click"><span class="glyphicon glyphicon-cog">Contraer</span></asp:LinkButton>
                            </div>
                        </div>
                         <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div id="theDiv" runat="server" style="overflow-y: scroll; height: 500px; width: 100%;">
                                    <input type="hidden" id="div_position" name="div_position" />
                                    <asp:HiddenField ID="SelectedGridCellIndex" runat="server" Value="-1" />
                                    <asp:GridView ID="grvDatos" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" DataKeyNames="ID, CANTIDAD ORIGINAL, FAMILIA, SUBFAMILIA, ARTICULO, PVP OBRA, MOSTRAR, MOSTRAR HIJOS, PVP ORIGINAL" OnRowEditing="grvDatos_RowEditing" OnRowCancelingEdit="grvDatos_RowCancelingEdit" OnRowUpdating="grvDatos_RowUpdating" OnRowDataBound="grvDatos_RowDataBound" Font-Size="Small" Width="100%" OnRowCreated="grvDatos_OnRowCreated" OnSelectedIndexChanged="grvDatos_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="8px" HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image ID="imagenExpandir" ImageUrl='<%# Eval("MOSTRAR").ToString()=="0" ?"~/Img/collapse.png":"~/Img/expand.png"%>' runat="server" Visible='<%# Eval("Articulo")==System.DBNull.Value || Eval("Articulo").ToString() == String.Empty ?true:false%>' Width="20px" Height="20px"></asp:Image>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="110px" HeaderText="CONCEPTO" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConceptoGrid" runat="server" Text='<%# Bind("[CONCEPTO]")%>' ToolTip='<%# Bind("[CONCEPTO COMPLETO]")%>' Width="110px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="PVP" HeaderText="PVP" Visible="false" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.00}" />

                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="Cantidad Original" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCantidadOriginal" runat="server" Text='<%# Bind("[Cantidad original]","{0:#,##0}") %>' Visible='<%# Eval("Articulo")!=System.DBNull.Value && Eval("Articulo").ToString() != String.Empty ?true:false%>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PVP ORIGINAL" HeaderText="PVP Original" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.00}" />

                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="DEV. % UDS." HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPorcentajeCantidad" runat="server" Text='<%# Bind("[PORCENTAJE DEVOLUCION]","{0:#,##0.00}") %>' ValidationExpression="\d" Width="45px" Visible='<%# Eval("Articulo") == System.DBNull.Value || Eval("Articulo").ToString() == String.Empty ?true:false%>' Enabled='<%# Eval("Articulo") == System.DBNull.Value || Eval("Articulo").ToString() == String.Empty ?true:false%>' onkeypress="return numerosEnterosSolo(event);"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="REtxtPorcentajeCantidad" runat="server" ControlToValidate="txtPorcentajeCantidad" ErrorMessage="¡ERROR!" ValidationExpression="\d{1,3}(,\d{1,2})?"></asp:RegularExpressionValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPorcentajeCantidad" runat="server" Text='<%# Bind("[PORCENTAJE DEVOLUCION]", "{0:#,##0.00}") %>' Visible='<%# Eval("Articulo")==System.DBNull.Value || Eval("Articulo").ToString() == String.Empty ?true:false%>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="Cantidad MPO" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad","{0:#,##0}") %>' ValidationExpression="\d" Width="45px" Visible='<%# Eval("Articulo")!=System.DBNull.Value && Eval("Articulo").ToString() != String.Empty ?true:false%>' Enabled='<%# Eval("Articulo")!=System.DBNull.Value && Eval("Articulo").ToString() != String.Empty ?true:false%>' onkeypress="return numerosEnterosSolo(event);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad","{0:#,##0}") %>' Visible='<%# Eval("Articulo")!=System.DBNull.Value && Eval("Articulo").ToString() != String.Empty ?true:false%>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PVP OBRA" HeaderText="PVP OBRA" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="25px" ItemStyle-Width="25px" ControlStyle-Width="25px" DataFormatString="{0:#,##0.00}" />

                                            <asp:BoundField DataField="FACTURACION" HeaderText="FACTURACIÓN" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.00}" />
                                            <asp:BoundField DataField="PORCENTAJE" HeaderText="% MPO vs Obra" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20px" ItemStyle-Width="20px" ControlStyle-Width="20px" DataFormatString="{0:0.00}" />

                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="DTO MPO" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescuento" runat="server" Text='<%# Bind("DESCUENTO", "{0:#,##0.00}") %>' ValidationExpression="\d*" Width="45px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescuento" runat="server" Text='<%# Bind("DESCUENTO","{0:#,##0.00}") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FACTURACION LIQUIDACION" HeaderText="FACTURACIÓN LIQ." ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px" DataFormatString="{0:#,##0.00}" />
                                            <asp:BoundField DataField="MOSTRAR" HeaderText="MOSTRAR" Visible="false" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="25px" ItemStyle-Width="25px" ControlStyle-Width="25px" />
                                            <asp:BoundField DataField="MOSTRAR HIJOS" HeaderText="MOSTRAR HIJOS" Visible="false" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="25px" ItemStyle-Width="25px" ControlStyle-Width="25px" />

                                            <asp:CommandField HeaderText="..." ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="80px" ItemStyle-Width="80px" ControlStyle-Width="30px" />
                                        </Columns>
                                        <HeaderStyle BackColor="#17a2b8" ForeColor="white" CssClass="headerSticky" />
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#8da8f9" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </address>
        </div>
    </div>
    <div class="d-none d-print-block">
        <asp:Label ID="lblNomreInformeImprimir" runat="server" Text="INFORME ROP" Visible="true" Font-Bold="true" ForeColor="Red" Font-Size="XX-Large" Style="margin-right: 0px"></asp:Label>
        <br />
        <asp:TextBox ID="lblCabeceraImprimir1" AcceptsTab="True" runat="server" BorderStyle="None" AutoPostBack="true" Text="" Style="margin-left: 0px" Width="100%"></asp:TextBox>
        <br />
        <asp:TextBox ID="lblCabeceraImprimir2" AcceptsTab="True" runat="server" BorderStyle="None" AutoPostBack="true" Text="" Style="margin-left: 0px" Width="100%"></asp:TextBox>
        <br />
        <asp:TextBox ID="lblCabeceraImprimir3" AcceptsTab="True" runat="server" BorderStyle="None" AutoPostBack="true" Text="" Style="margin-left: 0px" Width="100%"></asp:TextBox>
        <br />
        <asp:TextBox ID="lblCabeceraImprimir4" AcceptsTab="True" runat="server" BorderStyle="None" AutoPostBack="true" Text="" Style="margin-left: 0px" Width="100%"></asp:TextBox>
        <br />
        <br />
    </div>

    <div id="botonVisualizarOferta" runat="server" class="row d-print-none">
        <div id="botonesTabla" runat="server" class="col-md-4 d-print-none">
            <input runat="server" id="btnDesplegar" type="button" onclick="expandirTodo()" value="EXPANDIR" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <input runat="server" id="btnContraer" type="button" onclick="contraerTodo()" value="CONTRAER" />
        </div>
       <%-- <div class="col-md-10 align-middle-middle">
        </div>--%>
        <div class="col-md-2 align-middle-middle">
            <asp:LinkButton class="align-middle" ID="btnVisualizarOferta" usesubmitbehavior="false" CssClass="btn btn-warning btn-lg btn-block" runat="server" OnClick="btnVisualizarOferta_Click"></asp:LinkButton>
        </div>
    </div>
    <br />
   <%-- <div id="botonesTabla" runat="server" class="col-md-12 d-print-none">
        <input runat="server" id="btnDesplegar" type="button" onclick="expandirTodo()" value="EXPANDIR" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input runat="server" id="btnContraer" type="button" onclick="contraerTodo()" value="CONTRAER" />
        <br />
        <br />
    </div>--%>

    <div id="zonaImprimir" runat="server">
        <table id="table" data-detail-view="true" class="table table-striped table-borderless">
            <thead>
                 <tr>
                    <th data-field="Concepto" class="auto-style3">Concepto</th>
                     <th data-field="Importe" data-align="right" data-cell-style="cellStyle" class="auto-style1">Importe</th>
                    <th data-field="Porcentaje" data-align="right" data-cell-style="cellStyle" class="auto-style5">%</th>
                    <th data-field="ImportePrevisto" data-align="right" data-cell-style="cellStyle" class="auto-style4">Importe previsto</th>
                    <th data-field="PorcentajePrevisto" data-align="right" data-cell-style="cellStyle" class="auto-style5">%</th>
                   <%-- <th data-field="Importe" data-align="right" data-cell-style="cellStyle" data-formatter="formatMoney" class="auto-style1">Importe</th>
                    <th data-field="Porcentaje" data-align="right" data-cell-style="cellStyle" data-formatter="formatMoney" class="auto-style5">%</th>
                    <th data-field="ImportePrevisto" data-align="right" data-cell-style="cellStyle" data-formatter="formatMoney" class="auto-style4">Importe previsto</th>
                    <th data-field="PorcentajePrevisto" data-align="right" data-cell-style="cellStyle" data-formatter="formatMoney" class="auto-style5">%</th>--%>
                </tr>
            </thead>
        </table>
        &nbsp;&nbsp;
    </div>
    <div id="lanzamientos" runat="server" class="d-print-none">
        <br />
        <div class="row">
            <div class="col-md-4">
                <asp:LinkButton ID="btnProcesarFichas" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClientClick="return simulateProgress()" OnClick="btnProcesarFichas_Click"><span class="glyphicon glyphicon-cog">PROCESAR FICHAS 2024</span></asp:LinkButton>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblHilosFichas" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="Hilos fichas: &nbsp;"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtHilosFichas" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:LinkButton ID="btnProcesarFichasHilos" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClientClick="return simulateProgress()" OnClick="btnProcesarFichasHilos_Click"><span class="glyphicon glyphicon-cog">PROCESAR FICHAS HILOS 2024</span></asp:LinkButton>
            </div>
        </div>
        <br />
        <br />
        <br />
        <address>
            <div class="row">
                <asp:Label ID="Label10" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="TAXIMETROS"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataTax" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label11" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="GASTOS OFICINA"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataGastosOficina" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label12" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="GASTOS FIJOS"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataGastosFijos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label13" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="GASTOS PATIO"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataGastosPatio" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label16" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="ABONO MPO"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataAbonoMPO" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label14" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="BUEN TRATO"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataBuenTrato" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label15" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="AJUSTE x OCUPACIÓN"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataChatarra" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label17" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="AJUSTE GASTOS FIJOS"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataLiquidacion" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="Label18" runat="server" Enabled="false" Font-Size="Medium" Font-Bold="true" Text="LÍNEAS ARTÍCULOS"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataLineasArticulos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="dataFacturas" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
        </address>
        <asp:GridView ID="dataTiempos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
        </asp:GridView>
        <asp:GridView ID="dataTiemposPedidos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
        </asp:GridView>
        <asp:GridView ID="dataTiemposAjustes" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
        <br />

        <address>
            <div id="DATOS_CALCULOS" class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblIdiomaCalculo" runat="server" Text="" Visible="false"></asp:Label>
                    <%--<asp:GridView ID="gridView_dtFichaCalcularSimulacionEurosDias" runat="server" AutoGenerateColumns="True" visible="false" ></asp:GridView>--%>
                </div>
            </div>
            <br />
        </address>
        <br />
        <ajax:ModalPopupExtender ID="mpeInformacion" PopupControlID="PanelInformacion" TargetControlID="lblpopup" CancelControlID="btnCerrarInformacion" PopupDragHandleControlID="headerdivInformacion" runat="server"></ajax:ModalPopupExtender>
        <asp:Panel ID="PanelInformacion" Style="display: none;" CssClass="modalPopupInformacion" runat="server">
            <div id="headerdivInformacion" class="headerInformacion">
                <asp:Label ID="lblTituloInformacion" runat="server" Font-Bold="true" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
            </div>
            <div id="divdeateilsInformacion"></div>
            <br />
            &nbsp;
                <asp:Label ID="lblMensajeInformacion" runat="server" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
            <br />
            <br />
            <div id="footerdivInformacion" class="footerInformacion">
                <asp:Button ID="btnCerrarInformacion" runat="server" Text="Cerrar" class="buttonInformacion" HorizontalAlign="Center" />
            </div>
        </asp:Panel>
    </div>

    <script>
        var myTituloConcepto = '<%=tituloConcepto%>';
        var myTituloImporte = '<%=tituloImporte%>';
        var myTituloImportePrevisto = '<%=tituloImportePrevisto%>';
        var mydata =<%=datosJson%>;

        var tablas = new Array()
        $(function () {
            $('#table').bootstrapTable({
                tituloConcepto: myTituloConcepto,
                tituloImporte: myTituloImporte,
                tituloImportePrevisto: myTituloImportePrevisto,
                data: mydata,
                detailView: true,
                icons: {
                    "detailOpen": "fa fa-caret-square-o-down fa-lg",
                    "detailClose": "fa fa-caret-square-o-up fa-lg icon-rojo"
                },
                detailFilter: function (index, row) {
                    return row.hijo != null;
                },
                onExpandRow: function (index, row, $detail) {
                    if (row.hijo != null)
                        expandirTabla(index, row, $detail);
                }
            });
            $('#table').find('th').eq(1).text(myTituloConcepto);
            $('#table').find('th').eq(2).text(myTituloImporte);
            $('#table').find('th').eq(4).text(myTituloImportePrevisto);
            $('#table')[0].classList.value = "table table-bordered";

            if (myTituloImportePrevisto == "") 
            {
                $('#table').bootstrapTable('hideColumn', 'ImportePrevisto');
                $('#table').bootstrapTable('hideColumn', 'PorcentajePrevisto');
            }
            else
            {
                $('#table').bootstrapTable('showColumn', 'ImportePrevisto');
                $('#table').bootstrapTable('showColumn', 'PorcentajePrevisto');
            }

            if (mydata.colorEncabezado != null && mydata.colorEncabezado != "")
                $('#table')[0].tHead.style = "background-color:" + row.colorEncabezado;

            $('#table').find('th').eq(1).text(myTituloConcepto);
            $('#table').find('th').eq(2).text(myTituloImporte);
            $('#table').find('th').eq(4).text(myTituloImportePrevisto);

        });

        function expandirTabla(index, row, $detalle) {
            if (row.hijo != null && row.hijo.length > 0) {
                var $el = $detalle.html('<table></table>').find('table')
                var i; var j; var row
                var columns = []
                var data = []
                var rows = row.hijo.length;
                var columnas = 0;

                if (rows > 0) {
                    columnas = Object.keys(row.hijo[0]).filter(fila => fila != "hijo" && fila != "colorEncabezado" && fila != "colorFila");
                }
                else
                    return;

                for (i = 0; i < columnas.length; i++) {
                    if (myTituloImportePrevisto == "" && (i > 2)) {
                        continue;
                    }

                    var alineacion = "left";
                    if (i >= 1)
                        alineacion = "right";
                    else
                        alineacion = "left";

                    columns.push({
                        field: columnas[i],
                        // NO COLOCA EL TÍTULO EN CADA CONCEPTO
                        //title: columnas[i],
                        sortable: true,
                        align: alineacion,
                        cellStyle: cellStyle
                    })
                }

                for (i = 0; i < rows; i++) {
                    var rowtmp = row.hijo[i];
                    data.push(rowtmp)
                }

                tabla = $el.bootstrapTable({
                    columns: columns,
                    data: data,
                    detailView: true,

                    icons: {
                        "detailOpen": "fa fa-caret-square-o-down fa-lg",

                        "detailClose": "fa fa-caret-square-o-up fa-lg icon-rojo"
                    },
                    detailFilter: function (index, row) {
                        return row.hijo != null;
                    },
                    onExpandRow: function (index, row, $detail) {
                        expandirTabla(index, row, $detail)
                    }
                })
                $el[0].classList.value = "table table-bordered";
                tablas.push(tabla)
                if (row.colorEncabezado != null && row.colorEncabezado != "")
                    $el[0].tHead.style = "background-color:" + row.colorEncabezado;
            }
        }

        function cellStyle(value, row, index) {
            if (row.colorFila.length > 0 && value.indexOf("*") == -1) {
                return { classes: row.colorFila }
                //return {classes: 'text_custom'}
            }
            else {
                return { classes: 'text-dark' }
            }
        }

        //function formatMoney(value) {
        //    return new Intl.NumberFormat('es-ES', {
        //            style: 'decimal',
        //        minimumFractionDigits: 2,
        //        maximumFractionDigits: 2
        //    }).format(value);
        //}

        //function formatMoney(value) {
        //    // Convertir el valor a número (por si llega como string)
        //    let num = parseFloat(value);

        //    // Comprobar si es un número válido
        //    if (isNaN(num)) return "-"; // O cualquier otro mensaje que prefieras

        //    return new Intl.NumberFormat('es-ES', {
        //        style: 'decimal',
        //        minimumFractionDigits: 2,
        //        maximumFractionDigits: 2
        //    }).format(num);
        //}

        //function formatMoney(value) {
        //    let num = Number(value);
        //    if (isNaN(num)) return "-";

        //    return new Intl.NumberFormat('es-ES', {
        //        style: 'decimal',
        //        minimumFractionDigits: 2,
        //        maximumFractionDigits: 2
        //    }).format(num);
        //}

        var flg = true;
        function ponerSpinner() {
            if (flg) {
                try {
                    var btn = $("#<%=btnBuscarInformacion.ClientID%>");

                    var lbl = $("#<%=lblIdiomaSeleccionado.ClientID%>");
                    if (lbl == "CATALAN")
                        btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>¡Espereu!');//.addClass('disabled');
                    if (lbl == "INGLES")
                        btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>¡Wait!');//.addClass('disabled');
                    if (lbl == "ESPAÑOL")
                        btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>¡Espere!');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnBuscarInformacion', '');
                return true;
            }
        }

        var flg = true;
        function ponerSpinnerContinuar() {
            if (flg) {
                try {
                    var btn = $("#<%=btnContinuarCalculo.ClientID%>");

                    var lbl = $("#<%=lblIdiomaSeleccionado.ClientID%>");
                    if (lbl == "CATALAN")
                        btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>¡Espereu!');//.addClass('disabled');
                    if (lbl == "INGLES")
                        btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>¡Wait!');//.addClass('disabled');
                    if (lbl == "ESPAÑOL")
                        btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>¡Espere!');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnContinuarCalculo', '');
                return true;
            }
        }

        function numerosEnterosSolo(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if ((unicode < 48 || unicode > 57) && unicode != 45)//if not a number
            { return false } //disable key press    
        }

        function numerosDecimales(e, thix) {
            var keynum = window.event ? window.event.keyCode : e.which;
            if (document.getElementById(thix.id).value.indexOf('.') != -1 && keynum == 46)
                return false;
            if (document.getElementById(thix.id).value.indexOf('.') != -1 && keynum == 45)
                return false;
            if ((keynum == 8 || keynum == 48 || keynum == 46 || keynum == 45))
                return true;
            if (keynum <= 47 || keynum >= 58) return false;
            return /\d/.test(String.fromCharCode(keynum));
        }

        function ImprimeDiv(divId) {
            window.print();
        }

        function expandirTodo() {
            $('#table').bootstrapTable('expandAllRows')
            var tablasAbiertas = 0
            while (tablasAbiertas != tablas.length) {
                tablasAbiertas = tablas.length
                tablas.map(hijo => hijo.bootstrapTable('expandAllRows'))
            }
        }

        function contraerTodo() {
            $('#table').bootstrapTable('collapseAllRows')
            var tablasAbiertas = 0
            while (tablasAbiertas != tablas.length) {
                tablasAbiertas = tablas.length
                tablas.map(hijo => hijo.bootstrapTable('collapseAllRows'))
            }

            tablas = new Array()
        }

        function updateProgressBar(progressValue) {
            const progressBar = document.getElementById('myProgressBar');
            progressBar.style.width = progressValue + '%';
            const progressBarValue = document.getElementById('myProgressBarValue');
            progressBarValue.innerHTML = progressValue + ' %';
        }

        function simulateProgress() {
            const tiempo = document.getElementById('<%=lblTiempo.ClientID%>').innerText;
            //console.log("tiempo", tiempo);
            let startDate = new Date();
            //console.log("stardate", startDate);
            var interval = setInterval(function () {
                var now = new Date().getTime(); // Obtener timestamp actual
                var dif = (now - startDate) / 1000; // Diferencia en segundos
                var progress = Math.round((dif * 100) / parseFloat(tiempo));

                if (progress >= 100) {
                    //clearInterval(interval);
                    //simulateProgress();
                } else {
                    updateProgressBar(progress);
                }
            }, 100);
        }

        let cronometro = document.getElementById("cronometro");
        let final = Date.now() + 180000;
        let elcrono = setInterval(tiempo, 10);
        function tiempo() {
            var tiempo = "<%=Session.Timeout*60%>";
            if (tiempo <= 180) {
                let diferencia = final - Date.now();
                let sg = diferencia / 1000
                if (diferencia <= 0) {
                    clearInterval(elcrono);
                    cronometro.classList.add('rojo');
                    sg = 0.0;
                }
                cronometro.innerHTML = sg;
            }
        }
    </script>
</asp:Content>
