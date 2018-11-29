namespace eBikes.Core.Models
{
    public partial class ReceiveOrderDetail
    {
        public int ReceiveOrderDetailID { get; set; }

        public int ReceiveOrderID { get; set; }

        public int PurchaseOrderDetailID { get; set; }

        public int QuantityReceived { get; set; }

        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }

        public virtual ReceiveOrder ReceiveOrder { get; set; }
    }
}
