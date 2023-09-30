namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    public static async Task<Result<TOkB, TError>> Map<TOkA, TOkB, TError>(
            this Task<Result<TOkA, TError>> task,
            Func<TOkA, TOkB> f)
    {
        var aResult = await task;

        return
            aResult.Map(f);
    }


    public static async Task<Result<TOkResult, TError>> MapFlat<TOkA, TOkB, TOkResult, TError>(
        this Task<Result<(TOkA, TOkB), TError>> result, Func<TOkA, TOkB, Result<TOkResult, TError>> f)
    {
        Result<(TOkA, TOkB), TError> res = await result;
        return res.FlatMap(f);
    }

    public static async Task<Result<TOkResult, TError>> MapFlat<TOkA, TOkB, TOkC, TOkResult, TError>(
        this Task<Result<(TOkA, TOkB, TOkC), TError>> result, Func<TOkA, TOkB, TOkC, Result<TOkResult, TError>> f)
    {
        Result<(TOkA, TOkB, TOkC), TError> res = await result;
        return res.FlatMap(f);
    }

    public static async Task<Result<TOkResult, TError>> MapFlat<TOkA, TOkB, TOkC, TOkD, TOkResult, TError>(
        this Task<Result<(TOkA, TOkB, TOkC, TOkD), TError>> result, Func<TOkA, TOkB, TOkC, TOkD, Result<TOkResult, TError>> f)
    {
        Result<(TOkA, TOkB, TOkC, TOkD), TError> res = await result;
        return res.FlatMap(f);
    }

    public static async Task<Result<TOk, TErrorB>> MapError<TOk, TErrorA, TErrorB>(this Task<Result<TOk, TErrorA>> task, Func<TErrorA, TErrorB> f)
    {
        var result = await task;
        return result.MapError(f);
    }


    public static async Task<Result<TOkB, TErrorB>> Bimap<TOkA, TOkB, TErrorA, TErrorB>(
        this Task<Result<TOkA, TErrorA>> task, Func<TOkA, TOkB> fs, Func<TErrorA, TErrorB> fe)
    {
        var result = await task;
        return result.Bimap(fs, fe);
    }

    public static async Task<Result<TOkB, TError>> MapResult<TOkA, TOkB, TError>(this Task<Result<TOkA, TError>> taskResult, Func<TOkA, Result<TOkB, TError>> f)
    {
        var result = await taskResult;

        if (result.TryGetSuccessContext(out var sc))
        {
            return f(sc);
        }

        return new Result<TOkB, TError>.Error(result.FailedContext());
    }

    public static async Task<Result<TOkB, TError>> MapResult<TOkA, TOkB, TError>(this ValueTask<Result<TOkA, TError>> taskResult, Func<TOkA, Result<TOkB, TError>> f)
    {
        var result = await taskResult;

        if (result.TryGetSuccessContext(out var sc))
        {
            return f(sc);
        }

        return new Result<TOkB, TError>.Error(result.FailedContext());
    }
}

