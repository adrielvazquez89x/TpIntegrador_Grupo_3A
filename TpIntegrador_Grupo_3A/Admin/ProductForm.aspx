<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Admin.ProductForm" %>

<%@ Register Src="~/Admin/UserControl_ButtonBack.ascx" TagPrefix="uc1" TagName="UserControl_ButtonBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">

                <h2 class="text-center my-5">Agregar Nuevo Producto</h2>

                <uc1:UserControl_ButtonBack runat="server" ID="UserControl_ButtonBack" />

                <!-- Fila que contiene ambas columnas -->
                <div class="row">

                    <!-- Columna Izquierda -->
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtCode">Código:</label>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtName">Nombre:</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtPrice">Precio:</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtDescription">Descripción:</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlCategory">Categoría:</label>
                            <asp:DropDownList
                                ID="ddlCategory"
                                runat="server"
                                CssClass="form-control"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlSubCategory">Sub-Categoría:</label>
                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlSeason">Temporada:</label>
                            <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
<%--                        <div class="form-group">
                            <label for="ddlSection">Sección:</label>
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>--%>

                        <div class="text-center">
                            <asp:Button
                                ID="btnSave"
                                runat="server"
                                Text="Guardar Producto"
                                CssClass="btn btn-primary"
                                OnClick="btnSave_Click" />
                        </div>
                    </div>


                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtImageUrl">Imágenes (URLs):</label>
                            <div class="input-group mb-2">
                                <asp:TextBox
                                    ID="txtImageUrl"
                                    runat="server"
                                    CssClass="form-control"
                                    Placeholder="Ingresa la URL de la imagen" />
                                <div class="input-group-append">
                                    <asp:Button
                                        ID="btnAddImage"
                                        runat="server"
                                        Text="Agregar"
                                        CssClass="btn btn-secondary"
                                        OnClick="btnAddImage_Click" />
                                </div>
                            </div>
                            <asp:ListBox
                                ID="lstImages"
                                runat="server"
                                CssClass="form-control"
                                SelectionMode="Multiple"
                                Rows="5"
                                OnSelectedIndexChanged="lstImages_SelectedIndexChanged"
                                AutoPostBack="true"
                                ></asp:ListBox>
                            <asp:Button
                                ID="btnRemoveSelected"
                                runat="server"
                                Text="Eliminar Seleccionadas"
                                CssClass="btn btn-danger mt-2"
                                OnClick="btnRemoveSelected_Click" />
                            <asp:Image
                                ID="imgPreview"
                                runat="server"
                                CssClass="img-thumbnail mt-3"
                                Width="200px"
                                Height="200px"
                                Visible="false" />
                        </div>


                        <asp:Label ID="lblMessage" runat="server" CssClass="mt-3"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
