using eBikes.Core.DTO.Receiving;
using eBikes.Core.Models;
using System.Collections.Generic;

namespace eBikes.Core.Repositories
{
    public interface IReceivingRepository
    {
        void Add_ReceivedOrders(int purchaseOrderId, int poNumber, List<ReceiveOrderDetailDto> newOrders);
        void Update_ClosePO(PurchaseOrder purchaseOrder, List<ReceiveOrderDetailDto> orderDetails);
        ReceiveOrder GetReceiveOrderWithDetails(int id);
    }
}