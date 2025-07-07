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
    <address>
        <hr />
        <asp:HiddenField id="myTiempo" runat="server" />
        <div class="row">
            <div class="col-md-2">
                <asp:LinkButton ID="btnBuscarInformacion" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="buscar_1" OnClientClick="return simulateProgress()"><span class="glyphicon glyphicon-cog">Progress bar 1</span></asp:LinkButton>
                <asp:LinkButton ID="btnOtroProgressBar" usesubmitbehavior="false" CssClass="btn btn-success" runat="server" OnClick="buscar_2" OnClientClick="return moverBarra()"><span class="glyphicon glyphicon-cog">Progress bar 2</span></asp:LinkButton>
            </div>
        </div>
        <hr />
         <div class="row">
             <div class="col-md-12">
                <div class="progress">
                <div id="myProgressBar" class="progress-bar" style="width:0%;">
                    <span id="myProgressBarValue" class="progress-bar-text">0%</span>
                </div>
            </div>
            </div>
        </div>
         <div class="row">
            <div class="col-md-12">
                <div class="w3-light-grey w3-round">
                    <div id="myBar" class="w3-container w3-round w3-blue" style="width:50%">50%</div>
                </div> 
              </div>
        </div>
    </address>
 
     <script>

         function updateProgressBar(progressValue) {
             const progressBar = document.getElementById('myProgressBar');
             progressBar.style.width = progressValue + '%';
              const progressBarValue = document.getElementById('myProgressBarValue');
             progressBarValue.innerHTML = progressValue + ' %';
         }

         function simulateProgress() {
             const tiempoValor = parseInt(document.getElementById('myTiempo'));
             let tiempo = 100;
             //let progress = 0;
             let progress = parseInt(myTiempo.innerHTML);
             const interval = setInterval(() => {
                 if (progress >= tiempo) {
                     progress = 0;
                     updateProgressBar(progress);
                     clearInterval(interval);
                     simulateProgress();
                 } else {
                     //progress++;
                     updateProgressBar(progress);
                 }
             }, tiempo);
         }

         function moverBarra() {
             var elem = document.getElementById("myBar");
             var width = 1;
             var id = setInterval(frame, 10);
             function frame() {
                 if (width >= 100) {
                     clearInterval(id);
                 } else {
                     width++;
                     elem.style.width = width + '%';
                 }
             }
         }
     </script> 
</asp:Content>
