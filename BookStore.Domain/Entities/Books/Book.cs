using BookStore.Domain.Common;
using BookStore.Domain.Entities.Authors;
using BookStore.Domain.Entities.Categories;

namespace BookStore.Domain.Entities.Books;

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
    public bool IsAvailable => StockQuantity > 0;

    public ICollection<BookFile> BookFiles { get; set; } = [];
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = default!;
    public Category Category { get; set; } = default!;
}
