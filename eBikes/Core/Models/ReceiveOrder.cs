using System;
using System.Collections.Generic;

namespace eBikes.Core.Models
{
    public partial class ReceiveOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReceiveOrder()
        {
            ReceiveOrderDetails = new HashSet<ReceiveOrderDetail>();
            ReturnedOrderDetails = new HashSet<ReturnedOrderDetail>();
        }

        public int ReceiveOrderID { get; set; }

        public int PurchaseOrderID { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiveOrderDetail> ReceiveOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnedOrderDetail> ReturnedOrderDetails { get; set; }
    }
}
