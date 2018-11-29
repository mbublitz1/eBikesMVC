using System;

namespace eBikes.Core.DTO.Receiving
{
    public class ReceivingOrderList
    {
        public int PurchaseOrderId { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
    }
}
