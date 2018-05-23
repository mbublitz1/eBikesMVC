using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.DTO.POCO
{
    public class ReceivingOrderList
    {
        public int PurchaseOrderId { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
    }
}
