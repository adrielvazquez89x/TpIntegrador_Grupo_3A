<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_ModalStock.ascx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UserControl_ModalStock" %>

<!-- Modal de Stock -->
<div class="modal fade" id="stockModal" runat="server" tabindex="-1" aria-labelledby="stockModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="stockModalLabel">Manejar Stock</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>
            <div class="modal-body">
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Código</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="lblStockActual" class="form-label">Cantidad de stock actual:</label>
                    <asp:Label ID="lblStockActual" runat="server" Text="(cantidad)" CssClass="form-label"></asp:Label>
                </div>
                <div class="mb-3">
                    <label for="ddlColor" class="form-label">Color</label>
                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-select">
                        <!-- Opciones de color -->
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="ddlTalle" class="form-label">Talle</label>
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select">
                        <!-- Opciones de talle -->
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtCantidad" class="form-label">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClientClick="hideModal(); return false;" />
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
