<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ROP_Informe.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.css">
    <script src="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.js"></script>

    <style type="text/css">
        .icon-rojo {
            color: red;
        }
        .modalPopupInformacion
        {
            background-color:#FFFFFF;
            width: 400px;
            border: 3px solid #1BEBF0;
            height: 150px;
        }
        .modalPopupInformacion .headerInformacion
        {
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
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="lblpopup" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Display="None" Text="no mostrar" Style="margin-left: 0px"></asp:Label>
    <br />
    <div class="row">
        <div class="col-md-3">
            <h2>INFORME ROP</h2>
        </div>
        <%-- <div class="col-md-1">
            <asp:Label ID="lblVersion" runat="server" Text="Versión" Style="margin-right: 0px"></asp:Label>
         </div>--%>
         <div class="col-md-2">
            <asp:Label ID="lblVersion" runat="server" Text="Versión" Style="margin-right: 0px"></asp:Label>
            <asp:DropDownList ID="cmbVersion" runat="server" CssClass="form-control" OnSelectedIndexChanged="CambioVersionSeleccion" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <asp:Label ID="lblVersionUtilizada" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
        </div>
        <div class="col-md-2">
            <asp:Label ID="lblFecha" runat="server" Text="Fecha" Style="margin-right: 0px"></asp:Label>
            <asp:TextBox ID="txtFecha" runat="server"  AutoPostBack="False" Width="150px"  autocomplete="off"  MaxLength="10" textmode="Date" value=null CssClass="form-control"></asp:TextBox>
        </div>
       <%-- <div class="col-2 align-self-left">
            <div class="input-append date" id="datetimepickerFecha" data-date-format="dd-mm-yyyy">
                <asp:TextBox runat="server" class="span2" size="16" type="text" ReadOnly="" ID="txtFecha"></asp:TextBox>
                <span class="add-on"><i class="icon-th"></i></span>
            </div>
        </div>--%>
        <div class="col-md-3">
             &nbsp;&nbsp;
            <asp:ImageButton id="imgCatalonian" runat="server" ImageAlign="right" ImageUrl="Img/catalonia.png" OnClick="imgCatalonia_Click"/>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton id="imgEnglish" runat="server" ImageAlign="right" ImageUrl="Img/unitedKingdom.png" OnClick="imgUnited_Click"/>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton id="imgSpain" runat="server" ImageAlign="right" ImageUrl="Img/spain.png" OnClick="imgSpain_Click"/>
        </div>
    </div>
    <br />
    <address>
        <hr />
        <div class="row">
            <div class="col-md-1">
                <%--<span>Concepto</span>--%>
                <asp:Label ID="lblConcepto" runat="server" Text="Concepto" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="cmbConcepto" runat="server" CssClass="form-control" OnSelectedIndexChanged="CambioConceptoSeleccion" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-md-1">
               <%-- <span>Empresa</span>--%>
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="cmbEmpresa" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                 <%--<span>Número</span>--%>
                <asp:Label ID="lblNumero" runat="server" Text="Número" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <%--<div class="col-md-1">    
                <asp:CheckBox ID="chkBoxPortes" runat="server" Checked="false" Font-Bold="True"/>
                <asp:Label ID="lblPortes" runat="server" Font-Size="Medium" Text="Con portes"></asp:Label>
            </div> --%>
            <div class="col-md-1">
                <asp:LinkButton ID="btnBuscarInformacion" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnBuscarInformacion_Click"><span class="glyphicon glyphicon-cog">Obtener datos</span></asp:LinkButton>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnAbrirExcel" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnAbrirExcel_Click"><span class="glyphicon glyphicon-cog">Bajar excel</span></asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">    
                <asp:CheckBox ID="chkBoxPortes" runat="server" Checked="false" Font-Bold="True"/>
                <asp:Label ID="lblPortes" runat="server" Font-Size="Medium" Text="Con portes"></asp:Label>
            </div>
            <div class="col-md-3">    
                <asp:CheckBox ID="chkBoxFenolico" runat="server" Checked="false" Font-Bold="True"/>
                <asp:Label ID="lblFenolico" runat="server" Font-Size="Medium" Text="Con fenólico"></asp:Label>
            </div>
           <%-- <div class="col-md-1">
                <asp:Label ID="lblItem" runat="server" Text="Ítem" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtItem" runat="server" CssClass="form-control"></asp:TextBox>
            </div>--%>
        </div> 
        <hr />
        <%--<div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblDatosGenerales" runat="server" Text="Datos generales" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-11">
                 <asp:TextBox ID="txtNombreOferta" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblObra" runat="server" Text="Obra/Pedido" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtObra" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lblNombreObra" runat="server" Text="Nombre obra/pedido" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-5">
                <asp:TextBox ID="txtNombreObra" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lblCuentaCliente" runat="server" Text="Código cliente" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtCuentaCliente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblMasterObra" runat="server" Text="Master obra" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtMasterObra" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lblCentroCoste" runat="server" Text="Centro coste" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtCentroCoste" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
            </div>
            <div class="col-md-1">
                <asp:Label ID="lblMoneda" runat="server" Text="Moneda" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <hr />
  <%--      <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" Height="25px" Text="Concepto" Style="margin-left: 0px"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="cmbConcepto" runat="server" Width="100px" Height="20px" Font-Size="Small">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Height="25px" Text="Empresa" Style="margin-left: 0px"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="cmbEmpresa" runat="server" Width="75px" Height="20px" Font-Size="Small">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Height="25px" Text="Número " Width="54px"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtNumero" runat="server" Height="25px" Width="150px" Font-Size="Small"></asp:TextBox>
        &nbsp;
        &nbsp;    
        <asp:LinkButton ID="btnBuscarInformacion" usesubmitbehavior="false" OnClientClick="return ponerSpinner()" CssClass="btn btn-info" runat="server" OnClick="btnBuscarInformacion_Click"><span class="glyphicon glyphicon-cog">Obtener datos</span></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="btnAbrirExcel" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcel_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar excel</span></asp:LinkButton>
        &nbsp;--%>
    </address>
    <address>
        <div class="row">
            <div class="col-md-12">
                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Img/warning.png" AlternateText="" />
                &nbsp;
                <asp:Label ID="lblMensajeError" runat="server" BackColor="#FF9900" BorderStyle="Groove" BorderColor="DarkGray" Font-Bold="False" Font-Size="Medium" Text="..." Width="96%" Style="text-align: center" ForeColor="Black"></asp:Label>
                <%--<asp:Label ID="lblMensajeError" runat="server" BackColor="Black" BorderStyle="None" Font-Bold="False" Font-Size="Medium" ForeColor="#FFFF66" Text="..." Width="96%" Style="text-align: center"></asp:Label>--%>
            </div>
        </div>
    </address>
    <br />
  
    <table id="table" data-detail-view="true" class="table table-striped table-borderless">
         <thead>
             <tr>
                 <th data-field="Concepto">Concepto</th>
                 <th data-field="Importe" data-align="right">Importe</th>
                 <th data-field="%" data-align="right">%</th>
             </tr>
         </thead>
     </table>
    &nbsp;&nbsp;
  
    <br />
    <br />
    <hr />
    <br />
    <br />
     <address>
         <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblInformacionVisualizar" runat="server" Text="INFORMACION VISUALIZAR" Style="margin-left: 0px"></asp:Label>
                <asp:DropDownList ID="cmbFichaCalculos" runat="server" CssClass="form-control" OnSelectedIndexChanged="cambioFichaDesplegableOpcion" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="dataInformacion" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small"></asp:GridView>
            </div>
        </div>
    </address>
     <br />
    <asp:GridView ID="dataTiempos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />
    <asp:GridView ID="dataMovimientos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />
    <asp:GridView ID="dataPatio" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />
  <%--  <asp:GridView ID="dataMovimientosTradicionales" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />
    <asp:GridView ID="dataDatos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>--%>

   <ajax:ModalPopupExtender ID="mpeInformacion" PopupControlID="PanelInformacion"  TargetControlID="lblpopup" CancelControlID="btnCerrarInformacion" PopupDragHandleControlID="headerdivInformacion" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelInformacion"  Style="Display:none;" CssClass="modalPopupInformacion" runat="server">
        <div id ="headerdivInformacion" class="headerInformacion">
            <asp:Label ID="lblTituloInformacion" runat="server" Font-Bold="true" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
        </div>
        <div id ="divdeateilsInformacion"></div>
            <br />
            &nbsp;
            <asp:Label ID="lblMensajeInformacion" runat="server" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
            <br />
            <br />
        <div id ="footerdivInformacion" class="footerInformacion">
            <asp:Button ID="btnCerrarInformacion" runat="server" Text="Cerrar" class="buttonInformacion" HorizontalAlign="Center"/>
        </div>
    </asp:Panel>

 <%--  <script type="text/javascript">
          $(function () {
              $('#datetimepickerFecha').datepicker();
          });
    </script>--%>

    <script>
        var myTituloConcepto = '<%=tituloConcepto%>';
        var myTituloImporte = '<%=tituloImporte%>';
        var mydata =<%=datosJson%>;

        $(function () {
            $('#table').bootstrapTable({
                tituloConcepto: myTituloConcepto,
                tituloImporte: myTituloImporte,
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

            $('#table')[0].classList.value = "table table-bordered";
            if (mydata.colorEncabezado != null && mydata.colorEncabezado != "")
                $('#table')[0].tHead.style = "background-color:" + row.colorEncabezado;
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
                    columnas = Object.keys(row.hijo[0]).filter(fila => fila != "hijo" && fila != "colorEncabezado");
                }
                else
                    return;

                for (i = 0; i < columnas.length; i++) {
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
                        align: alineacion
                    })
                }

                for (i = 0; i < rows; i++) {
                    var rowtmp = row.hijo[i];
                    data.push(rowtmp)
                }

                $el.bootstrapTable({
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
                $el[0].classList.value = "table  table-bordered";

                if (row.colorEncabezado != null && row.colorEncabezado != "")
                    $el[0].tHead.style = "background-color:" + row.colorEncabezado;
            }
        }

        var flg = true;
        function ponerSpinner() {
            if (flg) {
                try {
                    var btn = $("#<%=btnBuscarInformacion.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Procesando');//.addClass('disabled');
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

       <%-- function ponerSpinnerExcel() {
            if (flg) {
                try {
                    var btn = $("#<=btnExcel.ClientID%>");
                     btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Generando');//.addClass('disabled');
                     btn.prop("disabled", true);
                     flg = false;
                 }
                 catch (e) {
                     console.log("Boton buscar error script");
                 }
                 javascript: __doPostBack('ctl00$MainContent$btnExcel', '');
                 return true;
             }
         }--%>

        //var $table = $('#table')
        //var $button = $('#button')
          
        //$(function() {
        //    button.click(function () {
        //        $table.bootstrapTable('updateColumnTitle', {
        //            field: 'Concepto',
        //            title: 'Concepto cambiado'
        //        })
        //    })
        //})


    </script>
</asp:Content>
