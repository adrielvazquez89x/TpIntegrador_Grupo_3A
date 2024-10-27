using DataAccessService;
using Microsoft.AspNet.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessUser
    {

        DataAccess data = new DataAccess();

        public void CreateUser(User user)
        {
            try
            {

                data.setQuery("INSERT INTO Usuarios (Email, ContraseniaHash, SeguridadStamp) OUTPUT INSERTED.IdUsuario values (@email, @pass, @security);");
                data.setParameter("@email", user.Email);
                data.setParameter("@pass", user.PasswordHash);
                data.setParameter("security", user.SecurityStamp);

                user.UserId = data.getIdEcalar();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.closeConnection();
            }

        }

        public bool Login(User user)
        {
            DataAccess data = new DataAccess();


            try
            {
  
                // Buscar el usuario por email
                data.setQuery("SELECT * FROM Usuarios WHERE Email = @Email;");
                data.setParameter("@Email", user.Email);
                data.executeRead();

                var reader = data.Reader;

                if (reader.Read())
                {
                    user.UserId = (int)data.Reader["IdUsuario"];
                    user.Admin = (bool)(data.Reader["EsAdmin"]);
                    //if (!(data.Reader["urlImagenPerfil"] is DBNull))
                    //    user.ImagenPerfil = (string)(data.Reader["urlImagenPerfil"]);
                    if (!(data.Reader["Nombre"] is DBNull))
                        user.FirstName = (string)(data.Reader["Nombre"]);
                    if (!(data.Reader["Apellido"] is DBNull))
                        user.LastName = (string)(data.Reader["Apellido"]);

                    var hashedPassword = reader["ContraseniaHash"].ToString();

                    // Verificar la contraseña hasheada
                    PasswordHasher hasher = new PasswordHasher();
                    var verificationResult = hasher.VerifyHashedPassword(hashedPassword, user.PasswordHash);

                    if (verificationResult == PasswordVerificationResult.Success)
                    {


                        // El inicio de sesión fue exitoso
                        return true;
                    }
                }

                // Si no se encuentra el usuario o la contraseña no es correcta
                return false;

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.closeConnection();
            }
        }

        public User GetUserByEmail(string email)
        {
            DataAccess data = new DataAccess();
            User aux = new User();

            try
            {
                data.setQuery("select * from Usuarios where Email = @email;");
                data.setParameter("@email", email);

                SqlDataReader reader = data.Reader;


                if (reader.Read())
                {
                    aux.UserId = (int)reader["Id"];
                    aux.Dni = reader["Dni"].ToString();
                    aux.FirstName = (string)reader["Nombre"];
                    aux.LastName = (string)reader["Apellido"];
                    aux.Email = (string)reader["Email"];
                    aux.ImageUrl = (string)reader["UrlImg"];
                    aux.Mobile = reader["Celular"].ToString();
                    aux.BirthDate = (DateTime)reader["FechaNac"];
                    aux.RegistrationDate = (DateTime)reader["FechaAlta"];
                }
                return aux;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                data.closeConnection();
            }
        }


        public bool emailExists(string email)
        {
            DataAccess data = new DataAccess();
            try
            {
                data.setQuery("SELECT COUNT(*) FROM Usuarios WHERE email = @email");
                data.setParameter("email", email);

                int count = data.ActionScalar();

                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { data.closeConnection(); }

        }
    }
}

//public User userById(int id)
//{
//    DataAccess data = new DataAccess();
//    try
//    {
//        data.setQuery("SELECT U.Id, U.Dni, U.Nombre, U.Apellido, U.Email, U.Contrasenia, U.UrlImg, U.Admin, U.Celular, U.IdDireccion, U.FechaNac, U.FechaAlta, U.Estado, "+
//             "D.Id, D.Provincia, D.Ciudad, D.Barrio, D.Calle, D.Numero, D.CP, D.Piso, D.Unidad "+
//             "FROM Usuarios U JOIN Direcciones D ON U.IdDireccion=D.Id WHERE U.Id=@id");
//        data.setParameter("@id", id);
//        data.executeRead();

//        SqlDataReader reader = data.Reader;
//        User aux = new User();

//        if (reader.Read()) // Si se encuentra un registro
//        {
//           // aux.Id = (int)reader["Id"];
//           // aux.Dni = (int)reader["Dni"];
//           // aux.Name = (string)reader["Nombre"];
//           // aux.Lastname = (string)reader["Apellido"];
//           // aux.Email = (string)reader["Email"];
//           // aux.Passsword = (string)reader["Contrasenia"];
//           // aux.UrlProfileImg= (string)reader["UrlImg"];
//           // aux.Admin = (bool)reader["Admin"];
//           // aux.CellPhone = reader["Celular"] is DBNull ? null : (string)reader["Celular"];
//           // aux.Adress = new Adress();
//           // aux.Adress.Id = (int)reader["IdDireccion"];
//           // aux.Adress.Province = (string)reader["Provincia"];
//           // aux.Adress.Town = (string)reader["Ciudad"];
//           // aux.Adress.District = (string)reader["Barrio"];
//           // aux.Adress.Street = (string)reader["Calle"];
//           // aux.Adress.Number = (int)reader["Numero"];
//           // aux.Adress.CP = (string)reader["Celular"];
//           // aux.Adress.Floor = reader["Piso"] is DBNull ? 0 : (int)reader["Piso"];
//           // aux.Adress.Unit = (string)reader["Unidad"];   //como es la sintaxis si viene con null este campo que toma char?
//           // aux.BirthDate = (DateTime)reader["FechaNac"];
//           // aux.SignInDate = (DateTime)reader["FechaAlta"];
//           //// aux.status = (bool)reader["Estado"];
//        }
//        else
//        {
//            /*aux.Id = -1*/; // User no encontrado
//        }

//        return aux;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        data.closeConnection();
//    }
//}
