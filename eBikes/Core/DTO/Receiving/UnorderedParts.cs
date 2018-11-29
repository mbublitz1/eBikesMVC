using System;
using System.ComponentModel.DataAnnotations;

namespace eBikes.Core.DTO.Receiving
{
    public class UnorderedParts
    {
        public int PurchaseOrderNumber { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a part number")]
        public string VendorPartNo { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity should not contain characters")]
        [Required(ErrorMessage = "Please enter a quantity")]
        public int Quantity { get; set; }
    }
}
