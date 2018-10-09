<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="SolicitudAprobacion.aspx.cs" Inherits="RRHH_SolicitudAprobacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
	  <style type="text/css">
	
	
	
	</style>
        <script type="text/javascript">
    function doAlert( mensaje)
        {
            var msg = new DOMAlert(
            {
                title: 'Mensaje del Sistema',
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
    <asp:HiddenField ID="hdcodigo" runat="server" />
    <asp:HiddenField ID="hdPersonal" runat="server" />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">REQUERIMIENTO DE PERSONAL MOI</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Dni (Personal nuevo)</label>
                        <asp:TextBox ID="txtDni" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Apellido paterno</label>
                        <asp:TextBox ID="txtPaterno" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Apellido materno</label>
                        <asp:TextBox ID="txtMaterno" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Nombres</label>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Cargo</label>
                        <asp:DropDownList ID="ddlCargos" runat="server" CssClass="ddl" Enabled="False">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <br />
                        <label>Ficha de solicitud</label>

                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" ForeColor="#FF3300" Visible="False">[HyperLink1]</asp:HyperLink>
                    </div>
                    <div class="col-md-3">
                        <label>Inicio Contrado</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtinicio" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                        TargetControlID="txtinicio"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />

                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtinicio" PopupButtonID="ImageButton1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha ingreso" Enabled="False" />
                                    </span>
                                </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Empresa</label>
                        <asp:DropDownList ID="ddlEmpresas" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                    </div>

                    <div class="col-md-3">

                        <label>Gerencia</label>
                        <asp:DropDownList ID="ddlGerencia" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerencia_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">

                        <label>Centro</label>

                        <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" Enabled="False"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>Area</label>
                        <asp:TextBox ID="txtarea" runat="server" MaxLength="100"></asp:TextBox>
                    </div>

                </div>
                
                <div class="row">
                    <div class="col-md-3">
                        <label>Formación academica</label>
                        <asp:DropDownList ID="ddlNivelAcademico" runat="server" CssClass="ddl" Enabled="False"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>Especialización</label>
                        <asp:DropDownList ID="ddlcarrera" runat="server" CssClass="ddl" Enabled="False"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>N° Colegiatura</label>
                        <asp:TextBox ID="txtColegiatura" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Habilitado</label>
                        <asp:RadioButtonList ID="RdoColegiatura" runat="server" RepeatDirection="Horizontal" Enabled="False">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
    </div>

  
   
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">COMENTARIOS GENERALES</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox ID="txtComentarioGnral" runat="server" TextMode="MultiLine" Height="80px" MaxLength="850"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

       <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_ASIGNACION,IDE_RECURSOS" EmptyDataText="There are no data records to display." Font-Size="9pt" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound">
                                        <AlternatingRowStyle Font-Size="10pt" />
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="#" SortExpression="Row" >
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DES_ASUNTO" HeaderText="Descripcion" SortExpression="DES_ASUNTO" />
                                            <asp:BoundField DataField="FEC_APROBADO" HeaderText="Fec.Aprobacion" SortExpression="FEC_APROBADO" />
                                            <asp:TemplateField HeaderText="A: Aprobado R: rechazado">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal"  Enabled='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>' >
                                                      
                                                        <asp:ListItem Value="1">A</asp:ListItem>
                                                        <asp:ListItem Value="0">R</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="lblEstado" runat="server" Visible="false" Text='<%# Eval("FLG_APROBADO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Procesar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnProcesar" runat="server" CommandArgument='<%# Eval("IDE_RECURSOS") %>' ImageUrl="~/imagenes/save16x16.png" ToolTip="Procesar requerimiento" OnClick ="ProcesarReconocimiento"  Visible='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>'/>
                                                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnProcesar">
                                                        </cc1:ConfirmButtonExtender>
                                                        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnProcesar"
                                                            OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                                        </cc1:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                                Mensaje 
                                                            </div>
                                                            <div class="body">
                                                                ¿Deseas procesar solicitud?
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                                                <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                                            </div>
                                                        </asp:Panel>
                                                
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
                        </div>
    <div class="row">
        <div class="col-md-12">
            
        </div>
    </div>
</asp:Content>

