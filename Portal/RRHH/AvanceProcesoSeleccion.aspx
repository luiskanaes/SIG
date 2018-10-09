<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPWeb.master" AutoEventWireup="true" CodeFile="AvanceProcesoSeleccion.aspx.cs" Inherits="RRHH_AvanceProcesoSeleccion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
    document.onselectstart = function () { return false; }
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
<table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center" width="10%">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas4.png" 
                        Width="50px" />
                </td>
                <td class="headerText" width="55%">
                    &nbsp;PROCESO SELECCION
                </td>
                <td  align="left" width="40%">
                <script>
                    var mydate = new Date();
                    var year = mydate.getYear();
                    if (year < 1000)
                        year += 1900;
                    var day = mydate.getDay();
                    var month = mydate.getMonth();
                    var daym = mydate.getDate();
                    if (daym < 10)
                        daym = "0" + daym;

                    var dayarray = new Array("Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "S&aacute;bado");
                    var montharray = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                    document.write("<small><font color='#000000' face='Arial' size='1'><b>" + " Lima - Perú, " + dayarray[day] + " " + daym + " de " + montharray[month] + " de " + year + "</b></font></small>");
			
			</script>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
    <table class="style1">
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
         <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" align="left">
            <label class="EtiquetaNegrita"> Fecha inicio </label>
                                        <asp:TextBox ID="txtInicio" runat="server" Width="95%" required></asp:TextBox>
                                        <cc1:maskededitextender ID="MaskedEditExtender5" runat="server"
                             TargetControlID="txtInicio"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:calendarextender ID="CalendarExtender1" runat="server" 
                    TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"  CssClass="cal_Theme1" />
            </td>
            <td width="20%" align="left">
              <label class="EtiquetaNegrita"> Fecha fin </label>
                                        <asp:TextBox ID="txtFin" runat="server" Width="95%" required></asp:TextBox>
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
                         
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFin" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" CssClass="cal_Theme1"  />
            </td>
            <td width="20%" align="center">
                <asp:Button ID="btnConsultar" runat="server" CssClass="buttonNaranja" 
                            Text="Consulta" Width="70%" onclick="btnConsultar_Click" />
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="5">
             <div style="overflow: scroll; width: 100%;  text-align: center">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                </rsweb:ReportViewer>
                </div> 
            </td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

