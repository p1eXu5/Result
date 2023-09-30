namespace p1eXu5.Result.Extensions;

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

public static partial class ResultExtensions
{
    public static Result<TOkB, TError> Bind<TOkA, TOkB, TError>(
        this Result<TOkA, TError> result,
        Func<TOkA, Result<TOkB, TError>> f) => result switch
    {
        Result<TOkA, TError>.Ok ok => f(ok.SuccessContext),
        Result<TOkA, TError>.Error err => new Result<TOkB, TError>.Error(err.FailedContext)
    };

    #region BindFlat

    public static Result<(TOkA, TOkB), TError> BindFlat<TOkA, TOkB, TError>(
        this Result<TOkA, TError> result,
        Func<Result<TOkB, TError>> f) => result switch
    {
        Result<TOkA, TError>.Ok okA =>
            f() switch
            {
                Result<TOkB, TError>.Ok okB => new Result<(TOkA, TOkB), TError>.Ok((okA.SuccessContext, okB.SuccessContext)),
                Result<TOkB, TError>.Error err => new Result<(TOkA, TOkB), TError>.Error(err.FailedContext)
            },
        Result<TOkA, TError>.Error err => new Result<(TOkA, TOkB), TError>.Error(err.FailedContext)
    };

    public static Result<(TOkA, TOkB, TOkC), TError> BindFlat<TOkA, TOkB, TOkC, TError>(
        this Result<(TOkA, TOkB), TError> result,
        Func<Result<TOkC, TError>> f) => result switch
    {
        Result<(TOkA, TOkB), TError>.Ok ok =>
            f() switch
            {
                Result<TOkC, TError>.Ok okC => new Result<(TOkA, TOkB, TOkC), TError>.Ok((ok.SuccessContext.Item1, ok.SuccessContext.Item2, okC.SuccessContext)),
                Result<TOkC, TError>.Error err => new Result<(TOkA, TOkB, TOkC), TError>.Error(err.FailedContext)
            },
        Result<(TOkA, TOkB), TError>.Error err => new Result<(TOkA, TOkB, TOkC), TError>.Error(err.FailedContext)
    };

    public static Result<(TOkA, TOkB, TOkC, TOkD), TError> BindFlat<TOkA, TOkB, TOkC, TOkD, TError>(
        this Result<(TOkA, TOkB, TOkC), TError> result,
        Func<Result<TOkD, TError>> f) => result switch
    {
        Result<(TOkA, TOkB, TOkC), TError>.Ok ok =>
            f() switch
            {
                Result<TOkD, TError>.Ok okD =>
                    new Result<(TOkA, TOkB, TOkC, TOkD), TError>.Ok(
                        (ok.SuccessContext.Item1, ok.SuccessContext.Item2, ok.SuccessContext.Item3, okD.SuccessContext)),
                Result<TOkD, TError>.Error err => new Result<(TOkA, TOkB, TOkC, TOkD), TError>.Error(err.FailedContext)
            },
        Result<(TOkA, TOkB, TOkC), TError>.Error err => new Result<(TOkA, TOkB, TOkC, TOkD), TError>.Error(err.FailedContext)
    };

    #endregion

    /*
    public static Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE), TError> BindFlat<TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE, TError>(
        this Result<(TOkA, TOkB, TSuccessC, TSuccessD), TError> result, Func<Result<TSuccessE, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE), TError>.Ok((scA.Item1, scA.Item2, scA.Item3, scA.Item4, sc));
            }

            return Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE), TError>.Error(resultB.FailedContext);
        }

        return Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE), TError>.Error(result.FailedContext);
    }

    public static Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE, TSuccessF), TError> BindFlat<TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE, TSuccessF, TError>(
        this Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE), TError> result, Func<Result<TSuccessF, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE, TSuccessF), TError>
                    .Ok((scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, sc));
            }

            return Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE, TSuccessF), TError>.Error(resultB.FailedContext);
        }

        return Result<(TOkA, TOkB, TSuccessC, TSuccessD, TSuccessE, TSuccessF), TError>.Error(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7), TError> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TError>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6), TError> result, Func<Result<TS7, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7), TError>
                    .Ok((scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7), TError>.Error(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7), TError>.Error(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8), TError> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TError>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7), TError> result, Func<Result<TS8, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8), TError>.Ok((scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8), TError>.Error(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8), TError>.Error(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9), TError> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TError>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8), TError> result, Func<Result<TS9, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9), TError>.Ok(
                    (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9), TError>.Error(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9), TError>.Error(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10), TError> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TError>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9), TError> result, Func<Result<TS10, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10), TError>.Ok(
                    (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, scA.Item9, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10), TError>.Error(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10), TError>.Error(result.FailedContext);
    }

    public static Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11), TError> BindFlat<TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11, TError>(
        this Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10), TError> result, Func<Result<TS11, TError>> f)
    {
        if (result.TryGetSucceededContext(out var scA))
        {
            var resultB = f();
            if (resultB.TryGetSucceededContext(out var sc))
            {
                return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11), TError>.Ok(
                    (scA.Item1, scA.Item2, scA.Item3, scA.Item4, scA.Item5, scA.Item6, scA.Item7, scA.Item8, scA.Item9, scA.Item10, sc));
            }

            return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11), TError>.Error(resultB.FailedContext);
        }

        return Result<(TS1, TS2, TS3, TS4, TS5, TS6, TS7, TS8, TS9, TS10, TS11), TError>.Error(result.FailedContext);
    }

    */
}
