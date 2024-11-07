<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UserForm" %>

<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>
<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">

                <h2 class="text-center my-5">Usuario</h2>

                <div class="text-center">
                    <asp:Button ID="btnExit" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="btnExit_Click" />
                </div>

                <!-- Formulario para Agregar Usuario -->
                <div class="form-group">
                    <label for="txtFirstName">Nombre:</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtLastName">Apellido:</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtDni">DNI:</label>
                    <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                </div>


                <div class="form-group">
                    <label for="txtEmail">Correo Electrónico:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtMobile">Teléfono Móvil:</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtBirthDate">Fecha de Nacimiento:</label>
                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <!-- Campo para la contraseña -->
                <div class="form-group" id="divPassword" runat="server">
                    <label for="txtPassword">Contraseña:</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar Usuario" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                </div>
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
