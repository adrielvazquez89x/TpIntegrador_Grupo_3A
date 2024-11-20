using Business;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Model;
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
                    Cart = user.Cart;

                    if (Cart.Items.Count == 0)
                        Response.Redirect("Cart.aspx", false);

                    Items = user.Cart.Items;
                    Total = user.Cart.SumTotal();

                    txtName.Text = user.FirstName ?? "";
                    txtDni.Text = user.Dni?.ToString() ?? "";

                    if (user.Address != null)
                    {
                        txtProvince.Text = user.Address.Province;
                        txtTown.Text = user.Address.Town;
                        txtDistrict.Text = user.Address.District;
                        txtCP.Text = user.Address.CP?.ToString() ?? "";
                        txtStreet.Text = user.Address.Street;
                        txtNumber.Text = user.Address.Number.ToString() ?? "";
                        txtFloor.Text = user.Address.Floor;
                        txtUnit.Text = user.Address.Unit;
                    }
                    else
                    {
                        txtProvince.Text = "";
                        txtTown.Text = "";
                        txtDistrict.Text = "";
                        txtCP.Text = "";
                        txtStreet.Text = "";
                        txtNumber.Text = "";
                        txtFloor.Text = "";
                        txtUnit.Text = "";
                    }

                    ViewState["delivery"] = false; // Inicia con la opción de retiro en tienda

                    // Mostrar el panel de checkout
                    pnlCheckout.Visible = true;
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

                // Gestionar la visibilidad de los paneles basados en 'delivery' y 'ddlMetodoPago'
                pnlDeliveryDetails.Visible = delivery;
                pnlPaymentDetails.Visible = ddlMetodoPago.SelectedValue == "2";
            }

            // Enlazar datos al Repeater
            BindCartItems();
        }

        private void BindCartItems()
        {
            if (Cart != null && Cart.Items.Count > 0)
            {
                var data = new
                {
                    Items = Cart.Items,
                    Total = Cart.SumTotal()
                };
                rptCartItems.DataSource = new List<dynamic> { data };
                rptCartItems.DataBind();
            }
        }

        protected void ddlEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            delivery = ddlEntrega.SelectedValue == "1";
            ViewState["delivery"] = delivery;
            pnlDeliveryDetails.Visible = delivery;
            UpdatePanelDelivery.Update();
        }

        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPaymentDetails.Visible = ddlMetodoPago.SelectedValue == "2";
            UpdatePanelPayment.Update();
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Model.User user = (Model.User)Session["user"];
                var userEmail = user.Email;
                byte[] pdfBytes = GeneratePdf();

                var subject = "Confirmacion de compra";
                var body = "Adjunto encontrarás el detalle de tu compra en formato PDF.";

                EmailService emailService = new EmailService("programacionsorteos@gmail.com", "rdnnfccpmyfoamap");

                using (MemoryStream ms = new MemoryStream(pdfBytes))
                {
                    var attachment = new System.Net.Mail.Attachment(ms, "ConfirmacionCompra.pdf", "application/pdf");
                    Task.Run(() => emailService.SendEmailAsync(userEmail, subject, body, attachment));
                }

                Response.Redirect("BuyConfirmation.aspx", false);
            }
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
                if (Cart != null && Cart.Items != null)
                {
                    foreach (var item in Cart.Items)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.Product.Name).SetFont(fontNormal).SetFontSize(12)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Number.ToString()).SetFont(fontNormal).SetFontSize(12)));
                        table.AddCell(new Cell().Add(new Paragraph($"${item.Product.Price:F2}").SetFont(fontNormal).SetFontSize(12)));
                        table.AddCell(new Cell().Add(new Paragraph($"${item.Subtotal:F2}").SetFont(fontNormal).SetFontSize(12)));
                    }

                    // Total de la compra
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("Total").SetFont(fontHeader).SetFontSize(12)).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph($"${Cart.SumTotal():F2}").SetFont(fontHeader).SetFontSize(12)));
                }

                // Agregar la tabla al documento
                document.Add(table);

                // Detalles adicionales (pie de página, fecha, etc.)
                document.Add(new Paragraph("\nGracias por tu compra!")
                    .SetFont(fontItalic)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph($"Fecha de compra: {DateTime.Now:yyyy-MM-dd}")
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
    }
}





//private byte[] GeneratePdf()
//{
//    // Crear el archivo PDF en memoria (stream)
//    using (MemoryStream ms = new MemoryStream())
//    {
//        PdfWriter writer = new PdfWriter(ms);
//        PdfDocument pdf = new PdfDocument(writer);
//        Document document = new Document(pdf);

//        // Añadir título y contenido
//        document.Add(new Paragraph("Confirmación de compra"));
//        document.Add(new Paragraph("Gracias por tu compra. A continuación, los detalles de la misma:"));

//        // Accedemos al carrito
//        Model.User user = (Model.User)Session["user"];
//        if (user != null && user.Cart != null && user.Cart.Items != null)
//        {
//            foreach (var item in user.Cart.Items)
//            {
//                document.Add(new Paragraph($"{item.Product.Name} - Cantidad: {item.Number} - Subtotal: ${item.Subtotal}"));
//            }

//            document.Add(new Paragraph($"Total: ${user.Cart.SumTotal()}"));
//        }

//        document.Close();
//        //string filePath = Server.MapPath("~/App_Data/BuyConfirmation.pdf");
//        //File.WriteAllBytes(filePath, ms.ToArray());




//        // Devolver el PDF como un string en base64 (si quieres adjuntarlo a un email o almacenarlo)
//        return ms.ToArray();

//    }
//}
//}
