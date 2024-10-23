<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PassConfig.aspx.cs" Inherits="TpIntegrador_Grupo_3A.PassConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container vh-100 d-flex align-items-center justify-content-center">
        <div class="row w-100 justify-content-center">
            <div class="col-md-4">

                <div class="card-header text-center bg-white border-0">
                    <h3 class="fw-bold">Cambio de Contraseña</h3>
                </div>

                <div class="card-body">
                    <asp:Panel ID="PanelContraseña" runat="server">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
                        <div class="mb-3">
                            <label for="txtContVieja" class="form-label">Anterior:</label>
                            <asp:TextBox ID="txtContVieja" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Nueva:</label>
                            <asp:TextBox ID="txtContNueva" runat="server" CssClass="form-control w-100" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtContNueva2" class="form-label">Repita Contraseña:</label>
                            <asp:TextBox ID="txtContNueva2" runat="server" CssClass="form-control w-100" TextMode="Password"></asp:TextBox>
                        </div>
                        <asp:Button
                            ID="btnConfirmar"
                            runat="server"
                            CssClass="btn btn-primary w-100"
                            Text="Guardar cambios"
                            OnClick="btnConfirmar_Click" />

                    </asp:Panel>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
