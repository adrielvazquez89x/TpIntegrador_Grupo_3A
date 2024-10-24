using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Model;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            BusinessCategory businessCategory = new BusinessCategory();
            string result = businessCategory.Add(new Category { Name = txtCategory.Text });

            if(result == "ok")
            {
                ShowToast("Categoría agregada correctamente.");
                txtCategory.Text = "";
            }
            else
            {
                ShowToast(result);
            }
            
        }

        private void ShowToast(string message)
        {
            ltlToastMessage.Text = message;

            ScriptManager.RegisterStartupScript(this, GetType(), "showToastie",
                   "$(document).ready(function() { $('.toast').toast({ delay: 3000 }).toast('show'); });", true);
        }
    }
}