<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlCheckout" runat="server" Visible="false">
        <div class="container mt-5">
            <h1 class="text-center mb-4" id="h1Confirmar">Confirmar Compra</h1>
            <div class="row">
                <!-- Primera columna: Formulario -->
                <div class="col-lg-6">
                    <h3 class="mb-4">Completar para finalizar la compra:</h3>
                    <asp:UpdatePanel ID="UpdatePanelDelivery" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <asp:TextBox ID="txtName" runat="server" placeholder="Nombre" CssClass="form-control mb-3" />
                                <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtDni" runat="server" placeholder="DNI" CssClass="form-control mb-3" TextMode="Number" />
                                <asp:RequiredFieldValidator ControlToValidate="txtDni" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                            </div>

                            <asp:Panel ID="pnlDeliveryDetails" runat="server" Visible="false">
                                <div class="form-group">
                                    <asp:TextBox ID="txtProvince" runat="server" placeholder="Provincia" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtProvince" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtTown" runat="server" placeholder="Ciudad" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtTown" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDistrict" runat="server" placeholder="Distrito" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtDistrict" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCP" runat="server" placeholder="Código Postal" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtCP" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtStreet" runat="server" placeholder="Calle" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtStreet" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNumber" runat="server" placeholder="Altura" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtNumber" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtFloor" runat="server" placeholder="Piso" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtFloor" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUnit" runat="server" placeholder="Departamento" CssClass="form-control mb-3" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtUnit" runat="server" ErrorMessage="Campo requerido" CssClass="text-danger" />
                                </div>
                            </asp:Panel>

                            <h4 class="mt-4">Método de entrega</h4>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlEntrega" OnSelectedIndexChanged="ddlEntrega_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-select mb-3">
                                    <asp:ListItem Value="1">A domicilio</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Retiro en sucursal</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <h4 class="mt-4">Método de pago</h4>
                    <asp:UpdatePanel ID="UpdatePanelPayment" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlMetodoPago" runat="server" AutoPostBack="true" CssClass="form-select mb-3" OnSelectedIndexChanged="ddlMetodoPago_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Efectivo</asp:ListItem>
                                    <asp:ListItem Value="2">Tarjeta</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Panel ID="pnlPaymentDetails" runat="server" Visible="false">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTarjetaNumero" runat="server" placeholder="Número de tarjeta" CssClass="form-control mb-3" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:Button ID="btnConfirmarCompra" runat="server" CssClass="btn btn-primary btn-lg mt-4" Text="Confirmar datos" />
                </div>

                <!-- Segunda columna: Carrito -->
                <div class="col-lg-6">
                    <h3 class="text-center mb-4">Carrito</h3>
                    <asp:Repeater ID="rptCartItems" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Producto</th>
                                        <th>Cantidad</th>
                                        <th>Subtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                <td><%# Eval("Product.Name") %></td>
                                <td><%# Eval("Number") %></td>
                                <td>$<%# Eval("Subtotal") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="table-active">
                                <td colspan="3" class="text-end"><strong>Total:</strong></td>
                                <td>$<%# Eval("Total") %></td>
                            </tr>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="btnFinalizar" runat="server" CssClass="btn btn-success btn-lg" Text="Realizar compra" OnClick="btnFinalizar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
