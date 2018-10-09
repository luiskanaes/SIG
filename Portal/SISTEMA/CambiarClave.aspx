<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="CambiarClave.aspx.cs" Inherits="SISTEMA_CambiarClave" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
     <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
    <br />
       <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>ACTUALIZAR CONTRASEÑA</b><asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
               
        </div>
        </div>
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
             <label class="EtiquetaNegrita">Actualizar Contraseña</label>
                                <asp:TextBox ID="txtPass1" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
        </div>
        <div class="col-md-4">
        </div>
    </div>
      <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
              <label class="EtiquetaNegrita">Confirmar Contraseña</label>
                                <asp:TextBox ID="txtPass2" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
        </div>
        <div class="col-md-4">
        </div>
    </div>
      <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <br />
            <center>
                   <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" onclick="btnActualizar_Click" />
            </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    	
    <br /><br /><br /><br />

       
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
    




