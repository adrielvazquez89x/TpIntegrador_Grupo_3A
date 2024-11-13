<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.ProductForm" %>

<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>
<%@ Register Src="~/Admin/UserControl_Toast.ascx" TagPrefix="uc1" TagName="UserControl_Toast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <h2 class="text-center my-5">Gestión de Producto</h2>

                <!-- Fila que contiene ambas columnas -->
                <div class="row">
                    <!-- Columna Izquierda -->
                    <div class="col-md-4">
                        <div class="mb-3">
                            <label for="txtCode" class="form-label">Código:</label>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtName" class="form-label">Nombre:</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtPrice" class="form-label">Precio:</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtDescription" class="form-label">Descripción:</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="ddlCategory" class="form-label">Categoría:</label>
                            <asp:DropDownList
                                ID="ddlCategory"
                                runat="server"
                                CssClass="form-select"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label for="ddlSubCategory" class="form-label">Sub-Categoría:</label>
                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <div class="mb-4">
                            <label for="ddlSeason" class="form-label">Temporada:</label>
                            <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <%--                        
                    <div class="form-group">
                        <label for="ddlSection">Sección:</label>
                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                        --%>

                        <!-- Botones de Guardar y Volver -->
                        <div class="d-flex justify-content-between mt-4">
                            <uc1:UserControl_ButtonBack runat="server" ID="UserControl_ButtonBack" CssClass="btn btn-danger" />
                            <asp:Button
                                ID="btnSave"
                                runat="server"
                                Text="Guardar Producto"
                                CssClass="btn btn-primary"
                                OnClick="btnSave_Click" />
                        </div>
                        <div>
                            <div class="container mt-5">
                                <div class="card shadow">
                                    <div class="card-body">
                                        <!-- Botones de Activar/Desactivar y Eliminar -->
                                        <div class="d-flex justify-content-between mb-4">
                                            <asp:Button
                                                ID="btnToggleEstado"
                                                runat="server"
                                                Text="Desactivar"
                                                CssClass="btn btn-danger" OnClick="btnToggleEstado_Click" />
                                            <asp:Button
                                                ID="btnEliminar"
                                                runat="server"
                                                Text="Eliminar"
                                                CssClass="btn btn-secondary" />
                                        </div>



                                        <div id="divConfirmarEliminar" class="alert alert-warning d-none">
                                            <!-- Información del Producto (puedes agregar más campos según necesidad) -->
                                            <div class="mb-3">
                                                <h4>Eliminación</h4>
                                            </div>
                                            <div class="form-check">
                                                <input type="checkbox" class="form-check-input" id="chkConfirmarEliminar" runat="server"
                                                    onclick="ToggleEliminarDefinitivo()" />
                                                <label class="form-check-label" for="chkConfirmarEliminar">Confirmar eliminación</label>
                                            </div>
                                            <asp:Button
                                                ID="btnEliminarDefinitivo"
                                                runat="server"
                                                Text="Eliminar Definitivamente"
                                                CssClass="btn btn-danger mt-2"
                                                Enabled="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Columna Derecha -->
                    <div class="col-md-8">
                        <div class="mb-4">
                            <label for="txtImageUrl" class="form-label">Imágenes (URLs):</label>
                            <!-- Input para URL -->
                            <div class="input-group">
                                <asp:TextBox
                                    ID="txtImageUrl"
                                    runat="server"
                                    CssClass="form-control"
                                    Placeholder="Ingresa la URL de la imagen" />
                                <!-- Botón Agregar -->
                                <asp:Button
                                    ID="btnAddImage"
                                    runat="server"
                                    Text="Agregar"
                                    CssClass="btn btn-secondary"
                                    OnClick="btnAddImage_Click" />
                            </div>
                        </div>

                        <!-- Lista de Imágenes y Previsualización -->
                        <div class="mb-4">
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <asp:ListBox
                                        ID="lstImages"
                                        runat="server"
                                        CssClass="form-control"
                                        SelectionMode="Multiple"
                                        Rows="5"
                                        OnSelectedIndexChanged="lstImages_SelectedIndexChanged"
                                        AutoPostBack="true"></asp:ListBox>
                                    <asp:Button
                                        ID="btnRemoveSelected"
                                        runat="server"
                                        Text="Eliminar Seleccionadas"
                                        CssClass="btn btn-danger mt-2"
                                        OnClick="btnRemoveSelected_Click" />
                                </div>
                                <div class="col-md-6 text-center">
                                    <asp:Image
                                        ID="imgPreview"
                                        runat="server"
                                        CssClass="img-thumbnail mt-2"
                                        Width="200px"
                                        Height="200px"
                                        Visible="false" />
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="lblMessage" runat="server" CssClass="mt-3"></asp:Label>

                        <!-- Sección de Imágenes Existentes -->
                        <div>
                            <%-- Columna Derecha: Gestión de Imágenes --%>
                            <div class="col-md-6">
                                <%-- Sección de Imágenes Existentes --%>
                                <%
                                    if (currentProductId != 0)
                                    {%>
                                <div class="form-group">
                                    <h4>Imágenes Existentes</h4>
                                    <asp:Repeater ID="rptExistingImages" runat="server">
                                        <HeaderTemplate>
                                            <div class="row">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="col-md-4 text-center image-container mx-2" id="imageContainer_<%# Eval("Id") %>">
                                                <div class="thumbnail position-relative">
                                                    <asp:Image
                                                        ID="imgExisting"
                                                        runat="server"
                                                        ImageUrl='<%# Eval("UrlImage") %>'
                                                        CssClass="img-thumbnail"
                                                        Width="150px"
                                                        Height="150px" />
                                                    <asp:ImageButton
                                                        ID="btnDeleteImage"
                                                        runat="server"
                                                        ImageUrl="~/Images/trash-icon.png"
                                                        CssClass="delete-button"
                                                        ToolTip="Eliminar Imagen"
                                                        OnClientClick='<%# Eval("Id", "return toggleDelete({0});") %>' />
                                                </div>
                                                <asp:HiddenField ID="hfImageId" runat="server" Value='<%# Eval("Id") %>' />
                                                <asp:HiddenField ID="hfOriginalUrl" runat="server" Value='<%# Eval("UrlImage") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <% }
                                %>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfImagesToDelete" runat="server" />
                <uc1:UserControl_Toast runat="server" ID="UserControl_Toast" />
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>



    <script type="text/javascript">
        function toggleDelete(imageId) {
            var imageContainer = document.getElementById('imageContainer_' + imageId);
            var hiddenField = document.getElementById('<%= hfImagesToDelete.ClientID %>');

            if (imageContainer.classList.contains('highlight-delete')) {
                // Si ya está marcada, desmarcarla
                imageContainer.classList.remove('highlight-delete');
                removeImageIdFromHiddenField(imageId, hiddenField);
            } else {
                // Marcar para eliminación
                imageContainer.classList.add('highlight-delete');
                addImageIdToHiddenField(imageId, hiddenField);
            }

            // Evitar que el botón cause un postback
            return false;
        }

        function addImageIdToHiddenField(imageId, hiddenField) {
            var ids = hiddenField.value ? hiddenField.value.split(',') : [];
            if (ids.indexOf(imageId.toString()) === -1) {
                ids.push(imageId);
                hiddenField.value = ids.join(',');
            }
        }

        function removeImageIdFromHiddenField(imageId, hiddenField) {
            var ids = hiddenField.value ? hiddenField.value.split(',') : [];
            var index = ids.indexOf(imageId.toString());
            if (index !== -1) {
                ids.splice(index, 1);
                hiddenField.value = ids.join(',');
            }
        }
    </script>
</asp:Content>
