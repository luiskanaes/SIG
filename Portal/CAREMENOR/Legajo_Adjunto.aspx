<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="Legajo_Adjunto.aspx.cs" Inherits="CAREMENOR_Legajo_Adjunto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style >
        .panelCuadro {
  margin-bottom: 20px;
  padding :10px;
  background-color: #F7DC6F;
  border: 1px solid transparent;
  border-radius: 4px;
  -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
          box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
}
                  a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 100%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
        .custom-combobox {
            position: relative;
            display: inline-block;
            width :100%;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
            width :100%;
            
            /* support: IE7 */
            *height: 2.0em;
            *top: 0.1em;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 0.5em;
           width :100%;
            
        }

        /*Demo fix*/
        .custom-combobox a {
            height: 35px;
            margin-top: -6px;
            visibility: hidden;
        }
            div.DialogueBackground 
        { 
            position:absolute; 
            width:98%; 
            height:100%; 
            top:0; 
            left:0; 
            background-color:#777; 
            opacity:0.5;
            filter: alpha(opacity=50); 
            text-align:center; 
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>Envio de Legajo (Arriendo)</b>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <asp:Panel ID="Panel1" runat="server" CssClass="panelCuadro">

                        <div class="row">
                            <div class="col-md-3">
                                <label>Fecha Legajo</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFechaLegajo" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                        TargetControlID="txtFechaLegajo"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />

                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaLegajo" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Cargar archivo</label>
                                <div class="input-group">
                                    <label class="input-group-btn">
                                        <span class="btn btn-primary">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </span>
                                    </label>

                                </div>
                            </div>
                            <div class="col-md-3">
                                
                                    <br />
                                    <asp:Button ID="btnCargar" runat="server" Text="Cargar" OnClick="btnCargar_Click" Visible="False" />
                            
                            </div>
                        </div>
                           </asp:Panel>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                           

                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="ide_LegajoFile,FILE_ARCHIVO" EmptyDataText="There are no data records to display." Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True">
                                            <Columns>

                                                <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />





                                                <asp:BoundField DataField="FILE_NOMBRE" HeaderText="File" SortExpression="FILE_NOMBRE" />
                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha" SortExpression="fecha_registro" />





                                                <asp:TemplateField HeaderText="File">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("FILE_ARCHIVO") %>' Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("ide_LegajoFile") %>' ImageUrl="~/imagenes/delete.png" OnClick="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>


                                    </div>

                               
                            </div>

                        </div>

                 
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCargar" />
            <asp:PostBackTrigger ControlID="btnCargar" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>






