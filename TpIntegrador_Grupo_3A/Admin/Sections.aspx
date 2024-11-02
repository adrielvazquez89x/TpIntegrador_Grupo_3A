<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sections.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Sections" %>

<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>
<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Contenedor principal --%>
            <div class="container my-4">
                <%-- Título de la página --%>
                <h2 class="text-center mb-4">Gestión de Secciones</h2>

                <%-- Botón para volver --%>
                <div class="mb-3">
                    <uc1:UserControl_ButtonBack runat="server" ID="UserControl_ButtonBack" />
                </div>

                <%-- Sección de Secciones --%>
                <div class="row">
                    <%-- Formulario de Secciones --%>
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-header bg-success text-white">
                                <h3 class="card-title mb-0">Secciones</h3>
                            </div>
                            <div class="card-body">
                                <asp:Label ID="lblSection" runat="server" Text="Sección" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtSection" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                                <asp:Button
                                    ID="btnAddSection"
                                    runat="server"
                                    Text="Agregar"
                                    CssClass="btn btn-primary w-100"
                                    OnClick="btnAddSection_Click" />
                            </div>
                        </div>
                    </div>

                    <%-- Lista de Secciones --%>
                    <div class="col-md-8">
                        <asp:GridView
                            ID="dgvSections"
                            runat="server"
                            CssClass="table table-striped table-bordered"
                            HeaderStyle-CssClass="table-dark"
                            DataKeyNames="Id"
                            AutoGenerateColumns="false"
                            OnPageIndexChanging="dgvSections_PageIndexChanging"
                            AllowPaging="true"
                            PageSize="5">
                            <Columns>
                                <%-- Nombre de la Sección --%>
                                <asp:BoundField DataField="Description" HeaderText="Nombre" ItemStyle-CssClass="align-middle" />

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
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">
                                            <%-- Botón Editar --%>
                                            <asp:LinkButton
                                                ID="btnEditSection"
                                                runat="server"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
                                                CssClass="btn btn-sm btn-outline-primary me-2"
                                                OnClick="btnEditSection_Click">
                                         <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Eliminar --%>
                                            <asp:LinkButton
                                                ID="btnDeleteSection"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-danger me-2"
                                                OnClick="btnDeleteSection_Click"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
                                                Visible='<%# Convert.ToBoolean(Eval("Active")) %>'
                                                ToolTip="Desactivar">
                                         <i class="bi bi-trash-fill"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Activar --%>
                                            <asp:LinkButton
                                                ID="btnActivateSection"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-success"
                                                OnClick="btnActivateSection_Click"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
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

                <%-- Toast de Notificaciones --%>
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
