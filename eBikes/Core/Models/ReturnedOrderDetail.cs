using System.ComponentModel.DataAnnotations;

namespace eBikes.Core.Models
{
    public partial class ReturnedOrderDetail
    {
        public int ReturnedOrderDetailID { get; set; }

        public int ReceiveOrderID { get; set; }

        public int? PurchaseOrderDetailID { get; set; }

        [StringLength(50)]
        public string ItemDescription { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string Reason { get; set; }

        [StringLength(50)]
        public string VendorPartNumber { get; set; }

        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }

        public virtual ReceiveOrder ReceiveOrder { get; set; }
    }
}
