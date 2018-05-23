namespace eBikes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JobDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobDetail()
        {
            JobDetailParts = new HashSet<JobDetailPart>();
        }

        public int JobDetailID { get; set; }

        public int JobID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public decimal JobHours { get; set; }

        public string Comments { get; set; }

        public int? CouponID { get; set; }

        public bool? Completed { get; set; }

        public virtual Coupon Coupon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobDetailPart> JobDetailParts { get; set; }

        public virtual Job Job { get; set; }
    }
}
