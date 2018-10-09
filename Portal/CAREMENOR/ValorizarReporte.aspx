<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="ValorizarReporte.aspx.cs" Inherits="CAREMENOR_ValorizarReporte" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
<script src="../js/gridviewScroll.min.js" type="text/javascript"></script>

     <style type="text/css">

    
     </style>
    <script type="text/javascript">

        

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
    function doAlert( mensaje)
    {
        var msg = new DOMAlert(
        {
            title: 'Mensaje del Sistema ',
            text: '<br/>' + mensaje,
            skin: 'default',
            width: 300,
            height: 80,
            ok: { value: true, text: 'Aceptar', onclick: showValue },
			   
        });
        msg.show();
    };
		
    function showValue(sender, value)
    {
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
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">

                 <div class="row">
                    <div class="col-md-8">
                        <b>REPORTE VALORIZACIÓN EQUIPOS MENORES</b>
                    </div>
                    <div class="col-md-4">
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
      <div class="row">
        <div class="col-md-2">
             <label>Año</label>
            <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged">
                            </asp:DropDownList>
        </div>
        <div class="col-md-2">
               <label>Mes</label>
            <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                            </asp:DropDownList>
        </div>

        <div class="col-md-2">
            <label>CC.</label>
            <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged1"></asp:DropDownList>

        </div>
        <div class="col-md-3">
            <label>Proveedores</label>

            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" ></asp:DropDownList>
        </div>


        
        <div class="col-md-3">
       <br />
     <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click"  />
            
        </div>
       
    </div>
     <div class="row">
        <div class="col-md-12">


            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                <LocalReport EnableExternalImages="True">
                </LocalReport>
            </rsweb:ReportViewer>


        </div>

    </div>
</asp:Content>

