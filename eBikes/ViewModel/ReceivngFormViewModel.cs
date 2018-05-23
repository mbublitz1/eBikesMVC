using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using eBike.DTO.POCO;
using eBike.DTO.POCO.Receiving;
using eBikes.Models;

namespace eBikes.ViewModel
{
    public class ReceivngFormViewModel
    {
        [Display(Name = "PO: ")]
        public int? PO { get; set; }
        [Display(Name = "Vendor: ")]
        public string Vendor { get; set; }
        [Display(Name = "Contact: ")]
        public string Contact { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        public UnorderedParts Parts { get; set; }

        public UnorderedPurchaseItemCart UnorderedPart { get; set; }
        public PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public List<ReceivedOrderDetail> ReceivedOrderDetails { get; set; }
        public IEnumerable<PurchaseOrder> OutstandingOrders { get; set; }
        public IEnumerable<UnorderedPurchaseItemCart> UnorderedParts { get; set; }

    }
}