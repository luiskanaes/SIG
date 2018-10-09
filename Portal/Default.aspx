<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: SSK ::.</title>
    <meta charset="UTF-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="description" content="Una gran empresa está formada por grandes personas. En SSK, nuestra gente hace la diferencia"/>
    <meta name="keywords" content="soluciones,administrativas, ingenieria, diseño, contruccion, ssk, software"/>
    <meta name="author" content="carlos carbonell"/>
    <meta name="language" content="es"/>
    <meta name="revisit-after" content="15 days"/>
    <meta name="robots" content="all"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <link rel="Stylesheet" href="../Styles/default.css" type="text/css" media="screen" />
    <script type="text/javascript" src="../js/DOMAlert.js"></script>
 

    <link rel="stylesheet" href="~/Content/bootstrapSSK.css" type="text/css" media="screen" />
    <link rel="Stylesheet" href="~/Content/timeline.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/lightbox.css" type="text/css" media="screen" />
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
     <script type="text/javascript">
   
         function doAlert(mensaje) {
             var msg = new DOMAlert(
             {
                 title: 'Mensaje SSK',
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
         document.onselectstart = function () { return false; }
        </script>
    <style type="text/css">
        .body {
             top:auto;
        }
  input[type=text] {
    width: 100%;
    
    background-color: white;
    background-image: url('/imagenes/Login_user.png');
    background-position: 10px 10px; 
    background-repeat: no-repeat;
    padding-left: 40px;
    border: 1px solid #004177;
    margin: 8px 0;
}
    input[type=text]:focus {
    width: 100%;
    background-image: url('/imagenes/Login_user.png');
    background-position: 10px 10px; 
    background-repeat: no-repeat;
    padding-left: 40px;
    border: 1px solid #004177;
    margin: 8px 0;
}
    input[type=password] {
    width: 100%;
    background-color: white;
    background-image: url('/imagenes/Login_candado.png');
    background-position: 10px 10px; 
    background-repeat: no-repeat;
    padding-left: 40px;
    border: 1px solid #004177;
    margin: 8px 0;
}
        input[type=password]:focus {
    width: 100%;

    background-image: url('/imagenes/Login_candado.png');
    background-position: 10px 10px; 
    background-repeat: no-repeat;
    padding-left: 40px;
    border: 1px solid #004177;
    margin: 8px 0;
}
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
    
    <form id="form1" runat="server">
        <br />
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-8">
                <center>
                    <asp:Image ID="Image1" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/logo SSK.png"></asp:Image>

                </center>
            </div>
            <div class="col-md-2">
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-8">
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
            </div>
            <div class="col-md-2">
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-8">
            </div>
            <div class="col-md-2">
            </div>
        </div>
      
        <div class="row">
            <div class="col-md-12">
                <br /><br />

                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                          <asp:TextBox runat="server" ID="Email" required="" placeholder="Ingresa tu usuario"/>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                         <asp:TextBox runat="server" ID="Password" TextMode="Password"  required=""  placeholder="Ingresa tu contraseña"/>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-6" style ="float:right">
                        
                     <asp:ImageButton ID="ImageButton1" runat="server" OnClick="LogIn" ImageUrl="~/imagenes/IniciarSession.jpg" CssClass="img-responsive"/>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4" style ="text-align :right;">
                   <center>
                    <b><asp:HyperLink ID="HyperLink1" runat="server" Font-Italic="True">¿Olvidaste tu contraseña? ingresa aquí</asp:HyperLink></b>
                   </center>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

               
                   
                   
                 

                 
             
            </div>

        </div>

    </form>
</body>
</html>
