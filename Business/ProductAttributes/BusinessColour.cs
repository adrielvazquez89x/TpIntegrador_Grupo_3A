using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ProductAttributes
{
    public class BusinessColour
    {
        List<Colour> listColour = new List<Colour>();
        DataAccess data = new DataAccess();
        public List<Colour> list(int id=0)
        {
            try
            {
                string query = "SELECT * FROM Colores WHERE Active=1";

                if (id != 0)
                {
                    query += " AND Id = "+ id;
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Colour aux = new Colour
                    {
                        Id = (int)data.Reader["Id"],
                        Description = (string)data.Reader["Descripcion"],
                        Active = (bool)data.Reader["Activo"]
                    };

                    listColour.Add(aux);
                }

                return listColour;
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

        public string Add(Colour colour)
        {
            try
            {
                data.setQuery($"INSERT INTO Colores (Descripcion) Values ({colour.Description})");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Vien de la base de datos
                {
                    return "El color ya existe.";
                }
                else
                {
                    return "Error al agregar el color: " + ex.Message;
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

        public string Update(Colour colour)
        {
            try
            {
                data.setQuery($"UPDATE Colores SET Descripcion = {colour.Description} WHERE Id ={colour.Id}");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "El color ya existe.";
                }
                else
                {
                    return "Error al agregar el color: " + ex.Message;
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

        public string Deactivate(int id)
        {
            try
            {
                data.setQuery($"Update Colores SET Activo = 0 where Id = {id}");
                data.executeAction();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al desactivar el color: " + ex.Message;
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
                data.setQuery($"Update Colores SET Activo = 1 where Id = {id}");
                data.executeAction();
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al activar el color: " + ex.Message;
            }
            finally
            {
                data.closeConnection();
            }
        }
    }
}
