<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center mb-5">Nuestro listado de Prendas</h2>
    <div class="container">

        <!-- Collapse con opciones para filtrar -->
        <div class="col mb-4">
            <button class="btn gap-1 btn-deep-purple" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFilter" aria-expanded="false" aria-controls="collapseFilter">
                <i class="bi bi-filter-circle"></i>
            </button>

            <div class="collapse" id="collapseFilter">
                <div class="card card-body">

                    <!-- Opcion generica "Ordenar por..."-->
                    <div class="row mb-3">
                        <div class="col-auto">
                            <asp:Label Text="Ordenar por:" runat="server" CssClass="form-label" />
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrdenar" OnSelectedIndexChanged="ddlOrdenar_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="A - Z" />
                                <asp:ListItem Text="Z - A" />
                                <asp:ListItem Text="Menor precio" />
                                <asp:ListItem Text="Mayor precio" />
                            </asp:DropDownList>
                        </div>

                        <!-- Filtrar Rango de Precio -->
                        <div class="col-auto">
                            <asp:Label Text="Precio" runat="server" CssClass="form-label" />
                            <div class="d-flex gap-2 w-50">
                                    <asp:TextBox runat="server" ID="txtPriceMin" CssClass="form-control form-control-sm w-50" TextMode="Number" Placeholder="mínimo" />
                                    <asp:TextBox runat="server" ID="txtPriceMax" CssClass="form-control form-control-sm w-50" TextMode="Number" Placeholder="máximo" />
                            </div>
                        </div>
                        <div class="col-auto">
                            <asp:LinkButton runat="server" ID="btnFilter" OnClick="btnFilter_Click" CssClass="btn btn-info mt-4">
                                <i class="bi bi-funnel"></i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnClearFilter" CssClass="btn btn-info mt-4">
                                <i class="bi bi-arrow-counterclockwise"></i>
                            </asp:LinkButton>
                        </div>

                    </div>

                </div>
            </div>
        </div>



        <div class="row justify-content-center">
            <asp:Repeater ID="rptProdList" runat="server" OnItemDataBound="rptProdList_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">

                        <div class="card h-100" style="width: 18rem;">
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
                                <p class="card-text">$ <%# Eval("Price") %></p>
                                <asp:Button
                                    ID="btnDetails" OnClick="btnDetails_Click"
                                    CommandArgument='<%# Eval("Code")%>'
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
