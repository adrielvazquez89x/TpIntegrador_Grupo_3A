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
using System.IO;
using System.Threading.Tasks;
using System.Web.UI;

namespace TpIntegrador_Grupo_3A
{
    public partial class Checkout : System.Web.UI.Page
    {
        public Model.Cart Cart { get; set; }
        public new List<ItemCart> Items = new List<ItemCart>();
        public decimal Total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionSecurity.ActiveSession(Session["user"]))
            {
                Model.User user = (Model.User)Session["user"];
                Cart = user.Cart;

                if (Cart == null || Cart.Items == null || Cart.Items.Count == 0)
                {
                   
                    Response.Redirect("Cart.aspx", false);
                    return;
                }

                Items = Cart.Items;
                Total = Cart.SumTotal();

                if (!IsPostBack)
                {
                    
                    txtName.Text = user.FirstName ?? "";
                    txtDni.Text = user.Dni?.ToString() ?? "";

                    if (user.Address != null)
                    {
                        txtProvince.Text = user.Address.Province ?? "";
                        txtTown.Text = user.Address.Town ?? "";
                        txtDistrict.Text = user.Address.District ?? "";
                        txtCP.Text = user.Address.CP?.ToString() ?? "";
                        txtStreet.Text = user.Address.Street ?? "";
                        txtNumber.Text = user.Address.Number.ToString() ?? "";
                        txtFloor.Text = user.Address.Floor ?? "";
                        txtUnit.Text = user.Address.Unit ?? "";
                    }
                }
            }
            else
            {
                Session.Add("error", "Debes estar logueado para ingresar a esta sección");
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];

            var userEmail = user.Email;
            var userName = user.FirstName;
            var purchase = new Purchase
            {
                IdUser = user.UserId,
                date = DateTime.Now,
                Total = user.Cart.SumTotal(),
                State = "Pendiente", 
                Details = new List<PurchaseDetail>()
            };

           
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

            
            var businessPurchase = new BusisnessPurchase();
            businessPurchase.SavePurchase(purchase);

            
            byte[] pdfBytes = GeneratePdf();


            var subject = "Confirmacion de compra";
            var body = "Adjunto encontrarás el detalle de tu compra en formato PDF.";
         

            EmailService emailService = new EmailService("programacionsorteos@gmail.com", "rdnnfccpmyfoamap");

            MemoryStream ms = new MemoryStream(pdfBytes);

            
            ms.Position = 0;
            Console.WriteLine($"Tamaño del PDF generado: {ms.Length} bytes");
            var attachment = new System.Net.Mail.Attachment(ms, "ConfirmacionCompra.pdf", "application/pdf");

            Task.Run(() => emailService.SendEmailAsync(userEmail, subject, body, attachment));

             
            user.Cart.ClearCart();

            Response.Redirect("BuyConfirmation.aspx", false);
        }

        private byte[] GeneratePdf()
        {
          
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

              
                PdfFont fontHeader = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont fontItalic = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

                
                float[] columnWidths = { 3, 1, 1, 2 };  
                iText.Layout.Element.Table table = new iText.Layout.Element.Table(columnWidths);

                
                document.Add(new Paragraph("Factura de Compra")
                    .SetFont(fontHeader)
                    .SetFontSize(18)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(10));

             
                table.AddHeaderCell(new Cell().Add(new Paragraph("Producto").SetFont(fontHeader).SetFontSize(12)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad").SetFont(fontHeader).SetFontSize(12)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Precio Unitario").SetFont(fontHeader).SetFontSize(12)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").SetFont(fontHeader).SetFontSize(12)));

               
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

                    
                    table.AddCell(new Cell(1, 3).Add(new Paragraph("Total").SetFont(fontHeader).SetFontSize(12)).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph($"${user.Cart.SumTotal():F2}").SetFont(fontHeader).SetFontSize(12)));
                }

                
                document.Add(table);

               
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

               
                document.Close();

                
                return ms.ToArray();
            }
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            List<string> errores = new List<string>();

           
            if (ddlMetodoPago.SelectedValue == "2") 
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

            
            if (ddlEntrega.SelectedValue == "1") 
            {
                if (string.IsNullOrWhiteSpace(txtProvince.Text) ||
                    string.IsNullOrWhiteSpace(txtTown.Text) ||
                    string.IsNullOrWhiteSpace(txtDistrict.Text) ||
                    string.IsNullOrWhiteSpace(txtCP.Text) ||
                    string.IsNullOrWhiteSpace(txtStreet.Text) ||
                    string.IsNullOrWhiteSpace(txtNumber.Text) ||
                    string.IsNullOrWhiteSpace(txtFloor.Text) ||
                    string.IsNullOrWhiteSpace(txtUnit.Text))
                {
                    errores.Add("Todos los campos de dirección son obligatorios para entrega a domicilio.");
                }
            }

            
            if (errores.Count > 0)
            {
                lblError.Text = string.Join("<br />", errores);
                lblError.CssClass = "text-danger";
                btnFinalizar.Enabled = false;
                return;
            }

            
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

        protected void btnMercadoPago_Click(object sender, EventArgs e)
        {
            
        }
    }
}
