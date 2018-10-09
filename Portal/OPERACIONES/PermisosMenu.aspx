<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="PermisosMenu.aspx.cs" Inherits="OPERACIONES_PermisosMenu" EnableEventValidation="false" %>

<%@ Register Src="~/Controles/ControlPermisos.ascx" TagPrefix="uc1" TagName="ControlPermisos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <div class="row">
        <br /><br />
        
        <div class="col-md-12">
            <center>
                <%--<uc1:ControlPermisos runat="server" ID="ControlPermisos" />--%>
            </center>
            
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <center>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/registro solicitudes-03.png" CssClass="img-responsive"></asp:Image>
            </center>
        </div>

    </div>

    
</asp:Content>

