using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Abstractions.Files.Contracts;

public class MultipleFileRequestValidator : AbstractValidator<MultipleFileRequest>
{
    public MultipleFileRequestValidator()
    {
        RuleForEach(x => x.Files)
            .SetValidator(new FileRequestValidator());

        RuleFor(x => x.Files)
            .NotEmpty();
    }
}
