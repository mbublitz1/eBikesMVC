namespace eBikes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
