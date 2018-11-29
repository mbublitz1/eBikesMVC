namespace eBikes.Core.DTO
{
    public class PurchaseOrderDetailDto
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int PartId { get; set; }
        public string PartDescription { get; set; }
        public int QtyOnOrder { get; set; }
        public int QtyOutstanding { get; set; }
        public int Received { get; set; }
        public int Returning { get; set; }
        public string Reason { get; set; }
    }
}
