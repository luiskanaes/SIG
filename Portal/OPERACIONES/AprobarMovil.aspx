<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPWeb.master" AutoEventWireup="true" CodeFile="AprobarMovil.aspx.cs" Inherits="OPERACIONES_AprobarMovil" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
	  <style type="text/css">
	
	table { 
		width: 100%; 
		border-collapse: collapse; 
	}
	
	tr:nth-of-type(odd) { 
		background: #eee; 
	}
	th { 
		background: #195183; 
		color: white; 
		font-weight: bold; 
	}
	td, th { 
		padding: 6px; 
		border: 1px solid #ccc; 
		text-align: left; 
	}
	

	@media only screen and (max-width: 760px),
	(min-device-width: 768px) and (max-device-width: 1024px)  
    {
	
	
		table, thead, tbody, th, td, tr { 
			display: block; 
		}
		
		
		thead tr { 
			position: absolute;
			top: -9999px;
			left: -9999px;
		}
		
		tr { border: 1px solid #ccc; }
		
		td { 
			/* Behave  like a "row" */
			border: none;
			border-bottom: 1px solid #eee; 
			position: relative;
			padding-left: 50%; 
		}
		
		td:before { 
			/* Now like a table header */
			position: absolute;
			/* Top/left values mimic padding */
			top: 6px;
			left: 6px;
			width: 45%; 
			padding-right: 10px; 
			white-space: nowrap;
		}
		

		td:nth-of-type(1):before { content: "N°"; }
		td:nth-of-type(2):before { content: "Fecha Solicitud"; }
		td:nth-of-type(3):before { content: "Usuario"; }
		td:nth-of-type(4):before { content: "Equipo"; }
		td:nth-of-type(5):before { content: "Fecha Requerida"; }
		td:nth-of-type(6):before { content: "Tiempo (Meses)"; }
		td:nth-of-type(7):before { content: "Lugar Entrega"; }
		td:nth-of-type(8):before { content: "Creado Por"; }
        td:nth-of-type(9):before { content: "Aprobar"; }
	
	}
	

	@media only screen
	and (min-device-width : 320px)
	and (max-device-width : 480px) {
		body { 
			padding: 0; 
			margin: 0; 
			width: 320px; }
		}
	
	@media only screen and (min-device-width: 768px) and (max-device-width: 1024px) {
		body { 
			width: 495px; 
		}
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
    <div class="row">
    <div class="col-md-12">
        <section id="loginForm">
           <div class="form-horizontal">
            
               <h4><asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label></h4>
               
               <hr />
                
              <div id="page-wrap"  >
               
                           <table id="tbl_rol" style="width =100%" >
                                                    <thead>
                                                    
                                                        <tr align="center" style="background-color:#195183;color:White;text-align:center; font-family : Arial, sans-serif; font-size :13px">
                                                        <th style ="text-align :center">N°</th>
                                                        <th width="15%" style ="text-align :center">Fecha Solicitud</th>
                                                        <th width="40%" style ="text-align :center">Usuario</th>
                                                        <th width="8%" style ="text-align :center">Equipo</th>
                                                        <th width="10%" style ="text-align :center">Fecha Requerida</th>
                                                        <th width="10%" style ="text-align :center">Tiempo (Meses)</th>
                                                        <th width="10%" style ="text-align :center">Lugar Entrega</th>
                                                        <th width="20%" style ="text-align :center">Creado Por</th>
                                                        <th width="30%" style ="text-align :center">Aprobar</th>
                                                       
                                                    </tr>
                                                    </thead>
                                                <tbody>
                                                <asp:ListView runat="server" ID="lstRol" DataKeyNames="id_detalle" >
                                                <ItemTemplate>
                                                    <tr style="font-family : Arial, sans-serif; font-size :12px ;border:double ;border-width :1px; ">
                                                        <td>
                                                        <%#Eval("Row") %>
                                                        </td>
                                                        <td>
                                                             <%#Eval("FechaSolicitud") %>
                                                          
                                                        </td>
                                                        <td  style ="text-align :left">
                                                        <%#Eval("NombreSolicitante") %>
                                                        </td>
                                                        <td>
                                                        <%#Eval("TipoEquipo") %>
                                                        </td>
                                                        <td>
                                                        <%#Eval("FechaRequerida") %>
                                                        </td>
                                                        <td>
                                                        <%#Eval("MesesRequerido") %>
                                                        </td>
                                                        <td>
                                                        <%#Eval("LugarEntrega") %>
                                                        </td>
                                                        <td>
                                                             <%#Eval("USER_CREACION") %>
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:RadioButtonList ID="rdoOpcion" runat="server" >
                                                            <asp:ListItem>SI</asp:ListItem>
                                                            <asp:ListItem>NO</asp:ListItem>
                            
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                </asp:ListView>
                                                </tbody>
                                                </table>
                     
              
                </div>
              <div style="vertical-align: middle; text-align: center" >
                       
                            <asp:Button runat="server"  OnClick ="LogAprobar" Text="Aprobar Equipos" CssClass="btn btn-default" OnClientClick="return confirm('Aprobar equipos, desea continuar?')" ID="btnAprobar" Height="32px" />
                       
                    </div>
             </div>
           </section>
        </div>
            

   </div>
     </ContentTemplate>
            <Triggers>    
                <asp:AsyncPostBackTrigger  ControlID="btnAprobar"/>
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

