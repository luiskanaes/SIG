<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="VerLegajos.aspx.cs" Inherits="CAREMENOR_VerLegajos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>Documento requerimiento <asp:Label ID="lblreq" runat="server" ></asp:Label></b>
            </div>
        </div>
    </div>
   
      <div class="row">
                            <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="ide_LegajoFile,FILE_ARCHIVO,CODIGO_GRUPO" EmptyDataText="There are no data records to display." Font-Size="8pt" >
                                            <Columns>

                                                <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                                <asp:BoundField DataField="FILE_NOMBRE" HeaderText="File" SortExpression="FILE_NOMBRE" />
                                                <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" />
                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha" SortExpression="fecha_registro" />
                                                <asp:BoundField DataField="CODIGO_GRUPO" HeaderText="Grupo Legajo" SortExpression="CODIGO_GRUPO">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="File">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("FILE_ARCHIVO") %>' Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                            </div>
                        </div>
</asp:Content>

