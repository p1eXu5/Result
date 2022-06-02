#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    /// <summary>
    /// <see cref="Result{TSuccess,TFailure}"/> monad.
    /// </summary>
    /// <typeparam name="_"></typeparam>
    /// <typeparam name="__"></typeparam>
    /// <param name="result"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public static Result<_, __> Bind<_, __>(this Result<_, __> result, Func<_, Result<_, __>> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return f(sc);
        }

        return result;
    }

    public static Result<TSuccessB> Bind<TSuccessA, TSuccessB>(this Result<TSuccessA> result, Func<TSuccessA, Result<TSuccessB>> f)
    {
        if (result.TryGetSucceededContext(out var sc))
        {
            return f(sc);
        }

        return Result.Failure<TSuccessB>(result.FailedContext);
    }


    public static Result<(TSuccessA, TSuccessB)> BindFlat<TSuccessA, TSuccessB>(this Result<TSuccessA> result, Func<Result<TSuccessB>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var scB))
            {
                return Result<(TSuccessA, TSuccessB)>.Success((scA, scB));
            }

            return Result<(TSuccessA, TSuccessB)>.Failure(resultB.FailedContext);
        }

        return Result<(TSuccessA, TSuccessB)>.Failure(result.FailedContext);
    }

    public static Result<(TSuccessA, TSuccessB, TSuccessC)> BindFlat<TSuccessA, TSuccessB, TSuccessC>(this Result<(TSuccessA, TSuccessB)> result, Func<Result<TSuccessC>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TSuccessA, TSuccessB, TSuccessC)>.Success((scA.Item1, scA.Item2, sc));
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC)>.Failure(resultB.FailedContext);
        }

        return Result<(TSuccessA, TSuccessB, TSuccessC)>.Failure(result.FailedContext);
    }

    public static Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)> BindFlat<TSuccessA, TSuccessB, TSuccessC, TSuccessD>(
        this Result<(TSuccessA, TSuccessB, TSuccessC)> result, Func<Result<TSuccessD>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)>.Success((scA.Item1, scA.Item2, scA.Item3, sc));
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)>.Failure(resultB.FailedContext);
        }

        return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)>.Failure(result.FailedContext);
    }

    public static Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)> BindFlat<TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE>(
        this Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD)> result, Func<Result<TSuccessE>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)>.Success((scA.Item1, scA.Item2, scA.Item3, scA.Item4, sc));
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)>.Failure(resultB.FailedContext);
        }

        return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)>.Failure(result.FailedContext);
    }

    public static Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)> BindFlat<TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF>(
        this Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE)> result, Func<Result<TSuccessF>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)>.Success((scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, sc));
            }

            return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)>.Failure(resultB.FailedContext);
        }

        return Result<(TSuccessA, TSuccessB, TSuccessC, TSuccessD, TSuccessE, TSuccessF)>.Failure(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6)> result, Func<Result<TS7>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)>.Success((scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)>.Failure(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)>.Failure(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7)> result, Func<Result<TS8>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)>.Success((scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)>.Failure(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)>.Failure(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8)> result, Func<Result<TS9>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)>.Success(
                    (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)>.Failure(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)>.Failure(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9)> result, Func<Result<TS10>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>.Success(
                    (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, scA.Item9, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>.Failure(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)>.Failure(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10)> result, Func<Result<TS11>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)>.Success(
                    (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, scA.Item9, scA.Item10, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)>.Failure(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11)>.Failure(result.FailedContext);
    }
}
