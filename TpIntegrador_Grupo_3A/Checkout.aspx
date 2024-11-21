<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Checkout" EnableEventValidation="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h1 class="text-center mb-4" id="h1Confirmar">Confirmar Compra</h1>
        <div class="row">
            <!-- Primera columna: Formulario -->
            <div class="col-lg-6">
                <h3 class="mb-4">Completar para finalizar la compra:</h3>
                <div>
                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" placeholder="Nombre" CssClass="form-control mb-3" />
                        <span id="errorName" class="text-danger"></span>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtDni" runat="server" placeholder="DNI" CssClass="form-control mb-3" TextMode="Number" />
                        <span id="errorDni" class="text-danger"></span>
                    </div>

                    <!-- Campos de dirección, inicialmente ocultos -->
                    <div id="addressContainer" class="d-none">
                        <div class="form-group">
                            <asp:TextBox ID="txtProvince" runat="server" placeholder="Provincia" CssClass="form-control mb-3" />
                            <span id="errorProvince" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtTown" runat="server" placeholder="Ciudad" CssClass="form-control mb-3" />
                            <span id="errorTown" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtDistrict" runat="server" placeholder="Distrito" CssClass="form-control mb-3" />
                            <span id="errorDistrict" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtCP" runat="server" placeholder="Código Postal" CssClass="form-control mb-3" />
                            <span id="errorCP" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtStreet" runat="server" placeholder="Calle" CssClass="form-control mb-3" />
                            <span id="errorStreet" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtNumber" runat="server" placeholder="Altura" CssClass="form-control mb-3" />
                            <span id="errorNumber" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtFloor" runat="server" placeholder="Piso" CssClass="form-control mb-3" />
                            <span id="errorFloor" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtUnit" runat="server" placeholder="Departamento" CssClass="form-control mb-3" />
                            <span id="errorUnit" class="text-danger"></span>
                        </div>
                    </div>

                    <h4 class="mt-4">Método de entrega</h4>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlEntrega" runat="server" CssClass="form-select mb-3">
                            <asp:ListItem Value="1">A domicilio</asp:ListItem>
                            <asp:ListItem Value="2" Selected="True">Retiro en sucursal</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <h4 class="mt-4">Método de pago</h4>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select mb-3">
                            <asp:ListItem Value="1">Efectivo</asp:ListItem>
                            <asp:ListItem Value="2">Tarjeta</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!-- Detalles de pago con tarjeta -->
                    <div id="panelTarjeta" class="mt-3" style="display: none;">
                        <div class="form-group">
                            <asp:TextBox ID="txtTarjetaNumero" runat="server" CssClass="form-control mb-3"
                                placeholder="Nro de tarjeta (XXXX-XXXX-XXXX-XXXX)" MaxLength="19" />
                            <span id="errorTarjetaNumero" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtFechaExpiracion" runat="server" CssClass="form-control mb-3"
                                placeholder="Fecha vencimiento (MM/AA)" MaxLength="5" />
                            <span id="errorFechaExpiracion" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control mb-3" TextMode="Password"
                                placeholder="Código de seguridad (CVV)" MaxLength="3" />
                            <span id="errorCVV" class="text-danger"></span>
                        </div>
                    </div>

                    <div id="panelEfectivo" class="mt-3" style="display: none;">
                        <p>Se abonará al recibir el producto</p>

                    </div>

                    <asp:Button 
                        ID="btnConfirmarCompra" 
                        runat="server" 
                        CssClass="btn btn-primary btn-lg mt-4"
                        UseSubmitBehavior="false" 
                        Text="Confirmar datos" 
                        OnClientClick="return validarFormulario();" 
                        />
                    <asp:Label ID="lblError" runat="server" CssClass="mt-3"></asp:Label>
                </div>
            </div>

            <!-- Segunda columna: Resumen Carrito -->
            <div class="col-lg-6">
                <h3 class="text-center mb-4">Carrito</h3>
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Producto</th>
                            <th>Cantidad</th>
                            <th>Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <% if (Cart != null && Cart.Items != null)
                            {
                                for (int x = 0; x < Cart.Items.Count; x++)
                                { %>
                        <tr>
                            <th scope="row"><%= x + 1 %></th>
                            <td><%= Cart.Items[x].Product.Name %></td>
                            <td><%= Cart.Items[x].Number %></td>
                            <td>$<%= Cart.Items[x].Subtotal %></td>
                        </tr>
                        <%    }
                            } %>
                        <tr class="table-active">
                            <td colspan="3" class="text-end"><strong>Total:</strong></td>
                            <td>$<%= Cart != null ? Cart.SumTotal().ToString("F2") : "0.00" %></td>
                        </tr>
                    </tbody>
                </table>
                <div class="d-flex justify-content-end">
                    <asp:Button 
                        ID="btnFinalizar" 
                        runat="server"
                        ClientIDMode="Static"
                        CssClass="btn btn-success btn-lg" 
                        Text="Realizar compra" 
                        OnClick="btnFinalizar_Click" 
                        Style="display: none;"
                        />
                </div>
            </div>
        </div>
    </div>

<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var ddlEntrega = document.getElementById('<%= ddlEntrega.ClientID %>');
        var addressContainer = document.getElementById('addressContainer');

        function toggleAddressFields() {
            if (ddlEntrega.value === "1") { // A domicilio
                addressContainer.classList.remove('d-none');
            } else {
                addressContainer.classList.add('d-none');
            }
        }

        // Inicializar la visibilidad
        toggleAddressFields();

        // Evento al cambiar el método de entrega
        ddlEntrega.addEventListener('change', toggleAddressFields);

        // Controlar la visibilidad de los paneles de pago
        var ddlMetodoPago = document.getElementById('<%= ddlMetodoPago.ClientID %>');
        var panelTarjeta = document.getElementById('panelTarjeta');

        togglePaymentPanels();


        ddlMetodoPago.addEventListener('change', togglePaymentPanels);


    });

    // Función de validación del formulario
    function validarFormulario() {
        var esValido = true;

        // Limpiar mensajes de error
        var errores = document.getElementsByClassName('text-danger');
        for (var i = 0; i < errores.length; i++) {
            errores[i].innerText = '';
        }

        // Validar nombre
        var txtName = document.getElementById('<%= txtName.ClientID %>');
        if (txtName.value.trim() === '') {
            document.getElementById('errorName').innerText = 'El nombre es obligatorio.';
            esValido = false;
        }

        // Validar DNI
        var txtDni = document.getElementById('<%= txtDni.ClientID %>');
        if (txtDni.value.trim() === '') {
            document.getElementById('errorDni').innerText = 'El DNI es obligatorio.';
            esValido = false;
        }

        // Validar dirección si es a domicilio
        var ddlEntrega = document.getElementById('<%= ddlEntrega.ClientID %>');
        if (ddlEntrega.value === "1") {
            var camposDireccion = [
                { id: '<%= txtProvince.ClientID %>', errorId: 'errorProvince', mensaje: 'La provincia es obligatoria.' },
                { id: '<%= txtTown.ClientID %>', errorId: 'errorTown', mensaje: 'La ciudad es obligatoria.' },
                //{ id: '<%= txtDistrict.ClientID %>', errorId: 'errorDistrict', mensaje: 'El distrito es obligatorio.' },
                { id: '<%= txtCP.ClientID %>', errorId: 'errorCP', mensaje: 'El código postal es obligatorio.' },
                { id: '<%= txtStreet.ClientID %>', errorId: 'errorStreet', mensaje: 'La calle es obligatoria.' },
                { id: '<%= txtNumber.ClientID %>', errorId: 'errorNumber', mensaje: 'La altura es obligatoria.' },
                //{ id: '<%= txtFloor.ClientID %>', errorId: 'errorFloor', mensaje: 'El piso es obligatorio.' },
                //{ id: '<%= txtUnit.ClientID %>', errorId: 'errorUnit', mensaje: 'El departamento es obligatorio.' },
            ];

            camposDireccion.forEach(function (campo) {
                var input = document.getElementById(campo.id);
                if (input.value.trim() === '') {
                    document.getElementById(campo.errorId).innerText = campo.mensaje;
                    esValido = false;
                }
            });
        }

        // Validar método de pago
        var ddlMetodoPago = document.getElementById('<%= ddlMetodoPago.ClientID %>');
        if (ddlMetodoPago.value === "2") { // Tarjeta
            var txtTarjetaNumero = document.getElementById('<%= txtTarjetaNumero.ClientID %>');
            var txtFechaExpiracion = document.getElementById('<%= txtFechaExpiracion.ClientID %>');
            var txtCVV = document.getElementById('<%= txtCVV.ClientID %>');

            if (txtTarjetaNumero.value.trim() === '') {
                document.getElementById('errorTarjetaNumero').innerText = 'El número de tarjeta es obligatorio.';
                esValido = false;
            } else if (!/^\d{4}-\d{4}-\d{4}-\d{4}$/.test(txtTarjetaNumero.value.trim())) {
                document.getElementById('errorTarjetaNumero').innerText = 'El número de tarjeta debe tener el formato XXXX-XXXX-XXXX-XXXX.';
                esValido = false;
            }

            if (txtFechaExpiracion.value.trim() === '') {
                document.getElementById('errorFechaExpiracion').innerText = 'La fecha de expiración es obligatoria.';
                esValido = false;
            } else if (!/^\d{2}\/\d{2}$/.test(txtFechaExpiracion.value.trim())) {
                document.getElementById('errorFechaExpiracion').innerText = 'La fecha de expiración debe tener el formato MM/AA.';
                esValido = false;
            }

            if (txtCVV.value.trim() === '') {
                document.getElementById('errorCVV').innerText = 'El CVV es obligatorio.';
                esValido = false;
            } else if (!/^\d{3}$/.test(txtCVV.value.trim())) {
                document.getElementById('errorCVV').innerText = 'El CVV debe tener 3 dígitos.';
                esValido = false;
            }
        }

        // Obtener el botón btnFinalizar por su ID estático
        var btnFinalizar = document.getElementById('btnFinalizar');

        // Mostrar u ocultar el botón Realizar compra según la validación
        if (esValido) {
            btnFinalizar.style.display = 'block';
        } else {
            btnFinalizar.style.display = 'none';
        }
        console.log('btnFinalizar:', btnFinalizar);

        return esValido;
    }
</script>

</asp:Content>
