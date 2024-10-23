using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessUser
    {
        List<User> listUsers = new List<User>();
        DataAccess data = new DataAccess();

        public void Add(User user)
        {
            try
            {
                data.setQuery("INSERT INTO Usuarios (Dni, Nombre, Apellido, Email, Contraseña, UrlImg, Admin, Celular," +
                    "IdDireccion, FechaNac, FechaAlta, Estado) OUTPUT INSERTED.Id " +
                    "VALUES (@Dni @Nombre, @Apellido, @Email, @Contraseña, @UrlImg, @Admin, @Celular, @IdDireccion, @FechaNac, @FechaAlta, @Estado);");

                data.setParameter("@Dni", user.Dni);
                data.setParameter("@Nombre", user.Name);
                //ETC, igual esto se va a ir modificando...

                user.Id = data.getIdEcalar();

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


        public User userById(int id)
        {
            DataAccess data = new DataAccess();
            try
            {
                data.setQuery("SELECT U.Id, U.Dni, U.Nombre, U.Apellido, U.Email, U.Contraseña, U.UrlImg, U.Admin, U.Celular, U.IdDireccion, U.FechaNac, U.FechaAlta, U.Estado, "+
                     "D.Id, D.Provincia, D.Ciudad, D.Barrio, D.Calle, D.Numero, D.CP, D.Piso, D.Unidad "+
                     "FROM Usuarios U JOIN Direcciones D ON U.IdDireccion=D.Id WHERE U.Id=@id");
                data.setParameter("@id", id);
                data.executeRead();

                SqlDataReader reader = data.Reader;
                User aux = new User();

                if (reader.Read()) // Si se encuentra un registro
                {
                    aux.Id = (int)reader["Id"];
                    aux.Dni = (int)reader["Dni"];
                    aux.Name = (string)reader["Nombre"];
                    aux.Lastname = (string)reader["Apellido"];
                    aux.Email = (string)reader["Email"];
                    aux.Passsword = (string)reader["Contraseña"];
                    aux.UrlProfileImg= (string)reader["UrlImg"];
                    aux.Admin = (bool)reader["Admin"];
                    aux.CellPhone = reader["Celular"] is DBNull ? null : (string)reader["Celular"];
                    aux.Adress = new Adress();
                    aux.Adress.Id = (int)reader["IdDireccion"];
                    aux.Adress.Province = (string)reader["Provincia"];
                    aux.Adress.Town = (string)reader["Ciudad"];
                    aux.Adress.District = (string)reader["Barrio"];
                    aux.Adress.Street = (string)reader["Calle"];
                    aux.Adress.Number = (int)reader["Numero"];
                    aux.Adress.CP = (string)reader["Celular"];
                    aux.Adress.Floor = reader["Piso"] is DBNull ? 0 : (int)reader["Piso"];
                    aux.Adress.Unit = (string)reader["Unidad"];   //como es la sintaxis si viene con null este campo que toma char?
                    aux.BirthDate = (DateTime)reader["FechaNac"];
                    aux.SignInDate = (DateTime)reader["FechaAlta"];
                   // aux.status = (bool)reader["Estado"];
                }
                else
                {
                    aux.Id = -1; // User no encontrado
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



    }
}
