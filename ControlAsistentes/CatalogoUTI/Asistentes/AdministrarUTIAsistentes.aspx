<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarUTIAsistentes.aspx.cs" Inherits="ControlAsistentes.CatalogoUTI.Asistentes.AdministrarUTIAsistentes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <center>
             <div class="col-md-12 col-xs-12 col-sm-12">
            <center>
                <asp:Label id="titulo" runat="server" Text="Administración de Asistentes UTI" Font-Size="Large" ForeColor="Black"></asp:Label>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <asp:Label id="tituloAS" runat="server" Text="" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">En esta sección podrá visualizar información de los asistentes</p>
            </center>
        </div>
        </center>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>
                <br />

                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <%--Filtro Unidades--%>
                <div class="col-md-6 col-xs-6 col-sm-6">
                    <h4>Unidad</h4>
                    <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                </div>
                
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                    <br />
                    <br />
                </div>
                <%--fn Filtro--%>     



                <%--Tabla--%>
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th></th>
                                <th>Nombre</th>
                                <th>Carné</th>
                                <th>Unidad Asistencia</th>
                                <th>Último Periodo Nombrado</th>
                                <th>Usuario</th>
                                <th>Tarjeta</th>

                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary glyphicon glyphicon-search" OnClick="filtrarAsistentes" aria-hidden="true"></asp:LinkButton></td>
                            <td>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro nombre asistente" AutoPostBack="true" onkeypress="enter_click()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txBool" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true" Visible="false"></asp:TextBox></td>

                            <td></td>
                            <td></td>



                        </tr>
                        <asp:Repeater ID="rpAsistentes" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center">


                                    <td></td>
                                    <td style='<%# Convert.ToString(Eval("aprobado")).Equals("True")? "background-color:#008f39":(Convert.ToString(Eval("aprobado")).Equals("False")&&Convert.ToString(Eval("solicitud")).Equals("2")? "background-color:#ff0000": "background-color:#fd8e03") %>'>
                                        <%# Eval("asistente.nombreCompleto") %></td>
                                    <td><%# Eval("asistente.carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>

                                     <td>
                                        <div id="btnUser" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("asistente.carnet") %>' />
                                            <asp:LinkButton ID="btnVerUsuario" OnClick="btnVeUsuarioAsistente" runat="server" ToolTip="Ver Usuario" CommandArgument='<%# Eval("asistente.carnet") %>'><span id="cambiarUser" class="glyphicon glyphicon-user"></span></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="btnTarjeta" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("asistente.carnet") %>' />
                                            <asp:LinkButton ID="btnVerTarjeta"  OnClick="btnVerTarjetaAsistente" runat="server" ToolTip="Ver Tarjeta" CommandArgument='<%# Eval("asistente.carnet") %>'><span id="cambiarCard" class="glyphicon glyphicon-credit-card"></span></asp:LinkButton>
                                        </div>
                                    </td>

                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <%--fn Tabla--%>     

           <%--Botón Atras--%>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <div style="text-align: right">
                        <asp:Button ID="Button1" runat="server" Text="Atrás" CssClass="btn btn-primary boton-otro" OnClick="btnDevolverse" />
                    </div>
                </div>
            <%--fn Botón--%>


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
                <br />
                <br />
                <br />
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                <hr />
            </div>


            <!-- Modal nuevo asistente -->


            <!-- Fin modal nuevo asistente -->

            <!-- Modal Eliminar -->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
          
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN modal eliminar unidad -->

        </ContentTemplate>

    </asp:UpdatePanel>





    <!-- Script inicio -->
    <script type="text/javascript">
        function activarModalTarjetaAsistente() {
            $('#modalTarjetaAsistente').modal('show');
        };

        function activarModalUsuarioAsistente() {
            $('#modalUsuarioAsistente').modal('show');
        };

        function enter_click() {
            if (window.event.keyCode == 13) {
                document.getElementById('<%=btnFiltrar.ClientID%>').focus();
                document.getElementById('<%=btnFiltrar.ClientID%>').click();
            }
        }

    </script>
    <!-- Script fin -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
