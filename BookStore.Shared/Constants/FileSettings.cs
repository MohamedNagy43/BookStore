using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Shared.Constants;

public static class FileSettings
{
    public const int MaxFileSizeInMB = 5;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    public static readonly string[] FileSigntureBlackList = ["4D-5A", "2F-2A", "D0-CF"];
    public static readonly string[] ImageAllowedExtensions = [".png", ".jpg", ".jpeg"];
    public const string FileUploadPath = "uploads";
}
