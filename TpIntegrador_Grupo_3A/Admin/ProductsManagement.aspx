<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Productos</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%          
                if (CurrentOption == Buttons.NotPicked)
                {%>
            <div class="d-flex col-6 justify-content-around">
                <div>
                    <asp:Button ID="btnPickCategory" runat="server" Text="Categories" CssClass="btn btn-dark" OnClick="btnPickCategory_Click" />
                </div>
                <div>
                    <asp:Button ID="btnPickSeason" runat="server" Text="Seasons" CssClass="btn btn-dark" />
                </div>
                <div>
                    <asp:Button ID="btnPickSection" runat="server" Text="Sections" CssClass="btn btn-dark" />
                </div>
            </div>
            <%}%>

            <%if (CurrentOption != Buttons.NotPicked)
           {%>
            <div>
                <asp:Button ID="btnBack" runat="server" Text="Volver" CssClass="btn btn-dark" OnClick="btnBack_Click"/>
            </div>
            <%}%>

            <%
                if (CurrentOption == Buttons.Category)
                {%>
            <div class="row">
                <%-- Categorias --%>
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
            <%}

            %>


            <%-- Toastie --%>
            <div class="toast-container position-fixed top-0 end-0 p-3">
                <div id="liveToast" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header">
                        <strong class="me-auto">Notificación</strong>
                        <small class="text-muted">Ahora</small>
                        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Cerrar"></button>
                    </div>
                    <div class="toast-body">
                        <asp:Literal ID="ltlToastMessage" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
