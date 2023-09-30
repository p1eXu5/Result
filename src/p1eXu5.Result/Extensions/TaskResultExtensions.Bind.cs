namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    //public static async Task<Result<TOkA, TError>> Bind<TOkA, TError>(
    //    this Task<Result<TOkA, TError>> taskResult,
    //    Func<TOkA, Task<Result<TOkA, TError>>> f)
    //{
    //    var result = await taskResult;

    //    if (result.TryGetSucceededContext(out var sc))
    //    {
    //        return await f(sc);
    //    }

    //    return Result<TOkA, TError>.Error(result.FailedContext);
    //}
    
    /// <summary>
    /// TaskResult monad.
    /// </summary>
    /// <typeparam name="TOkA"></typeparam>
    /// <typeparam name="TOkB"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="task"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static async Task<Result<TOkB, TError>> Bind<TOkA, TOkB, TError>(
        this Task<Result<TOkA, TError>> task,
        Func<TOkA, Task<Result<TOkB, TError>>> f)
    {
        var result = await task;

        if (result.TryGetSuccessContext(out var sc))
        {
            return await f(sc);
        }

        return new Result<TOkB, TError>.Error(result.FailedContext());
    }



    public static async Task<Result<TOkB, TError>> Bind<TOkA, TOkB, TError>(
        this ValueTask<Result<TOkA, TError>> taskResult, Func<TOkA, ValueTask<Result<TOkB, TError>>> f)
    {
        var result = await taskResult;

        if (result.TryGetSuccessContext(out var sc))
        {
            return await f(sc);
        }

        return new Result<TOkB, TError>.Error(result.FailedContext());
    }


    public static async ValueTask<Result<TOkB, TError>> BindV<TOkA, TOkB, TError>(
        this ValueTask<Result<TOkA, TError>> taskResult, Func<TOkA, Task<Result<TOkB, TError>>> f)
    {
        var result = await taskResult;

        if (result.TryGetSuccessContext(out var sc))
        {
            return await f(sc);
        }

        return new Result<TOkB, TError>.Error(result.FailedContext());
    }


    public static async Task<Result<TOkB, TError>> Bind<TOkA, TOkB, TError>(
        this Task<Result<TOkA, TError>> taskResult,
        Func<TOkA, ValueTask<TOkB>> f)
    {
        var result = await taskResult;

        if (result.TryGetSuccessContext(out var sc))
        {
            return (await f(sc)).ToOk<TOkB, TError>();
        }

        return new Result<TOkB, TError>.Error(result.FailedContext());
    }



    public static async Task<Result<TOkB, TErrorB>> Bind<TOkA, TOkB, TErrorA, TErrorB>(
        this Task<Result<TOkA, TErrorA>> task,
        Func<TOkA, Task<Result<TOkB, TErrorB>>> f, Func<TErrorA, TErrorB> fe)
    {
        var result = await task;

        if (result.TryGetSuccessContext(out var sc))
        {
            return await f(sc);
        }

        return new Result<TOkB, TErrorB>.Error(fe(result.FailedContext()));
    }


    public static async Task<Result<TOkB, TErrorB>> Bibind<TOkA, TOkB, TErrorA, TErrorB>(
        this Task<Result<TOkA, TErrorA>> task,
        Func<TOkA, Task<Result<TOkB, TErrorB>>> fs, Func<TErrorA, Task<Result<TOkB, TErrorB>>> fe)
    {
        var result = await task;

        if (result.TryGetSuccessContext(out var sc))
        {
            return await fs(sc);
        }

        return await fe(result.FailedContext());
    }



    public static async Task<Result<TOk, TErrorB>> BindError<TOk, TErrorA, TErrorB>(
        this Task<Result<TOk, TErrorA>> task,
        Func<TErrorA, Task<Result<TOk, TErrorB>>> f)
    {
        var result = await task;

        if (result.TryGetFailedContext(out var ec))
        {
            return await f(ec);
        }

        return new Result<TOk, TErrorB>.Ok(result.SuccessContext());
    }
}
