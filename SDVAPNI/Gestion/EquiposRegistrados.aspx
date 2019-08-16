<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EquiposRegistrados.aspx.cs" Inherits="SDVAPNI.Gestion.EquiposRegistrados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ExternalStyleSheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" type="text/css">
    <link rel="stylesheet" type="text/css" href="../Content/Alert.css" />
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap.min.css" />

    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Alert.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script>
        function myFunction() {
            location.replace("http://sdvapni.local/Gestion/IngresoEquipos")
        }
    </script>

    <br />
    <br />
    <asp:Label ID="lblWelcomeUser" runat="server" CssClass="form-control" Style="font-size: medium; color: deepskyblue"></asp:Label>

    <div>
        <button type="button" class="btn btn-outline-success" onclick="myFunction()" data-keyboard="false"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Registrar Nuevo</button>
    </div>

    <br />
    <br />
    <div class="scrolling-table-container">
        <asp:GridView runat="server" ID="gvEquiposRegistrados" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvEquiposRegistrados_PageIndexChanging" DataKeyNames="id" PageSize="10">
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" HeaderStyle-CssClass="badge-secondary text-center hide" HeaderStyle-ForeColor="White" Visible="false" />
                <asp:BoundField DataField="fecha_inicial" HeaderText="FECHA" HeaderStyle-CssClass="badge-secondary text-center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="nombres" ItemStyle-HorizontalAlign="Center" HeaderText="NOMBRES" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="compania" ItemStyle-HorizontalAlign="Center" HeaderText="EMPRESA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="cedula" ItemStyle-HorizontalAlign="Center" HeaderText="ID" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="marca" ItemStyle-HorizontalAlign="Center" HeaderText="MARCA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="modelo" ItemStyle-HorizontalAlign="Center" HeaderText="MODELO" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="color" ItemStyle-HorizontalAlign="Center" HeaderText="COLOR" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="serie" ItemStyle-HorizontalAlign="Center" HeaderText="SERIE" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="referencia_visita" ItemStyle-HorizontalAlign="Center" HeaderText="REFERENCIA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="firma_revision" ItemStyle-HorizontalAlign="Center" HeaderText="POR" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
            </Columns>
            <PagerSettings FirstPageText="Primera" LastPageText="Última" Mode="NumericFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
        </asp:GridView>
    </div>
</asp:Content>
