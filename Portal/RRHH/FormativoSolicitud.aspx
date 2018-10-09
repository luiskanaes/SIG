<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="FormativoSolicitud.aspx.cs" Inherits="RRHH_FormativoSolicitud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--   <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #82b623;
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
    </style>--%>
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
 
    <section>
        <div class="container-fluid">

            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <center>
                        <asp:Button ID="btnIniciar" runat="server" Text="Iniciar exámen" OnClick="btnIniciar_Click" />
                    </center>
                    
                </div>
            </div>
            <div class="row">
                <center>
                 <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/mensaje_formativo-03.fw.png"  class="img-responsive" />
                </center>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <center>
                    <asp:DropDownList runat="server" ID="ddlEspecialidad" CssClass="ddl"></asp:DropDownList>
                </center>
                </div>
                <div class="col-md-4">
                </div>
            </div>

            <br />
            <div class="row">

                <center><asp:Button runat="server" Text="Solicitar" ID="btnSolicitar" OnClick="btnSolicitar_Click" /> </asp:Button></center>
            </div>


        </div>
    </section>
    <br />
    <br />
    <br />


</ContentTemplate>
            <Triggers>    
                <%--<asp:AsyncPostBackTrigger  ControlID="btnIngreso"/>--%>
            </Triggers>
</asp:UpdatePanel>
 <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:modalpopupextender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>




