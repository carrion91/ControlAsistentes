<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master"  AutoEventWireup="true" CodeBehind="Periodo.aspx.cs" Inherits="ControlAsistentes.Catalogos.Periodo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <center>
            <asp:Label ID="lblAdministrarAistentes" runat="server" Text="Asistentes" Font-Size="Large" Font-Names="Bookman Old Style" Font-Bold="true"></asp:Label>
        </center>
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <hr />
        </div>

        <div class="col-md-6 col-xs-6 col-sm-6">
            <h4>Unidad</h4>
           <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" ></asp:DropDownList>
       </div>
        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
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
                                
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" ><span aria-hidden="true" class="glyphicon glyphicon-search"></span> </asp:LinkButton></td>
                            <td> 
                                <asp:TextBox ID="txtBuscarDesc" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true" ></asp:TextBox>
                            </td>
                            <td></td>
                            <td>
                              <div class="col-md-3 col-xs-4 col-sm-4">
                                </div>
                                <div class="col-md-5 col-xs-4 col-sm-4">
                                    <asp:DropDownList ID="ddlBuscarTipo" class="btn btn-default dropdown-toggle" runat="server"  AutoPostBack="true"></asp:DropDownList>                          
                                </div>

                               
                            </td>
                        </tr>
                        <asp:Repeater ID="rpPartidas" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">
                                    <td>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" ><span class="btn glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" ><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                                    </td>
                                    <td>
                                       
                                    </td>
                                    <td> 
                                        
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>  
    </div>
    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
