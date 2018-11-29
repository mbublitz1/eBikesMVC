using System.ComponentModel.DataAnnotations.Schema;

namespace eBikes.Core.Models
{
    public partial class JobDetailPart
    {
        public int JobDetailPartID { get; set; }

        public int JobDetailID { get; set; }

        public int PartID { get; set; }

        public short Quantity { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal SellingPrice { get; set; }

        public virtual JobDetail JobDetail { get; set; }

        public virtual Part Part { get; set; }
    }
}
