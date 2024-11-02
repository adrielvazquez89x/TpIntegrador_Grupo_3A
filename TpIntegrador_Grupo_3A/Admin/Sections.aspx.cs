using Business;
using Business.ProductAttributes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Sections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSections();
        }

        protected void btnAddSection_Click(object sender, EventArgs e)
        {
            BusinessSection businessSection = new BusinessSection();
            string result = "";

            //Manejamos el botón de agregar y editar
            if (btnAddSection.Text == "Agregar")
            {
                result = businessSection.Add(new Section { Description = txtSection.Text.Trim() });
            }
            else
            {
                if (Session["ObjectInMod"] != null)
                {
                    Section Section = (Section)Session["ObjectInMod"];
                    Section.Description = txtSection.Text.Trim();
                    result = businessSection.Update(Section);
                    Session["ObjectInMod"] = null;
                    btnAddSection.Text = "Agregar";
                }
            }
            //Para mostrar el toast
            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Sección agregada correctamente.", true);
                txtSection.Text = "";

                BindSections();
            }
            else
            {
                UserControl_Toast.ShowToast(result, false);
            }
            //CurrentOption = Buttons.Section;
        }

        private void BindSections()
        {
            try
            {
                BusinessSection businessSection = new BusinessSection();
                List<Section> sections = businessSection.list();

                dgvSections.DataSource = sections;
                dgvSections.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las Secciones", false);
            }
        }

        protected void btnEditSection_Click(object sender, EventArgs e)
        {
            Section Section = GetSectionFromCommandArgument(sender);

            Session["ObjectInMod"] = Section;

            txtSection.Text = Section.Description;
            btnAddSection.Text = "Editar";
            //CurrentOption = Buttons.Section;
        }
        protected void btnDeleteSection_Click(object sender, EventArgs e)
        {
            Section Section = GetSectionFromCommandArgument(sender);
            BusinessSection businessSection = new BusinessSection();

            string result = businessSection.Delete(Section.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Eliminada correctamente", true);
                //CurrentOption = Buttons.Section;
                BindSections();
            }

        }
        protected void btnActivateSection_Click(object sender, EventArgs e)
        {
            Section Section = GetSectionFromCommandArgument(sender);
            BusinessSection businessSection = new BusinessSection();

            string result = businessSection.Activate(Section.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Activada correctamente", true);
                //CurrentOption = Buttons.Section;
                BindSections();
            }
        }
        private Section GetSectionFromCommandArgument(object sender)
        {
            //Viene el botón de editar, se hace un split para obtener los datos
            LinkButton btn = (LinkButton)sender;
            //Separamos todo el string, con el método split, muy similar a js
            string[] args = btn.CommandArgument.Split('|');
            //Parseamos los datos, porque split me devuelve un array de strings
            int SectionId = int.Parse(args[0]);
            string SectionName = args[1];
            bool SectionActive = bool.Parse(args[2]);

            return new Section { Id = SectionId, Description = SectionName, Active = SectionActive };
        }
        protected void dgvSections_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSections.PageIndex = e.NewPageIndex;
            BindSections();
            //CurrentOption = Buttons.Section;
        }
    }
}