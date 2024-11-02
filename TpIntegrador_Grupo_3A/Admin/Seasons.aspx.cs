using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TpIntegrador_Grupo_3A.Admin.Seasons;


namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Seasons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSeasons();
        }

        protected void btnAddSeason_Click(object sender, EventArgs e)
        {
            BusinessSeason businessSeason = new BusinessSeason();
            string result = "";

            //Manejamos el botón de agregar y editar
            if (btnAddSeason.Text == "Agregar")
            {
                result = businessSeason.Add(new Season { Description = txtSeason.Text.Trim() });
            }
            else
            {
                if (Session["ObjectInMod"] != null)
                {
                    Season season = (Season)Session["ObjectInMod"];
                    season.Description = txtSeason.Text.Trim();
                    result = businessSeason.Update(season);
                    Session["ObjectInMod"] = null;
                    btnAddSeason.Text = "Agregar";
                }
            }
            //Para mostrar el toast
            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Temporada agregada correctamente.", true);
                txtSeason.Text = "";

                BindSeasons();
            }
            else
            {
                UserControl_Toast.ShowToast(result, false);
            }
            //CurrentOption = Buttons.Season;
        }

        private void BindSeasons()
        {
            try
            {
                BusinessSeason businessSeason = new BusinessSeason();
                List<Season> seasons = businessSeason.list();

                dgvSeasons.DataSource = seasons;
                dgvSeasons.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las temporadas", false);
            }
        }

        protected void btnEditSeason_Click(object sender, EventArgs e)
        {
            Season season = GetSeasonFromCommandArgument(sender);

            Session["ObjectInMod"] = season;

            txtSeason.Text = season.Description;
            btnAddSeason.Text = "Editar";
            //CurrentOption = Buttons.Season;
        }
        protected void btnDeleteSeason_Click(object sender, EventArgs e)
        {
            Season season = GetSeasonFromCommandArgument(sender);
            BusinessSeason businessSeason = new BusinessSeason();

            string result = businessSeason.Delete(season.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Eliminada correctamente", true);
                //CurrentOption = Buttons.Season;
                BindSeasons();
            }

        }
        protected void btnActivateSeason_Click(object sender, EventArgs e)
        {
            Season season = GetSeasonFromCommandArgument(sender);
            BusinessSeason businessSeason = new BusinessSeason();

            string result = businessSeason.Activate(season.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Activada correctamente", true);
                //CurrentOption = Buttons.Season;
                BindSeasons();
            }
        }
        private Season GetSeasonFromCommandArgument(object sender)
        {
            //Viene el botón de editar, se hace un split para obtener los datos
            LinkButton btn = (LinkButton)sender;
            //Separamos todo el string, con el método split, muy similar a js
            string[] args = btn.CommandArgument.Split('|');
            //Parseamos los datos, porque split me devuelve un array de strings
            int seasonId = int.Parse(args[0]);
            string seasonName = args[1];
            bool seasonActive = bool.Parse(args[2]);

            return new Season { Id = seasonId, Description = seasonName, Active = seasonActive };
        }
        protected void dgvSeasons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSeasons.PageIndex = e.NewPageIndex;
            BindSeasons();
            //CurrentOption = Buttons.Season;
        }
    }
}