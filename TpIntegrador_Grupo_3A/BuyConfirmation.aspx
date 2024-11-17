<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuyConfirmation.aspx.cs" Inherits="TpIntegrador_Grupo_3A.BuyConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container mt-5 text-center">
        <h2>¡Muchas gracias por tu compra!</h2>
        <p>Pronto recibirás un email con la confirmación de tu compra y todos los datos pertinentes.</p>
        <div class="mt-4">
            <asp:Button ID="btnVolverInicio" runat="server" Text="Volver al inicio" CssClass="btn btn-primary" OnClick="btnVolverInicio_Click" />
        </div>
    </div>
</asp:Content>

