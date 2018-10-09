<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="PERMISOS_VARIOS.aspx.cs" Inherits="OPERACIONES_PERMISOS_VARIOS" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/ControlPermisos.ascx" TagPrefix="uc1" TagName="ControlPermisos" %>
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
    <style type="text/css">
        .PnlDesign
{
    border: solid 1px #000000;
    height: 300px;
    width: 320px;
    overflow-y:scroll;
    background-color: #EAEAEA;
    font-size: 12px;
    font-family: Arial;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
       <%-- <div class="col-md-3">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Solicitudes.180x90.fw.png"/>
        </div>--%>
      
        <div class="col-md-12">
            <center>
              <%--  <uc1:controlpermisos runat="server" ID="ControlPermisos" />--%>
            </center>

        </div>
    </div>
 

            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <b>SOLICITUD MDP</b><asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

             <div class="row">
                 <div class="col-md-3">
                 </div>

                 <div class="col-md-3">
                   
                          
                                  
                            <asp:CheckBox ID="chkEstados" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="chkEstados_CheckedChanged" Text="(Seleccionar todo)"  />
                            <asp:TextBox ID="txtEstados" Text="Personal" runat="server"></asp:TextBox>
                            <asp:Panel ID="PnlEstados" runat="server" CssClass="PnlDesign">
                                <asp:CheckBoxList ID="ddlPersonalAcargo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPersonalAcargo_SelectedIndexChanged" >
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <cc1:PopupControlExtender ID="PceSelectProyecto" runat="server" TargetControlID="txtEstados"
                                PopupControlID="PnlEstados" Position="Bottom">
                            </cc1:PopupControlExtender>
                
                          
                 </div>
                 <div class="col-md-3">
                     <label>Motivo</label>
                    <asp:DropDownList ID="ddlmotivo" runat="server" CssClass="ddl" ></asp:DropDownList>
                 </div>
                 <div class="col-md-3">
                 </div>
             </div>
            <div class="row">
                
                <div class="col-md-3">
                 </div>
                <div class="col-md-3">
                    <label>Del</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtInicio" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                            TargetControlID="txtInicio"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                        </span>
                    </div>
                </div>


                <div class="col-md-3">
                    <label>Hasta</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtfin" runat="server" class="form-control"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtfin"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfin" PopupButtonID="btnCalender2" Format="dd/MM/yyyy" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha termino" />
                        </span>
                    </div>
                </div>
                <div class="col-md-3">
                 </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                 </div>
                <div class="col-md-6">
                    <label>Comentarios</label>
                    <asp:TextBox ID="txtcomentarios" runat="server" Height="150px" MaxLength="850" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-3">
                 </div>
            </div>
            <br />
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblpersonal" runat="server" ></asp:Label>
        </div>
        
    </div>

            <div class="row">
                <div class="col-md-12">
                    <center>
                <asp:Button ID="btnEnviar" runat="server" Text="ENVIAR SOLICITUD" OnClick="btnEnviar_Click"></asp:Button>
            </center>
                <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEnviar"></cc1:ConfirmButtonExtender>
                <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEnviar"
                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                Mensaje
                </div>
                <div class="body" style="text-align:left; padding:12px; font-size:13px">
                ¿Deseas enviar solicitud(es)? <br />
                    <%=ListaPersonal %>
                </div>
                <div class="footer" align="right">
                <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
                </asp:Panel>
                </div>
                <br />
            </div>
    
</asp:Content>

