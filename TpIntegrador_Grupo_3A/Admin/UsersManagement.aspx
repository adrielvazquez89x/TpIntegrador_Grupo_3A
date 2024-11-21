<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UsersManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>
    <%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="container my-4">
           
                <h2 class="text-center mb-4">Gestión de Administradores</h2>

                <div class="mb-3">
                    <uc1:UserControl_ButtonBack runat="server" ID="UserControl_ButtonBack" />
                </div>

                <div class="row d-flex justify-content-center">

                   
                    <div class="col-12 col-md-8 ">
                        <asp:GridView
                            ID="dgvUsers"
                            runat="server"
                            CssClass="table table-striped table-bordered text-center"
                            HeaderStyle-CssClass="table-dark"
                            DataKeyNames="UserId"
                            AutoGenerateColumns="false"
                            OnPageIndexChanging="dgvUsers_PageIndexChanging"
                            AllowPaging="true"
                            PageSize="5">
                            <Columns>

                                <asp:BoundField DataField="FirstName" HeaderText="Nombre" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="LastName" HeaderText="Apellido" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="Mobile" HeaderText="Celular" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="RegistrationDate" HeaderText="Fecha de Alta" ItemStyle-CssClass="align-middle" DataFormatString="{0:dd/MM/yyyy}" />

                              
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Convert.ToBoolean(Eval("Active")) ? 
                                                "<i class='bi bi-check-circle-fill text-success'></i>" : 
                                                "<i class='bi bi-x-circle-fill text-danger'></i>" %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">


                                           
                                            <asp:LinkButton
                                                ID="btnEditUser"
                                                runat="server"
                                                CommandArgument='<%# Eval("UserId") %>'
                                                CssClass="btn btn-sm btn-outline-primary me-2"
                                                OnClick="btnEditUser_Click">
                                                <i class="bi bi-pencil-square"></i>
                                            </asp:LinkButton>

                                           
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

                <div class="text-center my-3">
                    <asp:Button ID="btnAddUser" runat="server" CssClass="btn btn-primary mb-3" Text="Agregar Nuevo Usuario" OnClick="btnAddUser_Click" />
                </div>
               
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
