using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class SiteMaster : MasterPage
    {
        public List<Category> categList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BusinessCategory businessCategory = new BusinessCategory();
                categList = businessCategory.list();

                RepeaterSidebar.DataSource = categList;
                RepeaterSidebar.DataBind();
            }

            if (Request.Url.AbsolutePath.Contains("login") || Request.Url.AbsolutePath.Contains("Login"))
            {
                sidebar.Visible = false;
            }
        }
    }
}