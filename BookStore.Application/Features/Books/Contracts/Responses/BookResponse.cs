using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Books.Contracts.Responses;

public record BookResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    int PageCount,
    string Language,
    string PublisherName,
    DateTime PublicationDate,
    string Edition,
    double Weight,
    bool IsAvailable,
    string CoverUrl,
    IEnumerable<string> ImagesUrls
);
