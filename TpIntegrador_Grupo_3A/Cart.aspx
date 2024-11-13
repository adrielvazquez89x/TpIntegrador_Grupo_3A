<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Cart" %>
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
                <th scope="col">COLOR</th>
                <th scope="col">TALLE</th>
                <th scope="col">CANTIDAD</th>
                <th scope="col">SUBTOTAL</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="repeater">
                <ItemTemplate>
                    <tr>
                         <td>
   <img style="height: 100px; object-fit:contain;" src='<%# (Eval("Product.Images") != null && ((List<Model.ImageProduct>)Eval("Product.Images")).Count > 0) ?
((List<Model.ImageProduct>)Eval("Product.Images"))[0].UrlImage : "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg" %>'  />
 </td>
                        <td><%#Eval("Product.Description") %></td>
                         <td><%#Eval("Stock.Colour.Description") %></td>
                         <td><%#Eval("Stock.Size.Description") %></td>
                        <td><%#Eval("Number") %></td>
                        <td>$<%#Eval("Subtotal") %></td>
                        <td>
                                                <div>
    <!-- Botonera de acciones -->
    <div class="d-flex justify-content-around px-3">
        <!-- Agregar o disminuir cantidad -->
        <!-- Btn Aumentar -->
        <asp:LinkButton Text="text" runat="server" ID="btnMore" CommandArgument='<%#Eval("Stock.Id")%>' CssClass="text-secondary">
    <i class="bi bi-plus-lg" aria-hidden="true"></i>
        </asp:LinkButton>
        <!-- Btn Disminuir -->
        <asp:LinkButton Text="text" runat="server" ID="btnLess" CommandArgument='<%#Eval("Stock.Id")%>' CssClass="text-secondary">
      <i class="bi bi-dash-lg" aria-hidden="true"></i>
        </asp:LinkButton>
    </div>
    <!-- Eliminar producto del carrito -->
    <div class="d-flex justify-content-center px-3 mt-5">
        <asp:LinkButton Text="text" runat="server" ID="btnDelete" CommandArgument='<%#Eval("Stock.Id")%>' CssClass="text-danger">
           <i class="bi bi-trash" aria-hidden="true"></i>
        </asp:LinkButton>
    </div>

</div>
                        </td>
      
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
