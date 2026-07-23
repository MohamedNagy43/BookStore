using BookStore.Domain.Common;
using BookStore.Domain.Entities.Books;

namespace BookStore.Domain.Entities.Authors;

public class Author : BaseEntity<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Biography { get; set; } = string.Empty;
    public Guid FileId { get; set; }
    public StoredFile File { get; set; } = default!;
    public ICollection<Book> Books { get; set; } = [];
}
