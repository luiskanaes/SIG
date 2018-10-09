<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="ReconocimientoAdjunto.aspx.cs" Inherits="RRHH_ReconocimientoAdjunto" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript">
    document.onselectstart = function () { return false; }
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
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <img alt="" src="../imagenes/loading3.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
<br />
    <div class="shadowBox">
        <div class="page-container">
            <div class="container">
                <div class="row">
                    <section class="col-md-3">
                    </section>
                    <section class="col-md-3">
                    </section>
                    <section class="col-md-3">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/boton.regresar.gif" OnClick="ImageButton1_Click" />
                    </section>
                    <section class="col-md-3">
                    </section>
                    
                </div>
                <br />
                <br />
                <div class="row">
                    <center>
                    <section class="col-md-4">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </section>
                    <section class="col-md-4">
                        <asp:Button ID="btnCarga" runat="server" Text="Cargar adjunto" OnClick="btnCarga_Click" />
                        <asp:Button ID="btnRegistro" runat="server" Text="Registrar solicitudes" OnClick="btnRegistro_Click" />
                    </section>
                    <section class="col-md-4">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/RRHH/Bravo.xlsx" ForeColor="Black">Descargar archivo</asp:HyperLink>
                    </section>
                        </center>
                </div>
                <br />
                <div class="row">

                    <div class="col-lg-12 ">
                      
                            <asp:GridView ID="GridView1" runat="server" CssClass="mGridAzul" Width="100%"></asp:GridView>

                       
                    </div>

                </div>
            </div>
        </div>
   </div>
  </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCarga" />
              <asp:PostBackTrigger ControlID="btnRegistro" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        window.onsubmit = function () {
            if (Page_IsValid) {
                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, updateProgress.get_displayAfter());
            }
        }
    </script>
</asp:Content>




