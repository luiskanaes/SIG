<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPPortal.master" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="Principal" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
        /************************************************************************************
    STRUCTURE
    *************************************************************************************/
        #pagewrap {
            padding: 5px;
            width: 100%;
            margin: 20px auto;
        }

        #content {
            width: 60%;
            float: left;
            padding: 10px;
        }

        #sidebar {
            width: 20%;
            float: right;
            padding: 10px;
        }

        #sidebarCGO {
            width: 20%;
            float: left;
            padding: 10px;
        }

        #footer {
            clear: both;
        }


        /* border & guideline (you can ignore these) */
        #content {
        }

        #sidebar {
        }

        #header, #content, #sidebar {
            margin-bottom: 5px;
        }

        #content, #sidebar {
            border: solid 0px #ccc;
            border-radius: 5px;
        }

        .avatar {
            /* cambia estos dos valores para definir el tamaño de tu círculo */
            height: 100px;
            width: 100px;
            /* los siguientes valores son independientes del tamaño del círculo */
            background-repeat: no-repeat;
            background-position: 50%;
            border-radius: 50%;
            background-size: 100% auto;
        }


        input[type="submit"] {
            padding: 5px 15px;
            background: #fff;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #000;
            text-transform: uppercase;
        }

            input[type="submit"]:hover {
                outline: thin dotted #333;
                outline: 5px auto -webkit-focus-ring-color;
                outline-offset: -2px;
            }

        .box {
            width: 40%;
            margin: 0 auto;
            background: rgba(255,255,255,0.2);
            padding: 35px;
            border: 2px solid #fff;
            border-radius: 5px;
            background-clip: padding-box;
            text-align: center;
        }



        .overlay {
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0, 0, 0, 0.7);
            transition: opacity 500ms;
            visibility: hidden;
            opacity: 0;
        }

            .overlay:target {
                visibility: visible;
                opacity: 1;
                z-index: 999;
            }

        .popup {
            margin: 70px auto;
            padding: 10px;
            background: #fff;
            border-radius: 2px;
            width: 30%;
            position: relative;
        }

            .popup h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popup .close {
                position: absolute;
                top: 20px;
                right: 30px;
                font-size: 30px;
                color: #DA070E;
                opacity: 1;
            }

                .popup .close:hover {
                    color: #DA070E;
                }

            .popup .content {
                overflow: auto;
            }

    

        
        /************************************************************************************
    MEDIA QUERIES
    *************************************************************************************/

      

        /* for 980px or less */
        @media screen and (max-width: 980px) {

            #pagewrap {
                width: 94%;
            }

            #content {
                width: 60%;
            }

            #sidebar {
                width: 20%;
            }

            #sidebarCGO {
                width: 20%;
            }

        }

        /* for 700px or less */
        @media screen and (max-width: 700px) {

            #content {
                width: auto;
                float: none;
            }

            #sidebar {
                width: auto;
                float: none;
            }

            #sidebarCGO {
                width: auto;
                float: none;
            }

             .box {
                width: 70%;
            }

            .popup {
                width: 100%;
            }

            
        }

        /* for 480px or less */
        @media screen and (max-width: 480px) {

            #header {
                height: auto;
            }

            h1 {
                font-size: 24px;
            }

            #sidebar {
                height: auto;
            }

            #sidebarCGO {
                height: auto;
            }

           
        }
    </style>
    <script type="text/javascript">
        function doAlert(mensaje) {
            var msg = new DOMAlert(
            {
                title: 'Mensaje ICSK',
                text: '<br/>' + mensaje,
                skin: 'default',
                width: 300,
                height: 80,
                ok: { value: true, text: 'Aceptar', onclick: showValue },

            });
            msg.show();
        };

        function showValue(sender, value) {
            sender.close();

        }

        function OpenPopup() {

            window.location='<%= ResolveUrl("~/Principal#popup1") %>';
        }

        function OpenPopup_url(url) {

            window.open(url);
        }
    </script>
       <%-- <script type="text/javascript">

        snow_img = "http://app.ssk.com.pe/SIGSSK/imagenes/nieve.png";


        snow_no = 15;

        if (typeof (window.pageYOffset) == "number") {
            snow_browser_width = window.innerWidth;
            snow_browser_height = window.innerHeight;
        }
        else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
            snow_browser_width = document.body.offsetWidth;
            snow_browser_height = document.body.offsetHeight;
        }
        else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
            snow_browser_width = document.documentElement.offsetWidth;
            snow_browser_height = document.documentElement.offsetHeight;
        }
        else {
            snow_browser_width = 500;
            snow_browser_height = 500;
        }

        snow_dx = [];
        snow_xp = [];
        snow_yp = [];
        snow_am = [];
        snow_stx = [];
        snow_sty = [];

        for (i = 0; i < snow_no; i++) {
            snow_dx[i] = 0;
            snow_xp[i] = Math.random() * (snow_browser_width - 50);
            snow_yp[i] = Math.random() * snow_browser_height;
            snow_am[i] = Math.random() * 20;
            snow_stx[i] = 0.02 + Math.random() / 10;
            snow_sty[i] = 0.7 + Math.random();
            if (i > 0) document.write("<\div id=\"snow_flake" + i + "\" style=\"position:absolute;z-index:" + i + "\"><\img src=\"" + snow_img + "\" border=\"0\"><\/div>"); else document.write("<\div id=\"snow_flake0\" style=\"position:absolute;z-index:0\"><a href=\"http://peters1.dk/tools/snow.php\" target=\"_blank\"><\img src=\"" + snow_img + "\" border=\"0\"></a><\/div>");
        }

        function SnowStart() {
            for (i = 0; i < snow_no; i++) {
                snow_yp[i] += snow_sty[i];
                if (snow_yp[i] > snow_browser_height - 50) {
                    snow_xp[i] = Math.random() * (snow_browser_width - snow_am[i] - 30);
                    snow_yp[i] = 0;
                    snow_stx[i] = 0.02 + Math.random() / 10;
                    snow_sty[i] = 0.7 + Math.random();
                }
                snow_dx[i] += snow_stx[i];
                document.getElementById("snow_flake" + i).style.top = snow_yp[i] + "px";
                document.getElementById("snow_flake" + i).style.left = snow_xp[i] + snow_am[i] * Math.sin(snow_dx[i]) + "px";
            }
            snow_time = setTimeout("SnowStart()", 10);
        }
        SnowStart();
    </script>--%>


    <%--<a class="button" href="#popup1">¿CÓMO RECONOCER?</a>--%>

    <asp:HiddenField ID="hdUrl" runat="server" />
    <div id="popup1" class="overlay">
        <div class="popup">
            
            <a class="close" href="#">
                <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/imagenes/cerrar.png" OnClick="btnCerrar_Click"  ToolTip="Cerrar comunicado" /></a>
            <div class="content">
                <asp:ImageButton ID="imgPopup" runat="server"  OnClick="VerUrl"   CssClass="img-responsive"/>
            <%--    <asp:HyperLink ID="imgPopup" runat="server" CssClass="img-responsive"></asp:HyperLink>--%>
            </div>
        </div>
    </div>


    <div class="row">

       


        <div id="sidebarCGO">

            <div class="panel panel-default">
                <div class="panel-heading">
                     <asp:Image ID="Image8" runat="server" ImageUrl="~/imagenes/CarnetBlack.fw.png" />
                    <b><asp:Label runat="server" id="lblCcentro" Font-Bold="True"></asp:Label></b>
                </div>
                <div class="panel-body">
                    <div class="col-md-12" >
                        <center>
                            <asp:Image ID="imgFoto" runat="server"   ImageUrl="~/imagenes/Foto_Fondo.png"  CssClass="avatar"   />
                              <%-- <br /><asp:Label runat="server" id="lblNombre"  ></asp:Label>--%>
                           
                            
                    </center>
                    </div>
                </div>
            </div>



            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/Personal.png" />
                    <b>CUMPLEAÑOS </b>
                </div>
                <div class="panel-body">
                    <marquee direction="down" scrolldelay="200" height="270">
                            <asp:DataList ID="GridCumple" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row">
                            <ItemTemplate>
                            <div class="col-lg-12">
                            
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Torta.png" />
                            <asp:Label ID="lblPersonal" runat="server" Text='<%# Eval("NOMBRE") %>' CssClass="EtiquetaSimple"></asp:Label>
                            </div>
                            </ItemTemplate>
                            </asp:DataList></marquee>
                </div>
            </div>
        </div>

        <div id="content">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/imagenes/PencilBlack.png" />
                    <b>PORTAL ICSK </b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDE_OPCIONES">
                                <ItemTemplate>
                                    <div class="col-lg-4">
                                        <div class="panel panel-ICSK">
                                            <div class="panel-body">
                                                <center> 
                                        <asp:ImageButton ID="btnEnlace" runat="server" CommandArgument='<%# Eval("URL") %>' ImageUrl='<%# Eval("IMAGEN") %>'  CssClass="img-responsive" OnClick ="VerEnlace" />
                                        </center>
                                            </div>
                                        </div>
                                    </div>


                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Image ID="Image10" runat="server" ImageUrl="~/imagenes/HSEC32x32.fw.png" />
                    <b>HSEC - POLÍTICA DE SALUD, SEGURIDAD, MEDIOAMBIENTE Y COMUNIDAD</b>
                </div>
                <div class="panel-body">
                    <div class="panel with-nav-tabs panel-default" >
                        <div id="Tabs" role="tabpanel" class="panel-heading">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist" >
                                <li style="padding:5px"><asp:Button ID="btn1" runat="server" Text="TOOL BOX" OnClick="btn1_Click" /></li>
                                <li style="padding:5px"><asp:Button ID="btn2" runat="server" Text="COMUNICADOS" OnClick="btn2_Click" /></li>
                                <li style="padding:5px"><asp:Button ID="btn3" runat="server" Text="COMITÉ SST" OnClick="btn3_Click" /></li>
                                <li style="padding:5px"><asp:Button ID="btn4" runat="server" Text="BRIGADA DE EMERGENCIAS" OnClick="btn4_Click" /></li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content" style="padding-top: 5px; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; background: #ffffff;">
                                <div role="tabpanel" class="tab-pane active" id="personal">
                                    <%--<marquee direction="down" scrolldelay="400" height="120">--%>
                                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row">
                                        <ItemTemplate>
                                        <div class="col-lg-12">
                            
                                        <asp:Image ID="Image9" runat="server" ImageUrl="~/imagenes/Casco_amarrillo.png" />
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("DESCARGA") %>' Target="_blank" Text='<%# Eval("ARCHIVO_NOMBRE") %>'></asp:HyperLink>

                                        <asp:Label ID="lblfecha" runat="server" Text='<%# Eval("FECHA_INICIO") %>' CssClass="errorMessage"></asp:Label>
                                        </div>
                                        </ItemTemplate>
                                        </asp:DataList>

                                    <%--/marquee>--%>
                                </div>
                                <%--<div role="tabpanel" class="tab-pane" id="employment">
                                    This is Employment Information Tab
                                </div>--%>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/home_phone.png" />
                        <b>DIRECTORIO CORPORATIVO</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label>CONSULTAR COLABORADOR</label>
                                <div class="input-group">
                                    <asp:TextBox ID="tb_Buscar" runat="server"></asp:TextBox>

                                    <span class="input-group-addon">

                                        <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Imagenes/search32.png" OnClick="btnBuscar_Click" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-md-12">
                                <asp:Panel ID="PanelDirectorio" runat="server" class="panel panel-default" Style="max-height: 200px; overflow-y: scroll;" Visible="false">
                                    <div class="panel-body">
                                        <asp:DataList ID="GridBuscar" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-md-4" style="padding-right: 10px;">
                                                        <center>
                                            <asp:Image ID="Image1" runat="server"  ImageUrl='<%# Eval("FOTO") %>'  CssClass="avatar" 
                                            onerror="this.src='http://reboot.pro/public/style_images/metro/profile/default_large.png';" />
                                            </center>
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 20px;">
                                                        <br />
                                                        <b>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Trabajador")%>'></asp:Label></b>
                                                        <br />

                                                        <b>CELULAR</b>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Celular")%>'></asp:Label>
                                                        <br />

                                                        <b>ANEXO:</b>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Anexo")%>'></asp:Label>
                                                        <br />
                                                        <b>CORREO:</b>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("CORREO")%>'></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </asp:Panel>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            
            
            

            
          
        </div>

        <div id="sidebar">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/imagenes/IcoFTP.png" />FTP SIG </b>
                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://app.ssk.com.pe/FILE_SIG/" Target="_blank" Font-Underline="True" ForeColor="Red"> (Acceder)</asp:HyperLink>
                </div>

            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>
                        <asp:Image ID="Image7" runat="server" ImageUrl="~/imagenes/IcoMundo.png" />ENLACES ICSK </b>

                </div>
                <div class="panel-body">
                    <ul>
                        <li>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/OPERACIONES/reporteCostoManoObraSisplan.aspx" Target="_blank" Font-Underline="True" ForeColor="Red"> CMO SISPLAN</asp:HyperLink></li>
                    </ul>
       
                    
<%--                        <div id="Tabs" role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active"><a href="#personal" aria-controls="personal" role="tab" data-toggle="tab">Personal</a></li>
                                <li><a href="#employment" aria-controls="employment" role="tab" data-toggle="tab">Employment</a></li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content" style="padding-top: 20px">
                                <div role="tabpanel" class="tab-pane active" id="personal">
                                    This is Personal Information Tab
                                </div>
                                <div role="tabpanel" class="tab-pane" id="employment">
                                    This is Employment Information Tab
                                </div>
                            </div>
                        </div>
                    

                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" VerticalStripWidth="" Width="100%" CssClass="nav nav-tabs">
                        <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1" CssClass="active">
                        </cc1:TabPanel>
                         <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2">
                        </cc1:TabPanel>
                    </cc1:TabContainer>--%>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/aitxposes_02_290px.fw.png" CssClass="img-responsive" />
                    <br /><br /> <br /><br /><br />
                </div>
            </div>



        </div>
    </div>
    <br />

</asp:Content>


