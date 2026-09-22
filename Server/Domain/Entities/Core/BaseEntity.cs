namespace Server.Domain.Entities.Core
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Guid? RecordId { get; set; }
        public string? FormCode { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
