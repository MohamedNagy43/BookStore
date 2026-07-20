using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Abstractions.Files.Contracts;

public record MultipleFileRequest(
    IEnumerable<FileRequest> Files
);