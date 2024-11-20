using Business;
using Business.ProductAttributes;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Model;
using Model.ProductAttributes;
using Security;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Checkout : System.Web.UI.Page
    {
        public Model.Cart Cart { get; set; }
        public new List<ItemCart> Items = new List<ItemCart>();
        public decimal Total;
        public bool delivery = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionSecurity.ActiveSession(Session["user"]))
                {
                    Model.User user = (Model.User)Session["user"];
                    Cart = ((User)Session["user"]).Cart;

                    if (Cart.Items.Count == 0)
                        Response.Redirect("Cart.aspx", false);

                    Items = user.Cart.Items;
                    Total = user.Cart.SumTotal();

                    txtName.Text = user.FirstName is null ? "" : user.FirstName;
                    txtDni.Text = user.Dni is null ? "" : user.Dni.ToString();
                    txtProvince.Text = user.Address.Province is null ? "" : user.Address.Province;
                    txtTown.Text = user.Address.Town is null ? "" : user.Address.Town;
                    txtDistrict.Text = user.Address.District is null ? "" : user.Address.District;
                    txtCP.Text = user.Address.CP is null ? "" : user.Address.CP.ToString();
                    txtStreet.Text = user.Address.Street is null ? "" : user.Address.Street;
                    txtNumber.Text = user.Address.Number.ToString() is null ? "" : user.Address.Number.ToString();
                    txtFloor.Text = user.Address.Floor is null ? "" : user.Address.Floor;
                    txtUnit.Text = user.Address.Unit is null ? "" : user.Address.Unit;

                    ViewState["delivery"] = false; //inicia con la opcion de retiro en tienda
                }
                else
                {
                    Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                    Response.Redirect("Error.aspx", false);
                }
            }
            else
            {
                if (ViewState["delivery"] != null)
                    delivery = (bool)ViewState["delivery"];
            }
        }

        protected void ddlEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            delivery = ddlEntrega.SelectedValue == "1";  //verdadero, a domicilio
            ViewState["delivery"] = delivery;

            if (ddlEntrega.SelectedValue == "1")
            {
                Model.User user = (Model.User)Session["user"];
                txtProvince.Text = user.Address.Province is null ? "" : user.Address.Province;
                txtTown.Text = user.Address.Town is null ? "" : user.Address.Town;
                txtDistrict.Text = user.Address.District is null ? "" : user.Address.District;
                txtCP.Text = user.Address.CP is null ? "" : user.Address.CP.ToString();
                txtStreet.Text = user.Address.Street is null ? "" : user.Address.Street;
                txtNumber.Text = user.Address.Number.ToString() is null ? "" : user.Address.Number.ToString();
                txtFloor.Text = user.Address.Floor is null ? "" : user.Address.Floor;
                txtUnit.Text = user.Address.Unit is null ? "" : user.Address.Unit;
            }

            UpdatePanelPago.Update();
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];

            var userEmail = user.Email;
            var userName = user.FirstName;

            // Recoger los detalles del formulario
            //string name = txtName.Text;
            //string dni = txtDni.Text;
            //string province = txtProvince.Text;
            //string town = txtTown.Text;
            //string district = txtDistrict.Text;
            //string postalCode = txtCP.Text;
            //string street = txtStreet.Text;
            //string number = txtNumber.Text;
            //string floor = txtFloor.Text;
            //string unit = txtUnit.Text;
            //string deliveryMethod = ddlEntrega.SelectedValue; // "1" para entrega a domicilio, "2" para retiro en tienda
            //string paymentMethod = ddlMetodoPago.SelectedValue; // "1" para efectivo, "2" para tarjeta

            // Crear el objeto de compra (Purchase)
            var purchase = new Purchase
            {
                IdUser = user.UserId,
                date = DateTime.Now,
                Total = user.Cart.SumTotal(),
                State = "Pendiente",  // Estado inicial de la compra
                Details = new List<PurchaseDetail>()
            };

            // Añadir los detalles de la compra (productos del carrito)
            foreach (var item in user.Cart.Items)
            {
                var detail = new PurchaseDetail
                {
                    CodProd = item.Product.Code,
                    Quantity = item.Number,
                    Price = item.Product.Price
                };
                purchase.Details.Add(detail);
            }

            // Llamar al servicio de BusinessPurchase para guardar la compra
            var businessPurchase = new BusisnessPurchase();
            businessPurchase.SavePurchase(purchase);

            //var userEmail = user.Email;
            byte[] pdfBytes = GeneratePdf();


            var subject = "Confirmacion de compra";
            var body = "Adjunto encontrarás el detalle de tu compra en formato PDF.";
            //var body = GenerateBody();

            EmailService emailService = new EmailService("programacionsorteos@gmail.com", "rdnnfccpmyfoamap");

            MemoryStream ms = new MemoryStream(pdfBytes);

            // Crear el archivo adjunto (PDF)
            ms.Position = 0;
            Console.WriteLine($"Tamaño del PDF generado: {ms.Length} bytes");
            var attachment = new System.Net.Mail.Attachment(ms, "ConfirmacionCompra.pdf", "application/pdf");

            Task.Run(() => emailService.SendEmailAsync(userEmail, subject, body, attachment));

            // Vaciar el carrito despues de que ya compro 
            user.Cart.ClearCart();

            Response.Redirect("BuyConfirmation.aspx", false);

        }

        private byte[] GeneratePdf()
        {
            // Crear el archivo PDF en memoria (stream)
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Configuración de estilo (fuentes, tamaños y colores)
                PdfFont fontHeader = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont fontItalic = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

                // Estilo de la tabla
                float[] columnWidths = { 3, 1, 1, 2 };  // Ajuste el tamaño de las columnas
                iText.Layout.Element.Table table = new iText.Layout.Element.Table(columnWidths);

                // Título de la factura
                document.Add(new Paragraph("Factura de Compra")
                    .SetFont(fontHeader)
                    .SetFontSize(18)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(10));

                // Encabezados de la tabla
                table.AddHeaderCell(new Cell().Add(new Paragraph("Producto").SetFont(fontHeader).SetFontSize(12)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad").SetFont(fontHeader).SetFontSize(12)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Precio Unitario").SetFont(fontHeader).SetFontSize(12)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").SetFont(fontHeader).SetFontSize(12)));

                // Agregar los productos del carrito
                Model.User user = (Model.User)Session["user"];
                if (user != null && user.Cart != null && user.Cart.Items != null)
                {
                    foreach (var item in user.Cart.Items)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.Product.Name).SetFont(fontNormal).SetFontSize(12)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Number.ToString()).SetFont(fontNormal).SetFontSize(12)));
                        table.AddCell(new Cell().Add(new Paragraph($"${item.Product.Price:F2}").SetFont(fontNormal).SetFontSize(12)));
                        table.AddCell(new Cell().Add(new Paragraph($"${item.Subtotal:F2}").SetFont(fontNormal).SetFontSize(12)));
                    }

                    // Total de la compra
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("Total").SetFont(fontHeader).SetFontSize(12)).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph($"${user.Cart.SumTotal():F2}").SetFont(fontHeader).SetFontSize(12)));
                }

                // Agregar la tabla al documento
                document.Add(table);

                // Detalles adicionales (pie de página, fecha, etc.)
                document.Add(new Paragraph("\nGracias por tu compra!")
                    .SetFont(fontItalic)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph($"Fecha de compra: {DateTime.Now.ToString("yyyy-MM-dd")}")
                    .SetFont(fontItalic)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("\nPara más detalles, visita nuestro sitio web o contacta con soporte.")
                    .SetFont(fontItalic)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER));

                // Cerrar el documento
                document.Close();

                // Retornar el PDF generado como un arreglo de bytes
                return ms.ToArray();
            }
        }

        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMetodoPago.SelectedValue == "2") // Pago con tarjeta
            {
                PanelTarjeta.Visible = true;
            }
            else // Pago en efectivo
            {
                PanelTarjeta.Visible = false;
                txtTarjetaNumero.Text = string.Empty;
                txtFechaExpiracion.Text = string.Empty;
                txtCVV.Text = string.Empty;
            }
            UpdatePanelPago.Update();
        }

        protected void btnMercadoPago_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            List<string> errores = new List<string>();

            // Validar datos según el método de pago seleccionado
            if (ddlMetodoPago.SelectedValue == "2") // Tarjeta
            {
                if (string.IsNullOrWhiteSpace(txtTarjetaNumero.Text) || txtTarjetaNumero.Text.Length != 19)
                {
                    errores.Add("El número de tarjeta debe tener 19 caracteres en formato XXXX-XXXX-XXXX-XXXX.");
                }

                if (string.IsNullOrWhiteSpace(txtFechaExpiracion.Text) || !ValidarFecha(txtFechaExpiracion.Text))
                {
                    errores.Add("La fecha de vencimiento debe estar en el formato MM/AA y debe ser válida.");
                }

                if (string.IsNullOrWhiteSpace(txtCVV.Text) || txtCVV.Text.Length != 3)
                {
                    errores.Add("El código de seguridad (CVV) debe tener 3 dígitos.");
                }
            }
            

            // Si hay errores, mostrar mensaje y no habilitar el botón
            if (errores.Count > 0)
            {
                lblError.Text = string.Join("<br />", errores);
                lblError.CssClass = "text-danger";
                btnFinalizar.Enabled = false;
                return;
            }

            // Si no hay errores, habilitar el botón "Realizar compra"
            lblError.Text = "Datos confirmados correctamente.";
            lblError.CssClass = "text-success";
            btnFinalizar.Enabled = true;
        }

        private bool ValidarFecha(string fecha)
        {
            try
            {
                if (fecha.Length != 5 || fecha[2] != '/')
                    return false;

                var partes = fecha.Split('/');
                int mes = int.Parse(partes[0]);
                int anio = int.Parse("20" + partes[1]);

                if (mes < 1 || mes > 12)
                    return false;

                DateTime fechaExpiracion = new DateTime(anio, mes, 1).AddMonths(1).AddDays(-1);
                return fechaExpiracion >= DateTime.Now;
            }
            catch
            {
                return false;
            }
        }

    }
}

