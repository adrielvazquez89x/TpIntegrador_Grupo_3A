using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class BusinessUser
    {
        List<User> list = new List<User>();
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
    }
}
