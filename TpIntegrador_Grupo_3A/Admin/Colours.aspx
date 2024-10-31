﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Colours.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Colours" %>

<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>
<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Contenedor principal --%>
            <div class="container my-4">
                <%-- Título de la página --%>
                <h2 class="text-center mb-4">Gestión de Colores</h2>

                <%-- Botón para volver --%>
                <div class="mb-3">
                    <uc1:usercontrol_buttonback runat="server" id="UserControl_ButtonBack" />
                </div>

                <%-- Sección de Colores --%>
                <div class="row">
                    <%-- Formulario de Colores --%>
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-header bg-success text-white">
                                <h3 class="card-title mb-0">Colores</h3>
                            </div>
                            <div class="card-body">
                                <asp:Label ID="lblColour" runat="server" Text="Colores" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtColour" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                                <asp:Button
                                    ID="btnAddColour"
                                    runat="server"
                                    Text="Agregar"
                                    CssClass="btn btn-primary w-100"
                                    OnClick="btnAddColour_Click" />
                            </div>
                        </div>
                    </div>

                    <%-- Lista de Colores --%>
                    <div class="col-md-8">
                        <asp:GridView
                            ID="dgvColours"
                            runat="server"
                            CssClass="table table-striped table-bordered"
                            HeaderStyle-CssClass="table-dark"
                            DataKeyNames="Id"
                            AutoGenerateColumns="false"
                            OnPageIndexChanging="dgvColours_PageIndexChanging"
                            AllowPaging="true"
                            PageSize="5">
                            <Columns>
                                <%-- Nombre de la Color --%>
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
                                                ID="btnEditColour"
                                                runat="server"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
                                                CssClass="btn btn-sm btn-outline-primary me-2"
                                                OnClick="btnEditColour_Click">
                                     <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Eliminar --%>
                                            <asp:LinkButton
                                                ID="btnDeleteColour"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-danger me-2"
                                                OnClick="btnDeleteColour_Click"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
                                                Visible='<%# Convert.ToBoolean(Eval("Active")) %>'
                                                ToolTip="Desactivar">
                                     <i class="bi bi-trash-fill"></i>
                                            </asp:LinkButton>

                                            <%-- Botón Activar --%>
                                            <asp:LinkButton
                                                ID="btnActivateColour"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-success"
                                                OnClick="btnActivateColour_Click"
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
                <uc1:usercontrol_toast runat="server" id="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
