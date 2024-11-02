using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class UserControl_Buttons : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPickCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Categories.aspx");
        }

        protected void btnPickSeason_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Seasons.aspx");
        }

        protected void btnPickSection_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Sections.aspx");
        }

        protected void btnPickColour_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Colours.aspx");
        }

        protected void btnPickSize_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Sizes.aspx");
        }
    }
}