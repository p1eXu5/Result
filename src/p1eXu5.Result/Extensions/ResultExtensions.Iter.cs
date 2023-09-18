namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    public static Result<TOk, TError> Iter<TOk, TError>(
        this Result<TOk, TError> result,
        Action<TOk> action)
    {
        if (result is Result<TOk, TError>.Ok ok)
        {
            action(ok.SuccessContext);
        }

        return result;
    }


    public static async ValueTask<Result<TOk, TError>> IterAsync<TOk, TError>(this Result<TOk, TError> result, Func<TOk, ValueTask> actionAsync)
    {
        if (result is Result<TOk, TError>.Ok ok)
        {
            await actionAsync(ok.SuccessContext);
        }

        return result;
    }

    public static Result<TOk, TError> IterError<TOk, TError>(this Result<TOk, TError> result, Action<TError> f)
    {
        if (result is Result<TOk, TError>.Error err)
        {
            f(err.FailedContext);
        }

        return result;
    }
}
