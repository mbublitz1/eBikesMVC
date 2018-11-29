namespace eBikes.Core.Models
{
    public partial class StandardJobPart
    {
        public int StandardJobPartID { get; set; }

        public int StandardJobID { get; set; }

        public int PartID { get; set; }

        public int Quantity { get; set; }

        public virtual Part Part { get; set; }

        public virtual StandardJob StandardJob { get; set; }
    }
}
