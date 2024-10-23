<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Productos</h1>
    <div class="d-flex justify-content-between">
        <%foreach (var prod in listProd)
            {%>
        <div class="card" style="width: 18rem;">
            <img src="<%=prod.Images[0].Url %>" class="card-img-top" alt="...">
            <div class="card-body">
                <h5 class="card-title"><%=prod.Name %></h5>
                <p class="card-text"><%=prod.Name %></p>
            </div>
            <ul class="list-group list-group-flush">
                <li class="list-group-item">$<%=prod.Price %></li>

            </ul>
            <div class="card-body">
                <a href="#" class="card-link">Ver mas</a>
            </div>
        </div>
        <%}
        %>
    </div>

</asp:Content>
