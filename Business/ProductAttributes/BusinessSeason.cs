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
    public class BusinessSeason
    {
        List<Season> listSeason = new List<Season>();
        DataAccess data = new DataAccess();
        public List<Season> list(int id)
        {
            try
            {
                string query = "SELECT * FROM Temporadas WHERE Activo=1";

                if (id != 0)
                {
                    query += " AND Id = "+ id;
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Season aux = new Season
                    {
                        Id = (int)data.Reader["Id"],
                        Description = (string)data.Reader["Descripcion"],
                        Active = (bool)data.Reader["Activo"]
                    };

                    listSeason.Add(aux);
                }

                return listSeason;
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
        public string Add(Season season)
        {
            try
            {
                data.setQuery($"INSERT INTO Temporadas (Descripcion) Values ('{season.Description}')");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Viene de la base de datos
                {
                    return "La temporada ya existe.";
                }
                else
                {
                    return "Error al agregar la temporada: " + ex.Message;
                }

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

        public void Update(Season season)
        {
            try
            {
                data.setQuery($"UPDATE Temporadas SET Descripcion = '{season.Description}' WHERE Id = {season.Id}");
                data.executeAction();
            }
            catch (SqlException ex)
            {
                throw ex;
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
                data.setQuery("Update Temporadas SET Activo = 0 where Id = "+ id);
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

        public string Activate(int id)
        {
            try
            {
                data.setQuery("Update Temporadas SET Activo = 1 where Id = "+ id);
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
