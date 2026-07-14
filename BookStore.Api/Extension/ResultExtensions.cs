namespace BookStore.Api.Extension;

public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert to problem of successful result");


        // produce problem details using Results
        IResult ProblemResult = Results.Problem(statusCode: result.MapToStatusCode(),
            extensions: new Dictionary<string, object?>
            {
                {
                    "Errors",new[]{result.Error}
                }
            });

        ProblemDetails? problemDetails =
            ProblemResult.GetType().GetProperty(nameof(ProblemDetails))?.GetValue(ProblemResult) as ProblemDetails;

        return new ObjectResult(problemDetails);
    }
    private static int MapToStatusCode(this Result result)
    {
        return result.Error.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.None => throw new InvalidOperationException("Error.None should never be mapped to a status code."),
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
