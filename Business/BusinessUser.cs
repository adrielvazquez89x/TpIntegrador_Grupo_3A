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

                data.setQuery("INSERT INTO Usuarios (Email, ContraseniaHash, SeguridadStamp, FechaAlta) OUTPUT INSERTED.IdUsuario values (@email, @pass, @security, GETDATE());");
                data.setParameter("@email", user.Email);
                data.setParameter("@pass", user.PasswordHash);
                data.setParameter("@security", user.SecurityStamp); 
               

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
                (Dni, Nombre, Apellido, Email, Celular, FechaAlta,FechaNac, ContraseniaHash, SeguridadStamp, EsAdmin, EsOwner, Active)
            VALUES 
                (@dni, @nombre, @apellido, @email, @celular, GETDATE(),@fechanac, @pass, @stamp, @admin, @owner, @active);");


                data.setParameter("@dni", user.Dni);
                data.setParameter("@nombre", user.FirstName);
                data.setParameter("@apellido", user.LastName);
                data.setParameter("@email", user.Email);
                data.setParameter("@celular", user.Mobile);
               // data.setParameter("@fechaAlta", user.RegistrationDate);
                data.setParameter("fechaNac", user.BirthDate);
                data.setParameter("@pass", user.PasswordHash); 
                data.setParameter("@stamp", user.SecurityStamp); 
                data.setParameter("@admin", user.Admin); 
                data.setParameter("@owner", user.Owner); 
                data.setParameter("@active", user.Active); 

                data.executeAction();
                
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

             
                data.setQuery("SELECT * FROM Usuarios WHERE Email = @Email;");
                data.setParameter("@Email", user.Email);
                data.executeRead();

                var reader = data.Reader;

                if (reader.Read())
                {
                    user.UserId = (int)data.Reader["IdUsuario"];
                    user.firstAccess = (bool)data.Reader["PrimerAcceso"];
                    user.Admin = (bool)(data.Reader["EsAdmin"]);
                    user.Owner = (bool)(data.Reader["EsOwner"]);
                    user.Active = (bool)(data.Reader["Active"]);
                   
                    if (!(data.Reader["Nombre"] is DBNull))
                        user.FirstName = (string)(data.Reader["Nombre"]);
                    if (!(data.Reader["Apellido"] is DBNull))
                        user.LastName = (string)(data.Reader["Apellido"]);
                    if (!(data.Reader["UrlImg"] is DBNull))
                        user.ImageUrl = (string)(data.Reader["UrlImg"]);

                    var hashedPassword = reader["ContraseniaHash"].ToString();

                    if (user.Owner)
                    {
                       
                        return true;
                    }
                    else
                    {

                        // Verificar la contraseña hasheada
                        PasswordHasher hasher = new PasswordHasher();
                        var verificationResult = hasher.VerifyHashedPassword(hashedPassword, user.PasswordHash);

                        if (verificationResult == PasswordVerificationResult.Success)
                        {
              
                            return true;
                        }
                    }
                }

               
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
                  
                    aux.Email = (string)reader["Email"];
             
                    aux.AddressId = reader["IdDireccion"] != DBNull.Value ? (int)reader["IdDireccion"] : 0;

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

        public User GetUserById(int userId)
        {
            DataAccess data = new DataAccess();
            User user = new User();
            BusinessAdress businessAdress = new BusinessAdress();

            try
            {
                // Consulta para obtener los detalles del usuario por ID
                data.setQuery("SELECT * FROM Usuarios U WHERE IdUsuario = @IdUsuario;");
                data.setParameter("@IdUsuario", userId);
                data.executeRead();

                var reader = data.Reader;

                if (reader.Read())
                {

                    user.UserId = (int)data.Reader["IdUsuario"];
                    user.firstAccess = (bool)data.Reader["PrimerAcceso"];
                    user.Dni = data.Reader["Dni"] != DBNull.Value ? (string)data.Reader["Dni"] : string.Empty;
                    user.FirstName = data.Reader["Nombre"] != DBNull.Value ? (string)data.Reader["Nombre"] : string.Empty;
                    user.LastName = data.Reader["Apellido"] != DBNull.Value ? (string)data.Reader["Apellido"] : string.Empty;
                    user.Email = data.Reader["Email"] != DBNull.Value ? (string)data.Reader["Email"] : string.Empty;
                    user.Mobile = data.Reader["Celular"] != DBNull.Value ? (string)data.Reader["Celular"] : string.Empty;
                    user.RegistrationDate = data.Reader["FechaAlta"] != DBNull.Value ? (DateTime)data.Reader["FechaAlta"] : default(DateTime);
                    user.BirthDate = data.Reader["FechaNac"] != DBNull.Value ? (DateTime?)data.Reader["FechaNac"] : null;
                    user.Active = (bool)data.Reader["Active"];
                    user.Admin = (bool)data.Reader["EsAdmin"];
                    user.Owner = (bool)data.Reader["EsOwner"];
                    user.ImageUrl = data.Reader["UrlImg"] != DBNull.Value ? (string)data.Reader["UrlImg"] : string.Empty;


                   

                    user.AddressId = data.Reader["IdDireccion"] != DBNull.Value ? (int)data.Reader["IdDireccion"] : 0;
              
                }

                if (user.AddressId > 0)
                {
                    user.Address = businessAdress.GetAddressById(user.AddressId);
                }

                    return user;
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
                FechaNac = @FechaNacimiento,
                IdDireccion = @IdDireccion
            WHERE IdUsuario = @IdUsuario";

                
                data.setQuery(query);
                data.setParameter("@Dni", user.Dni);
                data.setParameter("@Nombre", user.FirstName);
                data.setParameter("@Apellido", user.LastName);
                data.setParameter("@Email", user.Email);
                data.setParameter("@Celular", user.Mobile);
                data.setParameter("@FechaNacimiento", user.BirthDate);
                data.setParameter("@IdDireccion", user.AddressId);
                data.setParameter("@IdUsuario", user.UserId);

                
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


        public void UpdateUserForm(User user)
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
            FechaNac = @FechaNacimiento
        WHERE IdUsuario = @IdUsuario";

                data.setQuery(query);
                data.setParameter("@Dni", user.Dni);
                data.setParameter("@Nombre", user.FirstName);
                data.setParameter("@Apellido", user.LastName);
                data.setParameter("@Email", user.Email);
                data.setParameter("@Celular", user.Mobile);
                data.setParameter("@FechaNacimiento", user.BirthDate);
                data.setParameter("@IdUsuario", user.UserId);

                data.executeAction();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la información del usuario: " + ex.Message);
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
                return false; 
            }

            try
            {
                data.setQuery("SELECT ResetearToken, TokenVencimiento FROM Usuarios WHERE Email = @email;");
                data.setParameter("@email", email);
                data.executeRead();

                if (data.Reader.Read())
                {
                    
                    var expiration = (DateTime)data.Reader["TokenVencimiento"];
                    
                    return expiration > DateTime.Now;
                }
                else
                {
                    Console.WriteLine("No se encontraron resultados para el email y token proporcionados.");
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return false;
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
                string query = "SELECT U.IdUsuario, U.Dni, U.Nombre, U.Apellido, U.Email, U.Celular, U.FechaAlta,U.FechaNac, U.Active, U.ContraseniaHash FROM Usuarios U WHERE U.EsAdmin = 1";

                
                if (!string.IsNullOrEmpty(id))
                {
                    query += " AND U.IdUsuario = @UserId";
                }

               
                data.setQuery(query);

               
                if (!string.IsNullOrEmpty(id))
                {
                    data.setParameter("@UserId", id); 
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
                        RegistrationDate = data.Reader["FechaAlta"] != DBNull.Value ? (DateTime)data.Reader["FechaAlta"] : default(DateTime),
                        BirthDate = data.Reader["FechaNac"] != DBNull.Value ? (DateTime?)data.Reader["FechaNac"] : null,
                        Active = (bool)data.Reader["Active"],
                        PasswordHash = data.Reader["ContraseniaHash"] != DBNull.Value ? (string)data.Reader["ContraseniaHash"] : string.Empty
                    };

                    userList.Add(user);
                }

                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message); 
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

        public void FirstAccessDone(int id)
        {
            try
            {
                data.setQuery($"Update Usuarios SET PrimerAcceso = 0 where IdUsuario = {id}");
                data.executeAction();
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
