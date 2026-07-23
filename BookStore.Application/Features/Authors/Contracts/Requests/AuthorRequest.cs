using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Authors.Contracts.Requests;

public record AuthorRequest(
        string FirstName,
        string LastName,
        string Biography,
        Guid ImageId
);

