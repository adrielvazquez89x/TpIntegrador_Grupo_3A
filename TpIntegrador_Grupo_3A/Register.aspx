<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Register" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row d-flex align-items-center justify-content-center mt-5 border border-3 shadow p-3 bg-body-tertiary rounded " style="height: 600px; border-radius: 10px">
        <div class="col-6">
            <h2 class="text-center mb-4">Crea tu perfil</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" REQUIRED />
                <asp:Label Text="" ID="lblError" CssClass="error-message" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" REQUIRED />
            </div>

            <div class="mb-3">
                <label class="form-label">Confirmar contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmPass" TextMode="Password" REQUIRED />
            </div>

            <div class="text-center m-4 d-flex gap-3 text-center justify-content-center">
                <asp:Button Text="Registrarse" runat="server" CssClass="btn btn-primary" OnClick="btnRegistrarse_Click" ID="btnRegistrarse" />
                <a href="Default.aspx" class="btn btn-warning">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
