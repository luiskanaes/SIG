<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="PERMISOS_CALENDARIO.aspx.cs" Inherits="OPERACIONES_PERMISOS_CALENDARIO" EnableEventValidation="false"%>


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


.calendar {
    width: 50%;  
   
}

.calendar a {
    color: #8e352e;
    text-decoration: none;
}

.calendar .borde {
  
    border-radius :5px;  
}

.calendar .Titulo {
    color:#000000;
    font-size:20px;
    width: 100%;
    background:#ffffff;
     text-transform:uppercase;
    border-top-left-radius :7px;
    border-top-right-radius :7px;
    height: 50px;
}

.calendar .CabeceraDia {
    color:#ffffff;
    font-size:17px;
    background:#00a0f0;
    height: 40px;
     text-align :center ;
      text-transform:uppercase;
}

.calendar .dias {
    background: #f5f5f5;
    color: #ffffff;
    height:60px;
    font-size:20px;
}
.calendar .hoy {
    background: #f7c600;
    color: #000000;
    height:60px;
    font-size:20px;
}

.calendar .finSemana {
    background: #ffe599;
    color: #000000;
    height:60px;
    font-size:20px;
}

.calendar .SgteMes {
     font-size:30px;
}

.calendar li {
    display: block;
    float: left;
    width:14.342%;
    padding: 5px;
    box-sizing:border-box;
    border: 1px solid #ccc;
    margin-right: -1px;
    margin-bottom: -1px;
}

.calendar .weekdays {
    height: 40px;
    background: #8e352e;
}

.calendar ul.weekdays li {
    text-align: center;
    text-transform: uppercase;
    line-height: 20px;
    border: none !important;
    padding: 10px 6px;
    color: #fff;
    font-size: 13px;
}

.calendar .days li {
    height: 180px;
}

.calendar .days li:hover {
    background: #d3d3d3;
}

.calendar .date {
    text-align: center;
    margin-bottom: 5px;
    padding: 4px;
    background: #333;
    color: #fff;
    width: 20px;
    border-radius: 50%;
    float: right;
}

.calendar .event {
    clear: both;
    display: block;
    font-size: 13px;
    border-radius: 4px;
    padding: 5px;
    margin-top: 40px;
    margin-bottom: 5px;
    line-height: 14px;
    background: #e4f2f2;
    border: 1px solid #b5dbdc;
    color: #009aaf;
    text-decoration: none;
}

.calendar .event-desc {
    color: #666;
    margin: 3px 0 7px 0;
    text-decoration: none;  
}

.calendar .other-month {
    background: #f5f5f5;
    color: #666;
}

/* ============================
                Mobile Responsiveness
   ============================*/


@media(max-width: 768px) {

   .calendar .Titulo,  .calendar .weekdays, .calendar .other-month {
  
        padding: 10px;
        font-size:13px;

    }
   .calendar .CabeceraDia
   {
    
       padding: 10px;
       font-size:10px;
   }
   .calendar .dias
   {
    
       height:10px;
       font-size:11px;
     
   }
   .calendar .hoy
   {
      
       height:10px;
       font-size:11px;
   }
   .calendar .finSemana
   {
     
       height:10px;
       font-size:11px;
   }
   .calendar .SgteMes {
     font-size:19px;
}

    .calendar li {
        height: auto !important;
        border: 1px solid #ededed;
    
        padding: 10px;
        margin-bottom: -1px;
    }

    .calendar .date {
        float: none;
    }
}
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="row">
       <%-- <div class="col-md-3">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Solicitudes.180x90.fw.png"/>
             
        </div>--%>
       
        <div class="col-md-12">
            <center>
               <%-- <uc1:controlpermisos runat="server" ID="ControlPermisos" />--%>
            </center>
            <asp:Label ID="lblfecha" runat="server" Visible ="false" ></asp:Label>
            <asp:Label ID="lblCentro" runat="server" Visible ="false" ></asp:Label>
        </div>
    </div>
    <div class="row">
<div class="panel panel-default">
<div class="panel-heading">
<b>SOLICITUDES GENERADAS  </b>
</div>
</div>
</div>

    <div class="row">
        <div class="col-md-4">
            <div class="row">


                <div class="col-md-12">
                    <center>
               
                <asp:Calendar ID="Calendar1" runat="server"   OnDayRender="CalendarDRender" 
                    OnSelectionChanged="Calendar1_SelectionChanged" 
                    OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" FirstDayOfWeek="Monday" ShowGridLines="True"  >
                    <NextPrevStyle CssClass="SgteMes" />
                    <TitleStyle CssClass ="Titulo"/>
                    <DayHeaderStyle CssClass="CabeceraDia"/>
                   
                    <TodayDayStyle CssClass="hoy"/>
                    <WeekendDayStyle CssClass ="finSemana"/> 
                    <DayStyle  CssClass="dias"/>
                    
                   
                </asp:Calendar>
                     
            </center>
                </div>
    </div>
        </div>
     <%--   FIN CUERPO 1--%>

        <div class="col-md-8">
            <div class="row">
                <div class="col-md-12">
                    <%--<div class="panel panel-default">--%>
                        <%--<div class="panel-heading">--%>
                            <b>LEYENDA</b>
                            <asp:DataList ID="GridLeyenda" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row" OnItemDataBound="GridLeyenda_ItemDataBound">

                                <ItemTemplate>

                                    <div class="col-sm-2">
                                        <!--THUMBNAIL#2-->
                                        <asp:Label ID="lblidParemetro" runat="server" Text='<%# Eval("ID_PARAMETRO")%>' Visible="false"></asp:Label>
                                        <asp:GridView ID="GridLeyendaParametros" runat="server" AutoGenerateColumns="False" CssClass="EtiquetaSimple" GridLines="None" ShowHeader="False" BackColor="White" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="color">
                                                    <ItemTemplate>

                                                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# "~/imagenes/"+Eval("DES_CAMPO2") %>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Detalle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="8pt" Text='<%# "(" + Eval("DES_CAMPO1") + ") "  + Eval("DES_ASUNTO")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <%--<div class="col-sm-3">
                    </div>
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-3">
                    </div>--%>
                                </ItemTemplate>

                            </asp:DataList>
                      <%--  </div>--%>
                   <%-- </div>--%>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                <asp:DropDownList ID="ddlLeyenda" runat="server" CssClass="ddlAjustado" AutoPostBack="True" OnSelectedIndexChanged="ddlLeyenda_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
        <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridPermisos" runat="server" AutoGenerateColumns="False" DataKeyNames="IDE_PERMISO" CssClass="table table-striped table-bordered table-hover" GridLines="None" BackColor="White" Font-Size="9pt">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <center>
        <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("COLOR_MOTIVO")%>'/>
        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LETRA_MOTIVO" HeaderText="" SortExpression="LETRA_MOTIVO" />
                                <asp:BoundField DataField="PERSONAL" HeaderText="PERSONAL" SortExpression="PERSONAL" />
                                <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" />
                                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
                            </Columns>


                        </asp:GridView>
                    </div>
                </div>

            </div>


            <div class="row">
                <asp:DataList ID="dlCustomers" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row" OnItemDataBound="dlCustomers_ItemDataBound">
                    <ItemTemplate>
                        <%-- <div class="row">--%>
                        <div class="col-sm-4">
                            <!--THUMBNAIL#2-->
                            <div class="panel-body">
                                <center> 
                           <b>
                               <asp:Label ID="lbldia" runat="server" Text='<%# Eval("DIA")%>'></asp:Label>
                               <hr />
                           </b> 
                           
                     </center>

                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IDE_PERMISO" CssClass="EtiquetaSimple" GridLines="None" ShowHeader="False" BackColor="White">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ee">
                                            <ItemTemplate>
                                                <center>
                                       <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("COLOR_MOTIVO")%>'/>
                                   </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LETRA_MOTIVO" HeaderText="LETRA_MOTIVO" SortExpression="LETRA_MOTIVO" />
                                        <asp:BoundField DataField="PERSONAL" HeaderText="PERSONAL" SortExpression="PERSONAL" />
                                    </Columns>


                                </asp:GridView>



                                <%-- <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/" +Eval("image1").ToString().Trim() %>' Width="150px" Height="150px" />--%>
                                <%--  <div class="thumbnail label-success">
                        
                         <div class="caption">
                             <h4>Rp.<small> <%# Eval("MOTIVO")%></small></h4>
                             <strong><%# Eval("MOTIVO") %></strong>
                             <p>
                                 <small>LT:<strong>  <%# Eval("PERSONAL")%> m2</strong> </small><small>LB : <strong><%# Eval("PERSONAL")%> m2</strong> </small>
                                 <small>Setifikat : <strong><%# Eval("PERSONAL")%></strong> </small>
                                 <br />
                                 <small>Kamar : <strong><%# Eval("PERSONAL")%></strong> </small>
                                 <br />
                                 <small>Kamar Mandi : <strong><%# Eval("PERSONAL")%></strong> </small>
                             </p>
                             <a href="#" class="btn btn-success">Lihat Details</a>
                         </div>
                     </div>--%>
                            </div>
                        </div>
                        <%--</div>--%>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <%--   FIN CUERPO 2--%>
    </div>

    
   
        


  
</asp:Content>

