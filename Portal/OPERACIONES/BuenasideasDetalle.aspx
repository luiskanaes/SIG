<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="BuenasideasDetalle.aspx.cs" Inherits="OPERACIONES_BuenasideasDetalle" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #d79b01;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }

        input[type="submit"]:hover {
            outline: thin dotted #333;
            outline: 5px auto -webkit-focus-ring-color;
            outline-offset: -2px;
        }
        .boton{
             padding: 5px 15px;
            background: #d79b01;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }
    </style>
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
       <div class="row">
        <div class="col-md-4">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Buenasideas.180x90.fw.png" />
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <br />
            <center>
            <asp:Button ID="btnRegresar" runat="server" Text="REGRESAR" OnClick="btnRegresar_Click"></asp:Button>

            </center>
            
        </div>
    </div>
    <br />
        <div class="row">

        <div class="col-lg-12 ">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDE_IDEAS">
                <ItemTemplate>


                    <ul class="timeline">
                        <li class="timeline-inverted">
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">
                                       
                                        
                                        <p><%# Eval("TITULO")%>
                                            <small class="text-muted"><i class="fa fa-time"></i><%# Eval("REGISTRO_FECHA")%></small>
                                             <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/Buenasideas/"+Eval("FILE_DOC") %>' Visible ='<%# (Convert.ToBoolean(Eval("FILE_VISIBLE") )) %>' ></asp:HyperLink>
                                        </p>
                                    </h4>
                                    
                                </div>
                                <div class="timeline-body">
                                    <div class="row">
                                        <div class="col-md-6" style="text-align: justify;">
                                                 <b>1.- Objetivo (Planteamiento de la meta o propósito a alcanzar)</b><br />
                                               <%# Eval("DESCRIPCION_PROPUESTA")%>
                                            <br /><br />
                                           <b>2.- Solución planteada</b><br />
                                            <%# Eval("SOLUCION")%> 
                                        
                                        </div>
                                        <div class="col-md-6" style="text-align: justify;">
                                           
                                            
                                            <b>3.- Ventejas</b><br />
                                            <%# Eval("VENTAJAS")%>
                                            <br />
                                            <br />
                                            <b>4.- Áreas involucradas en la propuesta de mejora</b><br />
                                            <%# Eval("AREAS")%>
                                            <br />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
