using DataAccessService;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessService.DataAccessService;
using System.Threading;
using System.Data;

namespace Business
{
    public class BusinessAdress
    {
        List<Address> adressList = new List<Address>();
        DataAccess data = new DataAccess();
        public List<Address> List(string id = "")
        {

            try
            {
                data.setQuery("SELECT Id, Provincia, Ciudad, Barrio, Calle, Numero, CP, Piso, Unidad FROM Direcciones");

             
                if (!string.IsNullOrEmpty(id))
                {
                    data.setQuery("SELECT Id, Provincia, Ciudad, Barrio, Calle, Numero, CP, Piso, Unidad FROM Direcciones WHERE Id=" + id);
                }

                data.executeRead();

                SqlDataReader reader = data.Reader;

                while (reader.Read())
                {
                    Address aux = new Address
                    {
                        Id = (int)reader["Id"],
                        Province = (string)reader["Provincia"],
                        Town = (string)reader["Ciudad"],
                        District = (string)reader["Barrio"],
                        Street = (string)reader["Calle"],
                        Number = (int)reader["Numero"],
                        CP = (string)reader["CP"],
                        Floor = (string)reader["Piso"],
                        Unit = (string)reader["Unidad"]
                    };

                    adressList.Add(aux);
                }
                return adressList;
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

        public void Add(Address adress)
        {
            try
            {
                data.setQuery("INSERT INTO Direcciones (Provincia,Ciudad,Barrio,Calle,Numero,CP,Piso,Unidad) OUTPUT INSERTED.Id VALUES (@Provincia, @Ciudad, @Barrio, @Calle, @Numero, @CP, @Piso, @Unidad)");

                data.setParameter("@Provincia", adress.Province);
                data.setParameter("@Ciudad", adress.Town);
                data.setParameter("@Barrio", adress.District);
                data.setParameter("@Calle", adress.Street);
                data.setParameter("@Numero", adress.Number);
                data.setParameter("CP", adress.CP);
                data.setParameter("Piso", adress.Floor);
                data.setParameter("Unidad", adress.Unit);
                data.setParameter("Activo", 1);

                
                adress.Id = data.ActionScalar();

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


        public void Update(Address adress)
        {
            DataAccess data = new DataAccess();
            try
            {
                string query = @"
            UPDATE Direcciones 
           SET Provincia = @provincia,
                        Ciudad = @ciudad,
                        Barrio = @barrio,
                        Calle = @calle,
                        Numero = @numero,
                        CP = @CP,
                        Piso = @piso,
                        Unidad = @unidad
                    WHERE Id = @Id";

              
                data.setQuery(query);
                data.setParameter("@provincia", adress.Province);
                data.setParameter("@ciudad",adress.Town);
                data.setParameter("@barrio", adress.District);
                data.setParameter("@calle", adress.Street);
                data.setParameter("@numero", adress.Number);
                data.setParameter("@CP", adress.CP);
                data.setParameter("@piso", adress.Floor);
                data.setParameter("@unidad", adress.Unit);


                data.setParameter("@Id", adress.Id);

                
                data.executeAction();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el domicilio: " + ex.Message);
            }
            finally
            {
                data.closeConnection();
            }
        }

        public Address GetAddressById(int addressId)
        {
            Address adress = null;
            DataAccess data = new DataAccess();
            

            try
            {
                data.setQuery("SELECT * FROM Direcciones WHERE Id = @Id");
                data.setParameter("@Id", addressId);
                data.executeRead();

                SqlDataReader reader = data.Reader;

                while (reader.Read())
                {
                    adress = new Address
                    {
                        Id = (int)reader["Id"],
                        Province = (string)reader["Provincia"],
                        Town = (string)reader["Ciudad"],
                        District = (string)reader["Barrio"],
                        Street = (string)reader["Calle"],
                        Number = (int)reader["Numero"],
                        CP = (string)reader["CP"],
                        Floor = (string)reader["Piso"],
                        Unit = (string)reader["Unidad"]
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return adress;
        }
    }
}
