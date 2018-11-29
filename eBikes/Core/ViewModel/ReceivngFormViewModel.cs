using eBikes.Core.DTO.Receiving;
using eBikes.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eBikes.Core.DTO;

namespace eBikes.Core.ViewModel
{
    public class ReceivngFormViewModel
    {
        [Display(Name = "PO: ")]
        public int? PO { get; set; }
        [Display(Name = "Vendor: ")]
        public string Vendor { get; set; }
        [Display(Name = "Contact: ")]
        public string Contact { get; set; }

        public string Message { get; set; }

        public string CloserReason { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        public UnorderedParts Parts { get; set; }

        public UnorderedPurchaseItemCart UnorderedPart { get; set; }
        public PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public List<PurchaseOrderDetailDto> PurchaseOrderDetails { get; set; }
        public List<ReceiveOrderDetailDto> ReceivedOrderDetails { get; set; }
        public IEnumerable<PurchaseOrder> OutstandingOrders { get; set; }
        public IEnumerable<UnorderedPurchaseItemCart> UnorderedParts { get; set; }

    }
}