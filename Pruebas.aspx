<%@ Page Title="Home Page" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pruebas.aspx.cs" Inherits="ROP_Informe.Pruebas" %>
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
            <h2>PRUEBAS ROP</h2>
        </div>
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
    <div class="modal-body">
        <div class="row">
            <div class="col-md-2" style="font-weight: bold;">
                Usar Paralelo:
            </div>
            <div class="col-md-2">
                <span id="txtParalelo" runat="server"></span>
            </div>
            <div class="col-md-2" style="font-weight: bold;">
                Ítems x paquete:
            </div>
            <div class="col-md-2">
                <span id="txtpaquete" runat="server"></span>
            </div>
            <div class="col-md-2" style="font-weight: bold;">
                Reintentar:
            </div>
            <div class="col-md-2">
                <span id="txtReintentar" runat="server"></span>
            </div>
        </div>
    </div>
    <br />
    <address>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <asp:FileUpload ID="ficheroSeleccionado" Font-Size="Small" CssClass="form-control" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btnSubirDatos" usesubmitbehavior="false" CssClass="btn btn-primary" runat="server" OnClick="btnSubirDatos_Click"><span class="glyphicon glyphicon-cog">Datos excel</span></asp:LinkButton>
            </div>
           <%-- <div class="col-md-1">    
                <asp:CheckBox ID="chkParalelos" runat="server" Checked="false" Font-Bold="True"/>
                <asp:Label ID="lblParalelos" runat="server" Font-Size="Medium" Text="¿Paralelo?"></asp:Label>
            </div> 
            <div class="col-md-1">
                <asp:Label ID="lblHilos" runat="server" Text="Máx hilos" Style="margin-right: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtHilos" runat="server" AutoPostBack="False" Width="100px"  autocomplete="off"  MaxLength="5" DataFormatString="{0:n0}" value="0" CssClass="form-control" OnTextChanged="txtHilos_TextChanged"></asp:TextBox>
            </div>--%>
            <div class="col-md-2">
                <asp:LinkButton ID="btnBuscarInformacion" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnBuscarInformacion_Click"><span class="glyphicon glyphicon-cog">Obtener datos</span></asp:LinkButton>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btnPruebas" with="100px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnPruebas_Click"><span class="glyphicon glyphicon-cog">Envío masivo</span></asp:LinkButton>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btnCalcularPaquetes" usesubmitbehavior="false" CssClass="btn btn-danger" runat="server" OnClick="btnCalcularPaquetes_Click"><span class="glyphicon glyphicon-cog">Ofertas masivo</span></asp:LinkButton>
            </div>
        </div>
        <hr />
    </address>
  <%--   <address>
        <hr />
        <div class="row">
            <div class="col-md-12">
                <asp:LinkButton ID="btnPruebas" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnPruebas_Click"><span class="glyphicon glyphicon-cog">Pruebas de envío masivo</span></asp:LinkButton>
            </div>
        </div>
        <hr />
    </address>--%>
    <address>
        <div class="row">
            <div class="col-md-12">
                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Img/warning.png" AlternateText="" />
                &nbsp;
                <asp:Label ID="lblMensajeError" runat="server" BackColor="#FF9900" BorderStyle="Groove" BorderColor="DarkGray" Font-Bold="False" Font-Size="Medium" Text="..." Width="96%" Style="text-align: center" ForeColor="Black"></asp:Label>
            </div>
        </div>
    </address>
   <%-- <br />
    <br />
    <br />
    <br />
    <asp:GridView ID="dataTiempos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />--%>
    <asp:GridView ID="dataDatos" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />
    <asp:GridView ID="dataResultados" runat="server" AutoGenerateColumns="True" Style="width: 875px; overflow: auto; height: 125px;" ViewStateMode="Enabled" AllowPaging="False" Font-Names="Arial" Font-Size="Small">
    </asp:GridView>
    <br />

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
    </script>
</asp:Content>
