<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchases.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Purchases" %>

<%@ Register Src="~/Control_Toast.ascx" TagPrefix="uc1" TagName="Control_Toast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div class="container my-4">
                <h2 class="text-center mb-4">Mis Compras</h2>

                 <asp:Panel ID="noPurchasesPanel" runat="server" Visible="false">
                    <div class="mx-auto">
                        <h3 class="text-center mt-5">Aún no tienes compras registradas</h3>
                    </div>
                </asp:Panel>

                <div class="row d-flex justify-content-center">

                    <div class="col-12 col-md-8">
                        <asp:GridView
                            ID="dgvPurchases"
                            runat="server"
                            CssClass="table table-striped table-bordered text-center"
                            HeaderStyle-CssClass="table-dark"
                            DataKeyNames="Id"
                            AutoGenerateColumns="false"
                            OnPageIndexChanging="dgvPurchases_PageIndexChanging"
                            AllowPaging="true"
                            PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID Compra" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="date" HeaderText="Fecha de Compra" ItemStyle-CssClass="align-middle"/>
                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-CssClass="align-middle" />
                                <asp:BoundField DataField="State" HeaderText="Estado" ItemStyle-CssClass="align-middle" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">
                                          
                                            <asp:LinkButton
                                                ID="btnViewDetails"
                                                runat="server"
                                                CommandArgument='<%# Eval("Id") %>'
                                                CssClass="btn btn-sm btn-outline-info me-2"
                                                OnClick="btnViewDetails_Click">
                                                Ver Detalles
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
                <asp:GridView
                    ID="dgvPurchaseDetails"
                    runat="server"
                    CssClass="table table-striped table-bordered text-center"
                    AutoGenerateColumns="false"
                    Visible="false">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Producto" />
                        <asp:BoundField DataField="CodProd" HeaderText="Código Producto" />
                        <asp:BoundField DataField="Quantity" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Price" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
                <div class="d-flex justify-content-center mt-4">
                    <asp:Button
                        ID="btnLogout"
                        runat="server"
                        Text="Volver a Inicio"
                        OnClick="btnLogout_Click"
                        CssClass="btn btn-info" />
                </div>

                <uc1:Control_Toast runat="server" ID="Control_Toast" />

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

