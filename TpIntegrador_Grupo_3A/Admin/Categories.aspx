﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.Categories" %>

<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>
<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <div class="container my-4">
               
                <h2 class="text-center mb-4">Gestión de Categorías</h2>

              
                <div class="mb-3">
                    <uc1:UserControl_ButtonBack runat="server" ID="UserControl_ButtonBack" />
                </div>

              
                <div class="row">
                   
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-header bg-success text-white">
                                <h3 class="card-title mb-0">Categorías</h3>
                            </div>
                            <div class="card-body">
                                <asp:Label ID="lblCategory" runat="server" Text="Categoría" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control mb-3"></asp:TextBox>
                                <asp:Button
                                    ID="btnAddCategory"
                                    runat="server"
                                    Text="Agregar"
                                    CssClass="btn btn-primary w-100"
                                    OnClick="btnAddCategory_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <asp:GridView
                            ID="dgvCategories"
                            runat="server"
                            CssClass="table table-striped table-bordered"
                            HeaderStyle-CssClass="table-dark"
                            DataKeyNames="Id"
                            AutoGenerateColumns="false"
                            OnPageIndexChanging="dgvCategories_PageIndexChanging"
                            AllowPaging="true"
                            PageSize="5">
                            <Columns>
                             
                                <asp:BoundField DataField="Description" HeaderText="Nombre" ItemStyle-CssClass="align-middle" />

                              
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Convert.ToBoolean(Eval("Active")) ? 
                                                "<i class='bi bi-check-circle-fill text-success'></i>" : 
                                                "<i class='bi bi-x-circle-fill text-danger'></i>" %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">
                                           
                                            <asp:LinkButton
                                                ID="btnEditCategory"
                                                runat="server"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
                                                CssClass="btn btn-sm btn-outline-primary me-2"
                                                OnClick="btnEditCategory_Click">
                                                <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                          
                                            <asp:LinkButton
                                                ID="btnDeleteCategory"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-danger me-2"
                                                OnClick="btnDeleteCategory_Click"
                                                CommandArgument='<%# Eval("Id") + "|" + Eval("Description")+ "|" + Eval("Active") %>'
                                                Visible='<%# Convert.ToBoolean(Eval("Active")) %>'
                                                ToolTip="Desactivar">
                                                <i class="bi bi-trash-fill"></i>
                                            </asp:LinkButton>

                                       
                                            <asp:LinkButton
                                                ID="btnActivateCategory"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-success"
                                                OnClick="btnActivateCategory_Click"
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

                
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
