<%@ Page Title="Ingreso de Equipos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresoEquipos.aspx.cs" Inherits="SDVAPNI.Gestion.IngresoEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ExternalStyleSheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" type="text/css">
    <link rel="stylesheet" type="text/css" href="../Content/Alert.css" />
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap.min.css" />

    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Alert.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#BtnFindByCedula').click(function () {
            
                
            });

            $('#BtnRegistraEquipo').click(function () {
                if ($("#<%= TxtNombres.ClientID%>").val() == "") {
                    ShowMessage('El campo Nombres no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtCompania.ClientID%>").val() == "") {
                    ShowMessage('El campo Empresa no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtCedula.ClientID%>").val() == "") {
                    ShowMessage('El campo Cedula no puede estar vacio', 'Info');
                    return false;
                }

                if ($('#DpdMarca :selected').val() == "Debes Seleccionar una Marca") {
                    ShowMessage('Debes Seleccionar un modelo', 'Info');
                    return false;
                }

                if ($("#<%= TxtModelo.ClientID%>").val() == "") {
                    ShowMessage('El campo Modelo no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtColor.ClientID%>").val() == "") {
                    ShowMessage('El campo Color no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtSerie.ClientID%>").val() == "") {
                    ShowMessage('El campo Serie no puede estar vacio', 'Info');
                    return false;
                }

                if ($("#<%= TxtPersonaVisita.ClientID%>").val() == "") {
                    ShowMessage('El Campo Referencia no puede estar vacio', 'Info');
                    return false;
                }               
            });
        });

        function PrintFormat() {
            window.print();           
        }
    </script>
    <br />
    <br />

    <h4 class="text-center">AIRPAK NICARAGUA / GRUPO COEN</h4>
    <h4 class="text-center">CONTROL DE INGRESO EQUIPO DE COMPUTO VISITANTES</h4>

    <div class="container" style="border: solid 1px; margin: auto;">
        <h6 class="text-center">---[ Revision de Ingreso ]---</h6>
        <div class="row">
            <div class="messagealert" id="alert_container">
            </div>

            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtFecha" CssClass="form-control" Style="font-size: medium">FECHA</asp:Label>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtNombres" CssClass="form-control" Style="font-size: medium">NOMBRES</asp:Label>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtCompania" CssClass="form-control" Style="font-size: medium">COMPANIA</asp:Label>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtCedula" CssClass="form-control" Style="font-size: medium">CEDULA</asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFecha" TextMode="Date" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtNombres" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtCompania" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtCedula" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <br />
    </div>
    <br />
    <div class="container" style="border: solid 1px;">
        <h6 class="text-center">---[ Datos del Equipo ]---</h6>
        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="DpdMarca" CssClass="form-control" Style="font-size: medium">MARCA</asp:Label>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtModelo" CssClass="form-control" Style="font-size: medium">MODELO</asp:Label>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtColor" CssClass="form-control" Style="font-size: medium">COLOR</asp:Label>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtSerie" CssClass="form-control" Style="font-size: medium">SERIE</asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:DropDownList runat="server" ID="DpdMarca" CssClass="form-control" ClientIDMode="Static">
                    <asp:ListItem Text="Debes Seleccionar una Marca" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="APPLE (MAC)" Value="1"></asp:ListItem>
                    <asp:ListItem Text="DELL" Value="2"></asp:ListItem>
                    <asp:ListItem Text="LENOVO" Value="3"></asp:ListItem>
                    <asp:ListItem Text="HP" Value="4"></asp:ListItem>
                    <asp:ListItem Text="LG" Value="5"></asp:ListItem>
                    <asp:ListItem Text="ACER" Value="6"></asp:ListItem>
                    <asp:ListItem Text="SAMSUNG" Value="7"></asp:ListItem>
                    <asp:ListItem Text="ASUS" Value="8"></asp:ListItem>
                    <asp:ListItem Text="MSI" Value="9"></asp:ListItem>
                    <asp:ListItem Text="XIAOMI" Value="10"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtModelo" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtColor" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtSerie" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <br />
    </div>
    <div class="container" style="border: solid 1px;">
        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtPersonaVisita" CssClass="form-control" Style="font-size: medium">A QUE PERSONA VISITA</asp:Label>
            </div>
            <div class="col-9">
                <asp:TextBox runat="server" ID="TxtPersonaVisita" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="container" style="border: solid 1px;">
        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtFirma" CssClass="form-control" Style="font-size: medium">FIRMA REVISION</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFirma" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtFirmaPortador" CssClass="form-control" Style="font-size: medium">FIRMA PORTADOR</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFirmaPortador" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        </div>
    </div>
    <br />
    <div class="container" style="border: solid 1px; margin: auto;">
        <h6 class="text-center">---[ Revision de Salida ]---</h6>
        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="DpdOption1" CssClass="form-control" Style="font-size: medium">Misma Marca</asp:Label>
            </div>
            <div class="col">
                <asp:DropDownList runat="server" ID="DpdOption1" CssClass="form-control" ClientIDMode="Static">
                    <asp:ListItem Text="Debes Seleccionar una Opcion" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                    <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="DpdOption2" CssClass="form-control" Style="font-size: medium">Mismo Color</asp:Label>
            </div>
            <div class="col">
                <asp:DropDownList runat="server" ID="DpdOption2" CssClass="form-control" ClientIDMode="Static">
                    <asp:ListItem Text="Debes Seleccionar una Opcion" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                    <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                 </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="DpdOption3" CssClass="form-control" Style="font-size: medium">Mismo Modelo</asp:Label>
            </div>
            <div class="col">
                <asp:DropDownList runat="server" ID="DpdOption3" CssClass="form-control" ClientIDMode="Static">
                    <asp:ListItem Text="Debes Seleccionar una Opcion" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                    <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="DpdOption4" CssClass="form-control" Style="font-size: medium">Mismo # de Serie</asp:Label>
            </div>
            <div class="col">
                <asp:DropDownList runat="server" ID="DpdOption4" CssClass="form-control" ClientIDMode="Static">
                    <asp:ListItem Text="Debes Seleccionar una Opcion" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                    <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtFechaSalida" CssClass="form-control" Style="font-size: medium">Fecha de Salida</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFechaSalida" TextMode="Date" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtHoraSalida" CssClass="form-control" Style="font-size: medium">Hora de Salida</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtHoraSalida" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtFirmaQuienRevisa" CssClass="form-control" Style="font-size: medium">Nombre y Firma Revision</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFirmaQuienRevisa" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col">
                <asp:Label runat="server" AssociatedControlID="TxtFirmaPortadorDelEquipo" CssClass="form-control" Style="font-size: medium">Firma Portador del Equipo</asp:Label>
            </div>
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFirmaPortadorDelEquipo" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <asp:TextBox runat="server" ID="TxtFindByCedula" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>       
            </div>
            <div class="col">
                <br />
                <asp:Button runat="server" Text="Buscar X Cedula" CssClass="btn btn-primary btn-sm" ID="BtnFindByCedula" ClientIDMode="Static" OnClick="BtnFindByCedula_Click"/>
                <br />
            </div>
            <div class="col text-center">
                <asp:Button runat="server" Text="Registrar" CssClass="btn btn-primary" data-backdrop="static" data-keyboard="false" ID="BtnRegistraEquipo" ClientIDMode="Static" OnClick="BtnRegistraEquipo_Click" />
            </div>
            <div class="col text-center">
                <asp:Button runat="server" Text="Limpiar Campos" CssClass="btn btn-info" data-backdrop="static" data-keyboard="false" ID="BtnLimpiaEquipo" ClientIDMode="Static" OnClick="BtnLimpiaEquipo_Click" />
            </div>
            <div class="col text-center">
                <asp:Button runat="server" Text="Imprimir Formato" CssClass="btn btn-info" ID="BtnPrintService" OnClientClick="return PrintFormat();"/>
            </div>
        </div>
    </div>
</asp:Content>