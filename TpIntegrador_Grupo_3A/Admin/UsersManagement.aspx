<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersManagement.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UsersManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%@ Register Src="~/Admin/UserControl_Buttons.ascx" TagPrefix="uc1" TagName="UserControl_Buttons" %>
<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>



    <h2 class="text-center my-5">Gestión de Usuarios</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2 class="my-5 text-center">Usuarios existentes</h2>
            <asp:GridView
                ID="dgvUsers"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-striped"
                OnPageIndexChanging="dgvUsers_PageIndexChanging"
                AllowPaging="true"
                PageSize="5"
                PagerSettings-Visible="true"
                PagerSettings-Mode="NumericFirstLast"
                PagerSettings-FirstPageText="<<"
                PagerSettings-LastPageText=">>"
                PagerSettings-NextPageText=">"
                PagerSettings-PreviousPageText="<"
                PagerStyle-HorizontalAlign="Center"
                OnRowCommand="dgvUsers_RowCommand"
                >
                <Columns>
                 
                    
                    <asp:BoundField DataField="FirstName" HeaderText="Nombre" />
                    <asp:BoundField DataField="LastName" HeaderText="Apellido" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />

                    <asp:BoundField DataField="RegistrationDate" HeaderText="Fecha Alta" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />

                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            
                            <asp:LinkButton
                                ID="btnView"
                                runat="server"
                                CommandName="View"
                                CommandArgument='<%# Eval("UserId") %>'
                                CssClass="btn btn-link text-primary">
                                <i class="bi bi-search"></i> 
                            </asp:LinkButton>

                            
                            <asp:LinkButton
                                ID="btnEdit"
                                runat="server"
                                CommandName="EditProduct"
                                CommandArgument='<%# Eval("UserId") %>'
                                CssClass="btn btn-link text-warning">
                                <i class="bi bi-pencil-square"></i> 
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


            <asp:LinkButton ID="btnAddUser" runat="server" CssClass="buttonCus btn-electric-blue" OnClick="btnAddUser_Click">

                Agregar Usuario
            </asp:LinkButton>

            <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
