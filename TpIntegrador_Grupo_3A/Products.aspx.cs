using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Model;

namespace TpIntegrador_Grupo_3A
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Product> listProd;
        protected void Page_Load(object sender, EventArgs e)
        {
            string category = Request.QueryString["category"];
            if (!IsPostBack)
            {
                // LoadProductsByCategory(category);    //Carga los productos según la categoría
            }



            var cat = new Category();
            cat.Name = "Noche";
            cat.Id = 1;
            var sea = new Season();
            sea.Id = 1;
            sea.Name = "Verano";
            var sect = new Section();
            sect.Name = "Mujer";
            sect.Id = 1;

            listProd = new List<Product>
            {
                new Product { Id = 1, Name = "Vestido", Price = 1000,
                    Category = cat, Season = sea, Section = sect, 
                    Images = new List<Model.Image>{ new Model.Image { Id= 1, IdProduct = 1, Url= "https://ninayco.com/81667-superlarge_default/vestido-red-shine-.jpg" } } },
                         new Product { Id = 2, Name = "Cazadora", Price = 10400,
                    Category = cat, Season = sea, Section = sect,
                    Images = new List<Model.Image>{ new Model.Image { Id= 2, IdProduct = 2, Url= "https://ninayco.com/81667-superlarge_default/vestido-red-shine-.jpg" } } },
                                  new Product { Id = 3, Name = "Capa de mago", Price = 2000,
                    Category = cat, Season = sea, Section = sect,
                    Images = new List<Model.Image>{ new Model.Image { Id= 3, IdProduct = 3, Url= "https://ninayco.com/81667-superlarge_default/vestido-red-shine-.jpg" } } },
            };
        }
    }
}