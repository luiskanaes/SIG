<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="BuenasIdeasRegistro.aspx.cs" Inherits="OPERACIONES_BuenasIdeasRegistro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #ec9600;
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
        .adjunto
        {
            padding: 5px 15px;
            background: #ec9600;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }

              .button {
                  padding: 5px 15px;
                  background: #57227a;
                  border: 0 none;
                  cursor: pointer;
                  -webkit-border-radius: 5px;
                  border-radius: 5px;
                  color: #ffffff;
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
                  width: 75%;
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

              @media screen and (max-width: 700px) {
                  .box {
                      width: 70%;
                  }

                  .popup {
                      width: 100%;
                  }
              }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-4">
          
            <asp:HyperLink ID="HyperLink2" runat="server"  ImageUrl="~/imagenes/Buenasideas.180x90.fw.png" NavigateUrl="~/OPERACIONES/BuenasIdeas.aspx" ></asp:HyperLink>

        </div>
        
        <div class="col-md-8" style="text-align :right">
            <br />
          
            
            <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/operaciones/BuenasideasPropuesta.aspx"  ImageUrl="~/imagenes/BI_Mis_propuesta.png"></asp:HyperLink>
            <asp:HyperLink runat="server" ID="hpPop" NavigateUrl="#popup1"  ImageUrl="~/imagenes/BI_ejemplo.png"></asp:HyperLink>
        
            

                
         
            
        </div>
    </div>
    <div id="popup1" class="overlay">
        <div class="popup">

            <a class="close" href="#">&times;</a>
            <div class="content">
                <asp:Image ID="Image9" runat="server" ImageUrl="~/imagenes/BI_ejemploFormulario.jpg" CssClass="img-responsive" />
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
           
                  <asp:Image ID="Image8" runat="server" ImageUrl="~/imagenes/BI_propuesta.png" CssClass="img-responsive"/>
           
         
                  <asp:TextBox ID="txttitulo" runat="server" MaxLength="200" Height="50px" TextMode="MultiLine"></asp:TextBox>
             
         
          
        </div>
        
    </div>
    <br />
        <div class="row">
        
        <div class="col-md-6">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/imagenes/BI_objetivo.png" CssClass="img-responsive"/>
            <asp:TextBox ID="txtdescripcion" runat="server" Height="150px" MaxLength="2500" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/BI_solucion.png" CssClass="img-responsive"/>
            <asp:TextBox ID="txtsolucion" runat="server" Height="150px" MaxLength="2500" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
     <div class="row">
        
        <div class="col-md-6">
            <asp:Image ID="Image6" runat="server" ImageUrl="~/imagenes/Bi_ventajas.png" CssClass="img-responsive"/>
            <asp:TextBox ID="txtventajas" runat="server" Height="150px" MaxLength="2500" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-md-6">
             <asp:Image ID="Image7" runat="server" ImageUrl="~/imagenes/BI_area.png" CssClass="img-responsive"/>
            <asp:TextBox ID="txtareas" runat="server" Height="150px" MaxLength="2500" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-6">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/BI_adjunto.png" CssClass="img-responsive"/>
            <asp:FileUpload ID="FileUpload1" runat="server"  CssClass="adjunto"/>
        </div>
        <div class="col-md-6" style="text-align:right">
          
<asp:Button ID="btnenviar" runat="server" Text="ENVIAR PROPUESTA" OnClick="btnenviar_Click"></asp:Button>
                 </center>
                <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnenviar"></cc1:ConfirmButtonExtender>
                <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnenviar"
                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                Mensaje
                </div>
                <div class="body">
                ¿Deseas enviar propuesta?
                </div>
                <div class="footer" align="right">
                <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
                </asp:Panel>
        
        
    </div>
        </div>
    <br />
    <div class="row">
        <div class="col-md-6">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/BI_leyenda.png" CssClass="img-responsive"/>
           
        </div>
        <div class="col-md-6">
        </div>
        
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            
           
        </div>

    </div>
</asp:Content>

