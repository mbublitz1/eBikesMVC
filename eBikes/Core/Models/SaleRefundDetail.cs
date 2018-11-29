using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBikes.Core.Models
{
    public partial class SaleRefundDetail
    {
        public int SaleRefundDetailID { get; set; }

        public int SaleRefundID { get; set; }

        public int PartID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }

        [StringLength(150)]
        public string Reason { get; set; }

        public virtual Part Part { get; set; }

        public virtual SaleRefund SaleRefund { get; set; }
    }
}
