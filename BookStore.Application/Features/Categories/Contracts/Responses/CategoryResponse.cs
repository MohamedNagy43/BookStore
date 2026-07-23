using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Categories.Contracts.Responses;

public record CategoryResponse(
        int Id,
        string Name,
        string? Description
);