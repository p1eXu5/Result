#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    public static Result<TSuccessA, TError> Filter<TSuccessA, TError>(
        this Result<TSuccessA, TError> result, Predicate<TSuccessA> filter, TError defaultError)
    {

        if (result.TryGetSucceededContext(out var s))
        {
            return
                filter(s)
                    ? result
                    : Result<TSuccessA, TError>.Failure(defaultError);
        }

        return result;
    }


    public static Result<TSuccessA, TError> FilterError<TSuccessA, TError>(
        this Result<TSuccessA, TError> result, Predicate<TError> filter, TSuccessA defaultError)
    {

        if (result.TryGetFailedContext(out var f))
        {
            return
                filter(f)
                    ? result
                    : Result<TSuccessA, TError>.Success(defaultError);
        }

        return result;
    }
}
