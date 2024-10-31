<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_Buttons.ascx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UserControl_Buttons" %>

<div class="d-flex col-12 justify-content-around">
    <div>
        <asp:Button ID="btnPickCategory" runat="server" Text="Categorias" CssClass="buttonCus btn-fuchsia" OnClick="btnPickCategory_Click" />
    </div>
    <div>
        <asp:Button ID="btnPickSeason" runat="server" Text="Temporadas" CssClass="buttonCus btn-electric-blue" OnClick="btnPickSeason_Click" />
    </div>
    <div>
        <asp:Button ID="btnPickSection" runat="server" Text="Secciones" CssClass="buttonCus btn-lime-green" OnClick="btnPickSection_Click" />
    </div>
    <div>
        <asp:Button ID="btnPickColour" runat="server" Text="Colores" CssClass="buttonCus btn-acid-yellow" OnClick="btnPickColour_Click" />
    </div>
    <div>
        <asp:Button ID="btnPickSize" runat="server" Text="Talles" CssClass="buttonCus btn-deep-purple" OnClick="btnPickSize_Click" />
    </div>
</div>

