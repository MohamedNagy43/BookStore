using BookStore.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Abstractions.Files.Contracts;

public class FileRequestValidator : AbstractValidator<FileRequest>
{
    public FileRequestValidator()
    {
        RuleFor(x => x.FileStream)
            .NotEmpty()
            .Must(x => x.Length <= FileSettings.MaxFileSizeInBytes)
            .When(x => x.FileStream is not null)
            .WithMessage($"File Size Must Not Exceed {FileSettings.MaxFileSizeInMB}MB");

        RuleFor(x => x.FileName)
            .Matches("^[A-Za-z0-9_\\-.]*$")
            .When(x => x is not null)
            .WithMessage("Invalid file name");


        RuleFor(x => x.FileName)
            .Must((request, context) =>
            {
                BinaryReader binary = new(request.FileStream);
                var bytes = binary.ReadBytes(2);

                var fileSequenceHex = BitConverter.ToString(bytes);

                foreach (var signature in FileSettings.FileSigntureBlackList)
                    if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                        return false;

                return true;
            })
            .When(x => x is not null)
            .WithMessage("File content Not allowed");

        RuleFor(x => x.FileName)
            .Must(x =>
            {
                var extension = Path.GetExtension(x.ToLower());
                return FileSettings.ImageAllowedExtensions.Contains(extension);
            })
            .When(x => x.FileStream is not null)
            .WithMessage("Not allowd extension");

    }
}
