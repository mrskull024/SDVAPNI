<%@ Page Title="Reporte Ingreso de Equipos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportesEquipos.aspx.cs" Inherits="SDVAPNI.Gestion.ReportesEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ExternalStyleSheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:Label ID="lblWelcomeUser" runat="server" CssClass="form-control" Style="font-size: medium; color: deepskyblue"></asp:Label>

    <div class="container">
        <div class="row">
            <div class="col-6 col-md-4">
                <asp:TextBox runat="server" ID="TxtDateStart" TextMode="Date" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col-6 col-md-4">
                <asp:TextBox runat="server" ID="TxtDateEnd" TextMode="Date" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col-6 col-md-4">
                <asp:Button runat="server" Text="Generar Datos" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnListData" ClientIDMode="Static" OnClick="BtnListData_Click"/>
            </div>
        </div>
    </div>

    <br />
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-6 col-md-4">
                <asp:Button runat="server" Text="PDF" CssClass="btn btn-danger" data-backdrop="static" data-keyboard="false" ID="BtnDownloadPDF" ClientIDMode="Static" OnClick="BtnDownloadPDF_Click"/>
                <asp:Button runat="server" Text="XLS" CssClass="btn btn-success" data-backdrop="static" data-keyboard="false" ID="BtnDonwloadXLS" ClientIDMode="Static" OnClick="BtnDonwloadXLS_Click"/>
                <asp:Button runat="server" Text="DOC" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnDonwloadDOC" ClientIDMode="Static" OnClick="BtnDonwloadDOC_Click"/>
            </div>
        </div>
    </div>

    <br />
    <br />
    <div class="scrolling-table-container">
        <asp:GridView runat="server" ID="gvEquiposList" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="false" DataKeyNames="id">
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="nombres" HeaderText="NOMBRES" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="compania" ItemStyle-HorizontalAlign="Center" HeaderText="EMPRESA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="cedula" ItemStyle-HorizontalAlign="Center" HeaderText="ID" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="marca" ItemStyle-HorizontalAlign="Center" HeaderText="MARCA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="modelo" ItemStyle-HorizontalAlign="Center" HeaderText="MODELO" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="color" ItemStyle-HorizontalAlign="Center" HeaderText="COLOR" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="serie" ItemStyle-HorizontalAlign="Center" HeaderText="SERIE" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="firma_revision" ItemStyle-HorizontalAlign="Center" HeaderText="REV." HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="fecha_insersion" ItemStyle-HorizontalAlign="Center" HeaderText="REGISTRO" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
            </Columns>
            <PagerSettings FirstPageText="Primera" LastPageText="Última" Mode="NumericFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
        </asp:GridView>
    </div>
</asp:Content>

