using BookStore.Domain.Entities.Common;

namespace BookStore.Domain.Entities.Book;

public class BookFile
{
    public Guid BookId { get; set; }
    public Guid FileId { get; set; }

    public bool IsPrimary { get; set; } = false;
    public int DisplayOrder { get; set; }

    public Book Book { get; set; } = null!;
    public StoredFile File { get; set; } = null!;
}
