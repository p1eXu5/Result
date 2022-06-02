#nullable enable

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
    public static Task<Result<_, ValueTuple>> Retn<_>( this _ succeededContext )
        => Task.FromResult( Result<_, ValueTuple>.Success( succeededContext ) );

    /// <summary>
    /// Retn for TaskResult.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Task<Result<TSuccess, TError>> Retn<TSuccess, TError>( this Result<TSuccess, TError> result )
        => Task.FromResult( result );

    /// <summary>
    /// Retn for TaskResult.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Task<Result<TSuccess>> Retn<TSuccess>( this Result<TSuccess> result )
        => Task.FromResult( result );
}
