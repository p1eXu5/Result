namespace p1eXu5.Result.Extensions;

/// <summary>
/// TaskResult extensions.
/// </summary>
public static partial class TaskResultExtensions
{
    /// <summary>
    /// Retn for TaskResult.
    /// </summary>
    /// <typeparam name="_"></typeparam>
    /// <param name="succeededContext"></param>
    /// <returns></returns>
    public static Task<Result<TOk, TError>> Retn<TOk, TError>(this TOk succeededContext)
        => Task.FromResult<Result<TOk, TError>>(new Result<TOk, TError>.Ok(succeededContext));

    /// <summary>
    /// Retn for TaskResult.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Task<Result<TOk, TError>> Retn<TOk, TError>(this Result<TOk, TError> result)
        => Task.FromResult(result);
}
