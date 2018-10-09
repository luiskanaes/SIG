<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="BuenasIdeasBases.aspx.cs" Inherits="OPERACIONES_BuenasIdeasBases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          document.onselectstart = function () { return false; }
       </script>
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
    <br />
    <div class="row">
        <div class="col-md-12">
           
                <asp:Image ID="Image1" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/Buenasideas.320x120.fw.png"/>
          
        </div>
    
    </div>
    <br />
    <div class="row">
       
        <div class="col-md-6">
            <center>
                <asp:Image ID="Image2" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/BuenasIdeasBases1.fw.png"/>
            </center>
            <br />
        </div>
        <div class="col-md-6">
            <center>
                <asp:Image ID="Image3" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/BuenasIdeasBases2.fw.png"/>
            </center>
            <br />
        </div>
    
          <%--    <p>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/pto.png" /> <b>BREVE DESCRIPCION:</b> La idea de esta iniciativa es promover la generación de ideas innovadoras que mejoren la eficiencia de SSK.
            </p>
            <p>
               <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/pto.png" /><b>CONCEPTO:</b>  &#8220;SSK premia tus buenas ideas&#8221;.
            </p>
            <p>
               <asp:Image ID="Image4" runat="server" ImageUrl="~/imagenes/pto.png" /> <b>ALCANCE:</b> Describir una acción de mejora, buena práctica o similar que sea realizable en la organización.
            </p>
            
            <p>
                <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/pto.png" /><b>COMPETENCIAS ASOCIADAS:</b> Eficiencia en Ejecución e Innovación.
            </p>
            <p>
                <asp:Image ID="Image6" runat="server" ImageUrl="~/imagenes/pto.png" /><b>PARTICIPANTES:</b> Aplica para todo el personal de SSK (oficinas y proyectos).
            </p>
            <p>
                <asp:Image ID="Image7" runat="server" ImageUrl="~/imagenes/pto.png" /><b>CRITERIOS DE EVALUACIÓN:</b>La propuesta debe ser Realizable, Simple e Innovadora.
            </p>
            <p>
                <asp:Image ID="Image8" runat="server" ImageUrl="~/imagenes/pto.png" /><b> PREMIOS:</b> 100 puntos bravo para buenas ideas, 200 puntos bravo para muy buenas ideas, 400 puntos bravo para ideas sobresalientes.
            </p>
            --%>

        
        
    </div>
    <br />
    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-6">
            <center>
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" ForeColor="#EC9600" NavigateUrl="~/OPERACIONES/BuenasIdeas.aspx" Font-Size="15pt">>> Regresar <<</asp:HyperLink>
<%--                    <asp:Button ID="BtnRegresar" runat="server" Text="REGRESAR" OnClick="BtnRegresar_Click"></asp:Button>--%>
                </center>
        </div>
        <div class="col-md-3">
        </div>
    </div>

  
</asp:Content>

