<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="ReconocerTalento.aspx.cs" Inherits="RRHH_ReconocerTalento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #57227a;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
    <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
        <br /><br />
        <section>
        <div class="container-fluid">
            <div class="row">
                <center>
                 <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Bravo.png"  class="img-responsive" />
                </center>
            </div> 
            <br /><br />
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-3">
                    <br />
                    <center><asp:Button runat="server" Text="¡Quiero reconocer!" ID="btnReconocer" OnClick="btnReconocer_Click" ></asp:Button></center>
                </div>
                <div class="col-md-3">
                    <br />
                    <center><asp:Button runat="server" Text="¡He sido reconocido!" ID="btnPremio" OnClick="btnPremio_Click" ></asp:Button></center>
                </div>
                <div class="col-md-3"></div>
            </div> 
            <br /><br />  <br />
                    <div class="row">
                        <center>
                            Para mayor información, escríbenos a: <a href ="mailto:desarrollodeltalento@ssk.com.pe">desarrollodeltalento@ssk.com.pe</a>
                        </center>
                        </div>   
        </div>
        </section>
        <br /><br /><br />
       

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


