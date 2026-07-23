using BookStore.Domain.Common;

namespace BookStore.Domain.Entities.Books;

public class BookFile
{
    public Guid BookId { get; set; }
    public Guid FileId { get; set; }

    public bool IsPrimary { get; set; } = false;
    public int DisplayOrder { get; set; }

    public Book Book { get; set; } = null!;
    public StoredFile File { get; set; } = null!;
}
