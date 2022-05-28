namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    /// <summary>
    /// TaskResult applicative functor.
    /// </summary>
    /// <typeparam name="TSuccessA"></typeparam>
    /// <typeparam name="TSuccessB"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="task"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static async Task<Result<TSuccessB, __>> Apply<TSuccessA, TSuccessB, __>(
        this Task<Result<TSuccessA, __>> task,
        Task<Result<Func<TSuccessA, TSuccessB>, __>> f)
    {
        Result<Func<TSuccessA, TSuccessB>, __> fResult;
        Result<TSuccessA, __> aResult;

        fResult = await f;
        aResult = await task;

        var isFResultSuccess = fResult.TryGetSucceededContext(out var fc);
        var isAResultSuccess = aResult.TryGetSucceededContext(out var ac);

        if (isFResultSuccess && isAResultSuccess)
        {
            return Result<TSuccessB, __>.Success(fc.Invoke(ac));
        }

        if (isFResultSuccess)
        {
            return Result<TSuccessB, __>.Failure(aResult.FailedContext);
        }

        if (isAResultSuccess)
        {
            return Result<TSuccessB, __>.Failure(fResult.FailedContext);
        }

        return Result<TSuccessB, __>.Failure(fResult.FailedContext);
    }
}
