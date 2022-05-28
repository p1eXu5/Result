#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    // ==================
    //        Map
    // ==================

    /// <summary>
    /// <see cref="Result{TSuccess,TFailure}"/> functor.
    /// </summary>
    /// <typeparam name="TSuccessA"></typeparam>
    /// <typeparam name="TSuccessB"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="result"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Result<TSuccessB, __> Map<TSuccessA, TSuccessB, __>(this Result<TSuccessA, __> result, Func<TSuccessA, TSuccessB> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return Result<TSuccessB, __>.Success(f(sc));
        }

        return Result<TSuccessB, __>.Failure(result.FailedContext);
    }

    /// <summary>
    /// <see cref="Result{TSuccess}"/> functor.
    /// </summary>
    /// <typeparam name="TSuccessA"></typeparam>
    /// <typeparam name="TSuccessB"></typeparam>
    /// <param name="result"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Result<TSuccessB> Map<TSuccessA, TSuccessB>(this Result<TSuccessA> result, Func<TSuccessA, TSuccessB> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return Result<TSuccessB>.Success(f(sc));
        }

        return Result<TSuccessB>.Failure(result.FailedContext);
    }

    public static Result<TS4> Map<TS1, TS2, TS3, TS4>(this Result<(TS1, TS2, TS3)> result, Func<TS1, TS2, TS3, TS4> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return Result<TS4>.Success(f(sc.Item1, sc.Item2, sc.Item3));
        }

        return Result<TS4>.Failure(result.FailedContext);
    }

    public static Result<TS5> Map<TS1, TS2, TS3, TS4, TS5>(this Result<(TS1, TS2, TS3, TS4)> result, Func<TS1, TS2, TS3, TS4, TS5> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return Result<TS5>.Success(f(sc.Item1, sc.Item2, sc.Item3, sc.Item4));
        }

        return Result<TS5>.Failure(result.FailedContext);
    }

    public static Result<TS11> Map<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> result,
        Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11> f )
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return Result<TS11>.Success(f(sc.Item1, sc.Item2, sc.Item3, sc.Item4, sc.Item5, sc.Item6, sc.Item7, sc.Item8, sc.Item9, sc.Item10));
        }

        return Result<TS11>.Failure(result.FailedContext);
    }


    // ==================
    //      MapFlat
    // ==================

    public static Result<TS11> MapFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> result, 
        Func<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, Result<TS11>> f )
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return f(sc.Item1, sc.Item2, sc.Item3, sc.Item4, sc.Item5, sc.Item6, sc.Item7, sc.Item8, sc.Item9, sc.Item10);
        }

        return Result<TS11>.Failure(result.FailedContext);
    }


    // ==================
    //      MapError
    // ==================

    /// <summary>
    /// <see cref="Result{TSuccess,TFailure}"/> error functor.
    /// </summary>
    /// <typeparam name="_"></typeparam>
    /// <typeparam name="TErrorA"></typeparam>
    /// <typeparam name="TErrorB"></typeparam>
    /// <param name="result"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Result<_, TErrorB> MapError<_, TErrorA, TErrorB>(this Result<_, TErrorA> result, Func<TErrorA, TErrorB> f)
    {
        if (result.TryGetFailedContext(out var fc))
        {
            return Result<_, TErrorB>.Failure(f(fc));
        }

        return Result<_, TErrorB>.Success(result.SuccessContext);
    }

    public static Result<_, TErrorB> MapError<_, TErrorB>(this Result<_> result, Func<string, TErrorB> f)
    {
        if (result.TryGetFailedContext(out var fc))
        {
            return Result<_, TErrorB>.Failure(f(fc));
        }

        return Result<_, TErrorB>.Success(result.SuccessContext);
    }

    public static Result<TSuccess> MapErrorToSuccess<TSuccess>(this Result<TSuccess> result, Func<string, TSuccess> f)
    {
        if (result.Succeeded)
        {
            return result;
        }

        return Result<TSuccess>.Success(f(result.FailedContext));
    }


    // ==================
    //       Bimap
    // ==================

    public static Result<TSuccessB, TErrorB> Bimap<TSuccessA, TSuccessB, TErrorA, TErrorB>( this Result<TSuccessA, TErrorA> result, 
                                                                                            Func<TSuccessA, TSuccessB> fs, 
                                                                                            Func<TErrorA, TErrorB> fe )
    {

        if (result.TryGetSucceededContext(out var s))
        {
            return Result<TSuccessB, TErrorB>.Success(fs(s));
        }

        return Result<TSuccessB, TErrorB>.Failure(fe(result.FailedContext));
    }

    // ==================
    //      MapTask
    // ==================

    public static Task<Result<TSuccessB>> MapTask<TSuccessA, TSuccessB>(this Result<TSuccessA> result, Func<TSuccessA, Task<Result<TSuccessB>>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            return f(scA);
        }
        return Task.FromResult(Result<TSuccessB>.Failure(result.FailedContext));
    }


    public static Task<Result> MapTask<TSuccessA>(this Result<TSuccessA> result, Func<TSuccessA, Task<Result>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            return f(scA);
        }
        return Task.FromResult(Result.Failure(result.FailedContext));
    }
}


