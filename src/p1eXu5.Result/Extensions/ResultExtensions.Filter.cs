namespace p1eXu5.Result.Extensions;

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

public static partial class ResultExtensions
{
    public static Result<TOkA, TError> Filter<TOkA, TError>(
        this Result<TOkA, TError> result,
        Predicate<TOkA> filter,
        TError defaultError) => result switch
    {
        Result<TOkA, TError>.Ok ok => filter(ok.SuccessContext) ? result : new Result<TOkA, TError>.Error(defaultError),
        Result<TOkA, TError>.Error err => err
    };

    public static Result<TOkA, TError> FilterError<TOkA, TError>(
        this Result<TOkA, TError> result,
        Predicate<TError> filter,
        TOkA defaultOk) => result switch
    {
        Result<TOkA, TError>.Error err => filter(err.FailedContext) ? result : new Result<TOkA, TError>.Ok(defaultOk),
        Result<TOkA, TError>.Ok ok => ok
    };
}
