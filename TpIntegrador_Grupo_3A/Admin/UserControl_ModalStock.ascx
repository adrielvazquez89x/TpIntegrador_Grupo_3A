<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_ModalStock.ascx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UserControl_ModalStock" %>

<!-- Modal de Stock -->
<div class="modal fade" id="stockModal" runat="server" tabindex="-1" aria-labelledby="stockModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="stockModalLabel">Manejar Stock</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>
            <div class="modal-body">

                <!-- Información del Producto -->
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Código</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <!-- Lista de Stocks Existentes -->
                <h6>Stock Actual</h6>
                <asp:Repeater ID="rptStock" runat="server">
                    <HeaderTemplate>
                        <div class="list-group">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                                <strong>Color:</strong> <%# Eval("Colour.Description") %> |
                                <strong>Talle:</strong> <%# Eval("Size.Description") %> |
                                <strong>Stock:</strong> <%# Eval("Amount") %>
                            </div>
                            <div>
                                <asp:TextBox 
                                    ID="txtEditStock"
                                    runat="server"
                                    CssClass="form-control d-inline-block w-25"
                                    Text='<%# Eval("Amount") %>'>

                                </asp:TextBox>
                                <asp:Button 
                                    ID="btnUpdateStock" 
                                    runat="server" 
                                    CssClass="btn btn-success btn-sm ms-2" 
                                    CommandName="UpdateStock" 
                                    CommandArgument='<%# Eval("Id") %>' 
                                    Text="Modificar" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Formulario para Agregar Nuevo Stock -->
                <h6 class="mt-4">Agregar Nueva Combinación</h6>
                <div class="mb-3">
                    <label for="ddlColor" class="form-label">Color</label>
                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="ddlTalle" class="form-label">Talle</label>
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtCantidad" class="form-label">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

            </div>
            <div class="modal-footer">
                <asp:Button ID="btnAgregarStock" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnAgregarStock_Click" />
                <asp:Button
                    ID="btnCancelar" 
                    runat="server" 
                    CssClass="btn btn-danger" 
                    Text="Cancelar" 
                    OnClientClick="hideModal(); return false;" />
            </div>
        </div>
    </div>
</div>

<!-- Script para ocultar el modal -->
<script type="text/javascript">
    function hideModal() {
        var toastEl = document.getElementById('<%= stockModal.ClientID %>');
        if (toastEl) {
            var modal = bootstrap.Modal.getInstance(toastEl);
            if (modal) {
                modal.hide();
            }
        }
    }
</script>
