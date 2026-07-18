using BookStore.Domain.Entities.Common;

namespace BookStore.Domain.Entities.Book;

public class Book : BaseEntity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int PageCount { get; set; }
    public string Language { get; set; } = string.Empty;
    public string PublisherName { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Edition { get; set; } = string.Empty;
    public double Weight { get; set; }
    public bool IsAvailable { get; set; } = true;
    public ICollection<BookFile> BookFiles { get; set; } = [];
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = default!;
    public Category Category { get; set; } = default!;
}
