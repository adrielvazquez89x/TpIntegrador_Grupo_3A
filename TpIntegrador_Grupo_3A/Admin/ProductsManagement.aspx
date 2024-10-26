<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Products" %>
<%@ Register Src="~/Admin/UserControl_Buttons.ascx" TagPrefix="uc1" TagName="UserControl_Buttons" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center my-5">Gestión de Productos</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:UserControl_Buttons runat="server" id="ControUser_Buttons" />




        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
