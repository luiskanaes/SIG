<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="ReporteGlobal_Carga.aspx.cs" Inherits="Logistica_ReporteGlobal_Carga" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Styles/sky-forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 10%;
        }
    </style>
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <img alt="" src="../imagenes/loading3.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div  class="sky-form">
                <table style="width:100%" class="">
                    <tr>
                        <td style="width: 50px; text-align: center">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/registro.png" 
                        Width="48px" />
                        </td>
                        <td class="headerText">
                    CARGA DE REPORTE GLOBAL
                        </td>
                        <td style="width: 50px; text-align: center">
                            <asp:ImageButton ID="btnConsultar" runat="server" 
                        ImageUrl="~/imagenes/Consultar.png" Width="50px" 
                        ToolTip="Consultar CJI3"  
                        CausesValidation="False" onclick="btnConsultar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" align="center" colspan="3">
                            <hr />
                            <asp:Label ID="lblMensaje" runat="server" CssClass="EtiquetaNegrita"></asp:Label>
                            <br />
                        </td>
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
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%" align="center">
                            <asp:Button ID="btnProcesar" runat="server" 
                    Text="Procesar" validationgroup="Validar" onclick="btnProcesar_Click" 
                    Width="80%"/>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" align="right">
                            <asp:Label ID="Label11" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="13pt" Text="EMPRESA :"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:RadioButtonList ID="RdoEmpresa" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="12pt" RepeatDirection="Horizontal">
                                <asp:ListItem Value="SSK"></asp:ListItem>
                                <asp:ListItem Value="SKEX"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="25%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Elegir Empresa"
                        ControlToValidate="RdoEmpresa" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                        <td align="center" width="25%">
                &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%" align="right">
                            <asp:Label ID="Label1" runat="server" CssClass="EtiquetaNegrita" Text="ME5A :" 
                    Font-Size="13pt"></asp:Label>
                        </td>
                        <td width="25%" colspan="2">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img1" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload1" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="Label2" runat="server" CssClass="EtiquetaNegrita" Text="ME2N :" 
                    Font-Size="13pt"></asp:Label>
                        </td>
                        <td colspan="2" width="25%">
                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img2" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload2" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="Label3" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="13pt" Text="ZMM033 :"></asp:Label>
                        </td>
                        <td colspan="2" width="25%">
                            <asp:FileUpload ID="FileUpload3" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img3" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload3" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="Label4" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="13pt" Text="ZMM033 "></asp:Label>
                            <asp:Label ID="Label5" runat="server" CssClass="EtiquetaNegrita" 
                    Text="(Concluidas) :"></asp:Label>
                        </td>
                        <td colspan="2" width="25%">
                            <asp:FileUpload ID="FileUpload4" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img4" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload4" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="Label8" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="13pt" Text="ME80FN "></asp:Label>
                            <asp:Label ID="Label6" runat="server" CssClass="EtiquetaNegrita" 
                    Text="(Repartos) :"></asp:Label>
                        </td>
                        <td colspan="2" width="25%">
                            <asp:FileUpload ID="FileUpload5" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img5" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload5" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="Label9" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="13pt" Text="ME80FN "></asp:Label>
                            <asp:Label ID="Label7" runat="server" CssClass="EtiquetaNegrita" 
                    Text="(Historial) :"></asp:Label>
                        </td>
                        <td colspan="2" width="25%">
                            <asp:FileUpload ID="FileUpload6" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img6" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload6" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="right" width="25%">
                            <asp:Label ID="Label10" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="13pt" Text="Almacen :"></asp:Label>
                        </td>
                        <td colspan="2" width="25%">
                            <asp:FileUpload ID="FileUpload7" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img7" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload7" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right" width="25%">
                &nbsp;</td>
                        <td align="left" colspan="2" width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvwAsignacion" runat="server" CssClass="EtiquetaSimple" 
                    onrowdatabound="gvwAsignacion_RowDataBound">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                        <td width="25%">
                &nbsp;</td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProcesar" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        window.onsubmit = function () {
            if (Page_IsValid) {
                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, updateProgress.get_displayAfter());
            }
        }
    </script>
</asp:Content>





