<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Details" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center mb-5">Detalles del Producto</h2>
    <div class="container">
        <div class="row justify-content-center">
            <asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound">
                <ItemTemplate>

                    <div class="col-md-6 mb-4">
                        <div class="card h-100">
                            <!-- Nombre del producto -->
                            <h3 class="card-title text-center"><%# Eval("Name") %></h3>
                            <% if (Security.SessionSecurity.ActiveSession(Session["user"]))
                                {
                                    if (isFavorite)
                                    {%>
                            <asp:LinkButton runat="server" ID="btnUndoFav" OnClick="btnUndoFav_Click" CommandArgument='<%# Eval("Code")%>'>
            <i class="bi bi-heart-fill"></i>
                            </asp:LinkButton>
                            <%}
                                else
                                { %>
                            <asp:LinkButton runat="server" ID="bntFav" OnClick="bntFav_Click" CssClass="btnFav" CommandArgument='<%# Eval("Code")%>'>
              <i class="bi bi-heart"></i>
                            </asp:LinkButton>
                            <% }
                                }%>
                            <!-- Carrusel de imágenes del producto -->
                            <div id="carouselProductImages" class="carousel slide" data-bs-ride="carousel">

                                <div class="carousel-inner">
                                    <asp:Repeater ID="rptImages" runat="server">
                                        <ItemTemplate>
                                            <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                                <img src='<%# Eval("UrlImage") %>' class="d-block w-100" alt="Imagen del producto">
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <button class="carousel-control-prev" type="button" data-bs-target="#carouselProductImages" data-bs-slide="prev">
                                    <span class="carousel-control-prev-icon"></span>
                                </button>
                                <button class="carousel-control-next" type="button" data-bs-target="#carouselProductImages" data-bs-slide="next">
                                    <span class="carousel-control-next-icon"></span>
                                </button>
                            </div>

                            <!-- Descripción, categoría y precio del producto -->
                            <div class="card-body">
                                <p class="card-text"><b>Descripción:</b> <%# Eval("Description") %></p>
                                <p class="card-text"><b>Temporada:</b> <%# Eval("Season.Description") %></p>
                                <p class="card-text"><b>Modelo:</b> <%# Eval("SubCategory.Description") %></p>
                                <p class="card-text"><b>Precio:</b> $<%# Eval("Price") %></p>
                                <p class="card-text"><b>Cod.Art:</b> <%# Eval("Code") %></p>

                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


            <div class="container">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row align-items-center">
                            <div class="col-auto">
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control form-control-sm text-center w-75" Text="0" ReadOnly="true" />
                            </div>
                            <div class="col-auto d-flex flex-column">
                                <asp:Button ID="btnAdd" Text="+" OnClick="btnAdd_Click" CssClass="btn btn-primary btn-sm mb-1" runat="server" />
                                <asp:Button ID="btnSubtract" Text="-" OnClick="btnSubtract_Click" CssClass="btn btn-primary btn-sm" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubtract" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

                <div class="row mt-3">
                    <div class="col">
                        <asp:Button ID="btnAddToCart" Text="Añadir al carrito" OnClick="btnAddToCart_Click" CommandArgument='<%# Eval("Code") %>' CssClass="btn btn-primary btn-sm w-100" runat="server" />
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>

