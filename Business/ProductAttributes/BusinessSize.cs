using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessService.DataAccessService;

namespace Business.ProductAttributes
{
    public class BusinessSize
    {
        List<Size> listSize = new List<Size>();
        DataAccess data = new DataAccess();
        public List<Size> list()
        {
            try
            {
                data.setQuery("SELECT * FROM Talles");
                data.executeRead();

                while (data.Reader.Read())
                {
                    Size aux = new Size
                    {
                        Id = (int)data.Reader["Id"],
                        Description = (string)data.Reader["Descripcion"],
                        Active = (bool)data.Reader["Activo"]
                    };

                    listSize.Add(aux);
                }

                return listSize;
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

        public string Add(Size size)
        {
            try
            {
                data.setQuery($"INSERT INTO Talles (Descripcion) Values ('{size.Description}')");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "El talle ya existe.";
                }
                else
                {
                    return "Error al agregar el talle: " + ex.Message;
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

        public string Update(Size size)
        {
            try
            {
                data.setQuery($"UPDATE Talles SET Descripcion = '{size.Description}' WHERE Id = {size.Id}");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "El talle ya existe.";
                }
                else
                {
                    return "Error al agregar el talle: " + ex.Message;
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

        public string Delete(int id)
        {
            try
            {
                data.setQuery("Update Talles SET Activo = 0 where Id = "+ id);
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
                data.setQuery("Update Talles SET Activo = 1 where Id = "+ id);
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
