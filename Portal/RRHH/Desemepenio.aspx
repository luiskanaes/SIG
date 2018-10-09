<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Desemepenio.aspx.cs" Inherits="RRHH_Desemepenio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #EEAA00;
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
        </div>
        <div class="col-md-4">
            <center>
<asp:Image runat="server" ID="Img1" ImageUrl="~/imagenes/Eval_desempeño.png" CssClass="img-responsive" ></asp:Image>
            </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <center>
     <asp:DataList ID="dlCustomers" runat="server"   RepeatDirection="Horizontal" CssClass="row" >
         <ItemTemplate>
             
                  <asp:Image ID="Image1" runat="server"  ImageUrl='<%# Eval("ICONO").ToString().Trim() %>' CssClass="img-responsive"  />
          
            
         </ItemTemplate>
     </asp:DataList>
             </center>
        </div>
    </div>
   <br />
<%--    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4" style="background-color:#f5f5f5; border-radius:5px">
            <br />
            <center>
                
            <asp:Label runat="server" Text="TU CALIFICACIÓN" ID="lbl1" Font-Bold="True" Font-Size="15pt" ForeColor="#EEAA00"></asp:Label><br />
            <asp:Label runat="server" Text="0.00" ID="lblNota" Font-Bold="True" Font-Size="30pt" ForeColor="#EEAA00"></asp:Label>
            </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>--%>
    <div class="row">
        <div class="col-md- 12">
            <center>
<asp:Image runat="server" ID="Img" CssClass="img-responsive" ImageUrl="~/imagenes/DesempenioImg.png"></asp:Image>
            </center>
        </div>
    </div>
 <%--   <div class="row">
        <div class="col-md-12">
       
            
            <asp:Menu ID="Menu1" runat="server" RenderingMode="List"
                IncludeStyleBlock="false"
                StaticMenuStyle-CssClass="nav navbar-nav"
                DynamicMenuStyle-CssClass="dropdown-menu"
               
                Orientation="Horizontal">
            </asp:Menu>

            



        </div>
    </div>--%>
    <div class="row">
     <div class="col-md-12" >
        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row" OnItemDataBound="dlCustomers_ItemDataBound" >
            <ItemTemplate>
                <%-- <div class="row">--%>
                <div class="col-sm-6">
                    <!--THUMBNAIL#2-->
                    <div class="panel-body">
                        <center> 
                           <b><asp:Image ID="Image2" runat="server"  ImageUrl='<%# Eval("Icono")%>'></asp:Image><br />
                               <asp:Label ID="Label1" runat="server" Text='<%# Eval("TIPO")%>' ></asp:Label>
                               <asp:Label ID="lblIdOpcion" runat="server" Text='<%# Eval("IdOpcion")%>' Visible="false"></asp:Label>
                            
                           </b> 
                           
                     </center>
                          <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdOpcion" CssClass="EtiquetaSimple" GridLines="None" ShowHeader="False" BackColor="White" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="1">
                                    <ItemTemplate>
                                        <center>
                                       <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl='<%# Eval("Icono")%>' NavigateUrl='<%# Eval("URL")%>'></asp:HyperLink>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="2">
                                    <ItemTemplate>

                                        <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("NombreOpcion")%>' NavigateUrl='<%# Eval("URL")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="DATO" HeaderText="DATO" SortExpression="DATO" />

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            
            </ItemTemplate>
        </asp:DataList>
        </div>
    </div>

</asp:Content>

