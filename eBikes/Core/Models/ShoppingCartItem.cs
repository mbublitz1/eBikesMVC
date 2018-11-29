using System.ComponentModel.DataAnnotations.Schema;

namespace eBikes.Core.Models
{
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
