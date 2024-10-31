<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirm.aspx.cs" Inherits="TpIntegrador_Grupo_3A.ResetPasswordConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="row d-flex align-items-center justify-content-center mt-5">
        <div class="col-6">
            <h2 class="text-center mb-4">Establecer Nueva Contraseña</h2>
            <div class="mb-3">
                <label class="form-label">Nueva Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNewPassword" TextMode="Password" REQUIRED />
            </div>
            <div class="mb-3">
                <label class="form-label">Confirmar Nueva Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmPassword" TextMode="Password" REQUIRED />
            </div>
            <asp:Label Text="" ID="lblConfirmError" CssClass="error-message" runat="server" />
            <asp:Button Text="Restablecer Contraseña" runat="server" CssClass="btn btn-primary" OnClick="btnReset_Click" ID="btnReset" />
        </div>
    </div>
</asp:Content>
