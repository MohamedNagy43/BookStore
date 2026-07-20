using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Abstractions.Files.Contracts;

public record FileRequest(Stream FileStream, string FileName, string ContentType);
