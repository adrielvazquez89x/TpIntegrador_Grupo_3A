<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Products" %>

<%@ Register Src="~/Admin/UserControl_Buttons.ascx" TagPrefix="uc1" TagName="UserControl_Buttons" %>
<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center my-5">Gestión de Productos</h2>
    <!-- Puedes eliminar el UpdatePanel si ya no es necesario -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:UserControl_Buttons runat="server" ID="ControUser_Buttons" />
            <h2 class="my-5 text-center">Artículos existentes</h2>
            <asp:GridView
                ID="dgvProducts"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-striped"
                OnPageIndexChanging="dgvProducts_PageIndexChanging"
                AllowPaging="true"
                PageSize="5"
                PagerStyle-HorizontalAlign="Center"
                OnRowCommand="dgvProducts_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Code" HeaderText="Código" />
                    <asp:BoundField DataField="Name" HeaderText="Nombre" />
                    <asp:BoundField DataField="Price" HeaderText="Precio" DataFormatString="{0:C}" HtmlEncode="False" />
                    <asp:TemplateField HeaderText="Categoría">
                        <ItemTemplate>
                            <%# Eval("Category.Description") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CreationDate" HeaderText="Fecha creación" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnView"
                                runat="server"
                                CommandName="View"
                                CommandArgument='<%# Eval("Code") %>'
                                CssClass="btn btn-link text-primary"
                                OnClick="btnView_Click">
                    <i class="bi bi-search"></i> 
                            </asp:LinkButton>
                            <asp:LinkButton
                                ID="btnEdit"
                                runat="server"
                                CommandName="EditProduct"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-link text-warning"
                                OnClick="btnEdit_Click">
                    <i class="bi bi-pencil-square"></i> 
                            </asp:LinkButton>
                            <%-- Botón para Editar Stock --%>
                            <asp:LinkButton
                                ID="btnEditStock"
                                runat="server"
                                CommandName="EditStock"
                                CommandArgument='<%# Eval("Code") %>'
                                CssClass="btn btn-link text-success">
                                <i class="bi bi-box-seam"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <%-- Paginación --%>
                <PagerTemplate>
                    <div class="d-flex justify-content-center align-items-center">
                        <asp:LinkButton
                            ID="lnkFirst"
                            runat="server"
                            CommandName="Page"
                            CommandArgument="First"
                            CssClass="btn btn-link">
                <i class="bi bi-skip-start-fill"></i>
            </asp:LinkButton>

                        <asp:LinkButton
                            ID="lnkPrevious"
                            runat="server"
                            CommandName="Page"
                            CommandArgument="Prev"
                            CssClass="btn btn-link">
                <i class="bi bi-chevron-left"></i>
            </asp:LinkButton>

                        <asp:LinkButton
                            ID="lnkNext"
                            runat="server"
                            CommandName="Page"
                            CommandArgument="Next"
                            CssClass="btn btn-link">
                <i class="bi bi-chevron-right"></i>
            </asp:LinkButton>

                        <asp:LinkButton
                            ID="lnkLast"
                            runat="server"
                            CommandName="Page"
                            CommandArgument="Last"
                            CssClass="btn btn-link">
                <i class="bi bi-skip-end-fill"></i>
            </asp:LinkButton>
                    </div>
                </PagerTemplate>

            </asp:GridView>

            <asp:LinkButton ID="btnAddProduct" runat="server" CssClass="buttonCus btn-electric-blue" OnClick="btnAddProduct_Click">
                <i class="bi bi-bag-plus fs-4"></i>
                Agregar Producto
            </asp:LinkButton>

            <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
