<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row d-flex align-items-center justify-content-center mt-5 border border-3 shadow p-3 bg-body-tertiary rounded " style="height: 600px; border-radius: 10px">
        <div class="col-6">
            <h2 class="text-center mb-4">Ingresa tus datos</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" REQUIRED />
                <asp:Label Text="" ID="lblError" CssClass="error-message" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" REQUIRED />
            </div>
            <div class="text-center m-4 d-flex gap-3 text-center justify-content-center">
                <asp:Button Text="Ingresar" runat="server" CssClass="btn btn-primary" OnClick="btnLogin_Click" ID="btnIniciar" />
                <a href="Default.aspx" class="btn btn-warning">Cancelar</a>
            </div>

            <div class="text-center">
                <a href="ResetPassword.aspx" class="link-primary">¿Olvidaste tu contraseña?</a>
            </div>
        </div>
    </div>
</asp:Content>
