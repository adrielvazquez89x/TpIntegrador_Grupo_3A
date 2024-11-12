using DataAccessService;
using Model.ProductAttributes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using DataAccessService.DataAccessService;

namespace Business.ProductAttributes
{
    public class BusinessStock
    {
        DataAccess data = new DataAccess();
        public Stock list(int id)
        {
            try
            {
                data.setQuery("SELECT * FROM Stock WHERE Id = " + id);
                data.executeRead();

                Stock aux = new Stock();
                while (data.Reader.Read())
                {
                    aux.Id = (int)data.Reader["Id"];
                    aux.ProdCode = (string)data.Reader["CodigoProducto"];
                    aux.IdColour = (int)data.Reader["IdColor"];
                    aux.IdSize = (int)data.Reader["IdTalle"];
                    aux.Amount = (int)data.Reader["Stock"];
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

        public Stock getStock(string prodCode, int idColor, int idSize)
        {
            try
            {
                data.setQuery($"SELECT * FROM Stock WHERE CodigoProducto ='{prodCode}' AND IdColor={idColor} AND IdTalle={idSize}");
                data.executeRead();

                Stock aux = new Stock();
                aux.Amount = 0;
                while (data.Reader.Read())
                {
                    aux.Id = (int)data.Reader["Id"];
                    aux.ProdCode = (string)data.Reader["CodigoProducto"];
                    aux.IdColour = (int)data.Reader["IdColor"];
                    aux.IdSize = (int)data.Reader["IdTalle"];
                    aux.Amount = (int)data.Reader["Stock"];
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
        public string Add(Stock stock)
        {
            try
            {
                data.setQuery($"INSERT INTO Stock (CodigoProducto, IdColor, IdTalle, Stock) Values ('{stock.ProdCode}', {stock.IdColour}, {stock.IdSize}, {stock.Amount})");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "Ya existe registro para este producto con el color y talles indicados.";
                }
                else
                {
                    return "Error al agregar el stock: " + ex.Message;
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

        public string Update(Stock stock)
        {
            try
            {
                data.setQuery($"UPDATE Stock SET CodigoProducto = '{stock.ProdCode}', IdColor={stock.IdColour}, IdTalle={stock.IdSize}, Stock={stock.Amount} WHERE Id = {stock.Id}");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                return "Error al actualizar el stock. Info de error: " + ex.Message;
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
