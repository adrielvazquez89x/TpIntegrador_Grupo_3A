<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TpIntegrador_Grupo_3A.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BannerContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager runat="server" />--%>
     <% if (Cart!=null && Cart.Items.Count != 0 && Items != null)
     {%>
    <h1 class="text-center mt-5" id="h1Confirmar">Confirmar Compra</h1>
    <%-- Primera parte del formulario, datos personales --%>
    <div class="d-flex gap-5">
        <div class="col-6">
            <div class="d-flex flex-column">
                <h3>Completa el formulario para finalizar la compra:</h3>
                <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" placeholder="Nombre" CssClass="form-control my-1"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" />
                <span id="spanNombreConfirmar"></span>
                <asp:TextBox ID="txtDni" ClientIDMode="Static" runat="server" placeholder="Dni" CssClass="form-control my-1" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtDni" runat="server" />
                <span id="spanDni"></span>
                <asp:TextBox ID="txtDireccion" ClientIDMode="Static" runat="server" placeholder="Direccion" CssClass="form-control my-1"></asp:TextBox>
                <span id="spanDireccion"></span>
                <asp:RequiredFieldValidator ControlToValidate="txtDireccion" runat="server" />
                <asp:TextBox ID="txtLocalidad" ClientIDMode="Static" runat="server" placeholder="Localidad" CssClass="form-control my-1"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtLocalidad" runat="server" />
                <span id="spanLocalidad"></span>
                <asp:TextBox ID="txtCP" ClientIDMode="Static" runat="server" placeholder="Codigo postal" CssClass="form-control my-1" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtCP" runat="server" />
                <span id="spanCp"></span>
            </div>
            <div>
                <h4>Elija método de pago</h4>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="d-flex flex-column col-8">
                            <asp:DropDownList ID="ddlMetodoPago" ClientIDMode="Static" runat="server" AutoPostBack="true"
                                CssClass="form-control my-1">
                                <asp:ListItem Value="1">Efectivo</asp:ListItem>
                                <asp:ListItem Value="2">Tarjeta</asp:ListItem>
                            </asp:DropDownList>

                            <% if (ddlMetodoPago.SelectedItem.Value == "Tarjeta")
                                {%>
                            <asp:TextBox ID="txtTarjetaNumero"
                                runat="server"
                                placeholder="Ingrese número de tarjeta"
                                CssClass="form-control my-1" Text="1111-1111-1111-1111"></asp:TextBox>
                            <%}%>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="mt-5">
                    <asp:Button ID="btnConfirmarCompra" runat="server"
                        CssClass="btnComprar" Text="Confirmar datos"
                        OnClientClick="return validacionConfirmar()" />
                </div>

            </div>
        </div>
    </div>
<%--Segunda mitad--%>
     <div class="col-6">
     <h3 class="text-center">Carrito</h3>
     <table class="table">
         <thead>
             <tr>
                 <th scope="col">#</th>
                 <th scope="col">Producto</th>
                 <th scope="col">Cantidad</th>
                 <th scope="col">Subtotal</th>
             </tr>
         </thead>
         <tbody>
             <%for (int x = 0; x < Cart.Items.Count; x++)
                 { %>
             <tr>
                 <th scope="row"><%:x+1 %></th>
                 <td><%:Cart.Items[x].Product.Name %> </td>
                 <td><%:Cart.Items[x].Number %></td>
                 <td><%:Cart.Items[x].Subtotal %></td>
             </tr>
                         <tr>
                <td colspan="5"></td>
                <td class="text-right">Total: $<%:Cart.SumTotal()%></td>
            </tr>
             <%} %>
         </tbody>
     </table>
     <div class="d-flex justify-content-end mt-5 ">
         <asp:Button ID="btnFinalizar" runat="server"
             CssClass="btnComprar" Text="Realizar compra"
              />
     </div>
 </div>
                   <%}%>
             
</asp:Content>
