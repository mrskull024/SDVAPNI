<%@ Page Title="Administrar Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsuarios.aspx.cs" Inherits="SDVAPNI.GestionUsuarios.ManageUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css">
    <link rel="stylesheet" type="text/css" href="../Content/Alerts.css" />
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap.min.css" />

    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Alert.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>

    <script>
        $(document).ready(function () {
            $('#BtnRegistrar').click(function () {

                if ($("#<%= TxtUsuario.ClientID%>").val() == "") {
                    ShowMessage('El campo Usuario no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtPassword1.ClientID%>").val() == "") {
                    ShowMessage('El campo Contraseña no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtPassword2.ClientID%>").val() == "") {
                    ShowMessage('Debes Repetir la Contraseña', 'Info');
                    return false;
                }

                if ($("#<%= TxtPassword1.ClientID%>").val().trim() != $("#<%= TxtPassword2.ClientID%>").val().trim()) {
                    ShowMessage('Las contraseñas deben coincidir', 'Info');
                    return false;
                }

                if ($("#<%= TxtEmail.ClientID%>").val() == "") {
                    ShowMessage('El campo Correo no puede estar Vacio', 'Info');
                }

                if ($("#<%= TxtPhoneNumber.ClientID%>").val() == "") {
                    ShowMessage('El Campo Telefono no puede estar vacio', 'Info')
                }

                if ($('#DpdRoleId :selected').val() == "0") {
                    ShowMessage('Debes Seleccionar un Rol', 'Info');
                    return false;
                }

            });


            $('#BtnModificarCredenciales').click(function () {
                if ($("#<%= TxtUsuarioNew.ClientID%>").val() == "") {
                    ShowMessage('El campo Usuario no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtPassword1New.ClientID%>").val() == "") {
                    ShowMessage('El campo Contraseña no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtPassword2New.ClientID%>").val() == "") {
                    ShowMessage('Debes Repetir la Contraseña', 'Info');
                    return false;
                }

                if ($("#<%= TxtPassword1New.ClientID%>").val().trim() != $("#<%= TxtPassword2New.ClientID%>").val().trim()) {
                    ShowMessage('Las contraseñas deben coincidir', 'Info');
                    return false;
                }

            });

        });

        function MostrarModalUsuario() {
            $("#ModalNewUsuario").modal({
                backdrop: 'static',
                keyboard: false
            });
        }

        function MostrarModalResetearCredenciales() {
            $("#ModalResetearCredenciales").modal({
                backdrop: 'static',
                keyboard: false
            });
        }

    </script>

    <br />
    <br />
    <asp:Label ID="lblWelcomeUser" runat="server" CssClass="form-control" Style="font-size: medium; color: deepskyblue"></asp:Label>

    <div>
        <button type="button" class="btn btn-outline-success" data-target="#ModalNewUsuario" data-toggle="modal" data-backdrop="static" data-keyboard="false"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Registrar Nuevo</button>
    </div>

    <br />
    <br />
    <div class="scrolling-table-container">
        <asp:GridView runat="server" ID="gvUserRols" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" OnRowCommand="gvUserRols_RowCommand">
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="Rol" ItemStyle-HorizontalAlign="Center" HeaderText="ROL" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="UserName" ItemStyle-HorizontalAlign="Center" HeaderText="USUARIO" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                 <asp:BoundField DataField="CompleteName" ItemStyle-HorizontalAlign="Center" HeaderText="NOMBRES" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="Email" ItemStyle-HorizontalAlign="Center" HeaderText="CORREO" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:BoundField DataField="PhoneNumber" ItemStyle-HorizontalAlign="Center" HeaderText="TELEFONO" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White" />
                <asp:TemplateField HeaderText="ACCIÓN" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="badge-secondary text-center" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Seleccionar" CausesValidation="false" CssClass="btn btn-primary" ToolTip="Seleccionar"><span class="fas fa-check"></span></asp:LinkButton>
                        <asp:LinkButton runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex%>" CommandName="Desactivar" CausesValidation="false" CssClass="btn btn-danger" ToolTip="Eliminar"><span class="fas fa-user-times"></span></asp:LinkButton>
                        <asp:LinkButton runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex%>" CommandName="Editar" CausesValidation="false" CssClass="btn btn-warning" ToolTip="Resetear Credenciales" ClientIDMode="Static" ID="lbEditar"><span class="fas fa-sync-alt"></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="Primera" LastPageText="Última" Mode="NumericFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
        </asp:GridView>
    </div>

    <%--Modal para anadir Nuevos Usuarios--%>
    <div class="modal" id="ModalNewUsuario">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registrar Nuevo Usuario</h5>
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
                                <asp:Label runat="server" AssociatedControlID="TxtFullName" CssClass="form-control" Style="font-size: medium">Nombres:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox style="text-transform:uppercase" runat="server" ID="TxtFullName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtUsuario" CssClass="form-control" Style="font-size: medium">Usuario:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtUsuario" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtPassword1" CssClass="form-control" Style="font-size: medium">Contraseña:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtPassword1" CssClass="form-control" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtPassword2" CssClass="form-control" Style="font-size: medium">Repetir:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtPassword2" CssClass="form-control" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtEmail" CssClass="form-control" Style="font-size: medium">Correo:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control" TextMode="Email" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtPhoneNumber" CssClass="form-control" Style="font-size: medium">Telefono:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtPhoneNumber" CssClass="form-control" TextMode="Phone" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <span runat="server" class="input-group-text" style="font-size: medium;">Elegir Rol:</span>
                            </div>
                            <div class="col">
                                <asp:DropDownList runat="server" ID="DpdRoleId" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Registrar" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnRegistrar" ClientIDMode="Static" OnClick="BtnRegistrar_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" data-keyboard="false">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <%--Modal para anadir Resetear Credenciales de Usuarios--%>
    <div class="modal" id="ModalResetearCredenciales">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registrar Nuevo Usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="messagealert" id="alert_container2">
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtUsuarioNew" CssClass="form-control" Style="font-size: medium">Usuario:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtUsuarioNew" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtPassword1New" CssClass="form-control" Style="font-size: medium">Contraseña:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtPassword1New" CssClass="form-control" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row">
                            <div class="col-3">
                                <asp:Label runat="server" AssociatedControlID="TxtPassword2New" CssClass="form-control" Style="font-size: medium">Repetir:</asp:Label>
                            </div>
                            <div class="col">
                                <asp:TextBox runat="server" ID="TxtPassword2New" CssClass="form-control" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Actualizar Password" CssClass="btn btn-primary" ID="BtnModificarCredenciales" ClientIDMode="Static" OnClick="BtnModificarCredenciales_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" data-keyboard="false">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>