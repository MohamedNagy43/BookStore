using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Categories.Contracts.Requests;

public record CategoryRequest(
        string Name,
        string? Description
);
