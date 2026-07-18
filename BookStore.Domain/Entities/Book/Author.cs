using BookStore.Domain.Entities.Common;

namespace BookStore.Domain.Entities.Book;

public class Author : BaseEntity<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Biography { get; set; } = string.Empty;
    public Guid FileId { get; set; }
    public StoredFile File { get; set; } = default!;
    public ICollection<Book> Books { get; set; } = [];
}
