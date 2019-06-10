<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginSDVAPNI.aspx.cs" Inherits="SDVAPNI.Account.LoginSDVAPNI" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../Content/LoginStyle.css" type="text/css" />
    <link rel="stylesheet" href="../Content/Alert.css" type="text/css" />
    <link rel="stylesheet" href="../Content/LoginStyle.css" type="text/css" />

    <script src="../Scripts/Alert.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>

    <script>
        $(document).ready(function () {
            $('#BtnSigIn').click(function () {
                var user = $("#<%=TxtUser.ClientID%>").val();
                var pass = $("#<%=TxtPassword.ClientID%>").val();
                if (user == "") {
                    ShowMessage('Favor Validar Usuario y Password!', 'Warning');
                    return false;
                }
                if (pass == "") {
                    ShowMessage('Favor Validar Usuario y Password!', 'Warning');
                    return false;
                }
            });
        });
    </script>
    <br />
    <br />
    <br />

    <div class="form-horizontal">
        <div class="row">
            <br />
            <br />
            <br />
            <div class="messagealert" id="alert_container">
            </div>

            <div class="col">
                <div class="panel">
                    <div class="panel-heading">
                        <img src="../Media/logoSVG.svg" alt="" width="330" height="190" />
                    </div>
                    <div class="panel-body">
                        <div class="col">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TxtUser" Text="Usuario"></asp:Label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="TxtUser" AutoCompleteType="Disabled" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TxtPassword" Text="Contrasena"></asp:Label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="TxtPassword" AutoCompleteType="Disabled" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <asp:Button runat="server" Text="Iniciar Sesion" ID="Button1" CssClass="btn btn-primary btn-sm" ClientIDMode="Static" OnClick="BtnSigIn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
