<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center mb-5">Nuestro listado de Prendas</h2>
    <div class="container">
        <div class="row justify-content-center">
            <asp:Repeater ID="rptProdList" runat="server" OnItemDataBound="rptProdList_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">

                        <div class="card h-100" style="width: 18rem;">


                            <asp:LinkButton runat="server" ID="btnUndoFav" OnClick="btnUndoFav_Click">
                                 <i class="bi bi-heart"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="bntFav" OnClick="bntFav_Click">
                                <i class="bi bi-heart-fill"></i>
                            </asp:LinkButton>

                            <!-- Carrusel de imágenes -->
                            <div id="carouselExample<%# Eval("Id") %>" class="carousel slide" data-bs-ride="carousel">
                                <div class="carousel-inner">
                                    <!-- Repeater anidado para las imágenes -->
                                    <asp:Repeater ID="rptImagesList" runat="server">
                                        <ItemTemplate>
                                            <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                                <img src='<%# Eval("UrlImage") %>' class="d-block w-100" style="height: 300px; object-fit: cover;">
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <!-- Controles del carrusel -->
                                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample<%# Eval("Id") %>" data-bs-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Previous</span>
                                </button>
                                <button class="carousel-control-next" type="button" data-bs-target="#carouselExample<%# Eval("Id") %>" data-bs-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Next</span>
                                </button>
                            </div>

                            <!-- Información del producto -->
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Name") %></h5>
                                <p class="card-text"><%# Eval("Description") %></p>
                                <asp:Button
                                    ID="btnDetails" OnClick="btnDetails_Click"
                                    CommandArgument='<%# Eval("Id")%>'
                                    runat="server"
                                    Text="Detalles" CssClass="btn btn-primary w-100" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>

</asp:Content>
