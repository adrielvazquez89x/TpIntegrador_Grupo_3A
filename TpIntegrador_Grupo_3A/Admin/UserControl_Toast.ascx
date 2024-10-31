<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_Toast.ascx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.UserControl_Toast" %>

 <%-- Toastie --%>
<div class="toast-container position-fixed top-0 end-0 p-3">
    <div id="toastDiv" runat="server" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="toast-header">
            <strong class="me-auto">Notificación</strong>
            <small class="text-muted">Ahora</small>
            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Cerrar"></button>
        </div>
        <div class="toast-body">
            <asp:Literal ID="ltlToastMessage" runat="server"></asp:Literal>
        </div>
    </div>
</div>