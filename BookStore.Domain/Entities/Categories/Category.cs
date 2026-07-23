using BookStore.Domain.Common;
using BookStore.Domain.Entities.Books;

namespace BookStore.Domain.Entities.Categories;

public class Category : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}
