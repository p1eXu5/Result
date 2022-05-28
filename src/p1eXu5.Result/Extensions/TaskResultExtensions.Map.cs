namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    public static async Task<Result<TSuccessB, __>> Map<TSuccessA, TSuccessB, __>(
            this Task<Result<TSuccessA, __>> task,
            Func<TSuccessA, TSuccessB> f)
    {
        var aResult = await task;

        return
            aResult.Map(f);
    }


    public static async Task<Result<TS11>> MapFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
        this Task<Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>> result, Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, Result<TS11>> f)
    {
        var res = await result;
        return res.MapFlat(f);
    }


    public static async Task<Result<_, TErrorB>> MapError<_, TErrorA, TErrorB>(this Task<Result<_, TErrorA>> task, Func<TErrorA, TErrorB> f)
    {
        var result = await task;
        return result.MapError(f);
    }


    public static async Task<Result<TSuccessB, TErrorB>> Bimap<TSuccessA, TSuccessB, TErrorA, TErrorB>(
        this Task<Result<TSuccessA, TErrorA>> task, Func<TSuccessA, TSuccessB> fs, Func<TErrorA, TErrorB> fe)
    {
        var result = await task;
        return result.Bimap(fs, fe);
    }

    public static async Task<Result<TSuccessB>> MapResult<TSuccessA, TSuccessB>(this Task<Result<TSuccessA>> taskResult, Func<TSuccessA, Result<TSuccessB>> f)
    {
        var result = await taskResult;

        if (result.TryGetSucceededContext(out var sc))
        {
            return f(sc);
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }

    public static async Task<Result<TSuccessB>> MapResult<TSuccessA, TSuccessB>(this ValueTask<Result<TSuccessA>> taskResult, Func<TSuccessA, Result<TSuccessB>> f)
    {
        var result = await taskResult;

        if (result.TryGetSucceededContext(out var sc))
        {
            return f(sc);
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }
}

