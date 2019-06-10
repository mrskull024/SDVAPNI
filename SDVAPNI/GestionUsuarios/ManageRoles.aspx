<%@ Page Title="Administrar Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRoles.aspx.cs" Inherits="SDVAPNI.GestionUsuarios.ManageRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <br />
    <br />
    <div class="form-horizontal">
        <br />
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <div class="col-lg-10">
                        <strong>Rol</strong>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombreRol" MaxLength="140"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-10">
                        <asp:Button runat="server" Text="Crear Rol" CssClass="btn btn-sm btn-primary" ID="BtnCrearRol" OnClick="BtnCrearRol_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <div class="col-lg-10">
                        <strong>Usuario</strong>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlUsuario"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-10">
                        <strong>Roles</strong>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRoles"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-10">
                        <asp:Button runat="server" Text="Asignar Rol" CssClass="btn btn-sm btn-primary" ID="BtnAsignarRol" />
                    </div>
                </div>
            </div>
        </div>
        <br />

    </div>

</asp:Content>
