namespace eBikes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        public int ShoppingCartItemID { get; set; }

        public int ShoppingCartID { get; set; }

        public int PartID { get; set; }

        public int Quantity { get; set; }

        public virtual Part Part { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
