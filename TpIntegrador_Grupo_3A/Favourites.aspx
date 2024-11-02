<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Favourites.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Favourites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center">Favoritos</h1>

    <% if (FavIdList.Count != 0)
        {%>
    <div class="row justify-content-center gap-4">
        <asp:Repeater runat="server" ID="rptFav">
            <ItemTemplate>
                <div class="card mb-3 col-5" style="max-width: 700px;">
                    <div class="row g-0">
                        <div class="col-md-4 favImg py-3">
                            <a href="Detalle.aspx?id=<%#Eval("Id") %>">
                                <%--<img src="<%#Eval("Images[0].UrlImage") %>" onerror="this.src='https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg'" class="img-fluid rounded-start">--%>
                            </a>
                        </div>
                        <div class="col-md-4 favImg py-3">
                            <a href="Details.aspx?id=<%#Eval("Id") %>"></a>
                        </div>
                        <div class="col-md-7">
                            <div class="card-body">
                                <a href="Details.aspx?id=<%#Eval("Id") %>">
                                    <h5 class="card-title"><%#Eval("Name")%></h5>
                                </a>
                                <p class="card-text"><%#Eval("Description") %></p>
                                <p class="card-text"><small class="text-body-secondary">$ <%#Eval("Price") %> </small></p>
                            </div>
                        </div>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <%}
        else
        {%>
    <div class="d-flex flex-column justify-content-center align-items-center">
        <h5>Todavía no registra Favoritos</h5>
        <i class="fa fa-exclamation-circle " aria-hidden="true"></i>
    </div>
    <%}
    %>
</asp:Content>
