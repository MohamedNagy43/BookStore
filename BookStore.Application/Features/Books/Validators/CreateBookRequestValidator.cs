using BookStore.Application.Features.Books.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Books.Validators;

public class CreateBookRequestValidator:AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.StockQuantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageCount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Language)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.PublisherName)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.PublicationDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.Edition)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Weight)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.AuthorId)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.CoverImageId)
            .NotEmpty();

        RuleFor(x => x.GalleryImageIds)
            .NotNull();

        RuleForEach(x => x.GalleryImageIds)
            .NotEmpty();

        RuleFor(x => x)
            .Must(x => x.GalleryImageIds.Distinct().Count() == x.GalleryImageIds.Count())
            .When(x=>x.GalleryImageIds is not null)
            .WithMessage("Duplicate image ids are not allowed.");

        RuleFor(x => x)
            .Must(x => !x.GalleryImageIds.Contains(x.CoverImageId))
            .When(x=>x.GalleryImageIds is not null)
            .WithMessage("Cover image cannot be included in image ids.");
    }
}
