<%@ Page Title="Log in" Language="C#" MasterPageFile="~/SiteWeb.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<style type="text/css">

.form-1 {
    /* Size & position */
    width: 300px;
    margin: 60px auto 30px;
    padding: 10px;
    position: relative; /* For the submit button positioning */

    /* Styles */
    box-shadow: 
        0 0 1px rgba(0, 0, 0, 0.3), 
        0 3px 7px rgba(0, 0, 0, 0.3), 
        inset 0 1px rgba(255,255,255,1),
        inset 0 -3px 2px rgba(0,0,0,0.25);
    border-radius: 5px;
    background: linear-gradient(#eeefef, #ffffff 10%);
}

.form-1 .field {
    position: relative; /* For the icon positioning */
}

.form-1 .field i {
    /* Size and position */
    left: 0px;
    top: 0px;
    position: absolute;
    height: 36px;
    width: 36px;

    /* Line */
    border-right: 1px solid rgba(0, 0, 0, 0.1);
    box-shadow: 1px 0 0 rgba(255, 255, 255, 0.7);

    /* Styles */
    color: #777777;
    text-align: center;
    line-height: 42px;
    transition: all 0.3s ease-out;
    pointer-events: none;
}
.form-1 input[type=text],
.form-1 input[type=password] {
    font-family: 'Lato', Calibri, Arial, sans-serif;
    font-size: 13px;
    font-weight: 400;
    text-shadow: 0 1px 0 rgba(255,255,255,0.8);

    /* Size and position */
    width: 100%;
    padding: 10px 18px 10px 45px;

    /* Styles */
    border: none; /* Remove the default border */
    box-shadow: 
        inset 0 0 5px rgba(0,0,0,0.1),
        inset 0 3px 2px rgba(0,0,0,0.1);
    border-radius: 3px;
    background: #f9f9f9;
    color: #777;
    transition: color 0.3s ease-out;
}

.form-1 input[type=text] {
    margin-bottom: 10px;
}
.form-1 input[type=text]:hover ~ i,
.form-1 input[type=password]:hover ~ i {
    color: #52cfeb;
}

.form-1 input[type=text]:focus ~ i,
.form-1 input[type=password]:focus ~ i {
    color: #42A2BC;
}

.form-1 input[type=text]:focus,
.form-1 input[type=password]:focus,
.form-1 button[type=submit]:focus {
    outline: none;
}
.form-1 .submit {
    /* Size and position */
    width: 65px;
    height: 65px;
    position: absolute;
    top: 17px;
    right: -25px;
    padding: 10px;
    z-index: 2;

    /* Styles */
    background: #ffffff;
    border-radius: 50%;
    box-shadow: 
        0 0 2px rgba(0,0,0,0.1),
        0 3px 2px rgba(0,0,0,0.1),
        inset 0 -3px 2px rgba(0,0,0,0.2);
}
.form-1 button {
    /* Size and position */
    width: 100%;
    height: 100%;
    margin-top: -1px;

    /* Icon styles */
    font-size: 1.4em;
    line-height: 1.75;
    color: white;

    /* Styles */
    border: none; /* Remove the default border */
    border-radius: inherit;
    background: linear-gradient(#52cfeb, #42A2BC);
    box-shadow: 
        inset 0 1px 0 rgba(255,255,255,0.3),
        0 1px 2px rgba(0,0,0,0.35),
        inset 0 3px 2px rgba(255,255,255,0.2),
        inset 0 -3px 2px rgba(0,0,0,0.1);

    cursor: pointer;
}
.form-1 button:hover,
.form-1 button[type=submit]:focus {
    background: #52cfeb;
    transition: all 0.3s ease-out;
}

.form-1 button:active {
    background: #42A2BC;
    box-shadow: 
        inset 0 0 5px rgba(0,0,0,0.3),
        inset 0 3px 4px rgba(0,0,0,0.3);
}
</style>
  <%--   <h2><%: Title %>.</h2>--%>
    <%--<script type="text/javascript">

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
    <div class="row">
        <div class="col-md-12">
            <br />
            <div style="text-align: left; font-size: 15px; color: #000000;">
                 <script>
                var mydate = new Date();
                var year = mydate.getYear();
                if (year < 1000)
                    year += 1900;
                var day = mydate.getDay();
                var month = mydate.getMonth();
                var daym = mydate.getDate();
                if (daym < 10)
                    daym = "0" + daym;

                var dayarray = new Array("Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "S&aacute;bado");
                var montharray = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                document.write("<small><font color='#000000' face='Arial' font-size='9px'><b>" + " Lima - Perú, " + dayarray[day] + " " + daym + " de " + montharray[month] + " de " + year + "</b></font></small>");
			
			 </script>
            </div>
        </div>
    </div>
    <div class="row">
         <div class="col-md-6">
   
        </div>
        <div class="col-md-6">
            <br />
            <section class="form-1">
                <p class="field">
                    <%--<input type="text" name="login" placeholder="Username or email">--%>
                     <asp:TextBox runat="server" ID="Email" required="" placeholder="Usuario"/>
                    <i class="icon-user icon-large">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Login_user.png" /></i>
                </p>
                <p class="field">
                   <%-- <input type="password" name="password" placeholder="Password">--%>
                     <asp:TextBox runat="server" ID="Password" TextMode="Password"  required=""  placeholder="contraseña"/>
                    <i class="icon-lock icon-large" > <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Login_candado.png" /></i>
                </p>
                <p class="submit">
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="LogIn" ImageUrl="~/imagenes/Login.fw.png"/>
                   <%-- <asp:Button runat="server" OnClick="LogIn" Text=">>"  />--%>
                        <i class="icon-arrow-right icon-large">
                           
                        </i>

                 
                </p>
            </section>

           
        </div>

       
        
    </div>
    <br /><br />
  
  <%--  <br /><br /><br /><br /><br /><br /><br /><br />--%>
</asp:Content>
