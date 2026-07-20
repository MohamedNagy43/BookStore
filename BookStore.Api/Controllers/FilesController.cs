using BookStore.Application.Abstractions.Files;
using BookStore.Application.Abstractions.Files.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FilesController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromServices] IValidator<FileRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new FileRequest(file.OpenReadStream(), file.FileName, file.ContentType);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var modelDictionary = new ModelStateDictionary();
            validationResult.Errors.ForEach(error => modelDictionary.AddModelError(error.PropertyName, error.ErrorMessage));
            return ValidationProblem(modelDictionary);
        }

        var Id = await _fileService.UploadAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Download), new { Id }, null);
    }

    [HttpPost("upload-many")]
    public async Task<IActionResult> UploadMany([FromForm] IFormFileCollection files,
        [FromServices] IValidator<MultipleFileRequest> validator, CancellationToken cancellationToken)
    {
        var fileRequests = files.Select(x => new FileRequest(x.OpenReadStream(), x.FileName, x.ContentType));
        var multipleFileRequest = new MultipleFileRequest(fileRequests);

        var validationResult = await validator.ValidateAsync(multipleFileRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            var modelDictionary = new ModelStateDictionary();
            validationResult.Errors.ForEach(error => modelDictionary.AddModelError(error.PropertyName, error.ErrorMessage));
            return ValidationProblem(modelDictionary);
        }
        var Ids = await _fileService.UploadManyAsync(multipleFileRequest, cancellationToken);

        return Ok(Ids);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var (fileContent, fileName, contentType) = await _fileService.DownloadAsync(id, cancellationToken);

        return fileContent is [] ? NotFound() : File(fileContent, contentType, fileName);
    }

    [HttpGet("stream/{id}")]
    public async Task<IActionResult> Stream([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var (stream, fileName, contentType) = await _fileService.StreamAsync(id, cancellationToken);

        return stream is null ? NotFound() : File(stream, contentType, fileName, true);
    }
}

