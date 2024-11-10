<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UsersManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>
    <%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Contenedor principal --%>
            <div class="container my-4">
                <%-- Título de la página --%>
                <h2 class="text-center mb-4">Gestión de Administradores</h2>

                <%-- Botón para volver --%>
                <div class="mb-3">
                    <uc1:UserControl_ButtonBack runat="server" ID="UserControl_ButtonBack" />
                </div>

                <%-- Sección de Categorías --%>
                <div class="row">
                    <%-- Formulario de Categorías --%>


                    <%-- Lista de Usuarios --%>
                    <div class="col-md-8">
                        <asp:GridView
                            ID="dgvUsers"
                            runat="server"
                            CssClass="table table-striped table-bordered"
                            HeaderStyle-CssClass="table-dark"
                            DataKeyNames="UserId"
                            AutoGenerateColumns="false"
                            OnPageIndexChanging="dgvUsers_PageIndexChanging"
                            AllowPaging="true"
                            PageSize="5">
                            <Columns>
                                <%-- Nombre de la Categoría --%>
                                <asp:BoundField DataField="FirstName" HeaderText="Nombre" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="LastName" HeaderText="Apellido" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="align-middle" />


                                <%-- Estado Activo/Inactivo --%>
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Convert.ToBoolean(Eval("Active")) ? 
                                                "<i class='bi bi-check-circle-fill text-success'></i>" : 
                                                "<i class='bi bi-x-circle-fill text-danger'></i>" %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- Acciones --%>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">


                                            <%-- Botón Editar --%>
                                            <asp:LinkButton
                                                ID="btnEditUser"
                                                runat="server"
                                                CommandArgument='<%# Eval("UserId") %>'
                                                CssClass="btn btn-sm btn-outline-primary me-2"
                                                OnClick="btnEditUser_Click">
                                                <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Eliminar --%>
                                            <asp:LinkButton
                                                ID="btnDeleteUser"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-danger me-2"
                                                OnClick="btnDeleteUser_Click"
                                                CommandArgument='<%# Eval("UserId") + "|" + Eval("Active") %>'
                                                Visible='<%# Convert.ToBoolean(Eval("Active")) %>'
                                                ToolTip="Desactivar">
                                                <i class="bi bi-trash-fill"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Activar --%>
                                            <asp:LinkButton
                                                ID="btnActivateUser"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-success"
                                                OnClick="btnActivateUser_Click"
                                                CommandArgument='<%# Eval("UserId") + "|" + Eval("Active") %>'
                                                Visible='<%# !Convert.ToBoolean(Eval("Active")) %>'
                                                ToolTip="Activar">
                                                <i class="bi bi-check-circle"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <asp:Button ID="btnAddUser" runat="server" CssClass="btn btn-primary mb-3" Text="Agregar Nuevo Usuario" OnClick="btnAddUser_Click" />

                <%-- Toast de Notificaciones --%>
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
