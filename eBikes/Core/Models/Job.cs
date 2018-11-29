using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eBikes.Core.Models
{
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            JobDetails = new HashSet<JobDetail>();
            Sales = new HashSet<Sale>();
        }

        public int JobID { get; set; }

        public DateTime JobDateIn { get; set; }

        public DateTime? JobDateStarted { get; set; }

        public DateTime? JobDateDone { get; set; }

        public DateTime? JobDateOut { get; set; }

        public int CustomerID { get; set; }

        public int EmployeeID { get; set; }

        public decimal ShopRate { get; set; }

        [Required]
        [StringLength(1)]
        public string StatusCode { get; set; }

        [Required]
        [StringLength(50)]
        public string VehicleIdentification { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobDetail> JobDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
