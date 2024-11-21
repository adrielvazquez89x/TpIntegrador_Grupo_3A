<%@ Page Title="Gestión de Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.SalesManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h1 class="my-4">Ventas Realizadas</h1>

        <!-- Filtros y Búsqueda -->
        <asp:Panel ID="FiltersPanel" runat="server" CssClass="row mb-4">
            <div class="col-md-3 mb-2">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Buscar..."></asp:TextBox>
            </div>
            <div class="col-md-3 mb-2">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
                    <asp:ListItem Text="Filtrar por estado" Value="" />
                    <asp:ListItem Text="Pagado" Value="Pagado" />
                    <asp:ListItem Text="Pendiente" Value="Pendiente" />
                    <asp:ListItem Text="Cancelado" Value="Cancelado" />
                </asp:DropDownList>
            </div>
        </asp:Panel>

        <!-- GridView de Ventas -->
        <asp:GridView ID="gvSales" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvSales_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Número de venta" />
                <asp:BoundField DataField="CustomerName" HeaderText="Cliente" />
                <asp:BoundField DataField="Date" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" CssClass='<%# GetStatusCssClass(Eval("State").ToString()) %>'>
                            <%# Eval("State") %>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination justify-content-center" />
        </asp:GridView>
    </div>
</asp:Content>
