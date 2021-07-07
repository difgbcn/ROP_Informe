<%@ Page Title="Datos WS Axapta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informe.aspx.cs" Inherits="ROP_Informe.Informe" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="height: 11px; width: 667px">&nbsp;</h1>
<h1 style="height: 65px; width: 667px">INFORME ROP</h1>
    <h2>&nbsp;</h2>
    <address>
        &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Height="20px" Text="Empresa"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="cmbEmpresa" runat="server" Width="100px" Height="20px">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Height="20px" Text="Número oferta"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="txtNumero" runat="server" Height="20px" Width="228px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBuscarInformacion" runat="server" Text="Buscar información" OnClick ="btnBuscarInformacion_Click" Height="20px" style="cursor: pointer; cursor: hand;"/>
    </address>
    <address>
        <asp:Label ID="lblMensajeError" runat="server" BackColor="Red" BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#FFFF99" Text="..." Width="100%"></asp:Label>
    </address>

    <address>
        <%--<asp:GridView ID="dataDatos_1" runat="server" AutoGenerateColumns="True" Width="900px" ViewStateMode="Enabled" AllowPaging="True" Font-Names="Arial" Font-Size="Small">--%>
        <asp:GridView ID="dataDatos_1" runat="server" AutoGenerateColumns="True" style="width: 875px; overflow: auto; height: 160px;" ViewStateMode="Enabled" AllowPaging="True" Font-Names="Arial" Font-Size="Small">
        </asp:GridView>
    </address>
</asp:Content>
