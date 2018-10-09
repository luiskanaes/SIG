<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="SeguimientoMOD.aspx.cs" Inherits="RRHH_SeguimientoMOD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    

<script type="text/javascript">

//    document.onselectstart = function () { return false; }
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
  .headerNew
  {
    font-weight:bold;
    position:absolute;
    color: #fff;   
    background: #4b6c9e;
     text-align:center;  border-radius: 2px;
     font-size: 9pt; font-weight:normal; font-family:Arial ;
  }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
     <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
    <br />
			
        <table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Usuarios.png" 
                        Width="50px" />
                </td>
                <td class="headerText">
                   REPORTE DE SEGUIMIENTO
                    </td>


                             <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnRequerimiento" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/requerimiento.png" 
                        ToolTip="Registro Requerimiento" Width="50px" Height="40px" onclick="btnRequerimiento_Click" 
                               />
                    </td>

                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnPersonal" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_5.png" 
                        onclick="btnPersonal_Click" ToolTip="Registro Postulante" Width="52px" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnControl" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_2.png" 
                        onclick="btnControl_Click" ToolTip="Control MOD" Width="55px" />
                </td>
                <td style="width: 50px; text-align: center">
                <asp:ImageButton ID="btnSeguimiento" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_4.png" 
                        onclick="btnSeguimiento_Click" ToolTip="Seguimiento MOD" Width="50px" /></td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnReportes" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_3.png" onclick="btnReportes_Click" 
                        ToolTip="Reporte MOI" Width="50px" Visible="False" />
                </td>

                
                     
            </tr>
            <tr>
                <td colspan="8" style="text-align: center">
                <hr /></td>
            </tr>
        </table>

        <table class="style1">
                <tr>
                    <td width="20%">
                    <label class="EtiquetaNegrita">Fecha inicio</label>
                                        <asp:TextBox ID="txtInicio" runat="server" Width="95%"></asp:TextBox>
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
                         
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   /></td>

                              <td width="20%">
                     <label class="EtiquetaNegrita">Fecha fin</label>
                                        <asp:TextBox ID="txtFin" runat="server" Width="95%"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                             TargetControlID="txtFin"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFin" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                   </td>

                    <td width="20%">
                     <%-- <label class="EtiquetaNegrita">Estados</label> --%>
                                   <asp:CheckBox ID="chkEstados" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkEstados_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />
                      <asp:TextBox ID="txtEstados" Text="ESTADOS" runat="server" 
                            Width="95%" ></asp:TextBox>
                <asp:Panel ID="PnlEstados" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="ddlEstados" runat="server">
                    </asp:CheckBoxList>
                </asp:Panel>
                <cc1:PopupControlExtender ID="PceSelectProyecto" runat="server" TargetControlID="txtEstados"
                    PopupControlID="PnlEstados" Position="Bottom">
                </cc1:PopupControlExtender>
                
                </td>
                    <td width="20%">
                 <%--  <label class="EtiquetaNegrita">Mano de Obra</label> --%>

                      <asp:CheckBox ID="chkManoObra" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkManoObra_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />

                          <asp:TextBox ID="txtTipoMano" Text="MANO DE OBRA" runat="server" 
                            Width="95%"></asp:TextBox>
                <asp:Panel ID="PnlMano" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="ddlMano" runat="server">
                    </asp:CheckBoxList>
                </asp:Panel>
                <cc1:PopupControlExtender ID="PceSelectManoObra" runat="server" TargetControlID="txtTipoMano"
                    PopupControlID="PnlMano" Position="Bottom">
                </cc1:PopupControlExtender></td>
           
                <td width="20%">
                <asp:CheckBox ID="chkAprobacion" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkAprobacion_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />

                         <asp:TextBox ID="txtAprobacion" Text="FECHA DE APROBACION" runat="server" 
                            Width="95%" ></asp:TextBox>
                <asp:Panel ID="pnlAprobacion" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="ddlAprobacion" runat="server">
                    </asp:CheckBoxList>
                </asp:Panel>

                <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtAprobacion"
                    PopupControlID="pnlAprobacion" Position="Bottom">
                </cc1:PopupControlExtender>
                </td>
           
                </tr>
                <tr>
                           <td width="20%">
  <%--               <label class="EtiquetaNegrita">Cargo</label> --%>
   <asp:CheckBox ID="chkCargo" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkCargo_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />
                          <asp:TextBox ID="txtCargo" Text="CARGO" runat="server" 
                            Width="95%"></asp:TextBox>
                <asp:Panel ID="PnlCargo" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="ddlCargo" runat="server">
                    </asp:CheckBoxList>
                </asp:Panel>
                 
                <cc1:PopupControlExtender ID="PceSelectCargo" runat="server" TargetControlID="txtCargo"
                    PopupControlID="PnlCargo" Position="Bottom">
                </cc1:PopupControlExtender></td>

                               <td width="20%">


                     <asp:CheckBox ID="chkEspecialidad" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkEspecialidad_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />
                        <asp:TextBox ID="txtEspecialidad" Text="ESPECIALIDAD" runat="server" 
                    CssClass="txtbox" ></asp:TextBox>
                        <asp:Panel ID="pnlEspecialidad" runat="server" CssClass="PnlDesign">
                            <asp:CheckBoxList ID="ddlEspecialidad" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <br />
                        <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txtEspecialidad"
                    PopupControlID="pnlEspecialidad" Position="Bottom">
                        </cc1:PopupControlExtender>
                       
                    </td>


                    <td width="20%"> 
                    <%-- <label class="EtiquetaNegrita">CENTRO DE COSTO</label> --%>
              <asp:CheckBox ID="chkTodosCECO" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkTodosCECO_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />
                    <asp:TextBox ID="txtCeco" Text="CENTRO DE COSTO" runat="server" 
                            Width="95%" ></asp:TextBox> 
                <asp:Panel ID="PnlCeco" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="ddlCecoB" runat="server" AutoPostBack="false">
                    </asp:CheckBoxList>
                </asp:Panel>
                <cc1:PopupControlExtender ID="PceSelectCeco" runat="server" TargetControlID="txtCeco"
                    PopupControlID="PnlCeco" Position="Bottom">
                </cc1:PopupControlExtender></td>
                    <td width="20%">
                 <%--    <label class="EtiquetaNegrita">Reclutador</label> --%>

                     <asp:CheckBox ID="chkReclutador" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkReclutador_CheckedChanged" Text="(Seleccionar todo)" 
                            Font-Size="Smaller" />

                         <asp:TextBox ID="txtAnalista" Text="RECLUTADOR" runat="server" 
                            Width="95%" ></asp:TextBox>
                <asp:Panel ID="PnlAnalista" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="ddlAnalista" runat="server">
                    </asp:CheckBoxList>
                </asp:Panel>

                <cc1:PopupControlExtender ID="PceSelectAnalista" runat="server" TargetControlID="txtAnalista"
                    PopupControlID="PnlAnalista" Position="Bottom">
                </cc1:PopupControlExtender>
                </td>
                   <%-- <td width="25%">
                        &nbsp;</td>--%>

                       

                           <td width="20%">
                               &nbsp;</td>

                       

                </tr>
                <tr>
                    <td width="20%">
                     </td>
                    <td width="20%" align="center">
                     
                        <asp:Button ID="btnConsulta" runat="server" CssClass="buttonVerde" 
                            onclick="btnConsulta_Click" Text="Consultar" Width="60%" /></td>
                    <td width="20%" align="center">
                     </td>
                    <td width="20%" align="center">
                      <asp:Button ID="btnDescarga" runat="server" Text="Descargar" 
                            CssClass="buttonVerde"  Width="60%" onclick="btnDescarga_Click" /></td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%" align="center" colspan="2" style="width: 50%">
                     <table class="style1">
                            <tr>
                                <td align="center" width="50%">
                                     
                                </td>
                                <td align="center" width="50%">
                                  
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" >
                <div style="overflow: auto;height: auto;width :1100px">
             
                 <asp:GridView ID="GridViewResultados" runat="server" AutoGenerateColumns="False" 
                            CssClass="mGridAzul"  >  
                             <RowStyle CssClass="GrillaRow" />                    
                            <Columns>
                            <asp:BoundField ItemStyle-Width = "150px" DataField="Row" HeaderText="Nro" SortExpression="Row" HeaderStyle-Width="150px"   />
                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" 
                                    SortExpression="EMPRESA" />
                                <asp:BoundField ItemStyle-Width = "550px"  HeaderStyle-Width="550px"   DataField="DES_NOMBRE" HeaderText="Apellidos y Nombres" 
                                    SortExpression="DES_NOMBRE">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="DES_DNI" HeaderText="Dni" 
                                    SortExpression="DES_DNI">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>    
                                  <asp:BoundField DataField="DES_TELEFONO" HeaderText="Teléfono" 
                                    SortExpression="DES_TELEFONO">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="PROYECTO" HeaderText="Proyecto" 
                                    SortExpression="PROYECTO" />
                                <asp:BoundField DataField="CENTROCOSTO" HeaderText="Centro de Costo" 
                                    SortExpression="CENTROCOSTO" />
                                      <asp:BoundField DataField="DES_REQUERIMIENTO" HeaderText="Req." 
                                    SortExpression="DES_REQUERIMIENTO" />
                                <asp:BoundField DataField="DES_ITEM" HeaderText="Item" 
                                    SortExpression="DES_ITEM" />
                                <asp:BoundField DataField="TIP_MANO" HeaderText="Tipo mano de Obra" 
                                    SortExpression="TIP_MANO" />
                                <asp:BoundField DataField="UBICACION" HeaderText="Ubicación" 
                                    SortExpression="UBICACION" />
                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                              <%--  <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>

                                  <asp:BoundField DataField="ESPECIALIDAD" HeaderText="Especialidad" SortExpression="ESPECIALIDAD">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FUENTE" HeaderText="Fuente" 
                                    SortExpression="FUENTE" />
                                <asp:BoundField DataField="TIPO_PROCESO" HeaderText="Tipo Proceso" 
                                    SortExpression="TIPO_PROCESO" />
                                <asp:BoundField DataField="ORIGEN_POSICION" HeaderText="Origen Posición" 
                                    SortExpression="ORIGEN_POSICION" />
                                <asp:BoundField DataField="RESPONSABLE" HeaderText="Responsable" 
                                    SortExpression="RESPONSABLE" />
                              
                                <asp:BoundField DataField="ESTADO_APROBACION" HeaderText="Estado Fecha Aprobación" 
                                    SortExpression="ESTADO_APROBACION" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado de Proceso" 
                                    SortExpression="ESTADO" > 
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                   
                                      <asp:BoundField DataField="FEC_REGISTRO" HeaderText="Fecha de Registro" 
                                    SortExpression="FEC_REGISTRO" />
                                <asp:BoundField DataField="FEC_FECHA_APROBACION" HeaderText="Fecha de Aprobación" 
                                    SortExpression="FEC_FECHA_APROBACION" />
                                     <asp:BoundField DataField="FEC_FECHA_INGRESO" HeaderText="Fecha de Ingreso" 
                                    SortExpression="FEC_FECHA_INGRESO" />
                                <asp:BoundField DataField="DIAS_EN_PROCESO" HeaderText="Días en Proceso" 
                                    SortExpression="DIAS_EN_PROCESO" />
                                <asp:BoundField DataField="TIEMPO_ATENCION" HeaderText="Tiempo Atención" 
                                    SortExpression="TIEMPO_ATENCION" />                               
                                <asp:BoundField DataField="FEC_FECHA_EXAMEN_MED" HeaderText="Exámen Médico" 
                                    SortExpression="FEC_FECHA_EXAMEN_MED" />
                                <asp:BoundField DataField="FEC_FECHA_VIAJE" HeaderText="Fecha de Viaje" 
                                    SortExpression="FEC_FECHA_VIAJE" />
                                <asp:BoundField DataField="FEC_FECHA_FEEDBACK" HeaderText="FeedBack" 
                                    SortExpression="FEC_FECHA_FEEDBACK" />
                                <asp:BoundField DataField="DES_COMENTARIOS" HeaderText="Comentarios" 
                                    SortExpression="DES_COMENTARIOS" />
                                <%--<asp:BoundField DataField="TIEMPO_RESPUESTA_RESPONSABLE" 
                                    HeaderText="Respuesta Responsable" 
                                    SortExpression="TIEMPO_RESPUESTA_RESPONSABLE" />--%>
                            </Columns>
                            
                        </asp:GridView>
               
                </div>
                </td>
                    
                </tr>



                 <tr>
                    <td colspan="5">
                <div style="overflow: auto;height: auto;width :1100px">
             
                 <asp:GridView ID="gvExcel" runat="server" AutoGenerateColumns="False" 
                             ondatabound="gvExcel_DataBound" Visible="False" 
                            onrowdatabound="gvExcel_RowDataBound"  >  
                       <%--      <RowStyle CssClass="GrillaRow" />  --%>                  
                            <Columns>
                          
                            <asp:BoundField ItemStyle-Width = "150px" DataField="Row" HeaderText="Nro" SortExpression="Row" HeaderStyle-Width="150px"   />
                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" 
                                    SortExpression="EMPRESA" />
                                <asp:BoundField ItemStyle-Width = "550px"  HeaderStyle-Width="550px"   DataField="DES_NOMBRE" HeaderText="Apellidos y Nombres" 
                                    SortExpression="DES_NOMBRE">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="DES_DNI" HeaderText="Dni" 
                                    SortExpression="DES_DNI">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>    
                                  <asp:BoundField DataField="DES_TELEFONO" HeaderText="Teléfono" 
                                    SortExpression="DES_TELEFONO">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="PROYECTO" HeaderText="Proyecto" 
                                    SortExpression="PROYECTO" />
                                <asp:BoundField DataField="CENTROCOSTO" HeaderText="Centro de Costo" 
                                    SortExpression="CENTROCOSTO" />
                                      <asp:BoundField DataField="DES_REQUERIMIENTO" HeaderText="Req." 
                                    SortExpression="DES_REQUERIMIENTO" />
                                <asp:BoundField DataField="DES_ITEM" HeaderText="Item" 
                                    SortExpression="DES_ITEM" />
                                <asp:BoundField DataField="TIP_MANO" HeaderText="Tipo mano de Obra" 
                                    SortExpression="TIP_MANO" />
                                <asp:BoundField DataField="UBICACION" HeaderText="Ubicación" 
                                    SortExpression="UBICACION" />
                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                              <%--  <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>

                                  <asp:BoundField DataField="ESPECIALIDAD" HeaderText="Especialidad" SortExpression="ESPECIALIDAD">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FUENTE" HeaderText="Fuente" 
                                    SortExpression="FUENTE" />
                                <asp:BoundField DataField="TIPO_PROCESO" HeaderText="Tipo Proceso" 
                                    SortExpression="TIPO_PROCESO" />
                                <asp:BoundField DataField="ORIGEN_POSICION" HeaderText="Origen Posición" 
                                    SortExpression="ORIGEN_POSICION" />
                                <asp:BoundField DataField="RESPONSABLE" HeaderText="Responsable" 
                                    SortExpression="RESPONSABLE" />
                              
                                <asp:BoundField DataField="ESTADO_APROBACION" HeaderText="Estado Fecha Aprobación" 
                                    SortExpression="ESTADO_APROBACION" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado de Proceso" 
                                    SortExpression="ESTADO" > 
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                   
                                      <asp:BoundField DataField="FEC_REGISTRO" HeaderText="Fecha de Registro" 
                                    SortExpression="FEC_REGISTRO" />
                                <asp:BoundField DataField="FEC_FECHA_APROBACION" HeaderText="Fecha de Aprobación" 
                                    SortExpression="FEC_FECHA_APROBACION" />
                                     <asp:BoundField DataField="FEC_FECHA_INGRESO" HeaderText="Fecha de Ingreso" 
                                    SortExpression="FEC_FECHA_INGRESO" />
                                <asp:BoundField DataField="DIAS_EN_PROCESO" HeaderText="Días en Proceso" 
                                    SortExpression="DIAS_EN_PROCESO" />
                                <asp:BoundField DataField="TIEMPO_ATENCION" HeaderText="Tiempo Atención" 
                                    SortExpression="TIEMPO_ATENCION" />                               
                                <asp:BoundField DataField="FEC_FECHA_EXAMEN_MED" HeaderText="Exámen Médico" 
                                    SortExpression="FEC_FECHA_EXAMEN_MED" />
                                <asp:BoundField DataField="FEC_FECHA_VIAJE" HeaderText="Fecha de Viaje" 
                                    SortExpression="FEC_FECHA_VIAJE" />
                                <asp:BoundField DataField="FEC_FECHA_FEEDBACK" HeaderText="FeedBack" 
                                    SortExpression="FEC_FECHA_FEEDBACK" />
                                <asp:BoundField DataField="DES_COMENTARIOS" HeaderText="Comentarios" 
                                    SortExpression="DES_COMENTARIOS" />  </Columns>
                            
                        </asp:GridView>
               
                </div>
                </td>
                    
                </tr>



            </table>
        
         
      
 </ContentTemplate>
            <Triggers>    
            </Triggers>
</asp:UpdatePanel>

 <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="../imagenes/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>
    
