<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="ErrorPage.aspx.cs" Inherits="ROP_Informe.ErrorPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Error de las tabla SQL / Copias de AXAPTA:</h2>
    <p></p>
    <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="true">
        <p>&nbsp;</p>
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="grvTablas" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                        <Columns>
                            <asp:BoundField DataField="OrigenDatos" HeaderText="Tabla" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                            <asp:BoundField DataField="FechaValidada" HeaderText="Fecha validada" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                            <asp:CheckBoxField DataField="DatosOk"  HeaderText="¿Dato OK?" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="100" />
                        </Columns>
                        <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
