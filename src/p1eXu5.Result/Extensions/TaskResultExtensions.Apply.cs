namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    /// <summary>
    /// TaskResult applicative functor.
    /// </summary>
    /// <typeparam name="TOkA"></typeparam>
    /// <typeparam name="TOkB"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="task"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static async Task<Result<TOkB, __>> Apply<TOkA, TOkB, __>(
        this Task<Result<TOkA, __>> task,
        Task<Result<Func<TOkA, TOkB>, __>> f)
    {
        Result<Func<TOkA, TOkB>, __> fResult;
        Result<TOkA, __> aResult;

        fResult = await f;
        aResult = await task;

        var isFResultSuccess = fResult.TryGetSuccessContext(out var fc);
        var isAResultSuccess = aResult.TryGetSuccessContext(out var ac);

        if (isFResultSuccess && isAResultSuccess)
        {
            return new Result<TOkB, __>.Ok(fc.Invoke(ac));
        }

        if (isFResultSuccess)
        {
            return new Result<TOkB, __>.Error(aResult.FailedContext());
        }

        if (isAResultSuccess)
        {
            return new Result<TOkB, __>.Error(fResult.FailedContext());
        }

        return new Result<TOkB, __>.Error(fResult.FailedContext());
    }
}
