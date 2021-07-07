<%@ Page Title="Datos WS Axapta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Original.aspx.cs" Inherits="ROP_Informe.Original" %>


<asp:Content ID="Content3" runat="server" contentplaceholderid="HeaderContent">
    <h2> &nbsp;&nbsp;INFORME ROP</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <address>
       <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Height="25px" Text="Empresa" style="margin-left: 0px"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="cmbEmpresa" runat="server" Width="100px" Height="20px" Font-Size="Small">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Height="25px" Text="Número oferta"></asp:Label>
&nbsp;&nbsp;
&nbsp;&nbsp;
        <asp:TextBox ID="txtNumero" runat="server" Height="25px" Width="228px" Font-Size="Small"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBuscarInformacion" runat="server" Text="Buscar información" OnClick ="btnBuscarInformacion_Click" Height="25px" style="cursor: pointer; cursor: hand;" Width="150px" Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="Small"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </address>
    <address>
        <asp:Label ID="lblMensajeError" runat="server" BackColor="Red" BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#FFFF99" Text="..." Width="100%"></asp:Label>
    </address>
    <address>
        &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" Height="18px" Text="Oferta" style="margin-left: 0px" Width="133px"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="txtNombreOferta" runat="server" Height="20px" Width="100%" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
    </address>
        <asp:GridView ID="dataDatos" runat="server" AutoGenerateColumns="True" style="width: 875px; overflow: auto; height: 160px;" ViewStateMode="Enabled" AllowPaging="True" Font-Names="Arial" Font-Size="Small" OnRowDataBound="dataDatos_RowDataBound">
        </asp:GridView>

    &nbsp;&nbsp;
  <%--  <table id="table" data-detail-view="true" class="table table-striped table-borderless">
        <thead>
            <tr>
                <th data-field="id">ID</th>
                <th data-field="name">Item Name</th>
                <th data-field="price">Item Price</th>
            </tr>
        </thead>
    </table>

    <script>
        var mydata =
            [
                {
                    "id": 0,
                    "name": "test0",
                    "price": "$0",
                    "hijo": [
                        {
                            "id": 1,
                            "name": "test1",
                            "price": "$1"
                        },
                        {
                            "id": 1,
                            "name": "test2",
                            "price": "$2",
                            "hijo": [
                                {
                                    "id": 1,
                                    "name": "test1",
                                    "price": "$1"
                                },
                                {
                                    "id": 1,
                                    "name": "test2",
                                    "price": "$2",
                                    "hijo": [
                                        {
                                            "id": 1,
                                            "name": "test1",
                                            "price": "$1"
                                        },
                                        {
                                            "id": 1,
                                            "name": "test2",
                                            "price": "$2"
                                        }
                                    ]
                                }
                            ]
                        }
                    ]
                },
                {
                    "id": 1,
                    "name": "test1",
                    "price": "$1"
                }
            ];

        $(function () {
            $('#table').bootstrapTable({
                data: mydata,
                detailView: true,
                detailFilter: function (index, row) {
                    return row.hijo != null;
                },
                onExpandRow: function (index, row, $detail) {
                    if (row.hijo != null)
                        expandirTabla(index, row, $detail);
                }
            });
            $('#table')[0].classList.value = "table table-bordered";

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
                    columnas = Object.keys(row.hijo[0]);
                }
                else
                    return;

                for (i = 0; i < columnas.length; i++) {
                    columns.push({
                        field: columnas[i],
                        title: columnas[i],
                        sortable: true
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
                    detailFilter: function (index, row) {
                        return row.hijo != null;
                    },
                    onExpandRow: function (index, row, $detail) {
                        /* eslint no-use-before-define: ["error", { "functions": false }]*/
                        expandirTabla(index, row, $detail)
                    }
                })
                $el[0].classList.value = "table  table-bordered";
            }
        }

    </script>--%>
</asp:Content>
