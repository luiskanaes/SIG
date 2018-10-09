<%@ Page Language="C#" MasterPageFile="~/Templates/MPWeb.master"  AutoEventWireup="true" CodeFile="frmLogGC.aspx.cs" Inherits="RRHH_SeguimientoReporte" %>
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
                   LOG DE GESTION DE CAMBIOS 
                    </td>


                             <td style="width: 50px; text-align: center">
                                 &nbsp;</td>

                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>

                
                     
            </tr>
            <tr>
                <td colspan="7" style="text-align: center">
                <hr /></td>
            </tr>
        </table>

        <table class="style1">
                <tr>
                    <td width="25%">
                     </td>
                    <td width="25%" align="center">
                     
                        
                        </td>
                    <td width="25%">
                    
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
              
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td align="center" width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                    <div style="overflow: auto;height: auto;width :1200px">
             
                 <asp:GridView ID="GridViewResultados" runat="server" AutoGenerateColumns="False" 
                            CssClass="mGridAzul"  >  
                             <RowStyle CssClass="GrillaRow" />                    
                            <Columns> 
                                <asp:BoundField DataField="PROYECTO" HeaderText="PROY " 
                                    SortExpression="PROYECTO" />
                                <asp:BoundField ItemStyle-Width = "550px"  HeaderStyle-Width="560px"   DataField="ORIGINADOR" HeaderText="ORIGINADOR" 
                                    SortExpression="ORIGINADOR">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="CODIGO" HeaderText="CODIGO" HeaderStyle-Width="380px"  
                                    SortExpression="CODIGO">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>    
                                  <asp:BoundField DataField="FECHA" HeaderText="FECHA" HeaderStyle-Width="100px"
                                    SortExpression="FECHA">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="TITULO_GCAMBIO" HeaderText="TITULO" HeaderStyle-Width="300px"
                                    SortExpression="TITULO_GCAMBIO" />
                                <asp:BoundField DataField="DESCRIPCION_GCAMBIO" HeaderText="DESCRIPCION" HeaderStyle-Width="400px"
                                    SortExpression="DESCRIPCION_GCAMBIO" />

                                      <asp:BoundField DataField="ID_TIPO_CAMBIO" HeaderText="TIPO CAMBIO" 
                                    SortExpression="ID_TIPO_CAMBIO" />

                                <asp:BoundField DataField="ID_DISCIPLINA" HeaderText="DISCIPLINA" 
                                    SortExpression="ID_DISCIPLINA" />

                                <asp:BoundField DataField="ID_IMPACTO" HeaderText="AREA" 
                                    SortExpression="ID_IMPACTO" />
                                  <asp:BoundField DataField="TIPO_MONEDA" HeaderText="T.MONEDA" 
                                    SortExpression="TIPO_MONEDA" />


                                        <asp:BoundField DataField="COSTO_MON" HeaderText="COSTO" 
                                    SortExpression="COSTO_MON" >
                                     <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="PLAZO" HeaderText="PLAZO (DIAS)" SortExpression="PLAZO">
                                  <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="HH" HeaderText="HORAS HOMBRE" SortExpression="HH">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            
                            </Columns>
                            
                        </asp:GridView>
               
                </div>
                </td>
                </tr>

                  <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%" align="center" colspan="2" style="width: 50%">
                     <table class="style1">
                            <tr>
                                <td align="center" width="50%">
                                     <asp:Button ID="btnConsulta" runat="server" 
                            Text="Consultar" Width="50%" />
                                </td>
                                <td align="center" width="50%">
                                   <asp:Button ID="btnDescarga" runat="server" Text="Descargar" 
                             Width="50%" onclick="btnDescarga_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>



                 <tr>
                    <td colspan="4">
                    <div style="overflow: auto;height: auto;width :1100px">
             
                 <asp:GridView ID="gvExcel" runat="server" AutoGenerateColumns="False" 
                             ondatabound="gvExcel_DataBound" Visible="False" 
                            onrowdatabound="gvExcel_RowDataBound"  >  
                       <%--      <RowStyle CssClass="GrillaRow" />  --%>                  
                            <Columns> 
                                <asp:BoundField DataField="PROYECTO" HeaderText="PROYECTO" 
                                    SortExpression="PROYECTO" />
                                <asp:BoundField ItemStyle-Width = "550px"  HeaderStyle-Width="550px"   DataField="ORIGINADOR" HeaderText="ORIGINADOR" 
                                    SortExpression="ORIGINADOR">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="CODIGO" HeaderText="CODIGO" HeaderStyle-Width="400px"  
                                    SortExpression="CODIGO">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>    
                                  <asp:BoundField DataField="FECHA" HeaderText="FECHA" HeaderStyle-Width="100px"
                                    SortExpression="FECHA">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                
                                <asp:BoundField DataField="TITULO_GCAMBIO" HeaderText="TITULO" 
                                    SortExpression="TITULO_GCAMBIO" />
                                <asp:BoundField DataField="DESCRIPCION_GCAMBIO" HeaderText="DESCRIPCION" 
                                    SortExpression="DESCRIPCION_GCAMBIO" />

                                      <asp:BoundField DataField="ID_TIPO_CAMBIO" HeaderText="TIPO CAMBIO" 
                                    SortExpression="ID_TIPO_CAMBIO" />

                                <asp:BoundField DataField="ID_DISCIPLINA" HeaderText="DISCIPLINA" 
                                    SortExpression="ID_DISCIPLINA" />

                                <asp:BoundField DataField="ID_IMPACTO" HeaderText="AREA" 
                                    SortExpression="ID_IMPACTO" />
                                <asp:BoundField DataField="TIPO_MONEDA" HeaderText="TIPO_MONEDA" 
                                    SortExpression="TIPO_MONEDA" />
                                        <asp:BoundField DataField="COSTO_MON" HeaderText="COSTO_MON" 
                                    SortExpression="COSTO_MON" />

                                <asp:BoundField DataField="PLAZO" HeaderText="PLAZO" SortExpression="PLAZO" />
                                <asp:BoundField DataField="HH" HeaderText="HORAS HOMBRE" SortExpression="HH">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            
                            </Columns>
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
    
