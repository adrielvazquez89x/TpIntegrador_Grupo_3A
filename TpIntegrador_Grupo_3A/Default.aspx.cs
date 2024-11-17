using Business;
using Business.ProductAttributes;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Section> sectionList;
        //public List<Product> prodList;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "UrbanGlam";
            if (!IsPostBack)
            {
                BusinessSection businessSection = new BusinessSection();
                sectionList = businessSection.list();

                RptSecciones.DataSource = sectionList;
                RptSecciones.DataBind();

                //BusinessProduct businessProd = new BusinessProduct();
                //prodList = businessProd.list();

                //rptProdList.DataSource = prodList;
                //rptProdList.DataBind();
            }
        }

        protected void RptSecciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Section currentSection = (Section)e.Item.DataItem;
                Repeater rptProdList = (Repeater)e.Item.FindControl("rptProdList"); // Toma el Repeater anidado
                rptProdList.DataSource = currentSection.Products;
                rptProdList.DataBind();
            }
        }

        protected void rptProdList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewProduct")
            {
                string productCode = e.CommandArgument.ToString();
                Response.Redirect($"Details.aspx?Code={productCode}");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            string name = Request.Form["nombre"];
            string sub = Request.Form["asunto"];
            string email = Request.Form["email"];
            string bod = Request.Form["mensaje"];

            var subject = "Mensaje de Contacto: " + sub;
            var body = $"<strong>Nombre:</strong> {name}<br>" +
                       $"<strong>Correo Electrónico:</strong> {email}<br>" +
                       $"<strong>Mensaje:</strong><br>{bod}";


            string to = "programacionsorteos@gmail.com";
            EmailService emailService = new EmailService("programacionsorteos@gmail.com", "rdnnfccpmyfoamap");


            // Llama al método SendEmailAsync de forma asíncrona
            Task.Run(async () => await emailService.SendEmailAsync(to, subject, body));

            Session["Success"] = "ok";
           // Response.Redirect("Default.aspx");
            Control_Toast.ShowToast("Mensaje enviado correctamente.",false);

        }
    }
}