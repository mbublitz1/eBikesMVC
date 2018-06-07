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

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "The description can only be 100 characters or less")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vendor Part Number is required")]
        [StringLength(50, ErrorMessage = "The part number can only be 50 characters or less")]
        [Display(Name = "Vendor Part Number")]
        public string VendorPartNumber { get; set; }

        public int Quantity { get; set; }
    }
}
