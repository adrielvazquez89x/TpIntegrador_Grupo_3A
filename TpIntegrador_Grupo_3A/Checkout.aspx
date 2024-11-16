<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager runat="server" />--%>

<h1 class="text-center mt-5" id="h1Confirmar">Confirmar Compra</h1>
<%-- Primera parte del formulario, datos personales --%>
<div class="d-flex gap-5">
    <div class="col-6">
        <div class="d-flex flex-column">
            <h3>Completa el formulario para finalizar la compra:</h3>
            <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" placeholder="Nombre" CssClass="form-control my-1"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" />
            <span id="spanNombreConfirmar"></span>
            <asp:TextBox ID="txtDni" ClientIDMode="Static" runat="server" placeholder="Dni" CssClass="form-control my-1" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDni" runat="server" />
            <span id="spanDni"></span>
            <asp:TextBox ID="txtDireccion" ClientIDMode="Static" runat="server" placeholder="Direccion" CssClass="form-control my-1"></asp:TextBox>
            <span id="spanDireccion"></span>
            <asp:RequiredFieldValidator ControlToValidate="txtDireccion" runat="server" />
            <asp:TextBox ID="txtLocalidad" ClientIDMode="Static" runat="server" placeholder="Localidad" CssClass="form-control my-1"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtLocalidad" runat="server" />
            <span id="spanLocalidad"></span>
            <asp:TextBox ID="txtCP" ClientIDMode="Static" runat="server" placeholder="Codigo postal" CssClass="form-control my-1" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtCP" runat="server" />
            <span id="spanCp"></span>
        </div>
        <div>
            <h4>Elija método de pago</h4>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="d-flex flex-column col-8">
                        <asp:DropDownList ID="ddlMetodoPago" ClientIDMode="Static" runat="server" AutoPostBack="true"
                            CssClass="form-control my-1">
                            <asp:ListItem Value="1">Efectivo</asp:ListItem>
                            <asp:ListItem Value="2">Tarjeta</asp:ListItem>
                        </asp:DropDownList>

                        <% if (ddlMetodoPago.SelectedItem.Value == "Tarjeta")
                            {%>
                        <asp:TextBox ID="txtTarjetaNumero"
                            runat="server"
                            placeholder="Ingrese número de tarjeta"
                            CssClass="form-control my-1" Text="1111-1111-1111-1111"></asp:TextBox>
                        <%}%>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="mt-5">
                <asp:Button ID="btnConfirmarCompra" runat="server"
                    CssClass="btnComprar" Text="Confirmar datos"
                   
                    OnClientClick="return validacionConfirmar()" />
            </div>

        </div>
    </div>
    </div>
</asp:Content>
