using eBikes.Core.DTO.Receiving;
using eBikes.Core.Models;
using System.Collections.Generic;

namespace eBikes.Core.Repositories
{
    public interface IPurchaseOrderRepository
    {
        List<PurchaseOrder> GetOpenOrdersList();
        PurchaseOrder GetPurchaseOrderWithDetails(int id);
        List<ReceivingOrderList> Get_OutstandingOrders();
    }
}