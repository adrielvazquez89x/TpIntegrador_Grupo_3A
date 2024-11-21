using DataAccessService.DataAccessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Business
{
    public class BusisnessPurchase
    {
        public void SavePurchase(Purchase purchase)
        {

            DataAccess data = new DataAccess();

            try
            {

                string queryCompra = "INSERT INTO Compras (IdUsuario, FechaCompra, Total, Estado) OUTPUT INSERTED.Id  " +
                                  "VALUES (@IdUsuario, @FechaCompra, @Total, @Estado);";
                data.setQuery(queryCompra);
                data.setParameter("@IdUsuario", purchase.IdUser);
                data.setParameter("@FechaCompra", purchase.date);
                data.setParameter("@Total", purchase.Total);
                data.setParameter("@Estado", purchase.State);
                int idCompra = data.ActionScalar();

                if (idCompra == 0)
                {
                    throw new Exception("No se pudo generar un ID de compra válido.");
                }

                foreach (var detail in purchase.Details)
                {
                    data.clearParams();
                    string queryDetalle = "INSERT INTO DetallesCompras (IdCompra, CodigoProducto, Cantidad, PrecioUnitario,Subtotal) " +
                                          "VALUES (@IdCompra, @CodigoProducto, @Cantidad, @PrecioUnitario,@Subtotal)";
                    data.setQuery(queryDetalle);
                    data.setParameter("@IdCompra", idCompra);
                    data.setParameter("@CodigoProducto", detail.CodProd);
                    data.setParameter("@Cantidad", detail.Quantity);
                    data.setParameter("@PrecioUnitario", detail.Price);
                    data.setParameter("@Subtotal", detail.Quantity * detail.Price);
                    data.executeAction(); // Ejecutar el insert del detalle
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<Model.Purchase> ListPurchases(string userId = "")
        {
            List<Purchase> purchaseList = new List<Purchase>();
            DataAccess data = new DataAccess();

            try
            {
                string query = "SELECT C.Id, C.FechaCompra, C.Total, C.Estado, C.IdUsuario FROM Compras C ";

                if(!string.IsNullOrEmpty(userId))
                {
                    query += "WHERE C.IdUsuario = @UserId";
                }

                data.setQuery(query);
                data.setParameter("@UserId", userId);

                data.executeRead();

                while (data.Reader.Read())
                {
                    Purchase purchase = new Purchase
                    {
                        Id = (int)data.Reader["Id"],
                        date = data.Reader["FechaCompra"] != DBNull.Value ? (DateTime)data.Reader["FechaCompra"] : default(DateTime),
                        Total = data.Reader["Total"] != DBNull.Value ? (decimal)data.Reader["Total"] : 0,
                        State = data.Reader["Estado"] != DBNull.Value ? (string)data.Reader["Estado"] : string.Empty,
                        IdUser = (int)data.Reader["IdUsuario"]
                    };
                    purchaseList.Add(purchase);
                }

                return purchaseList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar compras: " + ex.Message); 
            }
            finally
            {
                data.closeConnection();
            }
        }

        public List<PurchaseDetail> ListPurchaseDetails(int purchaseId)
        {
            List<PurchaseDetail> purchaseDetails = new List<PurchaseDetail>();
            DataAccess data = new DataAccess();

            try
            {
                
                string query = @"
                                SELECT 
                                    P.Nombre AS ProductoNombre, 
                                    D.CodigoProducto, 
                                    D.Cantidad, 
                                    D.PrecioUnitario, 
                                    D.Subtotal
                                FROM DetallesCompras D
                                INNER JOIN Productos P ON D.CodigoProducto = P.Codigo
                                WHERE D.IdCompra = @PurchaseId";

                data.setQuery(query);
                data.setParameter("@PurchaseId", purchaseId);

                data.executeRead();

                while (data.Reader.Read())
                {
                    
                    PurchaseDetail detail = new PurchaseDetail
                    {
                        ProductName = data.Reader["ProductoNombre"].ToString(),
                        CodProd = data.Reader["CodigoProducto"].ToString(),
                        Quantity = Convert.ToInt32(data.Reader["Cantidad"]),
                        Price = Convert.ToDecimal(data.Reader["PrecioUnitario"]),
                        Subtotal = Convert.ToDecimal(data.Reader["Subtotal"])
                    };

                    
                    purchaseDetails.Add(detail);
                }

                return purchaseDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los detalles de la compra: " + ex.Message);
            }
            finally
            {
                data.closeConnection();
            }
        }

        public void Update(int saleId, string newState)
        {
            DataAccess data = new DataAccess();
            try
            {

                data.setQuery("UPDATE Compras SET  Estado = @State Where Id = @Id ");
                data.setParameter("@State", newState);
                data.setParameter("@Id", saleId);

                data.executeAction();
            }
            catch (Exception ex )
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
