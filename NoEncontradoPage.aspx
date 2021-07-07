<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="NoEncontradoPage.aspx.cs" Inherits="ROP_Informe.ErrorPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Error:</h2>
    <p></p>
   <asp:Label ID="lblMensajeError" runat="server" Text="PÁGINA NO ENCONTRADA" Font-Size="XX-Large" style="color: red"></asp:Label>

    <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
        <p>&nbsp;</p>
        <p>
            <asp:Image ID="imgNoEncontrado" runat="server" ImageAlign="Middle" ImageUrl="~/Img/noEncontradda.png" />
        </p>
    </asp:Panel>
</asp:Content>
