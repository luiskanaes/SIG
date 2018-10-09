<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesempenioObjetivosVarios.aspx.cs" Inherits="RRHH_DesempenioObjetivosVarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #EEAA00;
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
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Desempenio180x90.fw.png" OnClick="ImageButton1_Click" />
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>

    </div>
        <div class="row">
         <div class="col-md-12">
            
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>FIJACIÓN DE OBJETIVOS TRANSVERSALES</b>
                    </div>
                </div>
            </div>
          
       

            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
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
                </div>
                
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <label>Objetivo</label>
                    <asp:TextBox ID="txtObjetivos" runat="server" Height="120px" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                </div>

                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <label>Indicador</label>
                    <asp:TextBox ID="txtIndicador" runat="server" MaxLength="100"></asp:TextBox>
                </div>
            
                <div class="col-md-3">
                    <label>Peso (%)</label>
                    <asp:TextBox ID="txtPeso" runat="server" onkeydown="return jsDecimals(event);" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <label>Fecha inico</label>
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

                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                        </span>
                    </div>
                </div>

                <div class="col-md-3">
                    <label>Fecha Termino</label>
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

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfin" PopupButtonID="btnCalender2" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha termino" />
                        </span>
                    </div>
                </div>
                <div class="col-md-3">
                </div>
            </div>
     
            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
                    <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click"></asp:Button>
                    <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>

                    <cc1:ConfirmButtonExtender ID="cbe1x" runat="server" DisplayModalPopupID="mpe1x" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpe1x" runat="server" PopupControlID="pnlPopup1x" TargetControlID="btnGuardar"
                    OkControlID="btnYes1x" CancelControlID="btnNo1x" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup1x" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                    Mensaje
                    </div>
                  <div class="body" style="text-align:left; padding:12px; font-size:13px">
                    ¿Deseas registar objetivos?<br />
                    <%=ListaPersonal %>
                    </div>
                    <div class="footer" align="right">
                    <asp:Button ID="btnYes1x" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo1x" runat="server" Text="No" CssClass="no" />
                    </div>
                    </asp:Panel>
            </center>
                </div>
                <br />
            </div>
   
            <%--  FIN SEGUNDO BLOQUE--%>
        </div>
    </div>

</asp:Content>

