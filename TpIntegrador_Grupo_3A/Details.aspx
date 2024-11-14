<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Details" %>

<%@ Register Src="~/Control_Toast.ascx" TagPrefix="uc1" TagName="Control_Toast" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2 class="text-center mb-5">Detalles del Producto</h2>
            <div class="container">
                <div class="row justify-content-center">
                    <uc1:Control_Toast runat="server" ID="Control_Toast" />

                    <asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound">
                        <ItemTemplate>

                            <div class="col-md-4 mb-1">
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
                                    <asp:LinkButton runat="server" ID="btnFav" OnClick="btnFav_Click" CssClass="" CommandArgument='<%# Eval("Code")%>'>
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
                                                        <img src='<%# Eval("UrlImage") %>' class="d-block w-100" style="height: 100px; object-fit: contain;" alt="Imagen del producto">
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
                    <% if (Security.SessionSecurity.ActiveSession(Session["user"]))
                        { %>
                    <!-- Opciones de Talle, Color, y Cantidad -->
                    <div class="col-md-3">
                        <h5 class="text-center mb-3">Opciones de compra</h5>

                        <div class="mb-3">
                            <asp:Label ID="lblSize" runat="server" CssClass="form-label">Talle</asp:Label>
                            <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-select" />
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblColour" runat="server" CssClass="form-label">Color</asp:Label>
                            <asp:DropDownList ID="ddlColour" runat="server" CssClass="form-select" />
                        </div>

                        <!-- Controles de Cantidad -->
                        <div class="input-group mb-3">
                            <span class="input-group-text">Cantidad</span>
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control text-center" Style="width: 60px;" Text="1" ReadOnly="true" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSubtract" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Button class="btn btn-outline-secondary" Text="-" runat="server" ID="btnSubtract" OnClick="btnSubtract_Click" />
                            <asp:Button class="btn btn-outline-secondary" Text="+" runat="server" ID="btnAdd" OnClick="btnAdd_Click" />

                        </div>
                        <!-- Botón Añadir al Carrito -->
                        <div class="d-grid">
                            <asp:Button ID="btnAddToCart" Text="Añadir al carrito" OnClick="btnAddToCart_Click" CssClass="btn btn-primary" runat="server" CommandArgument='<%# Eval("Code")%>' />
                        </div>

                        <!-- Mensaje de Error -->
                        <asp:Label ID="lblError" Text="" CssClass="text-danger mt-3" Visible="false" runat="server" />

                    </div>
                    <% } %>
                </div>
            </div>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="btnUndoFav" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnFav" EventName="Click" />
            </triggers>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

