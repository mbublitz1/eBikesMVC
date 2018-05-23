namespace eBikes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnorderedPurchaseItemCart")]
    public partial class UnorderedPurchaseItemCart
    {
        [Key]
        public int CartID { get; set; }
        [Required]
        public int PurchaseOrderNumber { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Vendor Part Number")]
        public string VendorPartNumber { get; set; }

        public int Quantity { get; set; }
    }
}
