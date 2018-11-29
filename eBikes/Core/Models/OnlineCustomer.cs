using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBikes.Core.Models
{
    [Table("OnlineCustomer")]
    public partial class OnlineCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineCustomer()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int OnlineCustomerID { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public Guid? TrackingCookie { get; set; }

        public DateTime CreatedOn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
