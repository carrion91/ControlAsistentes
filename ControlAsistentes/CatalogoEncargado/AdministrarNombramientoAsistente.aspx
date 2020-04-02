<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarNombramientoAsistente.aspx.cs" Inherits="ControlAsistentes.CatalogoEncargado.AdministrarNombramientoAsistente" %>

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
                <p class="mt-1">En esta sección podrá registrar nombramientos de las horas asistentes</p>
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


                <div class="col-md-12 col-xs-6 col-sm-6">

                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="btnNombramiento" runat="server" Text="Nuevo Nombramiento" CssClass="btn btn-primary boton-nuevo" OnClick="btnNombramiento_Click" />
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
                                <th>Período Nombramiento</th>
                                <th>Horas Nombramiento</th>
                                <th>Períodos Nombrado</th>


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
                                    <td>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idAsistente") %>' class="btn glyphicon glyphicon-pencil"></asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idAsistente") %>' class="btn glyphicon glyphicon-trash"></asp:LinkButton>
                                    </td>
                                    <td><%# Eval("nombreCompleto") %></td>
                                    <td><%# Eval("carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnDetalles" runat="server" ToolTip="Detalles" CommandArgument='<%# Eval("carnet") %>'><div class='<%# Eval("nombrado") %>'></div></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>
                                    <td><%# Eval("cantidadHorasNombrado") %></td>
                                    <td><%# Eval("cantidadPeriodosNombrado") %></td>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal nuevo asistente -->

    <contenttemplate>

                    <div id="modalNuevoNombramiento" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nuevo Asistente</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                     
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-xs-12 form-group">
                                    <div class="col-xs-3">
                                        <asp:Label runat="server" Text="Asistente: " Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </div>
                                   
                                </div>
                                          <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbUnidadNombramiento" runat="server" Text="Unidad Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" ></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtUnidadN" runat="server" Width="320px" Enabled="false"></asp:TextBox>
                                            </div>
                                           
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                         <br />
                                        </div>
                                       <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbHoras" runat="server" Text="Horas Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtHorasN" runat="server"></asp:TextBox>
                                            </div>
                                           
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbPeriodoNO" runat="server" Text="Período Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:DropDownList ID="periodosDDL" runat="server" CssClass="form-control" Width="200px">
                                                </asp:DropDownList>
                                            </div>
                                            
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lblInduccion" runat="server" Text="Recibe Inducción " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                             <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:CheckBox ID="ChckBxInduccion" runat="server" />
                                            </div>
                                                 </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <!-- Archivo Expediente -->
                                         <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbExpediente" runat="server" Text="Expediente Académico <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <asp:UpdatePanel ID="updExpediente" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="fileExpediente" runat="server" AllowMultiple="true" CssClass="form-control"  />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Archivos Expediente -->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>

                                        <!-- Archivo Informe-->
                                         <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbInforme" runat="server" Text="Informe de Matrícula " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <asp:UpdatePanel ID="UpInforme" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="fileInforme" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Archivos   Informe -->

                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>

                                        <!-- Archivo CV-->
                                       <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbCV" runat="server" Text="Curriculum VITAE" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <asp:UpdatePanel ID="upCV" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="fileCV" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Archivos CV -->

                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>

                                        <!-- Archivo Cuenta-->
                                        <div id="prueba" class="col-md-12 col-xs-12 col-sm-12" Visible="false" >
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbCuenta" runat="server" Text="Cuenta de Banco" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <asp:UpdatePanel ID="upCuenta" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="fileCuenta" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Archivos Cuenta -->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <!-- Archivos Muestra -->
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lblArchivos" runat="server" Text="Archivos " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                    <asp:FileUpload ID="fuArchivos" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                </div></div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnNuevoAsistenteModal" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-md-5 col-xs-5 col-sm-5" id="divArchivosVacio" runat="server" style="display: none;">
                                                <asp:Label ID="lblArchivosVacio" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- Fin Archivos Muestra -->

                                    </div>
                                </div>
                                <div class="modal-footer" >
                                    <asp:Button ID="btnNuevoAsistenteModal" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="guardarNombramiento_Click" />
                                    <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                </div>

                            </div>

                        </div>
                    </div>
                </contenttemplate>
            <triggers> 
                <asp:PostBackTrigger ControlID="btnNuevoAsistenteModal" />
          </triggers>
    <!-- Fin modal nuevo Nombramiento -->
    <script type="text/javascript">
        function activarModalNuevoNombramiento() {
            $('#modalNuevoNombramiento').modal('show');
        };
    </script>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
