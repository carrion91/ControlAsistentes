<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarAsistentes.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarAsistentes" %>

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
                <asp:Label id="tituloUn" runat="server" Text="Administración de Asistentes" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">En esta sección podrá aprobar los nombramientos de los asistentes</p>
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

                <div class="col-md-6 col-xs-6 col-sm-6">
                    <h4>Unidad</h4>
                    <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <div class="col-md-12 col-xs-6 col-sm-6">

                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="btnPendientes" runat="server" Text="Aprobaciones Pendientes" CssClass="btn btn-primary boton-nuevo" OnClick="btnPendientes_Click" />
                    </div>
                </div>


                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th></th>
                                <th>Nombre</th>
                                <th>Carné</th>
                                <th>Unidad Asistencia</th>
                                <th>Nombramiento Aprobado</th>
                                <th>Último Período Nombrado</th>
                                <th>Cantidad de Horas Nombrado</th>
                                <th>Cantidad de Períodos Nombrado</th>
                                <th>Documentos</th>

                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" OnClick="filtrarAsistentes"><span aria-hidden="true" class="glyphicon glyphicon-search"></span> </asp:LinkButton>

                            </td>
                            <td>

                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txBool" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true" Visible="false"></asp:TextBox></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>

                        </tr>
                        <asp:Repeater ID="rpAsistentes" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">


                                    <td></td>
                                    <td style='<%# Convert.ToString(Eval("nombrado")).Equals("True")? "background-color:#008f39":(Convert.ToString(Eval("nombrado")).Equals("False")&&Convert.ToString(Eval("solicitud")).Equals("1")? "background-color:#ff0000": "background-color:#fd8e03") %>'>
                                        <%# Eval("nombreCompleto") %></td>
                                    <td><%# Eval("carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnDetalles" runat="server" ToolTip="Detalles" CommandArgument='<%# Eval("idAsistente") %>' OnClick=" btnVerDetalles"><div class='<%# Eval("nombrado") %>'></div></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>
                                    <td><%# Eval("cantidadHorasNombrado") %></td>
                                    <td><%# Eval("cantidadPeriodosNombrado") %></td>
                                    <td>
                                        <div id="btnDocs" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" OnClick="btnVerArchivos_Click" CommandArgument='<%# Eval("idAsistente") %>'><span id="cambiar" class="glyphicon glyphicon-list-alt" ></span></asp:LinkButton>
                                        </div>
                                    </td>
                                     <td style="display:none;"><%# Eval("idAsistente") %></td>
                                    <td style="display:none;"><%# Eval("nombrado") %></td>
                                    <td style="display:none;"><%# Eval("solicitud") %></td>



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

                <br />
                <br />
                <br />
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                <hr />
            </div>
            <div id="divDocsAsist" runat="server" visible="false">
                <br />
                <br />
                <br />
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <center>
                            <asp:Label runat="server" Text="Documentos" Font-Size="Large" ForeColor="Black"></asp:Label>
                           
                        </center>
                </div>
                <br />
                <br />
                <!-- ------------------------ Tabla documentos asistente --------------------------- -->
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">

                    <table id="tblUnidadesProyecto" class="table table-bordered">
                        <thead>
                            <tr style="text-align: center" class="btn-primary">
                                <th>Nombre Asistente</th>
                                <th>Expediente Académico</th>
                                <th>Informe Matrícula</th>
                                <th>Curriculum</th>
                                <th>Ponderado</th>
                            </tr>
                            </tr>
                        </thead>

                        <asp:Repeater ID="rpDocumentosAsistente" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">

                                    <td>
                                        <%# Eval("nombreCompleto") %>
                                    </td>
                                    <td>
                                        <%# Eval("expediente") %>
                                        <td>
                                            <td>
                                                <%# Eval("informe") %>
                                            </td>
                                            <td>
                                                <%# Eval("ponderado") %>
                                            </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <!-- ---------------------- FIN tabla unidades proyecto  ------------------------- -->
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                    <hr />
                </div>

            </div>
              <!-- Modal Observacion-->
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div id="modalObservacionesAsistente" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Aprobar Asistentes</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <%-- fin titulo accion --%>

                                        <%-- campos a llenar --%>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblNombreAsistente" runat="server" Text="Nombre Asistente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:TextBox class="form-control" ID="txtNombreAsistente" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblCarnet" runat="server" Text="Numero de Carné <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:TextBox class="form-control" ID="txtNumeroCarné" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblCantidadHoras" runat="server" Text="Cantidad de horas nombrado <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:TextBox class="form-control" ID="txtCantidadHoras" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:Label ID="LbEstado" runat="server" Text="Seleccione un estado <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:DropDownList AutoPostBack="true" ID="AsistenteDDL" runat="server" CssClass="form-control" OnSelectedIndexChanged="SeleccionarEstado_OnChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label1" runat="server" Text="Observaciones <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:TextBox class="form-control" ID="txtObservaciones" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                       
                                

                                        <%-- botones --%>
                                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="AprobarAsistente_OnChanged" />
                                           <asp:Button ID="ButtonCerrar" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo" OnClick="btncerrar" />
                                           <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo"    OnClick="botoncerrar"/>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>
                                </div

                            </div>

                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
              <!-- Modal AsistentesAprobacionesPendientes-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="modalAsistentesAprobacionesPendientes" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Asistentes pendientes de Aprobación</h4>
                                </div>
                                  <div style="text-align: right">
                                      <asp:Button ID="Button1" runat="server" Text="Atrás" CssClass="btn btn-primary boton-otro" OnClick=" BotonAtras" />
                                  </div>

                                <div class="modal-body">
                                      <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="label8" runat="server" Text="Seleccionar una Unidad" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                         
                                <asp:DropDownList ID="ddlUnidadesAsistente" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidadAsistente_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                                    <div class="row">
                                        <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                                            <table class="table table-bordered">
                                                <thead style="text-align: center !important; align-content: center">
                                                    <tr style="text-align: center" class="btn-primary">
                                                        <th>Aprobar Nombramiento</th>
                                                        <th>Nombre</th>
                                                        <th>Carné</th>
                                                        <th>Unidad Asistencia</th>
                                                        <th>Último Período Nombrado</th>
                                                        <th>Cantidad de Horas Nombrado</th>
                                                        <th>Cantidad de Períodos Nombrado</th>
                                                        <th>Documentos</th>

                                                    </tr>
                                                </thead>
                                                <tr>
                                                     
                                                    <td></td>
                                                    <td>
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                                        <asp:TextBox ID="txtBuscarNombre1" runat="server" CssClass="form-control chat-input" placeholder="Filtro nombre asistente" AutoPostBack="true" OnTextChanged="filtrarAsistentesPendintes"></asp:TextBox>
                                                    </div>
                                                        </td>
                                                   
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    
                                                </tr>
                                                
                                                     <asp:Repeater ID="RpAprovaciones" runat="server">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <tr style="text-align: center">

                                                             <td>
                                                                <div class="btn-group">
                                                                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("carnet") %>' />
                                                                   <asp:LinkButton ID="btnAsistenteAprobar" runat="server" ToolTip="Seleccionar" CommandArgument='<%# Eval("idAsistente") %>' OnClick="AsistenteAprobar_OnChanged" CssClass="btn  glyphicon glyphicon-ok" />
                                                                </div>
                                                            </td>
                                                            <td><%# Eval("nombreCompleto") %></td>
                                                            <td><%# Eval("carnet") %></td>
                                                            <td><%# Eval("unidad.nombre") %></td>
                                                           
                                                            <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>
                                                            <td><%# Eval("cantidadHorasNombrado") %></td>
                                                            <td><%# Eval("cantidadPeriodosNombrado") %></td>
                                                            <td style="display:none;"><%# Eval("idAsistente") %></td>
                                                            <td>
                                                                <div id="btnDocs" class="btn-group">
                                                                    <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("carnet") %>' />
<%--                                                                    <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" CommandArgument='<%# Eval("carnet") %>'><span id="cambiar" class="glyphicon glyphicon-list-alt"></span></asp:LinkButton>--%>
                                                                     <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" CommandArgument='<%# Eval("idAsistente") %>' OnClick="btnVerArchivo_Click" CssClass="glyphicon glyphicon-list-alt" />
                                                                </div>
                                                            </td>



                                                        </tr>

                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                       
                                         <%--paginación--%>
                <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <center>
                     <table class="table" style="max-width:664px;">
                         <tr style="padding:1px !important">
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbPrimero2" runat="server" CssClass="btn btn-primary" OnClick="lbPrimero2_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbAnterior2" runat="server" CssClass="btn btn-default" OnClick="lbAnterior_Click"><span class="glyphicon glyphicon-backward"></asp:LinkButton>
                             </td>
                              <td style="padding:1px !important">
                                  <asp:DataList ID="rptAsistenteAprobado" runat="server"
                                    OnItemCommand="rptPaginacion2_ItemCommand"
                                    OnItemDataBound="rptPaginacion2_ItemDataBound" RepeatDirection="Horizontal">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbPaginacion" runat="server" CssClass="btn btn-default"
                                            CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                            Text='<%# Eval("PaginaText") %>' ></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:DataList>
                              </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbSiguiente2" CssClass="btn btn-default" runat="server" OnClick="lbSiguiente2_Click"><span class="glyphicon glyphicon-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbUltimo2" CssClass="btn btn-primary" runat="server" OnClick="lbUltimo_Click"><span class="glyphicon glyphicon-fast-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:Label ID="lblpagina2" runat="server" Text=""></asp:Label>
                             </td>
                         </tr>
                     </table>
                 </center>
                </div>

                <%--fn paginación--%>

                                    </div>

                                </div>

                            </div>

                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function observacionesAsistentes() {
            $('#modalObservacionesAsistente').modal('show');
        };
         function activarModalAsistentesAprobacionesPendientes() {
            $('#modalAsistentesAprobacionesPendientes').modal('show');
        };
         function closeModalAsistentes() {
            $('#modalAsistentes').modal('hide');
        }

      </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
