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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                List<Link> links = new List<Link>
                {
                    new Link { Name = "Buzos", Url = "/products?Idcategory=1", Icon ="bi-arrow-through-heart", Active= false },
                    new Link { Name = "Pantalones", Url = "/products?Idcategory=2", Icon="bi-balloon-heart", Active= false  },
                    new Link { Name = "Remeras", Url = "/products?Idcategory=3", Icon="bi-chat-heart", Active= false  },
                    new Link { Name = "Vestidos", Url = "/products?Idcategory=4", Icon="bi-emoji-heart-eyes", Active = false}
                };

                RepeaterSidebar.DataSource = links;
                RepeaterSidebar.DataBind();
            }

            if (Request.Url.AbsolutePath.Contains("login") || Request.Url.AbsolutePath.Contains("Login"))
            {
                sidebar.Visible = false;
            }
        }
    }
}