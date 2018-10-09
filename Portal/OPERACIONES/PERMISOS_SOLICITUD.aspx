<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="PERMISOS_SOLICITUD.aspx.cs" Inherits="OPERACIONES_PERMISOS_SOLICITUD" EnableEventValidation="false" Culture ="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/ControlPermisos.ascx" TagPrefix="uc1" TagName="ControlPermisos" %>
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
        <%--<div class="col-md-3">
            <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/Solicitudes.180x90.fw.png" />
        </div>--%>
        
        <div class="col-md-12">
            <center>
               <%-- <uc1:controlpermisos runat="server" ID="ControlPermisos" />--%>
            </center>

        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <b>SOLICITUD MDP</b><asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
              <asp:Panel ID="Panel1" runat="server" CssClass="panelCuadro">
            <div class="row">
                
                 <div class="col-md-12">
                    <label>Motivo</label>
                    <asp:DropDownList ID="ddlmotivo" runat="server" CssClass="ddl" ></asp:DropDownList>
                </div>
                <div class="col-md-12">
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

                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="btnCalender1" Format="dd/MM/yyyy"  CssClass="cal_Theme1"/>
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                        </span>
                    </div>
                </div>
            </div>
             <div class="row">
                <div class="col-md-12">
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

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfin" PopupButtonID="btnCalender2" Format="dd/MM/yyyy" CssClass="cal_Theme1"/>
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha termino" />
                        </span>
                    </div>
                </div>
                
            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Comentarios</label>
                    <asp:TextBox ID="txtcomentarios" runat="server" Height="150px" MaxLength="850" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Documento (Descanso médico)</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
                <asp:Button ID="btnviar" runat="server" Text="ENVIAR SOLICITUD" OnClick="btnviar_Click"></asp:Button>
            </center>
                <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnviar"></cc1:ConfirmButtonExtender>
                <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnviar"
                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                Mensaje
                </div>
                <div class="body">
                ¿Deseas enviar solicitud?
                </div>
                <div class="footer" align="right">
                <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
                </asp:Panel>
                </div>
                <br />
            </div>
            <br />



</asp:Panel>
        </div>
        <%-- segundo cuadro--%>
        <div class="col-md-8">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <b>SOLICITUDES GENERADAS</b>
                        </div>
                    </div>
                </div>
            </div>
            
            
            <div class="row">
                <div class="col-md-4">
                    <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
               
                <div class="col-md-8">
                    <br />
                    Pendiente<asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/bol_Yellow_Ball.png" />
                    Aprobado<asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/bol_Green_Ball.png" />
                    Rechazado<asp:Image ID="Image4" runat="server" ImageUrl="~/imagenes/bol_Red_Ball.png" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False"
                        DataKeyNames="IDE_PERMISO" EmptyDataText="There are no data records to display." Font-Size="9pt"  >
                        <Columns>
                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <center>
                                         <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("COLOR") %>' />
                                    </center>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="TICKET" HeaderText="Ticket" SortExpression="TICKET" />


                            <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                            <asp:BoundField DataField="FECHAS" HeaderText="Fechas" SortExpression="FECHAS" />
                            <asp:TemplateField HeaderText="Actualizar">
                                <ItemTemplate>
                                    <center>
                                          <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_PERMISO") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="Editar solicitud" OnClick="ver_editar" CausesValidation="false" Visible='<%# Convert.ToBoolean(Eval("FLG_EDITAR") ) %>'/>
                                    </center>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Eliminar">
                                  <ItemTemplate>
                                    <center>
                                          <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_PERMISO") %>' ImageUrl="~/imagenes/Ico_delete.png" ToolTip="Eliminar solicitud" OnClick="eliminarpermiso" CausesValidation="false" Visible='<%# Convert.ToBoolean(Eval("FLG_EDITAR") ) %>'/>
                                    </center>
                                    <cc1:ConfirmButtonExtender ID="cbe2" runat="server" DisplayModalPopupID="mpe2" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                                    <cc1:ModalPopupExtender ID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="btnEliminar"
                                    OkControlID="btnYes2" CancelControlID="btnNo2" BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                    Mensaje
                                    </div>
                                    <div class="body">
                                    ¿Deseas eliminar registro?
                                    </div>
                                    <div class="footer" align="right">
                                    <asp:Button ID="btnYes2" runat="server" Text="Sí" CssClass="yes" />
                                    <asp:Button ID="btnNo2" runat="server" Text="No" CssClass="no" />
                                    </div>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Formato">
                                                <ItemTemplate>
                                                   <%-- <asp:ImageButton ID="btnMdp" runat="server" ImageUrl="~/imagenes/pdf_download.png" OnClick ="Formato" Visible='<%# (Convert.ToBoolean(Eval("MDP") )) %>'/>--%>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" Visible='<%# Convert.ToBoolean(Eval("FORMATO") ) %>' Font-Bold="True" NavigateUrl='<%# Eval("LINK") %>' Target="_blank" Enabled='<%# Convert.ToBoolean(Eval("BLOQUEAR") ) %>'  Text='<%# Eval("Descargar") %>'></asp:HyperLink>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="File">
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/MDP/"+Eval("FILE_DOC") %>' Visible ='<%# (Convert.ToBoolean(Eval("FILE_VISIBLE") )) %>' Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>




        </div>
   </div>
    

</asp:Content>

