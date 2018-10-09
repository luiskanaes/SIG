<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesempenioEtapas.aspx.cs" Inherits="RRHH_DesempenioEtapas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">

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
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Desempenio180x90.fw.png" PostBackUrl="~/RRHH/DesempenioBandeja.aspx"  />
        </div>
        
        <div class="col-md-3">
            
     
        </div>
         <div class="col-md-3">
             <br />
            <asp:Button ID="btnGenerar" runat="server" Text="GENERAR ETAPAS" OnClick="btnGenerar_Click" Visible="False" />
         </div>
         <div class="col-md-2">
               </div>
    </div>
    <div class="row">
        
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>REGISTRO DE ETAPAS</b>
                    </div>
                </div>
                    </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                
                <div class="col-md-12">
                    <label>Proyecto</label>
                    <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged"></asp:DropDownList>

                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:CheckBox ID="chkEstados" runat="server" AutoPostBack="True"
                        OnCheckedChanged="chkEstados_CheckedChanged" Text="(Seleccionar todo)" />
                    <asp:TextBox ID="txtEstados" Text="Etapas" runat="server"></asp:TextBox>
                    <asp:Panel ID="PnlEstados" runat="server" CssClass="PnlDesign">
                        <asp:CheckBoxList ID="ddlestapas" runat="server" AutoPostBack="True" >
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <cc1:PopupControlExtender ID="PceSelectProyecto" runat="server" TargetControlID="txtEstados"
                        PopupControlID="PnlEstados" Position="Bottom">
                    </cc1:PopupControlExtender>
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
            <br />
        </div>
        
      <%--  FIN BLOQUE UNO--%>
        <div class="col-md-8">
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>LISTAR ETAPAS</b>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Proyecto</label>
                    <asp:DropDownList ID="ddlcecos" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcecos_SelectedIndexChanged"></asp:DropDownList>

                </div>
                <div class="col-md-6">
                </div>
                
            </div>
            <br />
             <div class="row">
                 <div class="col-lg-12 ">
                     <div class="table-responsive">
                         <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IDE_ETAPAS" EmptyDataText="There are no data records to display." Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                             <Columns>
                                 <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                         <center>
                                         <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("COLOR") %>' />
                                    </center>

                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="DESCRIPCION" HeaderText="ETAPA" SortExpression="DESCRIPCION">

                                     <HeaderStyle Height="30px" />
                                 </asp:BoundField>

                                 <asp:TemplateField HeaderText="Estado" Visible="False">
                                     <ItemTemplate>
                                         <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Inicio">
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtInicio" runat="server" Text='<%# Eval("INICIO") %>'></asp:TextBox>
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

                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Fin">
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtfin" runat="server" Text='<%# Eval("FIN") %>'></asp:TextBox>
                                         <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                             TargetControlID="txtfin"
                                             Mask="99/99/9999"
                                             MessageValidatorTip="true"
                                             OnFocusCssClass="MaskedEditFocus"
                                             OnInvalidCssClass="MaskedEditError"
                                             MaskType="Date"
                                             DisplayMoney="Left"
                                             AcceptNegative="Left"
                                             ErrorTooltipEnabled="True" />

                                         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfin" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Estado">
                                     <ItemTemplate>
                                         <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal" Width="100%">
                                             <asp:ListItem>CERRADO</asp:ListItem>
                                             <asp:ListItem>ABIERTO</asp:ListItem>
                                         </asp:RadioButtonList>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Guardar">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="btnProcesar" runat="server" CommandArgument='<%# Eval("IDE_ETAPAS") %>' ImageUrl="~/imagenes/SaveIcono.png" OnClick="Procesar" />


                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>


                             <HeaderStyle BackColor="#f1f1f1" Font-Bold="True" ForeColor="Black" />


                         </asp:GridView>


                     </div>
                 </div>
             </div>
        </div>
        <%--  FIN BLOQUE DOS--%>
    </div>

</asp:Content>

