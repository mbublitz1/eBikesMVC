namespace eBikes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
