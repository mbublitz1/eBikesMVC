using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBikes.Core.Models
{
    public partial class Part
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Part()
        {
            JobDetailParts = new HashSet<JobDetailPart>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            SaleDetails = new HashSet<SaleDetail>();
            SaleRefundDetails = new HashSet<SaleRefundDetail>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            StandardJobParts = new HashSet<StandardJobPart>();
        }

        public int PartID { get; set; }

        [Required]
        [StringLength(40)]
        public string Description { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal SellingPrice { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReorderLevel { get; set; }

        public int QuantityOnOrder { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(1)]
        public string Refundable { get; set; }

        public bool Discontinued { get; set; }

        public int VendorID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobDetailPart> JobDetailParts { get; set; }

        public virtual Vendor Vendor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleRefundDetail> SaleRefundDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StandardJobPart> StandardJobParts { get; set; }
    }
}
