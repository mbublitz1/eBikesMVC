using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eBikes.Core.Models
{
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Jobs = new HashSet<Job>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SaleRefunds = new HashSet<SaleRefund>();
            Sales = new HashSet<Sale>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(9)]
        public string SocialInsuranceNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(40)]
        public string Address { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(2)]
        public string Province { get; set; }

        [StringLength(6)]
        public string PostalCode { get; set; }

        [StringLength(12)]
        public string HomePhone { get; set; }

        [StringLength(30)]
        public string EmailAddress { get; set; }

        public int PositionID { get; set; }

        public virtual Position Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleRefund> SaleRefunds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
