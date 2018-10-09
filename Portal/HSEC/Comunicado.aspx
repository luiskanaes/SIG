<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Comunicado.aspx.cs" Inherits="HSEC_Comunicado" %>
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
    <div class="row">
        <div class="panel panel-ICSK">
            <div class="panel-heading">
                <b>REGISTRO DE COMUNICADOS HSEC</b>
            </div>
            <div class="panel-body">


                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Tipo comunicado</label>
                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Fecha Inicio</label>
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
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha ingreso" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Fecha fin</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtfin" runat="server" CssClass="form-control"></asp:TextBox>
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

                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfin" PopupButtonID="ImageButton2" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha ingreso" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Visible</label>
                                <asp:DropDownList ID="ddlVisible" runat="server" CssClass="ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>URL</label>
                                <asp:TextBox ID="txturl" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Titulo</label>
                                <asp:TextBox ID="txttitulo" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Comentarios</label>
                                <asp:TextBox ID="txtcomentarios" runat="server" TextMode="MultiLine" Height="80px" MaxLength="850"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                             <div class="col-md-12">
                                 <label>Adjuntar archivo</label>
                                 <asp:FileUpload ID="FileUpload1" runat="server" />
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                            <br />
                             <asp:Button ID="btnCargar" runat="server" Text="Guardar" OnClick="btnCargar_Click" />
                                </div>
                        </div>
                    </div>




                <%--    segundo bloque--%>
                    <div class="col-md-8">
                        <div class="row">
                            <div class="col-md-3">
                                <label>Tipo comunicado</label>
                                <asp:DropDownList ID="ddlTipo2" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo2_SelectedIndexChanged"></asp:DropDownList>
                                <br />
                            </div>
                             <div class="col-md-9">
                                 </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_ANUNCIO" EmptyDataText="There are no data records to display." Font-Size="9pt" AllowPaging="True">
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="#" SortExpression="Row" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ARCHIVO_NOMBRE" HeaderText="ARCHIVO" SortExpression="ARCHIVO_NOMBRE" />
                                            <asp:BoundField DataField="FECHA_INICIO" HeaderText="INICIO" SortExpression="FECHA_INICIO" >
                                             <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_FIN" HeaderText="TERMINO" SortExpression="FECHA_FIN">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FLG_VISIBLE" HeaderText="VISIBLE" SortExpression="FLG_VISIBLE">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderText="EDITAR">
                                                 <ItemTemplate>
                                                     <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_ANUNCIO") %>' ImageUrl="~/imagenes/page_edit.ico" ToolTip="ver información" OnClick ="ver_datos"  CausesValidation="false"  />
                                                 </ItemTemplate>
                                                 <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FILE">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# Eval("DESCARGA") %>' Target="_blank"></asp:HyperLink>
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

            </div>
        </div>
    </div>
</asp:Content>

