<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPWeb.master" AutoEventWireup="true" CodeFile="reporteCostoManoObraSisplan.aspx.cs" Inherits="OPERACIONES_reporteCostoManoObraSisplan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
@media screen and (min-width: 600px) {
 #mobile-share {
 visibility: hidden;
 clear: both;
 float: left;
 margin: 10px auto 5px 20px;
 width: 28%;
 display: none;
 }
}
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
                if (!jsIsUserFriendlyChar(key, "NoDecimals")) {
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
			    title: 'Mensaje del Sistemas :',
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
    

    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>COSTO DE MANO DE OBRA SISPLAN </b>
            </div>
        </div>
    </div>
    <div id="mobile-share">
<a href="whatsapp://send?text=<%=WspUrl%>"
    data-action="share/whatsapp/share">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/WhatsApp_20x20.png" /></a>
</div>
    <div class="row">
        <div class="col-md-3">
            <label class="EtiquetaNegrita">
                                            Año</label><asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl" 
                                            AutoPostBack="True"  >
                                            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label class="EtiquetaNegrita">
                                            Mes</label>
                                            <asp:DropDownList ID="ddlMeses" runat="server" CssClass="ddl" 
                                                AutoPostBack="True">
                                            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label class="EtiquetaNegrita">
                                                Nro DE SEMANA</label>
                                                <asp:DropDownList ID="ddlSemana" runat="server" CssClass="ddl" 
                                            AutoPostBack="True" >
                                            </asp:DropDownList>
        </div>
        <div class="col-md-3">
           <label class="EtiquetaNegrita">
                                            Centro Costo</label>
                                            <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" 
                                                AutoPostBack="True">
                                            </asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            
         
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
            <br />
                <asp:ImageButton ID="btnBuscador" runat="server" 
                            ImageUrl="~/imagenes/boton.Excel.jpg" ToolTip="Consultar Personal" 
                          onclick="btnBuscador_Click" />
        </div>
    </div>


 
            <asp:GridView ID="gvExcel" runat="server" Visible="False">
            </asp:GridView>
</asp:Content>

