<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalData.aspx.cs" Inherits="TpIntegrador_Grupo_3A.PersonalData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Datos Personales</h2>
        <div class="form-group">
            <label for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtApellido">Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtCelular">Celular</label>
            <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control" placeholder="Celular"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtDNI">DNI</label>
            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="DNI"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Ubicación</label>
        </div>
        <div class="form-group">
            <label for="txtProvincia">Provincia</label>
            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" placeholder="Provincia"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtLocalidad">Localidad</label>
            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Localidad"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtBarrio">Barrio</label>
            <asp:TextBox ID="TxtBarrio" runat="server" CssClass="form-control" placeholder="Barrio"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtCalle">Calle</label>
            <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" placeholder="Calle"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtCP">Código Postal</label>
            <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" placeholder="Código Postal"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtAltura">Altura</label>
            <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control" placeholder="Altura"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" />
        </div>
    </div>

</asp:Content>
