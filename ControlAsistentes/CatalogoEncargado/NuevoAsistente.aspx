<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="NuevoAsistente.aspx.cs" Inherits="ControlAsistentes.CatalogoEncargado.NuevoAsistente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
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
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>


        <%-- campos a llenar --%>
        <div class="col-xs-12">
            <div class="col-xs-3">
                <asp:Label ID="lbNombre" runat="server" Text="Nombre Completo <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div id="div1" runat="server" style="display: none" class="col-xs-5">
                <asp:Label ID="lbNombreC" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>
        <div class="col-xs-12">
            <div class="col-xs-3">
                <asp:Label ID="lbCarnet" runat="server" Text="Carné <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtCarnet" runat="server" Text="" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div id="div2" runat="server" style="display: none" class="col-xs-5">
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
            <div class="col-xs-4">
                <asp:TextBox ID="txtTelefono" runat="server" Text="" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div id="div3" runat="server" style="display: none" class="col-xs-5">
                <asp:Label ID="lblTelefono" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>
        <!-- Archivo Expediente -->
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <asp:Label ID="lblExpediente" runat="server" Text="Expediente Académico " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
            <div class="col-md-3 col-xs-3 col-sm-3">
            </div>
            <div class="col-md-4 col-xs-4 col-sm-4">
                <asp:FileUpload ID="fileExpediente" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
            </div>
            <div class="col-md-5 col-xs-5 col-sm-5" id="divArchivosVacio" runat="server" style="display: none;">
                <asp:Label ID="lbExpedienteVacio" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        </div>
        <!-- Fin Archivos Expediente -->

        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>

        <!-- Archivo Informe-->
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <asp:Label ID="lbInforme" runat="server" Text="lbInforme" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
            <div class="col-md-3 col-xs-3 col-sm-3">
            </div>
            <div class="col-md-4 col-xs-4 col-sm-4">
                <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" oninput="validarArchivos(this);" onchange="validarArchivos(this);" />
            </div>
            <div class="col-md-5 col-xs-5 col-sm-5" id="div4" runat="server" style="display: none;">
                <asp:Label ID="Label2" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Debe seleccionar al menos un archivo" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        </div>
        <!-- Fin Archivos Expediente -->

        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>
        <div class="col-md-6 col-xs-6 col-sm-6 col-lg-6">
            <hr />
        </div>

        <div class="col-md-12 col-xs-12 col-sm-12">
            <div class="col-md-3 col-xs-3 col-sm-3">

                <asp:Label ID="lblEquipo" runat="server" Text="Es un equipo " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4 col-xs-4 col-sm-4">
                <asp:CheckBox ID="ChckBxEquipo" runat="server" />
            </div>

        </div>

        <div class="col-md-6 col-xs-6 col-sm-6 col-lg-6">
            <hr />
        </div>

        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>

        <div id="VerMuestra" runat="server" style="display: block">

            <div class="col-xs-12">
                <div class="col-xs-3">
                    <asp:Label ID="lblTiposMuestra" runat="server" Text="Tipo <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtTipoMuestra" runat="server" Text="" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div id="divTipoMuestraIncorrecto" runat="server" style="display: none" class="col-xs-5">
                    <asp:Label ID="lblTipoMuestraIncorrecto" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Espacio obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-xs-12">
                <br />
                <div class="col-xs-3 col-xs-offset-3">
                    <asp:LinkButton ID="btnTipoMuestra" runat="server" Text="Seleccionar tipo de muestra"></asp:LinkButton>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-xs-12">
                <div class="col-xs-3">
                    <asp:Label ID="lblAreaResponsableMuestra" runat="server" Text="Área responsable " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtAreaResponsableMuestra" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-xs-12">
                <div class="col-xs-3 col-xs-offset-3">
                    <asp:LinkButton ID="btnAreaResponsable" runat="server" Text="Seleccionar área"></asp:LinkButton>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-xs-12">
                <div class="col-xs-3">
                    <asp:Label ID="lblLaboratorioResponsableMuestra" runat="server" Text="Laboratorio responsable " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtLaboratorioResponsableMuestra" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-xs-12">
                <div class="col-xs-3 col-xs-offset-3">
                    <asp:LinkButton ID="btnLaboratorioResponsable" runat="server" Text="Seleccionar laboratorio"></asp:LinkButton>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblDescMuestra" runat="server" Text="Descripción <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtDescMuestra" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div id="divDescMuestraIncorrecto" runat="server" style="display: none" class="col-md-5 col-xs-5 col-sm-5">
                    <asp:Label ID="lblDescMuestraIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblProyecto" runat="server" Text="Proyecto " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtProyecto" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblEntregadoPor" runat="server" Text="Entregado por " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtEntragadoPor" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblPatron" runat="server" Text="Patrón o a granel " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:CheckBox ID="ChckBxPatron" runat="server" />
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblMuestreoHechoPorCliente" runat="server" Text="Muestreo hecho por el cliente " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:CheckBox ID="ChckBxMuestreoHechoPorCliente" runat="server" onchange="Check()" />
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblFechaMuestreo" runat="server" Text="Fecha muestreo <span style='color:red'>**</span> " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4 input-group date" id="divFechaMuestreo">
                    <asp:TextBox CssClass="form-control" ID="txtFechaMuestreo" runat="server" onInput="validarFecha(this)" onChange="validarFecha(this)" onFocus="validarFecha(this)" placeholder="dd/mm/yyyy"></asp:TextBox>
                    <span class="input-group-addon">
                        <span class="fa fa-calendar"></span>
                    </span>
                </div>
                <div class="col-md-5 col-xs-5 col-sm-5" id="divFechaIncorrecta" runat="server" style="display: none;">
                    <asp:Label ID="lblFechaIncorrecta" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Fecha incorrecta" Visible="false" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblLugarMuestreo" runat="server" Text="Lugar de muestreo <span style='color:red'>**</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtLugarMuestreo" runat="server"></asp:TextBox>
                </div>
                <div id="divLugarMuestreoIncorrecto" runat="server" style="display: none" class="col-md-5 col-xs-5 col-sm-5">
                    <asp:Label ID="lblLugarMuestreoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblPrcedimiento" runat="server" Text="Procedimiento <span style='color:red'>**</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtProcedimiento" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div id="divProcedimientoIncorrecto" runat="server" style="display: none" class="col-md-5 col-xs-5 col-sm-5">
                    <asp:Label ID="lblProcedimientoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblCondicionesAmbientales" runat="server" Text="Condiciones ambientales <span style='color:red'>**</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtCondicionesAmbientales" runat="server"></asp:TextBox>
                </div>
                <div id="divCondicionesAmbientalesIncrrectas" runat="server" style="display: none" class="col-md-5 col-xs-5 col-sm-5">
                    <asp:Label ID="lblCondicionesAmbientalesIncorrectas" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtCantidad" runat="server" TextMode="Number" step="0.1"></asp:TextBox>
                </div>
                <div id="divCantidadIncorrecta" runat="server" class="col-md-5 col-xs-5 col-sm-5" style="display: none">
                    <asp:Label ID="lblCantidadIncorrecta" runat="server" Font-Size="Small" class="label alert-danger" Text="Cantidad incorrecta" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <div class="col-xs-12">
                <div class="col-xs-3">
                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-xs-12">
                <br />
            </div>

            <div class="col-xs-12">
                <div class="col-xs-12">
                    <h6 style="text-align: left">Los campos marcados con <span style='color: red'>*</span> son requeridos.</h6>
                    <h6 style="text-align: left">Los campos marcados con <span style='color: red'>**</span> son requeridos solo si la casilla de <strong>Muestreo hecho por el cliente</strong> no está seleccionada.</h6>
                </div>
            </div>

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
    </div>
</asp:Content>
