namespace eBikes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SaleRefund
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleRefund()
        {
            SaleRefundDetails = new HashSet<SaleRefundDetail>();
        }

        public int SaleRefundID { get; set; }

        public DateTime SaleRefundDate { get; set; }

        public int SaleID { get; set; }

        public int EmployeeID { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleRefundDetail> SaleRefundDetails { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
