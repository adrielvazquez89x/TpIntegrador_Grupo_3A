<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="TpIntegrador_Grupo_3A.User.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="text-center mt-5">Mi carrito </h3>
    <% if (Items.Count != 0 && Items != null)
        {%>
    <table class="table">
        <thead>
            <tr>
                <th scope="col"> </th>
                <th scope="col">PRODUCTO</th>
                <th scope="col">TALLE</th>
                <th scope="col">COLOR</th>
                <th scope="col">CANTIDAD</th>
                <th scope="col">SUBTOTAL</th>
                <th scope="col"> </th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="repeater">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%  %></th>
                        <td><%#Eval("Product.Description") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
           
        </tbody>
    </table>
    <div class="d-flex justify-content-around">
        <asp:Button runat="server" ID="btnVaciarCarrito" CssClass="btnVaciar" Text="Vaciar carrito" />
        <a href="Checkout.aspx" class="btnComprar">Realizar compra</a>
       <%-- <asp:Button runat="server" ID="btnRealizarCompra" CssClass="btnComprar" Text="Realizar compra" />--%>
    </div>


    <%}
        else
        {  %>
    <div class="mx-auto">
        <h3 class="text-center mt-5">El carrito está vacío</h3>
    </div>

    <%} %>
</asp:Content>
