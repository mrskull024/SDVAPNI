<%@ Page Title="Modulo Visitas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Visitas.aspx.cs" Inherits="SDVAPNI.Gestion.Visitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" type="text/css">
    <link rel="stylesheet" type="text/css" href="../Content/Alert.css" />
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap.min.css" />

    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Alert.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>

    <script>
        $(document).ready(function () {
            $('#BtnRegistrarVisita').click(function () {

                if ($("#<%= TxtCedula.ClientID%>").val() == "") {
                    ShowMessage('El campo Cedula no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtNombres.ClientID%>").val() == "") {
                    ShowMessage('El campo Nombres no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtEmpresa.ClientID%>").val() == "") {
                    ShowMessage('El campo Empresa no puede estar vacio', 'Info');
                    return false;
                }

                if ($('#DpdOpcionesAreas :selected').val() == "Debes Seleccionar un Area") {
                    ShowMessage('Debes Seleccionar un Area de Visita', 'Info');
                    return false;
                }

                if ($("#<%= TxtReferencia.ClientID%>").val() == "") {
                    ShowMessage('El campo Referencia de Visita no puede estar Vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtFechaRegistro.ClientID%>").val() == "dd/mm/aaaa") {
                    ShowMessage('Debes Seleccionar una Fecha', 'Info');
                    return false;
                }

                if ($("#<%= TxtRegistradoPor.ClientID%>").val() == "") {
                    ShowMessage('El Campo Registrado Por no puede estar vacio', 'Info');
                    return false;
                }

            });
        });

        function MostrarModalVisita() {
            $("#ModalNewVisit").modal({
                backdrop: 'static',
                keyboard: false
            });
        }

    </script>

    <br />
    <br />
    <br />
    <div>
        <button type="button" class="btn btn-outline-success" data-target="#ModalNewVisit" data-toggle="modal" data-backdrop="static" data-keyboard="false"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Registrar Nuevo</button>
    </div>

    <br />
    <br />
    <div class="scrolling-table-container">
        <asp:GridView runat="server" ID="gvVisitsList" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvVisitsList_PageIndexChanging" DataKeyNames="id" OnRowCommand="gvVisitsList_RowCommand" PageSize="5">
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" HeaderStyle-CssClass="badge-secondary text-center hide" HeaderStyle-ForeColor="White" Visible="false" />
                <asp:BoundField DataField="cedula" HeaderText="CEDULA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="nombre" ItemStyle-HorizontalAlign="Center" HeaderText="NOMBRE" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="empresa" ItemStyle-HorizontalAlign="Center" HeaderText="EMPRESA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="area_visita" ItemStyle-HorizontalAlign="Center" HeaderText="AREA VISITA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="ref_visita" ItemStyle-HorizontalAlign="Center" HeaderText="REFERENCIA" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="fecha_registro" ItemStyle-HorizontalAlign="Center" HeaderText="REGISTRO" HeaderStyle-CssClass="badge-secondary text-center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="registrado_por" ItemStyle-HorizontalAlign="Center" HeaderText="POR" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:TemplateField HeaderText="ACCIÓN" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Seleccionar" CausesValidation="false" CssClass="btn btn-primary btn-sm" ToolTip="Seleccionar"><span class="fas fa-check"></span></asp:LinkButton>
                        <asp:LinkButton runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex%>" CommandName="Eliminar" CausesValidation="false" CssClass="btn btn-danger btn-sm" ToolTip="Eliminar"><span class="fas fa-user-times"></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="Primera" LastPageText="Última" Mode="NumericFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
        </asp:GridView>
    </div>

    <%--Modal para anadir una nueva visita--%>
    <div class="modal" id="ModalNewVisit">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registrar Nueva Visita</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="messagealert" id="alert_container">
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtCedula" CssClass="form-control" Style="font-size: medium">Cedula No:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtCedula" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-3">
                                <asp:Button runat="server" Text="Buscar" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnBuscarXCedula" ClientIDMode="Static" OnClick="BtnBuscarXCedula_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtNombres" CssClass="form-control" Style="font-size: medium">Nombres:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtNombres" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtEmpresa" CssClass="form-control" Style="font-size: medium">Empresa:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtEmpresa" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="DpdOpcionesAreas" CssClass="form-control" Style="font-size: medium">Area Visita:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:DropDownList runat="server" ID="DpdOpcionesAreas" CssClass="form-control" ClientIDMode="Static">
                                    <asp:ListItem Text="Debes Seleccionar un Area" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="TÉCNOLOGIA AIRPAK" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="TÉCNOLOGIA CESCO" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="TALENTO HUMANO" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="CONTABILIDAD" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="ARCHIVO" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="GERENCIA" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="TESORERIA" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="CASHPAK" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="MANTENIMIENTO" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="COMERCIAL" Value="10"></asp:ListItem>
									<asp:ListItem Text="LEGAL" Value="11"></asp:ListItem>
									<asp:ListItem Text="CUMPLIMIENTO" Value="12"></asp:ListItem>
									<asp:ListItem Text="INFRAESTRUCTURA" Value="13"></asp:ListItem>
									<asp:ListItem Text="CONTRALORIA" Value="14"></asp:ListItem>
									<asp:ListItem Text="CONCILIACIÓN" Value="15"></asp:ListItem>
									<asp:ListItem Text="CALL CENTER" Value="16"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtReferencia" CssClass="form-control" Style="font-size: medium">Ref Visita:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtReferencia" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtFechaRegistro" CssClass="form-control" Style="font-size: medium">Fecha Reg.:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtFechaRegistro" TextMode="Date" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtRegistradoPor" CssClass="form-control" Style="font-size: medium">Reg. Por:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtRegistradoPor" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Registrar" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnRegistrarVisita" ClientIDMode="Static" OnClick="BtnRegistrarVisita_Click" />
                    <asp:Button runat="server" Text="Editar" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnEditarVisita" ClientIDMode="Static" OnClick="BtnEditarVisita_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" data-keyboard="false">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
