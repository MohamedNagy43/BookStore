using BookStore.Domain.Entities.Common;

namespace BookStore.Domain.Entities.Book;

public class Category : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}
