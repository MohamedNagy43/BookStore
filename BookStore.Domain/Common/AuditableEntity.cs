namespace BookStore.Domain.Common;

public abstract class AuditableEntity
{
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string? UpdatedById { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }
    public string? DeletedById { get; set; }
    public DateTime? DeletedOn { get; set; }
}
