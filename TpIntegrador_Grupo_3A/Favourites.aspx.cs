using Business;
using Business.ProductAttributes;
using Model;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Favourites : System.Web.UI.Page
    {
        public List<Product> ProdList = new List<Product>();
        public List<FavouriteProducts> FavIdList = new List<FavouriteProducts>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (SessionSecurity.ActiveSession(Session["user"]))
                    {
                        Model.User user = (Model.User)Session["user"];
                        FavIdList = listFavIdByUser(user.UserId);

                        ProdList = listProdFav(FavIdList);

                        rptFav.DataSource = ProdList;
                        rptFav.DataBind();
                    }
                    else
                    {
                        Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                        Response.Redirect("Error.aspx", false);
                    }
                }
               
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        private List<FavouriteProducts> listFavIdByUser(int idUser)
        {
            BusinessFavourite businessFav = new BusinessFavourite();
            List<FavouriteProducts> favProdList = new List<FavouriteProducts>();

            favProdList = businessFav.list(idUser);

            return favProdList;
        }

        private List<Product> listProdFav(List<FavouriteProducts> FavIdList)
        {
            List<Product> aux = new List<Product>();
            Product prod = new Product();
            BusinessProduct businessProd = new BusinessProduct();

            foreach (FavouriteProducts fav in FavIdList)
            {
               prod= businessProd.listByCode(fav.ProductCode);
               aux.Add(prod);
            }
            return aux;
        }

        protected void bntDeleteFav_Click(object sender, EventArgs e)
        {
            string codeProdFav = ((LinkButton)sender).CommandArgument.ToString();
            int idUser = ((Model.User)Session["user"]).UserId;

            try
            {
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Delete(idUser, codeProdFav);

                FavIdList = listFavIdByUser(idUser);

                ProdList = listProdFav(FavIdList);

                rptFav.DataSource = ProdList;
                rptFav.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}