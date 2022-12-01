<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="ROP_Informe.Configuracion" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.css">
    <script src="https://unpkg.com/bootstrap-table@1.17.1/dist/bootstrap-table.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style>
        body {
            min-height: 2000px;
            padding-top: 0px;
        }
    </style>
    <style type="text/css">
        .hidden {
            display: none;
        }

        .modalPopupError {
            background-color: #FFFFFF;
            width: 400px;
            border: 3px solid #A30E02;
            height: 250px;
        }

            .modalPopupError .headerError {
                background-color: #A30E02;
                text-decoration-color: white;
                height: 30px;
                color: white;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopupError .footerError {
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

        .modalPopupInformacion {
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

        .modalPopupPruebaReal {
            background-color: #FFFFFF;
            width: 400px;
            border: 3px solid #A30E02;
            height: 300px;
        }

            .modalPopupPruebaReal .headerPruebaReal {
                background-color: #A30E02;
                height: 30px;
                color: white;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopupPruebaReal .footerPruebaReal {
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
                position: absolute;
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
    <asp:Label ID="lblpopup" runat="server" Font-Bold="True" Font-Size="" Height="" Display="None" Text="no mostrar"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <h2>CONFIGURACIÓN ROP</h2>
    <br />
    <div style="border: 1px solid silver">
        <br />
        <div class="row">
            &nbsp;
            <div class="col-md-2">
                <asp:DropDownList ID="cmbVersionExportar" Font-Size="X-Small" Width="180px" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnExcel" Font-Size="X-Small" Width="90px" usesubmitbehavior="false" OnClientClick="return ponerSpinner()" CssClass="btn btn-info" runat="server" OnClick="btnExportar_Click"><span class="glyphicon glyphicon-cog">Exportar</span></asp:LinkButton>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnAbrirExcel" Font-Size="X-Small"  Width="90px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcel_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar</span></asp:LinkButton>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-2">
                <asp:DropDownList ID="cmbVersionEliminar" Font-Size="X-Small"  Width="180px" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnExcelEliminar" Font-Size="X-Small"  Width="90px" usesubmitbehavior="false" OnClientClick="return ponerSpinnerEliminar()" CssClass="btn btn-danger" runat="server" OnClick="btnEliminar_Click"><span class="glyphicon glyphicon-cog">Eliminar</span></asp:LinkButton>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="cmbVersionPruebas" Font-Size="X-Small"  Width="180px" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnVersionReal" Font-Size="X-Small"  Width="90px" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnReal_Click"><span class="glyphicon glyphicon-cog">Operativo</span></asp:LinkButton>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-6">
                <asp:FileUpload ID="ficheroSeleccionado" Font-Size="X-Small" CssClass="form-control" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
            </div>
             <div class="col-md-2">
                <asp:RadioButton ID="rdbPrueba" Font-Size="X-Small" runat="server" GroupName="TipoExportacion" Text="&nbsp; Prueba" AutoPostBack="true" style="text-align: center"/>
                &nbsp;
                <asp:RadioButton ID="rdbOperativo" Font-Size="X-Small" runat="server" GroupName="TipoExportacion" Text="&nbsp; Operativo" AutoPostBack="true" style="text-align: center"/>
            </div>
           <div class="col-md-2">
                <asp:LinkButton ID="btnImportarExcel" Font-Size="X-Small" Width="150px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnSubirExcel_Click"><span class="glyphicon glyphicon-cog">Importar excel</span></asp:LinkButton>
            </div>
        </div>
        <br />
    </div>
    <br />
    <asp:HiddenField ID="hidTAB" runat="server" Value="" />
    <ul class="nav nav-tabs nav-justified" id="nav-tab" role="tablist">
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value==""||hidTAB.Value=="0"?"active":"" %>" custom="0" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">Nivel 1</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="1"?"active":"" %>" id="nav-profile-tab" custom="1" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">Nivel 2</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="2"?"active":"" %>" id="nav-profile-tab-fijo" custom="2" data-toggle="tab" href="#nav-profile_fijo" role="tab" aria-controls="nav-profile_fijo" aria-selected="false">Nivel 3</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="3"?"active":"" %>" id="nav-profile-tab-servicio" custom="3" data-toggle="tab" href="#nav-profile_servicio" role="tab" aria-controls="nav-profile_servicio" aria-selected="false">Servicios</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="4"?"active":"" %>" id="nav-profile-tab-transporte" custom="4" data-toggle="tab" href="#nav-profile_transporte" role="tab" aria-controls="nav-profile_transporte" aria-selected="false">Transporte</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="5"?"active":"" %>" id="nav-profile-tab-panel" custom="5" data-toggle="tab" href="#nav-profile_panel" role="tab" aria-controls="nav-profile_panel" aria-selected="false">Paneles</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="6"?"active":"" %>" id="nav-profile-tab-usuario" custom="6" data-toggle="tab" href="#nav-profile_usuario" role="tab" aria-controls="nav-profile_usuario" aria-selected="false">Usuarios</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="7"?"active":"" %>" id="nav-profile-tab-bu" custom="7" data-toggle="tab" href="#nav-profile_bu" role="tab" aria-controls="nav-profile_bu" aria-selected="false">BU</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="8"?"active":"" %>" id="nav-profile-tab-historico" custom="7" data-toggle="tab" href="#nav-profile_historico" role="tab" aria-controls="nav-profile_historico" aria-selected="false">Control de cambios</a></li>
    </ul>
    <div class="tab-content" id="nav-tabContent" style="border: 1px solid #d5d3d3; border-top: none; padding: 10px">
        <div class="tab-pane fade <%=hidTAB.Value==""||hidTAB.Value=="0"?"show active":"" %>" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvDatos" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                        <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Desde" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Desde" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="45" ItemStyle-Width="45" ControlStyle-Width="45" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Hasta" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="45" ItemStyle-Width="45" ControlStyle-Width="45" />
                                <asp:TemplateField  HeaderStyle-Width="0" ItemStyle-Width="0" ControlStyle-Width="0" Visible="false">
                                    <HeaderTemplate>
                                        Actualiz.
                                        <asp:DropDownList ID="FiltroConcepto" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroConcepto" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Actualizacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Subgrupo" HeaderText="Grupo"  HeaderStyle-Width="0" ItemStyle-Width="0" ControlStyle-Width="0" Visible="false"/>
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Concepto
                                            <asp:DropDownList ID="FiltroConceptoValor" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroConceptoValor" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Concepto") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BU" HeaderText="BU" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Empresa" HeaderText="Emp." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Familia" HeaderText="Fam." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Subfamilia" HeaderText="Subf." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Articulo" HeaderText="Artículo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="ValDesde" HeaderText="Desde" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="35" ItemStyle-Width="35" ControlStyle-Width="35" />
                                <asp:BoundField DataField="ValHasta" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="35" ItemStyle-Width="35" ControlStyle-Width="35" />
                                <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Moneda" HeaderText="Mon" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="20" ItemStyle-Width="20" ControlStyle-Width="20" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="1"?"show active":"" %>" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
            <br />
 <%--           <div class="row">
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbVersionGeneralExportar" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnExcelGeneral" usesubmitbehavior="false" runat="server" OnClick="btnExportarGeneral_Click" OnClientClick="return ponerSpinnerGeneral()" CssClass="btn btn-info"><span class="glyphicon glyphicon-cog">Exportar excel</span></asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnAbrirExcelGeneral" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcelGeneral_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar excel</span></asp:LinkButton>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbVersionGeneralEliminar" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnExcelGeneralEliminar" usesubmitbehavior="false" OnClientClick="return ponerSpinnerGeneralEliminar()" CssClass="btn btn-danger" runat="server" OnClick="btnEliminarGeneral_Click"><span class="glyphicon glyphicon-cog">Eliminar versión</span></asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbVersionGeneralPruebas" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnVersionGeneralReal" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnRealGeneral_Click"><span class="glyphicon glyphicon-cog">Pasar operativo</span></asp:LinkButton>
                </div>
            </div>
            <hr />--%>
     <%--       <div class="row">
                <div class="col-md-6">
                    <asp:FileUpload ID="ficheroSeleccionadoGeneral" runat="server" CssClass="form-control" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
                </div>
                <div class="col-md-3">
                    <asp:RadioButton ID="rdbOperativoGeneral" runat="server" GroupName="TipoExportacionGeneral" Text="Operativo" AutoPostBack="true" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdbPruebaGeneral" runat="server" GroupName="TipoExportacionGeneral" Text="Prueba" AutoPostBack="true" />
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="btnImportarExcelGeneral" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnSubirExcelGeneral_Click"><span class="glyphicon glyphicon-cog">Importar excel</span></asp:LinkButton>
                </div>
            </div>
            <hr />--%>

            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvDatosGenerales" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                                <asp:CheckBoxField DataField="Prueba" HeaderText="Prueba" ItemStyle-HorizontalAlign="Center" ReadOnly="True" HeaderStyle-Width="100" Visible="false"/>
                                <asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                            <asp:DropDownList ID="FiltroVersionGeneral" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersionGeneral" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Versión") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:TemplateField HeaderStyle-Width="500">
                                    <HeaderTemplate>
                                        Concepto
                                            <asp:DropDownList ID="FiltroConceptoGeneral" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroConceptoGeneral" AutoPostBack="true" AppendDataBoundItems="true" HeaderStyle-Width="70%">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Concepto") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                        <asp:DropDownList ID="FiltroBUGeneral" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroBUGeneral" AutoPostBack="true" AppendDataBoundItems="true" HeaderStyle-Width="30%">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("BU") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="250">
                                    <HeaderTemplate>
                                        Empresa
                                        <asp:DropDownList ID="FiltroEmpresaGeneral" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaGeneral" AutoPostBack="true" AppendDataBoundItems="true" HeaderStyle-Width="50%">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Empresa") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="15%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="2"?"show active":"" %>" id="nav-profile_fijo" role="tabpanel" aria-labelledby="nav-profile_fijo-tab">
            <h6>Ajuste fecha Movimientos</h6>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjusteFechasMovimientos" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" DataKeyNames="ID" OnRowEditing="grvAjusteFechasMovimientos_RowEditing" OnRowCancelingEdit="grvAjusteFechasMovimientos_RowCancelingEdit" OnRowUpdating="grvAjusteFechasMovimientos_RowUpdating" Font-Size="Small">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                            <asp:DropDownList ID="FiltroVersionMovimientosGeneral" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersionMovimientosGeneral" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Versión") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="75" ReadOnly="True" />
                                <asp:TemplateField HeaderStyle-Width="400">
                                    <HeaderTemplate>
                                        Movimiento
                                    <asp:DropDownList ID="FiltroMovimiento" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroMovimiento" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Movimiento") %>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Signo" HeaderText="Signo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Dias" HeaderText="Días" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <%--<asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10%" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="3"?"show active":"" %>" id="nav-profile_servicio" role="tabpanel" aria-labelledby="nav-profile_servicio-tab">
            <br />
            <div class="row p0">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnLimpiarServicio" CauseValidation="false" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarServicio_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                </div>
                <div class="col-md-1" style="text-align: right">
                    <span>Familia</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtFamilia" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <span>Subfamilia</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtSubfamilia" runat="server" CssClass="form-control"></asp:TextBox>&nbsp;
                </div>
                <div class="col-md-1">
                    <span>Artículo</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtArticulo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <span>Tipo</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbTipo" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnAgregarServicio" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAgregarServicio_Click"><span class="glyphicon glyphicon-cog">Agregar</span></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvServicios" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" DataKeyNames="CFGSERV_ID" OnRowDeleting="grvServicios_RowDeleting" OnRowEditing="grvServicios_RowEditing" OnRowCancelingEdit="grvServicios_RowCancelingEdit" OnRowUpdating="grvServicios_RowUpdating" OnRowDataBound="grvServicios_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGSERV_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                                <asp:BoundField DataField="Subfamilia" HeaderText="Subfamilia" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                                <asp:BoundField DataField="ART_ID" HeaderText="Artículo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                                <asp:TemplateField HeaderText="Tipo servicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoServicio" runat="server" Text='<%# Eval("CFGSERV_Tipo") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbTipoServicio" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10%" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="10%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="4"?"show active":"" %>" id="nav-profile_transporte" role="tabpanel" aria-labelledby="nav-profile_transporte-tab">
            <br />
<%--             <div class="row">            
            <div class="col-md-8"></div>
            <div class="col-md-1">
                <asp:Label ID="lbltiempo" runat="server" Text="Tiempo: --:--" Style="margin-left: 0px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btnActualizarTransportPRUEBA" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnActualizarTransporteGeneralPRUEBA_Click"><span class="glyphicon glyphicon-cog">PRUEBA</span></asp:LinkButton>
            </div>
        </div>--%>
            <div class="row">
                <div class="col-md-3">
                    <asp:LinkButton ID="btnEditarTransporteGeneral" usesubmitbehavior="false" CssClass="btn btn-info" Width="80px" runat="server" OnClick="btnEditarTransporteGeneral_Click"><span class="glyphicon glyphicon-cog">Editar</span></asp:LinkButton>
                    <asp:LinkButton ID="btnGuardarTransporteGeneral" usesubmitbehavior="false" CssClass="btn btn-info" Width="80px" runat="server" OnClick="btnGuardarTransporteGeneral_Click"><span class="glyphicon glyphicon-cog">Guardar</span></asp:LinkButton>
                     &nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton ID="btnCancelarTransporteGeneral" usesubmitbehavior="false" CssClass="btn btn-danger" Width="80px" runat="server" OnClick="btnCancelarTransporteGeneral_Click"><span class="glyphicon glyphicon-cog">Cancelar</span></asp:LinkButton>
                </div>
                 <div class="col-md-1">
                    <span>Meses</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtMeses" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RegularExpressionValidator ID="validador5" runat="server" ControlToValidate="txtMeses" ErrorMessage="* Ingrese valores enteros" ForeColor="Red" style="font-size:x-small" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-1">
                    <span>%Desvío</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtDesvioTransporte" runat="server" AutoPostBack="true" DataFormatString="{0:N2}" CssClass="form-control" AutoComplete="off" OnTextChanged="txtDesvioTransporte_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RegularExpressionValidator ID="validador6" runat="server" ControlToValidate="txtDesvioTransporte" ErrorMessage="* Ingrese valores decimales" style="font-size:x-small" ForeColor="Red" ValidationExpression="[0-9]*\,?[0-9]*"></asp:RegularExpressionValidator>
                </div><div class="col-md-1 col-center">
                    <asp:LinkButton ID="btnctualizarTransporte" usesubmitbehavior="false" CssClass="btn btn-dark" Width="90px" runat="server" style="font-size:x-small" OnClick="btnActualizarTransporteGeneral_Click"><span class="glyphicon glyphicon-cog">Actualizar</span></asp:LinkButton>
                </div>
                 <div class="col-md-1 col-center">
                    <asp:TextBox ID="txtFechaActualizar" runat="server" CssClass="form-control" AutoComplete="off" Enabled="false" DataFormatString="{0:dd-MM-yyyy}" style="font-size:x-small" Width="80px" ForeColor="#cc0000"></asp:TextBox>
                </div>
            </div>
            <hr />

            <div class="row">
                <div class="col-md-2">
                    <asp:LinkButton ID="btnLimpiarTransporteSubfamilias" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarTransporteSubfamilias_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnAgregarTransporteSubfamilias" usesubmitbehavior="false" CssClass="btn btn-success"  Width="100px" runat="server" OnClick="btnAgregarTransporteSubfamilias_Click"><span class="glyphicon glyphicon-cog">Agregar</span></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                 <div class="col-md-1">
                    <span>BU</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbBUCodigos" runat="server" AutoPostBack = true CssClass="form-control" OnSelectedIndexChanged="cmbBUCodigos_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <div class="col-md-1">
                    <span>Empresa</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbEmpresaCodigos" runat="server" AutoPostBack = true CssClass="form-control" OnSelectedIndexChanged="cmbEmpresaCodigos_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <span>Delegacion</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbDelegacionCodigos" runat="server" AutoPostBack = true usesubmitbehavior="false" CssClass="form-control"></asp:DropDownList>&nbsp;
                </div>
                 <div class="col-md-1">
                    <span>Subfamilia</span>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSubfamillia" runat="server" CssClass="form-control" AutoComplete="off" Enabled="true" style="font-size:x-small" Width="80px" ForeColor="#cc0000"></asp:TextBox>
                </div>
             </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView CssClass="table table-hover" ID="grvTransporteSubfamilias" Font-Size="Small" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGTCO_ID" OnRowDeleting="grvTransporteSubfamilias_RowDeleting" OnRowEditing="grvTransporteSubfamilias_RowEditing" OnRowCancelingEdit="grvTransporteSubfamilias_RowCancelingEdit" OnRowUpdating="grvTransporteSubfamilias_RowUpdating" OnRowDataBound="grvTransporteSubfamilias_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="CFGTCO_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField HeaderText="BU">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBU" runat="server" Text='<%# Eval("BU") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbBU" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("Empresa") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbEmpresa" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Delegación">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelegacion" runat="server" Text='<%# Eval("Delegacion") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDelegacion" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Subfamilia" HeaderText="Subfamilia" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10%" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="10%" />
                            </Columns>
                             <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <hr />

            <div class="row">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnLimpiarTransporte" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarTransporte_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                </div>
                <div class="col-md-1"></div>
                <div class="col-md-1">
                    <span>Empresa</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbEmpresa" runat="server" AutoPostBack = true CssClass="form-control" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <span>Delegacion</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbDelegacion" runat="server" AutoPostBack = true usesubmitbehavior="false" CssClass="form-control"></asp:DropDownList>&nbsp;
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <span>Desde</span>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDesde" type="date" runat="server" CssClass="form-control" DataFormatString="{0:dd-MM-yyyy}"></asp:TextBox>
                </div>

                <div class="col-md-1">
                    <span>Hasta</span>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtHasta" type="date" runat="server" CssClass="form-control" DataFormatString="{0:dd-MM-yyyy}"></asp:TextBox>&nbsp;
                </div>
                <div class="col-md-1">
                    <span>%Margen</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtMargen" runat="server" AutoPostBack="true"  DataFormatString="{0:N2}" CssClass="form-control" AutoComplete="off" OnTextChanged="txtMargen_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <span>Distancia</span>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cmbDistancia" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnPropuesta" usesubmitbehavior="false" CssClass="btn btn-success" Width="60px" runat="server" OnClick="btnPropuesta_Click"><span class="glyphicon glyphicon-cog">Prop.</span></asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPropuesto" runat="server" DataFormatString="{0:n6}" CssClass="form-control"></asp:TextBox>&nbsp;
                </div>
                <div class="col-md-1">
                    <span>Valor</span>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtValor" runat="server" AutoPostBack="true" DataFormatString="{0:n6}" CssClass="form-control" AutoComplete="off" OnTextChanged="txtValor_TextChanged"></asp:TextBox>&nbsp;
                </div>
                <%--<div class="col-md-1">
                    <span>Desvío</span>
                </div>--%>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnAgregarTransporte" usesubmitbehavior="false" CssClass="btn btn-success"  Width="100px" runat="server" OnClick="btnAgregarTransporte_Click"><span class="glyphicon glyphicon-cog">Agregar</span></asp:LinkButton>
                    <asp:TextBox ID="txtDesvio" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                </div>
            </div>
            <hr />
            <div class="col-md-6">
                <asp:CheckBox ID="chkBoxActivos" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" OnCheckedChanged="chkBoxActivos_CheckedChanged" />
                <asp:Label ID="Label1" runat="server" Text="Visualizar sólo activos"></asp:Label>
            </div> 
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView CssClass="table table-hover" ID="grvTransporte" Font-Size="Small" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGTRA_ID" OnRowDeleting="grvTransporte_RowDeleting" OnRowEditing="grvTransporte_RowEditing" OnRowCancelingEdit="grvTransporte_RowCancelingEdit" OnRowUpdating="grvTransporte_RowUpdating" OnRowDataBound="grvTransporte_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="CFGTRA_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                        <asp:Label ID="lblBU" runat="server" Text='<%# Eval("BU") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbBU" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                        <asp:DropDownList ID="FiltroEmpresaTransporte" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaTransporte" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                        <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("Empresa") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbEmpresa" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                        <asp:DropDownList ID="FiltroDelegacionTransporte" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionTransporte" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                        <asp:Label ID="lblDelegacion" runat="server" Text='<%# Eval("Delegacion") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDelegacion" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Margen" HeaderText="Margen" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" ApplyFormatInEditMode="true" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:TemplateField  HeaderStyle-Width="120" ItemStyle-Width="120" ControlStyle-Width="120">
                                    <HeaderTemplate>
                                        Desde
                                        <asp:DropDownList ID="FiltroDesdeTransporte" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDesdeTransporte" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                     <EditItemTemplate>
                                         <asp:TextBox ID="txtDesde" runat="server" Text='<%# Bind("Desde","{0:yyyy-MM-dd}") %>' TextMode="Date" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label  ID="lblDesde" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("Desde").ToString())) ? "" : Eval("Desde", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hasta" HeaderStyle-Width="120" ItemStyle-Width="120" ControlStyle-Width="120">
                                    <EditItemTemplate>
                                         <asp:TextBox ID="txtHasta" runat="server" Text='<%# Bind("Hasta","{0:yyyy-MM-dd}") %>' TextMode="Date" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label  ID="lblHasta" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("Hasta").ToString())) ? "" : Eval("Hasta", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="65" ItemStyle-Width="70" ControlStyle-Width="70">
                                    <HeaderTemplate>
                                        Distancia
                                        <asp:DropDownList ID="FiltroDistanciaTransporte" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDistanciaTransporte" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                        <asp:Label ID="lblDistancia" runat="server" Text='<%# Eval("Distancia") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDistancia" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Prop" HeaderText="Prop" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n6}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="Valor" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n6}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="Desvio" HeaderText="Desvío" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:P2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="5" />
                            </Columns>
                             <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="5"?"show active":"" %>" id="nav-profile_panel" role="tabpanel" aria-labelledby="nav-profile_panel-tab">
            <br />
            <div class="row">
                <div class="col-md-1">
                    <asp:Label ID="lblPanel" runat="server" Text="Panel" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-3">
                  <asp:TextBox ID="txtPanel" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
                </div>
                <%--<div class="col-md-9">
                    <asp:DropDownList ID="cmbPaneles" runat="server" CssClass="form-control js-example-placeholder-single" AutoPostBack="true" ForeColor="#006699"></asp:DropDownList>
                </div>--%>
                <div class="col-md-1">
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnIncluirPanel" usesubmitbehavior="false" CssClass="btn btn-success" Width="150px" runat="server" OnClick="btnIncluirPanel_Click"><span class="glyphicon glyphicon-cog">Incluir panel</span></asp:LinkButton>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvPaneles" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="IDAsset" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                       <%-- <asp:GridView ID="grvPaneles" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="IDAsset" OnRowDataBound="grvPaneles_RowDataBound">--%>
                            <Columns>
                               <%-- <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ItemId" HeaderText="Item ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />--%>
                                <asp:BoundField DataField="IDAsset" HeaderText="AAF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ItemIdAsset" HeaderText="AAF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="ItemDescripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="60%" />
                                <asp:CheckBoxField DataField="Estandar" HeaderText="¿Estándar?" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="6"?"show active":"" %>" id="nav-profile_usuario" role="tabpanel" aria-labelledby="nav-profile_usuario-tab">
            <br />
            <div class="row">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnLimpiarUsuario" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarUsuario_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblUsuario" Text="Usuario red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtUsuarioRed" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <asp:CheckBox ID="chkVisualizar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblVisualizar" runat="server" Text="Visualizar"></asp:Label>
                    <asp:CheckBox ID="chkExportar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblExportar" runat="server" Text="Exportar"></asp:Label>
                    <asp:CheckBox ID="chkImportar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblImportar" runat="server" Text="Importar"></asp:Label>
                    <asp:CheckBox ID="chkEliminar" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblEliminar" runat="server" Text="Eliminar"></asp:Label>
                    <asp:CheckBox ID="chkElegirVersion" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblElegirVersion" runat="server" Text="Elegir versión"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:LinkButton ID="btnAgregarUsuario" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAgregarUsuario_Click"><span class="glyphicon glyphicon-cog">Agregar usuario</span></asp:LinkButton>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvUsuarios" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="USR_ID, USR_UsuarioRed" OnRowDeleting="grvUsuarios_RowDeleting" OnRowEditing="grvUsuarios_RowEditing" OnRowCancelingEdit="grvUsuarios_RowCancelingEdit" OnRowUpdating="grvUsuarios_RowUpdating" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="USR_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="USR_UsuarioRed" HeaderText="Usuario" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" />
                                <asp:CheckBoxField DataField="USR_Visualizar" HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                                <asp:CheckBoxField DataField="USR_Exportar" HeaderText="Exportar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                                <asp:CheckBoxField DataField="USR_Importar" HeaderText="Importar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                                <asp:CheckBoxField DataField="USR_Eliminar" HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                                <asp:CheckBoxField DataField="USR_ElegirVersion" HeaderText="Elegir versión" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10%" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="10%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

      <div class="tab-pane fade <%=hidTAB.Value=="7"?"show active":"" %>" id="nav-profile_bu" role="tabpanel" aria-labelledby="nav-profile_bu-tab">
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvBU" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                                <asp:CheckBoxField DataField="Prueba" HeaderText="Prueba" ItemStyle-HorizontalAlign="Center" ReadOnly="True" HeaderStyle-Width="100" Visible="false"/>
                                <asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                            <asp:DropDownList ID="FiltroVersionBU" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersionBU" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:BoundField DataField="BU"  HeaderText="BU" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                                <asp:BoundField DataField="Empresa"  HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>


        <div class="tab-pane fade <%=hidTAB.Value=="8"?"show active":"" %>" id="nav-profile_historico" role="tabpanel" aria-labelledby="nav-profile_historico-tab">
            <br />
            <%--<asp:RadioButton ID="rdbGFV" runat="server" GroupName="TipoHistorico" Text="GFV" AutoPostBack="true" OnCheckedChanged="rbtn_CheckedChanged" />
            &nbsp;
            <asp:RadioButton ID="rdbParametros" runat="server" GroupName="TipoHistorico" Text="Parámetros" AutoPostBack="true" OnCheckedChanged="rbtn_CheckedChanged" />--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvHistorico" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                                <%--<asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="115" />--%>
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" />
                                <asp:BoundField DataField="Versión" HeaderText="Versión" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Fecha Log" DataFormatString="{0:d}" HeaderText="Fecha Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Hora Log" HeaderText="Hora Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                <asp:BoundField DataField="Usuario Log" HeaderText="Usuario Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200" />
                                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="315" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            <%--<FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <ajax:ModalPopupExtender ID="mpeError" PopupControlID="PanelError" TargetControlID="lblpopup" CancelControlID="btnCerrarError" PopupDragHandleControlID="headerdivError" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelError" Style="display: none;" CssClass="modalPopupError" runat="server">
        <div id="headerdivError" class="headerError">
            <asp:Label ID="lblTituloError" runat="server" Font-Bold="true" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
        </div>
        <div id="divdeateilsError"></div>
        <br />
        &nbsp;
            <asp:Label ID="lblMensajeError" runat="server" Font-Size="Large" HorizontalAlign="Center" Text=""></asp:Label>
        <br />
        <br />
        <div id="footerdivError" class="footerError">
            <asp:Button ID="btnCerrarError" runat="server" Text="Cerrar" class="buttonError" HorizontalAlign="Center" />
        </div>
    </asp:Panel>

    <ajax:ModalPopupExtender ID="mpeInformacion" PopupControlID="PanelInformacion" TargetControlID="lblpopup" CancelControlID="btnCerrarInformacion" PopupDragHandleControlID="headerdivInformacion" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelInformacion" Style="display: none;" CssClass="modalPopupInformacion" runat="server">
        <div id="headerdivInformacion" class="headerInformacion">
            <asp:Label ID="lblTituloInformacion" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Center" Text=""></asp:Label>
        </div>
        <div id="divdeateilsInformacion"></div>
        <br />
        &nbsp;
            <asp:Label ID="lblMensajeInformacion" runat="server" Font-Size="Small" HorizontalAlign="Center" Text=""></asp:Label>
        <br />
        <br />
        <div id="footerdivInformacion" class="footerInformacion">
            <asp:Button ID="btnCerrarInformacion" runat="server" Font-Size="Small" Text="Cerrar" class="buttonInformacion" HorizontalAlign="Center" />
        </div>
    </asp:Panel>

    <ajax:ModalPopupExtender ID="mpePruebaReal" PopupControlID="PanelPruebaReal" TargetControlID="lblpopup" CancelControlID="btnCerrarPruebaReal" PopupDragHandleControlID="headerdivPruebaReal" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelPruebaReal" Style="display: none;" CssClass="modalPopupPruebaReal" runat="server">
        <div id="headerdivPruebaReal" class="headerPruebaReal">
            <asp:Label ID="lblTituloPruebaReal" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Center" Text="Pasar GFV prueba --> real"></asp:Label>
        </div>
        <div id="divdeateilsPruebaReal"></div>
        &nbsp;
            <asp:Label ID="lblObservaciones" runat="server" Font-Size="Small" Text="Observaciones:"></asp:Label>
        <br />
        &nbsp;
            <asp:TextBox ID="txtObservaciones" runat="server" Font-Size="Small" Height="190px" Width="375px" TextMode="MultiLine" Style="text-align: justify"></asp:TextBox>
        <br />
        <div id="footerdivPruebaReal" class="footerPruebaReal">
            <asp:Button ID="btnOkPruebaReal" runat="server" Font-Size="Small" Text="Ok" OnClick="btnOkPruebaReal_Click" class="buttonPruebaRealOk" />
            &nbsp;
            <asp:Button ID="btnCerrarPruebaReal" runat="server" Font-Size="Small" Text="Cerrar" class="buttonPruebaRealCancel" />
        </div>
    </asp:Panel>

    <script>
        var flg = true;
        function ponerSpinner() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcel.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Generar');//.addClass('disabled');
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

      <%--  function ponerSpinnerGeneral() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcelGeneral.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Generar');//.addClass('disabled');
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
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Eliminar');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnExcelGeneralEliminar', '');
                return true;
            }
        }--%>

        function ponerSpinnerEliminar() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcelEliminar.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Eliminar');//.addClass('disabled');
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
