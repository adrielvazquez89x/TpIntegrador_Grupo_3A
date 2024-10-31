<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Products" %>

<%@ Register Src="~/Admin/UserControl_Buttons.ascx" TagPrefix="uc1" TagName="UserControl_Buttons" %>
<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center my-5">Gestión de Productos</h2>
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
                PagerSettings-Visible="true"
                PagerSettings-Mode="NumericFirstLast"
                PagerSettings-FirstPageText="<<"
                PagerSettings-LastPageText=">>"
                PagerSettings-NextPageText=">"
                PagerSettings-PreviousPageText="<"
               
            >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Code" HeaderText="Código" />
                    <asp:BoundField DataField="Name" HeaderText="Nombre" />
                    <asp:BoundField DataField="Description" HeaderText="Descripción" />
                    <asp:BoundField DataField="Price" HeaderText="Precio" DataFormatString="{0:C}" HtmlEncode="False" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" />
                    <asp:TemplateField HeaderText="Categoría">
                        <ItemTemplate>
                            <%# Eval("Category.Description") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Talle">
                        <ItemTemplate>
                            <%# Eval("Size.Description") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Color">
                        <ItemTemplate>
                            <%# Eval("Colour.Description") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CreationDate" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                </Columns>
            </asp:GridView>

            <asp:LinkButton ID="btnAddProduct" runat="server" CssClass="buttonCus btn-electric-blue" OnClick="btnAddProduct_Click">
                <i class="bi bi-bag-plus fs-4"></i>
                Agregar Producto
            </asp:LinkButton>

            <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
