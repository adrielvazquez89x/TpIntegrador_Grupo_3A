<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UserForm" %>

<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>
<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">

                <h2 class="text-center my-5">Usuario</h2>

                <div class="text-center">
                    <asp:Button ID="btnExit" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="btnExit_Click" />
                </div>

                <!-- Formulario para Agregar Usuario -->
                <div class="form-group">
                    <label for="txtFirstName">Nombre:</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvFirstName"
                        runat="server"
                        ControlToValidate="txtFirstName"
                        InitialValue=""
                        ErrorMessage="Este campo es requerido"
                        ForeColor="Red"
                        Display="Dynamic" />
                </div>

                <div class="form-group">
                    <label for="txtLastName">Apellido:</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvLastName"
                        runat="server"
                        ControlToValidate="txtLastName"
                        InitialValue=""
                        ErrorMessage="Este campo es requerido"
                        ForeColor="Red"
                        Display="Dynamic" />
                </div>

                <div class="form-group">
                    <label for="txtDni">DNI:</label>
                    <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvDni"
                        runat="server"
                        ControlToValidate="txtDni"
                        InitialValue=""
                        ErrorMessage="Este campo es requerido"
                        ForeColor="Red"
                        Display="Dynamic" />

                    <asp:RegularExpressionValidator
                        ID="revDni"
                        runat="server"
                        ControlToValidate="txtDni"
                        ValidationExpression="^\d+$"
                        ErrorMessage="El DNI solo puede contener números"
                        ForeColor="Red"
                        Display="Dynamic" />
                </div>


                <div class="form-group">
                    <label for="txtEmail">Correo Electrónico:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        InitialValue=""
                        ErrorMessage="Este campo es requerido"
                        ForeColor="Red"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator
                        ID="revEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ValidationExpression="^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"
                        ErrorMessage="Formato de correo no válido"
                        ForeColor="Red"
                        Display="Dynamic" />
                </div>

                <div class="form-group">
                    <label for="txtMobile">Teléfono Móvil:</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtBirthDate">Fecha de Nacimiento:</label>
                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:CustomValidator
                        ID="cvBirthDate"
                        runat="server"
                        ControlToValidate="txtBirthDate"
                        OnServerValidate="cvBirthDate_ServerValidate"
                        ErrorMessage="La fecha de nacimiento no puede ser mayor a la fecha actual."
                        ForeColor="Red"
                        Display="Dynamic" />
                </div>

                <!-- Campo para la contraseña -->
                <div class="form-group" id="divPassword" runat="server">
                    <label for="txtPassword">Contraseña:</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvPassword"
                        runat="server"
                        ControlToValidate="txtPassword"
                        InitialValue=""
                        ErrorMessage="Este campo es requerido"
                        ForeColor="Red"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator
                        ID="revPassword"
                        runat="server"
                        ControlToValidate="txtPassword"
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$"
                        ErrorMessage="La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una letra minúscula y un número."
                        ForeColor="Red"
                        Display="Dynamic"
                        EnableClientScript="true" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnSave" runat="server" Text="" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                </div>
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
