<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Productos</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Botones para cambiar --%>
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
            <%-- Botón para volver --%>
            <%if (CurrentOption != Buttons.NotPicked)
                {%>
            <div>
                <asp:Button ID="btnBack" runat="server" Text="Volver" CssClass="btn btn-dark" OnClick="btnBack_Click" />
            </div>
            <%}%>

            <%-- Categoria --%>
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
                <div class="col-4">
                    <asp:GridView
                        ID="dgvCategories"
                        runat="server"
                        CssClass="table"
                        DataKeyNames="Id"
                        AutoGenerateColumns="false"
                        OnSelectedIndexChanged="dgvCategories_SelectedIndexChanged"
                        OnPageIndexChanging="dgvCategories_PageIndexChanging" AllowPaging="true"
                        PageSize="5">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Nombre" />

                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("Active")) ? 
                                     "<i class='bi bi-check-circle-fill text-success'></i>" : 
                                        "<i class='bi bi-x-circle-fill text-danger'></i>" %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <!-- Editar -->
                                    <asp:LinkButton
                                        ID="btnEditCategory"
                                        runat="server"
                                        CommandArgument='<%# Eval("Id") + "|" + Eval("Name")+ "|" + Eval("Active") %>'
                                        CssClass="btn btn-sm text-primary me-2"
                                        OnClick="btnEditCategory_Click">
                                    <i class="bi bi-pencil-square"></i>
                                    </asp:LinkButton>


                                    <!-- Botón de Eliminar (Visible cuando Active es true) -->
                                    <asp:LinkButton
                                        ID="btnDeleteCategory"
                                        runat="server"
                                        CssClass="btn btn-sm text-danger me-2"
                                        OnClick="btnDeleteCategory_Click"
                                        CommandArgument='<%# Eval("Id") + "|" + Eval("Name")+ "|" + Eval("Active") %>'
                                        Visible='<%# Convert.ToBoolean(Eval("Active")) %>'
                                        ToolTip="Desactivar">
                                        <i class="bi bi-trash-fill"></i>
                                     </asp:LinkButton>

                                    <!-- Botón de Activar (Visible cuando Active es false) -->
                                    <asp:LinkButton
                                        ID="btnActivateCategory"
                                        runat="server"
                                        CssClass="btn btn-sm text-success"
                                        OnClick="btnActivateCategory_Click"                                        
                                        CommandArgument='<%# Eval("Id") + "|" + Eval("Name")+ "|" + Eval("Active") %>'
                                        Visible='<%# !Convert.ToBoolean(Eval("Active")) %>'
                                        ToolTip="Activar">
                                        <i class="bi bi-check-circle"></i>
                                     </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
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
