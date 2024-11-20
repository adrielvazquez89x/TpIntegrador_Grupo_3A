using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Model;
using Security;

namespace TpIntegrador_Grupo_3A
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (SessionSecurity.ActiveSession(Session["user"]))
                    {

                        Model.User user = (Model.User)Session["user"];
             
                        if(user.firstAccess)
                        {
                            BusinessUser businessUser = new BusinessUser();
                            businessUser.FirstAccessDone(user.UserId);
                        }

                        txtDni.Text = user.Dni ?? "";  
                        txtNombre.Text = user.FirstName ?? "";  
                        txtApellido.Text = user.LastName ?? "";  
                        txtEmail.Text = user.Email ?? ""; 
                        txtCel.Text = user.Mobile ?? "";  
                        if (user.BirthDate.HasValue)
                        {
                            txtNacimiento.Text = user.BirthDate.Value.ToString("yyyy-MM-dd"); // Formato año-mes-día
                        }
                        else
                        {
                            txtNacimiento.Text = ""; // O un valor por defecto si es nula
                        }
                        //if (user.ImageUrl != null)
                        //{
                        //    imgNuevoPerfil.ImageUrl = "~/Images/" + user.ImageUrl;
                        //}
                        //else
                        //{
                        //    imgNuevoPerfil.ImageUrl = "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"; // Imagen por defecto
                        //}


                        if (user.AddressId > 0)
                        {
                            // Obtener la dirección del usuario
                            BusinessAdress businessAdress = new BusinessAdress();
                            Adress adress = businessAdress.GetAddressById(user.AddressId); 

                            if (adress != null)
                            {
                                txtProvincia.Text = adress.Province;
                                txtCiudad.Text = adress.Town;
                                txtBarrio.Text = adress.District;
                                txtCalle.Text = adress.Street;
                                txtNumero.Text = adress.Number.ToString(); // Convertimos el número a string
                                txtCP.Text = adress.CP;
                                txtPiso.Text = adress.Floor;
                                txtDpto.Text = adress.Unit;
                            }
                        }
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
        protected void cvBirthDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Intentamos obtener la fecha desde el control txtNacimiento
            if (DateTime.TryParse(txtNacimiento.Text, out DateTime birthDate))
            {
                // Verificamos si la fecha es mayor a la fecha actual
                if (birthDate > DateTime.Now)
                {
                    // La fecha es inválida
                    args.IsValid = false;
                }
                else
                {
                    // La fecha es válida
                    args.IsValid = true;
                }
            }
            else
            {
                // Si la fecha no es válida (por ejemplo, si el campo está vacío o mal formateado)
                args.IsValid = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    BusinessUser businessUser = new BusinessUser();
                    Model.User user = (Model.User)Session["user"];
                    user.FirstName = txtNombre.Text;
                    user.LastName = txtApellido.Text;
                    user.Mobile = txtCel.Text;
                    user.Dni = txtDni.Text;
                    user.BirthDate = Convert.ToDateTime(txtNacimiento.Text);

                    if (txtImagen.PostedFile != null && txtImagen.PostedFile.ContentLength > 0)
                    {
                        // Verifica si es JPG
                        string fileExtension = System.IO.Path.GetExtension(txtImagen.PostedFile.FileName).ToLower();
                        if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                        {
                            string ruta = Server.MapPath("~/Images/");
                            string imgName = "perfil-" + user.LastName + user.FirstName + user.UserId + ".jpg";
                            txtImagen.PostedFile.SaveAs(ruta + imgName);
                            user.ImageUrl = imgName;

                            // Actualiza la URL de la imagen mostrada en la página
                            //imgNuevoPerfil.ImageUrl = "~/Images/" + imgName;
                        }
                        else
                        {
                            // Manejo de error en caso de que el archivo no sea JPG
                            Response.Write("<script>alert('Por favor sube una imagen en formato JPG.');</script>");
                            return;
                        }
                    }

                    // Actualiza la base de datos
                    // businessUser.Update(user);

                    // Actualiza el avatar en el MasterPage si existe
                    Image imgAvatar = (Image)Master.FindControl("imgAvatar");
                    if (imgAvatar != null && user.ImageUrl != null)
                    {
                        imgAvatar.ImageUrl = "~/Images/" + user.ImageUrl;
                    }

                    BusinessAdress businessAdress = new BusinessAdress();
                    Adress adress = new Adress();

                    adress.Province = txtProvincia.Text;
                    adress.Town = txtCiudad.Text;
                    adress.District = txtBarrio.Text;
                    adress.Street = txtCalle.Text;
                    adress.Number = string.IsNullOrEmpty(txtNumero.Text) ? 0 : Convert.ToInt32(txtNumero.Text);
                    adress.CP = txtCP.Text;
                    adress.Floor =  txtPiso.Text;
                    adress.Unit = txtDpto.Text;

                    if ( user.AddressId > 0)
                    {
                        adress.Id = user.AddressId; // Si ya tiene dirección, asignamos el Id de la dirección
                        businessAdress.Update(adress); // Llamamos a un método Update que debería actualizar la tabla de direcciones
                    }
                    else
                    {
                        // Si el usuario no tiene dirección, hacemos un INSERT
                        businessAdress.Add(adress);// Este método debe insertar la nueva dirección en la base de datos
                        Console.WriteLine("Dirección insertada con Id: " + adress.Id);
                        user.AddressId = adress.Id; // Asignamos el nuevo Id de la dirección al usuario
                    }



                    //businessAdress.Add(adress);
                    //user.AddressId = adress.Id;

                    businessUser.Update(user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                // Redirige
                Response.Redirect("Default.aspx", false);
            }
        }

    }
}