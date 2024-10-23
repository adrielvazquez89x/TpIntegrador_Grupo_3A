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
    public partial class PersonalData : System.Web.UI.Page
    {
        public User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessUser business = new BusinessUser();
            //User aux = (User)Session["Usuario"]; //El usuario ya debe estar guardado en sesion, lo que dejo abajo es momentaneo
            //esto luego no va ya que lo traerá en sesion:
            User aux = new User();
            aux = business.userById(1); // de momento solo para mostrar que la BD está

            UploadData(aux); //validar si hay cliente en sesion...
        }

        protected void UploadData(User user)
        {
            txtNombre.Text = user.Name;
            txtApellido.Text = user.Lastname;
            txtCelular.Text = user.Dni.ToString();
            txtEmail.Text = user.Email;
            txtDNI.Text = user.Dni.ToString();
            txtProvincia.Text = user.Adress.Province;
            txtLocalidad.Text = user.Adress.Town;
            TxtBarrio.Text= user.Adress.District;
            txtCalle.Text= user.Adress.Floor.ToString();
            txtCP.Text = user.Adress.CP;
            txtAltura.Text = user.Adress.Number.ToString();

            //En realidad este grado de detalle deberia ir para las propiedades, no para los clientes... (revisar que incluso faltan algunos atributos en ese caso)
        }
    }
}