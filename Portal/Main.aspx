<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPPrincipal.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript">
       
            $(document).ready(function () {
                $('#myCarousel1').carousel({
                    interval: 9000
                })
                $('.fdi-Carousel .item').each(function () {
                    var next = $(this).next();
                    if (!next.length) {
                        next = $(this).siblings(':first');
                    }
                    next.children(':first-child').clone().appendTo($(this));

                    if (next.next().length > 0) {
                        next.next().children(':first-child').clone().appendTo($(this));
                    }
                    else {
                        $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
                    }
                });
            });

          
            function carrusel() {
                $('#myCarousel1').carousel({
                    interval: 9000
                })
                $('.fdi-Carousel .item').each(function () {
                    var next = $(this).next();
                    if (!next.length) {
                        next = $(this).siblings(':first');
                    }
                    next.children(':first-child').clone().appendTo($(this));

                    if (next.next().length > 0) {
                        next.next().children(':first-child').clone().appendTo($(this));
                    }
                    else {
                        $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
                    }
                });

                
            };
        
            function carruselBanner() {
                
              
            };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <style>
        .carousel-inner > .item > img,
        .carousel-inner > .item > a > img {
            /*width: 50%;
            margin: auto;
            height: 50%;*/
        }

        .scrollbar {
            height: 300px;
            background: #F5F5F5;
            overflow-y: scroll;
            margin-bottom: 25px;
        }

        .carousel-inner.onebyone-carosel {
            margin: auto;
            width: 90%;
        }

        .onebyone-carosel .active.left {
            left: -33.33%;
        }

        .onebyone-carosel .active.right {
            left: 33.33%;
        }

        .onebyone-carosel .next {
            left: 33.33%;
        }

        .onebyone-carosel .prev {
            left: -33.33%;
        }
    </style>

    <div class="row">
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner" role="listbox">
                <!-- Wrapper for slides -->
                <asp:Repeater ID="Rgallary" runat="server">
                    <ItemTemplate>
                        <div class="item <%# (Container.ItemIndex == 0 ? "active" : "") %>">
                            <asp:Image ID="imgId" runat="server" ImageUrl='<%#Eval("FilePath")%>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <!-- Left and right controls -->
                <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>
    </div>
    <br />
        


    <div class="row">
     

            <div class="well">
                <div id="myCarousel1" class="carousel fdi-Carousel slide">
                    <!-- Carousel items -->

                    <div class="carousel fdi-Carousel slide" id="eventCarousel" data-interval="0">
                        <div class="carousel-inner onebyone-carosel">
                            <asp:Repeater ID="images" runat="server">
                                <ItemTemplate>

                                    <div class="item <%# (Container.ItemIndex == 0 ? "active" : "") %>">
                                        <div class="col-md-4">
                                            <asp:Image ID="imgId2" runat="server" ImageUrl='<%#Eval("FilePath")%>' />
                                        </div>

                                    </div>
                                </ItemTemplate>


                            </asp:Repeater>
                        </div>

                        <a class="left carousel-control" href="#eventCarousel" data-slide="prev"></a>
                        <a class="right carousel-control" href="#eventCarousel" data-slide="next"></a>
                    </div>

                    <!--/carousel-inner-->
                </div>
                <!--/myCarousel-->
            </div>
    </div>
  



        <br />
    <div class="row">
        <div class="col-lg-4">
            <!--Pill Tabs   -->
            <div class="panel panel-default" style="height: 450px">
                <div class="panel-heading">
                    COMUNICACIONES
                </div>
                <div class="panel-body">
                    <ul class="nav nav-pills">
                        <li class="active">
                          
                            <asp:Button ID="Button4" runat="server" Text="ANIVERSARIOS" OnClick="Button4_Click" />
                        </li>
                        <li>
                            <asp:Button ID="Button5" runat="server" Text="CUMPLEAÑOS" OnClick="Button5_Click" />
                           
                        </li>

                    </ul>
                    <br />
                    <div class="tab-content">
                        <div class="scrollbar" id="style-default">
                            <div class="tab-pane fade in active" id="home-pills">
                                <br />
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-4">

                                                <asp:Image ID="imgFoto" runat="server" ImageUrl="~/imagenes/Foto_Fondo100x100.png" />

                                            </div>
                                            <div class="col-md-8" style="font-size: 8pt">
                                                <%#Eval("NOMBRE_COMPLETO") %>
                                                <br />
                                                <%#Eval("DATO") %>
                                                <br />
                                                <%#Eval("DESC_CECOS") %>
                                                <br />
                                            </div>

                                        </div>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <!--End Pill Tabs   -->
        </div>
        <div class="col-lg-4">
            <!--Pill Tabs   -->
            <div class="panel panel-default" style="height: 450px">
                <div class="panel-heading">
                    EVENTOS Y NOTICIAS
                </div>
                <div class="panel-body">
                    <ul class="nav nav-pills">
                        <li class="active">
                            <asp:Button ID="Button1" runat="server" Text="NUEVOS INGRESOS" OnClick="Button1_Click" />
                           
                        </li>
                        <li>
                           
                            <asp:Button ID="Button2" runat="server" Text="PROMOCIONES" OnClick="Button2_Click" />
                        </li>

                    </ul>
                    <br />
                    <div class="tab-content">
                        <div class="scrollbar" id="style-default">
                            <div class="tab-pane fade in active" id="home-pills">
                                <br />
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-4" >
                                                
                                                    <asp:Image ID="imgFoto" runat="server" ImageUrl="~/imagenes/Foto_Fondo100x100.png" />
                                                
                                            </div>
                                            <div class="col-md-8" style ="font-size:8pt">
                                                 <%#Eval("NOMBRE_COMPLETO") %> <br />
                                                <%#Eval("DESCRIPCION_NUEVO") %> <br />
                                                <%#Eval("DESC_CECOS") %> <br />
                                            </div>
                                            
                                        </div>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater> 
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <!--End Pill Tabs   -->
        </div>
        <div class="col-lg-4">
            <!--Pill Tabs   -->
            <div class="panel panel-default" style="height: 450px">
                <div class="panel-heading">
                    DESCARGAS
                </div>
                <div class="panel-body">
                    <ul class="nav nav-pills">
                        <li class="active">
                            <asp:Button ID="Button3" runat="server" Text="ARCHIVOS" OnClick="Button3_Click" />
                        </li>
                    </ul>
                    <br />
                    <div class="tab-content">
                        <div class="scrollbar" id="style-default">
                            <div class="tab-pane fade in active" id="home-pills">

                                <asp:DataList ID="dlCustomers" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row" OnItemDataBound="dlCustomers_ItemDataBound">
                                    <ItemTemplate>
                                        
                                        <div class="col-sm-12">
                                            <!--THUMBNAIL#2-->
                                          <%--  <div class="panel-body">--%>

                                                <b>
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION_ADICIONAL")%>'></asp:Label>

                                                    <hr />
                                                </b>


                                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IDE_BANNER" CssClass="EtiquetaSimple" GridLines="None" ShowHeader="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" ReadOnly="True" SortExpression="DESCRIPCION" ShowHeader="false" />
                                                        <asp:TemplateField HeaderText="Ver equipo"  ShowHeader="false">
                                                            <ItemTemplate>
                                                                <center>
                                                               
                                                                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# Eval("FilePath") %>'  Target="_blank"></asp:HyperLink>
                                                                </center>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        </asp:TemplateField>


                                                        


                                                    </Columns>
                                                </asp:GridView>
                                            <br />
                                           <%-- </div>--%>
                                        </div>
                                        
                                    </ItemTemplate>
                                </asp:DataList>

                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <!--End Pill Tabs   -->
        </div>
    </div>

    <br />
    <div class="row">
        <center>
              <asp:ListView ID="ListView2" runat="server" DataKeyNames="RUTA,IDE_BANNER">
            <ItemTemplate>
                <%--  <div class="col-lg-4 col-md-4 col-xs-6 thumb">--%>
                <div class="col-sm-6 col-md-3">
                       <br />
                            <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl ='<%# Bind("FilePath") %>' 
                                 NavigateUrl='<%# Bind("URL") %>'  ></asp:HyperLink>
                           
                       
                </div>

            </ItemTemplate>
        </asp:ListView>
        </center>

    </div>
    <br />
       
</asp:Content>

