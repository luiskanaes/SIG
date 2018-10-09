<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="TarifarEquipos.aspx.cs" Inherits="CAREMENOR_TarifarEquipos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .panelCuadro {
            margin-bottom: 20px;
            padding: 10px;
            background-color: #F7DC6F;
            border: 1px solid transparent;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
        }

        a img {
            border: none;
        }

        ol li {
            list-style: decimal outside;
        }

        div#container {
            margin: 0 auto;
            padding: 1em 0;
        }

        div.side-by-side {
            width: 100%;
            margin-bottom: 1em;
        }

            div.side-by-side > div {
                float: left;
                width: 100%;
            }

                div.side-by-side > div > em {
                    margin-bottom: 10px;
                    display: block;
                }

        .clearfix:after {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
            width: 100%;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
            width: 100%;
            /* support: IE7 */
            *height: 2.0em;
            *top: 0.1em;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 0.5em;
            width: 100%;
        }

        /*Demo fix*/
        .custom-combobox a {
            height: 35px;
            margin-top: -6px;
            visibility: hidden;
        }

        div.DialogueBackground {
            position: absolute;
            width: 98%;
            height: 100%;
            top: 0;
            left: 0;
            background-color: #777;
            opacity: 0.5;
            filter: alpha(opacity=50);
            text-align: center;
        }

        .btn-file {
            position: relative;
            overflow: hidden;
        }

            .btn-file input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                min-width: 100%;
                min-height: 100%;
                text-align: right;
                filter: alpha(opacity=0);
                opacity: 0;
                outline: none;
                background: white;
                cursor: inherit;
                display: block;
            }
    </style>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>REGISTRO DE TARIFAS</b>
            </div>
        </div>
    </div>
    <div class="row">
            <div class="col-md-6">
                 
                 <label>FECHA</label>
                <div class="input-group">
                    <asp:TextBox ID="txtInicio" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
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
            <div class="col-md-6">
                <label>TARIFA </label>
                 <asp:TextBox ID="txtPrecio" runat="server"  onkeydown="return jsDecimals(event);"></asp:TextBox>
            </div>
            
           

        </div>

    <div class="row">
            <br />
            <div class="col-md-12">
                <center>
                    
                 <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnSave_Click" />
                    <cc1:ConfirmButtonExtender ID="cbeP1" runat="server" DisplayModalPopupID="mpeP1" TargetControlID="btnSave"></cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpeP1" runat="server" PopupControlID="pnlPopupP1" TargetControlID="btnSave"
                    OkControlID="btnYesP1" CancelControlID="btnNoP1" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopupP1" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                    Mensaje
                    </div>
                    <div class="body">
                    ¿Deseas guardar registros?
                    </div>
                    <div class="footer" align="right">
                    <asp:Button ID="btnYesP1" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNoP1" runat="server" Text="No" CssClass="no" />
                    </div>
                    </asp:Panel>
                </center>
                
            </div>
        </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered table-condensed" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="row" HeaderText="#" SortExpression="row" />
                    <asp:BoundField DataField="V_FECHA_INICIO_VAL" HeaderText="Fecha Inico" SortExpression="V_FECHA_INICIO_VAL" />
                    <asp:BoundField DataField="V_FECHA_FIN_VAL" HeaderText="Fecha Termino" SortExpression="V_FECHA_FIN_VAL" />
                    <asp:BoundField DataField="V_TARIFA_DIA" HeaderText="Precio" SortExpression="V_TARIFA_DIA" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

