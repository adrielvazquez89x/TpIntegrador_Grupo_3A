<%@ Page Title="Gestión de Stock" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.StockManagement" %>

<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <h2 class="text-center my-5">Manejar Stock</h2>

                <!-- Información del Producto -->
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Código</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <!-- Lista de Stocks Existentes -->
                <h4>Stock Actual</h4>
                <asp:Repeater ID="rptStock" runat="server" OnItemCommand="rptStock_ItemCommand">
                    <HeaderTemplate>
                        <div class="list-group">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                                <strong>Color:</strong> <%# Eval("Colour.Description") %> |
                                <strong>Talle:</strong> <%# Eval("Size.Description") %> |
                                <strong>Stock:</strong> <%# Eval("Amount") %>
                            </div>
                            <div>
                                <asp:TextBox
                                    ID="txtEditStock"
                                    runat="server"
                                    CssClass="form-control d-inline-block w-25"
                                    Text='<%# Eval("Amount") %>'

                                    >
                                </asp:TextBox>
                                <asp:Button
                                    ID="btnUpdateStock"
                                    runat="server"
                                    CssClass="btn btn-success btn-sm ms-2"
                                    CommandName="UpdateStock"
                                    CommandArgument='<%# Eval("Id") %>'
                                    Text="Modificar" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Formulario para Agregar Nuevo Stock -->
                <h4 class="mt-5">Agregar Nueva Combinación</h4>
                <div class="mb-3">
                    <label for="ddlColor" class="form-label">Color</label>
                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="ddlTalle" class="form-label">Talle</label>
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtCantidad" class="form-label">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Button ID="btnAgregarStock" runat="server" CssClass="btn btn-success me-2" Text="Agregar" OnClick="btnAgregarStock_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnCancelar_Click" />
                </div>
            </div>

            <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
