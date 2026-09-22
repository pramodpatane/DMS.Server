using Server.Domain.Entities.Core;

namespace Server.Domain.Entities.Feature
{
    public class MilkCollectionEntity: BaseEntity
    {
        public Guid ClientId { get; set; }

        public DateTime CollectionDate { get; set; }

        public string CollectionTime { get; set; }

        public string CollectionShift { get; set; }

        public string MilkType { get; set; }

        public decimal Quantity { get; set; }

        public Guid UOM { get; set; }

        public decimal FAT { get; set; }

        public decimal SNF { get; set; }

        public decimal? Temperature { get; set; }

        public decimal? Rate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentStatus { get; set; }

        public string? PaymentMode { get; set; }

        public string? PaymentReference { get; set; }

        public Guid? CollectionCenter { get; set; }

        public string? Description { get; set; }

        
    }
}
