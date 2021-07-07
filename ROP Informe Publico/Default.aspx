<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ROP_Informe.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.css">
    <script src="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.js"></script>

    <style type="text/css">
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
        .modalPopupInformacion .footerInformacion
        {
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
    <h2>INFORME ROP</h2>
    <br />
    <address>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" Height="25px" Text="Concepto" Style="margin-left: 0px"></asp:Label>
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
<%--        <asp:checkBox ID="chkFecha" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="Large" Height="25px" Style="margin-left: 0px" />
        <asp:Label ID="lblFecha" runat="server" Font-Bold="True" Font-Size="Large" Text="Fecha on-line"></asp:Label>--%>
        &nbsp;    
        <asp:LinkButton ID="btnBuscarInformacion" usesubmitbehavior="false" OnClientClick="return ponerSpinner()" CssClass="btn btn-info" runat="server" OnClick="btnBuscarInformacion_Click"><span class="glyphicon glyphicon-cog">Obtener datos</span></asp:LinkButton>
        &nbsp;
     <%--   <asp:LinkButton ID="btnExcel" usesubmitbehavior="false" OnClientClick="return ponerSpinnerExcel()" CssClass="btn btn-info" runat="server" OnClick="btnExportar_Click"><span class="glyphicon glyphicon-cog">Exportar excel</span></asp:LinkButton>
        &nbsp;--%>
        <asp:LinkButton ID="btnAbrirExcel" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcel_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar excel</span></asp:LinkButton>
        &nbsp;
    </address>
    <address>
        <asp:Label ID="lblMensajeError" runat="server" BackColor="Black" BorderStyle="None" Font-Bold="False" Font-Size="Medium" ForeColor="#FFFF66" Text="..." Width="100%" Style="text-align: center"></asp:Label>
    </address>
    <address>
        &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" Height="18px" Text="Datos generales" Style="margin-left: 0px" Width="133px"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtNombreOferta" runat="server" Height="20px" Width="100%" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
    </address>
    &nbsp;&nbsp;
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
    &nbsp;&nbsp;
    <br />
    <br />
    &nbsp;&nbsp;
    <br />
    <br />
    &nbsp;&nbsp;
    <br />
    <br />
    &nbsp;&nbsp;
    <br />
    <br />
    &nbsp;&nbsp;
    <br />
    <asp:GridView ID="dataTiempos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />
    <asp:GridView ID="dataDatos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>

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

    <script>
        var mydata =<%=datosJson%>;

        $(function () {
            $('#table').bootstrapTable({
                data: mydata,
                detailView: true,
                icons: {
                    "detailOpen": "fa fa-caret-square-o-down",
                    "detailClose": "fa fa-caret-square-o-up"
                },
                detailFilter: function (index, row) {
                    return row.hijo != null;
                },
                onExpandRow: function (index, row, $detail) {
                    if (row.hijo != null)
                        expandirTabla(index, row, $detail);
                }
            });
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
                        "detailOpen": "fa fa-caret-square-o-down",
                        "detailClose": "fa fa-caret-square-o-up"
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
                    var btn = $("#<%=btnExcel.ClientID%>");
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
    </script>
</asp:Content>
