namespace p1eXu5.Result.Extensions;

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

using p1eXu5.Result.Exceptions;
using Unit = ValueTuple;

/// <summary>
/// <see cref="Result{TOk, TFailure}"/> extensions.
/// </summary>
public static partial class ResultExtensions
{
    public static Result<TOk, TError> ToOk<TOk, TError>(this TOk successContext)
        => new Result<TOk, TError>.Ok(successContext);

    public static Result<TOk, Unit> ToOk<TOk>(this TOk successContext)
        => new Result<TOk, Unit>.Ok(successContext);

    public static Result<TOk, string> ToOkWithStringError<TOk>(this TOk successContext)
        => new Result<TOk, string>.Ok(successContext);


    public static Result<TOk, TError> ToError<TOk, TError>(this TError failedContext)
        => new Result<TOk, TError>.Error(failedContext);

    public static Result<TOk, string> ToError<TOk>(this string failedContext)
        => new Result<TOk, string>.Error(failedContext);

    public static bool IsOk<TOk, TError>(this Result<TOk, TError> result) => result switch
    {
        Result<TOk, TError>.Ok _ => true,
        _ => false
    };

    public static bool IsError<TOk, TError>(this Result<TOk, TError> result)
        => !result.IsOk();

    public static TOk SuccessContext<TOk, TError>(this Result<TOk, TError> result) => result switch
    {
        Result<TOk, TError>.Ok ok => ok.SuccessContext,
        Result<TOk, TError>.Error _ => throw new ResultContextAccessException("Result does not have success context cause it is failed."),
    };

    public static bool TryGetSuccessContext<TOk, TError>(this Result<TOk, TError> result, out TOk successContext)
    {
        if (result is Result<TOk, TError>.Ok ok)
        {
            successContext = ok.SuccessContext;
            return true;
        }

        successContext = default!;
        return false;
    }

    public static TError FailedContext<TOk, TError>(this Result<TOk, TError> result) => result switch
    {
        Result<TOk, TError>.Ok _ => throw new ResultContextAccessException("Result does not have failed context cause it is success."),
        Result<TOk, TError>.Error err => err.FailedContext,
    };

    public static bool TryGetFailedContext<TOk, TError>(this Result<TOk, TError> result, out TError failedContext)
    {
        if (result is Result<TOk, TError>.Error err)
        {
            failedContext = err.FailedContext;
            return true;
        }

        failedContext = default!;
        return false;
    }
}
