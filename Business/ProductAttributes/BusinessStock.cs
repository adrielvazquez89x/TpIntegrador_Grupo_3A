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
        public List<Stock> list(string code)
        {
            List<Stock> stocks = new List<Stock>();
            try
            {
                data.setQuery($"SELECT S.Id AS Id, S.CodigoProducto, S.IdColor, S.IdTalle, S.Stock, " +
                    $"CO.Descripcion AS Color, T.Descripcion AS Talle FROM Stock S " +
                    $"INNER JOIN Colores Co ON CO.Id=S.IdColor INNER JOIN Talles T ON T.Id=S.IdTalle WHERE S.CodigoProducto = '{code}' ");

                data.executeRead();

                while (data.Reader.Read())
                {
                    Stock aux = new Stock();
                    aux.Id = (int)data.Reader["Id"];
                    aux.ProdCode = (string)data.Reader["CodigoProducto"];
                    aux.Colour = new Colour
                    {
                        Id = data.Reader["IdColor"] != DBNull.Value ? (int)data.Reader["IdColor"] : 0,
                        Description = data.Reader["Color"] != DBNull.Value ? (string)data.Reader["Color"] : string.Empty
                    };
                    aux.Size = new Size
                    {
                        Id = data.Reader["IdTalle"] != DBNull.Value ? (int)data.Reader["IdTalle"] : 0,
                        Description = data.Reader["Talle"] != DBNull.Value ? (string)data.Reader["Talle"] : string.Empty
                    };
                    aux.Amount = (int)data.Reader["Stock"];

                    stocks.Add(aux);
                }

                return stocks;
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
                data.setQuery($"SELECT S.Id AS Id, S.CodigoProducto, S.IdColor, S.IdTalle, S.Stock, " +
                    $"CO.Descripcion AS Color, T.Descripcion AS Talle FROM Stock S " +
                    $"INNER JOIN Colores Co ON CO.Id=S.IdColor INNER JOIN Talles T ON T.Id=S.IdTalle" +
                    $" WHERE CodigoProducto ='{prodCode}' AND IdColor={idColor} AND IdTalle={idSize}");
                data.executeRead();

                Stock aux = new Stock();
                aux.Amount = 0;
                while (data.Reader.Read())
                {
                    aux.Id = (int)data.Reader["Id"];
                    aux.ProdCode = (string)data.Reader["CodigoProducto"];
                    aux.Colour = new Colour
                    {
                        Id = data.Reader["IdColor"] != DBNull.Value ? (int)data.Reader["IdColor"] : 0,
                        Description = data.Reader["Color"] != DBNull.Value ? (string)data.Reader["Color"] : string.Empty
                    };
                    aux.Size = new Size
                    {
                        Id = data.Reader["IdTalle"] != DBNull.Value ? (int)data.Reader["IdTalle"] : 0,
                        Description = data.Reader["Talle"] != DBNull.Value ? (string)data.Reader["Talle"] : string.Empty
                    };
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
                data.setQuery($"INSERT INTO Stock (CodigoProducto, IdColor, IdTalle, Stock) Values ('{stock.ProdCode}', {stock.Colour.Id}, {stock.Size.Id}, {stock.Amount})");
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
                data.setQuery($"UPDATE Stock SET CodigoProducto = '{stock.ProdCode}', IdColor={stock.Colour.Id}, IdTalle={stock.Size.Id}, Stock={stock.Amount} WHERE Id = {stock.Id}");
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
