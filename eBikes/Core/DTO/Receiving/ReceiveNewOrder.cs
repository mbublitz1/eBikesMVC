namespace eBikes.Core.DTO.Receiving
{
    public class ReceiveNewOrder
    {
        public int PurchaseOrderDetailId { get; set; }
        public int PartId { get; set; }
        public string PartDescription { get; set; }
        public int QtyReceived { get; set; }
        public int QtyReturned { get; set; }
        public int Outstanding { get; set; }
        public string Notes { get; set; }
    }
}
