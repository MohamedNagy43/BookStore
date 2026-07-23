using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Abstractions.Files;

public interface IFileUrlProvider
{
    string GetFileFullUrl(string relativePath);
}
