namespace p1eXu5.Result.Extensions;

public static partial class TaskResultExtensions
{
    public static async Task<Result<TOkA, TError>> Filter<TOkA, TError>(
            this Task<Result<TOkA, TError>> task, Predicate<TOkA> filter, TError defaultError)
    {
        var result = await task;
        return result.Filter(filter, defaultError);
    }

    public static async Task<Result<TOkA, TError>> FilterError<TOkA, TError>(
        this Task<Result<TOkA, TError>> task, Predicate<TError> filter, TOkA defaultError)
    {
        var result = await task;
        return result.FilterError(filter, defaultError);
    }
}


