<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Register" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row d-flex align-items-center justify-content-center mt-5 border border-3 shadow p-3 bg-body-tertiary rounded " style="height: 600px; border-radius: 10px">
        <div class="col-12 col-md-6">
            <h2 class="text-center mb-4">Crea tu perfil</h2>

            <div class="mb-3">
                <label class="form-label" for="txtEmail">Email</label>
                <asp:TextBox runat="server" CssClass="form-control w-100" ID="txtEmail" REQUIRED />
                <asp:Label Text="" ID="lblError" CssClass="error-message" runat="server" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es requerido." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El correo electrónico no tiene un formato válido" CssClass="text-danger" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label" for="txtPassword">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" required="true" />
                <asp:RequiredFieldValidator 
                    ID="rfvPassword" 
                    runat="server" 
                    ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es requerida."
                    CssClass="text-danger" Display="Dynamic" />


                <asp:RegularExpressionValidator
                    ID="revPassword"
                    runat="server"
                    ControlToValidate="txtPassword"
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$"
                    ErrorMessage="La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una letra minúscula y un número."
                    ForeColor="Red"
                    Display="Dynamic" />



            </div>

            <div class="mb-3 bg-red">
                <label class="form-label">Confirmar contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control  w-100" ID="txtConfirmPass" TextMode="Password" REQUIRED />
                <asp:RequiredFieldValidator ID="rfvConfirmPass" runat="server" ControlToValidate="txtConfirmPass" ErrorMessage="La confirmación de la contraseña es requerida." CssClass="text-danger" Display="Dynamic" />
                <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtConfirmPass" ControlToCompare="txtPassword" ErrorMessage="Las contraseñas no coinciden." CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="text-center m-4 d-flex gap-3 text-center justify-content-center">
                <asp:Button Text="Registrarse" runat="server" CssClass="btn btn-primary" OnClick="btnRegistrarse_Click" ID="btnRegistrarse" />
                <a href="Default.aspx" class="btn btn-warning">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
