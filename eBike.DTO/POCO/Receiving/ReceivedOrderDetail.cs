using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eBike.DTO.POCO
{
    public class ReceivedOrderDetail
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int PartId { get; set; }
        public string PartDescription { get; set; }
        public int QtyOnOrder { get; set; }
        public int QtyOutstanding { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Received { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Returning { get; set; }
        public string Reason { get; set; }
    }
}
