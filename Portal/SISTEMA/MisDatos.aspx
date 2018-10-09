<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="MisDatos.aspx.cs" Inherits="SISTEMA_MisDatos" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/ControlPermisos.ascx" TagPrefix="uc1" TagName="ControlPermisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

        <script type="text/javascript">
   
 
    document.onselectstart = function () { return false; }
    function SoloNum(e) {
        var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
        return ((key_press > 47 && key_press < 58) || key_press == 46);
        // el  "|| key_press == 46" es para incluir el punto ".", si borramos solo ingresara enteros 
    }
    function jsDecimals(e) {

        var evt = (e) ? e : window.event;
        var key = (evt.keyCode) ? evt.keyCode : evt.which;
        if (key != null) {
            key = parseInt(key, 10);
            if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
                if (!jsIsUserFriendlyChar(key, "Decimals")) {
                    return false;
                }
            }
            else {
                if (evt.shiftKey) {
                    return false;
                }
            }
        }
        return true;
    }

    // Función para las teclas especiales
    //------------------------------------------
    function jsIsUserFriendlyChar(val, step) {
        // Backspace, Tab, Enter, Insert, y Delete
        if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
            return true;
        }
        // Ctrl, Alt, CapsLock, Home, End, y flechas
        if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
            return true;
        }
        if (step == "Decimals") {
            if (val == 190 || val == 110) {  //Check dot key code should be allowed
                return true;
            }
        }
        // The rest
        return false;
    }
    function val(e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8 || tecla == 32) return true;
        patron = /[A-Za-z]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    }
    function ValidaDDL(source, arguments) {
        if (arguments.Value < 1) {
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    } 
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
     <style type="text/css">
        

         .Tamanio {
             width: 350px;
             height :200px;
             background-color:white;
         }

         @media(max-width: 768px) {
             .Tamanio 
             {   width: 350px;
                 height :200px;
                  background-color:white;
             }
         }
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>DATOS PERSONALES</b><asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">

            <div class="row">
                <div class="col-md-12">
                    <center>
                         <asp:Image ID="imgFoto" runat="server"   ImageUrl="~/imagenes/Foto_Fondo.png" CssClass="img-rounded" />
                    </center>
                </div>
            </div>
          
        </div>
         <div class="col-md-3">

             <div class="row">
                <div class="col-md-12">
                    <center>
                         <asp:Image ID="ImgFirma" runat="server" CssClass="img-rounded"  />
<%--<asp:Image ID="imgprueba" runat="server"></asp:Image>--%>
                    </center>
                </div>
            </div>
            
        </div>
        <div class="col-md-6">
            <div class="row">
                
                <div class="col-md-12">
                    <label>Nombre:</label>
                    <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>
                </div>
               
            </div>
            <div class="row">
        
                <div class="col-md-6">
                     <label>Centro Costo:</label>
                    <asp:Label ID="lblCcentro" runat="server" Text="Label"></asp:Label>
                   
                </div>
                <div class="col-md-6">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <label>Adjuntar Foto</label>
                     <asp:FileUpload ID="FileFoto"  runat="server" />
                </div>
                <div class="col-md-6">
                </div>

            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Adjuntar Firma</label>
                     <asp:FileUpload ID="fudFirma"  runat="server" />
                </div>
                <div class="col-md-6">
                </div>

            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <center>
                         
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />

                    </center>
                </div>
                <div class="col-md-6">
                    <asp:Label runat="server" ID="lblfirma" visible="false" ></asp:Label>
                    <asp:Label runat="server" ID="lblfoto" visible="false" ></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>CREAR FIRMA DIGITAL</b><asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            


    <!-- title -->
    <div class="title">
   
      <p class="subtitle">(1)Dibujar firmar con el puntero de mouse.</p> 
       <p class="subtitle">(2) Para guardar firmar, clic derecho y "Guardar imagen como.." </p>  
       
    </div>

    <!-- main content starts here -->
  
      <div class="col-md-6">
       

       
            <div class="canvas-options">
              

                <div class="form-group">
                  <label>Tamaño</label>
                  <select name="pen-width" id="penWidth">
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <!-- <option value="6">6</option> -->
                  </select>
                </div>
 
             
            </div>
              <canvas id="signatureCanvas" width="380" height="150" style="border:solid 1px;border-radius :3px; background-color:white"></canvas>
              <%-- <canvas id="signatureCanvas" class="input-lg"></canvas>--%>
          
          <%--<a href="javascript:void(0)" class="btn btn-primary" id="saveButton">Save</a> --%>
          <br />
          <a href="javascript:void(0)" class="btn btn-default" id="clearButton">Limpiar</a>
        
      
       
      </div>
      <!-- end of result wrapper-->
 
 

       
        </div>

    </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>

    <!-- custom scripts -->
      <script src="../Scripts/script.min.js" type="text/javascript"></script>
 

    <!-- Google Analytics -->
    <script>
      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

      ga('create', 'UA-42126224-12', 'auto');
      ga('send', 'pageview');

    </script>

    

    
</asp:Content>

