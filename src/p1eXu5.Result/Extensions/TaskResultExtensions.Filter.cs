namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    public static async Task<Result<TSuccessA, TError>> Filter<TSuccessA, TError>(
            this Task<Result<TSuccessA, TError>> task, Predicate<TSuccessA> filter, TError defaultError)
    {
        var result = await task;
        return result.Filter(filter, defaultError);
    }

    public static async Task<Result<TSuccessA, TError>> FilterError<TSuccessA, TError>(
        this Task<Result<TSuccessA, TError>> task, Predicate<TError> filter, TSuccessA defaultError)
    {
        var result = await task;
        return result.FilterError(filter, defaultError);
    }
}


