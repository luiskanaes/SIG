<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Presupuesto.aspx.cs" Inherits="OPERACIONES_Presupuesto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
			    title: 'Mensaje del Sistema :',
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">
                <b>PRESUPUESTOS</b>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Empresa</label>
                        <asp:DropDownList ID="ddlEmpresas" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>Gerencia</label>
                        <asp:DropDownList ID="ddlGerencia" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerencia_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>Centro</label>
                        <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                </div>
               <br />
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <b>VENTAS</b>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                         <br />
                        <b>Sub Total : FACTURA (00)</b>
                    </div>
                    <div class="col-md-4">
                        <label>PO</label>
                        <asp:TextBox ID="txtV_PO_FACTURADO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>POM</label>
                        <asp:TextBox ID="txtV_POM_FACTURADO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
               
                <div class="row">
                    <div class="col-md-4">
                        <b>Sub Total : PROVISIÓN (05)</b>
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtV_PO_PROVISION" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtV_POM_PROVISION" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <b>DESAJUSTES</b>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtV_PO_DESAJUSTE" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtV_POM_DESAJUSTE" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <b>COSTOS</b>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <br />
                        <b>Sub Total : PROVISIÓN (06)</b>
                    </div>
                    <div class="col-md-4">
                          <label>PO</label>
                         <asp:TextBox ID="txtC_PO_PROVISION" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                          <label>POM</label>
                        <asp:TextBox ID="txtC_POM_PROVISION" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <b>Sub Total : DIRECTOS (22)</b>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtC_PO_DIRECTO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtC_POM_DIRECTO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <b>Sub Total : INDIRECTOS (11)</b>
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtC_PO_INDIRECTO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                          <asp:TextBox ID="txtC_POM_INDIRECTO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <b>Sub Total : MATERIAL (33)</b>
                    </div>
                    <div class="col-md-4">
                          <asp:TextBox ID="txtC_PO_MATERIAL" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtC_POM_MATERIAL" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <b>Sub Total : SUBCONTRACTOS (44)</b>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtC_PO_SUBCONTRATO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox ID="txtC_POM_SUBCONTRATO" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <center>
<asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>
                        </center>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

