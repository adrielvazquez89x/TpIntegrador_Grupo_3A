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
        List<Adress> adressList = new List<Adress>();
        DataAccess data = new DataAccess();
        public List<Adress> List(string id = "")
        {

            try
            {
                data.setQuery("SELECT Id, Provincia, Ciudad, Barrio, Calle, Numero, CP, Piso, Unidad FROM Direcciones");

                // Si se proporciona un ID, se ajusta la consulta
                //para precargar un agente existente
                if (!string.IsNullOrEmpty(id))
                {
                    data.setQuery("SELECT Id, Provincia, Ciudad, Barrio, Calle, Numero, CP, Piso, Unidad FROM Direcciones WHERE Id=" + id);
                }

                data.executeRead();

                SqlDataReader reader = data.Reader;

                while (reader.Read())
                {
                    Adress aux = new Adress
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

        public void Add(Adress adress)
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

                //ETC, igual esto se va a ir modificando...
                //data.executeAction();
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
    }
}
