using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Authors.Contracts.Responses;

public sealed record AuthorResponse(
        int Id,
        string FirstName,
        string LastName,
        string? Biography,
        string? ImageUrl
);
