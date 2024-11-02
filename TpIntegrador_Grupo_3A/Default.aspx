<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Default" %>

<asp:Content ID="BannerContent" ContentPlaceHolderID="BannerContent" runat="server">
    <!-- Carousel de imágenes -->
    <div id="carouselExample" class="carousel slide mb-4">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="./Images/slider1.jpg" class="d-block w-100" style="height: 50vh; object-fit: cover;" alt="Imagen 1">
            </div>
            <div class="carousel-item">
                <img src="./Images/slider2.jpg" class="d-block w-100" style="height: 50vh; object-fit: cover;" alt="Imagen 2">
            </div>
            <div class="carousel-item">
                <img src="./Images/slider3.jpg" class="d-block w-100" style="height: 50vh; object-fit: cover;" alt="Imagen 3">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <!-- Secciones de productos -->
        <div class="container m-5">
            <asp:Repeater ID="RptSecciones" runat="server" OnItemDataBound="RptSecciones_ItemDataBound">
                <ItemTemplate>
                    <div class="row mb-4">
                        <div class="card shadow-sm h-100">
                            <div class="card-body text-center">
                                <h3 class="card-title"><%# Eval("Description") %></h3>
                                <p class="card-text">
                                    <%# Eval("Description2") %>
                                </p>

                                <!-- Carrusel de Productos -->
                                <div id="carouselProd<%# Eval("Id") %>" class="carousel slide" data-bs-ride="carousel">
                                    <div class="carousel-inner">
                                        <div class="col-md-4 mb-4">
                                            <!-- Repeater anidado para los prod -->
                                            <asp:Repeater ID="rptProdList" runat="server">
                                                <ItemTemplate>
                                                    <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                                        <p class="card-text">
                                                            <%# Eval("Description") %>
                                                        </p>
                                                        <%-- Campo oculto para almacenar el Id del producto --%>
                                                        <asp:HiddenField ID="hfProdId" runat="server" Value='<%# Eval("Id") %>' />

                                                        <%-- Repeater de subcategorías --%>
                                                        <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_ItemDataBound">
                                                            <ItemTemplate>
                                                                <img src='<%# Eval("UrlImage") %>' class="d-block w-100" style="height: 300px; object-fit: cover;">
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>

                                    <!-- Controles del carrusel -->
                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselProd<%# Eval("Id") %>" data-bs-slide="prev">
                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                        <span class="visually-hidden">Previous</span>
                                    </button>
                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselProd<%# Eval("Id") %>" data-bs-slide="next">
                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                        <span class="visually-hidden">Next</span>
                                    </button>
                                </div>
                                <a href='#' class="btn btn-primary mt-3">Ver todos</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </main>
</asp:Content>
