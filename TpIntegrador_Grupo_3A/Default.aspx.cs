using Business;
using Business.ProductAttributes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class _Default : Page
    {
        public List<Section> sectionList;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "UrbanGlam";
            if (!IsPostBack)
            {
                BusinessSection businessSection = new BusinessSection();
                sectionList = businessSection.list();

                RptSecciones.DataSource = sectionList;
                RptSecciones.DataBind();
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
    }
}