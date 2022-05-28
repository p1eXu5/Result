namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    /// <summary>
    /// TaskResult monad.
    /// </summary>
    /// <typeparam name="TSuccessA"></typeparam>
    /// <typeparam name="TSuccessB"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="task"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static async Task<Result<TSuccessB, __>> Bind<TSuccessA, TSuccessB, __>(
        this Task<Result<TSuccessA, __>> task,
        Func<TSuccessA, Task<Result<TSuccessB, __>>> f)
    {
        var result = await task;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result<TSuccessB, __>.Failure(result.FailedContext);
    }


    public static async Task<Result> Bind<TSuccessA>(this Task<Result<TSuccessA>> taskResult, Func<TSuccessA, Task<Result>> f)
    {
        var result = await taskResult;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result.Failure(result.FailedContext);
    }


    public static async Task<Result<TSuccessB>> Bind<TSuccessA, TSuccessB>(this ValueTask<Result<TSuccessA>> taskResult, Func<TSuccessA, ValueTask<Result<TSuccessB>>> f)
    {
        var result = await taskResult;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }


    public static async ValueTask<Result<TSuccessB>> BindV<TSuccessA, TSuccessB>(this ValueTask<Result<TSuccessA>> taskResult, Func<TSuccessA, Task<Result<TSuccessB>>> f)
    {
        var result = await taskResult;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }


    public static async Task<Result<TSuccessB>> Bind<TSuccessA, TSuccessB>(this Task<Result<TSuccessA>> taskResult, Func<TSuccessA, ValueTask<TSuccessB>> f)
    {
        var result = await taskResult;

        if (result.TryGetSucceededContext(out var sc))
        {
            return (await f(sc)).ToSuccessResult();
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }


    public static async Task<Result> Bind(
        this Task<Result> task,
        Func<Task<Result>> f)
    {
        var result = await task;

        if (result.Succeeded)
        {
            return await f();
        }

        return result;
    }


    public static async Task<Result<TSuccessB>> Bind<TSuccessA, TSuccessB>(
        this Task<Result<TSuccessA>> task,
        Func<TSuccessA, Task<Result<TSuccessB>>> f)
    {
        var result = await task;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }


    public static async Task<Result<TSuccessB, TErrorB>> Bind<TSuccessA, TSuccessB, TErrorA, TErrorB>(
        this Task<Result<TSuccessA, TErrorA>> task,
        Func<TSuccessA, Task<Result<TSuccessB, TErrorB>>> f, Func<TErrorA, TErrorB> fe)
    {
        var result = await task;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result<TSuccessB, TErrorB>.Failure(fe(result.FailedContext));
    }


    public static async Task<Result<TSuccessB, TErrorB>> Bibind<TSuccessA, TSuccessB, TErrorA, TErrorB>(
        this Task<Result<TSuccessA, TErrorA>> task,
        Func<TSuccessA, Task<Result<TSuccessB, TErrorB>>> fs, Func<TErrorA, Task<Result<TSuccessB, TErrorB>>> fe)
    {
        var result = await task;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await fs(sc);
        }

        return await fe(result.FailedContext);
    }


    public static async Task<Result> Bind<TSuccessA>(
        this Task<Result<TSuccessA, string>> task,
        Func<TSuccessA, Task<Result>> f)
    {
        var result = await task;

        if (result.TryGetSucceededContext(out var sc))
        {
            return await f(sc);
        }

        return Result.Failure(result.FailedContext);
    }


    public static async Task<Result<_, TErrorB>> BindError<_, TErrorA, TErrorB>(
        this Task<Result<_, TErrorA>> task,
        Func<TErrorA, Task<Result<_, TErrorB>>> f)
    {
        var result = await task;

        if (result.TryGetFailedContext(out var ec))
        {
            return await f(ec);
        }

        return Result<_, TErrorB>.Success(result.SuccessContext);
    }
}
