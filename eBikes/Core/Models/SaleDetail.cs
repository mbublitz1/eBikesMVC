using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBikes.Core.Models
{
    public partial class SaleDetail
    {
        public int SaleDetailID { get; set; }

        public int SaleID { get; set; }

        public int PartID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }

        public bool Backordered { get; set; }

        public DateTime? ShippedDate { get; set; }

        public virtual Part Part { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
