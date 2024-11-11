using DataAccessService;
using DataAccessService.DataAccessService;
using Microsoft.AspNet.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
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

                user.UserId = data.ActionScalar();


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

        public void CreateAdmin(User user)
        {

            try
            {

                var passwordHasher = new PasswordHasher();
                string hashedPassword = passwordHasher.HashPassword(user.PasswordHash); // hasheo la contraseña


                string securityStamp = Guid.NewGuid().ToString();
                user.SecurityStamp = securityStamp;

                data.setQuery(@"
            INSERT INTO Usuarios 
                (Dni, Nombre, Apellido, Email, Celular, FechaAlta, ContraseniaHash, SeguridadStamp, EsAdmin, EsOwner, Active)
            VALUES 
                (@dni, @nombre, @apellido, @email, @celular, @fechaAlta, @pass, @stamp, @admin, @owner, @active);");


                data.setParameter("@dni", user.Dni);
                data.setParameter("@nombre", user.FirstName);
                data.setParameter("@apellido", user.LastName);
                data.setParameter("@email", user.Email);
                data.setParameter("@celular", user.Mobile);
                data.setParameter("@fechaAlta", user.RegistrationDate);
                data.setParameter("@pass", user.PasswordHash); // Contraseña ya hasheada
                data.setParameter("@stamp", user.SecurityStamp); // Security Stamp
                data.setParameter("@admin", user.Admin); // Admin: true
                data.setParameter("@owner", user.Owner); // Owner: false
                data.setParameter("@active", user.Active); // Active: true

                data.executeAction();
                //user.UserId = data.ActionScalar();
            }
            catch (Exception ex)
            {

                throw ex;
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
                    if (!(data.Reader["UrlImg"] is DBNull))
                        user.ImageUrl = (string)(data.Reader["UrlImg"]);

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
            catch (Exception ex)
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
                data.executeRead();

                var reader = data.Reader;


                if (reader.Read())
                {
                    aux.UserId = (int)reader["IdUsuario"];
                    // aux.Dni = (string)reader["Dni"];
                    // aux.FirstName = (string)reader["Nombre"];
                    //aux.LastName = (string)reader["Apellido"];
                    aux.Email = (string)reader["Email"];
                    // aux.ImageUrl = (string)reader["UrlImg"];
                    // aux.Mobile = (string)reader["Celular"];
                    // aux.BirthDate = (DateTime)reader["FechaNac"];
                    //aux.RegistrationDate = (DateTime)reader["FechaAlta"];
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



        public void Update(User user)
        {
            DataAccess data = new DataAccess();
            try
            {
                string query = @"
            UPDATE Usuarios 
            SET 
                Dni = @Dni,
                Nombre = @Nombre,
                Apellido = @Apellido,
                Email = @Email,
                Celular = @Celular,
                FechaAlta = @FechaAlta
                
               
            WHERE IdUsuario = @IdUsuario";

                // Asignar los parámetros a la consulta
                data.setQuery(query);
                data.setParameter("@Dni", user.Dni);
                data.setParameter("@Nombre", user.FirstName);
                data.setParameter("@Apellido", user.LastName);
                data.setParameter("@Email", user.Email);
                data.setParameter("@Celular", user.Mobile);
                data.setParameter("@FechaAlta", user.RegistrationDate);

                data.setParameter("@IdUsuario", user.UserId);

                // Ejecutar la acción
                data.executeAction();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                data.closeConnection();
            }
        }

        public string GenerateToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }


        public void StoreResetToken(string email, string token)
        {

            data.setQuery("UPDATE Usuarios SET ResetearToken = @token, TokenVencimiento = @expiration WHERE Email = @email;");
            data.setParameter("@token", token);
            data.setParameter("@expiration", DateTime.Now.AddHours(1)); // Expira en 1 hora
            data.setParameter("@email", email);
            data.executeAction();
        }



        public bool VerifyResetToken(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                return false; // Retorna false si el email o token están vacíos
            }

            try
            {
                data.setQuery("SELECT ResetearToken, TokenVencimiento FROM Usuarios WHERE Email = @email;");
                data.setParameter("@email", email);
                data.executeRead();

                if (data.Reader.Read())
                {
                    Console.WriteLine("El token es válido.");
                    var expiration = (DateTime)data.Reader["TokenVencimiento"];
                    //Console.WriteLine($"Email: {email}, Token: {token}, Expiration: {expiration}");

                    //return expiration > DateTime.Now; // Verifica si el token no ha expirado
                    return expiration > DateTime.Now;
                }
                else
                {
                    Console.WriteLine("No se encontraron resultados para el email y token proporcionados.");
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones adecuadamente
                Console.WriteLine($"Error al verificar el token: {ex.Message}");
            }

            return false; // Retorna false si no se encontró el token o si ha expirado
        }


        public void ResetPassword(string email, string newPassword)
        {

            try
            {
                var hashedPassword = new PasswordHasher().HashPassword(newPassword);

                using (var data = new DataAccess())
                {
                    data.setQuery("UPDATE Usuarios SET ContraseniaHash = @pass, ResetearToken = NULL, TokenVencimiento = NULL WHERE Email = @email;");
                    data.setParameter("@pass", hashedPassword);
                    data.setParameter("@email", email);

                    data.executeAction();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<User> ListUsers(string id = "")
        {
            DataAccess data = new DataAccess();
            List<User> userList = new List<User>();

            try
            {
                string query = "SELECT U.IdUsuario, U.Dni, U.Nombre, U.Apellido, U.Email, U.Celular, U.FechaAlta, U.Active, U.ContraseniaHash FROM Usuarios U";

                // Si se pasa un id, agrega la condición WHERE
                if (!string.IsNullOrEmpty(id))
                {
                    query += " WHERE U.IdUsuario = @UserId";
                }

                // Establecer la consulta
                data.setQuery(query);

                // Si el id se pasa, añade el parámetro para evitar SQL Injection
                if (!string.IsNullOrEmpty(id))
                {
                    data.setParameter("@UserId", id); // Usamos setParameter aquí para pasar el valor
                }

                data.executeRead();

                while (data.Reader.Read())
                {
                    User user = new User
                    {
                        UserId = (int)data.Reader["IdUsuario"],
                        Dni = data.Reader["Dni"] != DBNull.Value ? (string)data.Reader["Dni"] : string.Empty,
                        FirstName = data.Reader["Nombre"] != DBNull.Value ? (string)data.Reader["Nombre"] : string.Empty,
                        LastName = data.Reader["Apellido"] != DBNull.Value ? (string)data.Reader["Apellido"] : string.Empty,
                        Email = data.Reader["Email"] != DBNull.Value ? (string)data.Reader["Email"] : string.Empty,
                        Mobile = data.Reader["Celular"] != DBNull.Value ? (string)data.Reader["Celular"] : string.Empty,
                        RegistrationDate = (DateTime)data.Reader["FechaAlta"],
                        Active = (bool)data.Reader["Active"],
                        PasswordHash = data.Reader["ContraseniaHash"] != DBNull.Value ? (string)data.Reader["ContraseniaHash"] : string.Empty
                    };

                    userList.Add(user);
                }

                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message); // Mejor manejo de excepciones
            }
            finally
            {
                data.closeConnection();
            }
        }


        public string Activate(int id)
        {
            try
            {
                data.setQuery($"Update Usuarios SET Active = 1 where IdUsuario = {id}");
                data.executeAction();
                return "ok";
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


        public string Delete(int id)
        {
            try
            {
                data.setQuery($"Update Usuarios SET Active = 0 where IdUsuario = {id}");
                data.executeAction();
                return "ok";
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
