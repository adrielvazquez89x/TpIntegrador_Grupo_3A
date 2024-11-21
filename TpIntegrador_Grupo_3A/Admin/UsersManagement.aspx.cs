using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TpIntegrador_Grupo_3A.Admin.Categories;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class UsersManagement : System.Web.UI.Page
    {

        public Buttons CurrentOption;
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
                List<Model.User> users = businessUsers.ListUsers();

      
                dgvUsers.DataSource = users;
                dgvUsers.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar los usuarios", false);
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
            Response.Redirect("UserForm.aspx");
        }


        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            
            LinkButton btn = (LinkButton)sender;

            

            string userId = btn.CommandArgument;
            Response.Redirect("UserForm.aspx?id=" + userId);

        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            Model.User user = GetUserFromCommandArgument(sender);
            BusinessUser businessUser = new BusinessUser();

            string result = businessUser.Delete(user.UserId);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Usuario dado de baja", true);
  
                BindUsers();
            }
            else
            {
                
                UserControl_Toast.ShowToast("Error al dar de baja al usuario", false);
            }
        }

   

        protected void btnActivateUser_Click(object sender, EventArgs e)
        {
            Model.User user = GetUserFromCommandArgument(sender);
            BusinessUser businessUser = new BusinessUser();

            string result = businessUser.Activate(user.UserId);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Usuario Activado correctamente", true);
                BindUsers();
            }
            else
            {
                
                UserControl_Toast.ShowToast("Error al activar el usuario", false);
            }
        }


        private Model.User GetUserFromCommandArgument(object sender)
        {
            
            LinkButton btn = (LinkButton)sender;
           
            string[] args = btn.CommandArgument.Split('|');
            
            int userId = int.Parse(args[0]);
            bool userActive = bool.Parse(args[1]);

            return new Model.User { UserId = userId, Active = userActive };
        }
    }
}