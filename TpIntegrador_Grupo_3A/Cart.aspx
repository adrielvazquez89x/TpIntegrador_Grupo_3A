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
                <th scope="col"></th>
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
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <td>
                                <img style="height: 100px; object-fit: contain;" src='<%# (Eval("Product.Images") != null && ((List<Model.ImageProduct>)Eval("Product.Images")).Count > 0) ?
((List<Model.ImageProduct>)Eval("Product.Images"))[0].UrlImage : "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg" %>' />
                            </td>
                            <td><%#Eval("Product.Description") %></td>
                            <td><%#Eval("Stock.Colour.Description") %></td>
                            <td><%#Eval("Stock.Size.Description") %></td>
                            <td><%#Eval("Number") %></td>
                            <td>$<%#Eval("Subtotal") %></td>
                            <td>

                                <div>

                                    <div class="d-flex justify-content-around px-3">
                                        <!-- Btn Aumentar -->
                                        <asp:LinkButton runat="server" ID="btnMore" OnClick="btnMore_Click" CommandArgument='<%#Eval("Stock.Id")%>' CssClass="text-secondary">
                                        <i class="bi bi-plus-lg" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                        <!-- Btn Disminuir -->
                                        <asp:LinkButton runat="server" ID="btnLess" OnClick="btnLess_Click" CommandArgument='<%#Eval("Stock.Id")%>' CssClass="text-secondary">
                                        <i class="bi bi-dash-lg" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <!-- Eliminar producto del carrito -->
                                    <div class="d-flex justify-content-center px-3 mt-5">
                                        <asp:LinkButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" CommandArgument='<%#Eval("Stock.Id")%>' CssClass="text-danger">
                                        <i class="bi bi-trash" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </td>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnMore" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLess" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="5"></td>
                <td class="text-right">Total: $<%:Total%></td>
            </tr>
        </tbody>
    </table>
    <div class="d-flex justify-content-around">
        <asp:Button runat="server" ID="btnEmptyCart" CssClass="btn btn-danger" OnClick="btnEmptyCart_Click" Text="Vaciar carrito" />
        <asp:LinkButton runat="server" Text="Comprar" class="btn btn-primary" ID="btnBuy" OnClick="btnBuy_Click"></asp:LinkButton>
    </div>


    <%}
        else
        {  %>
    <div class="mx-auto">
        <h3 class="text-center mt-5">El carrito está vacío</h3>
    </div>

    <%} %>
</asp:Content>
