<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Intranet_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
   <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <%--<link rel="stylesheet" href="css/bootstrap.min.css">--%>
    <link rel="stylesheet" href="css/login.css">

    
     <div class="card card-container pull-left">
            <!-- <img class="profile-img-card" src="//lh3.googleusercontent.com/-6V8xOA6M7BA/AAAAAAAAAAI/AAAAAAAAAAA/rzlHcD0KYwo/photo.jpg?sz=120" alt="" /> -->
            <img id="profile-img" class="profile-img-card" src="imagenes/titulo-02.png" />
            <p id="profile-name" class="profile-name-card"></p>
            <%--<form class="form-signin">--%>
               <%-- <span id="reauth-email" class="reauth-email"></span>--%>
                <asp:TextBox runat="server" ID="Email" required="" placeholder="N° Dni"  class="form-control" />
               <%-- <input type="email" id="inputEmail" class="form-control" placeholder="Correo" required autofocus>--%>
                <%--<input type="password" id="inputPassword" class="form-control" placeholder="Contraseña" required>--%>
                 <asp:TextBox runat="server" ID="Password" TextMode="Password"  required=""  placeholder="contraseña" class="form-control"/>
                <div id="remember" class="checkbox">
                    <label>
                        <input type="checkbox" value="remember-me"> Recordar contraseña
                    </label>
                </div>
                <asp:Button ID="Button1" class="btn btn-lg btn-primary btn-block " runat="server" Text="Ingresar" OnClick="LogIn"  CausesValidation="false"/>
               <%-- <button class="btn btn-lg btn-primary btn-block btn-signin" type="submit">Ingresar</button>--%>
           <!-- /form -->
            <a href="#" class="forgot-password">
                ¿Olvidaste tu contraseña? Click aquí
            </a>
        </div><!-- /card-container -->

      <script src="js/jquery-2.2.0.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/login.js"></script>

</asp:Content>

