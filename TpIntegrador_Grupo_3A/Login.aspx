﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Login" %>

<%@ Register Src="~/Control_Toast.ascx" TagPrefix="uc1" TagName="Control_Toast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row d-flex align-items-center justify-content-center mt-5 border border-3 shadow p-3 bg-body-tertiary rounded " style="height: 600px; border-radius: 10px">
        <div class="col-6">
            <h2 class="text-center mb-4">Ingresa tus datos</h2>
            <div class="mb-3">



                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email requerido." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Formato de email invalido." CssClass="text-danger" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" Display="Dynamic"></asp:RegularExpressionValidator>
                <div class="container mb-2">
                    <asp:Label ID="lblErrorEmail" runat="server" Visible="false" CssClass="form-label text-danger"></asp:Label>
                </div>
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control w-100" ID="txtPassword" TextMode="Password" required="true" />

            </div>
           
            <div>
                <asp:Label ID="lblError" runat="server" Text="" CssClass="form-label text-danger"></asp:Label>
            </div>
            <div class="text-center m-4 d-flex gap-3 text-center justify-content-center">
                <asp:Button Text="Ingresar" runat="server" CssClass="btn btn-primary" OnClick="btnLogin_Click" ID="btnIniciar" />
                <a href="Default.aspx" class="btn btn-warning">Cancelar</a>
            </div>

            <div class="text-center">
                <a href="ResetPassword.aspx" class="link-primary">¿Olvidaste tu contraseña?</a>
            </div>
        </div>
        <uc1:Control_Toast runat="server" ID="Control_Toast" />
    </div>
</asp:Content>
