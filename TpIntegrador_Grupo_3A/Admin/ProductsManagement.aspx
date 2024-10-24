<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Gestión de Productos</h1>
    <div class="row">
        <div class="col-4">
            <h3 class="text-bg-success">Categorias</h3>
            <div class="d-flex gap-3">
                <asp:Label ID="lblCategory" runat="server" Text="Categoria"></asp:Label>
                <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button
                    ID="btnAddCategory"
                    runat="server"
                    Text="Agregar"
                    CssClass="btn btn-primary"
                    OnClick="btnAddCategory_Click" />
            </div>
        </div>
        <div class="col-8">
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </div>

    <div class="toast-container position-fixed top-0 end-0 p-5">
        <div id="toastMessage" class="toast align-items-center text-bg-success border-0" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">
                    <asp:Literal ID="ltlToastMessage" runat="server" />
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    </div>
</asp:Content>
