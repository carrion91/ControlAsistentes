<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarProyectos.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarProyectos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <center>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <center>
                        <asp:Label id="tituloUn" runat="server" Text="Administración de Proyectos" Font-Size="Large" ForeColor="Black"></asp:Label>
                        <p class="mt-1">En esta sección podrá asignar asistentes a los proyectos</p>
                    </center>
                </div>
            </center>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <br />
            <br />
            <br />

            
             <!-- Modal asignar asistente a Proyecto-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="modalAsignarAsistenteProyecto" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Asignar Asistente</h4>
                                </div>

                                <%--<div class="modal-body">--%>
                                    <div class="row">
                                        <%-- fin titulo accion --%>

                                        <%-- campos a llenar --%>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblIdProyecto" runat="server" Text="" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false" Visible="false"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label1" runat="server" Text="Proyecto <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false" ></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbNombreProyecto" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                         <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                              <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="Label5" runat="server" Text="Descripción <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                              <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div  class="input-group">
                                                    <asp:TextBox class="form-control" ID="txbDescripciónProyecto" TextMode="multiline" Columns="50" Rows="5"  runat="server"  Width="260" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        
                                         <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                              <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="Label2" runat="server" Text="Asistente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                              <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div  class="input-group">
                                                    <asp:DropDownList ID="ddlAsistentes" class="btn btn-default dropdown-toggle" runat="server" ></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                       
                                        <%-- botones --%>
                                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                            <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="btnGuardarAsistenteEnProyecto_Click" CommandArgument='<%# Eval("idProyecto") %>'/>
                                           <asp:Button ID="Button2" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo" data-dismiss="modal"/>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>
                                <%--</div--%>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- Fin Modal asignar asistente a Proyecto--%>

            <!-- Modal Nuevo Proyecto-->
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div id="modalNuevoProyecto" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nuevo Proyecto</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <%-- fin titulo accion --%>

                                        <%-- campos a llenar --%>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtNombreProyecto" runat="server" ReadOnly="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha de Inicio <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                             <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox TextMode="Date" class="form-control" ID="txtFechaInicio" runat="server" ReadOnly="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha de finalización <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox TextMode="Date" class="form-control" ID="txtFechaFinalización" runat="server" ReadOnly="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbDisponible" runat="server" Text="Disponible <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                                <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:RadioButton id="rdProyectoDisponible" runat="server" GroupName="measurementSystem" Checked="false" ></asp:RadioButton>
                                                </div>   
                                        </div>
                                         <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                              <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="lbDescripción" runat="server" Text="Descripción <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                              <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div  class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtDescripción" TextMode="multiline" Columns="50" Rows="5"  runat="server"  Width="260" ></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </div>
                                       
                                        <%-- botones --%>
                                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="btnGuardarProyecto_Click" />
                                           <asp:Button ID="ButtonCerrar" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo" data-dismiss="modal"/>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>
                                </div
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- Fin Modal Nuevo Proyecto--%>

              <!-- Modal Modificar Proyecto-->
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="modalModificarProyecto" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Modificar Proyecto</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <%-- fin titulo accion --%>

                                        <%-- campos a llenar --%>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label3" runat="server" Text="Nombre <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbNombre" runat="server" ReadOnly="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label4" runat="server"  Text="Fecha de Inicio <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                             <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbFInicio" runat="server" ReadOnly="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label6" runat="server"  Text="Fecha de finalización <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbFFin"  runat="server" ReadOnly="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label7" runat="server" Text="Disponible <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                                <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:RadioButton id="rdBtnDisponible" runat="server" GroupName="measurementSystem" Checked="false" ></asp:RadioButton>
                                                </div>   
                                        </div>
                                         <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                              <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="Label8" runat="server" Text="Descripción <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                              <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div  class="input-group">
                                                    <asp:TextBox class="form-control" ID="txbDescrip" TextMode="multiline" Columns="50" Rows="5"  runat="server"  Width="260" ></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </div>
                                       
                                        <%-- botones --%>
                                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                            <asp:Button ID="Button3" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="btnGuardarProyecto_Click" />
                                           <asp:Button ID="Button4" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo" data-dismiss="modal"/>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>
                                </div
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- Fin Modal Modificar Proyecto--%>
            

            <h4>Proyecto</h4>
            <asp:DropDownList ID="ddlProyecto" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged"></asp:DropDownList>
              
            <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                    <asp:Button ID="btnNuevoProyecto" runat="server" Text="Nuevo Proyecto" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevoProyecto_Click" />
            </div>
          

            <br />
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <br />
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                           
                                <th>Acciones</th>
                                <th>Proyecto</th>
                                <th>Descripción</th>
                                <th>Asistente</th>
                                <th>Unidad Asistencia</th>
                                <th>Fecha Inicio</th>
                                <th>Fecha Finalización</th>
                                <th>Finalizado</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" OnClick="filtrarProyectos"><span aria-hidden="true" class="glyphicon glyphicon-search"></span>

                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro por nombre" AutoPostBack="true"></asp:TextBox>
                            </td>
                
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <asp:Repeater ID="rpProyectos" runat="server">
<%--                            <HeaderTemplate>
                            </HeaderTemplate>--%>

                            <ItemTemplate>
                                <tr style="text-align: center">


                                    <td>
                                        <div id="btnTarjeta" class="btn-group">
                                            <asp:HiddenField runat="server" ID="hdfIdProyectoModificar" Value='<%# Eval("idProyecto") %>' />
                                            <asp:LinkButton ID="linkModificarProyecto" OnClick="linkModificarProyecto_Click" style="margin-right: 10px" class="glyphicon glyphicon-pencil btn" runat="server" ToolTip="Modificar Proyecto" CommandArgument='<%# Eval("idProyecto") %>'></asp:LinkButton>
                                       
                                            <asp:HiddenField runat="server" ID="hdfIdProyectoEliminar" Value='<%# Eval("idProyecto") %>' />
                                            <asp:LinkButton ID="linkEliminarProyecto" OnClick="btnEliminarProyecto_Click" style="margin-left: 10px" class="glyphicon glyphicon-trash btn" runat="server" ToolTip="Eliminar Proyecto" CommandArgument='<%# Eval("idProyecto") %>'></asp:LinkButton>
                                      
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("idProyecto") %>' />
                                            <asp:LinkButton ID="LinkButton1" OnClick="btnAsignarAsistenteProyecto_Click" style="margin-left: 10px" class="glyphicon glyphicon-pushpin btn" runat="server" ToolTip="Asignar Asistente" CommandArgument='<%# Eval("idProyecto") %>'></asp:LinkButton>
                                      
                                            <%--<asp:LinkButton ID="btnVerTarjeta"  OnClick="btnVerTarjetaAsistente" class="glyphicon glyphicon-credit-card"  runat="server" ToolTip="Ver Tarjeta" CommandArgument='<%# Eval("carnet") %>'></asp:LinkButton>--%>
                                        </div>
                                    </td>

                                    <td><%# Eval("nombre") %></td>
                                    <td><%# Eval("descripcion") %></td>
                                    <td><%# Eval("asistente.nombreCompleto") %></td>
                                    <td><%# Eval("asistente.unidad.nombre") %></td>
                                    <td><%# ((DateTime)Eval("fechaInicio")).ToShortDateString() %></td>
                                    <td><%# ((DateTime)Eval("fechaFinalizacion")).ToShortDateString() %></td>

                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("asistente.carnet") %>' />
                                            <asp:LinkButton ID="btnDetalles" runat="server" Enabled="false" AutoPostBack="false" ToolTip="Detalles" CommandArgument='<%# Eval("finalizado") %>' >
                                                <div class='<%# Eval("finalizado") %>'></div></asp:LinkButton>
                                        </div>
                                    </td>

                                </tr>

                            </ItemTemplate>

<%--                            <FooterTemplate>
                            </FooterTemplate>--%>
                        </asp:Repeater>
                    </table>
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
            <div >

                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
                
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                <hr />

            </div>


            </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function activarModalNuevoProyecto() {
            $('#modalNuevoProyecto').modal('show');
        };
        function closeModalNuevoProyecto() {
            $('#modalNuevoProyecto').modal('hide');
        };
        function activarModalAsignarAsistenteProyecto() {
            $('#modalAsignarAsistenteProyecto').modal('show');
        };
        function closeModalAsignarAsistenteProyecto() {
            $('#modalAsignarAsistenteProyecto').modal('hide');
        };
        function activarModalModificarProyecto() {
            $('#modalModificarProyecto').modal('show');
        };
        function closeModalModificarProyecto() {
            $('#modalModificarProyecto').modal('hide');
        };

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
