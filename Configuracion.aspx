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
        .derecha { float:right }

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

        .modalPopupDecidir {
            background-color: #FFFFFF;
            width: 320px;
            border: 3px solid #A30E02;
            height: 200px;
        }

            .modalPopupDecidir .headerDecidir {
                background-color: #A30E02;
                height: 30px;
                color: white;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopupDecidir .footerDecidir {
                padding: 3px;
                align-items: center;
                align-content: center;
            }

            .modalPopupDecidir .buttonDecidirOk {
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
                top: 160px;
                left: 20%;
                /*margin-left: 20%;*/
                /*transform: translateX(-50%);*/
            }

            .modalPopupDecidir .buttonDecidirCancel {
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
                top: 160px;
                left: 55%;
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
            <div class="col-md-2"></div>
            <div class="col-md-2">
                <asp:DropDownList ID="cmbVersionEliminar" Font-Size="X-Small"  Width="180px" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnExcelEliminar" Font-Size="X-Small"  Width="90px" usesubmitbehavior="false" OnClientClick="return ponerSpinnerEliminar()" CssClass="btn btn-danger" runat="server" OnClick="btnEliminar_Click"><span class="glyphicon glyphicon-cog">Eliminar</span></asp:LinkButton>
            </div>
<%--            <div class="col-md-2">
                <asp:DropDownList ID="cmbVersionPruebas" Font-Size="X-Small"  Width="180px" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btnVersionReal" Font-Size="X-Small"  Width="90px" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnReal_Click"><span class="glyphicon glyphicon-cog">Operativo</span></asp:LinkButton>
            </div>--%>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-6">
                <asp:FileUpload ID="ficheroSeleccionado" Font-Size="X-Small" CssClass="form-control" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
            </div>
             <%--<div class="col-md-2">
                <asp:RadioButton ID="rdbPrueba" Font-Size="X-Small" runat="server" GroupName="TipoExportacion" Text="&nbsp; Prueba" AutoPostBack="true" style="text-align: center"/>
                &nbsp;
                <asp:RadioButton ID="rdbOperativo" Font-Size="X-Small" runat="server" GroupName="TipoExportacion" Text="&nbsp; Operativo" AutoPostBack="true" style="text-align: center"/>
            </div>--%>
           <div class="col-md-2">
                <asp:LinkButton ID="btnImportarExcel" Font-Size="X-Small" Width="150px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnSubirExcel_Click"><span class="glyphicon glyphicon-cog">Importar excel</span></asp:LinkButton>
            </div>
        </div>
        <br />
        <div ID="errores" runat="server" class="row">
             <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-10">
                   <asp:GridView ID="grdErrores" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="Small">
                        <Columns>
                            <asp:BoundField DataField="Pestaña" HeaderText="Pestaña" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200" ItemStyle-Width="200" ControlStyle-Width="200" />
                            <asp:BoundField DataField="Error" HeaderText="Error" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="1000" ItemStyle-Width="1000" ControlStyle-Width="1000" />
                        </Columns>
                        <HeaderStyle BackColor="#570202" ForeColor="white" />
                    </asp:GridView>
                </div>
                <div class="col-md-1">
                </div>
            </div>
        </div>
        <br />
    </div>
    <br />
    <asp:HiddenField ID="hidTAB" runat="server" Value="" />
    <ul class="nav nav-tabs nav-justified" id="nav-tab" role="tablist">
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value==""||hidTAB.Value=="0"?"active":"" %>" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">Nivel 1</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="1"?"active":"" %>" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">Nivel 2</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="2"?"active":"" %>" id="nav-profile-tab-fijo" data-toggle="tab" href="#nav-profile_fijo" role="tab" aria-controls="nav-profile_fijo" aria-selected="false">Nivel 3</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="3"?"active":"" %>" id="nav-profile-tab-taximetro" data-toggle="tab" href="#nav-profile_taximetro" role="tab" aria-controls="nav-profile_taximetro" aria-selected="false">Taxímetros</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="4"?"active":"" %>" id="nav-profile-tab-servicio" data-toggle="tab" href="#nav-profile_servicio" role="tab" aria-controls="nav-profile_servicio" aria-selected="false">Servicios</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="5"?"active":"" %>" id="nav-profile-tab-transporte" data-toggle="tab" href="#nav-profile_transporte" role="tab" aria-controls="nav-profile_transporte" aria-selected="false">Transporte</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="6"?"active":"" %>" id="nav-profile-tab-panel" data-toggle="tab" href="#nav-profile_panel" role="tab" aria-controls="nav-profile_panel" aria-selected="false">Paneles</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="7"?"active":"" %>" id="nav-profile-tab-ajustes" data-toggle="tab" href="#nav-profile_ajustes" role="tab" aria-controls="nav-profile_ajustes" aria-selected="false">Ajustes</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="8"?"active":"" %>" id="nav-profile-tab-bu" data-toggle="tab" href="#nav-profile_bu" role="tab" aria-controls="nav-profile_bu" aria-selected="false">BU</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="9"?"active":"" %>" id="nav-profile-tab-historico" data-toggle="tab" href="#nav-profile_historico" role="tab" aria-controls="nav-profile_historico" aria-selected="false">Ctr. Cambios</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="10"?"active":"" %>" id="nav-profile-tab-usuario" data-toggle="tab" href="#nav-profile_usuario" role="tab" aria-controls="nav-profile_usuario" aria-selected="false">Usuarios</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="11"?"active":"" %>" id="nav-profile-tab-abonos" data-toggle="tab" href="#nav-profile_abonos" role="tab" aria-controls="nav-profile_abonos" aria-selected="false" <%=validarUsuarioAbono?"":"style='display:none'" %> >Aj. DG</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="12"?"active":"" %>" id="nav-profile-tab-tecnico" data-toggle="tab" href="#nav-profile_tecnico" role="tab" aria-controls="nav-profile_tecnico" aria-selected="false" <%=validarUsuarioTecnico?"":"style='display:none'" %> >Técnico</a></li>
        <li><a class="nav-item nav-justified nav-link <%=hidTAB.Value=="13"?"active":"" %>" id="nav-profile-tab-tablas" data-toggle="tab" href="#nav-profile_tablas" role="tab" aria-controls="nav-profile_tablas" aria-selected="false" <%=validarUsuarioTecnico?"":"style='display:none'" %> >Tablas (copia)</a></li>
    </ul>
    <div class="tab-content" id="nav-tabContent" style="border: 1px solid #d5d3d3; border-top: none; padding: 10px">
        <div class="tab-pane fade <%=hidTAB.Value==""||hidTAB.Value=="0"?"show active":"" %>" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvDatos" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="X-Small">
                            <Columns>
                                <asp:TemplateField  HeaderStyle-Width="35" ItemStyle-Width="35" ControlStyle-Width="35">
                                    <HeaderTemplate>
                                        Versión
                                        <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="XX-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Desde" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Desde" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Hasta" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:TemplateField  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        Concepto
                                            <asp:DropDownList ID="FiltroConceptoValor" runat="server" Font-Size="XX-Small" OnSelectedIndexChanged="CambioFiltroConceptoValor" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Concepto") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BU" HeaderText="BU" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Empresa" HeaderText="Emp." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Familia" HeaderText="Fam." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Subfamilia" HeaderText="Subf." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Articulo" HeaderText="Artículo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Valorar" HeaderText="Valorar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="ValDesde" HeaderText="Desde" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="ValHasta" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Moneda" HeaderText="Mon" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="20" ItemStyle-Width="20" ControlStyle-Width="20" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="1"?"show active":"" %>" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
            <br />
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
                                <asp:TemplateField HeaderStyle-Width="250">
                                    <HeaderTemplate>
                                        Delegación
                                        <asp:DropDownList ID="FiltroDelegacionGeneral" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionGeneral" AutoPostBack="true" AppendDataBoundItems="true" HeaderStyle-Width="50%">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Delegacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="15%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
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

        <div class="tab-pane fade <%=hidTAB.Value=="3"?"show active":"" %>" id="nav-profile_taximetro" role="tabpanel" aria-labelledby="nav-profile_taximetro-tab">
             <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvTaximetros" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" Font-Size="Small">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="20">
                                    <HeaderTemplate>
                                        Versión
                                            <asp:DropDownList ID="FiltroVersionTaximetros" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersionTaximetros" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FechaDesde" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Desde" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="FechaHasta" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Hasta" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                               <asp:TemplateField  HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        Concepto
                                            <asp:DropDownList ID="FiltroConceptoValorTaximetros" runat="server" Font-Size="XX-Small" OnSelectedIndexChanged="CambioFiltroConceptoValorTaximetros" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Concepto") %>
                                    </ItemTemplate>
                                </asp:TemplateField> <asp:BoundField DataField="BU" HeaderText="BU" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Empresa" HeaderText="Emp." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Familia" HeaderText="Fam." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Subfamilia" HeaderText="Subf." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="AAF" HeaderText="AAF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30" />
                                <asp:BoundField DataField="Valorar" HeaderText="Valorar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Desde" HeaderText="Desde" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Hasta" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                                <asp:BoundField DataField="Valor" DataFormatString="{0:n2}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25" />
                               <%-- <asp:BoundField DataField="Moneda" HeaderText="Mon" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="20" ItemStyle-Width="20" ControlStyle-Width="20" />
                      --%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="4"?"show active":"" %>" id="nav-profile_servicio" role="tabpanel" aria-labelledby="nav-profile_servicio-tab">
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvServicios" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" DataKeyNames="CFGSERV_ID" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGSERV_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                        <asp:DropDownList ID="FiltroVersionServicios" runat="server" Font-Size="XX-Small" OnSelectedIndexChanged="CambioFiltroVersionServicios" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                                <asp:BoundField DataField="Subfamilia" HeaderText="Subfamilia" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                                <asp:BoundField DataField="ART_ID" HeaderText="Artículo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                                <asp:BoundField DataField="CFGSERV_Tipo" HeaderText="Tipo servicio" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="25%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade <%=hidTAB.Value=="5"?"show active":"" %>" id="nav-profile_transporte" role="tabpanel" aria-labelledby="nav-profile_transporte-tab">
            <br />
            <div class="row">
                <div class="col-md-2">
                    <span>Meses</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtMeses" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <span class="derecha">% Desvío</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtDesvioTransporte" runat="server" AutoPostBack="true" DataFormatString="{0:N2}" CssClass="form-control" AutoComplete="off" OnTextChanged="txtDesvioTransporte_TextChanged"></asp:TextBox>
                </div>
                 <div class="col-md-2" >
                    <span class="derecha">Coef. Vuelta</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtCoeficienteVuelta" runat="server" AutoPostBack="true" DataFormatString="{0:N2}" CssClass="form-control" AutoComplete="off" OnTextChanged="txtCoeficienteVuelta_TextChanged"></asp:TextBox>
                </div>
                 <div class="col-md-2" >
                    <span class="derecha">Kms mínimos</span>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="txtKilometrosMinimos" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" OnTextChanged="txtKilometrosMinimos_TextChanged"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6 col-center">
                </div>
                <div class="col-md-3 col-center">
                    <asp:LinkButton ID="btnctualizarTransporte" usesubmitbehavior="false" CssClass="btn btn-dark" Width="90px" runat="server" style="font-size:small" OnClick="btnActualizarTransporteGeneral_Click"><span class="glyphicon glyphicon-cog">Actualizar</span></asp:LinkButton>
                </div>
                <div class="col-md-1 col-center">
                </div>
                <div class="col-md-2 col-center">
                    <asp:TextBox ID="txtFechaActualizar" runat="server" CssClass="form-control" AutoComplete="off" Enabled="false" DataFormatString="{0:dd-MM-yyyy}" style="font-size:small" Width="160px" ForeColor="#cc0000"></asp:TextBox>
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
                        <asp:GridView CssClass="table table-hover" ID="grvTransporte" Font-Size="X-Small" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGTRA_ID">
                            <Columns>
                                <asp:BoundField DataField="CFGTRA_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Version" HeaderText="Version" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" />
                                <%--<asp:TemplateField HeaderStyle-Width="25" ItemStyle-Width="25" ControlStyle-Width="25">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("BU") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderStyle-Width="35" ItemStyle-Width="35" ControlStyle-Width="35">
                                    <HeaderTemplate>
                                        Empresa
                                        <asp:DropDownList ID="FiltroEmpresaTransporte" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaTransporte" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                        <%# Eval("Empresa") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50">
                                    <HeaderTemplate>
                                        Delegación
                                        <asp:DropDownList ID="FiltroDelegacionTransporte" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionTransporte" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("Delegacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="Margen" HeaderText="Margen" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" ApplyFormatInEditMode="true" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:TemplateField  HeaderStyle-Width="80" ItemStyle-Width="80" ControlStyle-Width="80">
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
                                <asp:TemplateField HeaderText="Hasta" HeaderStyle-Width="80" ItemStyle-Width="80" ControlStyle-Width="80">
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
                                         <%# Eval("Distancia") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Prop" HeaderText="Prop" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n6}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="Valor" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n6}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="Desvio" HeaderText="Desvío" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:P2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="50"  ItemStyle-Width="50" ControlStyle-Width="50"/>
                                <asp:BoundField DataField="MonedaEmpresa" HeaderText="Mon" ItemStyle-HorizontalAlign="Center"  ApplyFormatInEditMode="true" HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40" />
                                <asp:BoundField DataField="Vales" HeaderText="Vales" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="Prop12Meses" HeaderText="Prop 12M" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n6}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="Desvio12Meses" HeaderText="Desvío 12M" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:P2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="50"  ItemStyle-Width="50" ControlStyle-Width="50"/>
                                <asp:BoundField DataField="Vales12Meses" HeaderText="Vales 12M" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                </Columns>
                             <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                         </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="6"?"show active":"" %>" id="nav-profile_panel" role="tabpanel" aria-labelledby="nav-profile_panel-tab">
            <br />
            <%--<div class="row">
                <div class="col-md-1">
                    <asp:Label ID="lblPanel" runat="server" Text="Panel" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-3">
                  <asp:TextBox ID="txtPanel" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnIncluirPanel" usesubmitbehavior="false" CssClass="btn btn-success" Width="150px" runat="server" OnClick="btnIncluirPanel_Click"><span class="glyphicon glyphicon-cog">Incluir panel</span></asp:LinkButton>
                </div>
            </div>
            <hr />--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvPaneles" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="IDAsset" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                        <asp:DropDownList ID="FiltroVersionPaneles" runat="server" Font-Size="XX-Small" OnSelectedIndexChanged="CambioFiltroVersionPaneles" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IDAsset" HeaderText="AAF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ItemIdAsset" HeaderText="AAF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="ItemDescripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="60%" />
                                <asp:CheckBoxField DataField="Estandar" HeaderText="¿Estándar?" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
<%--                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10%" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-pane fade <%=hidTAB.Value=="7"?"show active":"" %>" id="nav-profile_ajustes" aria-labelledby="nav-profile_ajustes-tab">
            <br />
            <div class="row">
                <div class="col-md-1">
                    <span>VERSIÓN: </span>
                </div>
                <div class="col-md-3">
                     <asp:DropDownList ID="cmbVersioAjustes" Font-Size="X-Small" Width="180px" runat="server" CssClass="form-control" OnSelectedIndexChanged="CambioFiltroVersionAjustes" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label2" runat="server" Text="AJUSTES POR OBRA: Taxímetro duración" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjustesTaximetro" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGAJU_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGAJU_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                        <%# Eval("CFGAJU_BU") %>
                                      <%--  <asp:Label ID="lblBU" runat="server" Text='<%# Eval("CFGAJU_BU") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbBU" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                        <%--<asp:DropDownList ID="FiltroEmpresaAjustes" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaAjustes" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGAJU_Empresa") %>
        <%--                                <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("CFGAJU_Empresa") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbEmpresa" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                      <%--  <asp:DropDownList ID="FiltroDelegacionAjuste" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionAjuste" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGAJU_Delegacion") %>
                                        <%--<asp:Label ID="lblDelegacion" runat="server" Text='<%# Eval("CFGAJU_Delegacion") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDelegacion" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGAJU_DuracionMinima" HeaderText="Duración mínima" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_DuracionMaxima" HeaderText="Duración máxima" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeAbonoMensual" HeaderText="% abono mensual" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
<%--                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="5" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label3" runat="server" Text="AJUSTES POR OBRA: Gastos fijos duración" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
             <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjustesGastosFijos" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGAJU_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGAJU_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGAJU_BU") %>
                                       <%-- <asp:Label ID="lblBU" runat="server" Text='<%# Eval("CFGAJU_BU") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbBU" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                        <%--<asp:DropDownList ID="FiltroEmpresaAjustes" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaAjustes" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_Empresa") %>
                                        <%--<asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("CFGAJU_Empresa") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbEmpresa" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                      <%--  <asp:DropDownList ID="FiltroDelegacionAjuste" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionAjuste" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_Delegacion") %>
                                        <%--<asp:Label ID="lblDelegacion" runat="server" Text='<%# Eval("CFGAJU_Delegacion") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDelegacion" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGAJU_Alcance" HeaderText="Alcance" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PocentajeCargoMaximo" HeaderText="% cargo máximo" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="CFGAJU_ValorObraCorta" HeaderText="Obra corta" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeCorreccionObraCorta" HeaderText="% corrección" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorObraLarga" HeaderText="Obra larga" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeCorreccionObraLarga" HeaderText="% corrección" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorMaximo" HeaderText="Valor máximo" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeCorreccionMaximo" HeaderText="% corrección" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
<%--                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="5" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label4" runat="server" Text="AJUSTES POR PRODUCTO: Ocupación" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjustesOcupacion" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGAJU_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGAJU_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_BU") %>
                                        <%--<asp:Label ID="lblBU" runat="server" Text='<%# Eval("CFGAJU_BU") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbBU" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                        <%--<asp:DropDownList ID="FiltroEmpresaAjustes" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaAjustes" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                           <%# Eval("CFGAJU_Empresa") %>
                                        <%--<asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("CFGAJU_Empresa") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbEmpresa" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                      <%--  <asp:DropDownList ID="FiltroDelegacionAjuste" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionAjuste" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_Delegacion") %>
                                        <%--<asp:Label ID="lblDelegacion" runat="server" Text='<%# Eval("CFGAJU_Delegacion") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDelegacion" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGAJU_AlcanceOcupacion" HeaderText="Alcance" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PeriodoOcupacion" HeaderText="Periodo ocupación" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="CFGAJU_ValorOcupacionMuyBaja" HeaderText="Valor ocupación muy baja" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeAbonoMaximo" HeaderText="% abono máximo" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorOcupacionBaja" HeaderText="Valor ocupación baja" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeAbonoOcupacionBaja" HeaderText="% abono ocupación baja" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorOcupacionMedia" HeaderText="Valor ocupación media" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeAbonoOcupacionMedia" HeaderText="% abono ocupación mediana" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorOcupacionAlta" HeaderText="Valor ocupación alta" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeAbonoOcupacionAlta" HeaderText="% abono ocupación alta" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PorcentajeAbonoOcupacionMuyAlta" HeaderText="% abono ocupación muy alta" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PorcentajeDelegacion" HeaderText="% delegación" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PorcentajePais" HeaderText="% país" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PorcentajeEmpresa" HeaderText="% empresa" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_AlmacenesObra" HeaderText="Almacenes obra" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_Almacenes" HeaderText="Almacenes" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_TiposProducto" HeaderText="Tipo producto" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
<%--                            <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="5" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label5" runat="server" Text="AJUSTES POR CLIENTE: Volumen" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjustesVolumen" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGAJU_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGAJU_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_BU") %>
                                       <%-- <asp:Label ID="lblBU" runat="server" Text='<%# Eval("CFGAJU_BU") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbBU" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                        <%--<asp:DropDownList ID="FiltroEmpresaAjustes" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroEmpresaAjustes" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGAJU_Empresa") %>
                                       <%-- <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("CFGAJU_Empresa") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbEmpresa" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                      <%--  <asp:DropDownList ID="FiltroDelegacionAjuste" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroDelegacionAjuste" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_Delegacion") %>
                                      <%--  <asp:Label ID="lblDelegacion" runat="server" Text='<%# Eval("CFGAJU_Delegacion") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbDelegacion" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGAJU_AlcanceVolumenCliente" HeaderText="Alcance" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorClienteMediano" HeaderText="Valor cliente mediano" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="CFGAJU_PorcentajeCorreccionClienteMediano" HeaderText="% corrección cliente mediano" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_ValorClienteGrande" HeaderText="Valor cliente grande" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n0}" ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_PorcentajeCorreccionClienteGrande" HeaderText="% corrección cliente grande" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
 <%--                               <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="10" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="5" />--%>
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label6" runat="server" Text="AJUSTES POR CUMPLIMIENTO PRESUPUESTO" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjustesPresupuesto" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGAJU_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGAJU_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_BU") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGAJU_Empresa") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_Delegacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGAJU_Meses" HeaderText="Meses" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_CumplimientoBajoAfectaGFCentral" HeaderText="Cump. Bajo GFC" ItemStyle-HorizontalAlign="Center"   ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="CFGAJU_CumplimientoBajoAfectaGFBU" HeaderText="Cump. Bajo GFBU" ItemStyle-HorizontalAlign="Center"   ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_CumplimientoAltoAfectaGFCentral" HeaderText="Cump. Alto GFC" ItemStyle-HorizontalAlign="Center"  ApplyFormatInEditMode="true"  HeaderStyle-Width="60"  ItemStyle-Width="60" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_CumplimientoAltoAfectaGFBU" HeaderText="Cump. Alto GFBU" ItemStyle-HorizontalAlign="Center"   ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_CumplimientoBajoPorcentaje" HeaderText="Cump. Bajo %" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_BonificacionCumplimientoBajoPorcentaje" HeaderText="Bon. Cump. Bajo %" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_CumplimientoAltoPorcentaje" HeaderText="Cump. Alto %" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_BonificacionCumplimientoAltoPorcentaje" HeaderText="Bon. Cump. Alto %" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n2}" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label7" runat="server" Text="TOPES AJUSTES" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAjustesTopes" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGAJU_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGAJU_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_BU") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGAJU_Empresa") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGAJU_Delegacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGAJU_Concepto" HeaderText="Concepto" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="60"/>
                                <asp:BoundField DataField="CFGAJU_ValorMaximoPorcentaje" HeaderText="Valor máximo" ItemStyle-HorizontalAlign="Center" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGAJU_AplicarSobre" HeaderText="Aplicar sobre" ItemStyle-HorizontalAlign="Left" ApplyFormatInEditMode="true" HeaderStyle-Width="50" ItemStyle-Width="50" ControlStyle-Width="50" />
                                <asp:BoundField DataField="CFGAJU_AplicarA" HeaderText="Aplicar a" ItemStyle-HorizontalAlign="Left" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label8" runat="server" Text="FALLIDOS" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvFallidos" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="CFGFAL_ID" OnRowEditing="grvPaneles_RowEditing" OnRowCancelingEdit="grvPaneles_RowCancelingEdit" OnRowUpdating="grvPaneles_RowUpdating" OnRowDataBound="grvPaneles_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="CFGFAL_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField  HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Versión
                                      <%--  <asp:DropDownList ID="FiltroVersion" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersion" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Version") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30" ItemStyle-Width="30" ControlStyle-Width="30">
                                    <HeaderTemplate>
                                        BU
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGFAL_BU") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ControlStyle-Width="40">
                                    <HeaderTemplate>
                                        Empresa
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                         <%# Eval("CFGFAL_Empresa") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60">
                                    <HeaderTemplate>
                                        Delegación
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                          <%# Eval("CFGFAL_Delegacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFGFAL_IM" HeaderText="IM" ItemStyle-HorizontalAlign="Center" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGFAL_PorcentajeCobrados" HeaderText="% Cobrados" ItemStyle-HorizontalAlign="Center" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                <asp:BoundField DataField="CFGFAL_PorcentajeFallidos" HeaderText="% Fallidos" ItemStyle-HorizontalAlign="Center" ApplyFormatInEditMode="true" HeaderStyle-Width="60" ItemStyle-Width="60" ControlStyle-Width="60" />
                                </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

      <div class="tab-pane fade <%=hidTAB.Value=="8"?"show active":"" %>" id="nav-profile_bu" role="tabpanel" aria-labelledby="nav-profile_bu-tab">
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


        <div class="tab-pane fade <%=hidTAB.Value=="9"?"show active":"" %>" id="nav-profile_historico" role="tabpanel" aria-labelledby="nav-profile_historico-tab">
            <br />
            <%--<asp:RadioButton ID="rdbGFV" runat="server" GroupName="TipoHistorico" Text="GFV" AutoPostBack="true" OnCheckedChanged="rbtn_CheckedChanged" />
            &nbsp;
            <asp:RadioButton ID="rdbParametros" runat="server" GroupName="TipoHistorico" Text="Parámetros" AutoPostBack="true" OnCheckedChanged="rbtn_CheckedChanged" />--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvHistorico" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                               <%-- <asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                            <asp:DropDownList ID="FiltroVersionHistorico" runat="server" SelectionMode="Multiple" Font-Size="X-Small" OnSelectedIndexChanged="CambioFiltroVersionBU" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Versión") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderStyle-Width="200">
                                    <HeaderTemplate>
                                        Versión
                                            <asp:ListBox  ID="FiltroVersionHistorico" runat="server" SelectionMode="Multiple" Font-Size="X-Small" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="test1"></asp:ListItem>
                                                <asp:ListItem Text="test2"></asp:ListItem>
                                                <asp:ListItem Text="test3"></asp:ListItem>
                                            </asp:ListBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Versión") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="115" />--%>
                                <%--<asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" />--%>
                                <asp:BoundField DataField="Versión" HeaderText="Versión" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="Base" HeaderText="Versión base" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="Desde" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HeaderText="Hasta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Fecha Log" DataFormatString="{0:d}" HeaderText="Fecha Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Hora Log" HeaderText="Hora Log" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                <asp:BoundField DataField="Usuario Log" HeaderText="Usuario Log" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200" />
                                <asp:BoundField DataField="Alcance" HeaderText="Grado de cambio" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="315" />
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

        <div class="tab-pane fade <%=hidTAB.Value=="10"?"show active":"" %>" id="nav-profile_usuario" role="tabpanel" aria-labelledby="nav-profile_usuario-tab">
            <br />
            <div class="row">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnLimpiarUsuario" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarUsuario_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtUsuarioRed" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

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
                    <asp:Label ID="lblElegirVersion" runat="server" Text="Elegir versión cálculo "></asp:Label>
                    <asp:CheckBox ID="chkAbonos" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblAbonosAjustes" runat="server" Text="Abonos/Ajustes "></asp:Label>
                    <asp:CheckBox ID="chkTecnico" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" />
                    <asp:Label ID="lblTecnico" runat="server" Text="Configurar aspectos técnicos "></asp:Label>
                </div>
                <div class="col-md-2">
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
                                <asp:CheckBoxField DataField="USR_Abono" HeaderText="Abonos/Ajustes" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                                <asp:CheckBoxField DataField="USR_Tecnico" HeaderText="Técnico" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
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

         <div class="tab-pane fade <%=hidTAB.Value=="11"?"show active":"" %>" id="nav-profile_abonos" role="tabpanel" aria-labelledby="nav-profile_abonos-tab">
            <br />
            <div class="row">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnLimpiarAbono" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnLimpiarAbono_Click"><span class="glyphicon glyphicon-cog">Inicializar</span></asp:LinkButton>
                </div>
                <div class="col-md-9">
                </div>
                 <div class="col-md-2">
                    <asp:LinkButton ID="btnAgregarAbono" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAgregarAbono_Click"><span class="glyphicon glyphicon-cog">Agregar ajuste</span></asp:LinkButton>
                </div>
             </div>
             <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblFichaAbono" runat="server" Text="Ficha " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtFicha" runat="server" AutoPostBack="true" CssClass="form-control"  AutoComplete="off" Width="100%" OnTextChanged="txtFicha_TextChanged"></asp:TextBox>
                </div>
                 <div class="col-md-3">
                    <asp:Label ID="lblMonedaAbono" runat="server" Text="Moneda " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control" Enabled="false" AutoComplete="off" Width="100%"></asp:TextBox>
                </div>
                  <div class="col-md-3">
                    <asp:Label ID="lblImporteAbono" runat="server" Text="Importe " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtImporteAbono" runat="server" AutoPostBack="true" DataFormatString="{0:N2}" CssClass="form-control" onkeydown = "return ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode = 44)" AutoComplete="off" ValidationExpression="\d" Width="100%" OnTextChanged="txtImporteAbono_TextChanged"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="REtxtxtImporteAbono" runat="server" ControlToValidate="txtImporteAbono" ErrorMessage="¡ERROR!" ValidationExpression="^\-?[0-9]+\,?[0-9]+$"></asp:RegularExpressionValidator>
                        <%--ValidationExpression="\d*\,?\d*"></asp:RegularExpressionValidator>--%>
                  </div>
                  <div class="col-md-3">
                    <asp:Label ID="Afecta" runat="server" Text="Afecta a " Style="margin-right: 0px"></asp:Label>
                    <asp:DropDownList ID="cmbAfectaAbono" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                </div>
             </div>
             <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblObservacionesAbono" runat="server" Text="Observaciones  " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtObservacionesAbono" runat="server" CssClass="form-control"  AutoComplete="off" Width="100%"></asp:TextBox>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-2">
                    <asp:TextBox ID="txtBuscarFicha" runat="server" CssClass="form-control"  AutoComplete="off" Width="125px"></asp:TextBox>
                 </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnBuscarFicha" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnBuscarFicha_Click"><span class="glyphicon glyphicon-cog">BUSCAR FICHA</span></asp:LinkButton>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvAbonos" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" Height="100%" Width="100%" AlternatingRowStyle-BackColor="#999999" HeaderStyle-BackColor="#284775" HeaderStyle-ForeColor="white" DataKeyNames="ABO_ID, ABO_Ficha" OnRowDeleting="grvAbonos_RowDeleting" OnRowEditing="grvAbonos_RowEditing" OnRowCancelingEdit="grvAbonos_RowCancelingEdit" OnRowUpdating="grvAbonos_RowUpdating" OnRowDataBound="grvAbonos_RowDataBound" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="ABO_ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField HeaderStyle-Width="110px" HeaderText="FICHA" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFicha" runat="server" Text='<%# Bind("[ABO_Ficha]")%>' Width="110px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60px" HeaderText="IMPORTE" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtImporte" runat="server" Text='<%# Bind("ABO_Importe", "{0:#,##0.00}") %>' ValidationExpression="\d*" Width="75px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("ABO_Importe","{0:#,##0.00}") %>' Width="55px"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="110px" HeaderText="MONEDA" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("[ABO_Moneda]")%>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="60px" HeaderText="AFECTA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAfecta" runat="server" Text='<%# Eval("ABO_Afecta") %>' Visible="false" />
                                        <asp:DropDownList ID="cmbAfecta" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="400px" HeaderText="OBSERVACIONES" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtObservaciones" runat="server" Text='<%# Bind("ABO_Observaciones") %>' Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblObservaciones" runat="server" Text='<%# Bind("ABO_Observaciones") %>' Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="110px" HeaderText="USUARIO" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsaurio" runat="server" Text='<%# Bind("[ABO_Usuario]")%>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="110px" HeaderText="FECHA" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfecha" runat="server" Text='<%# Bind("[ABO_Fecha]")%>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="110px" HeaderText="US. MODIF." HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsaurioModificar" runat="server" Text='<%# Bind("[ABO_UsuarioModificar]")%>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="110px" HeaderText="FECHA MODIF." HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfechaModificar" runat="server" Text='<%# Bind("[ABO_FechaModificar]")%>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Editar" ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Img/edicion.png" CancelImageUrl="~/Img/cancelar.png" UpdateImageUrl="~/Img/actualizar.png" HeaderStyle-Width="5%" />
                                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/eliminar.png" DeleteText="Borrar" HeaderStyle-Width="5%" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

         <div class="tab-pane fade <%=hidTAB.Value=="12"?"show active":"" %>" id="nav-profile_tecnico" role="tabpanel" aria-labelledby="nav-profile_tecnico-tab">
            <br />
            <div class="row">
                <div class="col-md-12">
                     <asp:Label ID="Label9" runat="server" Text="PROCESAR EN PARALELO" Style="margin-right: 0px" Font-Bold="true" Font-Size="Large"></asp:Label>
                </div>
            </div>
             <br />
             <div class="row">
                <div class="col-md-5">
                    <asp:Label ID="lblHilos" runat="server" Text="Número de pedidos a procesar de forma paralela: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtHilos" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                    <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1" MaximumValue ="100" ControlToValidate="txtHilos" Color="red" ErrorMessage="El número debe ser un entero mayor a cero" />
                </div>
                  <div class="col-md-3">
                    <asp:LinkButton ID="btnHilos" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnHilos_Click"><span class="glyphicon glyphicon-cog">Actualizar hilos</span></asp:LinkButton>
                </div>
            </div>
            <hr />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:CheckBox ID="chkPararEjecucion" runat="server" AutoPostBack="true" Checked="false" Font-Bold="True" Font-Size="" Height="" OnCheckedChanged="chkPararEjecucion_CheckedChanged"/>
                    <asp:Label ID="lblPararEjecucion" runat="server" Text=" Parar ejecución del ROP si se detectan errores en las tablas de datos "></asp:Label>
                 </div>
            </div>
            <hr />
            <br />   
            <div class="row">
                <div class="col-md-12">
                     <asp:Label ID="Label11" runat="server" Text="BARRA PROGRESO CÁLCULO" Style="margin-right: 0px" Font-Bold="true" Font-Size="Large"></asp:Label>
                </div>
            </div>
             <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblBarraProgreso" runat="server" Text="Factor para cálculo de la barra de progreso: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtBarraProgreso" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                    <asp:RangeValidator runat="server" Type="Double" MinimumValue="0,00" MaximumValue ="100,00" ControlToValidate="txtBarraProgreso" Color="red" ErrorMessage="El número debe ser un entero mayor a cero" />
                </div>
                 <div class="col-md-2">
                    <asp:Label ID="lblBarraProgresoOfertas" runat="server" Text="Segundos barra ofertas: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtBarraProgresoOfertas" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                    <asp:RangeValidator runat="server" Type="Double" MinimumValue="0,00" MaximumValue ="100,00" ControlToValidate="txtBarraProgresoOfertas" Color="red" ErrorMessage="El número debe ser un entero mayor a cero" />
                </div>
                 <div class="col-md-2">
                    <asp:Label ID="lblBarraProgresoPedidos" runat="server" Text="Segundos barra pedidos/facturas: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtBarraProgresoPedidos" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                    <asp:RangeValidator runat="server" Type="Double" MinimumValue="0,00" MaximumValue ="100,00" ControlToValidate="txtBarraProgresoPedidos" Color="red" ErrorMessage="El número debe ser un entero mayor a cero" />
                </div>
                  <div class="col-md-2">
                    <asp:Label ID="lblBarraProgresoFichas" runat="server" Text="Segundos barra fichas: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtBarraProgresoFichas" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                    <asp:RangeValidator runat="server" Type="Double" MinimumValue="0,00" MaximumValue ="100,00" ControlToValidate="txtBarraProgresoFichas" Color="red" ErrorMessage="El número debe ser un entero mayor a cero" />
                </div>
                <div class="col-md-4">
                    <asp:LinkButton ID="btnBarraProgreso" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnBarraProgreso_Click"><span class="glyphicon glyphicon-cog">Actualizar factor barra</span></asp:LinkButton>
                </div>
            </div>
            <hr />
            <br />
             <div class="row">
                <div class="col-md-12">
                     <asp:Label ID="lblVersiones" runat="server" Text="VERSIONES SOFTWARE" Style="margin-right: 0px" Font-Bold="true" Font-Size="Large"></asp:Label>
                </div>
            </div>
             <br />
             <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblVersión" runat="server" Text="Versión (software) del ROP: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtVersion" runat="server" AutoPostBack="true" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblCambios" runat="server" Text="Cambios realizados: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtCambios" runat="server" AutoPostBack="true" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                </div>
                 <div class="col-md-3">
                    <asp:LinkButton ID="btnVersion" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnVersion_Click"><span class="glyphicon glyphicon-cog">Actualizar versión</span></asp:LinkButton>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                 <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvVersiones" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="Version" HeaderText="Versión"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="Cambios" HeaderText="Cambios" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Desde" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                            </Columns>
                            <HeaderStyle BackColor="#025c2c" ForeColor="white" HorizontalAlign="Center"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <hr />
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:LinkButton ID="btnExcelEtiquetas" Width="120px" usesubmitbehavior="false" OnClientClick="return ponerSpinnerEtiqueta()" CssClass="btn btn-primary" runat="server" OnClick="btnExportarEtiquetas_Click"><span class="glyphicon glyphicon-cog">Exportar etiquetas</span></asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnAbrirExcelEtiquetas" Width="120px" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="btnAbrirExcelEtiquetas_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar</span></asp:LinkButton>
                </div>
                <div class="col-md-6">
                    <asp:FileUpload ID="ficheroSeleccionadoEtiquetas" CssClass="form-control" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnImportarExcelEtiquetas" Width="150px" usesubmitbehavior="false" CssClass="btn btn-primary" runat="server" OnClick="btnSubirExcelEtiquetas_Click"><span class="glyphicon glyphicon-cog">Importar excel etiquetas</span></asp:LinkButton>
                </div>
            </div>
            <hr />
            <br />
            <div class="row">
                <div class="col-md-12">
                     <asp:Label ID="lblDatosTransporte" runat="server" Text="DATOS TRANSPORTE" Style="margin-right: 0px" Font-Bold="true" Font-Size="Large"></asp:Label>
                </div>
            </div>
             <br />
            <div class="row">
                <div class="col-md-10">
                    <asp:Label ID="lblEmailTransporte" runat="server" Text="Correo aviso proceso de transporte: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtEmailTransporte" runat="server" AutoPostBack="true" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                </div>
                  <div class="col-md-2">
                    <asp:LinkButton ID="btnEmailTransporte" Width="90px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnEmailTransporte_Click"><span class="glyphicon glyphicon-cog">Actualizar correo</span></asp:LinkButton>
                </div>
            </div>
             <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblUnidadesPorte" runat="server" Text="Mínimo unidades porte: " Style="margin-right: 0px"></asp:Label>
                    <asp:TextBox ID="txtUnidadesPorte" runat="server" AutoPostBack="true" DataFormatString="{0:N0}" CssClass="form-control" AutoComplete="off" Width="100%"></asp:TextBox>
                    <asp:RangeValidator runat="server" Type="Integer" MinimumValue="0"  MaximumValue="1000000" ControlToValidate="txtUnidadesPorte" Color="red" ErrorMessage="El número debe ser un entero mayor a cero" />
                </div>
                  <div class="col-md-2">
                    <asp:LinkButton ID="btnUnidadesPorte" Width="150px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnUnidadesPorte_Click"><span class="glyphicon glyphicon-cog">Actualizar unidades porte</span></asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <asp:LinkButton ID="btnExportarTransporte" Width="150px" usesubmitbehavior="false" OnClientClick="return ponerSpinnerTransporte()" CssClass="btn btn-info" runat="server" OnClick="btnExportarTransporte_Click"><span class="glyphicon glyphicon-cog">Exportar datos portes</span></asp:LinkButton>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="btnAbrirExcelTransporte" Width="120px" usesubmitbehavior="false" CssClass="btn btn-info" runat="server" OnClick="btnAbrirExcelTransporte_Click"><span class="glyphicon glyphicon-cog">Abrir/Bajar</span></asp:LinkButton>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grvDatosTransporte" visible="false" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                            <Columns>
                                <asp:BoundField DataField="EMPRESA" HeaderText="EMPRESA" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="DISTANCIA" HeaderText="DISTANCIA" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="UPS" HeaderText="UPS" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="TONELADASALBARAN" DataFormatString="{0:N2}" HeaderText="TONELADAS ALBARÁN" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="KM" HeaderText="KM" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="FECHA" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField DataField="MONEDA" HeaderText="MONEDA" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="MONEDAEMPRESA" HeaderText="MONEDA EMPRESA" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="Cambio" HeaderText="CAMBIO" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="MesesProcesados" HeaderText="MESES PROCESADOS" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                            </Columns>
                            <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
     
        <div class="tab-pane fade <%=hidTAB.Value=="13"?"show active":"" %>" id="nav-profile_tablas" role="tabpanel" aria-labelledby="nav-profile_tablas-tab">
            <br />
            <h3>Tablas SQL / Copias de AXAPTA:</h3>
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

            <br />
            <h3>AXAPTA: Relación de copias</h3>
            <p></p>
            <asp:Panel ID="Panel1" runat="server" Visible="true">
                <p>&nbsp;</p>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grvTablasAxpata" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" Font-Size="Small">
                                <Columns>
                                    <asp:BoundField DataField="Source" HeaderText="Source" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                    <asp:BoundField DataField="Table" HeaderText="Table" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120" />
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120" />
                                    <asp:BoundField DataField="State" HeaderText="State" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120" />
                                    <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" />
                                   </Columns>
                                <HeaderStyle BackColor="#17a2b8" ForeColor="white" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <ajax:ModalPopupExtender ID="mpeError" PopupControlID="PanelError" TargetControlID="lblpopup" CancelControlID="btnCerrarError" PopupDragHandleControlID="headerdivError" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelError" Style="display: none;" CssClass="modalPopupError" runat="server">
        <div id="headerdivError" class="headerError">
            <asp:Label ID="lblTituloError" runat="server" Font-Bold="true" Font-Size="Large" HorizontalAlign="Left" Text=""></asp:Label>
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
            <asp:Label ID="lblTituloInformacion" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Left" Text=""></asp:Label>
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
    
    <ajax:ModalPopupExtender ID="mpeDecidir" PopupControlID="PanelDecidir" TargetControlID="lblpopup" CancelControlID="btnCerrarDecidir" PopupDragHandleControlID="headerdivDecidir" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelDecidir" Style="display: none;" CssClass="modalPopupDecidir" runat="server">
        <div id="headerdivDecidir" class="headerDecidir">
            <asp:Label ID="lblTituloDecidir" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Left" Text="...."></asp:Label>
        </div>
        <div id="divdeateilsDecidir"></div>
        &nbsp;
        <asp:Label ID="lblObservaciones" runat="server" Font-Size="Medium" Text="--" HorizontalAlign="Center"></asp:Label>
        <br />
        <div id="footerdivDecidir" class="footerDecidir">
            <asp:Button ID="btnOkDecidir" runat="server" Font-Size="Small" Text="SI" OnClick="btnOkDecidir_Click" class="buttonDecidirOk" />
            &nbsp;
            <asp:Button ID="btnCerrarDecidir" runat="server" Font-Size="Small" Text="NO" class="buttonDecidirCancel" />
        </div>
    </asp:Panel>

    <ajax:ModalPopupExtender ID="mpeDecidirBorrar" PopupControlID="PanelDecidirBorrar" TargetControlID="lblpopup" CancelControlID="btnCerrarDecidirBorrar" PopupDragHandleControlID="headerdivDecidirBorrar" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelDecidirBorrar" Style="display: none;" CssClass="modalPopupDecidir" runat="server">
        <div id="headerdivDecidirBorrar" class="headerDecidir">
            <asp:Label ID="lblTituloDecidirBorrar" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Left" Text="..."></asp:Label>
        </div>
        <div id="divdeateilsDecidirBorrar"></div>
        &nbsp;
        <asp:Label ID="lblDecidirBorrar" runat="server" Font-Size="Medium" Text="--" HorizontalAlign="Center"></asp:Label>
        <br />
        <div id="footerdivDecidirBorrar" class="footerDecidir">
            <asp:Button ID="btnOkDecidirBorrar" runat="server" Font-Size="Small" Text="SI" OnClick="btnOkDecidirBorrar_Click" class="buttonDecidirOk" />
            &nbsp;
            <asp:Button ID="btnCerrarDecidirBorrar" runat="server" Font-Size="Small" Text="NO" class="buttonDecidirCancel" />
        </div>
    </asp:Panel>

    <ajax:ModalPopupExtender ID="mpeDecidirBorrarUsuario" PopupControlID="PanelDecidirBorrarUsuario" TargetControlID="lblpopup" CancelControlID="btnCerrarDecidirBorrarUsuario" PopupDragHandleControlID="headerdivDecidirBorrarUsuario" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelDecidirBorrarUsuario" Style="display: none;" CssClass="modalPopupDecidir" runat="server">
        <div id="headerdivDecidirBorrarUsuario" class="headerDecidir">
            <asp:Label ID="lblTituloDecidirBorrarUsuario" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Left" Text="..."></asp:Label>
        </div>
        <div id="divdeateilsDecidirBorrarUsuario"></div>
        &nbsp;
        <asp:Label ID="lblDecidirBorrarUsuario" runat="server" Font-Size="Medium" Text="--" HorizontalAlign="Center"></asp:Label>
        <br />
        <div id="footerdivDecidirBorrarUsuario" class="footerDecidir">
            <asp:Button ID="btnOkDecidirBorrarUsuario" runat="server" Font-Size="Small" Text="SI" OnClick="btnOkDecidirBorrarUsuario_Click" class="buttonDecidirOk" />
            &nbsp;
            <asp:Button ID="btnCerrarDecidirBorrarUsuario" runat="server" Font-Size="Small" Text="NO" class="buttonDecidirCancel" />
        </div>
    </asp:Panel>

    <ajax:ModalPopupExtender ID="mpeDecidirBorrarAbono" PopupControlID="PanelDecidirBorrarAbono" TargetControlID="lblpopup" CancelControlID="btnCerrarDecidirBorrarAbono" PopupDragHandleControlID="headerdivDecidirBorrarAbono" runat="server"></ajax:ModalPopupExtender>
    <asp:Panel ID="PanelDecidirBorrarAbono" Style="display: none;" CssClass="modalPopupDecidir" runat="server">
        <div id="headerdivDecidirBorrarAbono" class="headerDecidir">
            <asp:Label ID="lblTituloDecidirBorrarAbono" runat="server" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Left" Text="..."></asp:Label>
        </div>
        <div id="divdeateilsDecidirBorrarAbono"></div>
        &nbsp;
        <asp:Label ID="lblDecidirBorrarAbono" runat="server" Font-Size="Medium" Text="--" HorizontalAlign="Center"></asp:Label>
        <br />
        <div id="footerdivDecidirBorrarAbono" class="footerDecidir">
            <asp:Button ID="btnOkDecidirBorrarAbono" runat="server" Font-Size="Small" Text="SI" OnClick="btnOkDecidirBorrarAbono_Click" class="buttonDecidirOk" />
            &nbsp;
            <asp:Button ID="btnCerrarDecidirBorrarAbono" runat="server" Font-Size="Small" Text="NO" class="buttonDecidirCancel" />
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

        var flg = true;
        function ponerSpinnerTransporte() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExportarTransporte.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Generar');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnExportarTransporte', '');
                return true;
            }
        }

         var flg = true;
        function ponerSpinnerEtiqueta() {
            if (flg) {
                try {
                    var btn = $("#<%=btnExcelEtiquetas.ClientID%>");
                    btn.html('<span class="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Generar');//.addClass('disabled');
                    btn.prop("disabled", true);
                    flg = false;
                }
                catch (e) {
                    console.log("Boton buscar error script");
                }
                javascript: __doPostBack('ctl00$MainContent$btnExcelEtiquetas', '');
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
