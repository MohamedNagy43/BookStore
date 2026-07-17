using BookStore.Domain.Common;

namespace BookStore.Domain.Entities;

public class Book : BaseEntity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Edition { get; set; } = string.Empty;
    public double Weight { get; set; }
    public string Dimensions { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
}
