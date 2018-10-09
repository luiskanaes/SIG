<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="formativoBandejaExamen.aspx.cs" Inherits="RRHH_formativoBandejaExamen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--   <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #82b623;
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
    </style>--%>
    <script type="text/javascript">
    document.onselectstart = function () { return false; }
 
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
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/mensaje_formativo-01.fw.png" />
        </div>
        <div class="col-md-4">
            <center>
            <asp:Image ID="imgFotos" runat="server" Width="120px" /><br />
                <asp:Label runat="server" ID="lblnombre"></asp:Label>
           <%-- <asp:HyperLink runat="server" ID="hpRegresar" ForeColor="#0066CC" NavigateUrl="~/RRHH/FormativoMenu.aspx">Regresar</asp:HyperLink>--%>
            </center>
        </div>

        <div class="col-md-4">
            <asp:Label ID="lblCodigoFicha" runat="server" Visible="False" ></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <asp:Label ID="lblIDE_FASE" runat="server" Visible="false" ></asp:Label>
        <div class="col-lg-12 ">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDE_FICHA">
                <ItemTemplate>

                    <div class="col-lg- 12">
                        <div class="panel panel-primary">
                            <div class="panel-footer">
                               <b> <%# Eval("NRO_FASE")%> </b> 
                             
                                
                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Proyecto</label>
                                         <%# Eval("PROYECTO")%>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Fecha inicio</label>
                                        <%# Eval("FECHA_INICIO")%>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Fecha fin</label> 
                                        <%# Eval("FECHA_FIN")%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Ubicación</label>
                                        <%# Eval("UBICACION")%>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Cargo</label>
                                        <%# Eval("CARGO")%>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Jefe directo</label>
                                        <%# Eval("JEFE")%>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                       <asp:Label ID="lblEtiqueta1" runat="server" Font-Bold="true" Text="Autoevaluación (Mitad)"  Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>'></asp:Label>
                                       

                                    </div>
                                    <div class="col-md-4">
                                         <asp:Label ID="lblpto1" runat="server" Text='<%# Eval("PTO_SEGUIMIENTO_MITAD")  %>' Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>'></asp:Label>
                                          <asp:ImageButton ID="btnDesempenioM" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/PencilAdd.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>' OnClick ="View_MitadDesempenio"/>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblEtiqueta2" runat="server" Font-Bold="true" Text="Autoevaluación (Final)"  Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>'></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                         <asp:Label ID="lblpto2" runat="server" Text='<%# Eval("PTO_SEGUIMIENTO")  %>' Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>'></asp:Label>
                                          <asp:ImageButton ID="btnDesempenio" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/PencilAdd.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>' OnClick ="View_FinalDesempenio"/>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            

                            </div>

                            <div class="panel-footer"> <b>Objetivos: </b>
                                <%# Eval("OBSERVACIONES")%>
                            </div>

                        </div>
                    </div>


                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>

