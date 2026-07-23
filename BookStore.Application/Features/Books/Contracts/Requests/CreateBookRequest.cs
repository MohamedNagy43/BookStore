using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Books.Contracts.Requests;

public record CreateBookRequest(
    string Title,
    string Description,
    decimal Price,
    int StockQuantity,
    int PageCount,
    string Language,
    string PublisherName,
    DateTime PublicationDate,
    string Edition,
    double Weight,
    int CategoryId,
    int AuthorId,
    Guid CoverImageId,
    IEnumerable<Guid> GalleryImageIds
);