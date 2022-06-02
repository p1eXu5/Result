#nullable enable

namespace p1eXu5.Result.Extensions;

public static partial class ResultExtensions
{
    public static Result<TSuccess> Iter<TSuccess>(this Result<TSuccess> result, Action<TSuccess> action)
    {
        if (result.TryGetSucceededContext(out TSuccess success))
        {
            action(success);
        }

        return result;
    }


    public static async ValueTask<Result<TSuccess>> IterAsync<TSuccess>(this Result<TSuccess> result, Func<TSuccess, ValueTask> actionAsync)
    {
        if (result.TryGetSucceededContext(out TSuccess success))
        {
            await actionAsync(success);
        }

        return result;
    }

    public static Result<TSuccess> IterError<TSuccess>(this Result<TSuccess> result, Func<string, TSuccess> f)
    {
        if (result.Succeeded)
        {
            return result;
        }

        f(result.FailedContext);

        return result;
    }
}
