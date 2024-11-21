<%@ Page Title="Gestión de Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.SalesManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
            <asp:GridView ID="gvSales" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                OnPageIndexChanging="gvSales_PageIndexChanging"
                OnRowCommand="gvSales_RowCommand"
                DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Número de venta" ReadOnly="True" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Cliente" ReadOnly="True" />
                    <asp:BoundField DataField="Date" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" CssClass='<%# GetStatusCssClass(Eval("State").ToString()) %>'>
                    <%# Eval("State") %>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cambiar Estado">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSetPaid" runat="server" CommandName="SetPagado" CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-success btn-sm me-1" ToolTip="Marcar como Pagado">
                    <i class="bi bi-check-circle""></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSetPending" runat="server" CommandName="SetPendiente" CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-warning btn-sm me-1" ToolTip="Marcar como Pendiente">
                    <i class="bi bi-hourglass-split"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSetCancelled" runat="server" CommandName="SetCancelado" CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-danger btn-sm" ToolTip="Marcar como Cancelado">
                    <i class="bi bi-x-circle"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination justify-content-center" />
            </asp:GridView>

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
