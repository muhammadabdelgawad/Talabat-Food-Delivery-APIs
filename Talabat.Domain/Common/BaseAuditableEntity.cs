namespace Talabat.Domain.Common
{
    public interface IBaseAuditableEntity
    {
        string? CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
    public abstract class BaseAuditableEntity<Tkey> : BaseEntity<Tkey> , IBaseAuditableEntity
        where Tkey : IEquatable<Tkey>
    {
        public  string? CreatedBy { get; set; } = null!;
        public DateTime? CreatedOn { get; set; }
        public  string? LastModifiedBy { get; set; } = null!;
        public DateTime? LastModifiedOn { get; set; }
    }
}
