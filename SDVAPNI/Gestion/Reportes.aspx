<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="SDVAPNI.Gestion.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ExternalStyleSheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />
    <br />
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
                <asp:Button runat="server" Text="PDF" CssClass="btn btn-danger" data-backdrop="static" data-keyboard="false" ID="BtnDownloadPDF" ClientIDMode="Static" OnClick="BtnDownloadPDF_Click" />
                <asp:Button runat="server" Text="XLS" CssClass="btn btn-success" data-backdrop="static" data-keyboard="false" ID="BtnDonwloadXLS" ClientIDMode="Static" OnClick="BtnDonwloadXLS_Click" />
                <asp:Button runat="server" Text="DOC" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnDonwloadDOC" ClientIDMode="Static" OnClick="BtnDonwloadDOC_Click"  />
            </div>
        </div>
    </div>


    <br />
    <br />
    <div class="scrolling-table-container">
        <asp:GridView runat="server" ID="gvVisitsList" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="false" DataKeyNames="id">
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="cedula" HeaderText="CEDULA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="nombre" ItemStyle-HorizontalAlign="Center" HeaderText="NOMBRE" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="empresa" ItemStyle-HorizontalAlign="Center" HeaderText="EMPRESA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="area_visita" ItemStyle-HorizontalAlign="Center" HeaderText="AREA VISITA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="ref_visita" ItemStyle-HorizontalAlign="Center" HeaderText="REFERENCIA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <%--<asp:BoundField DataField="fecha_registro" ItemStyle-HorizontalAlign="Center" HeaderText="REGISTRO" HeaderStyle-CssClass="badge-secondary text-center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-ForeColor="White" />--%>
                <asp:BoundField DataField="registrado_por" ItemStyle-HorizontalAlign="Center" HeaderText="POR" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="fecha_insercion" ItemStyle-HorizontalAlign="Center" HeaderText="Registro Exacto" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
            </Columns>
            <PagerSettings FirstPageText="Primera" LastPageText="Última" Mode="NumericFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
        </asp:GridView>
    </div>
</asp:Content>
