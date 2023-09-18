#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    // ==================
    //        Map
    // ==================

    /// <summary>
    /// <see cref="Result{TOk,TFailure}"/> functor.
    /// </summary>
    public static Result<TOkB, TError> Map<TOkA, TOkB, TError>(this Result<TOkA, TError> result, Func<TOkA, TOkB> f) => result switch
    {
        Result<TOkA, TError>.Ok ok => new Result<TOkB, TError>.Ok(f(ok.SuccessContext)),
        Result<TOkA, TError>.Error err => new Result<TOkB, TError>.Error(err.FailedContext)
    };

    public static Result<TOkResult, TError> Map<TOkA, TOkB, TOkResult, TError>(
        this Result<(TOkA, TOkB), TError> result,
        Func<TOkA, TOkB, TOkResult> f) => result switch
    {
        Result<(TOkA, TOkB), TError>.Ok ok => new Result<TOkResult, TError>.Ok(f(ok.SuccessContext.Item1, ok.SuccessContext.Item2)),
        Result<(TOkA, TOkB), TError>.Error err => new Result<TOkResult, TError>.Error(err.FailedContext)
    };

    public static Result<TOkResult, TError> Map<TOkA, TOkB, TOkC, TOkResult, TError>(
        this Result<(TOkA, TOkB, TOkC), TError> result,
        Func<TOkA, TOkB, TOkC, TOkResult> f) => result switch
    {
        Result<(TOkA, TOkB, TOkC), TError>.Ok ok => new Result<TOkResult, TError>.Ok(f(ok.SuccessContext.Item1, ok.SuccessContext.Item2, ok.SuccessContext.Item3)),
        Result<(TOkA, TOkB, TOkC), TError>.Error err => new Result<TOkResult, TError>.Error(err.FailedContext)
    };

    public static Result<TOkResult, TError> Map<TOkA, TOkB, TOkC, TOkD, TOkResult, TError>(
        this Result<(TOkA, TOkB, TOkC, TOkD), TError> result,
        Func<TOkA, TOkB, TOkC, TOkD, TOkResult> f) => result switch
    {
        Result<(TOkA, TOkB, TOkC, TOkD), TError>.Ok ok =>
            new Result<TOkResult, TError>.Ok(
                f(ok.SuccessContext.Item1, ok.SuccessContext.Item2, ok.SuccessContext.Item3, ok.SuccessContext.Item4)),
        Result<(TOkA, TOkB, TOkC, TOkD), TError>.Error err => new Result<TOkResult, TError>.Error(err.FailedContext)
    };


    // ==================
    //      MapFlat
    // ==================

    public static Result<TOkResult, TError> FlatMap<TOkA, TOkB, TOkResult, TError>(
        this Result<(TOkA, TOkB), TError> result,
        Func<TOkA, TOkB, Result<TOkResult, TError>> f) => result switch
    {
        Result<(TOkA, TOkB), TError>.Ok ok => f(ok.SuccessContext.Item1, ok.SuccessContext.Item2),
        Result<(TOkA, TOkB), TError>.Error err => new Result<TOkResult, TError>.Error(err.FailedContext)
    };

    public static Result<TOkResult, TError> FlatMap<TOkA, TOkB, TOkC, TOkResult, TError>(
        this Result<(TOkA, TOkB, TOkC), TError> result,
        Func<TOkA, TOkB, TOkC, Result<TOkResult, TError>> f) => result switch
    {
        Result<(TOkA, TOkB, TOkC), TError>.Ok ok => f(ok.SuccessContext.Item1, ok.SuccessContext.Item2, ok.SuccessContext.Item3),
        Result<(TOkA, TOkB, TOkC), TError>.Error err => new Result<TOkResult, TError>.Error(err.FailedContext)
    };

    public static Result<TOkResult, TError> FlatMap<TOkA, TOkB, TOkC, TOkD, TOkResult, TError>(
        this Result<(TOkA, TOkB, TOkC, TOkD), TError> result,
        Func<TOkA, TOkB, TOkC, TOkD, Result<TOkResult, TError>> f) => result switch
    {
        Result<(TOkA, TOkB, TOkC, TOkD), TError>.Ok ok =>
            f(ok.SuccessContext.Item1, ok.SuccessContext.Item2, ok.SuccessContext.Item3, ok.SuccessContext.Item4),
        Result<(TOkA, TOkB, TOkC, TOkD), TError>.Error err => new Result<TOkResult, TError>.Error(err.FailedContext)
    };

    // ==================
    //      MapError
    // ==================

    /// <summary>
    /// <see cref="Result{TOk,TFailure}"/> error functor.
    /// </summary>
    /// <typeparam name="_"></typeparam>
    /// <typeparam name="TErrorA"></typeparam>
    /// <typeparam name="TErrorB"></typeparam>
    /// <param name="result"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Result<TOk, TErrorB> MapError<TOk, TErrorA, TErrorB>(
        this Result<TOk, TErrorA> result,
        Func<TErrorA, TErrorB> f) => result switch
    {
        Result<TOk, TErrorA>.Ok ok => new Result<TOk, TErrorB>.Ok(ok.SuccessContext),
        Result<TOk, TErrorA>.Error err => new Result<TOk, TErrorB>.Error(f(err.FailedContext))
    };

    public static Result<TOk, TError> MapErrorToSuccess<TOk, TError>(
        this Result<TOk, TError> result,
        Func<TError, TOk> f) => result switch
    {
        Result<TOk, TError>.Ok ok => ok,
        Result<TOk, TError>.Error err => new Result<TOk, TError>.Ok(f(err.FailedContext))
    };

    // ==================
    //       Bimap
    // ==================

    public static Result<TOkB, TErrorB> Bimap<TOkA, TOkB, TErrorA, TErrorB>(
        this Result<TOkA, TErrorA> result, 
        Func<TOkA, TOkB> fOk, 
        Func<TErrorA, TErrorB> fErr) => result switch
    {
        Result<TOkA, TErrorA>.Ok ok => new Result<TOkB, TErrorB>.Ok(fOk(ok.SuccessContext)),
        Result<TOkA, TErrorA>.Error err => new Result<TOkB, TErrorB>.Error(fErr(err.FailedContext))
    };

    // ==================
    //      MapTask
    // ==================

    public static Task<Result<TOkB, TError>> MapTask<TOkA, TOkB, TError>(
        this Result<TOkA, TError> result,
        Func<TOkA, Task<Result<TOkB, TError>>> f) => result switch
    {
        Result<TOkA, TError>.Ok ok => f(ok.SuccessContext),
        Result<TOkA, TError>.Error err => Task.FromResult<Result<TOkB, TError>>(new Result<TOkB, TError>.Error(err.FailedContext))
    };
}


