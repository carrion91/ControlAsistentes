<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarPeriodos.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarPeriodos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <div class="col-md-12 col-xs-12 col-sm-12">
            <center>
                <asp:Label runat="server" Text="Apertura de Periodo" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">Seleccione un periodo, o ingrese uno nuevo</p>
            </center>
        </div>
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <hr />
        </div>
        <br />


        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>
        <div class="col-md-12 col-xs-6 col-sm-6">

            <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                <asp:Button ID="btnNuevoPeriodo" runat="server" Text="Nuevo periodo" CssClass="btn btn-primary boton-nuevo" />
            </div>
        </div>
        <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
            <table class="table table-bordered">
                <thead style="text-align: center !important; align-content: center">
                    <tr style="text-align: center" class="btn-primary">
                        <th>Habilitar</th>
                        <th>Año</th>
                        <th>Semestre</th>
                        <th></th>
                    </tr>
                </thead>

                <asp:Repeater ID="rpPeriodos" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr style="text-align: center">
                            <td>
                                <div class="btn-group">
                                    <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("anoPeriodo") %>' />
                                    <%--<asp:CheckBox ID="cbProyecto" runat="server" Text="" />--%>
                                    <asp:LinkButton ID="btnSelccionar" runat="server" ToolTip="Seleccionar" CommandArgument='<%# Eval("anoPeriodo") %>'  OnClick="EstablecerPeriodoActual_Click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                </div>
                            </td>
                            <td> <%# Eval("AnoPeriodo") %> <%# (Eval("habilitado").ToString() == "True")? "(Actual)" : "" %></td>
                            <td><%# Eval("semestre") %></td>
                            <td>
                                <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" CommandArgument='<%# Eval("anoPeriodo") %>'><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                            </td>
                        </tr>

                    </ItemTemplate>

                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="text-align: right">
            <asp:Button ID="btnAtras" runat="server" Text="Atrás" CssClass="btn btn-primary boton-otro" OnClick="btnDevolverse" />
        </div>
        <%--paginación--%>
        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
            <center>
                     <table class="table" style="max-width:664px;">
                         <tr style="padding:1px !important">
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbPrimero" runat="server" CssClass="btn btn-primary" OnClick="lbPrimero_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbAnterior" runat="server" CssClass="btn btn-default" OnClick="lbAnterior_Click"><span class="glyphicon glyphicon-backward"></asp:LinkButton>
                             </td>
                              <td style="padding:1px !important">
                                  <asp:DataList ID="rptPaginacion" runat="server"
                                    OnItemCommand="rptPaginacion_ItemCommand"
                                    OnItemDataBound="rptPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbPaginacion" runat="server" CssClass="btn btn-default"
                                            CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                            Text='<%# Eval("PaginaText") %>' ></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:DataList>
                              </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbSiguiente" CssClass="btn btn-default" runat="server" OnClick="lbSiguiente_Click"><span class="glyphicon glyphicon-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbUltimo" CssClass="btn btn-primary" runat="server" OnClick="lbUltimo_Click"><span class="glyphicon glyphicon-fast-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:Label ID="lblpagina" runat="server" Text=""></asp:Label>
                             </td>
                         </tr>
                     </table>
                 </center>
        </div>
        <%--fn paginación--%>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
