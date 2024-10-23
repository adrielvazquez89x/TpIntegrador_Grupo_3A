<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container vh-100 d-flex align-items-center justify-content-center">
        <div class="row w-100 justify-content-center">
            <div class="col-md-4">
                <div>
                    <h1 class="text-center mb-2">Bienvenido a UrbanGlam</h1>
                </div>
                <div class="card mt-5 shadow-sm border-0">


                    <div class="card-header text-center bg-white border-0">
                        <h3 class="fw-bold">Iniciar Sesión</h3>
                    </div>

                    <div class="card-body">
                        <asp:Panel ID="PanelLogin" runat="server">
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
                            <div class="mb-3">
                                <label for="txtUsername" class="form-label">Nombre de usuario:</label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Ingrese su nombre de usuario"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtPassword" class="form-label">Contraseña:</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control w-100" TextMode="Password" placeholder="Ingresá tu contraseña"></asp:TextBox>
                            </div>

                            <asp:Button
                                ID="btnLogin"
                                runat="server"
                                CssClass="btn btn-primary w-100"
                                Text="Iniciar Sesión"
                                Onclick="btnLogin_Click"
                                />

                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
