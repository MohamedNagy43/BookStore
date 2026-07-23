namespace BookStore.Application.Common.Result;

public record Error(string Code, string Description, ErrorType ErrorType)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    public static Error NotFoundEntity<TEntity>() => new($"{typeof(TEntity).Name}.NotFound",
        $"No {typeof(TEntity).Name} found with this key", ErrorType.Validation);

    public static Error DeletedEntity<TEntity>() => new($"{typeof(TEntity).Name}.IsDeleted",
        $"{typeof(TEntity).Name} entity has already been deleted", ErrorType.Validation);

    public static Error ActiveEntity<TEntity>() => new($"{typeof(TEntity).Name}.IsActive",
        $"{typeof(TEntity).Name} entity is already active", ErrorType.Validation);
}
