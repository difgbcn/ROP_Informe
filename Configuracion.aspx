<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="ROP_Informe.Configuracion" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.css">
    <script src="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    
    <style>
        body 
        {
            min-height: 2000px;
            padding-top: 0px;
        }
    </style>
    <style type="text/css">
        .hidden
        {
            display: none;
        }

        .modalPopupError
        {
            background-color:#FFFFFF;
            width: 400px;
            border: 3px solid #A30E02;
            height: 250px;
        }
        .modalPopupError .headerError
        {
            background-color: #A30E02;
            text-decoration-color: white;
            height: 30px;
            color: white;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopupError .footerError
        {
            padding: 3px;
            align-items: center;
            align-content: center;
        }

        .modalPopupError .buttonError {
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

        .modalPopupPruebaReal
        {
            background-color:#FFFFFF;
            width: 400px;
            border: 3px solid #A30E02;
            height: 300px;
        }
        .modalPopupPruebaReal .headerPruebaReal
        {
            background-color: #A30E02;
            height: 30px;
            color: white;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopupPruebaReal .footerPruebaReal
        {
            padding: 3px;
            align-items: center;
            align-content: center;
        }
        .modalPopupPruebaReal .buttonPruebaRealOk {
            height: 25px;
            width: 100px;
            color: black;
            line-height: 23px;
            text-align: center;
            font-weight: normal;
            cursor: pointer;
            background-color: #D3CECD;
            border: 1px solid #5C5C5C;
            position:absolute;
            top: 260px;
            left: 20%;
            /*margin-left: 20%;*/
            /*transform: translateX(-50%);*/
        }

        .modalPopupPruebaReal .buttonPruebaRealCancel {
            height: 25px;
            width: 100px;
            color: black;
            line-height: 23px;
            text-align: center;
            font-weight: normal;
            cursor: pointer;
            background-color: #D3CECD;
            border: 1px solid #5C5C5C;
            position: absolute;
            top: 260px;
            left: 55%;
            /*margin-top: -100px;
            margin-left: -100px;*/
            /*margin-right: 20%;*/
            /*transform: translateX(-50%);*/
        }

        .botonClass {
            /*padding: 2px 20px;*/
            text-decoration: none;
            border: solid 1px Gray;
            background-color: #ababab;
            width: 50%;
        }
            .botonClass:hover {
                /*border: solid 1px Black;*/
                background-color: #ffffff;
            }
    </style>

</asp:Content>

 <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="lblpopup" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Display="None" Text="no mostrar" Style="            margin-left: 0px
    "></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <h2>CONFIGURACIÓN ROP</h2>
    <br />
                
    <asp:HiddenField ID="hidTAB" runat="server" Value="" />
    <ul class="nav nav-tabs nav-justified" id="nav-tab" role="tablist">
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value==""||hidTAB.Value=="0"?"active":"" %>" custom="0" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">Nivel 1</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="1"?"active":"" %>" id="nav-profile-tab" custom="1" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">Nivel 2</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="2"?"active":"" %>" id="nav-profile-tab-fijo" custom="2" data-toggle="tab" href="#nav-profile_fijo" role="tab" aria-controls="nav-profile_fijo" aria-selected="false">Nivel 3</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="3"?"active":"" %>" id="nav-profile-tab-usuario" custom="3" data-toggle="tab" href="#nav-profile_usuario" role="tab" aria-controls="nav-profile_usuario" aria-selected="false">Usuarios</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="4"?"active":"" %>" id="nav-profile-tab-historico" custom="4" data-toggle="tab" href="#nav-profile_historico" role="tab" aria-controls="nav-profile_historico" aria-selected="false">Histórico</a></li>

    </ul>
    <div class="tab-content" id="nav-tabContent" style="            border: 1px solid #d5d3d3;
            border-top: none;
            padding: 10px">
        <div class="tab-pane fade <%=hidTAB.Value==""||hidTAB.Value=="1"?"show active":"" %>" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
            <br />
            <address>
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbVersionExportar" runat="server" Width="100px" Height="25px" Font-Size="Small"></asp:DropDownList>
                        <asp:LinkButton ID="btnExcel" usesubmitbehavior="false" OnClientClick="return ponerSpinner()" CssClass="btn btn-info" runat="server" OnClick="btnExportar_Click"><span class="glyphicon glyphicon-cog">Exportar excel</span></asp:LinkButton>
                    </div> 
                    <div class="col-md-3">
                        <asp:LinkButton ID="btnAbrirExcel" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcel_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar excel</span></asp:LinkButton>
                    </div> 
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbVersionEliminar" runat="server" Width="100px" Height="25px" Font-Size="Small"></asp:DropDownList>
                        <asp:LinkButton ID="btnExcelEliminar" usesubmitbehavior="false" OnClientClick="return ponerSpinnerEliminar()" CssClass="btn btn-danger" runat="server" OnClick="btnEliminar_Click"><span class="glyphicon glyphicon-cog">Eliminar versión</span></asp:LinkButton>
                    </div> 
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbVersionPruebas" runat="server" Width="100px" Height="25px" Font-Size="Small"></asp:DropDownList>
                        <asp:LinkButton ID="btnVersionReal" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnReal_Click"><span class="glyphicon glyphicon-cog">Pasar operativo</span></asp:LinkButton>
                    </div> 
                </div> 
                <br />
                <asp:FileUpload ID="ficheroSeleccionado" runat="server" CssClass="botonClass"  accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
                <asp:RadioButton ID="rdbOperativo" runat="server" GroupName="TipoExportacion" Text="Operativo" AutoPostBack="true"/>
                &nbsp;
                <asp:RadioButton ID="rdbPrueba" runat="server" GroupName="TipoExportacion" Text="Prueba" AutoPostBack="true"/>
                &nbsp;
                <asp:LinkButton ID="btnImportarExcel" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnSubirExcel_Click"><span class="glyphicon glyphicon-cog">Importar excel</span></asp:LinkButton>
            </address>
<%--            <br />--%>
            <asp:GridView ID="grvDatos" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="250">
                        <HeaderTemplate>
                            Versión
                            <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("Version") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="F. Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125"/>
                    <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="F. Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125"/>
                    <asp:TemplateField HeaderStyle-Width="400">
                        <HeaderTemplate>
                            Actualización
                            <asp:DropDownList ID="FiltroConcepto" runat="server" Font-Size="Small" OnSelectedIndexChanged="CambioFiltroConcepto" AutoPostBack="true" AppendDataBoundItems="true">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("Actualizacion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Subgrupo" HeaderText="Grupo" HeaderStyle-Width="300"/>
                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" HeaderStyle-Width="300"/>
                    <asp:BoundField DataField="Empresa" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Subfamilia" HeaderText="Subfamilia" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Articulo" HeaderText="Artículo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="ValDesde" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="ValHasta" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="125"/>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="1"?"show active":"" %>" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
            <br />
            <address>
                <div class="row">
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbVersionGeneralExportar" runat="server" Width="100px" Height="25px" Font-Size="Small"></asp:DropDownList>
                        <asp:LinkButton ID="btnExcelGeneral" usesubmitbehavior="false" OnClientClick="return ponerSpinnerGeneral()" CssClass="btn btn-info" runat="server" OnClick="btnExportarGeneral_Click"><span class="glyphicon glyphicon-cog">Exportar excel</span></asp:LinkButton>
                    </div> 
                    <div class="col-md-3">
                        <asp:LinkButton ID="btnAbrirExcelGeneral" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcelGeneral_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar excel</span></asp:LinkButton>
                    </div> 
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbVersionGeneralEliminar" runat="server" Width="100px" Height="25px" Font-Size="Small"></asp:DropDownList>
                        <asp:LinkButton ID="btnExcelGeneralEliminar" usesubmitbehavior="false" OnClientClick="return ponerSpinnerGeneralEliminar()" CssClass="btn btn-danger" runat="server" OnClick="btnEliminarGeneral_Click"><span class="glyphicon glyphicon-cog">Eliminar versión</span></asp:LinkButton>
                    </div> 
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbVersionGeneralPruebas" runat="server" Width="100px" Height="25px" Font-Size="Small"></asp:DropDownList>
                        <asp:LinkButton ID="btnVersionGeneralReal" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnRealGeneral_Click"><span class="glyphicon glyphicon-cog">Pasar operativo</span></asp:LinkButton>
                    </div> 
                </div> 
                <br />
                <asp:FileUpload ID="ficheroSeleccionadoGeneral" runat="server" CssClass="botonClass" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
                <asp:RadioButton ID="rdbOperativoGeneral" runat="server" GroupName="TipoExportacionGeneral" Text="Operativo" AutoPostBack="true"/>
                &nbsp;
                <asp:RadioButton ID="rdbPruebaGeneral" runat="server" GroupName="TipoExportacionGeneral" Text="Prueba" AutoPostBack="true"/>
                &nbsp;
                <asp:LinkButton ID="btnImportarExcelGeneral" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnSubirExcelGeneral_Click"><span class="glyphicon glyphicon-cog">Importar excel</span></asp:LinkButton>
            </address>

            <address>
                <asp:GridView ID="grvDatosGenerales" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white">
                <Columns>
                    <asp:CheckBoxField DataField="Prueba" HeaderText="Prueba" ItemStyle-HorizontalAlign="Center" ReadOnly="True" HeaderStyle-Width="5%"/>
                    <asp:TemplateField HeaderStyle-Width="200">
                    <HeaderTemplate>
                        Versión
                        <asp:DropDownList ID="FiltroVersionGeneral" runat="server" Font-Size="Small" OnSelectedIndexChanged="CambioFiltroVersionGeneral" AutoPostBack="true" AppendDataBoundItems="true">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Versión") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125"/>
                <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125"/>
                <asp:TemplateField HeaderStyle-Width="350">
                    <HeaderTemplate>
                        Concepto
                        <asp:DropDownList ID="FiltroConceptoGeneral" runat="server" Font-Size="Small" OnSelectedIndexChanged="CambioFiltroConceptoGeneral" AutoPostBack="true" AppendDataBoundItems="true" HeaderStyle-Width="50%">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <%# Eval("Concepto") %>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="350">
                    <HeaderTemplate>
                        Empresa
                        <asp:DropDownList ID="FiltroEmpresaGeneral" runat="server" Font-Size="Small" OnSelectedIndexChanged="CambioFiltroEmpresaGeneral" AutoPostBack="true" AppendDataBoundItems="true" HeaderStyle-Width="50%">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <%# Eval("Empresa") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="15%"/>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </address>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="2"?"show active":"" %>" id="nav-profile_fijo" role="tabpanel" aria-labelledby="nav-profile_fijo-tab">
            <br />
            <address>
                <asp:LinkButton ID="btnEditarFijo" usesubmitbehavior="false" CssClass="btn btn-info" width="100px" runat="server" OnClick="btnEditarFijo_Click"><span class="glyphicon glyphicon-cog">Editar</span></asp:LinkButton>
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:LinkButton ID="btnGuardarFijo" usesubmitbehavior="false" CssClass="btn btn-info" width="100px" runat="server" OnClick="btnGuardarFijo_Click"><span class="glyphicon glyphicon-cog">Guardar</span></asp:LinkButton>
                &nbsp;
                &nbsp;
                <asp:LinkButton ID="btnCancelarFijo" usesubmitbehavior="false" CssClass="btn btn-danger" width="100px" runat="server" OnClick="btnCancelarFijo_Click"><span class="glyphicon glyphicon-cog">Cancelar</span></asp:LinkButton>
                <br />
                <br />
                <asp:Label ID="Label7" runat="server" Font-Size="Large" Height="25px" Text="Días por mes" Style="margin-left: 0px"></asp:Label>
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtDiasCalculo" runat="server" Width="100px" Height="25px" Font-Size="Medium" AutoComplete="off"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validador0" runat="server" ControlToValidate="txtDiasCalculo" ErrorMessage="* Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="Label6" runat="server" Font-Size="Large" Height="25px" Text="Configuración para el cálculo de la fecha base" Font-Bold="True" Style="margin-left: 0px"></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" Font-Size="Large" Height="25px" Text="Días entre fecha oferta y fecha capítulo" Style="margin-left: 0px"></asp:Label>
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtDiasFechaOfertaCapitulo" runat="server" Width="100px" Height="25px" Font-Size="Medium" AutoComplete="off"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validaror1" runat="server" ControlToValidate="txtDiasFechaOfertaCapitulo" ErrorMessage="* Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Size="Large" Height="25px" Text="Días a retroceder fecha creación del capítulo de la oferta" Style="margin-left: 0px"></asp:Label>
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtDiasRetrocederOferta" runat="server" Width="100px" Height="25px" Font-Size="Medium" AutoComplete="off"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validaror2" runat="server" ControlToValidate="txtDiasRetrocederOferta" ErrorMessage="* Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="Label3" runat="server" Font-Size="Large" Height="25px" Text="Días entre fecha oferta y fecha pedido" Style="margin-left: 0px"></asp:Label>
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtDiasFechaOfertaPedido" runat="server" Width="100px" Height="25px" Font-Size="Medium" AutoComplete="off"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validaror3" runat="server" ControlToValidate="txtDiasFechaOfertaPedido" ErrorMessage="* Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="Label4" runat="server" Font-Size="Large" Height="25px" Text="Días a retroceder fecha creación del pedido" Style="margin-left: 0px"></asp:Label>
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtDiasRetrocederPedido" runat="server" Width="100px" Height="25px" Font-Size="Medium" AutoComplete="off"></asp:TextBox>
                <asp:RegularExpressionValidator ID="validaror4" runat="server" ControlToValidate="txtDiasRetrocederPedido" ErrorMessage="* Ingrese Valores Numericos" ForeColor="Red" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Font-Size="Large" Height="25px" Text="Ajuste fecha Movimientos" Font-Bold="True" Style="margin-left: 0px"></asp:Label>
                <br />
                <asp:GridView ID="grvAjusteFechasMovimientos" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="ID" OnRowEditing="grvAjusteFechasMovimientos_RowEditing" OnRowCancelingEdit="grvAjusteFechasMovimientos_RowCancelingEdit" OnRowUpdating="grvAjusteFechasMovimientos_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="Tipo" HeaderText ="Tipo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="75" ReadOnly="True"/>
                        <asp:TemplateField HeaderStyle-Width="400">
                            <HeaderTemplate>
                                Movimiento
                                <asp:DropDownList ID="FiltroMovimiento" runat="server" Font-Size="Small" OnSelectedIndexChanged="CambioFiltroMovimiento" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Movimiento") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Signo" HeaderText="Signo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                        <asp:BoundField DataField="Dias" HeaderText="Días" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                        <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width ="10%"/>  
                     </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                </asp:GridView>  
            </address>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="3"?"show active":"" %>" id="nav-profile_usuario" role="tabpanel" aria-labelledby="nav-profile_usuario-tab">
            <br />
            <address>
                <asp:LinkButton ID="btnLimpiarUsuario" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarUsuario_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Label ID="lblUsuario" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Usuario red" Style="margin-left: 0px"></asp:Label>
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtUsuarioRed" runat="server" Width="100px" Height="20px" Font-Size="Medium" AutoComplete="off"></asp:TextBox>
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:checkBox ID="chkVisualizar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="Large" Height="25px" Style="margin-left: 0px" />
                    <asp:Label ID="lblVisualizar" runat="server" Font-Bold="True" Font-Size="Large" Text="Visualizar"></asp:Label>
                <asp:checkBox ID="chkExportar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="Large" Height="25px" Style="                        margin-left: 0px" />
                    <asp:Label ID="lblExportar" runat="server" Font-Bold="True" Font-Size="Large" Text="Exportar"></asp:Label>
                <asp:checkBox ID="chkImportar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="Large" Height="25px" Style="margin-left: 0px" />
                    <asp:Label ID="lblImportar" runat="server" Font-Bold="True" Font-Size="Large" Text="Importar"></asp:Label>
                <asp:checkBox ID="chkEliminar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="Large" Height="25px" Style=" margin-left: 0px " />
                    <asp:Label ID="lblEliminar" runat="server" Font-Bold="True" Font-Size="Large" Text="Eliminar"></asp:Label>
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:LinkButton ID="btnAgregarUsuario" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAgregarUsuario_Click"><span class="glyphicon glyphicon-cog">Agregar usuario</span></asp:LinkButton>
            </address>
<%--            <br />--%>
            <asp:GridView ID="grvUsuarios" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="USR_ID, USR_UsuarioRed" OnRowDeleting="grvUsuarios_RowDeleting" OnRowEditing="grvUsuarios_RowEditing" OnRowCancelingEdit="grvUsuarios_RowCancelingEdit" OnRowUpdating="grvUsuarios_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="USR_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="USR_UsuarioRed" HeaderText="Usuario" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%"/>
                    <asp:CheckBoxField DataField="USR_Visualizar" HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:CheckBoxField DataField="USR_Exportar" HeaderText="Exportar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:CheckBoxField DataField="USR_Importar" HeaderText="Importar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:CheckBoxField DataField="USR_Eliminar" HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
                    <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width ="10%"/>  
                    <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true"  ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="10%"/>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="4"?"show active":"" %>" id="nav-profile_historico" role="tabpanel" aria-labelledby="nav-profile_historico-tab">
            <br />
            <asp:RadioButton ID="rdbGFV" runat="server" GroupName="TipoHistorico" Text="GFV" AutoPostBack="true" OnCheckedChanged="rbtn_CheckedChanged" />
            &nbsp;
            <asp:RadioButton ID="rdbParametros" runat="server" GroupName="TipoHistorico" Text="Parámetros" AutoPostBack="true" OnCheckedChanged="rbtn_CheckedChanged" />
 <%--           <br />--%>
            <asp:GridView ID="grvHistorico" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white">
                <Columns>
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="115"/>
                    <asp:BoundField DataField="ID" HeaderText="ID"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50"/>
                    <asp:BoundField DataField="Versión" HeaderText="Versión" HeaderStyle-Width="150"/>
                    <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Fecha Log" DataFormatString="{0:d}" HeaderText="Fecha Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100"/>
                    <asp:BoundField DataField="Hora Log" HeaderText="Hora Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80"/>
                    <asp:BoundField DataField="Usuario Log" HeaderText="Usuario Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200"/>
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200"/>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </div>

    <ajax:ModalPopupExtender ID="mpeError" PopupControlID="PanelError"  TargetControlID="lblpopup" CancelControlID="btnCerrarError" PopupDragHandleControlID="headerdivError" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelError"  Style="Display:none;" CssClass="modalPopupError" runat="server">
        <div id ="headerdivError" class="headerError">
            <asp:Label ID="lblTituloError" runat="server" Font-Bold="true" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
        </div>
        <div id ="divdeateilsError"></div>
            <br />
            &nbsp;
            <asp:Label ID="lblMensajeError" runat="server" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
            <br />
            <br />
        <div id ="footerdivError" class="footerError">
            <asp:Button ID="btnCerrarError" runat="server" Text="Cerrar" class="buttonError" HorizontalAlign="Center"/>
        </div>
    </asp:Panel>

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

    <ajax:ModalPopupExtender ID="mpePruebaReal" PopupControlID="PanelPruebaReal"  TargetControlID="lblpopup" CancelControlID="btnCerrarPruebaReal" PopupDragHandleControlID="headerdivPruebaReal" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelPruebaReal"  Style="Display:none;" CssClass="modalPopupPruebaReal" runat="server">
        <div id ="headerdivPruebaReal" class="headerPruebaReal">
            <asp:Label ID="lblTituloPruebaReal" runat="server" Font-Bold="true" Font-Size="Large" HorizontalAlign="Center" Text="Pasar GFV prueba --> real"></asp:Label>
        </div>
        <div id ="divdeateilsPruebaReal"></div>
            &nbsp;
            <asp:Label ID="lblObservaciones" runat="server" Font-Size="Large" Text="Observaciones:"></asp:Label>
            <br />
            &nbsp;
            <asp:TextBox ID="txtObservaciones" runat="server" Height="190px" Width="375px" TextMode="MultiLine" style="text-align:justify" Font-Size="Small"></asp:TextBox>
            <br />
        <div id ="footerdivPruebaReal" class="footerPruebaReal">
            <asp:Button ID="btnOkPruebaReal" runat="server" Text="Ok" OnClick="btnOkPruebaReal_Click" class="buttonPruebaRealOk"/>
            &nbsp;
            <asp:Button ID="btnCerrarPruebaReal" runat="server" Text="Cerrar" class="buttonPruebaRealCancel"/>
        </div>
    </asp:Panel>

    <script>
        var flg = true;
        function ponerSpinner() {
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
        }

        function ponerSpinnerGeneral() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcelGeneral.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Generando');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnExcelGeneral', '');
                return true;
            }
        }

        function ponerSpinnerGeneralEliminar() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcelGeneralEliminar.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Eliminando');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnExcelGeneralEliminar', '');
                return true;
            }
        }

        function ponerSpinnerEliminar() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcelEliminar.ClientID%>");
                     btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Eliminando');//.addClass('disabled');
                     btn.prop("disabled", true);
                     flg = false;
                 }
                 catch (e) {
                     console.log("Boton buscar error script");
                 }
                 javascript: __doPostBack('ctl00$MainContent$btnExcelEliminar', '');
                 return true;
             }
        }

        $(document).ready(function () {
    
             $(".nav-tabs a").click(function (e) {                     
                 document.getElementById('<%= hidTAB.ClientID%>').value = this.attributes["custom"].value;    
             });
        });
    </script>
</asp:Content>
