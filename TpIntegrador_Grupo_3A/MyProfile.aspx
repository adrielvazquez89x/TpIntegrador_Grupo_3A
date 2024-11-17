<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="TpIntegrador_Grupo_3A.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center m-5">Mi Perfil</h2>
    <div class="row d-flex justify-content-center">
        <div class="col-lg-4 ">

            <div class="mb-3 form-group">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                <asp:RequiredFieldValidator
                    ID="rfvNombre"
                    runat="server"
                    ControlToValidate="txtNombre"
                    InitialValue=""
                    ErrorMessage="Debe cargar un nombre"
                    ForeColor="Red"
                    Display="Dynamic" />

            </div>

            <div class="mb-3 form-group">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
                <asp:RequiredFieldValidator
                    ID="rfvApellido"
                    runat="server"
                    ControlToValidate="txtApellido"
                    InitialValue=""
                    ErrorMessage="Debe cargar el apellido"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Dni</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDni" />
                <asp:RequiredFieldValidator
                    ID="rfvDni"
                    runat="server"
                    ControlToValidate="txtDni"
                    InitialValue=""
                    ErrorMessage="Debe cargar un DNI"
                    ForeColor="Red"
                    Display="Dynamic" />

            </div>


            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" ReadOnly="true" />
                <asp:RequiredFieldValidator
                    ID="rfvEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    InitialValue=""
                    ErrorMessage="Debe cargar un correo electrónico"
                    ForeColor="Red"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator
                    ID="revEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"
                    ErrorMessage="Email no válido"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Celular</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCel" />
                <asp:RegularExpressionValidator
                    ID="revCelular"
                    runat="server"
                    ControlToValidate="txtCel"
                    ValidationExpression="^\d{10}$"
                    ErrorMessage="El número de celular debe tener 10 dígitos"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNacimiento" TextMode="Date" />
                <asp:RequiredFieldValidator
                    ID="rfvNacimiento"
                    runat="server"
                    ControlToValidate="txtNacimiento"
                    InitialValue=""
                    ErrorMessage="Debe seleccionar una fecha de nacimiento"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Imagen de Perfil</label>
                <input type="file" id="txtImagen" runat="server" class="form-control" />
            </div>
            <%--<asp:Image ID="imgNuevoPerfil" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    runat="server" CssClass="img-fluid mb-3" />--%>
        </div>


        <div class="col-lg-4 ">

            <asp:TextBox runat="server" CssClass="form-control" ID="txtIdAdress" Visible="false" />

            <div class="mb-3">
                <label class="form-label">Provincia</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtProvincia" />
                <asp:RequiredFieldValidator
                    ID="rfvProvincia"
                    runat="server"
                    ControlToValidate="txtProvincia"
                    InitialValue=""
                    ErrorMessage="Debe cargar su provincia"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Ciudad</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCiudad" />
                <asp:RequiredFieldValidator
                    ID="rfvCiudad"
                    runat="server"
                    ControlToValidate="txtCiudad"
                    InitialValue=""
                    ErrorMessage="Debe cargar su Ciudad"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Barrio</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtBarrio" />
                <asp:RequiredFieldValidator
                    ID="rfvBarrio"
                    runat="server"
                    ControlToValidate="txtBarrio"
                    InitialValue=""
                    ErrorMessage="Debe cargar su Barrio"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Calle</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCalle" />
                <asp:RequiredFieldValidator
                    ID="rfvCalle"
                    runat="server"
                    ControlToValidate="txtCalle"
                    InitialValue=""
                    ErrorMessage="Debe cargar su Calle"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Numero</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNumero" />
                <asp:RequiredFieldValidator
                    ID="rfvNumero"
                    runat="server"
                    ControlToValidate="txtNumero"
                    InitialValue=""
                    ErrorMessage="Debe cargar un número"
                    ForeColor="Red"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator
                    ID="revNumero"
                    runat="server"
                    ControlToValidate="txtNumero"
                    ValidationExpression="^\d+$"
                    ErrorMessage="Solo números"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Codigo Postal</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCP" />
                <asp:RequiredFieldValidator
                    ID="rfvCP"
                    runat="server"
                    ControlToValidate="txtCP"
                    InitialValue=""
                    ErrorMessage="Debe cargar su Código Postal"
                    ForeColor="Red"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator
                    ID="revCP"
                    runat="server"
                    ControlToValidate="txtCP"
                    ValidationExpression="^\d+$"
                    ErrorMessage="Solo números"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Piso</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPiso" />
                <asp:RegularExpressionValidator
                    ID="revPiso"
                    runat="server"
                    ControlToValidate="txtPiso"
                    ValidationExpression="^\d+$"
                    ErrorMessage="Solo números"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Departamento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDpto" />

                <asp:RegularExpressionValidator
                    ID="revDpto"
                    runat="server"
                    ControlToValidate="txtDpto"
                    ValidationExpression="^[A-Za-z0-9]{1,2}$"
                    ErrorMessage="Solo se permiten 1 o 2 caracteres alfanuméricos."
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>

        </div>
    </div>



    <div class="row m-4">
        <div class=" text-center">
            <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" />
            <a href="/" class="btn btn-secondary">Regresar</a>
        </div>
    </div>
</asp:Content>
