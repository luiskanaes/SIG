<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlPermisos.ascx.cs" Inherits="Controles_ControlPermisos"%>

<%--<div class="row">
    <div class="col-md-5">
        <asp:ListView ID="ListView1" runat="server" DataKeyNames="URL">
            <ItemTemplate>
                <asp:ImageButton ID="btnAcceso" runat="server" CommandArgument='<%# Eval("URL") %>' ImageUrl='<%# Eval("ICONO") %>' Width="80px" ToolTip='<%# Eval("MENU") %>' OnClick="URL" />
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="col-md-2">
        <asp:ListView ID="ListView2" runat="server" DataKeyNames="URL">
            <ItemTemplate>
                <asp:ImageButton ID="btnAcceso" runat="server" CommandArgument='<%# Eval("URL") %>' ImageUrl='<%# Eval("ICONO") %>' Width="80px" ToolTip='<%# Eval("MENU") %>' OnClick="URL" />
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="col-md-5">
        <asp:ListView ID="ListView3" runat="server" DataKeyNames="URL">
            <ItemTemplate>
                <asp:ImageButton ID="btnAcceso" runat="server" CommandArgument='<%# Eval("URL") %>' ImageUrl='<%# Eval("ICONO") %>' Width="80px" ToolTip='<%# Eval("MENU") %>' OnClick="URL" />
            </ItemTemplate>
        </asp:ListView>
    </div>

</div>--%>


    <div class="row" >
    <div class="col-md-12" >             
          <center>
                <asp:Menu ID="Menu2" runat="server"   RenderingMode="List" 
                    IncludeStyleBlock="false" StaticMenuStyle-CssClass="nav navbar-nav" DynamicMenuStyle-CssClass="dropdown-menu" >
                </asp:Menu>
                </center>

                <script type="text/javascript">
                    //Disable the default MouseOver functionality of ASP.Net Menu control.
                    Sys.WebForms.Menu._elementObjectMapper.getMappedObject = function () {
                        return false;
                    };
                    $(function () {
                        //to fix collapse mode width issue
                        $(".nav li,.nav li a,.nav li ul").removeAttr('style');

                        //for dropdown menu
                        $(".dropdown-menu").parent().removeClass().addClass('dropdown');
                        $(".dropdown>a").removeClass().addClass('dropdown-toggle').append('<b class="caret"></b>').attr('data-toggle', 'dropdown');

                        //remove default click redirect effect           
                        $('.dropdown-toggle').attr('onclick', '').off('click');

                    });
                </script>
   
          </div>
          </div>
