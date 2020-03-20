<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarAsistentesEncargado.aspx.cs" Inherits="ControlAsistentes.CatalogoEncargado.AdministrarAsistentesEncargado" %>

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
                <asp:Label runat="server" Text="Administración de Asistentes" Font-Size="Large" ForeColor="Black"></asp:Label>
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



                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>
                <div class="col-md-12 col-xs-6 col-sm-6">

                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="btnNuevoAsistente" runat="server" Text="Nuevo Asistente" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevoAsistente_Click" />
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
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" OnClick="filtrarAsistentes"><span aria-hidden="true" class="glyphicon glyphicon-search"></span> </asp:LinkButton></td>
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

                        </tr>
                        <asp:Repeater ID="rpAsistentes" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">


                                    <td></td>
                                    <td><%# Eval("nombreCompleto") %></td>
                                    <td><%# Eval("carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <% if (true == true)
                                        {
                                    %>
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnDetalles" runat="server" ToolTip="Detalles" CommandArgument='<%# Eval("carnet") %>'><span class="btn glyphicon glyphicon-ok-sign"></span></asp:LinkButton>
                                        </div>
                                    </td>
                                    <% }
                                        else
                                        {
                                    %>
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnDettalesX" runat="server" ToolTip="Detalles" CommandArgument='<%# Eval("carnet") %>'><span class="btn glyphicon glyphicon-remove-sign"></span></asp:LinkButton>
                                        </div>
                                    </td>
                                    <% }
                                    %>
                                    <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>
                                    <td><%# Eval("cantidadHorasNombrado") %></td>
                                    <td><%# Eval("cantidadPeriodosNombrado") %></td>
                                    <td>
                                        <div id="btnDocs" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" CommandArgument='<%# Eval("carnet") %>'><span id="cambiar" class="glyphicon glyphicon-list-alt"></span></asp:LinkButton>
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="modalNuevoAsistente1" class="modal fade" role="alertdialog">
                        <%-- titulo accion--%>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <center>
                        <asp:Label ID="lblNuevoAsistente" runat="server" Text="Nuevo Asistente" Font-Size="Large" ForeColor="Black"></asp:Label>
                    </center>
                        </div>
                        <%-- fin titulo accion --%>

                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                            <hr />
                        </div>

                        <%-- campos a llenar --%>







                        <!-- Archivos Muestra -->
                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lblArchivos" runat="server" Text="Archivos " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4 col-sm-4">
                                <asp:FileUpload ID="fuArchivos" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
                            </div>
                            <div class="col-md-5 col-xs-5 col-sm-5" id="divArchivosVacio" runat="server" style="display: none;">
                                <asp:Label ID="lblArchivosVacio" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <!-- Fin Archivos Muestra -->



                        <%-- fin campos a llenar --%>

                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                            <hr />
                        </div>

                        <%-- botones --%>
                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" />
                        </div>
                        <%-- fin botones --%>
                    </div>
                   
                </ContentTemplate>
            </asp:UpdatePanel>



            <!-- Modal nuevo periodo -->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div id="modalNuevoAsistente" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nuevo Asistente</h4>
                                </div>
                                <div class="modal-body">
                                    <%-- campos a llenar --%>
                                    <div class="row">

                                        <%-- fin campos a llenar --%>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="label4" runat="server" Text="Nombre Completo <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbCarnet" runat="server" Text="Carné <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtCarnet" runat="server"></asp:TextBox>
                                            </div>
                                            <div id="div5" runat="server" style="display: none" class="col-xs-5">
                                                <asp:Label ID="lbCarne" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbTelefono" runat="server" Text="Numéro Teléfono <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtTelefono" runat="server"></asp:TextBox>
                                            </div>
                                            <div id="div3" runat="server" style="display: none" class="col-xs-5">
                                                <asp:Label ID="lblTelefono" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbHoras" runat="server" Text="Horas Nombramiento<span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtHoras" runat="server"></asp:TextBox>
                                            </div>
                                            <div id="div7" runat="server" style="display: none" class="col-xs-5">
                                                <asp:Label ID="lblHoras" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbPeriodoN" runat="server" Text="Período  Nombramiento<span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-xs-4">
                                                <asp:DropDownList ID="periodosDDL" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div id="div8" runat="server" style="display: none" class="col-xs-5">
                                                <asp:Label ID="lblPeriodoN" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-md-3 col-xs-3 col-sm-3">

                                                <asp:Label ID="lblInducción" runat="server" Text="Recibe Inducción " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:CheckBox ID="ChckBxInduccion" runat="server" />
                                            </div>

                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <!-- Archivo Expediente -->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">

                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbExpe" runat="server" Text="Expediente" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:FileUpload ID="fileExpediente" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
                                            </div>
                                            <div class="col-md-5 col-xs-5 col-sm-5" id="div1" runat="server" style="display: none;">
                                                <asp:Label ID="lbExpedienteVacio" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- Fin Archivos Expediente -->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>

                                        <!-- Archivo Informe-->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbInforme" runat="server" Text="Informe de Matícula" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:FileUpload ID="fileInforme" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
                                            </div>
                                            <div class="col-md-5 col-xs-5 col-sm-5" id="div2" runat="server" style="display: none;">
                                                <asp:Label ID="lbInf" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- Fin Archivos   Informe -->

                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>

                                        <!-- Archivo CV-->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbCV" runat="server" Text="Curriculum VITAE" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:FileUpload ID="fileCV" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
                                            </div>
                                            <div class="col-md-5 col-xs-5 col-sm-5" id="div6" runat="server" style="display: none;">
                                                <asp:Label ID="lbCV1" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- Fin Archivos CV -->

                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>

                                        <!-- Archivo Cuenta-->
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbCuenta" runat="server" Text="Cuenta del Banco" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:FileUpload ID="fileCuenta" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
                                            </div>
                                            <div class="col-md-5 col-xs-5 col-sm-5" id="div9" runat="server" style="display: none;">
                                                <asp:Label ID="lblCuenta" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- Fin Archivos Cuenta -->


                                    </div>
                                </div>
                                <div class="modal-footer col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9" style="text-align: center">
                                    <asp:Button ID="btnGuardarA" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="guardarNuevoAsistente_Click" />
                                    <asp:Button ID="btnCancelarA" runat="server" Text="Cancelar" CssClass="btn btn-danger" />
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Fin modal nuevo periodo -->

        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- Script inicio -->
    <script type="text/javascript">
        function activarModalNuevoAsistente() {
            $('#modalNuevoAsistente').modal('show');
        };

        function validarArchivos(fileUpload) {
            var id = fileUpload.id.substring(12);

            var divArchivoIncorrecta = document.getElementById('<%= divArchivosVacio.ClientID %>');

            if (fileUpload.files.length > 0) {
                divArchivoIncorrecta.style.display = "none";
            } else {
                divArchivoIncorrecta.style.display = "block";
            }
        };
    </script>
    <!-- Script fin -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
