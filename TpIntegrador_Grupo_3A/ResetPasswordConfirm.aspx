<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirm.aspx.cs" Inherits="TpIntegrador_Grupo_3A.ResetPasswordConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="row d-flex align-items-center justify-content-center mt-5">
        <div class="col-6">
            <h2 class="text-center mb-4">Establecer Nueva Contraseña</h2>

            <div class="mb-3">
                <label class="form-label">Nueva Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNewPassword" TextMode="Password" required="true" />
                <asp:RegularExpressionValidator
                    ID="revNewPassword"
                    runat="server"
                    ControlToValidate="txtNewPassword"
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$"
                    ErrorMessage="La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una letra minúscula y un número."
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Confirmar Nueva Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmPassword" TextMode="Password" required="true" />
                <asp:CompareValidator
                    ID="cvPassword"
                    runat="server"
                    ControlToValidate="txtConfirmPassword"
                    ControlToCompare="txtNewPassword"
                    ErrorMessage="Las contraseñas no coinciden."
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>
            <asp:Label Text="" ID="lblConfirmError" CssClass="error-message" ForeColor="Red" runat="server" />

            <asp:Button Text="Restablecer Contraseña" runat="server" CssClass="btn btn-primary" OnClick="btnReset_Click" ID="btnReset" />
        </div>
    </div>
</asp:Content>
