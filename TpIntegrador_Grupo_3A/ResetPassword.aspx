<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="TpIntegrador_Grupo_3A.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div class="row d-flex align-items-center justify-content-center mt-5">
        <div class="col-6">
            <h2 class="text-center mb-4">Restablecer Contraseña</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtResetEmail" REQUIRED />
                <asp:Label Text="" ID="lblSend"  ForeColor="Green" runat="server" />
                <asp:Label Text="" ID="lblResetError" CssClass="error-message" ForeColor="Red" runat="server" />
            </div>
            <div class="text-center">
                <asp:Button Text="Enviar Enlace" runat="server" CssClass="btn btn-primary" OnClick="btnSendReset_Click" ID="btnSendReset" />
            </div>
        </div>
    </div>
</asp:Content>
