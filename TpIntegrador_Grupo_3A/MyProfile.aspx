<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="TpIntegrador_Grupo_3A.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center m-5">Mi Perfil</h2>
    <div class="row d-flex justify-content-center">
        <div class="col-lg-4 ">

            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar un nombre" CssClass="validacion" ControlToValidate="txtNombre" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar el apellido" CssClass="validacion" ControlToValidate="txtApellido" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
            </div>
            <div class="mb-3">
                <label class="form-label">Celular</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCel" />
            </div>
            <div class="mb-3">
                <label class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNacimiento" />
            </div>
        </div>


        <div class="col-lg-4 ">

            <div class="mb-3">
                <label class="form-label">Provincia</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtProvincia" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar su provincia" CssClass="validacion" ControlToValidate="txtProvincia" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Ciudad</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCiudad" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar su Ciudad" CssClass="validacion" ControlToValidate="txtCiudad" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Barrio</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtBarrio" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar su Barrio" CssClass="validacion" ControlToValidate="txtBarrio" runat="server" />

            </div>
            <div class="mb-3">
                <label class="form-label">Calle</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCalle" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar su Calle" CssClass="validacion" ControlToValidate="txtCalle" runat="server" />
            </div>

            <div class="mb-3">
                <label class="form-label">Numero</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNumero" />
            </div>

            <div class="mb-3">
                <label class="form-label">Codigo Postal</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCP" />
                <asp:RequiredFieldValidator ErrorMessage="Debe cargar su Codigo Postal" CssClass="validacion" ControlToValidate="txtCP" runat="server" />
            </div>

            <div class="mb-3">
                <label class="form-label">Piso</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="TextPiso" />
            </div>

            <div class="mb-3">
                <label class="form-label">Departamento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDpto" />
            </div>



        </div>



        <div class="row m-4">
            <div class=" text-center">
                <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" runat="server" />
                <a href="/" class="btn btn-secondary">Regresar</a>
            </div>
        </div>
</asp:Content>
