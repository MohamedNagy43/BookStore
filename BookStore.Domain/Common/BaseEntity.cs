namespace BookStore.Domain.Common;

public abstract class BaseEntity<TKey> : AuditableEntity where TKey : IEquatable<TKey> 
{
    public TKey Id { get; set; } = default!;
}
