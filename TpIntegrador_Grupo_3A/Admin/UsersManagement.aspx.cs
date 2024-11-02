using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class UsersManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
            }
        }


        private void BindUsers()
        {
            try
            {
                BusinessUser businessUsers = new BusinessUser();
                List<User> users = businessUsers.ListUsers();

      
                dgvUsers.DataSource = users;
                dgvUsers.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las categorías", false);
            }
        }


        protected void dgvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsers.PageIndex = e.NewPageIndex;
            BindUsers();
        }

        protected void dgvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }
    }
}