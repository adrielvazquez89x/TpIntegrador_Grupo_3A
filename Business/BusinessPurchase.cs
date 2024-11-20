using DataAccessService.DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessPurchase
    {
        DataAccess data = new DataAccess();
        public void SavePurchase(Purchase purchase)
        {
            try
            {
                // Insertar la compra principal en la tabla COMPRAS
                data.setQuery("INSERT INTO COMPRAS (IdUsuario, NroCompra, FechaCompra, Total) " +
                                  "VALUES (@IdUsuario, @NroCompra, @Fecha, @Total);");

                data.setParameter("@IdUsuario", purchase.UserId);
                data.setParameter("@NroCompra", purchase.PurchaseId); // Aquí asumo que NroCompra es PurchaseId
                data.setParameter("@Fecha", purchase.PurchaseDate);
                data.setParameter("@Total", purchase.Total);


                data.executeRead(); // Ejecuta el comando INSERT

                foreach (var product in purchase.Product)
                {
                    // Insertar cada producto de la compra en la tabla COMPRA_DETALLE
                    data.setQuery("INSERT INTO ComprasXProducts (IdCompra, Codigo) " +
                                      "VALUES (@IdCompra, @codigo)");

                    data.setParameter("@NroCompra", purchase.PurchaseId); // Usando PurchaseId como NroCompra
                    data.setParameter("@codigo", product.Code);


                    data.executeAction(); // Ejecuta el comando INSERT para cada producto
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.closeConnection(); // Asegúrate de cerrar la conexión
            }
        }
    }
}
