using Server.Domain.Entities.Core;

namespace Server.Application.DTOs
{
    public class CollectionsDTO: BaseEntity
    {
        public Guid ClientId{ get; set; }
        public string ClientName{ get; set; }
        public DateTime CollectionDate { get; set; }
        public string CollectionTime { get; set; }
        public string CollectionShift{ get; set; }
        public string MilkType { get; set; }
        public Decimal Quantity { get; set; }
        public Guid UOM {  get; set; }	
        public string Unit {  get; set; }
        public Decimal FAT { get; set; }	
        public Decimal SNF { get; set; }	
        public Decimal Temperature { get; set; }	
        public Decimal Rate { get; set; }	
        public Decimal TotalAmount { get; set; }	
        public string PaymentStatus { get; set; }	
        public string PaymentMode { get; set; }	
        public string PaymentReference { get; set; }	
        public Guid CollectionCenter {  get; set; }
        public string CenterName { get; set; }
        public string CollectionCenterName { get; set; }
        public string Description { get; set; }	
    }
}
