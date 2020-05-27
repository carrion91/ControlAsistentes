<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ReporteEncargadoAsistente.aspx.cs" Inherits="ControlAsistentes.Informes.ReporteEncargado.ReporteEncargadoAsistente" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>
    <div class="col-md-6 col-xs-6 col-sm-6">
        <h4>Unidad</h4>
        <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true"></asp:DropDownList>
    </div>
    <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
        <br />
    </div>
    <div class="col-md-6 col-xs-6 col-sm-6">
        <h4>Periodo</h4>
        <asp:DropDownList ID="ddlPeriodo" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true"></asp:DropDownList>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
        <LocalReport ReportPath="Informes\ReporteEncargado\ReporteEncargadoAsistenterdlc.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ReporteEncargadoAsistente" />
            </DataSources>
        </LocalReport>
      </rsweb:ReportViewer>
      <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ObtenerAsistentes" TypeName="Servicios.AsistenteServicios"></asp:ObjectDataSource>
    <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
        <br />
    </div>
    
    </asp:Content>