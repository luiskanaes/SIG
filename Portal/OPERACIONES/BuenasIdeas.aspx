<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPPrincipal.master" AutoEventWireup="true" CodeFile="BuenasIdeas.aspx.cs" Inherits="OPERACIONES_BuenasIdeas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #d79b01;
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
    <div class="row">

        <br /><br />
        <div class="col-md-8">
            <center>
                <asp:Image ID="Image1" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/Buenasideas.550x220.fw.png"/>
            </center>
             <br /> <br /> <br />
             <center>
          
                     <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" ForeColor="#EC9600" NavigateUrl="~/OPERACIONES/BuenasIdeasRegistro.aspx" Font-Size="12pt">>> Ingresar <<</asp:HyperLink>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/FondoBlanco.png" Width="65px"></asp:Image>
                      <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" ForeColor="#EC9600" NavigateUrl="~/OPERACIONES/BuenasIdeasBases.aspx" Font-Size="12pt">>> Ver bases <<</asp:HyperLink>
                
            </center>
                    <%--<asp:Button ID="btnBases" runat="server" Text="VER BASES" OnClick="btnBases_Click"></asp:Button>
                    <asp:Button ID="btnIngresar" runat="server" Text="INGRESAR" OnClick="btnIngresar_Click"></asp:Button>--%>
        </div>
        <div class="col-md-4">
            <br /><br /><br /><br /><br /><br />
               <center>
             <asp:Label ID="Label2" runat="server" Text="«Si hubiera preguntado a la gente qué querían, me habrían dicho que un caballo más rápido»" Font-Bold="True" Font-Size="12pt" Font-Italic="True" ForeColor="#EC9600"></asp:Label>
               </center>
                   <br /><br />
            <asp:Label ID="Label1" runat="server" Text="- Henry Ford -" Font-Bold="True" Font-Size="12pt" Font-Italic="True" ForeColor="#EC9600"></asp:Label>
        </div>
      
    </div>
       <%-- <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-6">
                <center>
                    <asp:Label ID="Label1" runat="server" Text="Buscamos promover la generación de ideas innovadoras que mejoren la eficiencia de SSK." Font-Size="12pt"></asp:Label>
                </center>
            </div>
            <div class="col-md-3">
            </div>
        </div>--%>
        <br />
      
    <br />
    <br />
</asp:Content>

