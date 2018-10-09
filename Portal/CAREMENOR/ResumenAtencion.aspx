<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="ResumenAtencion.aspx.cs" Inherits="CAREMENOR_ResumenAtencion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
        //document.onselectstart = function () { return false; }
        function SoloNum(e) {
            var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
            return ((key_press > 47 && key_press < 58) || key_press == 46);
            // el  "|| key_press == 46" es para incluir el punto ".", si borramos solo ingresara enteros 
        }
        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        // Función para las teclas especiales
        //------------------------------------------
        function jsIsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, y Delete
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, y flechas
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {  //Check dot key code should be allowed
                    return true;
                }
            }
            // The rest
            return false;
        }
        function val(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8 || tecla == 32) return true;
            patron = /[A-Za-z]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValidaDDL(source, arguments) {
            if (arguments.Value < 1) {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
        }
        function doAlert(mensaje) {
            var msg = new DOMAlert(
            {
                title: 'Mensaje del Sistema :',
                text: '<br/>' + mensaje,
                skin: 'default',
                width: 300,
                height: 80,
                ok: { value: true, text: 'Aceptar', onclick: showValue },

            });
            msg.show();
        };

        function showValue(sender, value) {
            sender.close();

        }
        function lettersOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
          ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
          (charCode < 97 || charCode > 122)) {

                return false;
            }
            return true;
        }

      
       function popup(cc, ancho, alto) {
           
            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (ancho / 2);
            posicion_y = (screen.height / 2) - (alto / 2);

            var win = window.open("../OPERACIONES/RptStatusReq.aspx?CENTRO_COSTO=" + cc , "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");

    }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>RESUMEN ESTADO REQUERIMIENTOS DE EQUIPOS  </b>
                <asp:HiddenField ID="HdColumnas" runat="server" />
                <asp:HiddenField ID="HdCC" runat="server" />
                <asp:Label ID="lblCol" runat="server" ></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-6">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-condensed" Width="100%" OnRowDataBound="GridView1_RowDataBound">
            </asp:GridView>
        </div>
        <div class="col-md-3">
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <center>
<asp:ImageButton runat="server" ID="btnDescargar" ImageUrl="~/imagenes/descargar.png" OnClick="btnDescargar_Click"></asp:ImageButton>
            </center>
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="height:300px">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="300px"></rsweb:ReportViewer>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>REQUERIMIENTOS SIN OR</b></div>
        </div>
    </div>
     <div class="row">
        <div class="col-md-3">
            <center>
<asp:ImageButton runat="server" ID="btnOR" ImageUrl="~/imagenes/descargar.png" OnClick="btnOR_Click" ></asp:ImageButton>
            </center>
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
    </div
   <div class="row">
        <div class="col-md-12" >
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="100%">
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>

